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
    public partial class SQLStdCrsBillWorksheetRepository : IStdCrsBillWorksheetRepository
    {
        Database db = null;

        private string sqlInsert = "StdCrsBillWorksheetInsert";
        private string sqlUpdate = "StdCrsBillWorksheetUpdate";
        private string sqlDelete = "StdCrsBillWorksheetDeleteById";
        private string sqlGetById = "StdCrsBillWorksheetGetById";
        private string sqlGetAll = "StdCrsBillWorksheetGetAll";
        
        
        public int Insert(StdCrsBillWorksheet stdCrsBillWorksheet)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, stdCrsBillWorksheet, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "BillWorkSheetId");

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

        public bool Update(StdCrsBillWorksheet stdCrsBillWorksheet)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, stdCrsBillWorksheet, isInsert);

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

                db.AddInParameter(cmd, "BillWorkSheetId", DbType.Int32, id);
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

        public StdCrsBillWorksheet GetById(int? id)
        {
            StdCrsBillWorksheet _stdCrsBillWorksheet = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StdCrsBillWorksheet> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StdCrsBillWorksheet>(sqlGetById, rowMapper);
                _stdCrsBillWorksheet = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _stdCrsBillWorksheet;
            }

            return _stdCrsBillWorksheet;
        }

        public List<StdCrsBillWorksheet> GetAll()
        {
            List<StdCrsBillWorksheet> stdCrsBillWorksheetList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StdCrsBillWorksheet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StdCrsBillWorksheet>(sqlGetAll, mapper);
                IEnumerable<StdCrsBillWorksheet> collection = accessor.Execute();

                stdCrsBillWorksheetList = collection.ToList();
            }

            catch (Exception ex)
            {
                return stdCrsBillWorksheetList;
            }

            return stdCrsBillWorksheetList;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StdCrsBillWorksheet stdCrsBillWorksheet, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "BillWorkSheetId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "BillWorkSheetId", DbType.Int32, stdCrsBillWorksheet.BillWorkSheetId);
            }

            db.AddInParameter(cmd, "StudentId", DbType.Int32, stdCrsBillWorksheet.StudentId);
            db.AddInParameter(cmd, "CalCourseProgNodeID", DbType.Int32, stdCrsBillWorksheet.CalCourseProgNodeID);
            db.AddInParameter(cmd, "AcaCalSectionID", DbType.Int32, stdCrsBillWorksheet.AcaCalSectionID);
            db.AddInParameter(cmd, "SectionTypeId", DbType.Int32, stdCrsBillWorksheet.SectionTypeId);
            db.AddInParameter(cmd, "AcaCalId", DbType.Int32, stdCrsBillWorksheet.AcaCalId);
            db.AddInParameter(cmd, "CourseId", DbType.Int32, stdCrsBillWorksheet.CourseId);
            db.AddInParameter(cmd, "VersionId", DbType.Int32, stdCrsBillWorksheet.VersionId);
            db.AddInParameter(cmd, "CourseTypeId", DbType.Int32, stdCrsBillWorksheet.CourseTypeId);
            db.AddInParameter(cmd, "ProgramId", DbType.Int32, stdCrsBillWorksheet.ProgramId);
            db.AddInParameter(cmd, "RetakeNo", DbType.Int32, stdCrsBillWorksheet.RetakeNo);
            db.AddInParameter(cmd, "PreviousBestGrade", DbType.String, stdCrsBillWorksheet.PreviousBestGrade);
            db.AddInParameter(cmd, "FeeSetupId", DbType.Int32, stdCrsBillWorksheet.FeeSetupId);
            db.AddInParameter(cmd, "Fee", DbType.Decimal, stdCrsBillWorksheet.Fee);
            db.AddInParameter(cmd, "DiscountTypeId", DbType.Int32, stdCrsBillWorksheet.DiscountTypeId);
            db.AddInParameter(cmd, "DiscountPercentage", DbType.Decimal, stdCrsBillWorksheet.DiscountPercentage);
            db.AddInParameter(cmd, "Remarks", DbType.String, stdCrsBillWorksheet.Remarks);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, stdCrsBillWorksheet.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, stdCrsBillWorksheet.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, stdCrsBillWorksheet.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, stdCrsBillWorksheet.ModifiedDate);
            
            return db;
        }

        private IRowMapper<StdCrsBillWorksheet> GetMaper()
        {
            IRowMapper<StdCrsBillWorksheet> mapper = MapBuilder<StdCrsBillWorksheet>.MapAllProperties()
            .Map(m => m.BillWorkSheetId).ToColumn("BillWorkSheetId")
            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.CalCourseProgNodeID).ToColumn("CalCourseProgNodeID")
            .Map(m => m.AcaCalSectionID).ToColumn("AcaCalSectionID")
            .Map(m => m.SectionTypeId).ToColumn("SectionTypeId")
            .Map(m => m.AcaCalId).ToColumn("AcaCalId")
            .Map(m => m.CourseId).ToColumn("CourseId")
            .Map(m => m.VersionId).ToColumn("VersionId")
            .Map(m => m.CourseTypeId).ToColumn("CourseTypeId")
            .Map(m => m.ProgramId).ToColumn("ProgramId")
            .Map(m => m.RetakeNo).ToColumn("RetakeNo")
            .Map(m => m.PreviousBestGrade).ToColumn("PreviousBestGrade")
            .Map(m => m.FeeSetupId).ToColumn("FeeSetupId")
            .Map(m => m.Fee).ToColumn("Fee")
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
