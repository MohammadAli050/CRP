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
    public partial class SQLOpeningDueRepository : IOpeningDueRepository
    {

        Database db = null;

        private string sqlInsert = "OpeningDueInsert";
        private string sqlUpdate = "OpeningDueUpdate";
        private string sqlDelete = "OpeningDueDeleteId";
        private string sqlGetById = "OpeningDueGetById";
        private string sqlGetAll = "OpeningDueGetAll";
        private string sqlGetByStudentId = "OpeningDueGetByStudentId";
               
        public int Insert(OpeningDue openingdue)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, openingdue, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "OpeningDueId");

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

        public bool Update(OpeningDue openingdue)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, openingdue, isInsert);

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

                db.AddInParameter(cmd, "OpeningDueId", DbType.Int32, id);
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

        public OpeningDue GetById(int? id)
        {
            OpeningDue _openingdue = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<OpeningDue> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<OpeningDue>(sqlGetById, rowMapper);
                _openingdue = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _openingdue;
            }

            return _openingdue;
        }

        public List<OpeningDue> GetAll()
        {
            List<OpeningDue> openingdueList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<OpeningDue> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<OpeningDue>(sqlGetAll, mapper);
                IEnumerable<OpeningDue> collection = accessor.Execute();

                openingdueList = collection.ToList();
            }

            catch (Exception ex)
            {
                return openingdueList;
            }

            return openingdueList;
        }
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, OpeningDue openingdue, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "OpeningDueId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "OpeningDueId", DbType.Int32, openingdue.OpeningDueId);
            }

            	
		db.AddInParameter(cmd,"StudentId",DbType.Int32,openingdue.StudentId);
		db.AddInParameter(cmd,"DueAmount",DbType.Decimal,openingdue.DueAmount);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,openingdue.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,openingdue.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,openingdue.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,openingdue.ModifiedDate);
            
            return db;
        }

        private IRowMapper<OpeningDue> GetMaper()
        {
            IRowMapper<OpeningDue> mapper = MapBuilder<OpeningDue>.MapAllProperties()

       	   .Map(m => m.OpeningDueId).ToColumn("OpeningDueId")
		.Map(m => m.StudentId).ToColumn("StudentId")
		.Map(m => m.DueAmount).ToColumn("DueAmount")
		.Map(m => m.CreatedBy).ToColumn("CreatedBy")
		.Map(m => m.CreatedDate).ToColumn("CreatedDate")
		.Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
		.Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            
            .Build();

            return mapper;
        }
        #endregion

        public OpeningDue GetByStudentId(int studentID)
        {
            OpeningDue _openingdue = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<OpeningDue> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<OpeningDue>(sqlGetByStudentId, rowMapper);
                _openingdue = accessor.Execute(studentID).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _openingdue;
            }

            return _openingdue;
        }

    }
}

