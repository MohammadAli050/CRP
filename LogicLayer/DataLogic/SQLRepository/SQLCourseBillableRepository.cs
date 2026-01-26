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
    public partial class SQLCourseBillableRepository : ICourseBillableRepository
    {
        Database db = null;

        private string sqlInsert = "IsCourseBillableInsert";
        private string sqlUpdate = "IsCourseBillableUpdate";
        private string sqlDelete = "IsCourseBillableDeleteById";
        private string sqlGetById = "IsCourseBillableGetById";
        private string sqlGetAll = "IsCourseBillableGetAll";
        
        
        public int Insert(CourseBillable courseBillable)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, courseBillable, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "BillableCourseID");

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

        public bool Update(CourseBillable courseBillable)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, courseBillable, isInsert);

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

                db.AddInParameter(cmd, "BillableCourseID", DbType.Int32, id);
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

        public CourseBillable GetById(int? id)
        {
            CourseBillable _courseBillable = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseBillable> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CourseBillable>(sqlGetById, rowMapper);
                _courseBillable = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _courseBillable;
            }

            return _courseBillable;
        }

        public List<CourseBillable> GetAll()
        {
            List<CourseBillable> courseBillableList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseBillable> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CourseBillable>(sqlGetAll, mapper);
                IEnumerable<CourseBillable> collection = accessor.Execute();

                courseBillableList = collection.ToList();
            }

            catch (Exception ex)
            {
                return courseBillableList;
            }

            return courseBillableList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, CourseBillable courseBillable, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "BillableCourseID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "BillableCourseID", DbType.Int32, courseBillable.BillableCourseID);
            }

            db.AddInParameter(cmd, "AcaCalID", DbType.Int32, courseBillable.AcaCalID);
            db.AddInParameter(cmd, "ProgramID", DbType.Int32, courseBillable.ProgramID);
            db.AddInParameter(cmd, "BillStartFromRetakeNo", DbType.Int32, courseBillable.BillStartFromRetakeNo);
            db.AddInParameter(cmd, "IsCreditCourse", DbType.Boolean, courseBillable.IsCreditCourse);
            db.AddInParameter(cmd, "CourseID", DbType.Int32, courseBillable.CourseID);
            db.AddInParameter(cmd, "VersionID", DbType.Int32, courseBillable.VersionID);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, courseBillable.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, courseBillable.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, courseBillable.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, courseBillable.ModifiedDate);
            
            return db;
        }

        private IRowMapper<CourseBillable> GetMaper()
        {
            IRowMapper<CourseBillable> mapper = MapBuilder<CourseBillable>.MapAllProperties()
            .Map(m => m.BillableCourseID).ToColumn("BillableCourseID")
            .Map(m => m.AcaCalID).ToColumn("AcaCalID")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.BillStartFromRetakeNo).ToColumn("BillStartFromRetakeNo")
            .Map(m => m.IsCreditCourse).ToColumn("IsCreditCourse")
            .Map(m => m.CourseID).ToColumn("CourseID")
            .Map(m => m.VersionID).ToColumn("VersionID")
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
