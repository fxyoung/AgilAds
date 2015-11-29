using AgilAds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilAds.DAL
{
    public interface IUnitOfWork
    {
        GenericRepository<Admin> AdminRepository { get; }
        GenericRepository<Priv> PrivRepository { get; }
        GenericRepository<Rep> RepRepository { get; }
        GenericRepository<Person> PersonRepository { get; }
        GenericRepository<BusinessInfo> BusinessRepository { get; }

        /// <summary>
        /// Save method.
        /// </summary>
        void Save();
        void Dispose();
    }
}
