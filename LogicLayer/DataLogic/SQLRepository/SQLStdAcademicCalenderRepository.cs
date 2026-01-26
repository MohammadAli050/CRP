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
    partial class SQLStdAcademicCalenderRepository : IStdAcademicCalenderRepository
    {
        Database db = null;

        private string sqlInsert = "Std_AcademicCalenderInsert";
        private string sqlUpdate = "Std_AcademicCalenderUpdate";
        private string sqlDelete = "Std_AcademicCalenderDeleteById";
        private string sqlGetById = "Std_AcademicCalenderGetById";
        private string sqlGetAll = "Std_AcademicCalenderGetAll";


        public int Insert(StdAcademicCalender std_AcademicCalender)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, std_AcademicCalender, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "StdAcademicCalenderID");

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

        public bool Update(StdAcademicCalender std_AcademicCalender)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, std_AcademicCalender, isInsert);

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

                db.AddInParameter(cmd, "Std_AcademicCalenderID", DbType.Int32, id);
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

        public StdAcademicCalender GetById(int? id)
        {
            StdAcademicCalender _std_AcademicCalender = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StdAcademicCalender> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StdAcademicCalender>(sqlGetById, rowMapper);
                _std_AcademicCalender = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _std_AcademicCalender;
            }

            return _std_AcademicCalender;
        }

        public List<StdAcademicCalender> GetAll()
        {
            List<StdAcademicCalender> std_AcademicCalenderList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StdAcademicCalender> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StdAcademicCalender>(sqlGetAll, mapper);
                IEnumerable<StdAcademicCalender> collection = accessor.Execute();

                std_AcademicCalenderList = collection.ToList();
            }

            catch (Exception ex)
            {
                return std_AcademicCalenderList;
            }

            return std_AcademicCalenderList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StdAcademicCalender std_AcademicCalender, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "StdAcademicCalenderID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "StdAcademicCalenderID", DbType.Int32, std_AcademicCalender.StdAcademicCalenderID);
            }

            db.AddInParameter(cmd, "StdAcademicCalenderID", DbType.Int32, std_AcademicCalender.StdAcademicCalenderID);
            db.AddInParameter(cmd, "StudentID", DbType.Int32, std_AcademicCalender.StudentID);
            db.AddInParameter(cmd, "AcademicCalenderID", DbType.Int32, std_AcademicCalender.AcademicCalenderID);
            db.AddInParameter(cmd, "Description", DbType.String, std_AcademicCalender.Description);
            db.AddInParameter(cmd, "RegStatusType", DbType.Boolean, std_AcademicCalender.RegStatusType);
            db.AddInParameter(cmd, "CGPA", DbType.Decimal, std_AcademicCalender.CGPA);
            db.AddInParameter(cmd, "GPA", DbType.Decimal, std_AcademicCalender.GPA);
            db.AddInParameter(cmd, "TotalCreditsPerCalender", DbType.Decimal, std_AcademicCalender.TotalCreditsPerCalender);
            db.AddInParameter(cmd, "TotalCreditsComleted", DbType.Decimal, std_AcademicCalender.TotalCreditsComleted);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, std_AcademicCalender.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, std_AcademicCalender.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, std_AcademicCalender.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, std_AcademicCalender.ModifiedDate);

            return db;
        }

        private IRowMapper<StdAcademicCalender> GetMaper()
        {
            IRowMapper<StdAcademicCalender> mapper = MapBuilder<StdAcademicCalender>.MapAllProperties()
            .Map(m => m.StdAcademicCalenderID).ToColumn("StdAcademicCalenderID")
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.AcademicCalenderID).ToColumn("AcademicCalenderID")
            .Map(m => m.Description).ToColumn("Description")
            .Map(m => m.RegStatusType).ToColumn("RegStatusType")
            .Map(m => m.CGPA).ToColumn("CGPA")
            .Map(m => m.GPA).ToColumn("GPA")
            .Map(m => m.TotalCreditsPerCalender).ToColumn("TotalCreditsPerCalender")
            .Map(m => m.TotalCreditsComleted).ToColumn("TotalCreditsComleted")
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
