using AgilAds.DAL;
using AgilAds.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace AgilAds.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Private member variables...
        private readonly AgilAdsDataContext _context = null;
        private GenericRepository<Admin> _adminRepository;
        private GenericRepository<Priv> _privRepository;
        private GenericRepository<Rep> _repRepository;
        private GenericRepository<Person> _personRepository;
        private GenericRepository<BusinessInfo> _businessRepository;
        #endregion

        public UnitOfWork(AgilAdsDataContext context)
        {
            _context = context;
        }

        #region Public Repository Creation properties...

        /// <summary>
        /// Get/Set Property for Super User Administrators repository.
        /// </summary>
        public GenericRepository<Admin> AdminRepository
        {
            get
            {
                if (this._adminRepository == null)
                    this._adminRepository = new GenericRepository<Admin>(_context);
                return _adminRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for user repository.
        /// </summary>
        public GenericRepository<Priv> PrivRepository
        {
            get
            {
                if (this._privRepository == null)
                    this._privRepository = new GenericRepository<Priv>(_context);
                return _privRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for token repository.
        /// </summary>
        public GenericRepository<Rep> RepRepository
        {
            get
            {
                if (this._repRepository == null)
                    this._repRepository = new GenericRepository<Rep>(_context);
                return _repRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for token repository.
        /// </summary>
        public GenericRepository<Person> PersonRepository
        {
            get
            {
                if (this._personRepository == null)
                    this._personRepository = new GenericRepository<Person>(_context);
                return _personRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for token repository.
        /// </summary>
        public GenericRepository<BusinessInfo> BusinessRepository
        {
            get
            {
                if (this._businessRepository == null)
                    this._businessRepository =
                        new GenericRepository<BusinessInfo>(_context);
                return _businessRepository;
            }
        }
        #endregion

        #region Public member methods...
        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        #endregion

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    System.Diagnostics.Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}