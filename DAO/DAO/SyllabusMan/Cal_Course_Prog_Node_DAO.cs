using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class Cal_Course_Prog_Node_DAO : Base_DAO
    {
        #region Constants
        private const string SELECT = "SELECT "
                            + "[CalCorProgNodeID], "
                            + "[TreeCalendarDetailID], "
                            + "[OfferedByProgramID], "
                            + "[CourseID], "
                            + "[VersionID], "
                            + "[Node_CourseID], "
                            + "[NodeID], "
                            + "[NodeLinkName], "
                            + "[Priority], "
                            + "[Credits], "
                            + "IsMajorRelated, "
                            + BASECOLUMNS
                            + "FROM [CalCourseProgNode] ";

        private const string INSERT = "INSERT INTO [CalCourseProgNode] ("
            //+ "[CalCorProgNodeID], "
                            + "[TreeCalendarDetailID], "
                            + "[OfferedByProgramID], "
                            + "[CourseID], "
                            + "[VersionID], "
                            + "[Node_CourseID], "
                            + "[NodeID], "
                            + "[NodeLinkName], "
                            + "[Priority], "
                            + "[Credits], "
                            + "IsMajorRelated, "
                            + BASECOLUMNS
                            + ")"
                            + "VALUES ( "
            //+ "@CalCorProgNodeID, "
                            + "@TreeCalendarDetailID, "
                            + "@OfferedByProgramID, "
                            + "@CourseID, "
                            + "@VersionID, "
                            + "@Node_CourseID, "
                            + "@NodeID, "
                            + "@NodeLinkName, "
                            + "@Priority, "
                            + "@Credits, "
                            + "@IsMajorRelated, "
                            + CREATORID_PA + ", "
                            + CREATEDDATE_PA + ", "
                            + MODIFIERID_PA + ", "
                            + MOIDFIEDDATE_PA
                            + ")";

        private const string UPDATE = "UPDATE [CalCourseProgNode] "
                            + "SET TreeCalendarDetailID = @TreeCalendarDetailID, "
                            + "OfferedByProgramID = @OfferedByProgramID, "
                            + "CourseID = @CourseID, "
                            + "VersionID = @VersionID, "
                            + "Node_CourseID = @Node_CourseID, "
                            + "NodeID = @NodeID, "
                            + "NodeLinkName = @NodeLinkName, "
                            + "Priority = @Priority, "
                            + "Credits = @Credits, "
                            + "IsMajorRelated = @IsMajorRelated, "
                            + CREATORID + " = " + CREATORID_PA + ", "
                            + CREATEDDATE + " = " + CREATEDDATE_PA + ", "
                            + MODIFIERID + " = " + MODIFIERID_PA + ", "
                            + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE FROM [CalCourseProgNode] ";
        #endregion
        #region Methods
        private static Cal_Course_Prog_NodeEntity Mapper(SQLNullHandler nullHandler)
        {
            Cal_Course_Prog_NodeEntity calendarDistribution = new Cal_Course_Prog_NodeEntity();

            calendarDistribution.Id = nullHandler.GetInt32("CalCorProgNodeID");
            calendarDistribution.TreeCalendarDetailID = nullHandler.GetInt32("TreeCalendarDetailID");
            calendarDistribution.ProgramID = nullHandler.GetInt32("OfferedByProgramID");
            calendarDistribution.NodeID = nullHandler.GetInt32("NodeID");
            calendarDistribution.CourseID = nullHandler.GetInt32("CourseID");
            calendarDistribution.VersionID = nullHandler.GetInt32("VersionID");
            calendarDistribution.NodeCourseID = nullHandler.GetInt32("Node_CourseID");
            calendarDistribution.NodeLinkName = nullHandler.GetString("NodeLinkName");
            calendarDistribution.Priority = nullHandler.GetInt32("Priority");
            calendarDistribution.Credits = nullHandler.GetDecimal("Credits");
            calendarDistribution.IsMajorRelated = nullHandler.GetBoolean("IsMajorRelated");
            calendarDistribution.CreatorID = nullHandler.GetInt32("CreatedBy");
            calendarDistribution.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            calendarDistribution.ModifierID = nullHandler.GetInt32("ModifiedBy");
            calendarDistribution.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");
            return calendarDistribution;
        }
        private static List<Cal_Course_Prog_NodeEntity> MapEntities(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Cal_Course_Prog_NodeEntity> calendarDistributions = null;

            while (theReader.Read())
            {
                if (calendarDistributions == null)
                {
                    calendarDistributions = new List<Cal_Course_Prog_NodeEntity>();
                }
                Cal_Course_Prog_NodeEntity treeDetail = Mapper(nullHandler);
                calendarDistributions.Add(treeDetail);
            }

            return calendarDistributions;
        }

        internal static List<Cal_Course_Prog_NodeEntity> GetByTreeCalDet(int treeCalendarDetailID)
        {
            List<Cal_Course_Prog_NodeEntity> calendarDistributions = null;

            string command = SELECT
                            + "WHERE TreeCalendarDetailID = @TreeCalendarDetailID";

            Common.DAOParameters dps = new Common.DAOParameters();
            dps.AddParameter("@TreeCalendarDetailID", treeCalendarDetailID);

            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
            SqlDataReader theReader = QueryHandler.ExecuteSelectBatchQuery(command, ps);

            calendarDistributions = MapEntities(theReader);
            theReader.Close();

            return calendarDistributions;
        }
        internal static List<Cal_Course_Prog_NodeEntity> GetCalCourseProgNodeByStudent(int stdID)
        {
            try
            {
                List<Cal_Course_Prog_NodeEntity> ccpn = null;
                string cmd = "DECLARE @return_value int EXEC @return_value = [dbo].[sp_GetCalCrsProgNdByStd] @stdID = @StudentID SELECT 'Return Value' = @return_value";

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter("@StudentID", stdID);
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                SqlDataReader rd = QueryHandler.ExecuteSelectBatchQuery(cmd, ps);
                ccpn = MapEntities(rd);
                rd.Close();
                return ccpn;
            }
            catch (Exception ex)
            {
                //FixMe
                throw ex;
            }
        }

        #endregion
    }
}
