using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.RO;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLEquiCourseRepository : IEquiCourseRepository
    {
        Database db = null;

        private string sqlInsert = "EquiCourseInsert";
        private string sqlUpdate = "EquiCourseUpdate";
        private string sqlDelete = "EquiCourseDeleteById";
        private string sqlGetById = "EquiCourseGetById";
        private string sqlGetAll = "EquiCourseGetAll";


        public int Insert(EquiCourse equiCourse)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, equiCourse, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "EquivalentID");

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

        public bool Update(EquiCourse equiCourse)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, equiCourse, isInsert);

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

                db.AddInParameter(cmd, "EquivalentID", DbType.Int32, id);
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

        public EquiCourse GetById(int? id)
        {
            EquiCourse _equiCourse = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<EquiCourse> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<EquiCourse>(sqlGetById, rowMapper);
                _equiCourse = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _equiCourse;
            }

            return _equiCourse;
        }

        public List<EquiCourse> GetAll()
        {
            List<EquiCourse> equiCourseList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<EquiCourse> mapper = GetMaper();
                var accessor = db.CreateSprocAccessor<EquiCourse>(sqlGetAll, mapper);
                IEnumerable<EquiCourse> collection = accessor.Execute();

                equiCourseList = collection.ToList();
            }

            catch (Exception ex)
            {
                return equiCourseList;
            }

            return equiCourseList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, EquiCourse equiCourse, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "EquivalentID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "EquivalentID", DbType.Int32, equiCourse.EquivalentID);
            }

            db.AddInParameter(cmd, "EquivalentID", DbType.Int32, equiCourse.EquivalentID);
            db.AddInParameter(cmd, "ParentCourseID", DbType.Int32, equiCourse.ParentCourseID);
            db.AddInParameter(cmd, "ParentVersionID", DbType.Int32, equiCourse.ParentVersionID);
            db.AddInParameter(cmd, "EquiCourseID", DbType.Int32, equiCourse.EquiCourseID);
            db.AddInParameter(cmd, "EquiVersionID", DbType.Int32, equiCourse.EquiVersionID);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, equiCourse.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, equiCourse.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, equiCourse.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, equiCourse.ModifiedDate);
           
            return db;
        }

        private IRowMapper<EquiCourse> GetMaper()
        {
            IRowMapper<EquiCourse> mapper = MapBuilder<EquiCourse>.MapAllProperties()
            .Map(m => m.EquivalentID).ToColumn("EquivalentID")
            .Map(m => m.ParentCourseID).ToColumn("ParentCourseID")
            .Map(m => m.ParentVersionID).ToColumn("ParentVersionID")
            .Map(m => m.EquiCourseID).ToColumn("EquiCourseID")
            .Map(m => m.EquiVersionID).ToColumn("EquiVersionID")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Build();

            return mapper;
        }
        #endregion

        public List<rEquivalentCourse> GetAllEquivalentCourseByProgramId(int programId)
        {
            List<rEquivalentCourse> equivalentCourseList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rEquivalentCourse> mapper = GetEquivalentCourseMaper();

                var accessor = db.CreateSprocAccessor<rEquivalentCourse>("EquiCourseGetAllByProgramId", mapper);
                IEnumerable<rEquivalentCourse> collection = accessor.Execute(programId);

                equivalentCourseList = collection.ToList();
            }

            catch (Exception ex)
            {
                return equivalentCourseList;
            }

            return equivalentCourseList;
        }

        private IRowMapper<rEquivalentCourse> GetEquivalentCourseMaper()
        {
            IRowMapper<rEquivalentCourse> mapper = MapBuilder<rEquivalentCourse>.MapAllProperties()
           .Map(m => m.ParentCourseID).ToColumn("ParentCourseID")
           .Map(m => m.MainCourseCode).ToColumn("MainCourseCode")
           .Map(m => m.ActualCourseName).ToColumn("ActualCourseName")
           .Map(m => m.MainCourseCredits).ToColumn("MainCourseCredits")
           .Map(m => m.EquiCourseID).ToColumn("EquiCourseID")
           .Map(m => m.PreCourseCode).ToColumn("PreCourseCode")
           .Map(m => m.PreRequisiteCourseName).ToColumn("PreRequisiteCourseName")
           .Map(m => m.PreCourseCredits).ToColumn("PreCourseCredits")
           .Map(m => m.ProgramName).ToColumn("ProgramName")
           .Map(m => m.DepartmentName).ToColumn("DepartmentName")

           .Build();

            return mapper;
        }
    }
}
