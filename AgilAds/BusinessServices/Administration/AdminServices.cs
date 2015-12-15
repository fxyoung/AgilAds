using AgilAds.DAL;
using AgilAds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace AgilAds.BusinessServices.Administration
{
    public class AdminServices : IAdminServices
    {
        private IUnitOfWork _unitOfWork;
        public AdminServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //
        //  User Admin Services
        //
        public Admin GetAdminById(int repId, int id)
        {
            throw new NotImplementedException();// return _unitOfWork.AdminRepository.GetByID(id);
        }

        public Admin GetAdminByName(int? repId, string name)
        {
            throw new NotImplementedException();// return _unitOfWork.AdminRepository.GetFunc(a => a.Identity.Username.Equals(name));
        }

        public IEnumerable<Admin> GetAllAdmins(int repId)
        {
            //var admins = _unitOfWork.AdminRepository.GetAll();
            throw new NotImplementedException();// return admins;
        }

        public int CreateAdmin(int repId, Admin admin)
        {
            //using (var scope = new TransactionScope())
            {
                //_unitOfWork.AdminRepository.Insert(admin);
                //_unitOfWork.Save();
                //scope.Complete();
                throw new NotImplementedException();// return admin.id;
            }
        }

        public bool UpdateAdmin(int repId, int id, Admin modifiedAdmin)
        {
            //var success = false;
            //if (modifiedAdmin != null)
            //{
            //    using (var scope = new TransactionScope())
            //    {
            //        var currentAdminExists = _unitOfWork.AdminRepository.Exists(id);
            //        if (currentAdminExists)
            //        {
            //            modifiedAdmin.id = id;
            //            _unitOfWork.AdminRepository.Update(modifiedAdmin);
            //            _unitOfWork.Save();
            //            scope.Complete();
            //            success = true;
            //        }
            //    }
            //}
            throw new NotImplementedException();// return success;
        }

        public bool DeleteAdmin(int repId, int id)
        {
            //var success = false;
            //using (var scope = new TransactionScope())
            //{
            //    var adminToDeleteExists = _unitOfWork.AdminRepository.Exists(id);
            //    if (adminToDeleteExists)
            //    {
            //        _unitOfWork.AdminRepository.Delete(id);
            //        _unitOfWork.Save();
            //        scope.Complete();
            //        success = true;
            //    }
            //}
            throw new NotImplementedException();// return success;
        }
    }
}