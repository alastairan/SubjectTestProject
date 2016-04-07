using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubjectTestProject.Domain.Model
{
    public class Assessment
    {
        public Assessment()
        {
            this.Units = new List<Unit>();
        }
        public Assessment(string Code, type Type, DateTime StartDate, DateTime EndDate)
        {
            this.Code = Code;
            this.Type = Type;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.Units = new List<Unit>();
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public type Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Unit> Units { get; set; }
        public enum type
        {
            WrittenExam,
            VerbalQuestioning,
            Observation,
            Project,
            Portfolio
        }
        public void AddUnit(Unit unit)
        {
            this.Units.Add(unit);
        }
    }
}
