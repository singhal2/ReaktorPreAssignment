using ReaktorPreAssignment.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ReaktorPreAssignment.Service
{
    public class FileService
    {
        public void ReadFile()
        {
            string path = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), @"Resources\status.txt");
            string[] lines = System.IO.File.ReadAllLines(path);

            Packages packages = new Packages() { List = new List<PackageItem>() };
            PackageItem package = new PackageItem();

            for (int i = 0; i < lines.Length; i++)
            {
                //Create a temp package object

                var line = lines[i];
                if (!string.IsNullOrEmpty(line))
                {
                    string[] words = line.Split(new char[] { ':' }, 2);

                    //Handle description
                    switch (words[0].Trim().ToLower())
                    {
                        case "package":
                            package.Name = words[1].Trim();
                            break;
                        case "description":
                            package.Decription = words[1].Trim();

                            var nextLine = lines[i + 1];
                            while (i < lines.Length - 1 && !string.IsNullOrEmpty(nextLine) && nextLine[0] == ' ')
                            {
                                package.Decription += Environment.NewLine + nextLine.Trim();
                                i++;
                                nextLine = lines[i];
                            }
                            break;
                    }

                    if (!string.IsNullOrEmpty(package.Decription) && i < lines.Length - 1)
                    {
                        i++;
                        line = lines[i];
                    }
                }

                if (string.IsNullOrEmpty(line))
                {
                    packages.List.Add(package);
                    package = new PackageItem();
                    continue;
                }






            }
        }


    }
}