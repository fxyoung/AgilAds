using AgilAds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilAds.BusinessServices.Utility
{
    public interface IBusinessInfoServices
    {
        BusinessInfo GetBusinessInfoById(object id);
        BusinessInfo GetBusinessInfoByName(string orgName);
        IEnumerable<BusinessInfo> GetAllBusinessInfo();
        int CreateBusinessInfo(BusinessInfo businessInfo);
        bool UpdateBusinessInfo(int id, BusinessInfo modifiedBusinessInfo);
        bool DeleteBusinessInfo(object id);
    }
}
