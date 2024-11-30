using System.IO;

namespace DirectoryHW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string folder;
            string path = @"D:\";
            do
            {
                Console.Clear();
                ListOfFile(path);
                folder = Console.ReadLine();
                if (folder.Equals("../"))
                {
                    Console.Clear();
                    DirectoryInfo parent = Directory.GetParent(path);
                    if (parent != null)
                    {
                        path = parent.FullName;
                    }
                    else
                    {
                        Console.WriteLine("Bundan tashqarida katalog mavjud emas");
                        Console.WriteLine("Davom ettish uchun enterni bosing");
                        Console.ReadKey();
                    }
                }
                else if (Directory.Exists(Path.Combine(path, folder)))
                {
                    path = Path.Combine(path, folder);
                }
                else if (folder.Equals("stop", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Clear();
                    break;
                }
                else if (!Directory.Exists(Path.Combine(path, folder)))
                {
                    Console.Clear();
                    Console.WriteLine("Bunday katalog topilmadi");
                    Console.WriteLine("Davom ettish uchun enterni bosing");
                    Console.ReadKey();
                }
            }while (!folder.Equals("stop", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine("Program closed....");
            Console.ReadKey();
        }
        static void ListOfFile(string path)
        {
            int limit = 50;
            int count = 0;
            Console.WriteLine(path);
            Console.WriteLine($"{"File/Folder (Names)",-50}\t {"Size (Bytes)",-5}\n");
            var directoriesAndFolders = new List<string>();
            if (Directory.Exists(path)) 
            {
                directoriesAndFolders = Directory.EnumerateFileSystemEntries(path).Take(limit+1).ToList();
            }
            foreach (var items in directoriesAndFolders)
            {
                if (count > limit) break;
                if (Directory.Exists(items)) 
                {
                    DirectoryInfo folder = new DirectoryInfo(items);
                    string folderName = folder.Name.Length > limit ? folder.Name.Substring(0,limit) + "..." : folder.Name;
                    Console.WriteLine($"..{folderName,-50}");
                }
                if(File.Exists(items))
                {
                    FileInfo file = new FileInfo(items);
                    string fileName = file.Name.Length > limit ? file.Name.Substring(0,limit) + "..." : file.Name;
                    Console.WriteLine($"..{fileName,-50}\t {file.Length} bytes");
                }
                count++;
            }
            if(count > limit)
                Console.WriteLine("\n...");
            Console.WriteLine("\nEnter the folder name to continue or ../ to return...\t\tEnter \"stop\" to stop the program");
        }
    }
}
