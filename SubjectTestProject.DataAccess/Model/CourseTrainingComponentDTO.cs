using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectTestProject.DataAccess.Model
{
    public class CourseTrainingComponentDTO
    {
        public CourseTrainingComponentDTO()
        {
            this.Units = new List<UnitTrainingComponentDTO>();
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<UnitTrainingComponentDTO> Units { get; set; }
        public string ParentCode { get; set; }
        public string ParentTitle { get; set; }
        public void AddUnit(UnitTrainingComponentDTO unit)
        {
            Units.Add(unit);
        }
    }
}
