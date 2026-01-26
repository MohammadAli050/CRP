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
    public partial class SQLStudentSkillTypeRepository : IStudentSkillTypeRepository
    {
        Database db = null;

        private string sqlInsert = "StudentSkillTypeInsert";
        private string sqlUpdate = "StudentSkillTypeUpdate";
        private string sqlDelete = "StudentSkillTypeDeleteById";
        private string sqlGetById = "StudentSkillTypeGetById";
        private string sqlGetAll = "StudentSkillTypeGetAll";
        
        
        public int Insert(StudentSkillType studentSkillType)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, studentSkillType, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "Std_SkillTypeID");

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

        public bool Update(StudentSkillType studentSkillType)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, studentSkillType, isInsert);

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

                db.AddInParameter(cmd, "Std_SkillTypeID", DbType.Int32, id);
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

        public StudentSkillType GetById(int? id)
        {
            StudentSkillType _studentSkillType = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentSkillType> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentSkillType>(sqlGetById, rowMapper);
                _studentSkillType = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentSkillType;
            }

            return _studentSkillType;
        }

        public List<StudentSkillType> GetAll()
        {
            List<StudentSkillType> studentSkillTypeList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentSkillType> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentSkillType>(sqlGetAll, mapper);
                IEnumerable<StudentSkillType> collection = accessor.Execute();

                studentSkillTypeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentSkillTypeList;
            }

            return studentSkillTypeList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StudentSkillType studentSkillType, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Std_SkillTypeID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Std_SkillTypeID", DbType.Int32, studentSkillType.Std_SkillTypeID);
            }

            db.AddInParameter(cmd, "SkillTypeID", DbType.Int32, studentSkillType.SkillTypeID);
            db.AddInParameter(cmd, "StudentID", DbType.Int32, studentSkillType.StudentID);
            db.AddInParameter(cmd, "Description", DbType.String, studentSkillType.Description);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, studentSkillType.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, studentSkillType.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, studentSkillType.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, studentSkillType.ModifiedDate);
            
            return db;
        }

        private IRowMapper<StudentSkillType> GetMaper()
        {
            IRowMapper<StudentSkillType> mapper = MapBuilder<StudentSkillType>.MapAllProperties()
            .Map(m => m.Std_SkillTypeID).ToColumn("Std_SkillTypeID")
            .Map(m => m.SkillTypeID).ToColumn("SkillTypeID")
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.Description).ToColumn("Description")
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
