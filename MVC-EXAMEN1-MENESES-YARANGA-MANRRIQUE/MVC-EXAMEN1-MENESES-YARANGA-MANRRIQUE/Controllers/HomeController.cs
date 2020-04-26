using MVC_EXAMEN1_MENESES_YARANGA_MANRRIQUE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC_EXAMEN1_MENESES_YARANGA_MANRRIQUE.Controllers
{

    public class HomeController : Controller
    {
        NorthwindEntities1 db = new NorthwindEntities1();
        public ActionResult Index()
        {
            ViewBag.lista = db.Orders.Where(a => a.ConfirmationStatus != true).ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.lista = db.Orders.Where(a => a.ConfirmationStatus == true).ToList();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "MENESES YARANGA, Manrrique.";
            return View();
        }

        public ActionResult Pendiente(int id)
        {
            ViewBag.orden = db.Orders.Find(id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Confirma(Confirmation confirmation, int id)
        {
            Orders orden = db.Orders.Find(id);
            Confirmation confirme = confirmation.ConfirmationID == 0 ? new Confirmation(): db.Confirmation.Find(confirmation.ConfirmationID);
            confirme.ConfirmationDate = confirmation.ConfirmationDate;
            confirme.ConfimationNote = confirmation.ConfimationNote;
            confirme.ConfirmationStatus = confirmation.ConfirmationStatus;

            if(confirmation.ConfirmationID == 0)
            {
                db.Confirmation.Add(confirme);
            }
            else {
                db.Entry(confirme).State = EntityState.Modified;
            }
            await db.SaveChangesAsync();

            orden.ConfirmationStatus = confirme.ConfirmationStatus;
            orden.ConfirmationID = confirme.ConfirmationID;

            db.Entry(orden).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Json(new { msg = "Confirmado Correctamente" });
        }
    }
}