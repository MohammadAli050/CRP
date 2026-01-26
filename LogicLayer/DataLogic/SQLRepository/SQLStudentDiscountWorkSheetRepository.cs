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
    public partial class SQLStudentDiscountWorkSheetRepository : IStudentDiscountWorkSheetRepository
    {
        Database db = null;

        private string sqlInsert = "StudentDiscountWorkSheetInsert";
        private string sqlUpdate = "StudentDiscountWorkSheetUpdate";
        private string sqlDelete = "StudentDiscountWorkSheetDeleteById";
        private string sqlGetById = "StudentDiscountWorkSheetGetById";
        private string sqlGetAll = "StudentDiscountWorkSheetGetAll";
        
        
        public int Insert(StudentDiscountWorkSheet studentDiscountWorkSheet)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, studentDiscountWorkSheet, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "StdDiscountWorksheetID");

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

        public bool Update(StudentDiscountWorkSheet studentDiscountWorkSheet)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, studentDiscountWorkSheet, isInsert);

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

                db.AddInParameter(cmd, "StdDiscountWorksheetID", DbType.Int32, id);
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

        public StudentDiscountWorkSheet GetById(int? id)
        {
            StudentDiscountWorkSheet _studentDiscountWorkSheet = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountWorkSheet> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountWorkSheet>(sqlGetById, rowMapper);
                _studentDiscountWorkSheet = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentDiscountWorkSheet;
            }

            return _studentDiscountWorkSheet;
        }

        public List<StudentDiscountWorkSheet> GetAll()
        {
            List<StudentDiscountWorkSheet> studentDiscountWorkSheetList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountWorkSheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountWorkSheet>(sqlGetAll, mapper);
                IEnumerable<StudentDiscountWorkSheet> collection = accessor.Execute();

                studentDiscountWorkSheetList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentDiscountWorkSheetList;
            }

            return studentDiscountWorkSheetList;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StudentDiscountWorkSheet studentDiscountWorkSheet, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "StdDiscountWorksheetID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "StdDiscountWorksheetID", DbType.Int32, studentDiscountWorkSheet.StdDiscountWorksheetID);
            }

            db.AddInParameter(cmd, "StudentID", DbType.Int32, studentDiscountWorkSheet.StudentID);
            db.AddInParameter(cmd, "ProgramID", DbType.Int32, studentDiscountWorkSheet.ProgramID);
            db.AddInParameter(cmd, "AcaCalID", DbType.Int32, studentDiscountWorkSheet.AcaCalID);
            db.AddInParameter(cmd, "AdmissionCalId", DbType.Int32, studentDiscountWorkSheet.AdmissionCalId);
            db.AddInParameter(cmd, "TotalCrRegInPreviousSession", DbType.Decimal, studentDiscountWorkSheet.TotalCrRegInPreviousSession);
            db.AddInParameter(cmd, "GPAinPreviousSession", DbType.Decimal, studentDiscountWorkSheet.GPAinPreviousSession);
            db.AddInParameter(cmd, "CGPAupToPreviousSession", DbType.Decimal, studentDiscountWorkSheet.CGPAupToPreviousSession);
            db.AddInParameter(cmd, "TotalCrRegInCurrentSession", DbType.Decimal, studentDiscountWorkSheet.TotalCrRegInCurrentSession);
            db.AddInParameter(cmd, "DiscountTypeId", DbType.Int32, studentDiscountWorkSheet.DiscountTypeId);
            db.AddInParameter(cmd, "DiscountPercentage", DbType.Decimal, studentDiscountWorkSheet.DiscountPercentage);
            db.AddInParameter(cmd, "Remarks", DbType.String, studentDiscountWorkSheet.Remarks);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, studentDiscountWorkSheet.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, studentDiscountWorkSheet.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, studentDiscountWorkSheet.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, studentDiscountWorkSheet.ModifiedDate);
            
            return db;
        }

        private IRowMapper<StudentDiscountWorkSheet> GetMaper()
        {
            IRowMapper<StudentDiscountWorkSheet> mapper = MapBuilder<StudentDiscountWorkSheet>.MapAllProperties()
            .Map(m => m.StdDiscountWorksheetID).ToColumn("StdDiscountWorksheetID")
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.AcaCalID).ToColumn("AcaCalID")
            .Map(m => m.AdmissionCalId).ToColumn("AdmissionCalId")
            .Map(m => m.TotalCrRegInPreviousSession).ToColumn("TotalCrRegInPreviousSession")
            .Map(m => m.GPAinPreviousSession).ToColumn("GPAinPreviousSession")
            .Map(m => m.CGPAupToPreviousSession).ToColumn("CGPAupToPreviousSession")
            .Map(m => m.TotalCrRegInCurrentSession).ToColumn("TotalCrRegInCurrentSession")
            .Map(m => m.DiscountTypeId).ToColumn("DiscountTypeId")
            .Map(m => m.DiscountPercentage).ToColumn("DiscountPercentage")
            .Map(m => m.Remarks).ToColumn("Remarks")
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
