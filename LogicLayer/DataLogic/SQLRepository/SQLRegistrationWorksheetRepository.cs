using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using System.Data;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLRegistrationWorksheetRepository : IRegistrationWorksheetRepository
    {
        Database db = null;

        private string sqlInsert = "RegistrationWorksheetInsert";
        private string sqlUpdate = "RegistrationWorksheetUpdate";
        private string sqlDelete = "RegistrationWorksheetDelete";
        private string sqlGetById = "RegistrationWorksheetGetById";
        private string sqlGetAllByStudentID = "RegistrationWorksheetGetAllByStudentID";
        private string sqlGetAllOpenCourseByStudentID = "RegistrationWorksheetGetAllOpenCourseByStudentID";
        private string sqlGetAllAutoAssignCourseByStudentID = "RegistrationWorksheetGetAllAutoAssignCourseByStudentID";
        private string sqlGetAll = "RegistrationWorksheetGetAll";
        private string sqlRegisterCourse = "RegistrationWorksheetRegisterCourse";
        private string sqlGetAllByStdProgCal = "RegistrationWorksheetGetAllByStdProgCal";
        private string sqlGetAllByAcaProgCourse = "RegistrationWorksheetGetAllByAcaProgCourse";
        private string sqlGetAllByProgCal = "RegistrationWorksheetGetAllByProgCal";
        private string sqlGetAllByProgSession = "RegistrationWorksheetGetAllByProgSession";
        private string sqlGetReqByProgCal = "RegistrationWorksheetGetReqByProgCal";
        private string sqlGetPreRegByProgCal = "RegistrationWorksheetGetPreRegisteredByProgCal";
        private string sqlGetPreAdByProgCal = "RegistrationWorksheetGetPreAdvisingByProgCal";
        private string sqlGetPreRegByCourse = "RegistrationWorksheetGetPreRegisteredByCourse";
        private string sqlGetPreReqByCourse = "RegistrationWorksheetGetReqByCourse";
        private string sqlGetAllRegistrationSessionDataByStudentID = "RegistrationWorksheetGetAllRegistrationSessionDataByStudentID";
        private string sqlGetAllByProgramId = "RegistrationWorksheetGetAllByProgramId";
        private string sqlGetAllByAcaCalIdProgramId = "RegistrationWorksheetGetAllByAcaCalIdProgramId";

        public int Insert(RegistrationWorksheet registrationWorksheet)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, registrationWorksheet, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ID");

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

        public bool Update(RegistrationWorksheet registrationWorksheet)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, registrationWorksheet, isInsert);

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

                db.AddInParameter(cmd, "ID", DbType.Int32, id);
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

        public RegistrationWorksheet GetById(int id)
        {
            RegistrationWorksheet registrationWorksheet = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RegistrationWorksheet> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>(sqlGetById, rowMapper);
                registrationWorksheet = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return registrationWorksheet;
            }

            return registrationWorksheet;
        }

        public List<RegistrationWorksheet> GetByStudentID(int studentID)
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<RegistrationWorksheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>(sqlGetAllByStudentID, mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute(studentID);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<RegistrationWorksheet> GetAllOpenCourseByStudentID(int studentID)
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<RegistrationWorksheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>(sqlGetAllOpenCourseByStudentID, mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute(studentID);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<RegistrationWorksheet> GetAll()
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RegistrationWorksheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>(sqlGetAll, mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public bool RegisterCourse(int id, int createdBy, int courseStatusID)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlRegisterCourse);

                db.AddInParameter(cmd, "ID", DbType.Int32, id);
                db.AddInParameter(cmd, "CreatedBy", DbType.Int32, createdBy);
                db.AddInParameter(cmd, "CourseStatusID", DbType.Int32, courseStatusID);
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

        public List<RegistrationWorksheet> GetAllByStdProgCal(int studentId, int programId, int batchId)
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<RegistrationWorksheet> mapper = GetMaperStdProgCall();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>(sqlGetAllByStdProgCal, mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute(studentId, programId, batchId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<RegistrationWorksheet> GetAllByProgCal(int programId, int batchId)
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<RegistrationWorksheet> mapper = GetMaperStdProgCall();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>(sqlGetAllByProgSession, mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute(programId, batchId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<RegistrationWorksheet> GetReqByProgCal(int programId, int batchId)
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<RegistrationWorksheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>(sqlGetReqByProgCal, mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute(programId, batchId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<RegistrationWorksheet> GetPreRegByProgCal(int programId, int batchId)
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<RegistrationWorksheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>(sqlGetPreRegByProgCal, mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute(programId, batchId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<RegistrationWorksheet> GetPreAdByProgCal(int programId, int batchId)
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<RegistrationWorksheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>(sqlGetPreAdByProgCal, mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute(programId, batchId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<RegistrationWorksheet> GetPreRegByCourse(int courseId, int versionId)
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<RegistrationWorksheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>(sqlGetPreRegByCourse, mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute(courseId, versionId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<RegistrationWorksheet> GetReqByCourse(int courseId, int versionId)
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<RegistrationWorksheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>(sqlGetPreReqByCourse, mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute(courseId, versionId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<RegistrationWorksheet> GetAllByAcaProgCourse(int acaCalId, int programId, string courseList)
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<RegistrationWorksheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>(sqlGetAllByAcaProgCourse, mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute(acaCalId, programId, courseList);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        #region Generate Registration Worksheet
        public bool RegistrationWorksheetGeneratePerStudent(RegistrationWorksheetParam registrationWorksheetGenerate, int semesterNumber)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("RegistrationWorksheetGeneratePerStudent");

                db.AddInParameter(cmd, "StudentId", DbType.Int32, registrationWorksheetGenerate.StudentId);
                db.AddInParameter(cmd, "TreeCalendarMasterID", DbType.Int32, registrationWorksheetGenerate.TreeCalendarMasterID);
                db.AddInParameter(cmd, "TreeMasterID", DbType.Int32, registrationWorksheetGenerate.TreeMasterID);
                db.AddInParameter(cmd, "BatchID", DbType.Int32, registrationWorksheetGenerate.BatchID);
                db.AddInParameter(cmd, "ProgramID", DbType.Int32, registrationWorksheetGenerate.ProgramID);
                db.AddInParameter(cmd, "DepartmentID", DbType.Int32, registrationWorksheetGenerate.DepartmentID);
                db.AddOutParameter(cmd, "ReturnValue", DbType.Int32, registrationWorksheetGenerate.ReturnValue);
                db.AddInParameter(cmd, "CourseOpenType", DbType.Int32, registrationWorksheetGenerate.CourseOpenType);
                db.AddInParameter(cmd, "SemesterNumber", DbType.Int32, semesterNumber);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ReturnValue");

                int id = 0;
                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out id);
                }

                if (id > 0)
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

        public bool RegistrationWorksheetAutoOpen(RegistrationWorksheetParam registrationWorksheetAutoOpen)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("RegistrationWorksheetAutoOpen");

                db = addParam(db, cmd, registrationWorksheetAutoOpen);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ReturnValue");

                int id = 0;
                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out id);
                }

                if (id > 0)
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

        public bool RegistrationWorksheetAutoPreRegistration(RegistrationWorksheetParam registrationWorksheetAutoPreReg)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("RegistrationWorksheetAutoPreRegistration");

                db = addParam(db, cmd, registrationWorksheetAutoPreReg);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ReturnValue");

                int id = 0;
                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out id);
                }

                if (id > 0)
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

        public bool RegistrationWorksheetAutoMandatory(RegistrationWorksheetParam registrationWorksheetAutoMandatory)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("RegistrationWorksheetAutoMandatory");

                db = addParam(db, cmd, registrationWorksheetAutoMandatory);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ReturnValue");

                int id = 0;
                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out id);
                }

                if (id > 0)
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
        #endregion

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, RegistrationWorksheet registrationWorksheet, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ID", DbType.Int32, registrationWorksheet.ID);
            }

            db.AddInParameter(cmd, "StudentID", DbType.Int32, registrationWorksheet.StudentID);
            db.AddInParameter(cmd, "CalCourseProgNodeID", DbType.Int32, registrationWorksheet.CalCourseProgNodeID);
            db.AddInParameter(cmd, "IsCompleted", DbType.Boolean, registrationWorksheet.IsCompleted);
            db.AddInParameter(cmd, "OriginalCalID", DbType.Int32, registrationWorksheet.OriginalCalID);
            db.AddInParameter(cmd, "IsAutoAssign", DbType.Boolean, registrationWorksheet.IsAutoAssign);
            db.AddInParameter(cmd, "IsAutoOpen", DbType.Boolean, registrationWorksheet.IsAutoOpen);
            db.AddInParameter(cmd, "Isrequisitioned", DbType.Boolean, registrationWorksheet.IsRequisitioned);
            db.AddInParameter(cmd, "IsMandatory", DbType.Boolean, registrationWorksheet.IsMandatory);
            db.AddInParameter(cmd, "IsManualOpen", DbType.Boolean, registrationWorksheet.IsManualOpen);
            db.AddInParameter(cmd, "TreeCalendarDetailID", DbType.Int32, registrationWorksheet.TreeCalendarDetailID);
            db.AddInParameter(cmd, "TreeCalendarMasterID", DbType.Int32, registrationWorksheet.TreeCalendarMasterID);
            db.AddInParameter(cmd, "TreeMasterID", DbType.Int32, registrationWorksheet.TreeMasterID);
            db.AddInParameter(cmd, "CalendarMasterName", DbType.String, registrationWorksheet.CalendarMasterName);
            db.AddInParameter(cmd, "CalendarDetailName", DbType.String, registrationWorksheet.CalendarDetailName);
            db.AddInParameter(cmd, "CourseID", DbType.Int32, registrationWorksheet.CourseID);
            db.AddInParameter(cmd, "VersionID", DbType.Int32, registrationWorksheet.VersionID);
            db.AddInParameter(cmd, "Credits", DbType.Decimal, registrationWorksheet.Credits);
            db.AddInParameter(cmd, "Node_CourseID", DbType.Int32, registrationWorksheet.Node_CourseID);
            db.AddInParameter(cmd, "NodeID", DbType.Int32, registrationWorksheet.NodeID);
            db.AddInParameter(cmd, "IsMajorRelated", DbType.Boolean, registrationWorksheet.IsMajorRelated);
            db.AddInParameter(cmd, "NodeLinkName", DbType.String, registrationWorksheet.NodeLinkName);
            db.AddInParameter(cmd, "FormalCode", DbType.String, registrationWorksheet.FormalCode);
            db.AddInParameter(cmd, "VersionCode", DbType.String, registrationWorksheet.VersionCode);
            db.AddInParameter(cmd, "CourseTitle", DbType.String, registrationWorksheet.CourseTitle);
            db.AddInParameter(cmd, "AcaCal_SectionID", DbType.Int32, registrationWorksheet.AcaCal_SectionID);
            db.AddInParameter(cmd, "SectionName", DbType.String, registrationWorksheet.SectionName);
            db.AddInParameter(cmd, "ProgramID", DbType.Int32, registrationWorksheet.ProgramID);
            db.AddInParameter(cmd, "DeptID", DbType.Int32, registrationWorksheet.DeptID);
            db.AddInParameter(cmd, "Priority", DbType.Int32, registrationWorksheet.Priority);
            db.AddInParameter(cmd, "RetakeNo", DbType.Int32, registrationWorksheet.RetakeNo);
            db.AddInParameter(cmd, "ObtainedGPA", DbType.Decimal, registrationWorksheet.ObtainedGPA);
            db.AddInParameter(cmd, "ObtainedGrade", DbType.String, registrationWorksheet.ObtainedGrade);

            db.AddInParameter(cmd, "AcaCalYear", DbType.Int32, registrationWorksheet.AcaCalYear);
            db.AddInParameter(cmd, "BatchCode", DbType.String, registrationWorksheet.BatchCode);
            db.AddInParameter(cmd, "AcaCalTypeName", DbType.String, registrationWorksheet.AcaCalTypeName);
            db.AddInParameter(cmd, "CalCrsProgNdCredits", DbType.Decimal, registrationWorksheet.CalCrsProgNdCredits);
            db.AddInParameter(cmd, "CalCrsProgNdIsMajorRelated", DbType.Boolean, registrationWorksheet.CalCrsProgNdIsMajorRelated);
            db.AddInParameter(cmd, "IsMultipleACUSpan", DbType.Boolean, registrationWorksheet.IsMultipleACUSpan);
            db.AddInParameter(cmd, "CompletedCredit", DbType.Decimal, registrationWorksheet.CompletedCredit);
            db.AddInParameter(cmd, "CourseCredit", DbType.Decimal, registrationWorksheet.CourseCredit);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, registrationWorksheet.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, registrationWorksheet.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, registrationWorksheet.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, registrationWorksheet.ModifiedDate);
            db.AddInParameter(cmd, "CourseStatusId", DbType.Int32, registrationWorksheet.CourseStatusId);
            db.AddInParameter(cmd, "IsRegistered", DbType.Boolean, registrationWorksheet.IsRegistered);
            db.AddInParameter(cmd, "IsAdd", DbType.Boolean, registrationWorksheet.IsAdd);
            db.AddInParameter(cmd, "ConflictedCourse", DbType.String, registrationWorksheet.ConflictedCourse);
            db.AddInParameter(cmd, "SequenceNo", DbType.Int32, registrationWorksheet.SequenceNo);
            db.AddInParameter(cmd, "CourseResultAcaCalID", DbType.Int32, registrationWorksheet.CourseResultAcaCalID);

            db.AddInParameter(cmd, "IsOfferedCourse", DbType.Boolean, registrationWorksheet.IsOfferedCourse);
            db.AddInParameter(cmd, "PostMajorNodeLevel", DbType.Int32, registrationWorksheet.PostMajorNodeLevel);
            db.AddInParameter(cmd, "IsCreditCourse", DbType.Boolean, registrationWorksheet.IsCreditCourse);
            db.AddInParameter(cmd, "BatchID", DbType.Int32, registrationWorksheet.BatchID);
            return db;
        }

        private Database addParam(Database db, DbCommand cmd, RegistrationWorksheetParam registrationWorksheetParam)
        {
            db.AddInParameter(cmd, "StudentId", DbType.Int32, registrationWorksheetParam.StudentId);
            db.AddInParameter(cmd, "TreeCalendarMasterID", DbType.Int32, registrationWorksheetParam.TreeCalendarMasterID);
            db.AddInParameter(cmd, "TreeMasterID", DbType.Int32, registrationWorksheetParam.TreeMasterID);

            db.AddInParameter(cmd, "ProgramID", DbType.Int32, registrationWorksheetParam.ProgramID);
            db.AddInParameter(cmd, "DepartmentID", DbType.Int32, registrationWorksheetParam.DepartmentID);
            db.AddInParameter(cmd, "CrOpenLimit", DbType.Decimal, registrationWorksheetParam.CrOpenLimit);
            db.AddOutParameter(cmd, "ReturnValue", DbType.Int32, registrationWorksheetParam.ReturnValue);
            db.AddOutParameter(cmd, "BatchID", DbType.Int32, registrationWorksheetParam.BatchID);
            return db;
        }

        private IRowMapper<RegistrationWorksheet> GetMaper()
        {
            IRowMapper<RegistrationWorksheet> mapper = MapBuilder<RegistrationWorksheet>.MapAllProperties()
           .Map(m => m.ID).ToColumn("ID")
           .Map(m => m.StudentID).ToColumn("StudentID")
           .Map(m => m.CalCourseProgNodeID).ToColumn("CalCourseProgNodeID")
           .Map(m => m.IsCompleted).ToColumn("IsCompleted")
           .Map(m => m.OriginalCalID).ToColumn("OriginalCalID")
           .Map(m => m.IsAutoAssign).ToColumn("IsAutoAssign")
           .Map(m => m.IsAutoOpen).ToColumn("IsAutoOpen")
           .Map(m => m.IsRequisitioned).ToColumn("Isrequisitioned")
           .Map(m => m.IsMandatory).ToColumn("IsMandatory")
           .Map(m => m.IsManualOpen).ToColumn("IsManualOpen")
           .Map(m => m.TreeCalendarDetailID).ToColumn("TreeCalendarDetailID")
           .Map(m => m.TreeCalendarMasterID).ToColumn("TreeCalendarMasterID")
           .Map(m => m.TreeMasterID).ToColumn("TreeMasterID")
           .Map(m => m.CalendarMasterName).ToColumn("CalendarMasterName")
           .Map(m => m.CalendarDetailName).ToColumn("CalendarDetailName")
           .Map(m => m.CourseID).ToColumn("CourseID")
           .Map(m => m.VersionID).ToColumn("VersionID")
           .Map(m => m.Credits).ToColumn("Credits")
           .Map(m => m.Node_CourseID).ToColumn("Node_CourseID")
           .Map(m => m.NodeID).ToColumn("NodeID")
           .Map(m => m.IsMajorRelated).ToColumn("IsMajorRelated")//above add one
           .Map(m => m.NodeLinkName).ToColumn("NodeLinkName")
           .Map(m => m.FormalCode).ToColumn("FormalCode")
           .Map(m => m.VersionCode).ToColumn("VersionCode")
           .Map(m => m.CourseTitle).ToColumn("CourseTitle")
           .Map(m => m.AcaCal_SectionID).ToColumn("AcaCal_SectionID")
           .Map(m => m.SectionName).ToColumn("SectionName")
           .Map(m => m.ProgramID).ToColumn("ProgramID")
           .Map(m => m.DeptID).ToColumn("DeptID")
           .Map(m => m.Priority).ToColumn("Priority")
           .Map(m => m.RetakeNo).ToColumn("RetakeNo")
           .Map(m => m.ObtainedGPA).ToColumn("ObtainedGPA")
           .Map(m => m.ObtainedGrade).ToColumn("ObtainedGrade")

           .Map(m => m.AcaCalYear).ToColumn("AcaCalYear")
           .Map(m => m.BatchCode).ToColumn("BatchCode")
           .Map(m => m.AcaCalTypeName).ToColumn("AcaCalTypeName")
           .Map(m => m.CalCrsProgNdCredits).ToColumn("CalCrsProgNdCredits")
           .Map(m => m.CalCrsProgNdIsMajorRelated).ToColumn("CalCrsProgNdIsMajorRelated")
           .Map(m => m.IsMultipleACUSpan).ToColumn("IsMultipleACUSpan")
           .Map(m => m.CompletedCredit).ToColumn("CompletedCredit")
           .Map(m => m.CourseCredit).ToColumn("CourseCredit")
           .Map(m => m.CreatedBy).ToColumn("CreatedBy")
           .Map(m => m.CreatedDate).ToColumn("CreatedDate")
           .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
           .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
           .Map(m => m.CourseStatusId).ToColumn("CourseStatusId")
           .Map(m => m.IsRegistered).ToColumn("IsRegistered")
           .Map(m => m.IsAdd).ToColumn("IsAdd")
           .Map(m => m.ConflictedCourse).ToColumn("ConflictedCourse")
           .Map(m => m.SequenceNo).ToColumn("SequenceNo")
           .Map(m => m.CourseResultAcaCalID).ToColumn("CourseResultAcaCalID")//above add one
           .Map(m => m.BatchID).ToColumn("BatchID")
           .Build();

            return mapper;
        }

        private IRowMapper<RegistrationWorksheet> GetMaperStdProgCall()
        {
            IRowMapper<RegistrationWorksheet> mapper = MapBuilder<RegistrationWorksheet>.MapAllProperties()
           .Map(m => m.ID).ToColumn("ID")
           .Map(m => m.StudentID).ToColumn("StudentID")
           .Map(m => m.CalCourseProgNodeID).ToColumn("CalCourseProgNodeID")
           .Map(m => m.IsCompleted).ToColumn("IsCompleted")
           .Map(m => m.OriginalCalID).ToColumn("OriginalCalID")
           .Map(m => m.IsAutoAssign).ToColumn("IsAutoAssign")
           .Map(m => m.IsAutoOpen).ToColumn("IsAutoOpen")
           .Map(m => m.IsRequisitioned).ToColumn("Isrequisitioned")
           .Map(m => m.IsMandatory).ToColumn("IsMandatory")
           .Map(m => m.IsManualOpen).ToColumn("IsManualOpen")
           .Map(m => m.TreeCalendarDetailID).ToColumn("TreeCalendarDetailID")
           .Map(m => m.TreeCalendarMasterID).ToColumn("TreeCalendarMasterID")
           .Map(m => m.TreeMasterID).ToColumn("TreeMasterID")
           .Map(m => m.CalendarMasterName).ToColumn("CalendarMasterName")
           .Map(m => m.CalendarDetailName).ToColumn("CalendarDetailName")
           .Map(m => m.CourseID).ToColumn("CourseID")
           .Map(m => m.VersionID).ToColumn("VersionID")
           .Map(m => m.Credits).ToColumn("Credits")
           .Map(m => m.Node_CourseID).ToColumn("Node_CourseID")
           .Map(m => m.NodeID).ToColumn("NodeID")
           .Map(m => m.IsMajorRelated).ToColumn("IsMajorRelated")
           .Map(m => m.NodeLinkName).ToColumn("NodeLinkName")
           .Map(m => m.FormalCode).ToColumn("FormalCode")
           .Map(m => m.VersionCode).ToColumn("VersionCode")
           .Map(m => m.CourseTitle).ToColumn("CourseTitle")
           .Map(m => m.AcaCal_SectionID).ToColumn("AcaCal_SectionID")
           .Map(m => m.SectionName).ToColumn("SectionName")
           .Map(m => m.ProgramID).ToColumn("ProgramID")
           .Map(m => m.DeptID).ToColumn("DeptID")
           .Map(m => m.Priority).ToColumn("Priority")
           .Map(m => m.RetakeNo).ToColumn("RetakeNo")
           .Map(m => m.ObtainedGPA).ToColumn("ObtainedGPA")
           .Map(m => m.ObtainedGrade).ToColumn("ObtainedGrade")
                //.Map(m => m.AcademicCalenderID).ToColumn("AcademicCalenderID")
           .Map(m => m.AcaCalYear).ToColumn("AcaCalYear")
           .Map(m => m.BatchCode).ToColumn("BatchCode")
           .Map(m => m.AcaCalTypeName).ToColumn("AcaCalTypeName")
           .Map(m => m.CalCrsProgNdCredits).ToColumn("CalCrsProgNdCredits")
           .Map(m => m.CalCrsProgNdIsMajorRelated).ToColumn("CalCrsProgNdIsMajorRelated")
           .Map(m => m.IsMultipleACUSpan).ToColumn("IsMultipleACUSpan")
           .Map(m => m.CompletedCredit).ToColumn("CompletedCredit")
           .Map(m => m.CourseCredit).ToColumn("CourseCredit")
           .Map(m => m.CreatedBy).ToColumn("CreatedBy")
           .Map(m => m.CreatedDate).ToColumn("CreatedDate")
           .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
           .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
           .Map(m => m.CourseStatusId).ToColumn("CourseStatusId")
           .Map(m => m.ConflictedCourse).ToColumn("ConflictedCourse")
           .Map(m => m.BatchID).ToColumn("BatchID")
           .Build();

            return mapper;
        }

        #endregion

        public bool UpdateForAssignCourseNew(RegistrationWorksheet registrationWorksheet)
        {

            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("RegistrationWorksheetUpdateForAssignCourseNew");

                db = addParam(db, cmd, registrationWorksheet, isInsert);

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

        public bool UpdateForAssignCourseRetake(RegistrationWorksheet registrationWorksheet)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("RegistrationWorksheetUpdateForAssignCourseRetake");

                db = addParam(db, cmd, registrationWorksheet, isInsert);

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

        public List<RegistrationWorksheet> GetAllAutoAssignCourseByStudentID(int studentId)
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<RegistrationWorksheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>(sqlGetAllAutoAssignCourseByStudentID, mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute(studentId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public bool CourseRegistrationForStudent(int studentId)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("RegistrationWorksheetCourseRegistrationForStudent");

                db.AddInParameter(cmd, "StudentId", DbType.Int32, studentId);

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

        public bool RegistrationWorksheetGeneratePerStudentBBA(RegistrationWorksheetParam registrationWorksheetGenerate)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("RegistrationWorksheetGeneratePerStudentBBA");

                db.AddInParameter(cmd, "StudentId", DbType.Int32, registrationWorksheetGenerate.StudentId);
                db.AddInParameter(cmd, "TreeCalendarMasterID", DbType.Int32, registrationWorksheetGenerate.TreeCalendarMasterID);
                db.AddInParameter(cmd, "TreeMasterID", DbType.Int32, registrationWorksheetGenerate.TreeMasterID);
                db.AddInParameter(cmd, "BatchID", DbType.Int32, registrationWorksheetGenerate.BatchID);
                db.AddInParameter(cmd, "ProgramID", DbType.Int32, registrationWorksheetGenerate.ProgramID);
                db.AddInParameter(cmd, "DepartmentID", DbType.Int32, registrationWorksheetGenerate.DepartmentID);
                db.AddOutParameter(cmd, "ReturnValue", DbType.Int32, registrationWorksheetGenerate.ReturnValue);


                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ReturnValue");

                int id = 0;
                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out id);
                }

                if (id > 0)
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

        public List<RegistrationWorksheet> GetAll(int acaCalId, int programId)
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<RegistrationWorksheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>(sqlGetAllByAcaCalIdProgramId, mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute(acaCalId, programId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<RegistrationWorksheet> GetRegistrationSessionDataByStudentID(int studentId)
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<RegistrationWorksheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>(sqlGetAllRegistrationSessionDataByStudentID, mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute(studentId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<RegistrationWorksheet> GetByProgramId(int programId)
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<RegistrationWorksheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>(sqlGetAllByProgramId, mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute(programId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public int CountTakenCourseInRW(int ProgramID, int CourseID, int VersionID, int TreeMasterID)
        {
            int count = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("RegistrationWorksheetCountTakenCourse");

                db.AddOutParameter(cmd, "Count", DbType.Int32, Int32.MaxValue);
                db.AddInParameter(cmd, "ProgramID", DbType.Int32, ProgramID);
                db.AddInParameter(cmd, "CourseID", DbType.Int32, CourseID);
                db.AddInParameter(cmd, "VersionID", DbType.Int32, VersionID);
                db.AddInParameter(cmd, "TreeMasterID", DbType.Int32, TreeMasterID);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "Count");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out count);
                }
            }
            catch (Exception ex)
            {
                count = 0;
            }

            return count;
        }

        public int CountOpenCourseInRW(int ProgramID, int CourseID, int VersionID, int TreeMasterID)
        {
            int count = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("RegistrationWorksheetCountOpenCourse");

                db.AddOutParameter(cmd, "Count", DbType.Int32, Int32.MaxValue);
                db.AddInParameter(cmd, "ProgramID", DbType.Int32, ProgramID);
                db.AddInParameter(cmd, "CourseID", DbType.Int32, CourseID);
                db.AddInParameter(cmd, "VersionID", DbType.Int32, VersionID);
                db.AddInParameter(cmd, "TreeMasterID", DbType.Int32, TreeMasterID);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "Count");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out count);
                }
            }
            catch (Exception ex)
            {
                count = 0;
            }

            return count;
        }

        public int CountMandatoryCourseInRW(int ProgramID, int CourseID, int VersionID, int TreeMasterID)
        {
            int count = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("RegistrationWorksheetCountMandatoryCourse");

                db.AddOutParameter(cmd, "Count", DbType.Int32, Int32.MaxValue);
                db.AddInParameter(cmd, "ProgramID", DbType.Int32, ProgramID);
                db.AddInParameter(cmd, "CourseID", DbType.Int32, CourseID);
                db.AddInParameter(cmd, "VersionID", DbType.Int32, VersionID);
                db.AddInParameter(cmd, "TreeMasterID", DbType.Int32, TreeMasterID);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "Count");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out count);
                }
            }
            catch (Exception ex)
            {
                count = 0;
            }

            return count;
        }

        #region IRegistrationWorksheetRepository Members

        public List<RegistrationWorksheet> GetAllOpenCourseWhichSectionIsMatchInStudentBatchByStudentID(int studentId)
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<RegistrationWorksheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>("RegistrationWorksheetGetAllOpenCourseWhichSectionIsMatchInStudentBatchByStudentID", mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute(studentId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public bool UpdateForSectionTake(RegistrationWorksheet registrationWorksheet)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("RegistrationWorksheetUpdateForSectionTake");

                db = addParam(db, cmd, registrationWorksheet, isInsert);

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

        public bool UpdateForSectionRemove(RegistrationWorksheet registrationWorksheet)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("RegistrationWorksheetUpdateForSectionRemove");

                db = addParam(db, cmd, registrationWorksheet, isInsert);

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

        public List<RegistrationWorksheet> GetAllStudentByProgramSession(int programId, int sessionId)
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<RegistrationWorksheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>("RegistrationWorksheetGetAllByProgSession", mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute(programId, sessionId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<RegistrationWorksheet> GetAllStudentByProgramSession(int programId, int sessionId, int batchId)
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<RegistrationWorksheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>("RegistrationWorksheetGetAllByProgramSessionBatch", mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute(programId, sessionId, batchId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<RegistrationWorksheet> GetAllStudentByProgramSessionCourse(int programId, int sessionId, int batchId, int courseId, int versionId)
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<RegistrationWorksheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>("RegistrationWorksheetGetAllByProgramSessionBatchCourse", mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute(programId, sessionId, batchId, courseId, versionId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        #endregion

        public List<StudentBulkRegistration> GetAllStudentWithAllCourseSectionByProgramSessionBatch(int programId, int sessionId, int batchId)
        {
            List<StudentBulkRegistration> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<StudentBulkRegistration> mapper = GetStudentCourseSectionMaper();

                var accessor = db.CreateSprocAccessor<StudentBulkRegistration>("StudentCourseWithSectionGetAllByProgramSessionBatch", mapper);
                IEnumerable<StudentBulkRegistration> collection = accessor.Execute(programId, sessionId, batchId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<StudentBulkRegistration> GetStudentCourseSectionMaper()
        {
            IRowMapper<StudentBulkRegistration> mapper = MapBuilder<StudentBulkRegistration>.MapAllProperties()
           .Map(m => m.StudentID).ToColumn("StudentID")
           .Map(m => m.Roll).ToColumn("Roll")
           .Map(m => m.FullName).ToColumn("FullName")
           .Map(m => m.Gender).ToColumn("Gender")
           .Map(m => m.CourseWithSection).ToColumn("CourseWithSection")
           .Map(m => m.RegisteredCourseSection).ToColumn("RegisteredCourseSection")
           .Build();

            return mapper;
        }

        public List<RegistrationWorksheet> GetAllCourseByProgramSessionBatchStudentId(int StudentId, int sessionId)
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<RegistrationWorksheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>("RegistrationWorksheetGetAllByProgramSessionBatchStudentId", mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute(sessionId, StudentId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<RegistrationWorksheet> GetAllForwardCoursesByStudentIDAcaCalId(int studentId, int AcaCalId)
        {
            List<RegistrationWorksheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<RegistrationWorksheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>("RegistrationWorksheetGetAllForwardCoursesByStudentIDAcaCalId", mapper);
                IEnumerable<RegistrationWorksheet> collection = accessor.Execute(studentId, AcaCalId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public RegistrationWorksheet GetByStudentIdCourseIdVersionId(int studentId, int courseId, int versionId)
        {
            RegistrationWorksheet registrationWorksheet = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RegistrationWorksheet> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationWorksheet>("RegistrationWorksheetGetByStudentIdCourseIdVersionId", rowMapper);
                registrationWorksheet = accessor.Execute(studentId, courseId, versionId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return registrationWorksheet;
            }

            return registrationWorksheet;
        }
        
        public bool IsForwardOrRegistrationDoneForStudent(int StudentId, int AcaCalId, int IsRetake)
        {
            int id = 0; 

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("GetStudentCourseRegistrationStausByStudentIdAcaCalIdIsRetake");

                db.AddOutParameter(cmd, "IsDone", DbType.Int32, Int32.MaxValue);
                db.AddInParameter(cmd, "StudentId", DbType.Int32, StudentId);
                db.AddInParameter(cmd, "AcaCalId", DbType.Int32, AcaCalId);
                db.AddInParameter(cmd, "IsRetake", DbType.Boolean, IsRetake);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "IsDone");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out id);
                }
            }
            catch (Exception ex)
            {
                id = 0;
            }
            if (id > 0)
                return true;
            else
                return false;

        }

    }
}
