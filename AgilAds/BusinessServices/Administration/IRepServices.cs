using AgilAds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilAds.BusinessServices.Administration
{
    public interface IRepServices
    {
        Models.Rep GetRepById(int id);
        Models.Rep GetRepByName(string username);
        IEnumerable<Models.RepListAllView> GetAllReps();
        IEnumerable<Rep> GetWithInclude(
            System.Linq.Expressions.Expression<Func<Rep, Boolean>> predicate, string[] include);
        int CreateRep(Models.Rep rep);
        bool UpdateRep(int id, Models.Rep modifiedRep);
        bool DeleteRep(int id);
    }
    public interface IRepServicesAsync
    {
        Task<Models.Rep> GetRepById(int id);
        //Task<Models.Rep> GetRepByName(string username);
        Task<IEnumerable<Models.RepListAllView>> GetAllReps();
        Task<IEnumerable<Rep>> GetWithInclude(
            System.Linq.Expressions.Expression<Func<Rep, Boolean>> predicate, string[] include);
        Task<int> CreateRep(Models.Rep rep);
        Task<bool> UpdateRep(int id, Models.Rep modifiedRep);
        Task<bool> DeleteRep(int id);
    }
}
