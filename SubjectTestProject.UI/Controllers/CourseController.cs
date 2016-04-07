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
            var courseDTO = Program.GetCourse(code);
            if (courseDTO == null)
            {
                return NotFound();
            }
            return Ok(courseDTO);
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