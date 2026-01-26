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
    public class PersonBlockManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "PersonBlockCache" };
        const double CacheDuration = 2.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<PersonBlock> GetCacheAsList(string rawKey)
        {
            List<PersonBlock> list = (List<PersonBlock>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static PersonBlock GetCacheItem(string rawKey)
        {
            PersonBlock item = (PersonBlock)HttpRuntime.Cache[GetCacheKey(rawKey)];
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
            DataCache.Insert(GetCacheKey(rawKey), value, dependency, DateTime.Now.AddSeconds(CacheDuration), System.Web.Caching.Cache.NoSlidingExpiration);
        }



        public static void InvalidateCache()
        {
            // Remove the cache dependency
            HttpRuntime.Cache.Remove(MasterCacheKeyArray[0]);
        }

        #endregion


        public static int Insert(PersonBlock personblock)
        {
            int id = RepositoryManager.PersonBlock_Repository.Insert(personblock);
            InvalidateCache();
            return id;
        }

        public static bool Update(PersonBlock personblock)
        {
            bool isExecute = RepositoryManager.PersonBlock_Repository.Update(personblock);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.PersonBlock_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static PersonBlock GetById(int id)
        {
            string rawKey = "PersonBlockByID" + id;
            PersonBlock personblock = GetCacheItem(rawKey);

            if (personblock == null)
            {
                personblock = RepositoryManager.PersonBlock_Repository.GetById(id);
                if (personblock != null)
                    AddCacheItem(rawKey, personblock);
            }

            return personblock;
        }

        public static List<PersonBlock> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "PersonBlockGetAll";

            List<PersonBlock> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.PersonBlock_Repository.GetAll();
                if (list != null && list.Count() != 0)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static PersonBlock GetByPersonId(int PersonID)
        {
            string rawKey = "PersonBlockByPersonId" + PersonID;
            PersonBlock personblock = GetCacheItem(rawKey);

            if (personblock == null)
            {
                personblock = RepositoryManager.PersonBlock_Repository.GetByPersonId(PersonID);
                if (personblock != null)
                    AddCacheItem(rawKey, personblock);
            }

            return personblock;
        }

        public static bool PersonIsBlock(int PersonID)
        {
            PersonBlock personblock = GetByPersonId(PersonID);

            if (personblock == null)
                return false;
            else
                return true;
        }

        public static bool DeleteByPerson(int personId)
        {
            bool isExecute = RepositoryManager.PersonBlock_Repository.DeleteByPerson(personId);
            InvalidateCache();
            return isExecute;
        }

        public static List<PersonBlockDTO> GetAllByProgramOrBatchOrRoll(int programId, int batchid, string roll, int dueUptoSession)
        {
            List<PersonBlockDTO> list = RepositoryManager.PersonBlock_Repository.GetAllByProgramOrBatchOrRoll(programId, batchid, roll, dueUptoSession);
            return list;
        }
        public static List<PersonBlockDTO> GetAllByProgramOrBatchOrRoll(int programId, int batchid, string roll,int registrationSession, int dueUptoSession)
        {
            List<PersonBlockDTO> list = RepositoryManager.PersonBlock_Repository.GetAllByProgramOrBatchOrRoll(programId, batchid, roll, registrationSession, dueUptoSession);
            return list;
        }

        public static PersonBlockDTO GetByRoll(string roll)
        {
            PersonBlockDTO person = RepositoryManager.PersonBlock_Repository.GetByRoll(roll);
            return person;
        }
    }
}

