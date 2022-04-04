using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto_Covid_Final;

namespace Proyecto_Covid_Final.Controllers
{
    public class NotificacionesController : Controller
    {
        private proyectocovidEntities db = new proyectocovidEntities();

        // GET: Notificaciones
        public ActionResult Index()
        {
            var notificaciones = db.Notificaciones.Include(n => n.Paciente);
            return View(notificaciones.ToList());
        }

        // GET: Notificaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notificaciones notificaciones = db.Notificaciones.Find(id);
            if (notificaciones == null)
            {
                return HttpNotFound();
            }
            return View(notificaciones);
        }

        // GET: Notificaciones/Create
        public ActionResult Create()
        {
            ViewBag.noti_ID = new SelectList(db.Paciente, "paciente_ID", "paciente_nombres");
            return View();
        }

        // POST: Notificaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "noti_ID,noti_asunto,noti_cuerpo,noti_respuesta,paciente_ID")] Notificaciones notificaciones)
        {
            if (ModelState.IsValid)
            {
                db.Notificaciones.Add(notificaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.noti_ID = new SelectList(db.Paciente, "paciente_ID", "paciente_nombres", notificaciones.noti_ID);
            return View(notificaciones);
        }

        // GET: Notificaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notificaciones notificaciones = db.Notificaciones.Find(id);
            if (notificaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.noti_ID = new SelectList(db.Paciente, "paciente_ID", "paciente_nombres", notificaciones.noti_ID);
            return View(notificaciones);
        }

        // POST: Notificaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "noti_ID,noti_asunto,noti_cuerpo,noti_respuesta,paciente_ID")] Notificaciones notificaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notificaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.noti_ID = new SelectList(db.Paciente, "paciente_ID", "paciente_nombres", notificaciones.noti_ID);
            return View(notificaciones);
        }

        // GET: Notificaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notificaciones notificaciones = db.Notificaciones.Find(id);
            if (notificaciones == null)
            {
                return HttpNotFound();
            }
            return View(notificaciones);
        }

        // POST: Notificaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notificaciones notificaciones = db.Notificaciones.Find(id);
            db.Notificaciones.Remove(notificaciones);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
