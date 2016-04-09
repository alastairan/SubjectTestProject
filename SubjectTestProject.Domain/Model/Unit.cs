using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubjectTestProject.Domain.Model
{
    public class Unit
    {
        public Unit()
        {
            this.AssessmentRequirements = null;
            this.Elements = null;
        }
        public Unit(string Code, string Name)
        {
            this.Code = Code;
            this.Name = Name;
        }

        public Unit(string Code, string Name, string AssessmentRequirements, string Elements)
        {
            this.Code = Code;
            this.Name = Name;
            this.AssessmentRequirements = AssessmentRequirements;
            this.Elements = Elements;
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string AssessmentRequirements { get; set; }
        public string Elements { get; set; }
    }
}
