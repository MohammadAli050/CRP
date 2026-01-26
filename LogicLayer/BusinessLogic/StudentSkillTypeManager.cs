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
    public class StudentSkillTypeManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StudentSkillTypeCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StudentSkillType> GetCacheAsList(string rawKey)
        {
            List<StudentSkillType> list = (List<StudentSkillType>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StudentSkillType GetCacheItem(string rawKey)
        {
            StudentSkillType item = (StudentSkillType)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(StudentSkillType studentSkillType)
        {
            int id = RepositoryManager.StudentSkillType_Repository.Insert(studentSkillType);
            InvalidateCache();
            return id;
        }

        public static bool Update(StudentSkillType studentSkillType)
        {
            bool isExecute = RepositoryManager.StudentSkillType_Repository.Update(studentSkillType);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StudentSkillType_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StudentSkillType GetById(int? id)
        {
            string rawKey = "StudentSkillTypeById" + id;
            StudentSkillType studentSkillType = GetCacheItem(rawKey);

            if (studentSkillType == null)
            {
                studentSkillType = RepositoryManager.StudentSkillType_Repository.GetById(id);
                if (studentSkillType != null)
                    AddCacheItem(rawKey, studentSkillType);
            }

            return studentSkillType;
        }

        public static List<StudentSkillType> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "StudentSkillTypeGetAll";

            List<StudentSkillType> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StudentSkillType_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
