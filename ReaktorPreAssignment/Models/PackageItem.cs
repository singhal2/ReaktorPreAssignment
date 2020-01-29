using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReaktorPreAssignment.Models
{
    public class PackageItem
    {
        public string Name { get; set; }
        public string Decription { get; set; }
        public List<List<Dependency>> DependsOn { get; set; }
        public List<string> RequiredBy { get; set; }
    }
}