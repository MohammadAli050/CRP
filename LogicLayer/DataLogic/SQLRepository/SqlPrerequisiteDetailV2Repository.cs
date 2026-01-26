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
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.BusinessObjects.RO;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SqlPrerequisiteDetailV2Repository : IPrerequisiteDetailV2Repository
    {

        Database db = null;

        private string sqlInsert = "PrerequisiteDetailV2Insert";//
        private string sqlUpdate = "PrerequisiteDetailV2Update";//
        private string sqlDelete = "PrerequisiteDetailV2DeleteById";//
        private string sqlGetById = "PrerequisiteDetailV2GetById";//
        private string sqlGetAll = "PrerequisiteDetailV2GetAll";//
        private string sqlGetAllPreRequisiteSetAndCourses = "PreRequisiteGetAllSetAndCourses";//
        private string sqlGetAllPreRequisiteDetailCourse = "PrerequisiteDetailV2GetByMasterId";//
        private string sqlGetAllPreRequisiteCourseByProgramId = "RptPreRequisiteCourseList";//
              
        public int Insert(PrerequisiteDetailV2 prerequisitedetailv2)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, prerequisitedetailv2, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "PreRequisiteDetailId");

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

        public bool Update(PrerequisiteDetailV2 prerequisitedetailv2)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, prerequisitedetailv2, isInsert);

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

                db.AddInParameter(cmd, "PreRequisiteDetailId", DbType.Int32, id);
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

        public PrerequisiteDetailV2 GetById(int? id)
        {
            PrerequisiteDetailV2 _prerequisitedetailv2 = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PrerequisiteDetailV2> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<PrerequisiteDetailV2>(sqlGetById, rowMapper);
                _prerequisitedetailv2 = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _prerequisitedetailv2;
            }

            return _prerequisitedetailv2;
        }

        public List<PrerequisiteDetailV2> GetAll()
        {
            List<PrerequisiteDetailV2> prerequisitedetailv2List= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PrerequisiteDetailV2> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<PrerequisiteDetailV2>(sqlGetAll, mapper);
                IEnumerable<PrerequisiteDetailV2> collection = accessor.Execute();

                prerequisitedetailv2List = collection.ToList();
            }

            catch (Exception ex)
            {
                return prerequisitedetailv2List;
            }

            return prerequisitedetailv2List;
        }

        public List<PreRequisiteSetDTO> GetAllPreRequisiteSetAndCourses(int programId, int courseId, int versionId, string versionCode )
        {
            List<PreRequisiteSetDTO> prerequisitedetailv2List = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PreRequisiteSetDTO> mapper = GetPreRequisiteSetDTOMaper();

                var accessor = db.CreateSprocAccessor<PreRequisiteSetDTO>(sqlGetAllPreRequisiteSetAndCourses, mapper);
                IEnumerable<PreRequisiteSetDTO> collection = accessor.Execute(programId, courseId, versionId, versionCode);

                prerequisitedetailv2List = collection.ToList();
            }

            catch (Exception ex)
            {
                return prerequisitedetailv2List;
            }

            return prerequisitedetailv2List;
        }

        private IRowMapper<PreRequisiteSetDTO> GetPreRequisiteSetDTOMaper()
        {
            IRowMapper<PreRequisiteSetDTO> mapper = MapBuilder<PreRequisiteSetDTO>.MapAllProperties()

       	    .Map(m => m.PreRequisiteDetailId).ToColumn("PreRequisiteDetailId")
		    .Map(m => m.PreRequisiteMasterId).ToColumn("PreRequisiteMasterId")
		    .Map(m => m.ProgramId).ToColumn("ProgramId")
		    .Map(m => m.NodeId).ToColumn("NodeId")
		    .Map(m => m.CourseId).ToColumn("CourseId")
		    .Map(m => m.VersionId).ToColumn("VersionId")
            .Map(m => m.NodeName).ToColumn("NodeName")
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.VersionCode).ToColumn("VersionCode")
            .Map(m => m.Title).ToColumn("Title")
            .Map(m => m.ProgramName).ToColumn("ProgramName")
            .Map(m => m.Credits).ToColumn("Credits")
            .Build();

            return mapper;
        }

        public List<PreRequisiteSetDTO> GetAllPreRequisiteDetailCourses(int preRequisiteMasterId)
        {
            List<PreRequisiteSetDTO> prerequisitedetailv2List = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PreRequisiteSetDTO> mapper = GetPreRequisiteSetDTOMaper();

                var accessor = db.CreateSprocAccessor<PreRequisiteSetDTO>(sqlGetAllPreRequisiteDetailCourse, mapper);
                IEnumerable<PreRequisiteSetDTO> collection = accessor.Execute(preRequisiteMasterId);

                prerequisitedetailv2List = collection.ToList();
            }

            catch (Exception ex)
            {
                return prerequisitedetailv2List;
            }

            return prerequisitedetailv2List;
        }

        public List<rPreRequisiteCourse> GetAllPreRequisiteCoursesProgramWise(int programID)
        {
            List<rPreRequisiteCourse> preRequisiteCoursesList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rPreRequisiteCourse> mapper = GetPreRequisiteCourseMaper();

                var accessor = db.CreateSprocAccessor<rPreRequisiteCourse>(sqlGetAllPreRequisiteCourseByProgramId, mapper);
                IEnumerable<rPreRequisiteCourse> collection = accessor.Execute(programID);

                preRequisiteCoursesList = collection.ToList();
            }

            catch (Exception ex)
            {
                return preRequisiteCoursesList;
            }

            return preRequisiteCoursesList;
        }
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, PrerequisiteDetailV2 prerequisitedetailv2, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "PreRequisiteDetailId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "PreRequisiteDetailId", DbType.Int32, prerequisitedetailv2.PreRequisiteDetailId);
            }

            	
		db.AddInParameter(cmd,"PreRequisiteMasterId",DbType.Int32,prerequisitedetailv2.PreRequisiteMasterId);
		db.AddInParameter(cmd,"ProgramId",DbType.Int32,prerequisitedetailv2.ProgramId);
		db.AddInParameter(cmd,"NodeId",DbType.Int32,prerequisitedetailv2.NodeId);
		db.AddInParameter(cmd,"CourseId",DbType.Int32,prerequisitedetailv2.CourseId);
		db.AddInParameter(cmd,"VersionId",DbType.Int32,prerequisitedetailv2.VersionId);
		db.AddInParameter(cmd,"Attribute1",DbType.String,prerequisitedetailv2.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,prerequisitedetailv2.Attribute2);
		db.AddInParameter(cmd,"Attribute3",DbType.String,prerequisitedetailv2.Attribute3);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,prerequisitedetailv2.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,prerequisitedetailv2.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,prerequisitedetailv2.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,prerequisitedetailv2.ModifiedDate);
            
            return db;
        }

        private IRowMapper<PrerequisiteDetailV2> GetMaper()
        {
            IRowMapper<PrerequisiteDetailV2> mapper = MapBuilder<PrerequisiteDetailV2>.MapAllProperties()

       	    .Map(m => m.PreRequisiteDetailId).ToColumn("PreRequisiteDetailId")
		    .Map(m => m.PreRequisiteMasterId).ToColumn("PreRequisiteMasterId")
		    .Map(m => m.ProgramId).ToColumn("ProgramId")
		    .Map(m => m.NodeId).ToColumn("NodeId")
		    .Map(m => m.CourseId).ToColumn("CourseId")
		    .Map(m => m.VersionId).ToColumn("VersionId")
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

        private IRowMapper<rPreRequisiteCourse> GetPreRequisiteCourseMaper()
        {
            IRowMapper<rPreRequisiteCourse> mapper = MapBuilder<rPreRequisiteCourse>.MapAllProperties()

            .Map(m => m.MainCourse).ToColumn("MainCourse")
            .Map(m => m.PreRequisiteCourse).ToColumn("PreRequisiteCourse")
            .Map(m => m.GroupCount).ToColumn("GroupCount")

            .Build();

            return mapper;
        }
        #endregion

    }
}

