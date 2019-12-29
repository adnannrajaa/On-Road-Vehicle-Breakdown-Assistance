using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleAssistance.Models.DataBaseModel;
using VehicleAssistance.ViewModels.Admin;

namespace VehicleAssistance.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetMechanicsUser()
        {
            return View();
        }
        public ActionResult GetCustomersUser()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllUsers(int roleId)
        {
            var data = UsersViewModel.GetAllUsers(roleId);
            return Json(new { result = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllMechanicsUsers(int roleId)
        {
            var data = MechanicsViewModel.GetAllMechanics(roleId);
            return Json(new { result = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EnableDisable(int id)
        {
            bool status = false;
            using (VehicleAssistanceDbContext db = new VehicleAssistanceDbContext())
            {
                var machnicId = db.MechanicsDetails.Join(db.Users, x => x.UserId, y => y.UserId, (x, y) => new { x, y }).Where(s=>s.x.UserId==id).Select(s => (s.x.MechanicsDetailId)).FirstOrDefault();

                var finddata = db.MechanicsDetails.Find(machnicId);
                if (finddata.IsBlock == true)
                {
                    finddata.IsBlock = false;
                }
                else
                {
                    finddata.IsBlock = true;
                    status = true;
                }
                db.SaveChanges();
            }
            return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        [HttpGet]
        public JsonResult ShowUsersFeedback()
        {
            var data = UsersViewModel.GetUsersfeedback();
            return Json(new { result = data }, JsonRequestBehavior.AllowGet);
        }

    }
}