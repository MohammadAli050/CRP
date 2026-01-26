using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLGenerateWorksheetRepository : IGenerateWorksheetRepository
    {
        Database db = null;

        private string sqlCustom = "PrepareRegistrationWorksheet";

        public int Insert(int studentId, int treeCalendarMasterID, int treeMasterID, int openCourse, int academicCalenderID, int programID)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlCustom);

                db = addParam(db, cmd, studentId, treeCalendarMasterID, treeMasterID, openCourse, academicCalenderID, programID, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ReturnValue");

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

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, int StudentId, int TreeCalendarMasterID, int TreeMasterID, int OpenCourse, int AcademicCalenderID, int ProgramID, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ReturnValue", DbType.Int32, Int32.MaxValue);
            }

            db.AddInParameter(cmd, "StudentId", DbType.Int32, StudentId);
            db.AddInParameter(cmd, "TreeCalendarMasterID", DbType.Int32, TreeCalendarMasterID);
            db.AddInParameter(cmd, "TreeMasterID", DbType.Int32, TreeMasterID);
            db.AddInParameter(cmd, "OpenCourse", DbType.Int32, OpenCourse);
            db.AddInParameter(cmd, "AcademicCalenderID", DbType.Int32, AcademicCalenderID);
            db.AddInParameter(cmd, "ProgramID", DbType.Int32, ProgramID);

            return db;
        }
        #endregion
    }
}
