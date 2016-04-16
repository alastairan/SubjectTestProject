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
using SubjectTestProject.Domain;
using SubjectTestProject.Domain.Model;
using SubjectTestProject.UI.Models;

namespace SubjectTestProject.UI.Controllers
{
    public class CourseDeliveryController : ApiController
    {
        private DomainDbContext db = new DomainDbContext();

        // GET: api/CourseDelivery
        public IQueryable<CourseDelivery> GetCourseDeliveries()
        {
            return db.CourseDeliveries;
        }

        // GET: api/CourseDelivery/5
        [HttpGet]
        public IHttpActionResult GetCourseDelivery(int id)
        {
            CourseDelivery courseDelivery = db.CourseDeliveries.Find(id);
            if (courseDelivery == null)
            {
                return NotFound();
            }

            return Ok(courseDelivery);
        }
        [HttpGet]
        public IHttpActionResult GetSubjectDelivery(int Courseid)
        {
            return Ok();
        }

        // PUT: api/CourseDelivery/5
        [HttpPut]
        public IHttpActionResult PutCourseDelivery(int id, CourseDelivery courseDelivery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != courseDelivery.Id)
            {
                return BadRequest();
            }

            db.Entry(courseDelivery).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseDeliveryExists(id))
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

        // POST: api/CourseDelivery
        [HttpPost]
        public IHttpActionResult PostCourseDelivery(CourseDeliveryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Semestar semestar = new Semestar();
            semestar.Code = model.Code;
            semestar.StartDate = model.StartDate;
            semestar.EndDate = model.EndDate;
            CourseDelivery courseDelivery = new CourseDelivery();
            courseDelivery.DeliveryType = model.DeliveryType;
            courseDelivery.Semestar = semestar;
            db.CourseDeliveries.Add(courseDelivery);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = courseDelivery.Id }, courseDelivery);
        }

        // DELETE: api/CourseDelivery/5
        [HttpDelete]
        public IHttpActionResult DeleteCourseDelivery(int id)
        {
            CourseDelivery courseDelivery = db.CourseDeliveries.Find(id);
            if (courseDelivery == null)
            {
                return NotFound();
            }

            db.CourseDeliveries.Remove(courseDelivery);
            db.SaveChanges();

            return Ok(courseDelivery);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CourseDeliveryExists(int id)
        {
            return db.CourseDeliveries.Count(e => e.Id == id) > 0;
        }
    }
}