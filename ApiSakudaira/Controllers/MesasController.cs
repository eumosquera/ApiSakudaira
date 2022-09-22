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
    public class MesasController : ApiController
    {
        private SakudairaEntities db = new SakudairaEntities();

        // GET: api/Mesas
        public IQueryable<Mesa> GetMesa()
        {
            return db.Mesa;
        }

        // GET: api/Mesas/5
        [ResponseType(typeof(Mesa))]
        public IHttpActionResult GetMesa(int id)
        {
            Mesa mesa = db.Mesa.Find(id);
            if (mesa == null)
            {
                return NotFound();
            }

            return Ok(mesa);
        }

        // PUT: api/Mesas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMesa(int id, Mesa mesa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mesa.Num_Mesa)
            {
                return BadRequest();
            }

            db.Entry(mesa).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MesaExists(id))
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

        // POST: api/Mesas
        [ResponseType(typeof(Mesa))]
        public IHttpActionResult PostMesa(Mesa mesa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Mesa.Add(mesa);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mesa.Num_Mesa }, mesa);
        }

        // DELETE: api/Mesas/5
        [ResponseType(typeof(Mesa))]
        public IHttpActionResult DeleteMesa(int id)
        {
            Mesa mesa = db.Mesa.Find(id);
            if (mesa == null)
            {
                return NotFound();
            }

            db.Mesa.Remove(mesa);
            db.SaveChanges();

            return Ok(mesa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MesaExists(int id)
        {
            return db.Mesa.Count(e => e.Num_Mesa == id) > 0;
        }
    }
}