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
    public class RoomTypeManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "RoomTypeCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<RoomType> GetCacheAsList(string rawKey)
        {
            List<RoomType> list = (List<RoomType>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static RoomType GetCacheItem(string rawKey)
        {
            RoomType item = (RoomType)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(RoomType roomType)
        {
            int id = RepositoryManager.RoomType_Repository.Insert(roomType);
            InvalidateCache();
            return id;
        }

        public static bool Update(RoomType roomType)
        {
            bool isExecute = RepositoryManager.RoomType_Repository.Update(roomType);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.RoomType_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static RoomType GetById(int? id)
        {
            string rawKey = "RoomTypeById" + id;
            RoomType roomType = GetCacheItem(rawKey);

            if (roomType == null)
            {
                roomType = RepositoryManager.RoomType_Repository.GetById(id);
                if (roomType != null)
                    AddCacheItem(rawKey, roomType);
            }

            return roomType;
        }

        public static List<RoomType> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "RoomTypeGetAll";

            List<RoomType> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.RoomType_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
