using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using DAL;

 
using DAL.Repositories;

namespace DAL.Repositories
{
    public class RepositoryFactory<T> where T : class
    {
        private readonly DatabaseContext _ctx;
        private IRepository<T> _repo;


        public RepositoryFactory(DatabaseContext ctx)
        {
            _ctx = ctx;
        }

        public IRepository<T> GetRepositoryInstance()
        {
            if (_repo != null)
                return _repo;

            #region Просматриваем наличие атрибута Repository, жестко укзывающего на репозитарий

            var customAttribute = (RepositoryAttribute)typeof(T).GetCustomAttribute(typeof(RepositoryAttribute), false);
            if (customAttribute != null)
            {
                var repoType = customAttribute.Type;
                if (repoType.GetInterface("IRepository`1") == null)
                    throw new Exception(repoType.Name + " not implements interface IRepository<T>");
                _repo = (IRepository<T>)Activator.CreateInstance(repoType.MakeGenericType(typeof(T)), _ctx);
                return _repo;
            }

            #endregion

            var types = Assembly.GetExecutingAssembly().GetTypes().ToArray();

            #region Ищем репозитарии непосредственно для типа T

            var typeInfo = types.SingleOrDefault(x => x.Name == typeof(T).Name + "Repository`1");
            if (typeInfo != null)
            {
                var genericType = typeInfo.MakeGenericType(typeof(T));
                _repo = (IRepository<T>)Activator.CreateInstance(genericType, _ctx);
                return _repo;
            }

            #endregion

            #region Смотрим интерфейсы, которые реализует тип T

            var interfaces = typeof(T).GetInterfaces();
            if (interfaces.Any())
            {
                var interfacesRepositories = interfaces.Where(x => types.Any(y => y.Name == x.Name.Substring(1) + "Repository`1")).ToArray();//массив интерфейсов, для которых есть соответствующий репозитарий
                if (interfacesRepositories.Any())
                {
                    var appropriateType = interfacesRepositories.First(x => !interfacesRepositories.Any(y => x.IsAssignableFrom(y) && y != x));//отсеиваем интерфейсы базовых классов; берем первый
                    var repoType = types.First(x => x.Name == appropriateType.Name.Substring(1) + "Repository`1");
                    if (repoType.GetInterface("IRepository`1") == null)
                        throw new Exception(repoType.Name + " not implements interface IRepository<T>");
                    _repo = (IRepository<T>)Activator.CreateInstance(repoType.MakeGenericType(typeof(T)), _ctx);
                    return _repo;
                }
            }

            #endregion

            #region Смотрим родительские классы

            var baseClasses = new List<Type>();
            var curType = typeof(T);
            while (curType != null && curType.IsClass)
            {
                baseClasses.Add(curType);
                curType = curType.BaseType;
            }
            var appropriateClass = baseClasses.FirstOrDefault(
                x => types.Any(y => y.Name == (x.IsAbstract ? x.Name.Substring(1) : x.Name) + "Repository`1"));
            if (appropriateClass != null)
            {
                var repoType = types.First(x => x.Name ==
                        (appropriateClass.IsAbstract ? appropriateClass.Name.Substring(1) : appropriateClass.Name) + "Repository`1");
                if (repoType.GetInterface("IRepository`1") == null)
                    throw new Exception(repoType.Name + " not implements interface IRepository<T>");
                _repo = (IRepository<T>)Activator.CreateInstance(repoType.MakeGenericType(typeof(T)), _ctx);
                return _repo;
            }

            #endregion

            //если ничего не найдено - возвращаем дефолтный репозитарий
            _repo = new EFRepository<T>(_ctx);
            return _repo;
        }
    }
}