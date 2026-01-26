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
    public partial class SQLLogGeneralRepository : ILogGeneralRepository
    {
        Database db = null;

        private string sqlInsert = "LogGeneralInsert";
        private string sqlGetAll = "LogGeneralGetAll";
        private string sqlGetByDateRange = "LogGeneralGetByDate";
        public int Insert(LogGeneral loggeneral)
        {
            int id = 0;
           
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>("ConnectionLoG");
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, loggeneral);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "SL");

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

        public List<LogGeneral> GetAll()
        {
            List<LogGeneral> loggeneralList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>("ConnectionLoG");

                IRowMapper<LogGeneral> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<LogGeneral>(sqlGetAll, mapper);
                IEnumerable<LogGeneral> collection = accessor.Execute();

                loggeneralList = collection.ToList();
            }

            catch (Exception ex)
            {
                return loggeneralList;
            }

            return loggeneralList;
        }

        public List<LogGeneral> GetByDateRange(DateTime fromDate,DateTime toDate)
        {
            List<LogGeneral> loggeneralList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>("ConnectionLoG");

                IRowMapper<LogGeneral> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<LogGeneral>(sqlGetByDateRange, mapper);
                IEnumerable<LogGeneral> collection = accessor.Execute(fromDate,toDate);

                loggeneralList = collection.ToList();
            }

            catch (Exception ex)
            {
                return loggeneralList;
            }

            return loggeneralList;
        }
        
        
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, LogGeneral loggeneral)
        {
            db.AddOutParameter(cmd, "SL", DbType.Int32, Int32.MaxValue);
            db.AddInParameter(cmd, "AcademicCalenderCode", DbType.String, loggeneral.AcademicCalenderCode);
            db.AddInParameter(cmd, "AcademicCalenderName", DbType.String, loggeneral.AcademicCalenderName);
            db.AddInParameter(cmd, "DateTime", DbType.DateTime, loggeneral.DateTime);
            db.AddInParameter(cmd, "UserLoginId", DbType.String, loggeneral.UserLoginId);
            db.AddInParameter(cmd, "PageId", DbType.String, loggeneral.PageId);
            db.AddInParameter(cmd, "PageName", DbType.String, loggeneral.PageName);
            db.AddInParameter(cmd, "PageUrl", DbType.String, loggeneral.PageUrl);
            db.AddInParameter(cmd, "EventName", DbType.String, loggeneral.EventName);
            db.AddInParameter(cmd, "MessageType", DbType.String, loggeneral.MessageType);
            db.AddInParameter(cmd, "Message", DbType.String, loggeneral.Message);
            db.AddInParameter(cmd, "StudentRoll", DbType.String, loggeneral.StudentRoll);
            db.AddInParameter(cmd, "CourseFormalCode", DbType.String, loggeneral.CourseFormalCode);
            db.AddInParameter(cmd, "CourseVersionCode", DbType.String, loggeneral.CourseVersionCode);

            return db;
        }

        private IRowMapper<LogGeneral> GetMaper()
        {
            IRowMapper<LogGeneral> mapper = MapBuilder<LogGeneral>.MapAllProperties()

            .Map(m => m.AcademicCalenderCode).ToColumn("AcademicCalenderCode")
            .Map(m => m.AcademicCalenderName).ToColumn("AcademicCalenderName")
            .Map(m => m.DateTime).ToColumn("DateTime")
            .Map(m => m.UserLoginId).ToColumn("UserLoginId")
            .Map(m => m.PageId).ToColumn("PageId")
            .Map(m => m.PageName).ToColumn("PageName")
            .Map(m => m.PageUrl).ToColumn("PageUrl")
            .Map(m => m.EventName).ToColumn("EventName")
            .Map(m => m.MessageType).ToColumn("MessageType")
            .Map(m => m.Message).ToColumn("Message")
            .Map(m => m.StudentRoll).ToColumn("StudentRoll")
            .Map(m => m.CourseFormalCode).ToColumn("CourseFormalCode")
            .Map(m => m.CourseVersionCode).ToColumn("CourseVersionCode")

            .Build();

            return mapper;
        }
        #endregion

    }
}

