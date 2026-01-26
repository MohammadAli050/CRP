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
    public partial class SQLScholarshipListRepository : IScholarshipListRepository
    {
        Database db = null;

        private string sqlInsert = "ScholarshipListInsert";
        private string sqlUpdate = "ScholarshipListUpdate";
        private string sqlDelete = "ScholarshipListDelete";
        private string sqlGetById = "ScholarshipListGetById";
        private string sqlGetAll = "ScholarshipListGetAll";
        private string sqlGetAllByAcaCalProgBatch = "ScholarshipListGetAllByAcaCalProgBatch";
        private string sqlGetAllByParameter = "ScholarshipListGetAllByParameter";
        private string sqlGetAllByAcaCalProg = "ScholarshipListGetAllByAcaCalProg";
        private string sqlGetStudentMeritListForScholarship = "StudentMeritListForScholarship";
        private string sqlGetStudentMeritListForScholarship2 = "StudentMeritListForScholarship2";

        public int Insert(ScholarshipList scholarshiplist)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, scholarshiplist, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "Id");

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

        public bool Update(ScholarshipList scholarshiplist)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, scholarshiplist, isInsert);

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

                db.AddInParameter(cmd, "Id", DbType.Int32, id);
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

        public ScholarshipList GetById(int id)
        {
            ScholarshipList _scholarshiplist = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ScholarshipList> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ScholarshipList>(sqlGetById, rowMapper);
                _scholarshiplist = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _scholarshiplist;
            }

            return _scholarshiplist;
        }

        public List<ScholarshipList> GetAll()
        {
            List<ScholarshipList> scholarshiplistList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ScholarshipList> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ScholarshipList>(sqlGetAll, mapper);
                IEnumerable<ScholarshipList> collection = accessor.Execute();

                scholarshiplistList = collection.ToList();
            }

            catch (Exception ex)
            {
                return scholarshiplistList;
            }

            return scholarshiplistList;
        }

        public List<ScholarshipList> GetAll(int acaCalId, string programCode, string fromBatch, string toBatch)
        {
            List<ScholarshipList> scholarshipList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ScholarshipList> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ScholarshipList>(sqlGetAllByAcaCalProgBatch, mapper);
                IEnumerable<ScholarshipList> collection = accessor.Execute(acaCalId, programCode, fromBatch, toBatch);

                scholarshipList = collection.ToList();
            }

            catch (Exception ex)
            {
                return scholarshipList;
            }

            return scholarshipList;
        }

        public List<ScholarshipList> GetAllByParameter(int acaCalId, string programCode, string fromBatch, string toBatch)
        {
            List<ScholarshipList> scholarshipList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ScholarshipList> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ScholarshipList>(sqlGetAllByParameter, mapper);
                IEnumerable<ScholarshipList> collection = accessor.Execute(acaCalId, programCode, fromBatch, toBatch);

                scholarshipList = collection.ToList();
            }

            catch (Exception ex)
            {
                return scholarshipList;
            }

            return scholarshipList;
        }

        public List<ScholarshipList> GetAllByAcaCalProg(int acaCalId, string programCode)
        {
            List<ScholarshipList> scholarshipList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ScholarshipList> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ScholarshipList>(sqlGetAllByAcaCalProg, mapper);
                IEnumerable<ScholarshipList> collection = accessor.Execute(acaCalId, programCode);

                scholarshipList = collection.ToList();
            }

            catch (Exception ex)
            {
                return scholarshipList;
            }

            return scholarshipList;
        }

        public List<StudentMeritListForScholarship> GetStudentMeritListForScholarship(int acaCalId, int programId, int batchId)
        {
            List<StudentMeritListForScholarship> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentMeritListForScholarship> mapper = GetStudentMeritListForScholarshipMaper();

                var accessor = db.CreateSprocAccessor<StudentMeritListForScholarship>(sqlGetStudentMeritListForScholarship, mapper);
                IEnumerable<StudentMeritListForScholarship> collection = accessor.Execute(acaCalId, programId, batchId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<StudentMeritListForScholarship> GetStudentMeritListForScholarship2(int acaCalId, int programId, int batchId, decimal registeredCredit)
        {
            List<StudentMeritListForScholarship> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentMeritListForScholarship> mapper = GetStudentMeritListForScholarshipMaper();

                var accessor = db.CreateSprocAccessor<StudentMeritListForScholarship>(sqlGetStudentMeritListForScholarship2, mapper);
                IEnumerable<StudentMeritListForScholarship> collection = accessor.Execute(acaCalId, programId, batchId, registeredCredit);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        #region Mapper

        private IRowMapper<StudentMeritListForScholarship> GetStudentMeritListForScholarshipMaper()
        {
            IRowMapper<StudentMeritListForScholarship> mapper = MapBuilder<StudentMeritListForScholarship>.MapAllProperties()

             .Map(m => m.Roll).ToColumn("Roll")
             .Map(m => m.FullName).ToColumn("FullName")
             .Map(m => m.Credit).ToColumn("Credit")
             .Map(m => m.BatchNO).ToColumn("BatchNO")
             .Map(m => m.CGPA).ToColumn("CGPA")
             .Map(m => m.GPA).ToColumn("GPA")
             .Map(m => m.TranscriptCredit).ToColumn("TranscriptCredit")
             .Map(m => m.TranscriptCGPA).ToColumn("TranscriptCGPA")
             .Map(m => m.TranscriptGPA).ToColumn("TranscriptGPA")

             .Build();

            return mapper;
        }

        private Database addParam(Database db, DbCommand cmd, ScholarshipList scholarshiplist, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, scholarshiplist.Id);
            }

            db.AddInParameter(cmd, "AcaCalId", DbType.Int32, scholarshiplist.AcaCalId);
            db.AddInParameter(cmd, "StudentId", DbType.Int32, scholarshiplist.StudentId);
            db.AddInParameter(cmd, "Roll", DbType.String, scholarshiplist.Roll);
            db.AddInParameter(cmd, "Name", DbType.String, scholarshiplist.Name);
            db.AddInParameter(cmd, "GPA", DbType.Decimal, scholarshiplist.GPA);
            db.AddInParameter(cmd, "Credit", DbType.Decimal, scholarshiplist.Credit);
            db.AddInParameter(cmd, "PassCredit", DbType.Decimal, scholarshiplist.PassCredit);
            db.AddInParameter(cmd, "RegisterCredit", DbType.Decimal, scholarshiplist.RegisterCredit);
            db.AddInParameter(cmd, "CalculateScholarship", DbType.String, scholarshiplist.CalculateScholarship);
            db.AddInParameter(cmd, "ManualScholarship", DbType.String, scholarshiplist.ManualScholarship);
            db.AddInParameter(cmd, "Attribute1", DbType.String, scholarshiplist.Attribute1);
            db.AddInParameter(cmd, "Attribute2", DbType.String, scholarshiplist.Attribute2);
            db.AddInParameter(cmd, "Attribute3", DbType.String, scholarshiplist.Attribute3);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, scholarshiplist.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, scholarshiplist.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, scholarshiplist.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, scholarshiplist.ModifiedDate);

            return db;
        }

        private IRowMapper<ScholarshipList> GetMaper()
        {
            IRowMapper<ScholarshipList> mapper = MapBuilder<ScholarshipList>.MapAllProperties()

            .Map(m => m.Id).ToColumn("Id")
            .Map(m => m.AcaCalId).ToColumn("AcaCalId")
            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.Name).ToColumn("Name")
            .Map(m => m.GPA).ToColumn("GPA")
            .Map(m => m.Credit).ToColumn("Credit")
            .Map(m => m.PassCredit).ToColumn("PassCredit")
            .Map(m => m.RegisterCredit).ToColumn("RegisterCredit")
            .Map(m => m.CalculateScholarship).ToColumn("CalculateScholarship")
            .Map(m => m.ManualScholarship).ToColumn("ManualScholarship")
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
    }
}
