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
    public partial class SqlStudentSessionRepository : IStudentSessionRepository
    {

        Database db = null;

        private string sqlInsert = "StudentSessionInsert";
        private string sqlUpdate = "StudentSessionUpdate";
        private string sqlDelete = "StudentSessionDelete";
        private string sqlGetById = "StudentSessionGetById";
        private string sqlGetAll = "StudentSessionGetAll";
               
        public int Insert(StudentSession studentsession)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, studentsession, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "Id");

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

        public bool Update(StudentSession studentsession)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, studentsession, isInsert);

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

                db.AddInParameter(cmd, "Id", DbType.Int32, id);
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

        public StudentSession GetById(int? id)
        {
            StudentSession _studentsession = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentSession> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentSession>(sqlGetById, rowMapper);
                _studentsession = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentsession;
            }

            return _studentsession;
        }

        public List<StudentSession> GetAll()
        {
            List<StudentSession> studentsessionList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentSession> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentSession>(sqlGetAll, mapper);
                IEnumerable<StudentSession> collection = accessor.Execute();

                studentsessionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentsessionList;
            }

            return studentsessionList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StudentSession studentsession, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, studentsession.Id);
            }

            	
		db.AddInParameter(cmd,"Session",DbType.String,studentsession.Session);
		db.AddInParameter(cmd,"SequenceNo",DbType.Int32,studentsession.SequenceNo);
		db.AddInParameter(cmd,"Attribute1",DbType.String,studentsession.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,studentsession.Attribute2);
		db.AddInParameter(cmd,"Attribute3",DbType.String,studentsession.Attribute3);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,studentsession.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,studentsession.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,studentsession.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,studentsession.ModifiedDate);
            
            return db;
        }

        private IRowMapper<StudentSession> GetMaper()
        {
            IRowMapper<StudentSession> mapper = MapBuilder<StudentSession>.MapAllProperties()

       	   .Map(m => m.Id).ToColumn("Id")
		.Map(m => m.Session).ToColumn("Session")
		.Map(m => m.SequenceNo).ToColumn("SequenceNo")
		.Map(m => m.Attribute1).ToColumn("Attribute1")
		.Map(m => m.Attribute2).ToColumn("Attribute2")
		.Map(m => m.Attribute3).ToColumn("Attribute3")
		.Map(m => m.CreatedBy).ToColumn("CreatedBy")
		.Map(m => m.CreatedDate).ToColumn("CreatedDate")
		.Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
		.Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            
            .Build();

            return mapper;
        }
        #endregion

    }
}

