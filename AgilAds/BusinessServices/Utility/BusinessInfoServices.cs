using AgilAds.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace AgilAds.BusinessServices.Utility
{
    public class BusinessInfoServices : IBusinessInfoServices
    {
        private readonly IUnitOfWork _uow;
        public BusinessInfoServices(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public Models.BusinessInfo GetBusinessInfoById(object id)
        {
            return _uow.BusinessRepository.GetByID(id);
        }

        public Models.BusinessInfo GetBusinessInfoByName(string orgName)
        {
            return _uow.BusinessRepository.
                GetSingle(b=>b.OrganizationName.Equals(orgName));
        }

        public IEnumerable<Models.BusinessInfo> GetAllBusinessInfo()
        {
            return _uow.BusinessRepository.GetAll();
        }

        public int CreateBusinessInfo(Models.BusinessInfo businessInfo)
        {
            using(var scope = new TransactionScope())
            {
                _uow.BusinessRepository.Insert(businessInfo);
                _uow.Save();
                scope.Complete();
            }
            return businessInfo.id;
        }

        public bool UpdateBusinessInfo(int id, Models.BusinessInfo modifiedBusinessInfo)
        {
            var success = false;
            using (var scope = new TransactionScope())
            {
                var infoExists = _uow.BusinessRepository.Exists(id);
                if (infoExists)
                {
                    _uow.BusinessRepository.Update(modifiedBusinessInfo);
                    _uow.Save();
                    scope.Complete();
                    success = true;
                }
                return success;
            }
        }

        public bool DeleteBusinessInfo(object id)
        {
            var success = false;
            using (var scope = new TransactionScope())
            {
                var infoExists = _uow.BusinessRepository.Exists(id);
                if (infoExists)
                {
                    _uow.BusinessRepository.Delete(id);
                    _uow.Save();
                    scope.Complete();
                    success = true;
                }
                return success;
            }
        }
    }
}