﻿using System;
using System.Data.Entity;
using Microsoft.Practices.Unity;
using FPCS.Core.Unity;
using System.Data.Entity.Validation;

namespace FPCS.Data
{
    internal class UnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : DbContext, new()
    {
        #region public constructors

        public UnitOfWork()
        {
            Context = new TDbContext();
        }

        #endregion public constructors

        #region public properties

        internal TDbContext Context { get; private set; }

        #endregion public properties

        #region public methods

        public Int32 Commit()
        {
            //try
            //{
            //    Context.SaveChanges();
            //}
            //catch (DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        string str = $"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:";
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            string str2 = $"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"";
            //        }
            //    }
            //    throw;
            //}
            //return 0;
            return Context.SaveChanges();
        }

        public TRepo GetRepo<TRepo>()
        {
            var unity = UnityManager.Instance.UnityContainer;
            var repo = unity.Resolve<TRepo>(new ParameterOverride("unitOfWork", this));
            return repo;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion public methods

        #region private fields

        private Boolean _isDisposed = false;

        #endregion private fields

        #region private methods

        private void Dispose(Boolean disposing)
        {
            if (!_isDisposed && disposing)
            {
                Context.Dispose();
            }
            _isDisposed = true;
        }

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method 
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~UnitOfWork()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        #endregion private methods
    }
}
