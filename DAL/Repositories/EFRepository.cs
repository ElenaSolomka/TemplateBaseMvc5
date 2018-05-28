using System.Data.Entity;
using System.Reflection;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
 using DAL;


namespace DAL.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext _ctx;
        private readonly DbSet<T> _dbSet;

        internal EFRepository(DatabaseContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<T>();
        }

        public virtual IQueryable<T> All()
        {
            return _dbSet;
        }

        public virtual List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T Find(int? id)
        {
            if (id == null)
                return null;
            return _dbSet.Find(id);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }
        /// <summary>
        /// !!!!Works ONLY with entities, wich key property is value type 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void AddOrUpdate(T entity)
        {
            if (IsToBeAdded(entity))
                _dbSet.Add(entity);
            else
                _ctx.Entry(entity).State = EntityState.Modified;
        }

        public virtual void AddOrUpdate(T entity, Action<T> actionBeforeAdd, Action<T> actionBeforeEdit)
        {
            if (IsToBeAdded(entity))
            {
                if (actionBeforeAdd != null)
                    actionBeforeAdd(entity);
                _dbSet.Add(entity);
            }
            else
            {
                if (actionBeforeEdit != null)
                    actionBeforeEdit(entity);
                _ctx.Entry(entity).State = EntityState.Modified;
            }
        }

        protected virtual bool IsToBeAdded(T entity)
        {
            var type = entity.GetType();
            var objectContext = ((IObjectContextAdapter)_ctx).ObjectContext;
            var set = objectContext.CreateObjectSet<T>();
            var keyPropName = set.EntitySet.ElementType.KeyMembers.Select(k => k.Name).First();
            var keyPropType = type.GetProperty(keyPropName);
            if (!keyPropType.PropertyType.IsValueType)
            {
                throw new Exception("Key property of model is not value type");
            }
            return Equals(Activator.CreateInstance(keyPropType.PropertyType), keyPropType.GetValue(entity));
        }

        public virtual T FirstOrDefault()
        {
            return _dbSet.FirstOrDefault();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            if (_ctx.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public virtual void DeleteRange(IEnumerable<T> entity)
        {
            foreach (var i in entity)
            {
                _ctx.Entry(i).State = EntityState.Deleted;

            }
        }
    }
}