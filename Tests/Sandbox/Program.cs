namespace Sandbox
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using CollectorHub.Data;
    using CollectorHub.Data.Common;
    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models;
    using CollectorHub.Data.Repositories;
    using CollectorHub.Data.Seeding;
    using CollectorHub.Services.Data;
    using CollectorHub.Services.Messaging;

    using CommandLine;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public static class Program
    {
        public class PremiumHWCarDTO
        {
            public int Id { get; set; }

            public string Col { get; set; }

            public string ToyId { get; set; }

            public string Name { get; set; }

            public string Color { get; set; }

            public string Tampos { get; set; }

            public string WheelType { get; set; }

            public string Movie { get; set; }

            public string Notes { get; set; }

            public string PhotoLooseLink { get; set; }

            public string PhotoCardLink { get; set; }

            public int SerieId { get; set; }

            public PremiumHWSerie Serie { get; set; }
        }

        public class PremiumHWSerie
        {
            public PremiumHWSerie()
            {
                this.Cars = new HashSet<PremiumHWCarDTO>();
            }

            public int Id { get; set; }

            public string Year { get; set; }

            public string Name { get; set; }

            public int OrderOfApperance { get; set; }

            public ICollection<PremiumHWCarDTO> Cars { get; set; }
        }

        public static async Task<int> Main(string[] args)
        {

            ClearSomeIgNames();


            // Console.WriteLine(await CallForHotWHeels());
            return 0;

            Console.WriteLine($"{typeof(Program).Namespace} ({string.Join(" ", args)}) starts working...");
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider(true);

            // Seed data on application startup
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            using (var serviceScope = serviceProvider.CreateScope())
            {
                serviceProvider = serviceScope.ServiceProvider;

                return Parser.Default.ParseArguments<SandboxOptions>(args).MapResult(
                    opts => SandboxCode(opts, serviceProvider).GetAwaiter().GetResult(),
                    _ => 255);
            }
        }

        public static void ClearSomeIgNames()
        {
            List<string> names = new List<string>();

            int counter = 1;
            while (true)
            {
                string input = Console.ReadLine();

                if (counter == 2)
                {
                    names.Add(input);
                    counter = 1;
                    continue;
                }

                if (input.Contains("Снимката на профила на"))
                {
                    counter = 2;
                }

                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }
            }

            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }

        public static async Task<string> CallForHotWHeels() // shoud use try catch
        {
            var sb = new StringBuilder();

            string FandFPremiumLink = "https://hotwheels.fandom.com/wiki/Fast_%26_Furious_Premium_Series";
            string FandFBasicLink = "https://hotwheels.fandom.com/wiki/Fast_%26_Furious_Series";

            // string FandFPremiumLinkPath = @"C:\Users\35989\Desktop\CollectorHub\Tests\Sandbox\TestFiles\HotWheelsFFPremiumHTML.txt";
            // string FandFPremiumHTML = File.ReadAllText(FandFPremiumLinkPath);

            string html = await CallSiteReturnHTML(FandFPremiumLink);

            string htmlForAllCars = html.Replace("\n", string.Empty);

            sb.AppendLine("Fast and Furious Premium Link Called");

            List<PremiumHWSerie> allPremiumHWSeries = CollectYearAndNames(html);
            List<PremiumHWCarDTO> allCars = CollectInfoForCars(htmlForAllCars);

            int indexer = 0;
            foreach (var serie in allPremiumHWSeries)
            {
                for (int i = 0; i < 5; i++)
                {
                    allCars[indexer].Serie = serie;
                    serie.Cars.Add(allCars[indexer]);
                    indexer++;
                }
            }

            sb.AppendLine("Series Years and Names collected and models merged");

            return sb.ToString();
        }

        public static List<PremiumHWCarDTO> CollectInfoForCars(string html)
        {
            List<PremiumHWCarDTO> allCars = new List<PremiumHWCarDTO>();

            string tdInfoPattern = @"<td>.*?<\/td>";
            var mathes = GetMatchesFrom(html, tdInfoPattern);

            List<string> clearInformation = ClearTheInfoForCars(mathes);

            PremiumHWCarDTO currCar = new PremiumHWCarDTO();
            PremiumHWCarDTO lastCar = new PremiumHWCarDTO();
            lastCar.ToyId = "this is id to not dail null ref except";

            int counter = 1; // counts ++ until last model column
            foreach (var item in clearInformation)
            {
                if (counter == 11)
                {
                    counter = 1;
                }

                if (counter == 1)
                {
                    currCar.Col = item;
                }

                if (counter == 2)
                {
                    currCar.ToyId = item;
                }

                if (counter == 3)
                {
                    currCar.Name = item;
                }

                if (counter == 4)
                {
                    currCar.Color = item;
                }

                if (counter == 5)
                {
                    currCar.Tampos = item;
                }

                if (counter == 6)
                {
                    currCar.WheelType = item;
                }

                if (counter == 7)
                {
                    currCar.Movie = item;
                }

                if (counter == 8)
                {
                    currCar.Notes = item;
                }

                if (counter == 9)
                {
                    currCar.PhotoLooseLink = item;
                }

                if (counter == 10)
                {
                    currCar.PhotoCardLink = item;

                    if (currCar.ToyId != lastCar.ToyId)
                    {
                        allCars.Add(currCar);
                    }

                    lastCar = currCar;
                    currCar = new PremiumHWCarDTO();
                }

                counter++;
            }

            return allCars;
        }

        public static List<string> ClearTheInfoForCars(MatchCollection mathes)
        {
            int counter = 0;

            List<string> resultList = new List<string>();

            string httpsPattern = @"https:.*?.(png|JPG|jpg|PNG)";

            foreach (Match match in mathes)
            {
                if (counter == 200)
                {
                    Console.WriteLine();
                }

                string infoRow = string.Empty;

                if (match.ToString().Contains("https:"))
                {
                    var links = GetMatchesFrom(match.ToString(), httpsPattern);
                    foreach (Match link in links)
                    {
                        infoRow = link.ToString(); // we catch to exact same links so we get the first as the info and break
                        break;
                    }

                    resultList.Add(infoRow);
                    continue;
                }

                infoRow = ReplaceTagsWithStringEmpty(match.ToString());

                if (infoRow.Contains("\n"))
                {
                    infoRow = infoRow.Replace("\n", string.Empty);
                }

                if (infoRow.Contains("&amp;"))
                {
                    infoRow = infoRow.Replace("&amp;", "and");
                }

                resultList.Add(infoRow);

                counter++;
            }

            return resultList;
        }

        public static List<PremiumHWSerie> CollectYearAndNames(string html)
        {
            List<string> yearsAndNames = new List<string>();

            string yearNamePattern = @"<span class=.mw-headline. id=..*.>.*<\/span>"; // dots replace " 
            var mathes = GetMatchesFrom(html, yearNamePattern);

            foreach (Match match in mathes)
            {
                string yearOrName = ReplaceTagsWithStringEmpty(match.ToString()); // replacing all tags and so on with string.Empty to extract only name or year
                if (yearOrName != "External Links") // This is not year or name
                {
                    yearsAndNames.Add(yearOrName);
                }

                // Console.WriteLine(match);
            }

            List<PremiumHWSerie> allPremiumHWSeries = new List<PremiumHWSerie>();

            int yearCounter = 0;
            int orderCounter = 1;
            string year = string.Empty;

            // if counter is 0 item is Year !
            // if counter is 5 => counter back to 0 !
            foreach (var item in yearsAndNames)
            {
                if (yearCounter == 0 || yearCounter == 6)
                {
                    yearCounter = 0;
                    year = item;
                    yearCounter++;
                    continue;
                }

                PremiumHWSerie currSerie = new PremiumHWSerie();

                currSerie.Year = year;
                currSerie.Name = item;
                currSerie.OrderOfApperance = orderCounter;

                allPremiumHWSeries.Add(currSerie);

                yearCounter++;
                orderCounter++;
            }

            return allPremiumHWSeries;
        }

        public static MatchCollection GetMatchesFrom(string html, string pattern)
        {
            Regex regex = new Regex(pattern);
            return regex.Matches(html);
        }

        public static string ReplaceTagsWithStringEmpty(string content)
        {
            return Regex.Replace(content, "<.*?>", string.Empty);
        }

        public static async Task<string> CallSiteReturnHTML(string link)
        {
            using var client = new HttpClient();

            var content = await client.GetStringAsync(link);

            return content;
        }

        private static async Task<int> SandboxCode(SandboxOptions options, IServiceProvider serviceProvider)
        {
            var sw = Stopwatch.StartNew();

            var settingsService = serviceProvider.GetService<ISettingsService>();
            Console.WriteLine($"Count of settings: {settingsService.GetCount()}");

            Console.WriteLine(sw.Elapsed);
            return await Task.FromResult(0);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                    .UseLoggerFactory(new LoggerFactory()));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
        }
    }
}
