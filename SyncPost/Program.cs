using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MetaWeblogSharp;

namespace SyncPost
{
    class Program
    {
        static void Main(string[] args)
        {
            var password = getPassword();

            if (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Password can not be empty!");
                return;
            }


            string title;
            string body;

            getLastestBlog(out title, out body);

            postLastestBlog(password, title, body);
        }

        private static void getLastestBlog(out string title, out string body)
        {
            title = "";
            string date = "";

            string fromBlog = System.Configuration.ConfigurationManager.AppSettings["FromBlog"];
            string fromBlogName = System.Configuration.ConfigurationManager.AppSettings["FromBlogName"];
            string postDir = System.Configuration.ConfigurationManager.AppSettings["PostDir"];
            DirectoryInfo di = new DirectoryInfo(postDir);
            FileInfo latestInfo = di.GetFiles("*.markdown").OrderByDescending(fi => fi.Name).First();

            using (StreamReader sr = new StreamReader(latestInfo.FullName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line.StartsWith("title: "))
                    {
                        title = line.Substring(8, line.Length - 9);
                    }
                    else if (line.StartsWith("date: "))
                    {
                        date = line.Substring(6, 10);
                        break;
                    }
                }
            }

            string address = string.Format("{0}blog/{1}/{2}/", fromBlog, date.Replace('-', '/'),
                Path.GetFileNameWithoutExtension(latestInfo.Name).Substring(11));
            body =
                string.Format(
                    @"<p>博客搬到了<a href=""{2}"">{3}</a>，请各位看官挪步。最新的一篇是：<a href=""{0}"">{1}</a>。</p>",
                    address, title, fromBlog, fromBlogName);
            Console.WriteLine("Original Link: {0}", address);
        }

        private static void postLastestBlog(string password, string title, string body)
        {
            string username = System.Configuration.ConfigurationManager.AppSettings["UserName"];
            string blogMetweblogUrl = System.Configuration.ConfigurationManager.AppSettings["ToBlog"];
            var blogcon = new BlogConnectionInfo(
                string.Empty,
                blogMetweblogUrl,
                string.Empty,
                username,
                password);

            var client = new Client(blogcon);
            client.NewPost(title, body, new List<string>(), true, null);
            Console.WriteLine("Done!");
        }

        private static string getPassword()
        {
            string password = System.Configuration.ConfigurationManager.AppSettings["Password"];
            if (!string.IsNullOrEmpty(password))
            {
                return password;
            }

            Console.Write("Password: ");
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            } while (key.Key != ConsoleKey.Enter);
            return password;
        }
    }
}
