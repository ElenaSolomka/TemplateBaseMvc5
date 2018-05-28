
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
  
    public interface IRepository<T> where T : class
    {
        /** @fn T FirstOrDefault()
         * �������, ������� ���������� ������ ������� ���� T �� �����������
         * @return T ������ ������
         */
        T FirstOrDefault();

        /** @fn void Add(T entity)
         * ������� Add ��������� ������ � �����������
         * @param[in] entity - ������ ������
         */
        void Add(T entity);

        /** @fn void Update(T entity)
         * ������� Update ��������� ������ � �����������
         * @param[in] entity - ������ ������
         */
        void Update(T entity);

        /** @fn void Delete(T entity)
         * ������� Delete ������� ������ �� �����������
         * @param[in] entity - ������ ������
         */
        void Delete(T entity);

        /** @fn List<T> GetAll()
         * ������� GetAll �������� ������ ������ �� �����������
         * \return List<T> - ������ �������� ������
         */
        List<T> GetAll();

        /** @fn IQueryable<T> All()
         * ������� All ���������� ������ ��� ��������� ������ �� �����������
         * \return IQueryable<T> - ������ ��� ��������� �������� ������
         */
        IQueryable<T> All();

        /** @fn T Find(int? id)
         * ������� Find ���������� ������� ������ ������ �� �����������
         * \param id - ������������� ������� ������
         * \return T - ��������� ������ ������
         */
        T Find(int? id);

        /** @fn AddRange(IEnumerable<T> entities)
         * ������� AddRange ��������� ��������� �������� ������ � �����������
         * @param[in] entities - ������� ������
         */
        void AddRange(IEnumerable<T> entities);

        /** @fn AddOrUpdate(T entity)
         * ������� AddOrUpdate ���������/ �������� ������ � �����������
         * @param[in] entities - ������� ������
         */
        void AddOrUpdate(T entity);
        void DeleteRange(IEnumerable<T> entity);
    }
}