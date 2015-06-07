using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ivony.Html;
using Ivony.Html.Parser;

namespace HTMLClean
{
    public class Cleaner
    {
        public static void CleanWordHTML(ref string htmlString)
        {
            var parser = new JumonyParser();
            var doc = parser.Parse(htmlString);
            var allNodes = doc.DescendantNodes();
            var nodeToRemove = new List<IHtmlElement>();
            var nodeToCombineAsText = new List<IHtmlElement>();
            foreach (var node in allNodes)
            {
                if (node is IHtmlElement)
                {
                    var element = node as IHtmlElement;
                    if (element.Name == "meta" || element.Name == "style")
                    {
                        nodeToRemove.Add(element);
                    }
                    else if (element.Name == "p" || element.Name == "li" || element.Name=="h2" || element.Name=="h3" || element.Name=="h4" )
                    {
                        var text = element.InnerText();
                        nodeToCombineAsText.Add(element);
                    }
                    else if(element.Name == "table")
                    {
                        element.RemoveAttribute("border");
                        element.RemoveAttribute("cellpadding");
                        element.RemoveAttribute("width");
                        element.RemoveAttribute("bgcolor");
                    }
                    else if (element.Name == "td")
                    {
                        element.RemoveAttribute("width");
                        element.RemoveAttribute("valign");
                    }
                    else if (element.Name == "ul")
                    {
                        element.RemoveAttribute("type");
                    }
                    element = element.RemoveAttribute("class");
                    element = element.RemoveAttribute("style");
                }
            }

            for (int i = 0; i < nodeToCombineAsText.Count; ++i)
            {
                var node = nodeToCombineAsText[i];
                var text = node.InnerText();
                node.ClearNodes();
                node.AddTextNode(text);
            }

            var strBuilder = new StringBuilder(htmlString.Length);
            using (var textWriter = new StringWriter(strBuilder))
            {
                doc.Render(textWriter);
            }
            htmlString = strBuilder.ToString();
        }

    }
}
