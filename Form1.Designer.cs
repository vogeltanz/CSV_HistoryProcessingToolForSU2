namespace CSVProcessingForSU2
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonCSVOpen = new System.Windows.Forms.Button();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.checkBoxcheckBoxSearchSubdirectories = new System.Windows.Forms.CheckBox();
            this.textBoxOut = new System.Windows.Forms.TextBox();
            this.folderBrowserDialogMain = new System.Windows.Forms.FolderBrowserDialog();
            this.textBoxFileNameHistory = new System.Windows.Forms.TextBox();
            this.labelOutputFileNameHistory = new System.Windows.Forms.Label();
            this.buttonPathsSheetNames = new System.Windows.Forms.Button();
            this.radioButtonNamesSheetAsDirectory = new System.Windows.Forms.RadioButton();
            this.radioButtonNamesCustomSheet = new System.Windows.Forms.RadioButton();
            this.groupBoxNamesOptions = new System.Windows.Forms.GroupBox();
            this.checkBoxCreateComparisonXLSX = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.saveFileDialogMain = new System.Windows.Forms.SaveFileDialog();
            this.buttonPathsClear = new System.Windows.Forms.Button();
            this.groupBoxNamesOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCSVOpen
            // 
            this.buttonCSVOpen.Location = new System.Drawing.Point(13, 13);
            this.buttonCSVOpen.Name = "buttonCSVOpen";
            this.buttonCSVOpen.Size = new System.Drawing.Size(75, 40);
            this.buttonCSVOpen.TabIndex = 0;
            this.buttonCSVOpen.Text = "CSV directory";
            this.buttonCSVOpen.UseVisualStyleBackColor = true;
            this.buttonCSVOpen.Click += new System.EventHandler(this.buttonCSVOpen_Click);
            // 
            // buttonExecute
            // 
            this.buttonExecute.Location = new System.Drawing.Point(13, 188);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(75, 40);
            this.buttonExecute.TabIndex = 1;
            this.buttonExecute.Text = "Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // checkBoxcheckBoxSearchSubdirectories
            // 
            this.checkBoxcheckBoxSearchSubdirectories.AutoSize = true;
            this.checkBoxcheckBoxSearchSubdirectories.Checked = true;
            this.checkBoxcheckBoxSearchSubdirectories.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxcheckBoxSearchSubdirectories.Location = new System.Drawing.Point(105, 13);
            this.checkBoxcheckBoxSearchSubdirectories.Name = "checkBoxcheckBoxSearchSubdirectories";
            this.checkBoxcheckBoxSearchSubdirectories.Size = new System.Drawing.Size(141, 17);
            this.checkBoxcheckBoxSearchSubdirectories.TabIndex = 3;
            this.checkBoxcheckBoxSearchSubdirectories.Text = "Search in Subdirectories";
            this.checkBoxcheckBoxSearchSubdirectories.UseVisualStyleBackColor = true;
            // 
            // textBoxOut
            // 
            this.textBoxOut.Location = new System.Drawing.Point(105, 199);
            this.textBoxOut.Name = "textBoxOut";
            this.textBoxOut.ReadOnly = true;
            this.textBoxOut.Size = new System.Drawing.Size(141, 20);
            this.textBoxOut.TabIndex = 4;
            // 
            // textBoxFileNameHistory
            // 
            this.textBoxFileNameHistory.Location = new System.Drawing.Point(274, 24);
            this.textBoxFileNameHistory.Name = "textBoxFileNameHistory";
            this.textBoxFileNameHistory.Size = new System.Drawing.Size(221, 20);
            this.textBoxFileNameHistory.TabIndex = 5;
            this.textBoxFileNameHistory.Text = "History_All.csv";
            // 
            // labelOutputFileNameHistory
            // 
            this.labelOutputFileNameHistory.AutoSize = true;
            this.labelOutputFileNameHistory.Location = new System.Drawing.Point(271, 8);
            this.labelOutputFileNameHistory.Name = "labelOutputFileNameHistory";
            this.labelOutputFileNameHistory.Size = new System.Drawing.Size(139, 13);
            this.labelOutputFileNameHistory.TabIndex = 6;
            this.labelOutputFileNameHistory.Text = "Output File Name for History";
            // 
            // buttonPathsSheetNames
            // 
            this.buttonPathsSheetNames.Location = new System.Drawing.Point(13, 87);
            this.buttonPathsSheetNames.Name = "buttonPathsSheetNames";
            this.buttonPathsSheetNames.Size = new System.Drawing.Size(75, 40);
            this.buttonPathsSheetNames.TabIndex = 7;
            this.buttonPathsSheetNames.Text = "Paths";
            this.buttonPathsSheetNames.UseVisualStyleBackColor = true;
            this.buttonPathsSheetNames.Click += new System.EventHandler(this.buttonPathsSheetNames_Click);
            // 
            // radioButtonNamesSheetAsDirectory
            // 
            this.radioButtonNamesSheetAsDirectory.AutoSize = true;
            this.radioButtonNamesSheetAsDirectory.Checked = true;
            this.radioButtonNamesSheetAsDirectory.Location = new System.Drawing.Point(6, 19);
            this.radioButtonNamesSheetAsDirectory.Name = "radioButtonNamesSheetAsDirectory";
            this.radioButtonNamesSheetAsDirectory.Size = new System.Drawing.Size(174, 17);
            this.radioButtonNamesSheetAsDirectory.TabIndex = 0;
            this.radioButtonNamesSheetAsDirectory.TabStop = true;
            this.radioButtonNamesSheetAsDirectory.Text = "Sheet Name as Directory Name";
            this.radioButtonNamesSheetAsDirectory.UseVisualStyleBackColor = true;
            // 
            // radioButtonNamesCustomSheet
            // 
            this.radioButtonNamesCustomSheet.AutoSize = true;
            this.radioButtonNamesCustomSheet.Enabled = false;
            this.radioButtonNamesCustomSheet.Location = new System.Drawing.Point(6, 42);
            this.radioButtonNamesCustomSheet.Name = "radioButtonNamesCustomSheet";
            this.radioButtonNamesCustomSheet.Size = new System.Drawing.Size(127, 17);
            this.radioButtonNamesCustomSheet.TabIndex = 1;
            this.radioButtonNamesCustomSheet.Text = "Custom Sheet Names";
            this.radioButtonNamesCustomSheet.UseVisualStyleBackColor = true;
            // 
            // groupBoxNamesOptions
            // 
            this.groupBoxNamesOptions.Controls.Add(this.radioButtonNamesSheetAsDirectory);
            this.groupBoxNamesOptions.Controls.Add(this.radioButtonNamesCustomSheet);
            this.groupBoxNamesOptions.Location = new System.Drawing.Point(105, 68);
            this.groupBoxNamesOptions.Name = "groupBoxNamesOptions";
            this.groupBoxNamesOptions.Size = new System.Drawing.Size(200, 71);
            this.groupBoxNamesOptions.TabIndex = 10;
            this.groupBoxNamesOptions.TabStop = false;
            this.groupBoxNamesOptions.Text = "Sheet Names Options";
            // 
            // checkBoxCreateComparisonXLSX
            // 
            this.checkBoxCreateComparisonXLSX.AutoSize = true;
            this.checkBoxCreateComparisonXLSX.Checked = true;
            this.checkBoxCreateComparisonXLSX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCreateComparisonXLSX.Location = new System.Drawing.Point(105, 36);
            this.checkBoxCreateComparisonXLSX.Name = "checkBoxCreateComparisonXLSX";
            this.checkBoxCreateComparisonXLSX.Size = new System.Drawing.Size(164, 17);
            this.checkBoxCreateComparisonXLSX.TabIndex = 11;
            this.checkBoxCreateComparisonXLSX.Text = "Create Comparison XLSX File";
            this.checkBoxCreateComparisonXLSX.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 145);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 32);
            this.button1.TabIndex = 15;
            this.button1.Text = "Output File Path for Comparison";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonOutputXLSXFilePath_Click);
            // 
            // saveFileDialogMain
            // 
            this.saveFileDialogMain.FileName = "Comparison.xlsx";
            this.saveFileDialogMain.Filter = "Excel files (*.xlsx)|*.xlsx";
            // 
            // buttonPathsClear
            // 
            this.buttonPathsClear.Location = new System.Drawing.Point(311, 96);
            this.buttonPathsClear.Name = "buttonPathsClear";
            this.buttonPathsClear.Size = new System.Drawing.Size(75, 23);
            this.buttonPathsClear.TabIndex = 16;
            this.buttonPathsClear.Text = "Clear Paths";
            this.buttonPathsClear.UseVisualStyleBackColor = true;
            this.buttonPathsClear.Click += new System.EventHandler(this.buttonPathsClear_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 236);
            this.Controls.Add(this.buttonPathsClear);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBoxCreateComparisonXLSX);
            this.Controls.Add(this.groupBoxNamesOptions);
            this.Controls.Add(this.buttonPathsSheetNames);
            this.Controls.Add(this.labelOutputFileNameHistory);
            this.Controls.Add(this.textBoxFileNameHistory);
            this.Controls.Add(this.textBoxOut);
            this.Controls.Add(this.checkBoxcheckBoxSearchSubdirectories);
            this.Controls.Add(this.buttonExecute);
            this.Controls.Add(this.buttonCSVOpen);
            this.Name = "FormMain";
            this.Text = "CSV_History Processing Tool For SU2 v0.2.1 (©Tomáš Vogeltanz)";
            this.groupBoxNamesOptions.ResumeLayout(false);
            this.groupBoxNamesOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCSVOpen;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.CheckBox checkBoxcheckBoxSearchSubdirectories;
        private System.Windows.Forms.TextBox textBoxOut;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogMain;
        private System.Windows.Forms.TextBox textBoxFileNameHistory;
        private System.Windows.Forms.Label labelOutputFileNameHistory;
        private System.Windows.Forms.Button buttonPathsSheetNames;
        private System.Windows.Forms.RadioButton radioButtonNamesCustomSheet;
        private System.Windows.Forms.RadioButton radioButtonNamesSheetAsDirectory;
        private System.Windows.Forms.GroupBox groupBoxNamesOptions;
        private System.Windows.Forms.CheckBox checkBoxCreateComparisonXLSX;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SaveFileDialog saveFileDialogMain;
        private System.Windows.Forms.Button buttonPathsClear;
    }
}

