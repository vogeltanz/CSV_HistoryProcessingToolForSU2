using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSVProcessingForSU2
{
    public partial class FormMain : Form
    {
        List<string> directoryPathsSelected;

        public FormMain()
        {
            InitializeComponent();

            directoryPathsSelected = new List<string>();
        }

        private void buttonCSVOpen_Click(object sender, EventArgs e)
        {
            textBoxOut.Text = String.Empty;
            folderBrowserDialogMain.ShowDialog();
            if (directoryPathsSelected.Contains(folderBrowserDialogMain.SelectedPath) == false)
            {
                directoryPathsSelected.Add(folderBrowserDialogMain.SelectedPath);
            }
        }

        private void buttonPathsSheetNames_Click(object sender, EventArgs e)
        {
            string message = String.Empty;

            foreach (string path in this.directoryPathsSelected)
            {
                message += path + Environment.NewLine + Environment.NewLine;
            }

            MessageBox.Show(message);
        }

        private void buttonPathsClear_Click(object sender, EventArgs e)
        {
            this.directoryPathsSelected.Clear();
        }

        private void buttonOutputXLSXFilePath_Click(object sender, EventArgs e)
        {
            textBoxOut.Text = String.Empty;
            saveFileDialogMain.ShowDialog();
        }


        private void buttonExecute_Click(object sender, EventArgs e)
        {
            textBoxOut.Text = "Processing...";


            if (CheckPaths(this.directoryPathsSelected) == false)
            {
                this.buttonCSVOpen_Click(sender, e);
                return;
            }



            List<List<string>> directoryPathsList = new List<List<string>>();

            foreach (string path in this.directoryPathsSelected)
            {
                directoryPathsList.Add(FileCSV.SearchFileAndCreateAllInOneCSV(path, checkBoxcheckBoxSearchSubdirectories.Checked, textBoxFileNameHistory.Text));
            }



            if (checkBoxCreateComparisonXLSX.Checked == true)
            {
                try
                {
                    List<string> directoryPaths = ConvertListListStringToListString(directoryPathsList);

                    FileXLSX.SaveXLSX(this.saveFileDialogMain.FileName, directoryPaths, textBoxFileNameHistory.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }



            textBoxOut.Text = "Successfully done!";
            this.directoryPathsSelected.Clear();
        }

        private static List<string> ConvertListListStringToListString(List<List<string>> listListString)
        {
            List<string> listString = new List<string>();

            foreach (List<string> listS in listListString)
            {
                foreach (string str in listS)
                {
                    listString.Add(str);
                }
            }

            return listString;
        }

        private static bool CheckPaths(List<string> paths)
        {
            for (int i = 0; i < paths.Count; ++i)
            {
                if (Directory.Exists(paths[i]) == false)
                {
                    paths.RemoveAt(i);
                }
            }

            if (paths.Count > 0)
                return true;
            else
                return false;

        }
    }
}
