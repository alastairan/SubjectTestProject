using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectTestProject.DataAccess.Model
{
    public class UnitDTO
    {
        public UnitDTO()
        {

        }
        public int Id { get; set; }
        public string Code { get; set; }
        public bool? IsEssential { get; set; }
        public string Name { get; set; }
    }
}
