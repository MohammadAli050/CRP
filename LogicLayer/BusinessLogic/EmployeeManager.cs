using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class EmployeeManager
    {

        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "EmployeeCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<Employee> GetCacheAsList(string rawKey)
        {
            List<Employee> list = (List<Employee>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static Employee GetCacheItem(string rawKey)
        {
            Employee item = (Employee)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return item;
        }

        public static void AddCacheItem(string rawKey, object value)
        {
            System.Web.Caching.Cache DataCache = HttpRuntime.Cache;

            // Make sure MasterCacheKeyArray[0] is in the cache - if not, add it
            if (DataCache[MasterCacheKeyArray[0]] == null)
                DataCache[MasterCacheKeyArray[0]] = DateTime.Now;

            // Add a CacheDependency
            System.Web.Caching.CacheDependency dependency = new System.Web.Caching.CacheDependency(null, MasterCacheKeyArray);
            DataCache.Insert(GetCacheKey(rawKey), value, dependency, DateTime.Now.AddMinutes(CacheDuration), System.Web.Caching.Cache.NoSlidingExpiration);
        }



        public static void InvalidateCache()
        {
            // Remove the cache dependency
            HttpRuntime.Cache.Remove(MasterCacheKeyArray[0]);
        }

        #endregion

        public static int Insert(Employee employee)
        {
            int id = RepositoryManager.Employee_Repository.Insert(employee);
            InvalidateCache();
            return id;
        }

        public static bool Update(Employee employee)
        {
            bool isExecute = RepositoryManager.Employee_Repository.Update(employee);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.Employee_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static Employee GetById(int id)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            //string rawKey = "EmployeeById" + id;
            //Employee employee = GetCacheItem(rawKey);

            //if (employee == null)
            //{
            //    // Item not found in cache - retrieve it and insert it into the cache
            //    employee = RepositoryManager.Employee_Repository.GetById(id);
            //    if (employee != null)
            //        AddCacheItem(rawKey, employee);
            //}
            Employee employee = RepositoryManager.Employee_Repository.GetById(id);
            return employee;
        }

        public static Employee GetByPersonId(int id)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            string rawKey = "EmployeeByPersonId" + id;
            Employee employee = GetCacheItem(rawKey);

            if (employee == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                employee = RepositoryManager.Employee_Repository.GetByPersonId(id);
                if (employee != null)
                    AddCacheItem(rawKey, employee);
            }

            return employee;
        }

        public static List<Employee> GetAll()
        {
            List<Employee> list = null;

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Employee_Repository.GetAll();
            }

            return list;
        }

        public static List<Employee> GetAllByCode(string code)
        {
            string rawKey = "EmployeeGetAllByInitial" + code;

            List<Employee> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.Employee_Repository.GetAllByCode(code);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<Employee> GetAllByNameOrCode(string name, string code)
        {
            List<Employee> list = list = RepositoryManager.Employee_Repository.GetAllByNameOrCode(name, code);

            return list;
        }

        public static bool ValidateEmployee(string code)
        {
            bool isValidate = RepositoryManager.Employee_Repository.ValidateEmployee(code);
            return isValidate;
        }

        public static List<rEmployeeInfo> GetEmployeeInfoByDeptId(int deptId)
        {
            List<rEmployeeInfo> list = RepositoryManager.Employee_Repository.GetEmployeeInfoByDeptId(deptId);
            return list;
        }

    }
}
