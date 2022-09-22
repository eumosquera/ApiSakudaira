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
using ApiSakudaira;

namespace ApiSakudaira.Controllers
{
    public class ReservacionsController : ApiController
    {
        private SakudairaEntities db = new SakudairaEntities();

        // GET: api/Reservacions
        public IQueryable<Reservacion> GetReservacion()
        {
            return db.Reservacion;
        }

        // GET: api/Reservacions/5
        [ResponseType(typeof(Reservacion))]
        public IHttpActionResult GetReservacion(int id)
        {
            Reservacion reservacion = db.Reservacion.Find(id);
            if (reservacion == null)
            {
                return NotFound();
            }

            return Ok(reservacion);
        }

        // PUT: api/Reservacions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReservacion(int id, Reservacion reservacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservacion.Num_Reserva)
            {
                return BadRequest();
            }

            db.Entry(reservacion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservacionExists(id))
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

        // POST: api/Reservacions
        [ResponseType(typeof(Reservacion))]
        public IHttpActionResult PostReservacion(Reservacion reservacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reservacion.Add(reservacion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = reservacion.Num_Reserva }, reservacion);
        }

        // DELETE: api/Reservacions/5
        [ResponseType(typeof(Reservacion))]
        public IHttpActionResult DeleteReservacion(int id)
        {
            Reservacion reservacion = db.Reservacion.Find(id);
            if (reservacion == null)
            {
                return NotFound();
            }

            db.Reservacion.Remove(reservacion);
            db.SaveChanges();

            return Ok(reservacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservacionExists(int id)
        {
            return db.Reservacion.Count(e => e.Num_Reserva == id) > 0;
        }
    }
}