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
    public class PersonManager
    {

        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "PersonCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<Person> GetCacheAsList(string rawKey)
        {
            List<Person> list = (List<Person>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static Person GetCacheItem(string rawKey)
        {
            Person item = (Person)HttpRuntime.Cache[GetCacheKey(rawKey)];
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
        
        public static int Insert(Person person)
        {
            int id = RepositoryManager.Person_Repository.Insert(person);
            InvalidateCache();
            return id;
        }

        public static bool Update(Person person)
        {
            bool isExecute = RepositoryManager.Person_Repository.Update(person);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.Person_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static Person GetById(int? id)
        {
            
            string rawKey = "PersonById" + id;
            Person person = GetCacheItem(rawKey);

            if (person == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                person = RepositoryManager.Person_Repository.GetById(id);
                if (person != null)
                    AddCacheItem(rawKey, person);
            }

            return person;
        }

        public static List<Person> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "PersonGetAll";

            List<Person> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Person_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static Person GetByUserId(int User_ID)
        {
            string rawKey = "PersonByUserId" + User_ID;
            Person person = GetCacheItem(rawKey);

            if (person == null)
            {   
                person = RepositoryManager.Person_Repository.GetByUserId(User_ID);
                if (person != null)
                    AddCacheItem(rawKey, person);
            }

            return person;
        }
    }
}
