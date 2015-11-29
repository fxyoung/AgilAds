using AgilAds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilAds.BusinessServices.Administration
{
    interface IPrivServices
    {
        //Super Priviledge services
        IEnumerable<Priv> GetPrivsForUser(string username);
        Priv GetPrivById(int id);
        int CreatePriv(Priv priv);
        bool DeletePriv(int id);
    }
}
