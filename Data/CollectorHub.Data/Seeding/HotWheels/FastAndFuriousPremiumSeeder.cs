namespace CollectorHub.Data.Seeding.HotWheels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using CollectorHub.Data.Models.HotWheels;

    public class FastAndFuriousPremiumSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.FastAndFuriousPremiumCars.Any())
            {
                return;
            }
            else
            {
                await this.CallForHotWHeels(dbContext);
            }
        }

        public async Task<string> CallForHotWHeels(ApplicationDbContext db) // shoud use try catch
        {
            var sb = new StringBuilder();

            string fandFPremiumLink = "https://hotwheels.fandom.com/wiki/Fast_%26_Furious_Premium_Series";
            //// string fandFBasicLink = "https://hotwheels.fandom.com/wiki/Fast_%26_Furious_Series";

            // string FandFPremiumLinkPath = @"C:\Users\35989\Desktop\CollectorHub\Tests\Sandbox\TestFiles\HotWheelsFFPremiumHTML.txt"
            // string FandFPremiumHTML = File.ReadAllText(FandFPremiumLinkPath);
            string html = await this.CallSiteReturnHTML(fandFPremiumLink);

            string htmlForAllCars = html.Replace("\n", string.Empty);

            sb.AppendLine("Fast and Furious Premium Link Called");

            List<FastAndFuriousPremiumSerie> allPremiumHWSeries = this.CollectYearAndNames(html);
            List<FastAndFuriousPremiumCar> allPremiumHWCars = this.CollectInfoForCars(htmlForAllCars);

            int indexer = 0;
            foreach (var serie in allPremiumHWSeries)
            {
                for (int i = 0; i < 5; i++)
                {
                    allPremiumHWCars[indexer].Serie = serie;
                    serie.Cars.Add(allPremiumHWCars[indexer]);
                    indexer++;
                }
            }

            foreach (var serie in allPremiumHWSeries)
            {
                await db.FastAndFuriousPremiumSeries.AddAsync(serie);
            }

            foreach (var car in allPremiumHWCars)
            {
                await db.FastAndFuriousPremiumCars.AddAsync(car);
            }

            await db.SaveChangesAsync();

            sb.AppendLine("Series Years and Names collected and models merged");

            return sb.ToString();
        }

        public List<FastAndFuriousPremiumCar> CollectInfoForCars(string html)
        {
            List<FastAndFuriousPremiumCar> allCars = new List<FastAndFuriousPremiumCar>();

            // string tdInfoPattern = @"<td>.*?<\/td>";
            var mathes = this.GetMatchesFrom(html, @"<td>.*?<\/td>");

            List<string> clearInformation = this.ClearTheInfoForCars(mathes);

            FastAndFuriousPremiumCar currCar = new FastAndFuriousPremiumCar();
            FastAndFuriousPremiumCar lastCar = new FastAndFuriousPremiumCar();
            lastCar.ToyId = "this is id to not dail null ref except";

            int counter = 1; // counts ++ until last model column
            foreach (var item in clearInformation)
            {
                // there are newly added cars that brake the logic so we will just ignore them for now
                if (allCars.Count == 55)
                {
                    break;
                }

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
                    currCar = new FastAndFuriousPremiumCar();
                }

                counter++;
            }

            return allCars;
        }

        public List<string> ClearTheInfoForCars(MatchCollection mathes)
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
                    var links = this.GetMatchesFrom(match.ToString(), httpsPattern);
                    foreach (Match link in links)
                    {
                        infoRow = link.ToString(); // we catch to exact same links so we get the first as the info and break
                        break;
                    }

                    resultList.Add(infoRow);
                    continue;
                }

                infoRow = this.ReplaceTagsWithStringEmpty(match.ToString());

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

        public List<FastAndFuriousPremiumSerie> CollectYearAndNames(string html)
        {
            List<string> yearsAndNames = new List<string>();

            string yearNamePattern = @"<span class=.mw-headline. id=..*.>.*<\/span>"; // dots replace "
            var mathes = this.GetMatchesFrom(html, yearNamePattern);

            foreach (Match match in mathes)
            {
                string yearOrName = this.ReplaceTagsWithStringEmpty(match.ToString()); // replacing all tags and so on with string.Empty to extract only name or year
                if (yearOrName != "External Links") //// This is not year or name
                {
                    yearsAndNames.Add(yearOrName);
                }

                // Console.WriteLine(match);
            }

            List<FastAndFuriousPremiumSerie> allPremiumHWSeries = new List<FastAndFuriousPremiumSerie>();

            int yearCounter = 0;
            int orderCounter = 1;
            string year = string.Empty;

            // if counter is 0 item is Year !
            // if counter is 5 => counter back to 0 !
            foreach (var item in yearsAndNames)
            {
                // newly added series that brakes the current logil WILL BE REFACTORED
                if (allPremiumHWSeries.Count == 11)
                {
                    break;
                }

                if (yearCounter == 0 || yearCounter == 6)
                {
                    yearCounter = 0;
                    year = item;
                    yearCounter++;
                    continue;
                }

                FastAndFuriousPremiumSerie currSerie = new FastAndFuriousPremiumSerie();

                currSerie.Year = year;
                currSerie.Name = item;
                currSerie.OrderOfApperance = orderCounter;

                allPremiumHWSeries.Add(currSerie);

                yearCounter++;
                orderCounter++;
            }

            return allPremiumHWSeries;
        }

        public MatchCollection GetMatchesFrom(string html, string pattern)
        {
            Regex regex = new Regex(pattern);
            return regex.Matches(html);
        }

        public string ReplaceTagsWithStringEmpty(string content)
        {
            return Regex.Replace(content, "<.*?>", string.Empty);
        }

        public async Task<string> CallSiteReturnHTML(string link)
        {
            using var client = new HttpClient();

            var content = await client.GetStringAsync(link);

            return content;
        }
    }
}
