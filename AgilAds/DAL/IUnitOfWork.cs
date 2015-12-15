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
        GenericRepository<Priv> PrivRepository { get; }
        GenericRepository<Rep> RepRepository { get; }

        /// <summary>
        /// Save method.
        /// </summary>
        void Save();
        void Dispose();
    }
    public interface IUnitOfWorkAsync
    {
        GenericRepositoryAsync<Priv> PrivRepository { get; }
        GenericRepositoryAsync<Rep> RepRepository { get; }

        /// <summary>
        /// Save method.
        /// </summary>
        void Save();
        void Dispose();
    }
}
