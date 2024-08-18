using System.Net;
using System;
using System.IO;
using System.Collections;
using System.Security.Cryptography;
namespace DuplicateDetectorV1
{
    class Program
    {
        static Dictionary<string, Item> _li = new Dictionary<string, Item>();
        static Dictionary<string, Item> _files = new Dictionary<string, Item>();
        static bool stop;
        static void Main(string[] args)
        {
            stop = true;
            DateTime now = DateTime.Now;
            Console.Write("Enter path:");
            string path = Console.ReadLine();
            Thread loading = new Thread(LoadScreen);
            loading.Start();
            Check(path);
            stop = false;
            Console.Clear();
            Console.WriteLine("Enter filename to save results (duplicates):");
            string resultFileName = Console.ReadLine() + ".txt";
            using (StreamWriter writer = File.CreateText(resultFileName))
            {
                writer.WriteLine($"[{now.Date.ToShortDateString()} {now.Hour}:{now.Minute}]");
                foreach (var i in _li)
                {
                    writer.WriteLine(i.Value.File);
                    foreach (var z in i.Value.Duplicates)
                    {
                        writer.WriteLine($"\t{z}");
                    }
                }
            }
            Console.CursorVisible = true;
            Console.WriteLine("Complete. Check the result file!");
        }

        static void LoadScreen()
        {
            Console.Clear();
            int end = Console.WindowWidth;
            string loadText = "....Searching....", temp = loadText;
            int counter = 0;
            while (stop)
            {
                Console.CursorVisible = false;
                Console.CursorTop = 1;
                Console.CursorLeft = counter + 1;
                counter++;
                if (counter > end - loadText.Length)
                    temp = loadText.Substring(0, loadText.Length - (counter - (end - loadText.Length)));
                Console.Write(temp);
                Console.CursorLeft = counter - 1;
                Console.Write(" ");
                if (counter + 1 == end)
                {
                    counter = 0;
                    temp = loadText;
                }

                Thread.Sleep(100);
            }
        }
        static void Check(string dir)
        {
            List<string> dirs = Directory.EnumerateDirectories(dir).ToList();
            List<string> files = Directory.EnumerateFiles(dir).ToList();
            string hash = "";
            foreach (var i in files)
            {
                hash = getHash(i);
                if (_files.ContainsKey(hash))
                {
                    if (_li.ContainsKey(hash))
                    {
                        _li[hash].Duplicates.Add(i);
                    }
                    else
                    {
                        _li.Add(hash, new Item(_files[hash].File, new List<string>() { i }));
                    }
                }
                else
                {
                    _files.Add(hash, new Item(i));
                }
            }
            foreach (var i in dirs)
            {
                Check(i);
            }
        }
        static string getHash(string path)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(path))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "");
                }
            }
        }
    }
    class Item
    {
        public string File { get; set; }
        public List<string> Duplicates { get; set; }
        public Item()
        {

        }
        public Item(string File, List<string> Duplicates = null)
        {
            this.File = File;
            this.Duplicates = Duplicates;
        }
    }
}