using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectTestProject.UI.Models
{
    public class UnitViewModel
    {
        public UnitViewModel()
        {

        }
        public int Id { get; set; }
        public string Code { get; set; }
        public bool? IsEssential { get; set; }
        public string Name { get; set; }
    }
}
