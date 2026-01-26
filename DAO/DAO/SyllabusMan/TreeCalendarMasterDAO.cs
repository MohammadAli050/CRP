using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class TreeCalendarMaster_DAO : Base_DAO
    {
        #region Constants
        private const string SELECT = "SELECT "
                            + "[TreeCalendarMasterID], "
                            + "[TreeMasterID], "
                            + "[CalendarMasterID], "
                            + "[Name], "
                            + BASECOLUMNS
                            + "FROM [TreeCalendarMaster] ";
        #endregion

        #region Methods

        private static TreeCalendarMasterEntity Mapper(SQLNullHandler nullHandler)
        {
            TreeCalendarMasterEntity treeCalMas = new TreeCalendarMasterEntity();

            treeCalMas.Id = nullHandler.GetInt32("TreeCalendarMasterID");
            treeCalMas.TreeMasterID = nullHandler.GetInt32("TreeMasterID");
            treeCalMas.CalendarMasterID = nullHandler.GetInt32("CalendarMasterID");
            treeCalMas.Name = nullHandler.GetString("Name");
            treeCalMas.CreatorID = nullHandler.GetInt32("CreatedBy");
            treeCalMas.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            treeCalMas.ModifierID = nullHandler.GetInt32("ModifiedBy");
            treeCalMas.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");
            return treeCalMas;
        }

        private static TreeCalendarMasterEntity MapEntity(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            TreeCalendarMasterEntity treeCalMas = null;
            if (theReader.Read())
            {
                treeCalMas = new TreeCalendarMasterEntity();
                treeCalMas = Mapper(nullHandler);
            }

            return treeCalMas;
        }

        internal static TreeCalendarMasterEntity GetByTreeMasterID(int treeMasterID)
        {
            TreeCalendarMasterEntity treeCalMas = null;

            string command = SELECT
                            + "WHERE TreeMasterID = @TreeMasterID";

            Common.DAOParameters dps = new Common.DAOParameters();
            dps.AddParameter("@TreeMasterID", treeMasterID);

            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
            SqlDataReader theReader = QueryHandler.ExecuteSelectBatchQuery(command, ps);

            treeCalMas = MapEntity(theReader);
            theReader.Close();

            return treeCalMas;
        }
        #endregion
    }
}
