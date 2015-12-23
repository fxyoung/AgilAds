using AgilAds.DAL;
using AgilAds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AgilAds.BusinessServices.Administration
{
    public class PrivServicesAsync : IPrivServices
    {
        private IUnitOfWorkAsync _uow;
        public PrivServicesAsync(IUnitOfWorkAsync uow)
        {
            _uow = uow;
        }
        private object retval;

        public IEnumerable<Priv> GetPrivsForUser(string username)
        {
            GetPrivsForUserIF(username);
            IEnumerable<Priv> privs = retval as IEnumerable<Priv>;
            return privs;
        }
        private async void GetPrivsForUserIF(string username)
        {
            retval = await GetPrivsForUserAsync(username);
        }
        private async Task<IEnumerable<Priv>> GetPrivsForUserAsync(string username)
        {
            return await _uow.PrivRepository.GetMany(p => p.Username.Equals(username));
        }

        public Priv GetPrivById(int id)
        {
            throw new NotImplementedException();
        }

        public int CreatePriv(Priv priv)
        {
            throw new NotImplementedException();
        }

        public bool DeletePriv(int id)
        {
            throw new NotImplementedException();
        }
    }
}