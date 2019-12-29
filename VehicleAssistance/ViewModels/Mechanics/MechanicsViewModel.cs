using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleAssistance.Models.Admin;
using VehicleAssistance.Models.DataBaseModel;

namespace VehicleAssistance.ViewModels.Mechanics
{
    public class MechanicsViewModel
    {
        public List<UsersModel> Users { get; set; }
        public static MechanicsViewModel GetCustomerRequest(int currentUserId)
        {
            using (VehicleAssistanceDbContext db = new VehicleAssistanceDbContext())
            {
                var machanicId = db.MechanicsDetails.Where(s => s.UserId == currentUserId).Select(s=>s.MechanicsDetailId).FirstOrDefault();
                var customerRequestId = db.CustomerRequests.Where(s => s.MechanicId == machanicId).ToList();
                List<UsersModel> UsersData = new List<UsersModel>();
                foreach (var items in customerRequestId)
                {
                    UsersModel userDetail = db.Users.Join(db.CustomerDetails, x => x.UserId, y => y.UserId, (x, y) => new
                    {
                        x,
                        y
                    }).Where(s => s.y.CustomerDetailId == items.CustomerId).Select(f =>
                    new UsersModel
                    {
                        UserId = f.x.UserId,
                        RequestId = items.RequestId,
                        UserName = f.x.UserName,
                        FirstName = f.x.FirstName,
                        LastName = f.x.LastName,
                        PhoneNumber = f.x.PhoneNumber,
                        CarType = f.y.CarType
                    }).FirstOrDefault();
                    UsersData.Add(userDetail);
                }
                var customers = new MechanicsViewModel
                {
                    Users = UsersData
                };
                return customers;
            }
        }
    }
}