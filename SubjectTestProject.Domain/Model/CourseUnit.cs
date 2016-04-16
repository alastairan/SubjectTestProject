using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.ComponentModel.DataAnnotations;

namespace SubjectTestProject.Domain.Model
{
    public class CourseUnit
    {
        public int Id { get; set; }
        public bool? IsEssential { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
    }
}
