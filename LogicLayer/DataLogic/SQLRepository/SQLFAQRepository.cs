using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLFAQRepository : IFAQRepository
    {

        Database db = null;

        private string sqlInsert = "FAQInsert";
        private string sqlUpdate = "FAQUpdate";
        private string sqlDelete = "FAQDelete";
        private string sqlGetById = "FAQGetById";
        private string sqlGetAll = "FAQGetAll";
               
        public int Insert(FAQ faq)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, faq, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "FaqID");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out id);
                }
            }
            catch (Exception ex)
            {
                id = 0;
            }

            return id;
        }

        public bool Update(FAQ faq)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, faq, isInsert);

                int rowsAffected = db.ExecuteNonQuery(cmd);

                if (rowsAffected > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public bool Delete(int id)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlDelete);

                db.AddInParameter(cmd, "FaqID", DbType.Int32, id);
                int rowsAffected = db.ExecuteNonQuery(cmd);

                if (rowsAffected > 0)
                {
                    result = true;
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public FAQ GetById(int? id)
        {
            FAQ _faq = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<FAQ> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<FAQ>(sqlGetById, rowMapper);
                _faq = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _faq;
            }

            return _faq;
        }

        public List<FAQ> GetAll()
        {
            List<FAQ> faqList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<FAQ> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<FAQ>(sqlGetAll, mapper);
                IEnumerable<FAQ> collection = accessor.Execute();

                faqList = collection.ToList();
            }

            catch (Exception ex)
            {
                return faqList;
            }

            return faqList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, FAQ faq, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "FaqID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "FaqID", DbType.Int32, faq.FaqID);
            }

            	db.AddInParameter(cmd,"Tag",DbType.String,faq.Tag);
		db.AddInParameter(cmd,"Key",DbType.String,faq.Key);
		db.AddInParameter(cmd,"Question",DbType.String,faq.Question);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,faq.CreatedBy);
		db.AddInParameter(cmd,"CreatedOn",DbType.DateTime,faq.CreatedOn);
            
            return db;
        }

        private IRowMapper<FAQ> GetMaper()
        {
            IRowMapper<FAQ> mapper = MapBuilder<FAQ>.MapAllProperties()

       	   .Map(m => m.FaqID).ToColumn("FaqID")
		.Map(m => m.Tag).ToColumn("Tag")
		.Map(m => m.Key).ToColumn("Key")
		.Map(m => m.Question).ToColumn("Question")
		.Map(m => m.CreatedBy).ToColumn("CreatedBy")
		.Map(m => m.CreatedOn).ToColumn("CreatedOn")
            
            .Build();

            return mapper;
        }
        #endregion

    }
}

