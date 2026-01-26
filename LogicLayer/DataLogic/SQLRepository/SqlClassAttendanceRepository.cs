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
using LogicLayer.BusinessObjects.RO;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SqlClassAttendanceRepository : IClassAttendanceRepository
    {
        Database db = null;

        private string sqlInsert = "ClassAttendanceInsert";
        private string sqlUpdate = "ClassAttendanceUpdate";
        private string sqlGetById = "ClassAttendanceGetAllByID";
        private string sqlGetAttendanceReportByClassSession = "ClassAttendanceReportByAcaCalSec";
        private string sqlByAcaCalSectionDate = "ClassAttendanceByAcaCalSectionIdDate";
        private string sqlGetByStudentIdAcaCalSecIdDate = "ClassAttendanceGetByStudentIdAcaCalSecIdDate";

        public int Insert(ClassAttendance _classAttendance)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, _classAttendance, isInsert);
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

        public bool Update(ClassAttendance _classAttendance)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, _classAttendance, isInsert);

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
                DbCommand cmd = db.GetStoredProcCommand("ClassAttendanceDeleteById");

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
        
        public ClassAttendance GetById(int id)
        {
            ClassAttendance _classAttendance = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ClassAttendance> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ClassAttendance>(sqlGetById, rowMapper);
                _classAttendance = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _classAttendance;
            }

            return _classAttendance;
        }

        public ClassAttendance GetByDateStudentIDAcaCalSecIDate(int studentId, int acaCalSecId, DateTime attendanceDate)
        {
            ClassAttendance _classAttendance = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ClassAttendance> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ClassAttendance>(sqlGetByStudentIdAcaCalSecIdDate, rowMapper);
                _classAttendance = accessor.Execute(studentId, acaCalSecId, attendanceDate).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _classAttendance;
            }

            return _classAttendance;
        }

        public List<rClassAttendance> GetAttendanceReportByAcaCalSection(int acaCalSectionId, DateTime attendanceFromDate, DateTime attendanceToDate)
        {
            List<rClassAttendance> _classAttendance = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rClassAttendance> mapper = GetAttendanceMaper();

                var accessor = db.CreateSprocAccessor<rClassAttendance>(sqlGetAttendanceReportByClassSession, mapper);
                IEnumerable<rClassAttendance> collection = accessor.Execute(acaCalSectionId, attendanceFromDate, attendanceToDate);

                _classAttendance = collection.ToList();
            }

            catch (Exception ex)
            {
                return _classAttendance;
            }

            return _classAttendance;
        }

        public List<ClassAttendance> GetAllByAcaCalSectionDate(int acaCalSectionId, DateTime attendanceDate)
        {
            List<ClassAttendance> _classAttendance = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ClassAttendance> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ClassAttendance>(sqlByAcaCalSectionDate, mapper);
                IEnumerable<ClassAttendance> collection = accessor.Execute(acaCalSectionId, attendanceDate);

                _classAttendance = collection.ToList();
            }

            catch (Exception ex)
            {
                return _classAttendance;
            }

            return _classAttendance;
        }

        public List<ClassAttendance> GetAttendanceByAcaCalSectionDate(int acaCalSectionId, DateTime attendanceDate)
        {
            List<ClassAttendance> _classAttendance = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ClassAttendance> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ClassAttendance>("ClassAttendanceGetByAcaCalSectionIdDate", mapper);
                IEnumerable<ClassAttendance> collection = accessor.Execute(acaCalSectionId, attendanceDate);

                _classAttendance = collection.ToList();
            }

            catch (Exception ex)
            {
                return _classAttendance;
            }

            return _classAttendance;
        }

        public List<ClassAttendance> GetAllByStudentIdCourseIdAcaCalId(int studentId, int courseId, int acaCalId)
        {
            List<ClassAttendance> _classAttendance = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ClassAttendance> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ClassAttendance>("ClassAttendanceGetAllByStudentIdCourseIdAcaCalId", mapper);
                IEnumerable<ClassAttendance> collection = accessor.Execute(studentId, courseId, acaCalId);

                _classAttendance = collection.ToList();
            }

            catch (Exception ex)
            {
                return _classAttendance;
            }

            return _classAttendance;
        }

        public List<rDateTime> GetAllDate()
        {
            return null;
        }

        public List<rClassAttendance> GetAttendanceReportDatewiseByAcaCalSection(int acaCalSectionId, DateTime attendanceFromDate, DateTime attendanceToDate)
        {
            List<rClassAttendance> _classAttendance = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rClassAttendance> mapper = GetAttendanceMaperDatewise();

                var accessor = db.CreateSprocAccessor<rClassAttendance>("ClassAttendanceReportDatewiseByAcaCalSec", mapper);
                IEnumerable<rClassAttendance> collection = accessor.Execute(acaCalSectionId, attendanceFromDate, attendanceToDate);

                _classAttendance = collection.ToList();
            }

            catch (Exception ex)
            {
                return _classAttendance;
            }

            return _classAttendance;
        }

        #region Mapper

        private Database addParam(Database db, DbCommand cmd, ClassAttendance _attendance, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ID", DbType.Int32, _attendance.ID);
            }

            db.AddInParameter(cmd, "StudentID", DbType.Int32, _attendance.StudentId);
            db.AddInParameter(cmd, "AcaCalSectionID", DbType.Int32, _attendance.AcaCalSectionID);
            db.AddInParameter(cmd, "AttendanceDate", DbType.DateTime, _attendance.AttendanceDate);
            db.AddInParameter(cmd, "Status", DbType.Int32, _attendance.StatusID);
            db.AddInParameter(cmd, "Comment", DbType.String, _attendance.Comment);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, _attendance.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, _attendance.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, _attendance.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, _attendance.ModifiedDate);
            
            return db;
        }

        private IRowMapper<ClassAttendance> GetMaper()
        {
            IRowMapper<ClassAttendance> mapper = MapBuilder<ClassAttendance>.MapAllProperties()
            .Map(m => m.ID).ToColumn("ID")
            .Map(m => m.StudentId).ToColumn("StudentID")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.Name).ToColumn("FullName")
            .Map(m => m.AcaCalSectionID).ToColumn("AcaCalSectionID")
            .Map(m => m.AttendanceDate).ToColumn("AttendanceDate")
            .Map(m => m.StatusID).ToColumn("Status")
            .Map(m => m.Comment).ToColumn("Comment")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            
            .Build();

            return mapper;
        }

        private IRowMapper<rClassAttendance> GetAttendanceMaper()
        {
            IRowMapper<rClassAttendance> mapper = MapBuilder<rClassAttendance>.MapAllProperties()
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.Name).ToColumn("FullName")
            .Map(m => m.PreasentCount).ToColumn("PreasentCount")
            .Map(m => m.AbsentCount).ToColumn("AbsentCount")
            .Map(m => m.LateCount).ToColumn("LateCount")
            .DoNotMap(m=>m.AttendanceDate)
            .DoNotMap(m=>m.Status)
            .Build();

            return mapper;
        }

        private IRowMapper<rClassAttendance> GetAttendanceMaperDatewise()
        {
            IRowMapper<rClassAttendance> mapper = MapBuilder<rClassAttendance>.MapAllProperties()
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.Name).ToColumn("FullName")
            .Map(m => m.AttendanceDate).ToColumn("AttendanceDate")
            .Map(m => m.Status).ToColumn("Status")
            .DoNotMap(m=>m.LateCount)
            .DoNotMap(m => m.PreasentCount)
            .DoNotMap(m => m.AbsentCount)
            .Build();

            return mapper;
        }
        #endregion
    }
}
