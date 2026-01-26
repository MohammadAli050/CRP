using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.BusinessObjects.RO;

namespace LogicLayer.BusinessLogic
{
    public class BillHistoryManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "BillHistoryCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<BillHistory> GetCacheAsList(string rawKey)
        {
            List<BillHistory> list = (List<BillHistory>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static BillHistory GetCacheItem(string rawKey)
        {
            BillHistory item = (BillHistory)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(BillHistory billhistory)
        {
            int id = RepositoryManager.BillHistory_Repository.Insert(billhistory);
            InvalidateCache();
            return id;
        }

        public static bool Update(BillHistory billhistory)
        {
            bool isExecute = RepositoryManager.BillHistory_Repository.Update(billhistory);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.BillHistory_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static BillHistory GetById(int? id)
        {
            string rawKey = "BillHistoryByID" + id;
            BillHistory billhistory = GetCacheItem(rawKey);

            if (billhistory == null)
            {
                billhistory = RepositoryManager.BillHistory_Repository.GetById(id);
                if (billhistory != null)
                    AddCacheItem(rawKey,billhistory);
            }

            return billhistory;
        }

        public static List<BillHistory> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "BillHistoryGetAll";

            List<BillHistory> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.BillHistory_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<BillHistory> GetForBillPrintByBillHistoryMasterId(int billHistoryMasterId)
        {
            List<BillHistory> list = RepositoryManager.BillHistory_Repository.GetForBillPrintByBillHistoryMasterId(billHistoryMasterId);
            return list;
        }

        public static List<PaymentPostingDTO> GetBillForPaymentPosting(int programId, int sessionId, int batchId, int studentId) 
        {
            List<PaymentPostingDTO> list = RepositoryManager.BillHistory_Repository.GetBillForPaymentPosting(programId, sessionId, batchId, studentId);
            return list;
        }

        public static List<BillHistory> GetByBillHistoryMasterId(int billHistoryMasterId)
        {
            List<BillHistory> list = RepositoryManager.BillHistory_Repository.GetByBillHistoryMasterId(billHistoryMasterId);
            return list;
        }

        public static List<BillPaymentHistoryDTO> GetBillPaymentHistoryByBillHistoryMasterId(int billHistoryMasterId)
        {
            List<BillPaymentHistoryDTO> list = RepositoryManager.BillHistory_Repository.GetBillPaymentHistoryByBillHistoryMasterId(billHistoryMasterId);
            return list;
        }

        public static List<rStudentBillPaymentDue> GetBillPaymentDueByProgramIdBatchIdSessionId(int programId, int batchId, int sessionId)
        {
            List<rStudentBillPaymentDue> list = RepositoryManager.BillHistory_Repository.GetBillPaymentDueByProgramIdBatchIdSessionId(programId, batchId, sessionId);
            return list;
        }

        public static List<BillDeleteDTO> GetBillForDelete(int programId, int batchId, int sessionId, int studentId, DateTime? date)
        {
            List<BillDeleteDTO> billhistoryMaster = RepositoryManager.BillHistory_Repository.GetBillForDelete(programId, batchId, sessionId, studentId, date);
            return billhistoryMaster;
        }

        public static bool DeleteByBillHistoryMasterId(int billHistoryMasterId)
        {
            bool isExecute = RepositoryManager.BillHistory_Repository.DeleteByBillHistoryMasterId(billHistoryMasterId);
            return isExecute;
        }

        public static List<BillPaymentHistoryMasterDTO> GetBillPaymentHistoryMasterByStudentId(int studentId) 
        {
            List<BillPaymentHistoryMasterDTO> list = RepositoryManager.BillHistory_Repository.GetBillPaymentHistoryMasterByStudentId(studentId);
            return list;
        }
    }
}

