using AgilAds.DAL;
using AgilAds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AgilAds.BusinessServices.User
{
    public class MemServices : IMemServices
    {
        private IUnitOfWorkAsync _uow;
        public MemServices(IUnitOfWorkAsync uow)
        {
            _uow = uow;
        }
        public async Task<Rep> GetRoot(int repId)
        {
            var task = _uow.RepRepository.GetWithInclude(r => r.id.Equals(repId), new string[] { "Members" });
            var rep = await task;
            if (!rep.Count().Equals(1)) throw new ArgumentException("Root not found");
            return rep.First();
            //var task = db.Rep.Include("Members").SingleAsync(r => r.id.Equals(repId));
        }

        public Member GetMemById(Rep rep, int id)
        {
            return rep.Members.SingleOrDefault(m => m.id.Equals(id));
        }

        public Member GetMemByName(Rep rep, string username)
        {
            return null;// rep.Members.SingleOrDefault(m => m.FocalPoint.Username.Equals(username));
        }

        public IEnumerable<MemListAllView> GetAllMems(Rep rep)
        {
            var mems = rep.Members.ToList();
            var retval = new List<MemListAllView>();
            foreach (var mem in mems)
            {
                retval.Add(new MemListAllView(mem));
            }
            return retval;
        }

        public IEnumerable<Member> GetWithInclude(Rep rep, System.Linq.Expressions.Expression<Func<Rep, bool>> predicate, string[] include)
        {
            throw new NotImplementedException();
        }

        public int CreateMem(Rep rep, Member mem)
        {
            rep.Members.Add(mem);
            _uow.RepRepository.Update(rep);
            _uow.Save();
            return mem.id;
        }

        public bool UpdateMem(Rep rep, int id, Member modifiedMem)
        {
            var mem = rep.Members.SingleOrDefault(m => m.id.Equals(id));
            if (mem == null) return false;
            rep.Members.Remove(mem);
            rep.Members.Add(mem);
            _uow.Save();
            return true;
        }

        public bool DeleteMem(Rep rep, int id)
        {
            var mem = rep.Members.SingleOrDefault(m => m.id.Equals(id));
            if (mem == null) return false;
            rep.Members.Remove(mem);
            _uow.Save();
            return true;
        }
    }
}