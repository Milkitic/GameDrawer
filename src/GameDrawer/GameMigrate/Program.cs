using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMigrate
{
    class Program
    {
        private static string CurrentPath => AppDomain.CurrentDomain.BaseDirectory;
        private static readonly StringBuilder StringBuilder = new StringBuilder();
        static void Main(string[] args)
        {
            string oldPath = null, newPath = null;
            if (args.Length > 0)
            {
                var path = args[0];
                if (Directory.Exists(path))
                    newPath = path;
                if (args.Length > 1)
                {
                    var path2 = args[1];
                    if (Directory.Exists(path2))
                        oldPath = path2;
                }
            }

            if (newPath == null && File.Exists(Path.Combine(CurrentPath, "GameDrawer.exe")))
                newPath = CurrentPath;

            if (oldPath == null)
                Console.WriteLine(@"请指定原Rom Manager目录：（粘贴并回车）");
            oldPath = GetPath(oldPath);

            if (newPath == null)
                Console.WriteLine(@"请指定新Game Drawer目录：（粘贴并回车）");
            newPath = GetPath(newPath);

            oldPath = AppendSymbol(oldPath);
            newPath = AppendSymbol(newPath);

            var oldDi = new DirectoryInfo(oldPath);
            var newDi = new DirectoryInfo(newPath);
            Console.WriteLine("Rom Manager目录：" + oldDi.FullName);
            Console.WriteLine("Game Drawer目录：" + newDi.FullName);
            Console.WriteLine("即将开始迁移，请确认信息。迁移信息将会保存至 migrate-MMddHHmmss.txt 中。确认请回车。");
            Console.ReadLine();
            StartMigrate(oldDi, newDi);
            Console.WriteLine("按任意键继续...");
            Console.ReadKey();
        }

        private static void StartMigrate(DirectoryInfo oldDi, DirectoryInfo newDi)
        {
            var oldDis = oldDi.EnumerateDirectories().Where(k => k.Name == "GAME" || k.Name == "ROMInfo").ToArray();
            if (oldDis.Length < 2)
            {
                WriteLine("原Rom Manager目录缺失相关子目录（GAME 和 ROMInfo），迁移失败。可能是选择了错误的目录。");
                return;
            }
            var newGameDi = newDi.EnumerateDirectories().FirstOrDefault(k => k.Name == "Games");
            if (newGameDi == null)
            {
                WriteLine("新Game Drawer目录缺失相关子目录（Games），迁移失败。可能是选择了错误的目录。");
                return;
            }

            int fileCount = 0, metaCount = 0;
            var oldGameDi = oldDis.First(k => k.Name == "GAME");
            var oldMetaDi = oldDis.First(k => k.Name == "ROMInfo");
            foreach (var dir in oldGameDi.EnumerateDirectories())
            {
                bool hasCreated = false;
                WriteLine($"找到目录：{dir}。");
                var romDi = dir.EnumerateDirectories().FirstOrDefault(k => k.Name == "ROM");
                if (romDi == null)
                {
                    WriteLine($"未找到目录：{dir}的ROM子目录，跳过。");
                    continue;
                }

                var machinePath = Path.Combine(newGameDi.FullName, dir.Name);
                var appPath = Path.Combine(machinePath, "Apps");

                var list = new List<string>();
                foreach (var rom in romDi.EnumerateFiles())
                {
                    if (!hasCreated)
                    {
                        if (!Directory.Exists(machinePath))
                        {
                            Console.WriteLine("初始化分类目录：" + machinePath);
                            Directory.CreateDirectory(machinePath);
                        }

                        if (!Directory.Exists(appPath))
                        {
                            Console.WriteLine("初始化App目录：" + appPath);
                            Directory.CreateDirectory(appPath);
                        }
                        hasCreated = true;
                    }
                    Write($"找到{dir}目录下的游戏：{rom}。\t");
                    list.Add(rom.FullName);

                    var targetFullName = Path.Combine(appPath, rom.Name);
                    var metaPath = targetFullName + ".meta";

                    if (!Directory.Exists(metaPath))
                    {
                        //Console.WriteLine("初始化meta目录：" + metaPath);
                        Directory.CreateDirectory(metaPath);
                    }

                    //copy
                    if (File.Exists(targetFullName))
                    {
                        Write($"\r\n已存在文件：{targetFullName}。是否覆盖？(Y/N) ");
                        bool condition = GetCliCondition();
                        if (condition)
                        {
                            File.Copy(rom.FullName, targetFullName, true);

                            fileCount++;
                            WriteLine($"已复制：{rom.Name}。");
                        }
                        else
                        {
                            WriteLine("用户已取消。");
                        }
                    }
                    else
                    {
                        File.Copy(rom.FullName, targetFullName);
                        fileCount++;
                        WriteLine($"已复制：{rom.Name}。");
                    }

                }

                var oldDirMetaDi = oldMetaDi.EnumerateDirectories().FirstOrDefault(k => k.Name == dir.Name);
                if (oldDirMetaDi == null)
                {
                    WriteLine($"找不到找到说明目录：{dir}，跳过说明迁移。");
                    continue;
                }
                WriteLine($"找到找到说明目录：{dir}");
                foreach (var metaFile in oldDirMetaDi.EnumerateFiles())
                {
                    Write($"找到目录{dir}下的说明：{metaFile}。\t");
                    var file = list.FirstOrDefault(k =>
                        Path.GetFileNameWithoutExtension(k) == Path.GetFileNameWithoutExtension(metaFile.FullName));
                    if (file != null)
                    {
                        var targetFullName = Path.Combine(appPath, Path.GetFileName(file));
                        var metaPath = targetFullName + ".meta";
                        var targetMeta = Path.Combine(metaPath, "description.txt");
                        //copy
                        if (File.Exists(targetMeta))
                        {
                            Write($"\r\n已存在说明{metaFile.Name}。是否覆盖？(Y/N) ");
                            bool condition = GetCliCondition();
                            if (condition)
                            {
                                CopyDescription(metaFile, targetMeta);
                                metaCount++;
                                WriteLine($"已复制说明：{metaFile.Name}。");
                            }
                            else
                            {
                                WriteLine("用户已取消。");
                            }
                        }
                        else
                        {
                            CopyDescription(metaFile, targetMeta);
                            metaCount++;
                            WriteLine($"已复制说明：{metaFile.Name}。");
                        }
                    }
                    else
                    {
                        WriteLine($"{metaFile.Name}为无效说明，跳过。");
                    }
                }
            }
            WriteLine($"迁移完成，共{fileCount}个文件，{metaCount}个说明文档。");
            File.WriteAllText(Path.Combine(CurrentPath, $"migrate-{DateTime.Now:MMddHHmmss}.txt"),
                StringBuilder.ToString());
        }

        private static void CopyDescription(FileInfo metaFile, string targetMeta)
        {
            //File.Copy(metaFile.FullName, targetMeta, true);
            var allText = File.ReadAllText(metaFile.FullName, Encoding.GetEncoding("gbk"));
            File.WriteAllText(targetMeta, allText, Encoding.UTF8);
        }

        private static bool GetCliCondition()
        {
            bool con;
            string condition = null;
            while (condition == null)
            {
                condition = Console.ReadLine();
                var str = condition?.ToLower().Trim();
                if (str != "y" && str != "n")
                    condition = null;
            }
            switch (condition)
            {
                case "y":
                    con = true;
                    break;
                default:
                case "n":
                    con = false;
                    break;
            }

            return con;
        }

        private static string GetPath(string path)
        {
            while (path == null || !Directory.Exists(path))
            {
                if (!string.IsNullOrEmpty(path))
                    Console.WriteLine("指定目录不存在。");
                path = Console.ReadLine()?.Trim();
            }

            return path;
        }

        private static string AppendSymbol(string path)
        {
            return !path.EndsWith("\\") ? path + "\\" : path;
        }

        private static void Write(string content)
        {
            StringBuilder.Append(content);
            Console.Write(content);
        }

        private static void WriteLine(string content)
        {
            StringBuilder.AppendLine(content);
            Console.WriteLine(content);
        }
    }
}
