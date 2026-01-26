using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class TreeCalendarDetail_DAO : Base_DAO
    {
        #region Constants
        private const string SELECT = "SELECT "
                            + "[TreeCalendarDetailID], "
                            + "[TreeCalendarMasterID], "
                            + "[TreeMasterID], "
                            + "[CalendarMasterID], "
                            + "[CalendarDetailID], "
                            + BASECOLUMNS
                            + "FROM " + TABLENAME;
        private const string TABLENAME = "[TreeCalendarDetail] ";
        #endregion
        #region Methods

        private static TreeCalendarDetailEntity Mapper(SQLNullHandler nullHandler)
        {
            TreeCalendarDetailEntity treeDetail = new TreeCalendarDetailEntity();

            treeDetail.Id = nullHandler.GetInt32("TreeCalendarDetailID");
            treeDetail.TreeCalendarMasterID = nullHandler.GetInt32("TreeCalendarMasterID");
            treeDetail.TreeMasterID = nullHandler.GetInt32("TreeMasterID");
            treeDetail.CalendarMasterID = nullHandler.GetInt32("CalendarMasterID");
            treeDetail.CalendarDetailID = nullHandler.GetInt32("CalendarDetailID");
            treeDetail.CreatorID = nullHandler.GetInt32("CreatedBy");
            treeDetail.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            treeDetail.ModifierID = nullHandler.GetInt32("ModifiedBy");
            treeDetail.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");
            return treeDetail;
        }
        private static List<TreeCalendarDetailEntity> MapEntities(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<TreeCalendarDetailEntity> treeCalDetails = null;

            while (theReader.Read())
            {
                if (treeCalDetails == null)
                {
                    treeCalDetails = new List<TreeCalendarDetailEntity>();
                }
                TreeCalendarDetailEntity treeDetail = Mapper(nullHandler);
                treeCalDetails.Add(treeDetail);
            }

            return treeCalDetails;
        }


        internal static List<TreeCalendarDetailEntity> GetByTreeCalMaster(int tranCalMasterID)
        {
            List<TreeCalendarDetailEntity> treeDetails = null;

            try
            {
                string command = SELECT
                                    + "WHERE TreeCalendarMasterID = @TreeCalendarMasterID";

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter("@TreeCalendarMasterID", tranCalMasterID);

                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader theReader = QueryHandler.ExecuteSelectBatchQuery(command, ps);

                treeDetails = MapEntities(theReader);
                theReader.Close();
            }
            catch (Exception ex)
            {
                //FixMe
                throw ex;
            }

            return treeDetails;
        }

        #endregion
    }
}
