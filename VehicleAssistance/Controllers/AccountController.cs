using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleAssistance.Models.Account;
using VehicleAssistance.Models.DataBaseModel;
using VehicleAssistance.ViewModels.Account;

namespace VehicleAssistance.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            using (VehicleAssistanceDbContext db = new VehicleAssistanceDbContext())
            {
                var isAuthenticated = db.Users.Where(s => (s.UserName == username && s.Password == password)).Count();
                if (isAuthenticated == 1)
                {
                    var currentUserRole = db.Users.Join(db.Roles, x => x.RoleId, y => y.RoleId, (x, y) => new { u = x, r = y }).Where(s => s.u.UserName == username).Select(s => s.r.RoleName).FirstOrDefault();
                    if (currentUserRole == "Mechanic")
                    {
                        var IsBlock = db.MechanicsDetails.Join(db.Users, x => x.UserId, y => y.UserId, (x, y) => new { m = x, u = y }).Where(s => s.u.UserName == username).Select(s => s.m.IsBlock).FirstOrDefault();
                        if (IsBlock == true)
                        {
                            return Json(new { result = 2, UserRole = currentUserRole }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    var currentUserId = db.Users.Where(s => s.UserName == username).Select(s => s.UserId).FirstOrDefault();
                    Session["currentUser"] = username;
                    Session["currentUserId"] = currentUserId;
                    Session["currentUserRole"] = currentUserRole;
                    return Json(new { result = 1, UserRole = currentUserRole }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { result = 3 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult registerUser(RegisterModel _model)
        {
            bool IsUserAdded = RegisterViewModel.AddNewUserData(_model);
            if (IsUserAdded) {return Json(new { result = true }, JsonRequestBehavior.AllowGet);}
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult isUserExist(string username)
        {
            using (VehicleAssistanceDbContext db = new VehicleAssistanceDbContext())
            {
                var user = db.Users.Where(s => s.UserName == username).Count();
                if (user >= 1)
                {
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Logout()
        {
            Session["currentUser"] = null;
            Session["currentUserId"] = null;
            Session["currentUserRole"] = null;
            return RedirectToAction("Login", "Account");
        }

    }
}