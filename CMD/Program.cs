using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamespaceCMD
{
    enum Colors { Blue = 1, Green = 2, Red = 3 };
    public class CMD
    {
        public static string CurrentPath { get; set; } = @"D:\";
        public static string MoveBack { get; set; } = "cd..";
        public static string Clear { get; set; } = "cls";
        public static string Dir { get; set; } = "dir";
        public static string Cd { get; set; } = "cd ";
        public static string Mkdir { get; set; } = "mkdir ";
        public static string Type { get; set; } = "type nul > ";
        public static string Copy { get; set; } = "copy ";
        public static string Tree { get; set; } = "tree";
        public static string Move { get; set; } = "move ";
        public static string Delete { get; set; } = "del ";
        public static string Xcopy { get; set; } = "xcopy ";
        public static string Xmove { get; set; } = "xmove ";

        public static void ChangeToOldPath()
        {
            var directory = new DirectoryInfo(CurrentPath);
            if (directory.Parent == null)
            {
                CurrentPath = @"D:\";
            }
            else
            {
                CurrentPath = directory.Parent.FullName;
            }
        }
        public static void Color(string text)
        {
            var data = text.Split();
            if (data.Length == 2)
            {
                int no = int.Parse(data[1]);
                switch ((Colors)no)
                {
                    case Colors.Blue:

                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case Colors.Green:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case Colors.Red:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                }
            }
        }
        public static void ClearConsole()
        {
            Console.Clear();
        }
        public static void Show()
        {
            var dirInfo = new DirectoryInfo(CurrentPath);
            var dirs = dirInfo.GetFileSystemInfos();
            if (dirs != null)
            {
                foreach (var dir in dirs)
                {
                    Console.Write(dir.CreationTime);
                    Console.Write(" => " + dir.Name);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("There is not file or directory");
            }
        }
        public static void ShowAll(string path)
        {
            var dirInfo = new DirectoryInfo(path);
            var dirs = dirInfo.GetDirectories();
            foreach (var dirr in dirs)
            {
                if (dirr.Name == "$RECYCLE.BIN")
                {
                    continue;
                }
                if (dirr.Name == "System Volume Information")
                {
                    continue;
                }
                var files = dirr.GetFiles();
                foreach (var file in files)
                {
                    Console.Write("-----");
                    Console.WriteLine(file);
                }
                ShowAll(dirr.FullName);
            }
        }
        public static string OpenFolder(string path, string data)
        {
            var dirinfo = new DirectoryInfo(path);
            var dirs = dirinfo.GetFileSystemInfos();
            var data2 = data.Split(' ');
            if (dirs != null)
            {
                foreach (var item in dirs)
                {
                    if (data2[1] == item.Name)
                    {
                        return item.FullName;
                    }
                }
            }
            else
            {
                Console.WriteLine("There is not file or directory");
            }
            return null;
        }
        public static void CreatFolder(string data)
        {
            var dirInfo = new DirectoryInfo(CurrentPath);
            var dirs = dirInfo.GetFileSystemInfos();
            bool check = false;
            foreach (var item in dirs)
            {
                if (item.Name == data)
                {
                    check = true;
                    break;
                }
            }
            if (check)
            {
                Console.WriteLine("Already there is a folder");
            }
            else
            {
                dirInfo.CreateSubdirectory(data);
            }
        }
        public static void CreatTxt(string data)
        {
            var dirInfo = new DirectoryInfo(CurrentPath);
            var dirs = dirInfo.GetFileSystemInfos();
            bool check = false;
            foreach (var item in dirs)
            {
                if (item.Name == data)
                {
                    check = true;
                    break;
                }
            }
            if (check)
            {
                Console.WriteLine("Already there is a file");
            }
            else
            {
                string fileName = CurrentPath + @"\" + data;
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine("");
                }
            }
        }
        public static void CopyFile(string data, string path)
        {
            var dirInfo = new DirectoryInfo(CurrentPath);
            var dirs = dirInfo.GetFileSystemInfos();
            foreach (var item in dirs)
            {
                if (data == item.FullName)
                {
                    File.Copy(data, path + @"\" + Path.GetFileName(data));
                }
            }
        }
        public static void MoveFile(string data, string path)
        {
            var dirInfo = new DirectoryInfo(CurrentPath);
            var dirs = dirInfo.GetFileSystemInfos();
            foreach (var item in dirs)
            {
                if (data == item.FullName)
                {
                    File.Move(data, path + @"\" + Path.GetFileName(data));
                }
            }
        }
        public static void CopyDirectory(string sourceDirectory, string targetDirectory)
        {
            var diSource = new DirectoryInfo(sourceDirectory);
            var diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }
        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetFileSystemInfos())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
        public static void MoveDirectory(string sourceDirectory, string targetDirectory)
        {
            var diSource = new DirectoryInfo(sourceDirectory);
            var diTarget = new DirectoryInfo(targetDirectory);
            MoveAll(diSource, diTarget);
        }
        public static void MoveAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.MoveTo(Path.Combine(target.FullName, fi.Name));
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetFileSystemInfos())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                MoveAll(diSourceSubDir, nextTargetSubDir);
            }
        }
        public static void DeleteFile(string path)
        {
            var dirInfo = new DirectoryInfo(CurrentPath);
            var dirs = dirInfo.GetFileSystemInfos();
            foreach (var item in dirs)
            {
                if (path == item.FullName)
                {
                    File.Delete(path);
                }
            }
        }

    }

    internal class Program
    {
        public static void Menu()
        {
            CMD.CurrentPath = @"C:\";
            while (true)
            {
                Console.Write(CMD.CurrentPath + '>');
                string command = Console.ReadLine();
                if (command.Trim() == CMD.MoveBack)
                {
                    CMD.ChangeToOldPath();
                }
                else if (command.ToLower().StartsWith("color"))
                {
                    CMD.Color(command);
                }
                else if (command.ToLower().StartsWith("cls"))
                {
                    CMD.ClearConsole();
                }
                else if (command.ToLower().StartsWith("dir"))
                {
                    CMD.Show();
                }
                else if (command.ToLower().StartsWith("cd "))
                {

                    var result = CMD.OpenFolder(CMD.CurrentPath, command);
                    if (result != null)
                    {
                        CMD.CurrentPath = result;

                    }
                }
                else if (command.ToLower().StartsWith("mkdir "))
                {
                    var result = command.Split(' ');
                    CMD.CreatFolder(result[1]);
                }
                else if (command.ToLower().StartsWith("type nul > "))
                {
                    var result = command.Split(' ');
                    CMD.CreatTxt(result[3]);
                }
                else if (command.ToLower().StartsWith("copy "))
                {
                    var result = command.Split(' ');
                    CMD.CopyFile(result[1], result[2]);
                }
                else if (command.ToLower().StartsWith("tree"))
                {
                    CMD.ShowAll(CMD.CurrentPath);
                }
                else if (command.ToLower().StartsWith("move "))
                {
                    var result = command.Split(' ');
                    CMD.MoveFile(result[1], result[2]);
                }
                else if (command.ToLower().StartsWith("del "))
                {
                    var result = command.Split(' ');
                    CMD.DeleteFile(result[1]);
                }
                else if (command.ToLower().StartsWith("xcopy"))
                {
                    var result = command.Split(' ');
                    CMD.CopyDirectory(result[1], result[2]);
                }
                else if (command.ToLower().StartsWith("xmove"))
                {
                    var result = command.Split(' ');
                    CMD.MoveDirectory(result[1], result[2]);
                }
            }
        }
        static void Main(string[] args)
        {
            try
            {
                Menu();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

