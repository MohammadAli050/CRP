using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogicLayer.BusinessLogic
{
    public class RoomInformationManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "RoomInformationCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<RoomInformation> GetCacheAsList(string rawKey)
        {
            List<RoomInformation> list = (List<RoomInformation>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static RoomInformation GetCacheItem(string rawKey)
        {
            RoomInformation item = (RoomInformation)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static RoomInformation GetById(int? id)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            string rawKey = "RoomInformationById" + id;
            RoomInformation roomInformation = GetCacheItem(rawKey);

            if (roomInformation == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                roomInformation = RepositoryManager.RoomInformation_Repository.GetById(id);
                if (roomInformation != null)
                    AddCacheItem(rawKey, roomInformation);
            }

            return roomInformation;
        }

        public static List<RoomInformation> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "RoomInformationGetAll";

            List<RoomInformation> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.RoomInformation_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<RoomInformation> GetAllByBuildingIdCustom(int buildingId, int acaCalId, int examScheduleSetId, int dayId, int timeSlotId)
        {
            return RepositoryManager.RoomInformation_Repository.GetAllByBuildingIdCustom(buildingId, acaCalId, examScheduleSetId, dayId, timeSlotId);
        }

        public static List<RoomList> LoadRoom(int examScheduleSetId, int dayId, int timeSlotId)
        {
            //const string rawKey = "RoomList";

            List<RoomList> list = null;

            //if (list == null)
            //{
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.RoomInformation_Repository.LoadRoom(examScheduleSetId, dayId, timeSlotId);
            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            return list;
        }

        private static List<RoomList> GetCacheAsRoomList(string rawKey)
        {
            List<RoomList> list = (List<RoomList>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static int Insert(RoomInformation roominformation)
        {
            int id = RepositoryManager.RoomInformation_Repository.Insert(roominformation);
            InvalidateCache();
            return id;
        }

        public static bool Update(RoomInformation roominformation)
        {
            bool isExecute = RepositoryManager.RoomInformation_Repository.Update(roominformation);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.RoomInformation_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }
    }
}
