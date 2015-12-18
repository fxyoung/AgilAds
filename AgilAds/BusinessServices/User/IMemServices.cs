using AgilAds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilAds.BusinessServices.User
{
    public interface IMemServices
    {
        Task<Rep> GetRoot(int repId);
        Member GetMemById(Rep rep, int id);
        Member GetMemByName(Rep rep, string username);
        IEnumerable<MemListAllView> GetAllMems(Rep rep);
        IEnumerable<Member> GetWithInclude(Rep rep, 
        System.Linq.Expressions.Expression<Func<Rep, Boolean>> predicate, string[] include);
        int CreateMem(Rep rep, Member mem);
        bool UpdateMem(Rep rep, int id, Member modifiedMem);
        bool DeleteMem(Rep rep, int id);
    }
}
