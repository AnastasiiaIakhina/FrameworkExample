using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace mantis_tests
{
    [Table(Name = "mantis_project_table")]
    public class ProjectData : IEquatable<ProjectData>, IComparable<ProjectData>
    {
        public ProjectData()
        {

        }

        public ProjectData(string name)
        {
            Name = name;
            Status = "development";
            InheritGlobalCategories = true;
            Description = "";
        }

        [Column(Name = "name")]
        public string Name { get; set; }

        [Column(Name = "status")]
        public string Status { get; set; }

        [Column(Name = "enabled")]
        public bool InheritGlobalCategories { get; set; }

        [Column(Name = "view_state")]
        public string ViewStatus { get; set; }

        [Column(Name = "description")]
        public string Description { get; set; }

        [Column(Name = "id")]
        public string Id { get; set; }

        public int CompareTo(ProjectData other)
        {
            if (Object.ReferenceEquals(other, null)) return 1;
            return Name.CompareTo(other.Name);
        }

        public bool Equals(ProjectData other)
        {
            if (Object.ReferenceEquals(other, null)) return false;
            if (Object.ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }

        public override string ToString()
        {
            return "name= " + Name;
        }

        public static List<ProjectData> GetAll()
        {
            using (MantisDB db = new MantisDB())
            {
                return (from p in db.Projects select p).ToList();
            }
        }
    }
}
