using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using LogicLayer.BusinessObjects.DTO;

namespace LogicLayer.BusinessLogic
{
    public class BillHistoryMasterManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "BillHistoryMasterCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<BillHistoryMaster> GetCacheAsList(string rawKey)
        {
            List<BillHistoryMaster> list = (List<BillHistoryMaster>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static BillHistoryMaster GetCacheItem(string rawKey)
        {
            BillHistoryMaster item = (BillHistoryMaster)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(BillHistoryMaster billhistorymaster)
        {
            int id = RepositoryManager.BillHistoryMaster_Repository.Insert(billhistorymaster);
            InvalidateCache();
            return id;
        }

        public static bool Update(BillHistoryMaster billhistorymaster)
        {
            bool isExecute = RepositoryManager.BillHistoryMaster_Repository.Update(billhistorymaster);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.BillHistoryMaster_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static BillHistoryMaster GetById(int? id)
        {
            string rawKey = "BillHistoryMasterByID" + id;
            BillHistoryMaster billhistorymaster = GetCacheItem(rawKey);

            if (billhistorymaster == null)
            {
                billhistorymaster = RepositoryManager.BillHistoryMaster_Repository.GetById(id);
                if (billhistorymaster != null)
                    AddCacheItem(rawKey,billhistorymaster);
            }

            return billhistorymaster;
        }

        public static List<BillHistoryMaster> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "BillHistoryMasterGetAll";

            List<BillHistoryMaster> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.BillHistoryMaster_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static string GetBillMasterMaxReferenceNo(DateTime date)
        {
            string referenceNo = RepositoryManager.BillHistoryMaster_Repository.GetBillMasterMaxReferenceNo(date);
            return referenceNo;
        }

        public static bool IsDuplicateBill(int studentId, int acaCalId, decimal fees, bool isDue)
        {
            BillHistoryMaster billHistoryMasterObj = GetByStudentIdAcaCalIdFees(studentId, acaCalId, fees, isDue);
            if (billHistoryMasterObj == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static BillHistoryMaster GetByStudentIdAcaCalIdFees(int studentId, int acaCalId, decimal fees, bool isDue)
        {
            BillHistoryMaster billhistoryMaster = RepositoryManager.BillHistoryMaster_Repository.GetByStudentIdAcaCalIdFees(studentId, acaCalId, fees, isDue);
            return billhistoryMaster;
        }

        public static int CheckBillMasterDueBillCount(int studentId, int acaCalId)
        {
            List<BillHistoryMaster> billHistoryMasterList = new List<BillHistoryMaster>();
            billHistoryMasterList = BillHistoryMasterManager.GetBillDueCountByStudentIdAcaCalId(studentId, acaCalId);
            int count = 0;
            if (billHistoryMasterList != null)
            {
                count = billHistoryMasterList.Count;
            }
            return count;
        }

        public static List<BillHistoryMaster> GetBillDueCountByStudentIdAcaCalId(int studentId, int acaCalId)
        {
            List<BillHistoryMaster> billhistoryMaster = RepositoryManager.BillHistoryMaster_Repository.GetBillDueCountByStudentIdAcaCalId(studentId, acaCalId);
            return billhistoryMaster;
        }

    }
}

