using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ivony.Html;
using Ivony.Html.ExpandedAPI;
using Ivony.Html.Parser;

namespace HTMLClean
{
    public class Cleaner
    {
        public static void CleanWordHTML(ref string htmlString)
        {
            var parser = new JumonyParser();
            var doc = parser.Parse(htmlString);
            var allNodes = doc.DescendantNodes().ToList();
            for (var i=0; i<allNodes.Count; ++i)
            {
                var node = allNodes[i];

                if(!(node is IHtmlElement)) continue;
                var element = node as IHtmlElement;
                if (element.Name == "meta" || element.Name == "style")
                {
                    element.Remove();
                }
                else if (element.Name == "p" || element.Name == "li" ||
                    element.Name == "h2" || element.Name == "h3" || element.Name == "h4" || element.Name == "h1")
                {
                    var text = element.InnerText();
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        if (element.Parent() != null && element.Parent().Name != "td")
                            element.AddElementAfterSelf("br");
                        element.Remove();
                    }
                    else
                    {
                        element.ClearNodes();
                        element.AddTextNode(text);
                    }
                }

                if(!element.IsAllocated()) continue;
                if (element.Name == "table")
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
                else if (element.Name == "body")
                {
                    element.RemoveAttribute("lang");
                    element.RemoveAttribute("link");
                    element.RemoveAttribute("vlink");
                }
                element.RemoveAttribute("class");
                element.RemoveAttribute("style");
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
