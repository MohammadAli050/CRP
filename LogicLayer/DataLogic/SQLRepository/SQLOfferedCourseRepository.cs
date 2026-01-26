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
    public partial class SQLOfferedCourseRepository : IOfferedCourseRepository
    {
        Database db = null;

        private string sqlInsert = "OfferedCourseInsert";
        private string sqlUpdate = "OfferedCourseUpdate";
        private string sqlDelete = "OfferedCourseDeleteById";
        private string sqlDeleteByProgramIdAcaCalId = "OfferedCourseDeleteByProgramIdAcaCalId";
        private string sqlDeleteByProgramIdAcaCalIdTreeRootId = "OfferedCourseDeleteByProgramIdAcaCalIdTreeRootId";
        private string sqlGetById = "OfferedCourseGetById";
        private string sqlGetAll = "OfferedCourseGetAll";
        private string sqlOfferedCourseDTOGetByProgramAcacalTreeroot = "OfferedCourseDTOGetByProgramAcacalTreeroot";
        private string sqlWorkSheetCourseHistoryGetStudentByProgramCourseVersionSection = "OfferedCourseGetStudentByAcaCalProgramCourseVersionSection";

        public int Insert(OfferedCourse offeredCourse)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, offeredCourse, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "OfferID");

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

        public int InsertList(List<OfferedCourse> offeredCourseList)
        {
            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>(); //CreateDB.GetInstance();
            using (DbConnection dbConnection = db.CreateConnection())
            {
                dbConnection.Open();
                DbTransaction dbTransaction = dbConnection.BeginTransaction();

                try
                {
                    foreach (OfferedCourse offeredCourse in offeredCourseList)
                    {
                        int id = 0;
                        bool isInsert = true;

                        DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                        db = addParam(db, cmd, offeredCourse, isInsert);
                        db.ExecuteNonQuery(cmd);

                        object obj = db.GetParameterValue(cmd, "OfferID");

                        if (obj != null)
                        {
                            int.TryParse(obj.ToString(), out id);
                        }
                    }

                    dbTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    return 0;
                }
                finally
                {
                    dbConnection.Close();
                }
            }
            return 1;
        }

        public bool Update(OfferedCourse offeredCourse)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>(); //CreateDB.GetInstance();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, offeredCourse, isInsert);

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
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>(); // CreateDB.GetInstance();
                DbCommand cmd = db.GetStoredProcCommand(sqlDelete);

                db.AddInParameter(cmd, "OfferID", DbType.Int32, id);
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

        public OfferedCourse GetById(int? id)
        {
            OfferedCourse _offeredCourse = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>(); // CreateDB.GetInstance();

                IRowMapper<OfferedCourse> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<OfferedCourse>(sqlGetById, rowMapper);
                _offeredCourse = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _offeredCourse;
            }

            return _offeredCourse;
        }

        public List<OfferedCourse> GetAll()
        {
            List<OfferedCourse> offeredCourseList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>(); //CreateDB.GetInstance();

                IRowMapper<OfferedCourse> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<OfferedCourse>(sqlGetAll, mapper);
                IEnumerable<OfferedCourse> collection = accessor.Execute();

                offeredCourseList = collection.ToList();
            }

            catch (Exception ex)
            {
                return offeredCourseList;
            }

            return offeredCourseList;
        }

        public bool DeleteByProgIdAndAcaCalId(int programId, int acaCalId)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>(); // CreateDB.GetInstance();
                DbCommand cmd = db.GetStoredProcCommand(sqlDeleteByProgramIdAcaCalId);

                db.AddInParameter(cmd, "ProgramID", DbType.Int32, programId);
                db.AddInParameter(cmd, "AcademicCalenderID", DbType.Int32, acaCalId);
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

        public bool UpdateList(List<OfferedCourse> offeredCourseList)
        {

            bool result = false;
            bool isInsert = false;

            db = EnterpriseLibraryContainer.Current.GetInstance<Database>(); //CreateDB.GetInstance();
            using (DbConnection dbConnection = db.CreateConnection())
            {
                dbConnection.Open();
                DbTransaction dbTransaction = dbConnection.BeginTransaction();

                try
                {
                    foreach (OfferedCourse offeredCourse in offeredCourseList)
                    {

                        DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                        db = addParam(db, cmd, offeredCourse, isInsert);

                        int rowsAffected = db.ExecuteNonQuery(cmd);

                        if (rowsAffected > 0)
                        {
                            result = true;
                        }
                    }

                    dbTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    return false;
                }
                finally
                {
                    dbConnection.Close();
                }
            }
            return result;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, OfferedCourse offeredCourse, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "OfferID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "OfferID", DbType.Int32, offeredCourse.OfferID);
            }
            db.AddInParameter(cmd, "AcademicCalenderID", DbType.Int32, offeredCourse.AcademicCalenderID);
            db.AddInParameter(cmd, "DeptID", DbType.Int32, offeredCourse.DeptID);
            db.AddInParameter(cmd, "ProgramID", DbType.String, offeredCourse.ProgramID);
            db.AddInParameter(cmd, "TreeRootID", DbType.Int32, offeredCourse.TreeRootID);
            db.AddInParameter(cmd, "CourseID", DbType.Int32, offeredCourse.CourseID);
            db.AddInParameter(cmd, "VersionID", DbType.Int32, offeredCourse.VersionID);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, offeredCourse.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, offeredCourse.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, offeredCourse.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, offeredCourse.ModifiedDate);
            db.AddInParameter(cmd, "Limit", DbType.Int32, offeredCourse.Limit);
            db.AddInParameter(cmd, "Occupied", DbType.Int32, offeredCourse.Occupied);
            db.AddInParameter(cmd, "Attribute1", DbType.String, offeredCourse.Attribute1);
            db.AddInParameter(cmd, "Attribute2", DbType.String, offeredCourse.Attribute2);
            db.AddInParameter(cmd, "Node_CourseID", DbType.Int32, offeredCourse.Node_CourseID);
            db.AddInParameter(cmd, "IsActive", DbType.Boolean, offeredCourse.IsActive);
            return db;
        }

        private IRowMapper<OfferedCourse> GetMaper()
        {
            IRowMapper<OfferedCourse> mapper = MapBuilder<OfferedCourse>.MapAllProperties()
            .Map(m => m.OfferID).ToColumn("OfferID")
            .Map(m => m.AcademicCalenderID).ToColumn("AcademicCalenderID")
            .Map(m => m.DeptID).ToColumn("DeptID")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.TreeRootID).ToColumn("TreeRootID")
            .Map(m => m.CourseID).ToColumn("CourseID")
            .Map(m => m.VersionID).ToColumn("VersionID")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Map(m => m.Limit).ToColumn("Limit")
            .Map(m => m.Occupied).ToColumn("Occupied")
            .Map(m => m.Attribute1).ToColumn("Attribute1")
            .Map(m => m.Attribute2).ToColumn("Attribute2")
            .Map(m => m.Node_CourseID).ToColumn("Node_CourseID")
            .Map(m => m.IsActive).ToColumn("IsActive")

            .DoNotMap(m => m.FormalCodeAndTitle)
            .Build();

            return mapper;
        }
        #endregion

        #region Mapper DTO
        private IRowMapper<OfferedCourseDTO> GetMaperDTO()
        {
            IRowMapper<OfferedCourseDTO> mapper = MapBuilder<OfferedCourseDTO>.MapAllProperties()
            .Map(m => m.OfferID).ToColumn("OfferID")
            .Map(m => m.AcademicCalenderID).ToColumn("AcademicCalenderID")
            .Map(m => m.DeptID).ToColumn("DeptID")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.TreeRootID).ToColumn("TreeRootID")
            .Map(m => m.CourseID).ToColumn("CourseID")
            .Map(m => m.VersionID).ToColumn("VersionID")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Map(m => m.Limit).ToColumn("Limit")
             .Map(m => m.Occupied).ToColumn("Occupied")
            .Map(m => m.Attribute1).ToColumn("Attribute1")
            .Map(m => m.Attribute2).ToColumn("Attribute2")
            .Map(m => m.Node_CourseID).ToColumn("Node_CourseID")
            .Map(m => m.IsActive).ToColumn("IsActive")

            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.VersionCode).ToColumn("VersionCode")
            .Map(m => m.Title).ToColumn("Title")

            .Map(m => m.Opened).ToColumn("Opened")
            .Map(m => m.OpenedAll).ToColumn("OpenedAll")
            .Map(m => m.Assigned).ToColumn("Assigned")
            .Map(m => m.AssignedAll).ToColumn("AssignedAll")
            .Map(m => m.Mandatory).ToColumn("Mandatory")
            .Map(m => m.MandatoryAll).ToColumn("MandatoryAll")
            .Build();

            return mapper;
        }

        private IRowMapper<WorkSheetCourseHistoryDTO> GetMaperStudentListDTO()
        {
            IRowMapper<WorkSheetCourseHistoryDTO> mapper = MapBuilder<WorkSheetCourseHistoryDTO>.MapAllProperties()
            .Map(m => m.FullName).ToColumn("FullName")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.BatchNo).ToColumn("BatchNo")
            .Map(m => m.IsWorkSheet).ToColumn("IsWorkSheet")
            .Map(m => m.IsCourseHistory).ToColumn("IsCourseHistory")
            .Build();

            return mapper;
        }
        #endregion

        public bool ActiveInactiveList(List<OfferedCourse> offeredCourseList)
        {
            bool result = false;
            bool isInsert = false;

            db = EnterpriseLibraryContainer.Current.GetInstance<Database>(); //CreateDB.GetInstance();
            using (DbConnection dbConnection = db.CreateConnection())
            {
                dbConnection.Open();
                DbTransaction dbTransaction = dbConnection.BeginTransaction();

                try
                {
                    foreach (OfferedCourse offeredCourse in offeredCourseList)
                    {

                        DbCommand cmd = db.GetStoredProcCommand("OfferedCourseActiveInactive");

                        db = addParam(db, cmd, offeredCourse, isInsert);

                        int rowsAffected = db.ExecuteNonQuery(cmd);

                        if (rowsAffected > 0)
                        {
                            result = true;
                        }
                    }

                    dbTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    return false;
                }
                finally
                {
                    dbConnection.Close();
                }
            }
            return result;
        }

        public bool DeleteByProgramAndAcaCalAndTreeRoot(int programId, int acaCalId, int treeRoot)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>(); // CreateDB.GetInstance();
                DbCommand cmd = db.GetStoredProcCommand(sqlDeleteByProgramIdAcaCalIdTreeRootId);

                db.AddInParameter(cmd, "ProgramID", DbType.Int32, programId);
                db.AddInParameter(cmd, "AcademicCalenderID", DbType.Int32, acaCalId);
                db.AddInParameter(cmd, "TreeRootID", DbType.Int32, treeRoot);
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

        public List<OfferedCourse> GetAllByProgramAcacalTreeroot(int programId, int acaCalId, int treeRoot)
        {
            List<OfferedCourse> offeredCourseList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>(); //CreateDB.GetInstance();

                IRowMapper<OfferedCourse> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<OfferedCourse>("OfferedCourseGetByProgramAcacalTreeroot", mapper);
                IEnumerable<OfferedCourse> collection = accessor.Execute(programId, acaCalId, treeRoot);

                offeredCourseList = collection.ToList();
            }

            catch (Exception ex)
            {
                return offeredCourseList;
            }

            return offeredCourseList;
        }

        public List<OfferedCourseDTO> GetAllDtoObjByProgramAcacalTreeroot(int programId, int acaCalId, int treeRoot)
        {
            List<OfferedCourseDTO> offeredCourseList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<OfferedCourseDTO> mapper = GetMaperDTO();

                var accessor = db.CreateSprocAccessor<OfferedCourseDTO>(sqlOfferedCourseDTOGetByProgramAcacalTreeroot, mapper);
                IEnumerable<OfferedCourseDTO> collection = accessor.Execute(programId, acaCalId, treeRoot);

                offeredCourseList = collection.ToList();
            }

            catch (Exception ex)
            {
                return offeredCourseList;
            }

            return offeredCourseList;
        }

        public OfferedCourse GetBy(int ProgramID, int AcademicCalenderID, int TreeMasterID, int CourseID, int VersionID)
        {
            OfferedCourse _offeredCourse = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>(); // CreateDB.GetInstance();

                IRowMapper<OfferedCourse> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<OfferedCourse>("OfferedCourseGetByProgramAcacalTreemasterCourseVersion", rowMapper);
                _offeredCourse = accessor.Execute(ProgramID, AcademicCalenderID, TreeMasterID, CourseID, VersionID).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _offeredCourse;
            }

            return _offeredCourse;
        }

        public List<WorkSheetCourseHistoryDTO> GetStudentByProgramCourseVersionSection(int acaCalId, int programId, int courseId, int versionId, int sectionId)
        {
            List<WorkSheetCourseHistoryDTO> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<WorkSheetCourseHistoryDTO> mapper = GetMaperStudentListDTO();

                var accessor = db.CreateSprocAccessor<WorkSheetCourseHistoryDTO>(sqlWorkSheetCourseHistoryGetStudentByProgramCourseVersionSection, mapper);
                IEnumerable<WorkSheetCourseHistoryDTO> collection = accessor.Execute(acaCalId, programId, courseId, versionId, sectionId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }
    }
}
