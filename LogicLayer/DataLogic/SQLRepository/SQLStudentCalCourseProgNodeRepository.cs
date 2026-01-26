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
    public partial class SQLStudentCalCourseProgNodeRepository : IStudentCalCourseProgNodeRepository
    {
        Database db = null;

        private string sqlInsert = "StudentCalCourseProgNodeInsert";
        private string sqlUpdate = "StudentCalCourseProgNodeUpdate";
        private string sqlDelete = "StudentCalCourseProgNodeDeleteById";
        private string sqlGetById = "StudentCalCourseProgNodeGetById";
        private string sqlGetAll = "StudentCalCourseProgNodeGetAll";
        
        
        public int Insert(StudentCalCourseProgNode studentCalCourseProgNode)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, studentCalCourseProgNode, isInsert);
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

        public bool Update(StudentCalCourseProgNode studentCalCourseProgNode)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, studentCalCourseProgNode, isInsert);

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

        public StudentCalCourseProgNode GetById(int? id)
        {
            StudentCalCourseProgNode _studentCalCourseProgNode = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCalCourseProgNode> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCalCourseProgNode>(sqlGetById, rowMapper);
                _studentCalCourseProgNode = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentCalCourseProgNode;
            }

            return _studentCalCourseProgNode;
        }

        public List<StudentCalCourseProgNode> GetAll()
        {
            List<StudentCalCourseProgNode> studentCalCourseProgNodeList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCalCourseProgNode> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCalCourseProgNode>(sqlGetAll, mapper);
                IEnumerable<StudentCalCourseProgNode> collection = accessor.Execute();

                studentCalCourseProgNodeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentCalCourseProgNodeList;
            }

            return studentCalCourseProgNodeList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StudentCalCourseProgNode studentCalCourseProgNode, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ID", DbType.Int32, studentCalCourseProgNode.ID);
            }

            db.AddInParameter(cmd, "StudentID", DbType.Int32, studentCalCourseProgNode.StudentID);
            db.AddInParameter(cmd, "CalCourseProgNodeID", DbType.Int32, studentCalCourseProgNode.CalCourseProgNodeID);
            db.AddInParameter(cmd, "IsCompleted", DbType.Boolean, studentCalCourseProgNode.IsCompleted);
            db.AddInParameter(cmd, "OriginalCalID", DbType.Int32, studentCalCourseProgNode.OriginalCalID);
            db.AddInParameter(cmd, "IsAutoAssign", DbType.Boolean, studentCalCourseProgNode.IsAutoAssign);
            db.AddInParameter(cmd, "IsAutoOpen", DbType.Boolean, studentCalCourseProgNode.IsAutoOpen);
            db.AddInParameter(cmd, "Isrequisitioned", DbType.Boolean, studentCalCourseProgNode.Isrequisitioned);
            db.AddInParameter(cmd, "IsMandatory", DbType.Boolean, studentCalCourseProgNode.IsMandatory);
            db.AddInParameter(cmd, "IsManualOpen", DbType.Boolean, studentCalCourseProgNode.IsManualOpen);
            db.AddInParameter(cmd, "TreeCalendarDetailID", DbType.Int32, studentCalCourseProgNode.TreeCalendarDetailID);
            db.AddInParameter(cmd, "TreeCalendarMasterID", DbType.Int32, studentCalCourseProgNode.TreeCalendarMasterID);
            db.AddInParameter(cmd, "TreeMasterID", DbType.Int32, studentCalCourseProgNode.TreeMasterID);
            db.AddInParameter(cmd, "CalendarMasterName", DbType.String, studentCalCourseProgNode.CalendarMasterName);
            db.AddInParameter(cmd, "CalendarDetailName", DbType.String, studentCalCourseProgNode.CalendarDetailName);
            db.AddInParameter(cmd, "CourseID", DbType.Int32, studentCalCourseProgNode.CourseID);
            db.AddInParameter(cmd, "VersionID", DbType.Int32, studentCalCourseProgNode.VersionID);
            db.AddInParameter(cmd, "Credits", DbType.Decimal, studentCalCourseProgNode.Credits);
            db.AddInParameter(cmd, "Node_CourseID", DbType.Int32, studentCalCourseProgNode.Node_CourseID);
            db.AddInParameter(cmd, "NodeID", DbType.Int32, studentCalCourseProgNode.NodeID);
            db.AddInParameter(cmd, "IsMajorRelated", DbType.Boolean, studentCalCourseProgNode.IsMajorRelated);
            db.AddInParameter(cmd, "NodeLinkName", DbType.String, studentCalCourseProgNode.NodeLinkName);
            db.AddInParameter(cmd, "FormalCode", DbType.String, studentCalCourseProgNode.FormalCode);
            db.AddInParameter(cmd, "VersionCode", DbType.String, studentCalCourseProgNode.VersionCode);
            db.AddInParameter(cmd, "CourseTitle", DbType.String, studentCalCourseProgNode.CourseTitle);
            db.AddInParameter(cmd, "AcaCal_SectionID", DbType.Int32, studentCalCourseProgNode.AcaCal_SectionID);
            db.AddInParameter(cmd, "SectionName", DbType.String, studentCalCourseProgNode.SectionName);
            db.AddInParameter(cmd, "ProgramID", DbType.Int32, studentCalCourseProgNode.ProgramID);
            db.AddInParameter(cmd, "DeptID", DbType.Int32, studentCalCourseProgNode.DeptID);
            db.AddInParameter(cmd, "Priority", DbType.Int32, studentCalCourseProgNode.Priority);
            db.AddInParameter(cmd, "RetakeNo", DbType.Int32, studentCalCourseProgNode.RetakeNo);
            db.AddInParameter(cmd, "ObtainedGPA", DbType.Decimal, studentCalCourseProgNode.ObtainedGPA);
            db.AddInParameter(cmd, "ObtainedGrade", DbType.String, studentCalCourseProgNode.ObtainedGrade);
            db.AddInParameter(cmd, "AcademicCalenderID", DbType.Int32, studentCalCourseProgNode.AcademicCalenderID);
            db.AddInParameter(cmd, "AcaCalYear", DbType.Int32, studentCalCourseProgNode.AcaCalYear);
            db.AddInParameter(cmd, "BatchCode", DbType.String, studentCalCourseProgNode.BatchCode);
            db.AddInParameter(cmd, "AcaCalTypeName", DbType.String, studentCalCourseProgNode.AcaCalTypeName);
            db.AddInParameter(cmd, "CalCrsProgNdCredits", DbType.Decimal, studentCalCourseProgNode.CalCrsProgNdCredits);
            db.AddInParameter(cmd, "CalCrsProgNdIsMajorRelated", DbType.Boolean, studentCalCourseProgNode.CalCrsProgNdIsMajorRelated);
            db.AddInParameter(cmd, "IsMultipleACUSpan", DbType.Boolean, studentCalCourseProgNode.IsMultipleACUSpan);
            db.AddInParameter(cmd, "CompletedCredit", DbType.Decimal, studentCalCourseProgNode.CompletedCredit);
            db.AddInParameter(cmd, "CourseCredit", DbType.Decimal, studentCalCourseProgNode.CourseCredit);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, studentCalCourseProgNode.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, studentCalCourseProgNode.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, studentCalCourseProgNode.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, studentCalCourseProgNode.ModifiedDate);
            return db;
        }

        private IRowMapper<StudentCalCourseProgNode> GetMaper()
        {
            IRowMapper<StudentCalCourseProgNode> mapper = MapBuilder<StudentCalCourseProgNode>.MapAllProperties()
            .Map(m => m.ID).ToColumn("ID")
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.CalCourseProgNodeID).ToColumn("CalCourseProgNodeID")
            .Map(m => m.IsCompleted).ToColumn("IsCompleted")
            .Map(m => m.OriginalCalID).ToColumn("OriginalCalID")
            .Map(m => m.IsAutoAssign).ToColumn("IsAutoAssign")
            .Map(m => m.IsAutoOpen).ToColumn("IsAutoOpen")
            .Map(m => m.Isrequisitioned).ToColumn("Isrequisitioned")
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
            .Map(m => m.AcademicCalenderID).ToColumn("AcademicCalenderID")
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
            .Build();

            return mapper;
        }
        #endregion
    }
}
