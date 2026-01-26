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

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SqlEquiCourseDetailsRepository : IEquiCourseDetailsRepository
    {

        Database db = null;

        private string sqlInsert = "EquiCourseDetailsInsert";//
        private string sqlUpdate = "EquiCourseDetailsUpdate";//
        private string sqlDelete = "EquiCourseDetailsDeleteById";//
        private string sqlGetById = "EquiCourseDetailsGetById";//
        private string sqlGetAll = "EquiCourseDetailsGetAll";//
        private string sqlGetEquivalentCourseAll = "EquivalentCourseGetAll";//
        private string sqlGetEquiCourseMasterId = "EquivalentCourseGetByEquiCourseMasterId";//
        private string sqlGetEquiCourseByParameters = "EquiCourseDetailsByParameters";//
        private string sqlGetEquiCourseForRpt = "RptEquivalentCourses";//
               
        public int Insert(EquiCourseDetails equicoursedetails)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, equicoursedetails, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "EquiCourseDetailId");

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

        public bool Update(EquiCourseDetails equicoursedetails)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, equicoursedetails, isInsert);

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

                db.AddInParameter(cmd, "EquiCourseDetailId", DbType.Int32, id);
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

        public EquiCourseDetails GetById(int? id)
        {
            EquiCourseDetails _equicoursedetails = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<EquiCourseDetails> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<EquiCourseDetails>(sqlGetById, rowMapper);
                _equicoursedetails = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _equicoursedetails;
            }

            return _equicoursedetails;
        }

        public List<EquiCourseDetails> GetAll()
        {
            List<EquiCourseDetails> equicoursedetailsList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<EquiCourseDetails> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<EquiCourseDetails>(sqlGetAll, mapper);
                IEnumerable<EquiCourseDetails> collection = accessor.Execute();

                equicoursedetailsList = collection.ToList();
            }

            catch (Exception ex)
            {
                return equicoursedetailsList;
            }

            return equicoursedetailsList;
        }
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, EquiCourseDetails equicoursedetails, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "EquiCourseDetailId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "EquiCourseDetailId", DbType.Int32, equicoursedetails.EquiCourseDetailId);
            }

            	
		db.AddInParameter(cmd,"EquiCourseMasterId",DbType.Int32,equicoursedetails.EquiCourseMasterId);
		db.AddInParameter(cmd,"ProgramId",DbType.Int32,equicoursedetails.ProgramId);
		db.AddInParameter(cmd,"CourseId",DbType.Int32,equicoursedetails.CourseId);
		db.AddInParameter(cmd,"VersionId",DbType.Int32,equicoursedetails.VersionId);
		db.AddInParameter(cmd,"Attribute1",DbType.String,equicoursedetails.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,equicoursedetails.Attribute2);
		db.AddInParameter(cmd,"Attribute3",DbType.String,equicoursedetails.Attribute3);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,equicoursedetails.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,equicoursedetails.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,equicoursedetails.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,equicoursedetails.ModifiedDate);
            
            return db;
        }

        private IRowMapper<EquiCourseDetails> GetMaper()
        {
            IRowMapper<EquiCourseDetails> mapper = MapBuilder<EquiCourseDetails>.MapAllProperties()

       	   .Map(m => m.EquiCourseDetailId).ToColumn("EquiCourseDetailId")
		.Map(m => m.EquiCourseMasterId).ToColumn("EquiCourseMasterId")
		.Map(m => m.ProgramId).ToColumn("ProgramId")
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
        #endregion

        public List<EquivalentCourseDTO> GetAllEquivalentCourse()
        {
            List<EquivalentCourseDTO> equicoursedetailsList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<EquivalentCourseDTO> mapper = GetEquivalentCourseMaper();

                var accessor = db.CreateSprocAccessor<EquivalentCourseDTO>(sqlGetEquivalentCourseAll, mapper);
                IEnumerable<EquivalentCourseDTO> collection = accessor.Execute();

                equicoursedetailsList = collection.ToList();
            }

            catch (Exception ex)
            {
                return equicoursedetailsList;
            }

            return equicoursedetailsList;
        }

        public List<EquivalentCourseDTO> GetAllEquivalentCourseForRpt()
        {
            List<EquivalentCourseDTO> equicoursedetailsList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<EquivalentCourseDTO> mapper = GetEquivalentCourseMaperForRpt();

                var accessor = db.CreateSprocAccessor<EquivalentCourseDTO>(sqlGetEquiCourseForRpt, mapper);
                IEnumerable<EquivalentCourseDTO> collection = accessor.Execute();

                equicoursedetailsList = collection.ToList();
            }

            catch (Exception ex)
            {
                return equicoursedetailsList;
            }

            return equicoursedetailsList;
        }


        public List<EquivalentCourseDTO> GetAllEquivalentCourseByMasterId(int equivalentCoursemasterId)
        {
            List<EquivalentCourseDTO> equicoursedetailsList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<EquivalentCourseDTO> mapper = GetEquivalentCourseMaper();

                var accessor = db.CreateSprocAccessor<EquivalentCourseDTO>(sqlGetEquiCourseMasterId, mapper);
                IEnumerable<EquivalentCourseDTO> collection = accessor.Execute(equivalentCoursemasterId);

                equicoursedetailsList = collection.ToList();
            }

            catch (Exception ex)
            {
                return equicoursedetailsList;
            }

            return equicoursedetailsList;
        }

        public List<EquivalentCourseDTO> GetAllEquivalentCourseByParameters(int programId, int courseId, int versionId, string vesionCode)
        {
            List<EquivalentCourseDTO> equicoursedetailsList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<EquivalentCourseDTO> mapper = GetEquivalentCourseMaper();

                var accessor = db.CreateSprocAccessor<EquivalentCourseDTO>(sqlGetEquiCourseByParameters, mapper);
                IEnumerable<EquivalentCourseDTO> collection = accessor.Execute(programId, courseId, versionId, vesionCode);

                equicoursedetailsList = collection.ToList();
            }

            catch (Exception ex)
            {
                return equicoursedetailsList;
            }

            return equicoursedetailsList;
        }

        private IRowMapper<EquivalentCourseDTO> GetEquivalentCourseMaper()
        {
            IRowMapper<EquivalentCourseDTO> mapper = MapBuilder<EquivalentCourseDTO>.MapAllProperties()

            .Map(m => m.GroupName).ToColumn("GroupName")
            .Map(m => m.GroupNo).ToColumn("GroupNo")
            .Map(m => m.EquiCourseDetailId).ToColumn("EquiCourseDetailId")
            .Map(m => m.EquiCourseMasterId).ToColumn("EquiCourseMasterId")
            .Map(m => m.ProgramId).ToColumn("ProgramId")
            .Map(m => m.CourseId).ToColumn("CourseId")
            .Map(m => m.VersionId).ToColumn("VersionId")
            .Map(m => m.Credits).ToColumn("Credits")
            .Map(m => m.Title).ToColumn("Title")
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.VersionCode).ToColumn("VersionCode")
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
        private IRowMapper<EquivalentCourseDTO> GetEquivalentCourseMaperForRpt()
        {
            IRowMapper<EquivalentCourseDTO> mapper = MapBuilder<EquivalentCourseDTO>.MapAllProperties()

            .Map(m => m.GroupName).ToColumn("GroupName")
            .Map(m => m.Title).ToColumn("EquiValentCourse")
            .Map(m => m.CourseId).ToColumn("RowNumber") // Just for Efficient use by Sandy

            .DoNotMap(m => m.Credits)
            .DoNotMap(m => m.FormalCode)
            .DoNotMap(m => m.GroupNo)
            .DoNotMap(m => m.EquiCourseDetailId)
            .DoNotMap(m => m.EquiCourseMasterId)
            .DoNotMap(m => m.ProgramId)
            .DoNotMap(m => m.VersionId)
            .DoNotMap(m => m.VersionCode)
            .DoNotMap(m => m.Attribute1)
            .DoNotMap(m => m.Attribute2)
            .DoNotMap(m => m.Attribute3)
            .DoNotMap(m => m.CreatedBy)
            .DoNotMap(m => m.CreatedDate)
            .DoNotMap(m => m.ModifiedBy)
            .DoNotMap(m => m.ModifiedDate)

            .Build();

            return mapper;
        }

    }
}

