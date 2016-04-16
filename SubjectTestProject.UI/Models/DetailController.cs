using SubjectTestProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SubjectTestProject.UI.Models
{
    public class DetailController : ApiController
    {
        private DomainDbContext db = new DomainDbContext();
        // GET: api/Detail

        // GET: api/Detail/5
        [HttpGet]
        public IHttpActionResult GetDetail(int id)
        {
            DetailVieiwModel detail = db.Subjects.Include("Course").Select(d => new DetailVieiwModel()
            {
                Id = d.Id,
                Name = d.Name,
                CourseCode = d.Course.Code,
                CourseName = d.Course.Name
            }).FirstOrDefault(s => s.Id == id);
            if (detail == null)
            {
                return NotFound();
            }

            return Ok(detail);
        }
    }
}
