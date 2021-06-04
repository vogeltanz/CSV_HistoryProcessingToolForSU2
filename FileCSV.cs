using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CSVProcessingForSU2
{
    public class FileCSV
    {
        const string FileNameBeginning = "history";
        const string SearchPattern = "*.csv";

        const string separatorIn = ",";
        public const string SeparatorOut = ";";

        const string decimalPointIn = ".";
        public const string DecimalPointOut = ",";

        const string endOfTheLine = "\r\n";

        const string CSVExtension = ".csv";
        const string AoALabel = "AngleOfAttack";
        const string AoAMarkInFile = "AoA";
        //const string SALabel = "SideAngle";
        //const string SAMarkInFile = "SA";


        public static List<string> SearchFileAndCreateAllInOneCSV(string pathDirectory, bool searchRecursive, string CSV_AllInOneFileName)
        {
            SearchOption searchOption = SearchOption.TopDirectoryOnly;
            if (searchRecursive == true)
            {
                searchOption = SearchOption.AllDirectories;
            }

            return FileCSV.SearchFileAndCreateAllInOneCSV(pathDirectory, FileCSV.SearchPattern, searchOption, CSV_AllInOneFileName);
        }


        private static List<string> SearchFileAndCreateAllInOneCSV(string pathDirectory, string searchPattern, SearchOption searchOption, string CSV_AllInOneFileName)
        {
            IEnumerable<string> files;
            List<string> dictionaryPaths = new List<string>();

            //The EnumerateFiles (.NET 4.0 and later) and GetFiles methods differ as follows:
            //When you use EnumerateFiles, you can start enumerating the collection of names before the whole collection is returned;
            //when you use GetFiles, you must wait for the whole array of names to be returned before you can access the array.
            //Therefore, when you are working with many files and directories, EnumerateFiles can be more efficient.
            try
            {
                files = Directory.EnumerateFiles(pathDirectory, searchPattern, searchOption);
            }
            catch (Exception ex)
            {
                FileCSV.WriteExceptionToDebug(ex);
                return dictionaryPaths;
            }



            //delete all History_All.csv files from directories. Otherwise the lines will be added to existed files.
            try
            {
                IEnumerable<string> filesHistoryAll;
                filesHistoryAll = Directory.EnumerateFiles(pathDirectory, CSV_AllInOneFileName, searchOption);
                foreach (string currentFile in filesHistoryAll)
                {
                    File.Delete(currentFile);
                }
            }
            catch (Exception ex)
            {
                FileCSV.WriteExceptionToDebug(ex);
                return dictionaryPaths;
            }



            foreach (string currentFile in files)
            {
                FileInfo infoFile = new FileInfo(currentFile);
                if (infoFile.Name.Substring(0, 7) == FileCSV.FileNameBeginning)
                {
                    try
                    {
                        string lastLine = FileCSV.OpenCSVAndGetLastLine(currentFile);
                        lastLine = FileCSV.ChangeSeparators(lastLine);
                        string directoryName = FileCSV.GetDirectory(currentFile);
                        FileCSV.AddToDirectoryList(ref dictionaryPaths, FileCSV.GetDirectoryPath(currentFile));
                        string historyAllInOnePath = directoryName + "\\" + CSV_AllInOneFileName;
                        string AoANumber = FileCSV.ChangeCurrentFileNameToNumberAoA(infoFile.Name);
                        string lastLineWhole = AoANumber + FileCSV.SeparatorOut + lastLine;
                        if (File.Exists(historyAllInOnePath) == false)
                        {
                            string header = FileCSV.OpenCSVAndGetFirstLine(currentFile);
                            header = "\"" + AoALabel + "\"" + FileCSV.SeparatorOut + FileCSV.ChangeSeparators(header);
                            FileCSV.WriteLineToCSVWithHeaderFirst(historyAllInOnePath, lastLineWhole, header);
                        }
                        else
                        {
                            FileCSV.WriteLineToCSV(historyAllInOnePath, lastLineWhole);
                        }
                    }
                    catch (Exception ex)
                    {
                        FileCSV.WriteExceptionToDebug(ex);
                        continue;
                    }
                }
            }

            return dictionaryPaths;

        }

        private static string ChangeCurrentFileNameToNumberAoA(string fileNameHistoryWithAoA)
        {
            string AoANumber = fileNameHistoryWithAoA;

            if (AoANumber.Contains(FileCSV.AoAMarkInFile) == true)
            {
                int AoAIndex = AoANumber.IndexOf(FileCSV.AoAMarkInFile);
                int extensionIndex = AoANumber.IndexOf(FileCSV.CSVExtension);
                AoANumber = AoANumber.Substring(AoAIndex + FileCSV.AoAMarkInFile.Count(), extensionIndex - AoAIndex - FileCSV.AoAMarkInFile.Count());
            }

            AoANumber = FileCSV.ChangeSeparators(AoANumber);
            return AoANumber;
        }


        private static string OpenCSVAndGetLastLine(string fileName)
        {
            string[] allLines = File.ReadAllLines(fileName);
            string lastLine = allLines[allLines.Length - 1];

            return lastLine;
        }

        private static string OpenCSVAndGetFirstLine(string fileName)
        {
            string[] allLines = File.ReadAllLines(fileName);
            string firstLine = allLines[0];

            return firstLine;
        }

        private static string ChangeSeparators(string line)
        {
            line = line.Replace(FileCSV.separatorIn, FileCSV.SeparatorOut);
            line = line.Replace(FileCSV.decimalPointIn, FileCSV.DecimalPointOut);
            return line;
        }

        private static string GetDirectory(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            return info.DirectoryName;
        }

        private static string GetDirectoryPath(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            return info.Directory.FullName;
        }

        private static void AddToDirectoryList(ref List<string> directoryPaths, string directoryPath)
        {
            if (directoryPaths.Contains(directoryPath) == false)
            {
                directoryPaths.Add(directoryPath);
            }
        }


        private static void WriteLineToCSV(string fileNameWithDirectory, string line)
        {
            using (StreamWriter streamWriter = new StreamWriter(fileNameWithDirectory, true))
            {
                streamWriter.WriteLine(line);
                streamWriter.Close();
            }
        }

        private static void WriteLineToCSVWithHeaderFirst(string fileNameWithDirectory, string line, string header)
        {
            if (File.Exists(fileNameWithDirectory) == false)
            {
                using (StreamWriter streamWriter = new StreamWriter(fileNameWithDirectory, true))
                {
                    streamWriter.WriteLine(header);
                    streamWriter.WriteLine(line);
                    streamWriter.Close();
                }
            }
        }



        private static void WriteExceptionToDebug(Exception ex)
        {
            Debug.WriteLine(String.Format("Message: {0}\nSource: {1}\nInnerException: {2}\nStackTrace:{3}\n\n",
                                                            ex.Message,
                                                            ex.Source,
                                                            ex.InnerException,
                                                            ex.StackTrace));
        }
    }
}
