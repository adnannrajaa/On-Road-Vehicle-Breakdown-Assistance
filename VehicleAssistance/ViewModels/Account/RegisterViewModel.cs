using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleAssistance.Models.Account;
using VehicleAssistance.Models.DataBaseModel;

namespace VehicleAssistance.ViewModels.Account
{
    public class RegisterViewModel
    {
        public static bool AddNewUserData(RegisterModel _model)
        {
            using (VehicleAssistanceDbContext db = new VehicleAssistanceDbContext())
            {
                User addNewUser = new User();
                addNewUser = _model.User;
                db.Users.Add(addNewUser);
                if (addNewUser.RoleId == 2)
                {
                    CustomerDetail newCust = new CustomerDetail();
                    newCust = _model.CustomerDetail;
                    newCust.UserId = addNewUser.UserId;
                    db.CustomerDetails.Add(newCust);

                }
                else if (addNewUser.RoleId == 3)
                {
                    MechanicsDetail newMech = new MechanicsDetail();
                    newMech = _model.MechanicsDetail;
                    newMech.UserId = addNewUser.UserId;
                    newMech.IsBlock = false;
                    db.MechanicsDetails.Add(newMech);
                }

                db.SaveChanges();
            }
            return true;
        }
    }
}