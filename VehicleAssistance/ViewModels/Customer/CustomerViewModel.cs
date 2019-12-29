using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleAssistance.Models.Customer;
using VehicleAssistance.Models.DataBaseModel;

namespace VehicleAssistance.ViewModels.Customer
{
    public class CustomerViewModel
    {
        public IList<CustomerModel> Customer { get; set; }
        public static CustomerViewModel GetAllMechanics()
        {
            using (VehicleAssistanceDbContext db = new VehicleAssistanceDbContext())
            {
                var Mechanic = new CustomerViewModel
                {
                    Customer = db.Users.Join(db.MechanicsDetails, x => x.UserId, y => y.UserId, (x, y) => new { x, y }).Where(s => (s.y.IsBlock!=true)).Select(f => new CustomerModel
                    {
                        MechanicId = f.y.MechanicsDetailId,
                        FirstName = f.x.FirstName,
                        LastName= f.x.LastName,
                        PhoneNumber = f.x.PhoneNumber,
                        Location = f.y.Location,
                    }).ToList()
                };
                return Mechanic;
            }
        }

        public static CustomerViewModel GetAllRequestedMechanics(int currentUserId)
        {
            using (VehicleAssistanceDbContext db = new VehicleAssistanceDbContext())
            {
                var customerId = db.CustomerDetails.Where(s => s.UserId == currentUserId).Select(s => s.CustomerDetailId).FirstOrDefault();
                var mechanicRequestId = db.CustomerRequests.Where(s => s.CustomerId == customerId).ToList();
                List<CustomerModel> UsersData = new List<CustomerModel>();
                foreach (var items in mechanicRequestId)
                {
                    CustomerModel userDetail = db.Users.Join(db.MechanicsDetails, x => x.UserId, y => y.UserId, (x, y) => new
                    {
                        x,
                        y
                    }).Where(s => s.y.MechanicsDetailId == items.MechanicId).Select(f =>
                    new CustomerModel
                    {
                       
                        RequestId = items.RequestId,
                        FirstName = f.x.FirstName,
                        LastName = f.x.LastName,
                        PhoneNumber = f.x.PhoneNumber,
                        Location = f.y.Location
                    }).FirstOrDefault();
                    UsersData.Add(userDetail);
                }
                var customers = new CustomerViewModel
                {
                    Customer = UsersData
                };
                return customers;
            }
        }

        public static bool SaveCustomerFeedback(int requestId , int currentUserId ,string feebackdata)
        {
            using (VehicleAssistanceDbContext db = new VehicleAssistanceDbContext())
            {
                Feedback feedBack = new Feedback();
                feedBack.RequestId = requestId;
                feedBack.UserId = currentUserId;
                feedBack.FeedbackData = feebackdata;
                db.Feedbacks.Add(feedBack);
                db.SaveChanges();
            }
            return true;
        }

        public static bool SendRequest(int currentUserId, int mechanicId)
        {
            using (VehicleAssistanceDbContext db = new VehicleAssistanceDbContext())
            {
                var customerId = db.CustomerDetails.Where(s => s.UserId == currentUserId).Select(s => s.CustomerDetailId).FirstOrDefault();

                CustomerRequest custR = new CustomerRequest();
                custR.MechanicId = mechanicId;
                custR.CustomerId = customerId;
                db.CustomerRequests.Add(custR);
                db.SaveChanges();
            }
            return true;
        }
    }
}