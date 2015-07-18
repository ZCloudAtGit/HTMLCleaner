using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ivony.Html;
using Ivony.Html.Parser;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var htmlString = "<p>&lt;string&gt;</p>";
            var parser = new JumonyParser();
            var doc = parser.Parse(htmlString);
            var firstElement = doc.Elements("*").First();
            var text = firstElement.InnerHtml();
            Console.Write(text);
            firstElement.ClearNodes();
            
            firstElement.AddTextNode(text);
            var strBuilder = new StringBuilder(htmlString.Length);
            using (var textWriter = new StringWriter(strBuilder))
            {
                doc.Render(textWriter);
            }
            Console.Write(strBuilder.ToString());
            Console.ReadKey();
        }
    }
}
