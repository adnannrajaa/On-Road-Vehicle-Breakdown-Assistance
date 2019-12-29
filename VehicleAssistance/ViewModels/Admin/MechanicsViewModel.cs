using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleAssistance.Models.Admin;
using VehicleAssistance.Models.DataBaseModel;

namespace VehicleAssistance.ViewModels.Admin
{
    public class MechanicsViewModel
    {
        public IList<MechanicsModel> Mechanics { get; set; }
        public static MechanicsViewModel GetAllMechanics(int RoleId)
        {
            using (VehicleAssistanceDbContext db = new VehicleAssistanceDbContext())
            {
                var Mechanic = new MechanicsViewModel
                {
                    Mechanics = db.Users.Join(db.MechanicsDetails, x => x.UserId, y => y.UserId, (x, y) => new { x, y }).Where(s => (s.x.RoleId == RoleId)).Select(f => new MechanicsModel
                    {
                        UserId = f.x.UserId,
                        UserName = f.x.UserName,
                        FullName = f.x.FirstName +" "+f.x.LastName,
                        PhoneNumber = f.x.PhoneNumber,
                        Location = f.y.Location,
                        IsBlock = f.y.IsBlock
                    }).ToList()
                };
                return Mechanic;
            }
        }

       
    }
}