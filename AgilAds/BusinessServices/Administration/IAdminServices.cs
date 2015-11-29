using AgilAds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgilAds.BusinessServices.Administration
{
    interface IAdminServices
    {
        //Owner Admin servicesAdmin
        Admin GetAdminById(int repId, int id);
        Admin GetAdminByName(int? repId, string name);
        IEnumerable<Admin> GetAllAdmins(int repId);
        int CreateAdmin(int repId, Admin admin);
        bool UpdateAdmin(int repId, int id, Admin modifiedAdmin);
        bool DeleteAdmin(int repId, int id);
    }
}