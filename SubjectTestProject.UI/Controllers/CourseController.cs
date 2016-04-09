using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SubjectTestProject.DataAccess.Model;
using SubjectTestProject.Domain;
using SubjectTestProject.Domain.Model;
using SubjectTestProject.DataAccess;
using SubjectTestProject.DataAccess.TgaTrainingComp;
using SubjectTestProject.UI.Models;

namespace SubjectTestProject.UI.Controllers
{
    public class CourseController : ApiController
    {
        private DomainDbContext db = new DomainDbContext();

        // GET: api/Course
        [HttpGet]
        public IQueryable<Course> Get()
        {
            return db.Courses;
        }

        // GET: api/Course/5
        [HttpGet]
        public IHttpActionResult Search(string code)
        {
            code = code.ToUpperInvariant();
            //var courseDTO = Program.GetCourse(code);
            var course = new CourseDTO(code);
            if (course == null)
            {
                return NotFound();
            }
            //var dx = db.Courses.FirstOrDefault(s => s.Code == courseDTO.Code);
            //if (dx == null)
            //{
            //    var course = new Course(courseDTO.Code, courseDTO.Name, courseDTO.ParentCode, courseDTO.ParentCode);
            //    db.Courses.Add(course);
            //    db.SaveChanges();
            //}
            //else
            //{
            //    if(courseDTO.Name != null)
            //    {
            //        dx.Name = courseDTO.Name;
            //    }
            //    if(courseDTO.ParentCode != null)
            //    {
            //        dx.ParentCode = courseDTO.ParentCode;
            //    }
            //    if(courseDTO.ParentTitle != null)
            //    {
            //        dx.ParentTitle = courseDTO.ParentTitle;
            //    }
            //    db.Entry(dx).State = EntityState.Modified;
            //    db.SaveChanges();
            //}
            //CourseViewModel cvm = db.Courses.Select(p => new CourseViewModel
            //{
            //    Id = p.Id,
            //    Name = p.Name,
            //    Code = p.Code
            //}).FirstOrDefault(s => s.Code == courseDTO.Code);
            //foreach (UnitTrainingComponentDTO u in courseDTO.Units)
            //{
            //    var ux = db.Units.FirstOrDefault(s => s.Code == u.Code);
            //    if(ux == null)
            //    {
            //        var unit = new Unit(u.Code, u.Name, u.AssessmentRequirements, u.Elements);
            //        db.Units.Add(unit);
            //        db.SaveChanges();
            //    }
            //    else
            //    {
            //        if (u.Name != null)
            //        {
            //            ux.Name = u.Name;
            //        }
            //        if(u.AssessmentRequirements != null)
            //        {
            //            ux.AssessmentRequirements = u.AssessmentRequirements;
            //        }
            //        if(u.Elements != null)
            //        {
            //            ux.Elements = u.Elements;
            //        }
            //        db.Entry(ux).State = EntityState.Modified;
            //        db.SaveChanges();
            //    }
            //    var unitx = db.Units.Select(p => new UnitViewModel
            //    {
            //        Id = p.Id,
            //        Code = p.Code,
            //        Name = p.Name,
            //        IsEssential = u.IsEssential
            //    }).FirstOrDefault(a => a.Code == u.Code);
            //    cvm.AddUnit(unitx);
            //}
            return Ok(course);
        }

        // PUT: api/Course/5
        [HttpPut]
        public IHttpActionResult Put(int id, Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != course.Id)
            {
                return BadRequest();
            }

            db.Entry(course).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Course
        [HttpPost]
        public IHttpActionResult Post(Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Courses.Add(course);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = course.Id }, course);
        }

        // DELETE: api/Course/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            db.Courses.Remove(course);
            db.SaveChanges();

            return Ok(course);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CourseExists(int id)
        {
            return db.Courses.Count(e => e.Id == id) > 0;
        }
    }
}