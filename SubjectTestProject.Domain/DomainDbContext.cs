using SubjectTestProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectTestProject.Domain
{
    public class DomainDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<CourseDelivery> CourseDeliveries { get; set; }
        public DbSet<SubjectDelivery> SubjectDeliveries { get; set; }
    }
    
}
