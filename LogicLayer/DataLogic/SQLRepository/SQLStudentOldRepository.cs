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
    partial class SQLStudentOldRepository : IStudentOldRepository
    {
        Database db = null;

        private string sqlInsert = "Student_OldInsert";
        private string sqlUpdate = "Student_OldUpdate";
        private string sqlDelete = "Student_OldDeleteById";
        private string sqlGetById = "Student_OldGetById";
        private string sqlGetAll = "Student_OldGetAll";


        public int Insert(StudentOld student_Old)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, student_Old, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "StudentID");

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

        public bool Update(StudentOld student_Old)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, student_Old, isInsert);

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

                db.AddInParameter(cmd, "StudentID", DbType.Int32, id);
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

        public StudentOld GetById(int? id)
        {
            StudentOld _student_Old = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentOld> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentOld>(sqlGetById, rowMapper);
                _student_Old = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _student_Old;
            }

            return _student_Old;
        }

        public List<StudentOld> GetAll()
        {
            List<StudentOld> student_OldList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentOld> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentOld>(sqlGetAll, mapper);
                IEnumerable<StudentOld> collection = accessor.Execute();

                student_OldList = collection.ToList();
            }

            catch (Exception ex)
            {
                return student_OldList;
            }

            return student_OldList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StudentOld student_Old, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "StudentID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "StudentID", DbType.Int32, student_Old.StudentID);
            }

            db.AddInParameter(cmd, "Roll", DbType.Int32, student_Old.Roll);
            db.AddInParameter(cmd, "Prefix", DbType.String, student_Old.Prefix);
            db.AddInParameter(cmd, "FirstName", DbType.Int32, student_Old.FirstName);
            db.AddInParameter(cmd, "MiddleName", DbType.String, student_Old.MiddleName);
            db.AddInParameter(cmd, "LastName", DbType.String, student_Old.LastName);
            db.AddInParameter(cmd, "NickOrOtherName", DbType.String, student_Old.NickOrOtherName);
            db.AddInParameter(cmd, "DOB", DbType.DateTime, student_Old.DOB);
            db.AddInParameter(cmd, "Gender", DbType.String, student_Old.Gender);
            db.AddInParameter(cmd, "MatrialStatus", DbType.Int32, student_Old.MatrialStatus);
            db.AddInParameter(cmd, "BloodGroup", DbType.Int32, student_Old.BloodGroup);
            db.AddInParameter(cmd, "ReligionID", DbType.Int32, student_Old.ReligionID);
            db.AddInParameter(cmd, "NationalityID", DbType.Int32, student_Old.NationalityID);
            db.AddInParameter(cmd, "PhotoPath", DbType.String, student_Old.PhotoPath);
            db.AddInParameter(cmd, "ProgramID", DbType.Int32, student_Old.ProgramID);
            db.AddInParameter(cmd, "TotalDue", DbType.Decimal, student_Old.TotalDue);
            db.AddInParameter(cmd, "TotalPaid", DbType.Decimal, student_Old.TotalPaid);
            db.AddInParameter(cmd, "Balance", DbType.Decimal, student_Old.Balance);
            db.AddInParameter(cmd, "TuitionSetUpID", DbType.Int32, student_Old.TuitionSetUpID);
            db.AddInParameter(cmd, "WaiverSetUpID", DbType.Int32, student_Old.WaiverSetUpID);
            db.AddInParameter(cmd, "DiscountSetUpID", DbType.Int32, student_Old.DiscountSetUpID);
            db.AddInParameter(cmd, "RelationTypeID", DbType.Int32, student_Old.RelationTypeID);
            db.AddInParameter(cmd, "RelativeID", DbType.Int32, student_Old.RelativeID);
            db.AddInParameter(cmd, "TreeMasterID", DbType.Int32, student_Old.TreeMasterID);
            db.AddInParameter(cmd, "Major1NodeID", DbType.Int32, student_Old.Major1NodeID);
            db.AddInParameter(cmd, "Major2NodeID", DbType.Int32, student_Old.Major2NodeID);
            db.AddInParameter(cmd, "Major3NodeID", DbType.Int32, student_Old.Major3NodeID);
            db.AddInParameter(cmd, "Minor1NodeID", DbType.Int32, student_Old.Minor1NodeID);
            db.AddInParameter(cmd, "Minor2NodeID", DbType.Int32, student_Old.Minor2NodeID);
            db.AddInParameter(cmd, "Minor3NodeID", DbType.Int32, student_Old.Minor3NodeID);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, student_Old.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, student_Old.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, student_Old.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, student_Old.ModifiedDate);

            return db;
        }

        private IRowMapper<StudentOld> GetMaper()
        {
            IRowMapper<StudentOld> mapper = MapBuilder<StudentOld>.MapAllProperties()
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.Prefix).ToColumn("Prefix")
            .Map(m => m.FirstName).ToColumn("FirstName")
            .Map(m => m.MiddleName).ToColumn("MiddleName")
            .Map(m => m.LastName).ToColumn("LastName")
            .Map(m => m.NickOrOtherName).ToColumn("NickOrOtherName")
            .Map(m => m.DOB).ToColumn("DOB")
            .Map(m => m.Gender).ToColumn("Gender")
            .Map(m => m.MatrialStatus).ToColumn("MatrialStatus")
            .Map(m => m.BloodGroup).ToColumn("BloodGroup")
            .Map(m => m.ReligionID).ToColumn("ReligionID")
            .Map(m => m.NationalityID).ToColumn("NationalityID")
            .Map(m => m.PhotoPath).ToColumn("PhotoPath")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.TotalDue).ToColumn("TotalDue")
            .Map(m => m.TotalPaid).ToColumn("TotalPaid")
            .Map(m => m.Balance).ToColumn("Balance")
            .Map(m => m.TuitionSetUpID).ToColumn("TuitionSetUpID")
            .Map(m => m.WaiverSetUpID).ToColumn("WaiverSetUpID")
            .Map(m => m.DiscountSetUpID).ToColumn("DiscountSetUpID")
            .Map(m => m.RelationTypeID).ToColumn("RelationTypeID")
            .Map(m => m.RelativeID).ToColumn("RelativeID")
            .Map(m => m.TreeMasterID).ToColumn("TreeMasterID")
            .Map(m => m.Major1NodeID).ToColumn("Major1NodeID")
            .Map(m => m.Major2NodeID).ToColumn("Major2NodeID")
            .Map(m => m.Major3NodeID).ToColumn("Major3NodeID")
            .Map(m => m.Minor1NodeID).ToColumn("Minor1NodeID")
            .Map(m => m.Minor2NodeID).ToColumn("Minor2NodeID")
            .Map(m => m.Minor3NodeID).ToColumn("Minor3NodeID")
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
