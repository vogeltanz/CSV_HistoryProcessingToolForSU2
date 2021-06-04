using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace CSVProcessingForSU2
{
    public class FileXLSX
    {
        const string Culture = "cs-CZ";


        public static void SaveXLSX(string PathSaveXLSX, List<string> InputCSVPaths, string CSV_AllInOneFileName = "History_All.csv")
        {
            FileInfo newFile = new FileInfo(PathSaveXLSX);
            if (newFile.Exists)
            {
                newFile.Delete();  // ensures we create a new workbook
                newFile = new FileInfo(PathSaveXLSX);
            }

            using (ExcelPackage package = new ExcelPackage())
            {
                List<ExcelRangeBase> ranges = new List<ExcelRangeBase>();

                foreach (string inputCSVPath in InputCSVPaths)
                {
                    ranges.Add(LoadCSV(package, inputCSVPath + "\\" + CSV_AllInOneFileName));
                }

                ComparisonSheet(package, ranges, "srovnani");

                package.SaveAs(newFile);

            }
        }


        private static ExcelRangeBase LoadCSV(ExcelPackage package, string path, string sheetName = "")
        {
            FileInfo fileInfo = new FileInfo(path);
            if (sheetName == "")
            {
                DirectoryInfo directoryInfo = fileInfo.Directory;
                sheetName = directoryInfo.Name;
            }


            //Create the Worksheet
            var sheet = package.Workbook.Worksheets.Add(sheetName);


            //Create the format object to describe the text file
            var format = new ExcelTextFormat();
            format.Culture = new CultureInfo(FileXLSX.Culture);
            format.SkipLinesBeginning = 0;
            format.TextQualifier = '"';
            format.Delimiter = FileCSV.SeparatorOut[0];
            format.Culture.NumberFormat.NumberDecimalSeparator = FileCSV.DecimalPointOut;

            //Now read the file into the sheet.
            var range = sheet.Cells["A1"].LoadFromText(fileInfo, format, TableStyles.None, true);


            SortByColumn(0, ref range, true);

            ChangeRowPositions(ref range);


            //sheet.View.ShowGridLines = false;
            //sheet.Calculate();
            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

            //delete table cause in no table style features etc. (there is some problem with ChangeRowPositions - first change is OK but next changes cause minor error)
            sheet.Tables.Delete(sheet.Tables.First());




            //Now add a XYScatterSmooth charts...

            int marginX = 20;
            int marginY = 20;
            int width = 600;
            int height = 400;
            int initialYPosition = 20 * (range.End.Row + 1);

            //Create one chart for each column...
            for (int col = 1; col < range.End.Column; col++)
            {
                var chart = sheet.Drawings.AddChart(sheet.Cells[Char.ConvertFromUtf32(65 + col) + "1"].Value.ToString(), eChartType.XYScatterSmooth);
                //chart.Name = "=" + Char.ConvertFromUtf32(65 + col) + "1";

                chart.SetPosition(initialYPosition + ((col - 1) / 3) * marginY + ((col - 1) / 3) * height, marginX + ((col - 1) % 3) * marginX + ((col - 1) % 3) * width);
                chart.SetSize(width, height);

                var ser = chart.Series.Add(range.Offset(1, col, range.End.Row - 2, 1), range.Offset(1, 0, range.End.Row - 2, 1));
                ser.HeaderAddress = range.Offset(0, col, 1, 1);

                chart.Legend.Remove();
                chart.Title.Text = range.Worksheet.Cells[1, col + 1].Value.ToString();
                chart.XAxis.Title.Text = "alpha [°]";
                chart.XAxis.Title.Font.Size = 11;
                chart.XAxis.Font.Size = 11;
                chart.YAxis.Font.Size = 11;
                chart.YAxis.CrossesAt = 0.5;	//x axis is crossed at 0.5 (however, it is 0 in excel. If not applicable it is crossed at -0.5 - WHY?)
                chart.XAxis.MinorTickMark = eAxisTickMark.Out;
                chart.YAxis.MinorTickMark = eAxisTickMark.Out;
            }




            string separatorVS = " vs ";


            int? CLiftColumnNumber = FindColumnNumber(sheet, "CLift");
            int? CDragColumnNumber = FindColumnNumber(sheet, "CDrag");

            var chart2 = sheet.Drawings.AddChart("CDrag_CLift", eChartType.XYScatterSmooth);

            chart2.SetPosition(initialYPosition + 0 * marginY + 0 * height, marginX + 3 * marginX + 3 * width);
            chart2.SetSize(width, height);

            var ser2 = chart2.Series.Add(range.Offset(1, CDragColumnNumber.Value, range.End.Row - 2, 1), range.Offset(1, CLiftColumnNumber.Value, range.End.Row - 2, 1));
            ser2.HeaderAddress = range.Offset(0, CDragColumnNumber.Value, 1, 1);

            chart2.Legend.Remove();
            chart2.Title.Text = range.Worksheet.Cells[1, CDragColumnNumber.Value + 1].Value.ToString() + separatorVS + range.Worksheet.Cells[1, CLiftColumnNumber.Value + 1].Value.ToString();
            chart2.XAxis.Title.Text = "CLift [-]";
            chart2.XAxis.Title.Font.Size = 10;
            chart2.YAxis.CrossesAt = 0.5;	//x axis is crossed at 0.5 (however, it is 0 in excel. If not applicable it is crossed at -0.5 - WHY?)
            chart2.XAxis.MinorTickMark = eAxisTickMark.Out;
            chart2.YAxis.MinorTickMark = eAxisTickMark.Out;




            int? CMzColumnNumber = FindColumnNumber(sheet, "CMz");

            var chart3 = sheet.Drawings.AddChart("CMz_CLift", eChartType.XYScatterSmooth);

            chart3.SetPosition(initialYPosition + 1 * marginY + 1 * height, marginX + 3 * marginX + 3 * width);
            chart3.SetSize(width, height);

            var ser3 = chart3.Series.Add(range.Offset(1, CMzColumnNumber.Value, range.End.Row - 2, 1), range.Offset(1, CLiftColumnNumber.Value, range.End.Row - 2, 1));
            ser3.HeaderAddress = range.Offset(0, CMzColumnNumber.Value, 1, 1);

            chart3.Legend.Remove();
            chart3.Title.Text = range.Worksheet.Cells[1, CMzColumnNumber.Value + 1].Value.ToString() + separatorVS + range.Worksheet.Cells[1, CLiftColumnNumber.Value + 1].Value.ToString();
            chart3.XAxis.Title.Text = "CLift [-]";
            chart3.XAxis.Title.Font.Size = 10;
            chart3.YAxis.CrossesAt = 0.5;	//x axis is crossed at 0.5 (however, it is 0 in excel. If not applicable it is crossed at -0.5 - WHY?)
            chart3.XAxis.MinorTickMark = eAxisTickMark.Out;
            chart3.YAxis.MinorTickMark = eAxisTickMark.Out;




            //chart.Style = eChartStyle.None;

            return range;
        }

        /// <summary>
        /// selection sort which sorts rows by specific column
        /// </summary>
        /// <param name="column"></param>
        /// <param name="range"></param>
        /// <param name="firstRowIsHeader"></param>
        private static void SortByColumn(int column, ref ExcelRangeBase range, bool firstRowIsHeader = true)
        {
            int firstRowCorrection = 0;
            if (firstRowIsHeader == true)
            {
                firstRowCorrection = 1;
            }
            for (int i = 1 + firstRowCorrection; i < range.End.Row - 1; ++i)
            {
                int indexMin = i;
                ExcelRange excelRange = range.Worksheet.Cells[i, 1];

                for (int j = range.End.Row - 1; j >= i + firstRowCorrection; --j)
                {
                    if (Convert.ToDouble(excelRange.Value) > Convert.ToDouble(range.Worksheet.Cells[j, 1].Value))
                    {
                        indexMin = j;
                        excelRange = range.Worksheet.Cells[j, 1];
                    }
                }

                if (indexMin != i)
                {
                    range.Worksheet.InsertRow(i, 1);
                    range.Worksheet.Cells[indexMin + 1, 1, indexMin + 1, range.End.Column].Copy(range.Worksheet.Cells[i, 1]);
                    range.Worksheet.DeleteRow(indexMin + 1);
                    //range = range.Worksheet.Cells[1, 1, range.End.Row, range.End.Column];
                }
            }
        }

        private static void ChangeRowPositions(ref ExcelRangeBase range)
        {
            //move "Iteration" before "Linear_Solver_Iterations"
            int? LinearSolverIterationsColumnNumber = FindColumnNumber(range.Worksheet, "Linear_Solver_Iterations");
            range.Worksheet.InsertColumn(LinearSolverIterationsColumnNumber.Value + 1, 1);
            int? IterationColumnNumber = FindColumnNumber(range.Worksheet, "Iteration");
            range.Worksheet.Cells[1, IterationColumnNumber.Value + 1, range.End.Row, IterationColumnNumber.Value + 1].Copy(range.Worksheet.Cells[1, LinearSolverIterationsColumnNumber.Value + 1]);
            range.Worksheet.DeleteColumn(IterationColumnNumber.Value + 1);

            //move "CL/CD" before "CSideForce"
            int? CSideForceColumnNumber = FindColumnNumber(range.Worksheet, "CSideForce");
            range.Worksheet.InsertColumn(CSideForceColumnNumber.Value + 1, 1);
            int? CL_CDColumnNumber = FindColumnNumber(range.Worksheet, "CL/CD");
            range.Worksheet.Cells[1, CL_CDColumnNumber.Value + 1, range.End.Row, CL_CDColumnNumber.Value + 1].Copy(range.Worksheet.Cells[1, CSideForceColumnNumber.Value + 1]);
            range.Worksheet.DeleteColumn(CL_CDColumnNumber.Value + 1);

            //move "Time" before "Iteration"
            IterationColumnNumber = FindColumnNumber(range.Worksheet, "Iteration");
            range.Worksheet.InsertColumn(IterationColumnNumber.Value + 1, 1);
            int? TimeColumnNumber = FindColumnNumber(range.Worksheet, "Time(min)");
            range.Worksheet.Cells[1, TimeColumnNumber.Value + 1, range.End.Row, TimeColumnNumber.Value + 1].Copy(range.Worksheet.Cells[1, IterationColumnNumber.Value + 1]);
            range.Worksheet.DeleteColumn(TimeColumnNumber.Value + 1);
        }

        private static int? FindColumnNumber(ExcelWorksheet sheet, string columnName)
        {

            int totalRows = sheet.Dimension.End.Row;
            int totalCols = sheet.Dimension.End.Column;
            var range = sheet.Cells[1, 1, 1, totalCols];
            for (int i = 1; i <= totalCols; i++)
            {
                if (range[1, i].Address != "" && range[1, i].Value != null && range[1, i].Value.ToString() == columnName)
                    return i - 1;

            }
            return null;
        }

        private static string FindColumnAddress(ExcelWorksheet sheet, string columnName)
        {

            int totalRows = sheet.Dimension.End.Row;
            int totalCols = sheet.Dimension.End.Column;
            var range = sheet.Cells[1, 1, 1, totalCols];
            for (int i = 1; i <= totalCols; i++)
            {
                if (range[1, i].Address != "" && range[1, i].Value != null && range[1, i].Value.ToString() == columnName)
                    return range[1, i].Address;

            }
            return null;
        }



        private static void ComparisonSheet(ExcelPackage package, List<ExcelRangeBase> ranges, string comparisonSheetName)
        {
            //Create the Worksheet
            var sheet = package.Workbook.Worksheets.Add(comparisonSheetName);
            package.Workbook.Worksheets.MoveToStart(package.Workbook.Worksheets.Count);



            //Now add a XYScatterSmooth charts...


            int marginX = 20;
            int marginY = 20;
            int width = 600;
            int height = 400;
            int initialYPosition = 20;

            //Create one chart for each column and sheet
            for (int col = 1; col < ranges[0].End.Column; col++)
            {
                var chart = sheet.Drawings.AddChart(ranges[0].Worksheet.Cells[Char.ConvertFromUtf32(65 + col) + "1"].Value.ToString(), eChartType.XYScatterSmooth);
                //chart.Name = "=" + Char.ConvertFromUtf32(65 + col) + "1";

                chart.SetPosition(initialYPosition + ((col - 1) / 3) * marginY + ((col - 1) / 3) * height, marginX + ((col - 1) % 3) * marginX + ((col - 1) % 3) * width);
                chart.SetSize(width, height);

                //add series of the column from each sheet
                foreach (ExcelRangeBase range in ranges)
                {
                    var ser = chart.Series.Add(range.Offset(1, col, range.End.Row - 2, 1), range.Offset(1, 0, range.End.Row - 2, 1));
                    ser.Header = range.Worksheet.Name;
                }

                chart.Title.Text = ranges[0].Worksheet.Cells[1, col + 1].Value.ToString();
                chart.XAxis.Title.Text = "alpha [°]";
                chart.XAxis.Title.Font.Size = 11;
                chart.XAxis.Font.Size = 11;
                chart.YAxis.Font.Size = 11;
                chart.YAxis.CrossesAt = 0.5;	//x axis is crossed at 0.5 (however, it is 0 in excel. If 0 is applied, it is crossed at -0.5 - WHY?)
                chart.XAxis.MinorTickMark = eAxisTickMark.Out;
                chart.YAxis.MinorTickMark = eAxisTickMark.Out;
            }




            string separatorVS = " vs ";


            int? CLiftColumnNumber = FindColumnNumber(ranges[0].Worksheet, "CLift");
            int? CDragColumnNumber = FindColumnNumber(ranges[0].Worksheet, "CDrag");

            var chart2 = sheet.Drawings.AddChart("CDrag_CLift", eChartType.XYScatterSmooth);

            chart2.SetPosition(initialYPosition + 0 * marginY + 0 * height, marginX + 3 * marginX + 3 * width);
            chart2.SetSize(width, height);

            //add series of the column from each sheet
            foreach (ExcelRangeBase range in ranges)
            {
                var ser2 = chart2.Series.Add(range.Offset(1, CDragColumnNumber.Value, range.End.Row - 2, 1), range.Offset(1, CLiftColumnNumber.Value, range.End.Row - 2, 1));
                ser2.Header = range.Worksheet.Name;
            }

            chart2.Title.Text = ranges[0].Worksheet.Cells[1, CDragColumnNumber.Value + 1].Value.ToString() + separatorVS + ranges[0].Worksheet.Cells[1, CLiftColumnNumber.Value + 1].Value.ToString();
            chart2.XAxis.Title.Text = "CLift [-]";
            chart2.XAxis.Title.Font.Size = 10;
            chart2.YAxis.CrossesAt = 0.5;	//x axis is crossed at 0.5 (however, it is 0 in excel. If not applicable it is crossed at -0.5 - WHY?)
            chart2.XAxis.MinorTickMark = eAxisTickMark.Out;
            chart2.YAxis.MinorTickMark = eAxisTickMark.Out;




            int? CMzColumnNumber = FindColumnNumber(ranges[0].Worksheet, "CMz");

            var chart3 = sheet.Drawings.AddChart("CMz_CLift", eChartType.XYScatterSmooth);

            chart3.SetPosition(initialYPosition + 1 * marginY + 1 * height, marginX + 3 * marginX + 3 * width);
            chart3.SetSize(width, height);

            //add series of the column from each sheet
            foreach (ExcelRangeBase range in ranges)
            {
                var ser3 = chart3.Series.Add(range.Offset(1, CMzColumnNumber.Value, range.End.Row - 2, 1), range.Offset(1, CLiftColumnNumber.Value, range.End.Row - 2, 1));
                ser3.Header = range.Worksheet.Name;
            }

            chart3.Title.Text = ranges[0].Worksheet.Cells[1, CMzColumnNumber.Value + 1].Value.ToString() + separatorVS + ranges[0].Worksheet.Cells[1, CLiftColumnNumber.Value + 1].Value.ToString();
            chart3.XAxis.Title.Text = "CLift [-]";
            chart3.XAxis.Title.Font.Size = 10;
            chart3.YAxis.CrossesAt = 0.5;	//x axis is crossed at 0.5 (however, it is 0 in excel. If not applicable it is crossed at -0.5 - WHY?)
            chart3.XAxis.MinorTickMark = eAxisTickMark.Out;
            chart3.YAxis.MinorTickMark = eAxisTickMark.Out;


        }


    }
}
