using CodeGenerator.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeGenerator
{
    public partial class RadForm1 : Telerik.WinControls.UI.RadForm
    {
        StringBuilder regionLog;
        StringBuilder snippetLog;

        public RadForm1()
        {
            InitializeComponent();
            regionLog = new StringBuilder();
            snippetLog = new StringBuilder();

            radBrowseEditor1.DialogType = Telerik.WinControls.UI.BrowseEditorDialogType.FolderBrowseDialog;
            radBrowseEditor2.DialogType = Telerik.WinControls.UI.BrowseEditorDialogType.FolderBrowseDialog;
            radBrowseEditor3.DialogType = Telerik.WinControls.UI.BrowseEditorDialogType.FolderBrowseDialog;

            radBrowseEditor1.Value = Settings.Default.SamplesFolder;
            radBrowseEditor2.Value = Settings.Default.OutputFolder;
            radBrowseEditor3.Value = Settings.Default.RepoFolder;

            codeMainPathCS = Settings.Default.SamplesFolder + @"\SamplesCS\";
            codeMainPathVB = Settings.Default.SamplesFolder + @"\SamplesVB\";

            docsDirectory = Settings.Default.RepoFolder;
            outputDirectory = Settings.Default.OutputFolder;
        }

        string docsDirectory = "";
        string codeMainPathCS = "";
        string codeMainPathVB = "";
        string outputDirectory = "";
        
        const string SourceTag = "{{source=";
        const string EndRegion = "{{endregion}}";
        const string StartRegion = "region=";

        private void radButton1_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            DirSearch(docsDirectory);

            foreach (string file in allMDFIles)
            {
                ReplaceCode(file);
            }
            sw.Stop();

            Console.WriteLine("replaced " + count + " in " + sw.Elapsed);

            File.WriteAllText("regionLog.txt", regionLog.ToString());
            File.WriteAllText("snippedLog.txt", snippetLog.ToString());
        }

        int count = 0;

        public void ReplaceCode(string mdFilePath)
        {
            string text = File.ReadAllText(mdFilePath);
            if (text.IndexOf(SourceTag) < 0)
            {
                return;
            }
            count++;
            int indexCS = text.IndexOf(SourceTag);
            int indexVB = text.IndexOf(SourceTag, indexCS + SourceTag.Length);

            while (indexCS > -1 && indexVB > -1)
            {
                int endIndex = text.IndexOf("}}", indexCS);
                int regionIndex = text.IndexOf(StartRegion, indexCS);

                string CSPath = codeMainPathCS + text.Substring(indexCS + SourceTag.Length, regionIndex - (indexCS + StartRegion.Length + 2));
                string CSRegion = text.Substring(regionIndex + StartRegion.Length, endIndex - (regionIndex + StartRegion.Length));

                string CSsnippet = GetSnippet(CSPath, CSRegion, mdFilePath);

                endIndex = text.IndexOf("}}", indexVB);
                regionIndex = text.IndexOf(StartRegion, indexVB);

                string VBPath = codeMainPathVB + text.Substring(indexVB + SourceTag.Length, regionIndex - (indexVB + StartRegion.Length + 2));
                string VBRegion = text.Substring(regionIndex + StartRegion.Length, endIndex - (regionIndex + StartRegion.Length));

                string VBsnippet = GetSnippet(VBPath, VBRegion, mdFilePath);

                int endRegion = text.IndexOf(EndRegion, indexVB);

                if (endRegion < 0)
                {
                    regionLog.AppendLine(mdFilePath + " Wrong region configuration");
                }
                else
                {
                    StringBuilder result = new StringBuilder();
                    result.Append("````C#");
                    result.Append(CSsnippet);
                    result.Append("\n````\n");
                    result.Append("````VB.NET");
                    result.Append(VBsnippet);
                    result.Append("\n````");

                    int startTagEndIndex = endIndex + 2;
                    int length = endRegion - startTagEndIndex;
                    if (length < 0)
                    {
                        length = 0;
                    }
                    string oldString = text.Substring(startTagEndIndex, length).Trim();

                    if (oldString.Length == 0)
                    {
                        text = text.Insert(startTagEndIndex, "\n" + result.ToString());
                    }
                    else
                    {
                        text = text.Replace(oldString, result.ToString());
                    }
                }
                if (indexVB + SourceTag.Length > text.Length)
                {
                    regionLog.AppendLine("Check: " + mdFilePath);
                    break;                 
                }
                indexCS = text.IndexOf(SourceTag, indexVB + SourceTag.Length);
                if (indexCS > -1)
                {
                    indexVB = text.IndexOf(SourceTag, indexCS + SourceTag.Length);
                }
            }

            string newFile = mdFilePath.Substring(mdFilePath.LastIndexOf("C:\\") + 3);
            newFile = outputDirectory + newFile;
            if (!Directory.Exists(newFile))
            {
                DirectoryInfo di = Directory.CreateDirectory(Path.GetDirectoryName(newFile));
            }

            File.WriteAllText(newFile, text);
        }

        string GetSnippet(string path, string region, string mdFilePath)
        {
            try
            {
                string snippet = "";

                string fileContent = File.ReadAllText(path);
                int endIndex = -1;
                //#region - #endregion - without quotes
                int startIndex = fileContent.IndexOf("#region " + region, StringComparison.InvariantCultureIgnoreCase);
                if (startIndex >= 0)
                {
                    endIndex = fileContent.IndexOf("#endregion", startIndex, StringComparison.InvariantCultureIgnoreCase);
                    if (endIndex < 0)
                    {
                        endIndex = fileContent.IndexOf("#end region", startIndex, StringComparison.InvariantCultureIgnoreCase);
                    }
                }

                //#Region " "  - #End Region - with quotes
                if (startIndex < 0)
                {
                    startIndex = fileContent.IndexOf("#region \"" + region + "\"", StringComparison.InvariantCultureIgnoreCase);

                    if (startIndex >= 0)
                    {
                        endIndex = fileContent.IndexOf("#endregion", startIndex, StringComparison.InvariantCultureIgnoreCase);
                        if (endIndex < 0)
                        {
                            endIndex = fileContent.IndexOf("#end region", startIndex, StringComparison.InvariantCultureIgnoreCase);
                        }
                    }
                }

                if (startIndex < 0 || endIndex < 0)
                {
                    snippetLog.Append("Wrong region configuration." + " Path: " + path + " Region: " + region);
                    snippetLog.AppendLine("\n Article " +  mdFilePath);
                    snippetLog.AppendLine("---------");
                    return "";
                }

                snippet = fileContent.Substring(startIndex + region.Length + 9, endIndex - (startIndex + region.Length + 9));
                //in VB there can be regions with comments
                snippet = snippet.TrimEnd().TrimEnd(new char[] { '\'' });

                return AdjustTabs(snippet, region);
            }
            catch (Exception ex)
            {
                snippetLog.AppendLine("--------------");
                snippetLog.AppendLine(ex.Message + " Path: " + path + "\nRegion: " + region + "\n Article: " + mdFilePath);
                snippetLog.AppendLine("--------------");
            }
            return "";
        }

        int counter = 0;

        public string AdjustTabs(string snipet, string region)
        {
            snipet = snipet.TrimEnd().TrimStart(new char[]
            {
                '\n',
                '\r',
                '\"'
            });

            StringBuilder result = new StringBuilder();
            string[] rows = snipet.Split(new string[] { Environment.NewLine }, int.MaxValue, StringSplitOptions.RemoveEmptyEntries);
            int tabsToRemove = 0;
            //find first valid line of code
            int firstValidIndex = 0;
            for (int i = 0; i < rows.Length; i++)
            {
                if (rows[i].Trim().Length == 0)
                {
                    firstValidIndex++;
                }
                else
                {
                    break;
                }
            }
            //find how much tabs needs to be removed
            int temp = 0;
            if (rows.Length > 0)
            {
                for (int i = 0; i < rows[firstValidIndex].Length; i++)
                {
                    if (rows[firstValidIndex][i] == ' ')
                    {
                        temp++;
                        if (temp == 4)
                        {
                            temp = 0;
                            tabsToRemove++;
                        }
                    }
                    if (rows[firstValidIndex][i] != ' ')
                    {
                        break;
                    }
                }
            }
            else
            {
                snippetLog.AppendLine("Empty snipped");
            }

            result.AppendLine();

            int spacesToRemove = tabsToRemove * 4;
            if (region == "createContextMenu")
            {
            }
            //remove tabs in each line
            foreach (string row in rows)
            {
                if (row.Length > spacesToRemove)
                {
                    int noWhiteSpacecharIndex = row.TakeWhile(char.IsWhiteSpace).Count();

                    if (noWhiteSpacecharIndex > 0 && noWhiteSpacecharIndex < spacesToRemove)
                    {
                        result.AppendLine(row.Substring(noWhiteSpacecharIndex));
                    }
                    else
                    {
                        result.AppendLine(row.Substring(spacesToRemove));
                    }
                }
                else
                {
                    result.AppendLine(row);
                }
            }

            return result.ToString();
        }

        List<string> allMDFIles = new List<string>();

        void DirSearch(string sDir)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        if (f.EndsWith(".md"))
                        {
                            allMDFIles.Add(f);
                        }
                    }
                    DirSearch(d);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        private void radBrowseEditor1_ValueChanged(object sender, EventArgs e)
        {
            codeMainPathCS = radBrowseEditor1.Value + @"SamplesCS\";
            codeMainPathVB = radBrowseEditor1.Value + @"SamplesVB\";
            Settings.Default.SamplesFolder = radBrowseEditor1.Value;
        }

        private void radBrowseEditor2_ValueChanged(object sender, EventArgs e)
        {
            outputDirectory = radBrowseEditor2.Value;
            Settings.Default.OutputFolder = radBrowseEditor2.Value;
        }

        private void radBrowseEditor3_ValueChanged(object sender, EventArgs e)
        {
            docsDirectory = radBrowseEditor3.Value;
            Settings.Default.RepoFolder = radBrowseEditor3.Value;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Properties.Settings.Default.Save();
            base.OnClosing(e);
        }
    }
}