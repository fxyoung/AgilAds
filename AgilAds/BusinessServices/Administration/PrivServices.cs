using AgilAds.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace AgilAds.BusinessServices.Administration
{
    public class PrivServices : IPrivServices
    {
        private IUnitOfWork _uow;
        public PrivServices(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<Models.Priv> GetPrivsForUser(string username)
        {
            if (String.IsNullOrWhiteSpace(username))
            {
                return new List<Models.Priv>();
            }
            return _uow.PrivRepository.GetMany(p => p.Username.Equals(username));
        }

        public Models.Priv GetPrivById(int id)
        {
            return _uow.PrivRepository.GetByID(id);
        }

        public int CreatePriv(Models.Priv priv)
        {
            using (var scope = new TransactionScope())
            {
                _uow.PrivRepository.Insert(priv);
                _uow.Save();
                scope.Complete();
                return priv.id;
            }
        }

        public bool DeletePriv(int id)
        {
            var success = false;
            using (var scope = new TransactionScope())
            {
                var entityToDeleteExists = _uow.PrivRepository.Exists(id);
                if (entityToDeleteExists)
                {
                    _uow.PrivRepository.Delete(id);
                    _uow.Save();
                    scope.Complete();
                    success = true;
                }
            }
            return success;
        }
    }
}