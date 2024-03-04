using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace GenerateReleaseNotes
{
    internal class Program
    {

        static string newImagePath = @"![new](../images/new.png)";
        static string fixedImagePath = @"![fixed](../images/fixed.png)";
        static string changedImagePath = @"![changed](../images/changed.png)";
        static string headerContent;
        static StringBuilder fileContent = new StringBuilder();

        #region RequiredToChange
        static string releaseNotesVersion = @"2024 Q1 (version 2024.1.124)";
        static string inputFile = @"..\..\ASP.NET Core, Blazor, Xamarin, WinUI.xml";
        #endregion
        static void Main(string[] args)
        {
            string tags = string.Join(",", releaseNotesVersion.Replace("(", string.Empty).Replace(")", string.Empty).Replace("version", string.Empty).Split(' '));
            headerContent = @"---" + Environment.NewLine +
            "title: " + releaseNotesVersion + Environment.NewLine +
            "page_title: What is new in " + releaseNotesVersion + " for the Document Processing Libraries" + Environment.NewLine +
            "description: " + releaseNotesVersion + Environment.NewLine + //2024 Q1 (version 2024.1.124)" +
            "slug: release-notes-2024-q1" + Environment.NewLine +
            "tags: " + tags + Environment.NewLine + //q1, 2024, release, notes"+
            "published: True" + Environment.NewLine +
            "position: 0" + Environment.NewLine +
            "---" + Environment.NewLine;
            fileContent.AppendLine(headerContent);
            fileContent.AppendLine(Environment.NewLine + "# " + releaseNotesVersion + Environment.NewLine);
            XmlDocument dom = new XmlDocument();
            dom.Load(inputFile);
            AddNode(dom.DocumentElement);

            string path = @"..\..\release-notes-" + releaseNotesVersion + ".md";
            File.Delete(path);
            File.WriteAllText(path, fileContent.ToString());
            Process.Start(path);
        }

        private static void AddNode(XmlNode inXmlNode)
        {
            XmlNode xNode;
            XmlNodeList nodeList;
            int i;

            // Loop through the XML nodes until the leaf is reached.
            // Add the nodes to the TreeView during the looping process.
            if (inXmlNode.HasChildNodes)
            {
                nodeList = inXmlNode.ChildNodes;
                for (i = 0; i <= nodeList.Count - 1; i++)
                {
                    xNode = inXmlNode.ChildNodes[i];
                    switch (xNode.Name)
                    {
                        case "releaseNoteItem":
                            fileContent.AppendLine(Environment.NewLine + "## " + xNode.Attributes["Title"].Value + Environment.NewLine);
                            break;
                        case "newItems":
                            fileContent.AppendLine(Environment.NewLine + newImagePath + Environment.NewLine);
                            break;
                        case "fixedItems":
                            fileContent.AppendLine(Environment.NewLine + fixedImagePath + Environment.NewLine);
                            break;
                        case "changedItems":
                            fileContent.AppendLine(Environment.NewLine + changedImagePath + Environment.NewLine);
                            break;
                        default:
                            break;
                    }
                    AddNode(xNode);
                }
            }
            else
            {
                string content = (inXmlNode.OuterXml).Trim();
                fileContent.AppendLine("* " + content);
            }
        }
    }
}
