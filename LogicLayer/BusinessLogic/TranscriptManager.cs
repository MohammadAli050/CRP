using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using LogicLayer.BusinessLogic;

namespace LogicLayer.BusinessLogic
{
    public class TranscriptManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "TranscriptCache" };
        const double CacheDuration = 1.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<TranscriptResultDetails> GetCacheAsList(string rawKey)
        {
            List<TranscriptResultDetails> list = (List<TranscriptResultDetails>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static TranscriptResultDetails GetCacheItem(string rawKey)
        {
            TranscriptResultDetails item = (TranscriptResultDetails)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return item;
        }

        public static List<TranscriptStudentInfo> GetCacheInfoList(string rawKey)
        {
            List<TranscriptStudentInfo> list = (List<TranscriptStudentInfo>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static List<TranscriptTransferDetails> GetCacheTranferList(string rawKey)
        {
            List<TranscriptTransferDetails> list = (List<TranscriptTransferDetails>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static void AddCacheItem(string rawKey, object value)
        {
            System.Web.Caching.Cache DataCache = HttpRuntime.Cache;

            // Make sure MasterCacheKeyArray[0] is in the cache - if not, add it
            if (DataCache[MasterCacheKeyArray[0]] == null)
                DataCache[MasterCacheKeyArray[0]] = DateTime.Now;

            // Add a CacheDependency
            System.Web.Caching.CacheDependency dependency = new System.Web.Caching.CacheDependency(null, MasterCacheKeyArray);
            DataCache.Insert(GetCacheKey(rawKey), value, dependency, DateTime.Now.AddSeconds(CacheDuration), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public static void InvalidateCache()
        {
            // Remove the cache dependency
            HttpRuntime.Cache.Remove(MasterCacheKeyArray[0]);
        }

        #endregion

        public static List<TranscriptResultDetails> GetResultByStudentId(string roll)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "GetTranscriptResult"+roll;

            List<TranscriptResultDetails> transcript = GetCacheAsList(rawKey);

            if (transcript == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                transcript = RepositoryManager.Transcript_Repository.GetResultByStudentId(roll);
                
                    if (transcript != null)
                    AddCacheItem(rawKey, transcript);
            }

            return transcript;
        }
        public static List<TranscriptStudentInfo> GetInfoByStudentId(string roll)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "GetTranscriptInfo"+roll;

            List<TranscriptStudentInfo> transcript = GetCacheInfoList(rawKey);

            if (transcript == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                transcript = RepositoryManager.Transcript_Repository.GetInfoByStudentId(roll);

                if (transcript != null)
                    AddCacheItem(rawKey, transcript);
            }

            return transcript;
        }

        public static List<TranscriptTransferDetails> GetTransferResultStudentId(string id)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "GetTranscriptTransferInfo" + id;

            List<TranscriptTransferDetails> transcript = GetCacheTranferList(rawKey);

            if (transcript == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                transcript = RepositoryManager.Transcript_Repository.GetTransferResultStudentId(id);

                if (transcript != null)
                    AddCacheItem(rawKey, transcript);
            }

            return transcript;
        }
        public static List<TranscriptTransferDetails> GetWaiverResultStudentId(string id)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "GetTranscriptWaiverInfo" + id;

            List<TranscriptTransferDetails> transcript = GetCacheTranferList(rawKey);

            if (transcript == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                transcript = RepositoryManager.Transcript_Repository.GetWaiverResultStudentId(id);

                if (transcript != null)
                    AddCacheItem(rawKey, transcript);
            }

            return transcript;
        }
    }

    
}
