using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;


namespace LogicLayer.DataLogic.SQLRepository
{
    public class SQLTranscriptRepository:ITranscriptRepository
    {
        Database db = null;
        //public DataTable tableB;
        private string sqlGetResultByStudentID = "GetTranscriptResult";
        private string sqlGetInfoByStudentID = "GetTranscriptStudentInfo";
        private string sqlGetTransferResult = "GetTranscriptTransferedResult";
        private string sqlGetWaiverResult = "GetTranscriptWaiverResult";

        public List<TranscriptResultDetails> GetResultByStudentId(string roll)
        {
            List<TranscriptResultDetails> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TranscriptResultDetails> mapper = GetResultMaper();

                var accessor = db.CreateSprocAccessor<TranscriptResultDetails>(sqlGetResultByStudentID, mapper);
                IEnumerable<TranscriptResultDetails> collection = accessor.Execute(roll);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<TranscriptStudentInfo> GetInfoByStudentId(string roll)
        {
            List<TranscriptStudentInfo> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TranscriptStudentInfo> mapper = GetInfoMaper();

                var accessor = db.CreateSprocAccessor<TranscriptStudentInfo>(sqlGetInfoByStudentID, mapper);
                IEnumerable<TranscriptStudentInfo> collection = accessor.Execute(roll);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<TranscriptTransferDetails> GetTransferResultStudentId(string id)
        {
            List<TranscriptTransferDetails> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TranscriptTransferDetails> mapper = GetTransferResultMaper();

                var accessor = db.CreateSprocAccessor<TranscriptTransferDetails>(sqlGetTransferResult, mapper);
                IEnumerable<TranscriptTransferDetails> collection = accessor.Execute(id);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }
        public List<TranscriptTransferDetails> GetWaiverResultStudentId(string id)
        {
            List<TranscriptTransferDetails> list = null;

            try
            {
                 db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TranscriptTransferDetails> mapper = GetTransferResultMaper();

                var accessor = db.CreateSprocAccessor<TranscriptTransferDetails>(sqlGetWaiverResult, mapper);
                IEnumerable<TranscriptTransferDetails> collection = accessor.Execute(id);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<TranscriptResultDetails> GetResultMaper()
        {
            IRowMapper<TranscriptResultDetails> mapper = MapBuilder<TranscriptResultDetails>.MapAllProperties()
           .Map(m => m.CGPA).ToColumn("CGPA")
           .Map(m => m.GPA).ToColumn("GPA")
           .Map(m=>m.Grade).ToColumn("ObtainedGrade")
           .Map(m => m.ObtainedGPA).ToColumn("ObtainedGPA")
           .Map(m => m.CourseTitles).ToColumn("Title")
           .Map(m => m.Credit).ToColumn("Credits")
           .Map(m => m.Trimesters).ToColumn("AcademicCalender")
           .Map(m => m.CourseCode).ToColumn("FormalCode")
           .Map(m => m.TriID).ToColumn("AcademicCalenderID")
           .Build();

            return mapper;
        }
        private IRowMapper<TranscriptTransferDetails> GetTransferResultMaper()
        {
            IRowMapper<TranscriptTransferDetails> mapper = MapBuilder<TranscriptTransferDetails>.MapAllProperties()
           .Map(m => m.CourseTitles).ToColumn("Title")
           .Map(m => m.Credit).ToColumn("Credits")
           .Map(m => m.CourseCode).ToColumn("FormalCode")
           .Map(m => m.UniversityName).ToColumn("UniversityName")
           .Build();

            return mapper;
        }
        private IRowMapper<TranscriptStudentInfo> GetInfoMaper()
        {
            IRowMapper<TranscriptStudentInfo> mapper = MapBuilder<TranscriptStudentInfo>.MapAllProperties()
           .Map(m => m.Name).ToColumn("FullName")
           .Map(m => m.Roll).ToColumn("Roll")
           .Map(m => m.DOB).ToColumn("dob")
           .Map(m => m.Degree).ToColumn("Degree")
           .Map(m => m.CompletionTri).ToColumn("IsCompleted")
           .Map(m => m.EnrollmentTri).ToColumn("EnrollmentTrimester")
           .Map(m => m.IssuedDate).ToColumn("IssueDate")
           .Map(m=>m.StudentID).ToColumn("StudentID")
           .Build();

            return mapper;
        }

    }
}
