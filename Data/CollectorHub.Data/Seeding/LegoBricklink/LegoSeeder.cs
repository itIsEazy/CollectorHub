namespace CollectorHub.Data.Seeding.LegoBricklink
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;

    using CollectorHub.Common;
    using CollectorHub.Data.Models.Collections.Lego;

    public class LegoSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.LegoTypes.Any())
            {
                this.SeedLegoTypes(dbContext);
            }

            if (dbContext.LegoMinifigures.Any())
            {
                return;
            }
            else
            {
                this.SeedSomeMinifigures(dbContext);
            }
        }

        public void SeedLegoTypes(ApplicationDbContext db)
        {
            var type = new LegoType();
            type.Name = GlobalConstants.LegoStarWarsTypeName;
            type.ImageUrl = "https://static.wikia.nocookie.net/hotwheels/images/5/5a/2014.jpg";

            db.LegoTypes.Add(type);

            db.SaveChanges();
        }

        private void SeedSomeMinifigures(ApplicationDbContext db)
        {
            var list = new List<string>
            {
                "0314 50 n",
                "0380 50 n",
                "0330 50 n",
                "0202 50 n",
                "0223 50 n",
                "0201 50 n",
                "0378 50 n",
                "0837 50 n",
                "0492 50 n",
                "0297 50 n",
            };

            var collectedMinifigs = new List<LegoMinifigure>();
            var legoStarWarsTypeId = db.LegoTypes.Where(x => x.Name == GlobalConstants.LegoStarWarsTypeName).Select(x => x.Id).FirstOrDefault();

            foreach (var row in list)
            {
                var minifig = new LegoMinifig(row);
                Console.WriteLine(minifig.CollectInformation().Result);

                collectedMinifigs.Add(new LegoMinifigure
                {
                    SwNumber = minifig.SwNumber,
                    Name = minifig.Name,
                    WeightInGrams = double.Parse(minifig.WeightInGrams.ToString()),
                    AvgPriceNew = minifig.AvgPriceNew,
                    AvgPriceUsed = minifig.AvgPriceUsed,
                    ProductionYear = minifig.ProductionYear,
                    LegoTypeId = legoStarWarsTypeId,
                });

            }

            Console.WriteLine("yey");

            db.LegoMinifigures.AddRange(collectedMinifigs);

            db.SaveChanges();
        }
    }

    public class LegoMinifig
    {
        private string brickLinkCatalogLink = "https://www.bricklink.com/v2/catalog/catalogitem.page?M=sw";
        private string brickLinkPriceGuideLink = "https://www.bricklink.com/catalogPG.asp?M=sw";
        private string conditionNEW = "NEW";
        private string conditionUSED = "USED";

        public LegoMinifig(string information)
        {
            string[] informationSplitted = information.Split().ToArray();
            string swNumber = informationSplitted[0];
            this.PriceBoughted = decimal.Parse(informationSplitted[1]);
            string condition = informationSplitted[2];
            if (condition == "n")
            {
                this.Condition = this.conditionNEW;
            }
            else
            {
                this.Condition = this.conditionUSED;
            }

            this.SwNumber = swNumber;
            this.CatalogLink = brickLinkCatalogLink + swNumber;
            this.PriceGuideLink = brickLinkPriceGuideLink + swNumber;
        }

        public string SwNumber { get; set; } // maybe not needed sw0002 => only 0002 
        public string CatalogLink { get; set; } // https://www.bricklink.com/v2/catalog/catalogitem.page?M=sw0454
        public string PriceGuideLink { get; set; } // https://www.bricklink.com/catalogPG.asp?M=sw0454
        public string Name { get; set; } // 
        public decimal PriceBoughted { get; set; } // IN LEVA
        public decimal PriceNow { get; set; } // IN LEVA
        public decimal AvgPriceNew { get; set; }
        public decimal AvgPriceUsed { get; set; }
        public decimal Profit { get; set; }
        public int ProductionYear { get; set; } // 
        public decimal WeightInGrams { get; set; } // shows the weight in grams
        public string Condition { get; set; } // NEW / USED
        public List<decimal> BrickLinkPrices { get; set; }


        public async Task<string> CollectInformation()
        {
            var sb = new StringBuilder();

            Task task = Task.Run(() => CallPriceGuideLink(this.PriceGuideLink));
            Thread.Sleep(5000);

            task = Task.Run(() => CallCatalogLink(this.CatalogLink));
            Thread.Sleep(5000);

            CalculateProfit();
            CalculatePriceNow();

            sb.AppendLine("Link : " + this.CatalogLink);
            sb.AppendLine("Production Year : " + this.ProductionYear.ToString());
            sb.AppendLine("Name : " + this.Name);
            sb.AppendLine("Condition : " + this.Condition);
            sb.AppendLine("Money i spend : " + this.PriceBoughted);
            sb.AppendLine("Avg Price NEW : " + this.AvgPriceNew);
            sb.AppendLine("Avg Price USED : " + this.AvgPriceUsed);
            sb.AppendLine("Profit : " + this.Profit);
            sb.AppendLine("Weight : " + this.WeightInGrams);

            return sb.ToString();
        }

        private async Task<string> CallPriceGuideLink(string link)
        {
            Console.WriteLine(this.SwNumber + " " + "Collecting Name and Prices ...");

            using var client = new HttpClient();
            //var result = client.GetAsync(link);
            var content = await client.GetStringAsync(link);

            string tablePattern = @"<TABLE.*>.*<\/TABLE>";
            Regex regex = new Regex(tablePattern);

            var tableMatch = regex.Match(content);

            string trPattern = @"<TR.*?>.*?<\/TR>";
            regex = new Regex(trPattern);

            var trMatches = regex.Matches(tableMatch.ToString());

            //Console.WriteLine(content);
            //Console.WriteLine(result.StatusCode);
            //Console.WriteLine(tableMatch);
            //Console.WriteLine(link);

            var itemsWeNeed = new List<string>
            {
                "Total Qty",
                "Min Price",
                "Avg Price",
                "Qty Avg Price",
                "Max Price",
            };

            int counter = 0; // we create this and when it hits 20 we know 
            bool informationIsCollected = false;
            string pricesPattern = @">[0-9]+<|;*[0-9]+.*?[0-9]*<";

            List<decimal> pricesList = new List<decimal>();

            foreach (Match trMatch in trMatches)
            {
                if (informationIsCollected)
                {
                    break;
                }

                foreach (var item in itemsWeNeed)
                {
                    if (informationIsCollected)
                    {
                        break;
                    }

                    if (trMatch.ToString().Contains(item))
                    {
                        //Console.WriteLine(trMatch);
                        //Console.WriteLine();
                        counter++;
                        regex = new Regex(pricesPattern);

                        var priceMatch = regex.Match(trMatch.ToString());
                        string priceMatchInString = priceMatch.ToString().Substring(1, priceMatch.ToString().Length - 2);
                        priceMatchInString = priceMatchInString.Replace(".", ",");
                        decimal priceMatchInDecimal = decimal.Parse(priceMatchInString);
                        pricesList.Add(priceMatchInDecimal);

                        //Console.WriteLine(priceMatchInString);
                        //Console.WriteLine(counter - 1);

                        if (counter == 24) // so we get avg price 2ce idk why so we will just skip the seconnd one :/ /..
                        {
                            informationIsCollected = true;
                        }
                    }
                }
            }

            string bTagPattern = @"<B>.*?<\/B>";
            regex = new Regex(bTagPattern);

            var bTagMatch = regex.Match(content);

            string name = bTagMatch.ToString().Replace("<B>", "").Replace("</B>", "");

            string GetOnlyNamePattern = @"[A-z -]+";
            regex = new Regex(GetOnlyNamePattern);
            var nameCleared = regex.Match(name.ToString());

            this.Name = nameCleared.ToString();
            this.BrickLinkPrices = pricesList;
            this.AvgPriceNew = pricesList[14];
            this.AvgPriceUsed = pricesList[20];

            return "Name And Price Collected";
        }

        private async Task<string> CallCatalogLink(string link)
        {
            Console.WriteLine(this.SwNumber + " " + "Collecting Year and Weight ...");

            using var client = new HttpClient();
            var content = await client.GetStringAsync(link);

            //string imgTagPattern = @"<img valign.*>";
            string pattern = "<span id=\"yearReleasedSec\".*>";
            string priceAndWeight = "";
            int productionYear = 0;
            decimal weightInGrams = 0;

            Regex regex = new Regex(pattern);
            var match = regex.Match(content);

            pattern = @">[0-9]*<|>[\d]+\.*[\d]*[\w]+<";
            regex = new Regex(pattern);
            var matches = regex.Matches(match.ToString());

            // get only the prices 
            foreach (Match currMatch in matches)
            {
                priceAndWeight += currMatch.ToString().Substring(1, currMatch.ToString().Length - 2);
                priceAndWeight = priceAndWeight.Replace(".", ",");
                priceAndWeight = priceAndWeight.Replace("g", "");
                priceAndWeight += " ";
            }

            string[] priceAndWeightSplitted = priceAndWeight.Split(" ").ToArray();

            productionYear = int.Parse(priceAndWeightSplitted[0]);
            weightInGrams = decimal.Parse(priceAndWeightSplitted[1]);

            //Console.WriteLine(productionYear);
            //Console.WriteLine(weightInGrams);

            this.ProductionYear = productionYear;
            this.WeightInGrams = weightInGrams;

            return "Year And Grams Collected";

            //Console.Write(".");
        }

        private string CalculateProfit()
        {
            if (this.Condition == this.conditionNEW)
            {
                this.Profit = this.AvgPriceNew - this.PriceBoughted;
            }
            else
            {
                this.Profit = this.AvgPriceUsed - this.PriceBoughted;
            }

            return "Profit Calculated";
        }
        private string CalculatePriceNow()
        {
            if (this.Condition == this.conditionNEW)
            {
                this.PriceNow = this.AvgPriceNew;
            }
            else
            {
                this.PriceNow = this.AvgPriceUsed;
            }

            return "PriceNow Calculated";
        }
    }
}
