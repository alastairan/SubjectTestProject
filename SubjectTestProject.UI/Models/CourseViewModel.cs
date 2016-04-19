using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectTestProject.UI.Models
{
    public class CourseViewModel
    {
        public CourseViewModel()
        {
            Units = new List<UnitViewModel>();
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<UnitViewModel> Units { get; set; }
        public void AddUnit(UnitViewModel unit)
        {
            Units.Add(unit);
        }
    }
}
