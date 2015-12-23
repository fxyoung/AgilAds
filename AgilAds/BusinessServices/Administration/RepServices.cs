using AgilAds.DAL;
using AgilAds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;

namespace AgilAds.BusinessServices.Administration
{
    public class RepServices : IRepServices
    {
        private string[] include = new string[] { "FocalPoint", "Organization" };
        private readonly IUnitOfWork _uow;
        public RepServices(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Rep GetRepById(int id)
        {
            return _uow.RepRepository.GetByID(id);
        }

        public Rep GetRepByName(string username)
        {
            return null;// _uow.RepRepository.GetSingleOrDefault(r => r.FocalPoint.Username.Equals(username));
        }

        public IEnumerable<Rep> GetWithInclude(
            System.Linq.Expressions.Expression<Func<Rep, Boolean>> predicate,
            string[] include)
        {
            return _uow.RepRepository.GetWithInclude(predicate, include);
        }

        public IEnumerable<RepListAllView> GetAllReps()
        {
            var subclasses = new string[] { "FocalPoint" };
            var l = _uow.RepRepository.GetWithInclude(null, subclasses);
            var r = new List<RepListAllView>();
            foreach(var viewEntry in l)
            {
                r.Add(new RepListAllView(viewEntry));
            }
            return r;
        }

        public int CreateRep(Rep rep)
        {
            //using (var scope = new TransactionScope())
            {
                _uow.RepRepository.Insert(rep);
                _uow.Save();
                //scope.Complete();
            }
            return rep.id;
        }

        public bool UpdateRep(int id, Rep modifiedRep)
        {
            var success = false;
            //using (var scope = new TransactionScope())
            {
                var repExists = _uow.RepRepository.Exists(id);
                if (repExists)
                {
                    _uow.RepRepository.Update(modifiedRep);
                    _uow.Save();
                    //scope.Complete();
                    success = true;
                }
            }
            return success;
        }

        public bool DeleteRep(int id)
        {
            var success = false;
            //using (var scope = new TransactionScope())
            {
                _uow.RepRepository.Delete(id);
                _uow.Save();
                //scope.Complete();
                success = true;
            }
            return success;
        }
    }
   
    public class RepServicesAsync : IRepServicesAsync
    {
        private string[] include = new string[] { "FocalPoint", "Organization" };
        private readonly IUnitOfWorkAsync _uow;
        public RepServicesAsync(IUnitOfWorkAsync uow)
        {
            _uow = uow;
        }

        public async Task<Rep> GetRepById(int id)
        {
            return await _uow.RepRepository.GetByID(id);
        }

        //public async Task<Rep> GetRepByName(string username)
        //{
        //    throw new NotImplementedException();// _uow.RepRepository.GetSingleOrDefault(r => r.FocalPoint.Username.Equals(username));
        //}

        public async Task<IEnumerable<Rep>> GetWithInclude(
            System.Linq.Expressions.Expression<Func<Rep, Boolean>> predicate,
            string[] include)
        {
            return await _uow.RepRepository.GetWithInclude(predicate, include);
        }

        public async Task<IEnumerable<RepListAllView>> GetAllReps()
        {
            var subclasses = new string[] { "FocalPoint" };
            var l = await _uow.RepRepository.GetWithInclude(null, subclasses);
            var r = new List<RepListAllView>();
            foreach (var viewEntry in l)
            {
                r.Add(new RepListAllView(viewEntry));
            }
            return r;
        }

        public async Task<int> CreateRep(Rep rep)
        {
            //using (var scope = new TransactionScope())
            {
                _uow.RepRepository.Insert(rep);
                await _uow.Save();
                //scope.Complete();
            }
            return rep.id;
        }

        public async Task<bool> UpdateRep(int id, Rep modifiedRep)
        {
            var success = false;
           // using (var scope = new TransactionScope())
            {
                var repExists = await _uow.RepRepository.Exists(id);
                if (repExists)
                {
                    _uow.RepRepository.Update(modifiedRep);
                    await _uow.Save();
                    //scope.Complete();
                    success = true;
                }
            }
            return success;
        }

        public async Task<bool> DeleteRep(int id)
        {
            var success = false;
            //using (var scope = new TransactionScope())
            {
                _uow.RepRepository.Delete(id);
                await _uow.Save();
               // scope.Complete();
                success = true;
            }
            return success;
        }
    }
}