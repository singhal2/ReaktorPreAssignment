using ReaktorPreAssignment.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ReaktorPreAssignment.Service
{
    public class Parser
    {
        public Dictionary<string, PackageItem> GetData()
        {
            //Get directory and read file
            try
            {
                string path = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), @"Resources\status.real");
                string[] lines = System.IO.File.ReadAllLines(path);
                //Parse data
                var modelledData = ParseData(lines);
                return modelledData;
            }
            catch (Exception)
            {
                return new Dictionary<string, PackageItem>();
            }
        }

        private Dictionary<string, PackageItem> ParseData(string[] lines)
        {
            Dictionary<string, PackageItem> packages = new Dictionary<string, PackageItem>();
            PackageItem package = new PackageItem() { DependsOn = new List<List<Dependency>>(), RequiredBy = new List<string>() };

            for (int currentLineInex = 0; currentLineInex < lines.Length; currentLineInex++)
            {
                //Get current line text
                var line = lines[currentLineInex];
                if (!string.IsNullOrEmpty(line)) //If text
                {
                    string[] words = line.Split(new char[] { ':' }, 2); //Split key and value

                    switch (words[0].Trim().ToLower()) //If key is of interest
                    {
                        case "package":
                            if (currentLineInex != 0) //For every new package name after the first, add old ackage to list
                            {
                                packages.Add(package.Name, package);
                                package = new PackageItem() { DependsOn = new List<List<Dependency>>(), RequiredBy = new List<string>() };
                            }
                            package.Name = words[1].Trim();
                            break;
                        case "description":
                            package.Decription = words[1].Trim();
                            //Loop for package description based on any of the following two conditions
                            //1. If the line is either indented by ' '
                            //2. Or if the line does not start with 'Homepage: ' AND 'Original-Maintainer: ' 
                            while (currentLineInex < lines.Length - 1 && !string.IsNullOrEmpty(lines[currentLineInex + 1]) &&
                                (lines[currentLineInex + 1][0] == ' ' || (!lines[currentLineInex + 1].StartsWith("Homepage: ") && !lines[currentLineInex + 1].StartsWith("Original-Maintainer: "))))
                            {
                                currentLineInex++;
                                var nextLine = lines[currentLineInex];
                                package.Decription += " <br/ > " + nextLine.Trim();
                            }
                            currentLineInex++;
                            break;
                        case ("depends"):
                            package.DependsOn = new List<List<Dependency>>();
                            //Get dependencies
                            string[] dependencyList = words[1].Split(new char[] { ',' });
                            //Clean dependencies (alternates and versions)
                            foreach (var dependency in dependencyList)
                            {
                                Dictionary<int, string> Dependencies = new Dictionary<int, string>();
                                if (dependency.Contains("|"))
                                {
                                    //Alternate dependencies
                                    string[] alternateDependencies = dependency.Split(new char[] { '|' }).Select(p => p.Trim()).Distinct().ToArray();
                                    //Alternate dependencies without version
                                    List<Dependency> alternatePackages = alternateDependencies.Select(x => new Dependency() { Name = x.Trim().Contains(' ') ? x.Split(new char[] { ' ' })[0] : x }).ToList();
                                    package.DependsOn.Add(alternatePackages);
                                }
                                else
                                {
                                    //Normal dependencies without version
                                    string normalDependency = dependency.Trim().Split(new char[] { ' ' })[0];
                                    package.DependsOn.Add(new List<Dependency>() { new Dependency() { Name = normalDependency } });
                                }
                            }
                            break;
                    }
                }

                //Make sure that the last package is added
                if (currentLineInex == lines.Length - 1)
                {
                    packages.Add(package.Name, package);
                    package = new PackageItem() { DependsOn = new List<List<Dependency>>(), RequiredBy = new List<string>() };
                    continue;
                }
            }
            ParseDependencies(packages);

            return packages;
        }

        private void ParseDependencies(Dictionary<string, PackageItem> packages)
        {
            //Check each package
            foreach (var package in packages)
            {
                //Package can have dependencies
                foreach (var dependency in package.Value.DependsOn)
                {
                    //Some packages can have multiple alternate dependencies
                    foreach (Dependency alternative in dependency)
                    {
                        //If the package is found in the main list of packages, update required by and register the existance of package
                        if (packages.ContainsKey(alternative.Name))
                        {
                            if (packages[alternative.Name].RequiredBy == null)
                                packages[alternative.Name].RequiredBy = new List<string>();
                            packages[alternative.Name].RequiredBy.Add(package.Key);
                            alternative.Exists = true;
                        }
                    }
                }
            }
        }
    }
}