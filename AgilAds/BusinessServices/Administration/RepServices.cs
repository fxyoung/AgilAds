using AgilAds.DAL;
using AgilAds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return _uow.RepRepository.GetFunc(r => r.FocalPoint.Fullname.Equals(username));
        }

        public IEnumerable<Rep> GetWithInclude(
            System.Linq.Expressions.Expression<Func<Rep, Boolean>> predicate,
            string[] include)
        {
            return _uow.RepRepository.GetWithInclude(predicate, include);
        }

        public IEnumerable<Rep> GetAllReps()
        {
            return _uow.RepRepository.GetWithInclude(null, include);
        }

        public int CreateRep(Rep rep)
        {
            using (var scope = new TransactionScope())
            {
                _uow.RepRepository.Insert(rep);
                _uow.Save();
                scope.Complete();
            }
            return rep.id;
        }

        public bool UpdateRep(int id, Rep modifiedRep)
        {
            var success = false;
            using (var scope = new TransactionScope())
            {
                var repExists = _uow.RepRepository.Exists(id);
                if (repExists)
                {
                    _uow.RepRepository.Update(modifiedRep);
                    _uow.Save();
                    scope.Complete();
                    success = true;
                }
            }
            return success;
        }

        public bool DeleteRep(int id)
        {
            var success = false;
            using (var scope = new TransactionScope())
            {
                _uow.RepRepository.Delete(id);
                _uow.Save();
                scope.Complete();
                success = true;
            }
            return success;
        }
    }
}