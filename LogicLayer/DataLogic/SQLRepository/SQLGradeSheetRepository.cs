using LogicLayer.BusinessObjects;
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
    partial class SQLGradeSheetRepository : IGradeSheetRepository
    {
        Database db = null;
        private string sqlInsert = "GradeSheetInsert";
        private string sqlUpdate = "GradeSheetUpdate";
        private string sqlDelete = "GradeSheetDeleteById";
        private string sqlGetById = "GradeSheetGetById";
        private string sqlGetAll = "GradeSheetGetAll";
        private string sqlGetAllByAcaCaSectionlId = "GradeSheetGetAllByAcaCalSectionId";


        public int Insert(GradeSheet gradeSheet)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, gradeSheet, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "GradeSheetId");

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

        public bool Update(GradeSheet gradeSheet)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, gradeSheet, isInsert);

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

                db.AddInParameter(cmd, "GradeSheetId", DbType.Int32, id);
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

        public GradeSheet GetById(int? id)
        {
            GradeSheet _gradeSheet = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<GradeSheet> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<GradeSheet>(sqlGetById, rowMapper);
                _gradeSheet = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _gradeSheet;
            }

            return _gradeSheet;
        }

        public List<GradeSheet> GetAll()
        {
            List<GradeSheet> gradeSheetList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<GradeSheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<GradeSheet>(sqlGetAll, mapper);
                IEnumerable<GradeSheet> collection = accessor.Execute();

                gradeSheetList = collection.ToList();
            }

            catch (Exception ex)
            {
                return gradeSheetList;
            }

            return gradeSheetList;
        }

        public List<GradeSheet> GetAllByAcaCalSectionId(int id)
        {
            List<GradeSheet> gradeSheetList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<GradeSheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<GradeSheet>(sqlGetAllByAcaCaSectionlId, mapper);
                IEnumerable<GradeSheet> collection = accessor.Execute(id);

                gradeSheetList = collection.ToList();
            }

            catch (Exception ex)
            {
                return gradeSheetList;
            }

            return gradeSheetList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, GradeSheet gradeSheet, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "GradeSheetId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "GradeSheetId", DbType.Int32, gradeSheet.GradeSheetId);
            }

            db.AddInParameter(cmd, "ExamMarksAllocationID", DbType.Int32, gradeSheet.ExamMarksAllocationID);
            db.AddInParameter(cmd, "ProgramID", DbType.Int32, gradeSheet.ProgramID);
            db.AddInParameter(cmd, "AcademicCalenderID", DbType.Int32, gradeSheet.AcademicCalenderID);
            db.AddInParameter(cmd, "CourseID", DbType.Int32, gradeSheet.CourseID);
            db.AddInParameter(cmd, "VersionID", DbType.Int32, gradeSheet.VersionID);
            db.AddInParameter(cmd, "StudentID", DbType.Int32, gradeSheet.StudentID);
            db.AddInParameter(cmd, "AcaCal_SectionID", DbType.Int32, gradeSheet.AcaCal_SectionID);
            db.AddInParameter(cmd, "TeacherID", DbType.Int32, gradeSheet.TeacherID);
            db.AddInParameter(cmd, "ObtainMarks", DbType.Decimal, gradeSheet.ObtainMarks);
            db.AddInParameter(cmd, "ObtainGrade", DbType.String, gradeSheet.ObtainGrade);
            db.AddInParameter(cmd, "GradeId", DbType.Int32, gradeSheet.GradeId);
            db.AddInParameter(cmd, "IsFinalSubmit", DbType.Boolean, gradeSheet.IsFinalSubmit);
            db.AddInParameter(cmd, "IsTransfer", DbType.Boolean, gradeSheet.IsTransfer);
            db.AddInParameter(cmd, "IsConflictWithRetake", DbType.Boolean, gradeSheet.IsConflictWithRetake);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, gradeSheet.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, gradeSheet.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, gradeSheet.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, gradeSheet.ModifiedDate);

            return db;
        }

        private IRowMapper<GradeSheet> GetMaper()
        {
            IRowMapper<GradeSheet> mapper = MapBuilder<GradeSheet>.MapAllProperties()
            .Map(m => m.GradeSheetId).ToColumn("GradeSheetId")
            .Map(m => m.ExamMarksAllocationID).ToColumn("ExamMarksAllocationID")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.AcademicCalenderID).ToColumn("AcademicCalenderID")
            .Map(m => m.CourseID).ToColumn("CourseID")
            .Map(m => m.VersionID).ToColumn("VersionID")
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.AcaCal_SectionID).ToColumn("AcaCal_SectionID")
            .Map(m => m.TeacherID).ToColumn("TeacherID")
            .Map(m => m.ObtainMarks).ToColumn("ObtainMarks")
            .Map(m => m.ObtainGrade).ToColumn("ObtainGrade")
            .Map(m => m.GradeId).ToColumn("GradeId")
            .Map(m => m.IsFinalSubmit).ToColumn("IsFinalSubmit")
            .Map(m => m.IsTransfer).ToColumn("IsTransfer")
            .Map(m => m.IsConflictWithRetake).ToColumn("IsConflictWithRetake")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .DoNotMap(m => m.StudentRoll)
            .DoNotMap(m => m.StudentName)
            .DoNotMap(m => m.IsCurrentGrade)
            .DoNotMap(m => m.IsPreviousGrade)
            .DoNotMap(m => m.CourseHistoryId)
            .DoNotMap(m => m.CourseHistoryGrade)
            .DoNotMap(m => m.PreviousRecord)
            .Build();

            return mapper;
        }
        #endregion
    }
}
