using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectTestProject.DataAccess.Model
{
    public class UnitTrainingComponentDTO
    {
        public UnitTrainingComponentDTO()
        {

        }
        public string Code { get; set; }
        public bool? IsEssential { get; set; }
        public string Name { get; set; }
        public string AssessmentRequirements { get; set; }
        public string Elements { get; set; }
    }
}
