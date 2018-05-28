
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
  
    public interface IRepository<T> where T : class
    {
        /** @fn T FirstOrDefault()
         * Функция, которая возвращает первый элемент типа T из репозитория
         * @return T объект данных
         */
        T FirstOrDefault();

        /** @fn void Add(T entity)
         * Функция Add добавляет данные в репозиторий
         * @param[in] entity - объект данных
         */
        void Add(T entity);

        /** @fn void Update(T entity)
         * Функция Update обновляет данные в репозитории
         * @param[in] entity - объект данных
         */
        void Update(T entity);

        /** @fn void Delete(T entity)
         * Функция Delete удаляет данные из репозитория
         * @param[in] entity - объект данных
         */
        void Delete(T entity);

        /** @fn List<T> GetAll()
         * Функция GetAll получает список данных из репозитория
         * \return List<T> - список объектов данных
         */
        List<T> GetAll();

        /** @fn IQueryable<T> All()
         * Функция All генерирует запрос для получения данных из репозитория
         * \return IQueryable<T> - запрос для получения объектов данных
         */
        IQueryable<T> All();

        /** @fn T Find(int? id)
         * Функция Find возвращает искомый объект данных из репозитория
         * \param id - идентификатор объекта данных
         * \return T - найденный объект данных
         */
        T Find(int? id);

        /** @fn AddRange(IEnumerable<T> entities)
         * Функция AddRange добавляет несколько объектов данных в репозиторий
         * @param[in] entities - объекты данных
         */
        void AddRange(IEnumerable<T> entities);

        /** @fn AddOrUpdate(T entity)
         * Функция AddOrUpdate добавляет/ объектов данных в репозиторий
         * @param[in] entities - объекты данных
         */
        void AddOrUpdate(T entity);
        void DeleteRange(IEnumerable<T> entity);
    }
}