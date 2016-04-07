using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectTestProject.Domain.Model
{
    public class Subject
    {
        public Subject()
        {
            Units = new List<Unit>();
            Assessments = new List<Assessment>();
        }
        public Subject(string Name)
        {
            this.Name = Name;
            this.Units = new List<Unit>();
            this.Assessments = new List<Assessment>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Unit> Units { get; set; }
        public List<Assessment> Assessments { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public void AddUnit(Unit unit)
        {
            this.Units.Add(unit);
        }
        public void AddAssessment(Assessment assessment)
        {
            this.Assessments.Add(assessment);
        }
        public void RemoveAssessment(Assessment assessment)
        {
            this.Assessments.Remove(assessment);
        }
    }
}
