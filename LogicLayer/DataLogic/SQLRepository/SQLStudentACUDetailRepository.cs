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
    public class SQLStudentACUDetailRepository : IStudentACUDetailRepository
    {
        #region Variable
        Database db = null;

        private string sqlInsert = "Student_ACUDetailInsert";
        private string sqlUpdate = "StudentACUDetailUpdate";
        private string sqlDelete = "Student_ACUDetailDeleteById";
        private string sqlGetById = "StudentACUDetailGetById";
        private string sqlGetAll = "StudentACUDetailGetAll";
        private string sqlUpdateGPA = "Student_ACUDetailUpdateByAcaCalRoll";
        private string sqlGetLatestCGPAByStudentId = "StudentACUDetailGetLatestCGPAByStudentId";
        private string sqlCalculateGPAandCGPAByRoll = "Student_ACUDetail_Calculate_GPAandCGPAByRoll";
        private string sqlCalculateGPAandCGPAByBatch = "Student_ACUDetail_Calculate_GPAandCGPAByBatch";
        private string sqlCalculateGPAandCGPABulk = "Student_ACUDetail_Calculate_GPAandCGPA_Bulk";
        private string sqlCalculateGpaCgpa = "StudentACUDetailCalculateGPACGPA";
        private string sqlGetAllAcaCalProgramBatchStudent = "StudentACUDetailGetAllAcaCalProgramBatchStudent";
        #endregion

        #region Data Access Method

        public int Insert(StudentACUDetail studentacudetail)
        {
            int id = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = AddParam(db, cmd, studentacudetail, true);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "StdACUDetailID");

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

        public bool Update(StudentACUDetail studentacudetail)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = AddParam(db, cmd, studentacudetail, false);

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

        public List<StudentACUDetail> GetAll(int studentId)
        {
            List<StudentACUDetail> studentacudetailList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentACUDetail> mapper = getMapper();

                var accessor = db.CreateSprocAccessor<StudentACUDetail>(sqlGetAll, mapper);
                IEnumerable<StudentACUDetail> collection = accessor.Execute(studentId);

                studentacudetailList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentacudetailList;
            }

            return studentacudetailList;
        }

        public bool Delete(int id)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlDelete);

                db.AddInParameter(cmd, "StdACUDetailID", DbType.Int32, id);

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

        public StudentACUDetail GetById(int id)
        {
            StudentACUDetail _studentacudetail = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentACUDetail> rowMapper = getMapper();

                var accessor = db.CreateSprocAccessor<StudentACUDetail>(sqlGetById, rowMapper);
                _studentacudetail = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentacudetail;
            }

            return _studentacudetail;
        }

        public int UpdateByAcaCalRoll(int studentId, int acaCalId)
        {
            int id = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdateGPA);

                db.AddOutParameter(cmd, "result", DbType.Int32, Int32.MaxValue);
                db.AddInParameter(cmd, "studentId", DbType.Int32, studentId);
                db.AddInParameter(cmd, "acaCalId", DbType.Int32, acaCalId);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "result");

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

        public StudentACUDetail GetLatestCGPAByStudentId(int studentId)
        {
            StudentACUDetail _studentacudetail = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentACUDetail> rowMapper = getMapper();
                
                var accessor = db.CreateSprocAccessor<StudentACUDetail>(sqlGetLatestCGPAByStudentId, rowMapper);
                _studentacudetail = accessor.Execute(studentId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentacudetail;
            }

            return _studentacudetail;
        }

        public int Calculate_GPAandCGPAByRoll(string roll)
        {
            int result = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlCalculateGPAandCGPAByRoll);

                db.AddOutParameter(cmd, "ProcessResult", DbType.Int32, Int32.MaxValue);
                db.AddInParameter(cmd, "roll", DbType.String, roll);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ProcessResult");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out result);
                }
            }
            catch (Exception ex)
            {
                result = 0;
            }

            return result;
        }

        public int Calculate_GPAandCGPAByBatch(string batch)
        {
            int result = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlCalculateGPAandCGPAByBatch);

                db.AddOutParameter(cmd, "ProcessResult", DbType.Int32, Int32.MaxValue);
                db.AddInParameter(cmd, "roll", DbType.String, batch);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ProcessResult");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out result);
                }
            }
            catch (Exception ex)
            {
                result = 0;
            }

            return result;
        }

        public int Calculate_GPAandCGPA_Bulk()
        {
            int result = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlCalculateGPAandCGPABulk);

                db.AddOutParameter(cmd, "ProcessResult", DbType.Int32, Int32.MaxValue);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ProcessResult");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out result);
                }
            }
            catch (Exception ex)
            {
                result = 0;
            }

            return result;
        }

        public string Calculate_GpaCgpa(int acaCalId, int programId, int batchId, string studentId)
        {
            string result = "0-0";

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlCalculateGpaCgpa);

                db.AddOutParameter(cmd, "ProcessResult", DbType.String, Int32.MaxValue);
                db.AddInParameter(cmd, "AcaCalId", DbType.Int32, acaCalId);
                db.AddInParameter(cmd, "ProgramId", DbType.Int32, programId);
                db.AddInParameter(cmd, "BatchId", DbType.Int32, batchId);
                db.AddInParameter(cmd, "StudentId", DbType.String, studentId);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ProcessResult");

                if (obj != null)
                {
                    result = obj.ToString();
                }
            }
            catch (Exception ex)
            {
                result = "0-0";
            }

            return result;
        }

        public List<StudentACUDetail> GetAllByAcaCalProgramBatchStudent(int acaCalId, int programId, int batchId, string studentId)
        {
            List<StudentACUDetail> studentacudetailList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentACUDetail> mapper = getCustomMapper();

                var accessor = db.CreateSprocAccessor<StudentACUDetail>(sqlGetAllAcaCalProgramBatchStudent, mapper);
                IEnumerable<StudentACUDetail> collection = accessor.Execute(acaCalId, programId, batchId, studentId);

                studentacudetailList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentacudetailList;
            }

            return studentacudetailList;
        }

        public List<StudentACUDetail> GetAllByAcaCalProgramBatchStudentForRemarks(int semesterNo, int programId, int batchId, string studentId)
        {
            List<StudentACUDetail> studentacudetailList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentACUDetail> mapper = getCustomMapper();

                var accessor = db.CreateSprocAccessor<StudentACUDetail>("StudentACUDetailGetAllAcaCalProgramBatchStudentForRemarks", mapper);
                IEnumerable<StudentACUDetail> collection = accessor.Execute(semesterNo, programId, batchId, studentId);

                studentacudetailList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentacudetailList;
            }

            return studentacudetailList;
        }

        public List<rStudentMeritList> GetMeritListByProgramSessionBatch(int programId, int acaCalId, int batchId) 
        {
            List<rStudentMeritList> rStudentMeritList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentMeritList> mapper = GetStudentMeritListMapper();

                var accessor = db.CreateSprocAccessor<rStudentMeritList>("StudentACUDetailMeritListByProgramSessionBatch", mapper);
                IEnumerable<rStudentMeritList> collection = accessor.Execute(programId, acaCalId, batchId);

                rStudentMeritList = collection.ToList();
            }

            catch (Exception ex)
            {
                return rStudentMeritList;
            }

            return rStudentMeritList;
        }        
        #endregion

        #region Mapper

        private IRowMapper<StudentACUDetail> getMapper()
        {
            IRowMapper<StudentACUDetail> mapper = MapBuilder<StudentACUDetail>.MapAllProperties()
            .Map(m => m.StdACUDetailID).ToColumn("StdACUDetailID")
            .Map(m => m.StdAcademicCalenderID).ToColumn("StdAcademicCalenderID")
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.StatusTypeID).ToColumn("StatusTypeID")
            .Map(m => m.SchSetUpID).ToColumn("SchSetUpID")
            .Map(m => m.Credit).ToColumn("Credit")
            .Map(m => m.CGPA).ToColumn("CGPA")
            .Map(m => m.GPA).ToColumn("GPA")
            .Map(m => m.Description).ToColumn("Description")
            .Map(m => m.Remarks).ToColumn("Remarks")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .DoNotMap(m => m.Semester)
            .DoNotMap(m => m.Roll)
            .DoNotMap(m => m.Name)
            .Build();
             
            return mapper;
        }

        private Database AddParam(Database db, DbCommand cmd, StudentACUDetail studentacudetail, bool isInsert)
        {
            if (isInsert)
                db.AddOutParameter(cmd, "StdACUDetailID", DbType.Int32, Int32.MaxValue);
            else
                db.AddInParameter(cmd, "StdACUDetailID", DbType.Int32, studentacudetail.StdACUDetailID);

            db.AddInParameter(cmd, "StdAcademicCalenderID", DbType.Int32, studentacudetail.StdAcademicCalenderID);
            db.AddInParameter(cmd, "StudentID", DbType.Int32, studentacudetail.StudentID);
            db.AddInParameter(cmd, "StatusTypeID", DbType.Int32, studentacudetail.StatusTypeID);
            db.AddInParameter(cmd, "SchSetUpID", DbType.Int32, studentacudetail.SchSetUpID);
            db.AddInParameter(cmd, "Credit", DbType.Decimal, studentacudetail.Credit);
            db.AddInParameter(cmd, "CGPA", DbType.Decimal, studentacudetail.CGPA);
            db.AddInParameter(cmd, "GPA", DbType.Decimal, studentacudetail.GPA);
            db.AddInParameter(cmd, "Description", DbType.String, studentacudetail.Description);
            db.AddInParameter(cmd, "Remarks", DbType.String, studentacudetail.Remarks);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, studentacudetail.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, studentacudetail.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, studentacudetail.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, studentacudetail.ModifiedDate);

            return db;
        }

        private IRowMapper<StudentACUDetail> getCustomMapper()
        {
            IRowMapper<StudentACUDetail> mapper = MapBuilder<StudentACUDetail>.MapAllProperties()
            .Map(m => m.StdACUDetailID).ToColumn("StdACUDetailID")
            .Map(m => m.StdAcademicCalenderID).ToColumn("StdAcademicCalenderID")
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.StatusTypeID).ToColumn("StatusTypeID")
            .Map(m => m.SchSetUpID).ToColumn("SchSetUpID")
            .Map(m => m.Credit).ToColumn("Credit")
            .Map(m => m.CGPA).ToColumn("CGPA")
            .Map(m => m.GPA).ToColumn("GPA")
            .Map(m => m.TranscriptCredit).ToColumn("TranscriptCredit")
            .Map(m => m.TranscriptCGPA).ToColumn("TranscriptCGPA")
            .Map(m => m.TranscriptGPA).ToColumn("TranscriptGPA")
            .Map(m => m.IsAllGradeSubmitted).ToColumn("IsAllGradeSubmitted")
            .Map(m => m.Description).ToColumn("Description")
            .Map(m => m.Remarks).ToColumn("Remarks")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Map(m => m.Semester).ToColumn("Semester")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.Name).ToColumn("Name")
            .Build();

            return mapper;
        }

        private IRowMapper<rStudentMeritList> GetStudentMeritListMapper()
        {
            IRowMapper<rStudentMeritList> mapper = MapBuilder<rStudentMeritList>.MapAllProperties()
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.StudentName).ToColumn("StudentName")
            .Map(m => m.GPA).ToColumn("GPA")
            .Map(m => m.TranscriptCGPA).ToColumn("TranscriptCGPA")
            .Map(m => m.Position).ToColumn("Position")
            .Build();

            return mapper;
        }

        #endregion
    }
}
