using AngleSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CityAreasScraping

{
    class Program
    {
        static async Task Main()
        {
            /*default settings
                //Use the default configuration for AngleSharp
                var config = Configuration.Default;

                //Create a new context for evaluating webpages with the given config
                var context = BrowsingContext.New(config);

                //Source to be parsed
                var source = "<h1>Some example source</h1><p>This is a paragraph element";

                //Create a virtual request to specify the document to load (here from our fixed string)
                var document = await context.OpenAsync(req => req.Content(source));

                //Do something with document like the following
                Console.WriteLine("Serializing the (original) document:");
                Console.WriteLine(document.DocumentElement.OuterHtml);

                var p = document.CreateElement("p");
                p.TextContent = "This is another paragraph.";

                Console.WriteLine("Inserting another element in the body ...");
                document.Body.AppendChild(p);

                Console.WriteLine("Serializing the document again:");
                Console.WriteLine(document.DocumentElement.OuterHtml);
            */


            /*
             first example list of areas from moph.gov
            public async Task ImportCityAreas()
        {
            var document = this.context.OpenAsync("https://www.moph.gov.qa/english/OurServices/eservices/Pages/Health-Facilities.aspx")
                .GetAwaiter()
                .GetResult();

            var cityAreas = document.QuerySelectorAll("#ddlAreas")
                .Select(x => x.TextContent)
                .FirstOrDefault()
                .Split("\n\t\t\t\t\t\t\t", StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            foreach (var area in cityAreas.Skip(1))
            {
                Console.WriteLine(area.Trim());
            }
        }
             */



            //second example for insurance companies:
            //Use the default configuration for AngleSharp
            var config = Configuration.Default.WithDefaultLoader();

            //Create a new context for evaluating webpages with the given config
            var context = BrowsingContext.New(config);

            //Create a virtual request to specify the document to load (here from direct url)
            var document = await context.OpenAsync("https://dohaclinichospital.com/content/Insurance-Companies-2");

            var cityAreas = document.QuerySelectorAll(".content-sub > p")
                .Select(x=>x.TextContent)
                .ToList();

            foreach (var area in cityAreas.Skip(3))
            {
                Console.WriteLine(area.Trim());
            }

            //imageUrl example
            //var imageUrl = document.QuerySelector("#newsGal > div.image > img").GetAttribute("src");
            //Console.WriteLine(imageUrl);
        }
    }
}
