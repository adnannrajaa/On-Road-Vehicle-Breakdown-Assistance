using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleAssistance.Models.Admin;
using VehicleAssistance.Models.DataBaseModel;

namespace VehicleAssistance.ViewModels.Admin
{
    public class UsersViewModel
    {
        public IList<UsersModel> Users { get; set; }
        public IList<UsersFeedbackModel> feedback { get; set; }
        public static UsersViewModel GetAllUsers(int RoleId)
        {
            using (VehicleAssistanceDbContext db = new VehicleAssistanceDbContext())
            {
                var customers = new UsersViewModel
                {
                    Users = db.Users.Join(db.CustomerDetails,x=>x.UserId,y=>y.UserId,(x,y) => new {x,y }).Where(s => (s.x.RoleId == RoleId)).Select(f => new UsersModel
                    {
                        UserId = f.x.UserId,
                        UserName = f.x.UserName,
                        FirstName = f.x.FirstName,
                        LastName = f.x.LastName,
                        PhoneNumber = f.x.PhoneNumber,
                        CarType = f.y.CarType
                        
                    }).ToList()
                };
                return customers;
            }
        }

        internal static UsersViewModel GetUsersfeedback()
        {
            using (VehicleAssistanceDbContext db = new VehicleAssistanceDbContext())
            {
                var feedBack = new UsersViewModel
                {
                    feedback = db.Users.Join(db.Feedbacks, x => x.UserId, y => y.UserId, (x, y) => new { x, y }).Select(f => new UsersFeedbackModel
                    {
                        UserRoleId = f.x.RoleId,
                        FullName = f.x.FirstName+" "+f.x.LastName,
                        Feedbackdata =  f.y.FeedbackData

                    }).ToList()
                };
                return feedBack;
            }
        }
    }
}