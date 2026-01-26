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
    public partial class SQLCourseGroupRepository : ICourseGroupRepository
    {

        Database db = null;

        private string sqlInsert = "CourseGroupInsert";
        private string sqlUpdate = "CourseGroupUpdate";
        private string sqlDelete = "CourseGroupDeleteById";
        private string sqlGetById = "CourseGroupGetById";
        private string sqlGetAll = "CourseGroupGetAll";
               
        public int Insert(CourseGroup coursegroup)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, coursegroup, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "CourseGroupId");

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

        public bool Update(CourseGroup coursegroup)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, coursegroup, isInsert);

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

                db.AddInParameter(cmd, "CourseGroupId", DbType.Int32, id);
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

        public CourseGroup GetById(int? id)
        {
            CourseGroup _coursegroup = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseGroup> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CourseGroup>(sqlGetById, rowMapper);
                _coursegroup = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _coursegroup;
            }

            return _coursegroup;
        }

        public List<CourseGroup> GetAll()
        {
            List<CourseGroup> coursegroupList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseGroup> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CourseGroup>(sqlGetAll, mapper);
                IEnumerable<CourseGroup> collection = accessor.Execute();

                coursegroupList = collection.ToList();
            }

            catch (Exception ex)
            {
                return coursegroupList;
            }

            return coursegroupList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, CourseGroup coursegroup, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "CourseGroupId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "CourseGroupId", DbType.Int32, coursegroup.CourseGroupId);
            }

            	
		db.AddInParameter(cmd,"GroupName",DbType.String,coursegroup.GroupName);
		db.AddInParameter(cmd,"TypeDefinitionId",DbType.Int32,coursegroup.TypeDefinitionId);
		db.AddInParameter(cmd,"Remarks",DbType.String,coursegroup.Remarks);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,coursegroup.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,coursegroup.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,coursegroup.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,coursegroup.ModifiedDate);
            
            return db;
        }

        private IRowMapper<CourseGroup> GetMaper()
        {
            IRowMapper<CourseGroup> mapper = MapBuilder<CourseGroup>.MapAllProperties()

       	   .Map(m => m.CourseGroupId).ToColumn("CourseGroupId")
		.Map(m => m.GroupName).ToColumn("GroupName")
		.Map(m => m.TypeDefinitionId).ToColumn("TypeDefinitionId")
		.Map(m => m.Remarks).ToColumn("Remarks")
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

