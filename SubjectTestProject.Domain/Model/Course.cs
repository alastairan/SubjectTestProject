using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectTestProject.Domain.Model
{
    public class Course
    {
        public Course()
        {
            this.Units = new List<CourseUnit>();
        }
        public Course(string Code,string Name, string ParentCode, string ParentTitle)
        {
            this.Code = Code;
            this.Name = Name;
            this.ParentCode = ParentCode;
            this.ParentTitle = ParentTitle;
            this.Units = new List<CourseUnit>();
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ParentCode { get; set; }
        public string ParentTitle { get; set; }
        public List<CourseUnit> Units { get; set; }
        public void AddUnit(CourseUnit unit)
        {
            Units.Add(unit);
        }
        public void RemoveUnit(CourseUnit unit)
        {
            Units.Remove(unit);
        }
        public void RemoveAllUnit()
        {
            Units.Clear();
        }
    }
}
