using Sudoku.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Sudoku.CommandsForGame
{
    public static class Grid
    {
        public static void CreateGrid(int SizeOfGrid, ObservableCollection<ObservableCollection<Cell>> Elements)
        {
            for (int i = 0; i < SizeOfGrid; i++)
            {
                Elements.Add(new ObservableCollection<Cell>());
                for (int j = 0; j < SizeOfGrid; j++)
                {
                    Elements[i].Add(new Cell(i, j, null, null));
                }
            }
        }

        public static void PopulateGrid(List<string> numbers, ObservableCollection<ObservableCollection<Cell>> Elements, int SizeOfGrid, string Path)
        {
            int index = 0, index2 = 0;
            foreach (ObservableCollection<Cell> subList in Elements)
            {
                for (int j = 0; j < SizeOfGrid; j++)
                {
                    subList[j] = new Cell(index, j, numbers[index2], Path);
                    index2++;
                }
                index++;
            }
        }


        //read grid from file, if the name is a version of sudoku(Ex: Sudoku4x4_V1), then will enter into the first condition
        //else if the file is saved by user, then will skip the first line where is the name of the sudoku version
        public static void ReadFromFile(List<string> numbers, string Path)
        {
            int index = 0;
            using (StreamReader file = new StreamReader(new FileStream(Path, FileMode.Open)))
            {
                if (Path.Substring(0, 6) == "Sudoku")
                {
                    while (!file.EndOfStream)
                    {
                        string line = file.ReadLine();
                        if (line != null)
                            numbers.Add(line);
                        else
                            numbers.Add("0");
                    }
                    file.Dispose();
                }

                else
                {
                    while (!file.EndOfStream)
                    {
                        string line = file.ReadLine();
                        if (index > 0)
                        {
                            if (line != null)
                                numbers.Add(line);
                            else
                                numbers.Add("0");
                        }
                        index++;
                    }
                    file.Dispose();
                }
            }
        }

        //put on the first line the name of the sudoku version (Ex: in the file userName.txt the first line will be Sudoku9x9_V3.txt)
        private static string ReadFromUserFileFirstLine(string Path)
        {
            string firstLine = "";
            using (StreamReader file = new StreamReader(new FileStream(Path, FileMode.Open)))
            {
                firstLine = file.ReadLine().ToString();
                file.Dispose();
            }

            return firstLine;
        }

        public static Random Random = new Random();
        public static string GetRandomFile(int size)
        {
            List<string> files = new List<string>();

            if (size == 4)
            {
                files.Add("Sudoku4x4_V1.txt");
                files.Add("Sudoku4x4_V2.txt");
                files.Add("Sudoku4x4_V3.txt");

                int getRandomNumber = Random.Next(0, files.Count);
                return files[getRandomNumber];
            }

            else if (size == 6)
            {
                files.Add("Sudoku6x6_V1.txt");
                files.Add("Sudoku6x6_V2.txt");

                int getRandomNumber = Random.Next(0, files.Count);
                return files[getRandomNumber];
            }

            else if (size == 9)
            {
                files.Add("Sudoku9x9_V1.txt");
                files.Add("Sudoku9x9_V2.txt");
                files.Add("Sudoku9x9_V3.txt");

                int getRandomNumber = Random.Next(0, files.Count);
                return files[getRandomNumber];
            }

            else if (size == 16)
                files.Add("Sudoku16x16.txt");

            return files[0];
        }

        public static string GetRandomFileForStandard()
        {
            List<string> files = new List<string>
            {
                "Sudoku9x9_V1.txt",
                "Sudoku9x9_V2.txt",
                "Sudoku9x9_V3.txt"
            };

            int getRandomNumber = Random.Next(0, files.Count);
            return files[getRandomNumber];
        }

        //if the file name is UserName.txt it will get the path with ReadFromUserFileFirstLine()
        public static string GetTheResolvedFile(ObservableCollection<ObservableCollection<Cell>> Elements)
        {
            string path = null;
            foreach (ObservableCollection<Cell> subList in Elements)
            {
                foreach (Cell cell in subList)
                    path = cell.GamePath;
            }

            if (path.Substring(0, 6) != "Sudoku")
                path = ReadFromUserFileFirstLine(path);

            string nameOfFile = path.Substring(0, path.Length - 4);
            string nameOfResolvedFile = nameOfFile + "_Resolve.txt";

            return nameOfResolvedFile;
        }


        //verify if all the grid checks is equals with the numbers from resolved file
        public static bool FinishGame(List<string> numbers, ObservableCollection<ObservableCollection<Cell>> Elements, int SizeOfGrid)
        {
            int index2 = 0;
            bool done = true;
            foreach (ObservableCollection<Cell> subList in Elements)
            {
                for (int j = 0; j < SizeOfGrid; j++)
                {
                    if (!subList[j].Number.Equals(numbers[index2]))
                    {
                        MessageBox.Show("You lose. Let`s try again.", "Sorry :(");
                        done = false;
                        break;
                    }
                    index2++;
                }
            }
            if (done)
                MessageBox.Show("You`re a winner. Congratulations", "Good news :)");

            return done;
        }

        public static void SaveGameInfile(ObservableCollection<ObservableCollection<Cell>> Elements, string Path)
        {
            using (StreamWriter file = File.AppendText(Path))
            {
                string SudokuVersion = "";
                foreach (ObservableCollection<Cell> subList in Elements)
                {
                    foreach (Cell cell in subList)
                    {
                        SudokuVersion = cell.GamePath;
                        break;
                    }
                }

                //write in the file the name of the sudoku version, to get the resolve file at the end
                file.WriteLine(SudokuVersion);

                foreach (ObservableCollection<Cell> subList in Elements)
                {
                    foreach (Cell cell in subList)
                    {
                        file.WriteLine(cell.Number);
                    }
                }
                MessageBox.Show(Path + " was created", "Info");
                file.Dispose();
            }
        }
    }
}
