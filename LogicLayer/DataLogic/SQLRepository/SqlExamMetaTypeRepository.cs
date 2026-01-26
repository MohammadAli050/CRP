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
    public partial class SqlExamMetaTypeRepository : IExamMetaTypeRepository
    {

        Database db = null;

        private string sqlInsert = "ExamMetaTypeInsert";
        private string sqlUpdate = "ExamMetaTypeUpdate";
        private string sqlDelete = "ExamMetaTypeDelete";
        private string sqlGetById = "ExamMetaTypeGetById";
        private string sqlGetAll = "ExamMetaTypeGetAll";
               
        public int Insert(ExamMetaType exammetatype)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, exammetatype, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ExamMetaTypeId");

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

        public bool Update(ExamMetaType exammetatype)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, exammetatype, isInsert);

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

                db.AddInParameter(cmd, "ExamMetaTypeId", DbType.Int32, id);
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

        public ExamMetaType GetById(int? id)
        {
            ExamMetaType _exammetatype = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMetaType> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMetaType>(sqlGetById, rowMapper);
                _exammetatype = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _exammetatype;
            }

            return _exammetatype;
        }

        public List<ExamMetaType> GetAll()
        {
            List<ExamMetaType> exammetatypeList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMetaType> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMetaType>(sqlGetAll, mapper);
                IEnumerable<ExamMetaType> collection = accessor.Execute();

                exammetatypeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return exammetatypeList;
            }

            return exammetatypeList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamMetaType exammetatype, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ExamMetaTypeId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ExamMetaTypeId", DbType.Int32, exammetatype.ExamMetaTypeId);
            }
            	
		    db.AddInParameter(cmd,"ProgramId",DbType.Int32,exammetatype.ProgramId);
            db.AddInParameter(cmd, "ExamMetaTypeName", DbType.String, exammetatype.ExamMetaTypeName);
		    db.AddInParameter(cmd,"Attribute1",DbType.String,exammetatype.Attribute1);
		    db.AddInParameter(cmd,"Attribute2",DbType.String,exammetatype.Attribute2);
		    db.AddInParameter(cmd,"Attribute3",DbType.String,exammetatype.Attribute3);
		    db.AddInParameter(cmd,"CreatedBy",DbType.Int32,exammetatype.CreatedBy);
		    db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,exammetatype.CreatedDate);
		    db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,exammetatype.ModifiedBy);
		    db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,exammetatype.ModifiedDate);
            
            return db;
        }

        private IRowMapper<ExamMetaType> GetMaper()
        {
            IRowMapper<ExamMetaType> mapper = MapBuilder<ExamMetaType>.MapAllProperties()

       	    .Map(m => m.ExamMetaTypeId).ToColumn("ExamMetaTypeId")
		    .Map(m => m.ProgramId).ToColumn("ProgramId")
            .Map(m => m.ExamMetaTypeName).ToColumn("ExamMetaTypeName")
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

