using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using System.Web;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class PersonPreviousExamManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "PersonPreviousExamManagerCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<PersonPreviousExam> GetCacheAsList(string rawKey)
        {
            List<PersonPreviousExam> list = (List<PersonPreviousExam>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static PersonPreviousExam GetCacheItem(string rawKey)
        {
            PersonPreviousExam item = (PersonPreviousExam)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(PersonPreviousExam exam)
        {
            int id = RepositoryManager.PersonPreviousExam_Repository.Insert(exam);
            InvalidateCache();
            return id;
        }

        public static bool Update(PersonPreviousExam exam)
        {
            bool isExecute = RepositoryManager.PersonPreviousExam_Repository.Update(exam);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.PersonPreviousExam_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static PersonPreviousExam GetById(int id)
        {
            string rawKey = "PersonPreviousExam" + id;
            PersonPreviousExam exam = GetCacheItem(rawKey);

            if (exam == null)
            {
                exam = RepositoryManager.PersonPreviousExam_Repository.GetById(id);
                if (exam != null)
                    AddCacheItem(rawKey, exam);
            }

            return exam;
        }

        public static List<PersonPreviousExam> GetAll()
        {
            List<PersonPreviousExam> list = RepositoryManager.PersonPreviousExam_Repository.GetAll();
            return list;
        }

        public static List<PersonPreviousExam> GetAllByPersonId(int id)
        {
            List<PersonPreviousExam> list = RepositoryManager.PersonPreviousExam_Repository.GetAllByPersonId(id);
            return list;
        }

    }
}
