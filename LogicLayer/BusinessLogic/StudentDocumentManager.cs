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
    public class StudentDocumentManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StudentDocumentCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StudentDocument> GetCacheAsList(string rawKey)
        {
            List<StudentDocument> list = (List<StudentDocument>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StudentDocument GetCacheItem(string rawKey)
        {
            StudentDocument item = (StudentDocument)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(StudentDocument studentdocument)
        {
            int id = RepositoryManager.StudentDocument_Repository.Insert(studentdocument);
            InvalidateCache();
            return id;
        }

        public static bool Update(StudentDocument studentdocument)
        {
            bool isExecute = RepositoryManager.StudentDocument_Repository.Update(studentdocument);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StudentDocument_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StudentDocument GetById(int? id)
        {
            string rawKey = "StudentDocumentByID" + id;
            StudentDocument studentdocument = GetCacheItem(rawKey);

            if (studentdocument == null)
            {
                studentdocument = RepositoryManager.StudentDocument_Repository.GetById(id);
                if (studentdocument != null)
                    AddCacheItem(rawKey,studentdocument);
            }

            return studentdocument;
        }

        public static List<StudentDocument> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "StudentDocumentGetAll";

            List<StudentDocument> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StudentDocument_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
        public static StudentDocument GetByPersonIdImageType(int personId, int ImageType)
        {
            StudentDocument studentDocument = RepositoryManager.StudentDocument_Repository.GetByPersonIdImageType(personId, ImageType);
            return studentDocument;
        }
    }
}

