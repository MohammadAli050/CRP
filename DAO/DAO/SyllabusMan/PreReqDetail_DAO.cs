using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;

namespace DataAccess
{
    public class PreReqDetail_DAO : Base_DAO
    {
        #region Constants

        #region Columns

        private const string PREREQDETAILID = "PrerequisiteID";

        private const string PREREQMASID = "PrerequisiteMasterID";
        private const string PREREQMASID_PA = "@PrerequisiteMasterID";

        private const string NODE_COURSE_ID = "NodeCourseID";
        private const string NODE_COURSE_ID_PA = "@NodeCourseID";

        private const string PREREQ_NODECOURSE_ID = "PreReqNodeCourseID";
        private const string PREREQ_NODECOURSE_ID_PA = "@PreReqNodeCourseID";

        private const string OPERATORID = "OperatorID";
        private const string OPERATORID_PA = "@OperatorID";

        private const string OPERATORID_MINOCCURANCE = "OperatorIDMinOccurance";
        private const string OPERATORID_MINOCCURANCE_PA = "@OperatorIDMinOccurance";

        private const string REQCREDITS = "ReqCredits";
        private const string REQCREDITS_PA = "@ReqCredits";

        private const string NODEID = "NodeID";
        private const string NODEID_PA = "@NodeID";

        private const string PREREQNODEID = "PreReqNodeID";
        private const string PREREQNODEID_PA = "@PreReqNodeID";

        #endregion

        #region PKColumns

        private const string ALLCOLUMNS = "[" + PREREQDETAILID + "], "
                                        + "[" + PREREQMASID + "], "
                                        + "[" + NODE_COURSE_ID + "], "
                                        + "[" + PREREQ_NODECOURSE_ID + "], "
                                        + "[" + OPERATORID + "], "
                                        + "[" + OPERATORID_MINOCCURANCE + "], "
                                        + "[" + REQCREDITS + "], "
                                        + "[" + NODEID + "], "
                                        + "[" + PREREQNODEID + "], ";
        #endregion

        #region NOPKColumns

        private const string NOPKCOLUMNS = "[" + PREREQMASID + "], "
                                        + "[" + NODE_COURSE_ID + "], "
                                        + "[" + PREREQ_NODECOURSE_ID + "], "
                                        + "[" + OPERATORID + "], "
                                        + "[" + OPERATORID_MINOCCURANCE + "], "
                                        + "[" + REQCREDITS + "], "
                                        + "[" + NODEID + "], "
                                        + "[" + PREREQNODEID + "], ";
        #endregion

        #region TableName
        private const string TABLENAME = " [PrerequisiteDetail] ";
        #endregion

        #region Select Query

        private const string SELECT = "SELECT "
                    + ALLCOLUMNS
                    + BASECOLUMNS
                    + "FROM" + TABLENAME;

        #endregion

        #region Insert Query

        private const string INSERT = "INSERT INTO" + TABLENAME + "(" + NOPKCOLUMNS
                                                                     + BASECOLUMNS + ")"
                                                                     + "VALUES ( "
                                                                     + PREREQMASID_PA + ", "
                                                                     + NODE_COURSE_ID_PA + ", "
                                                                     + PREREQ_NODECOURSE_ID_PA + ", "
                                                                     + OPERATORID_PA + ", "
                                                                     + OPERATORID_MINOCCURANCE_PA + ", "
                                                                     + REQCREDITS_PA + ", "
                                                                     + NODEID_PA + ", "
                                                                     + PREREQNODEID_PA + ", "
                                                                     + CREATORID_PA + ", "
                                                                     + CREATEDDATE_PA + ", "
                                                                     + MODIFIERID_PA + ", "
                                                                     + MOIDFIEDDATE_PA + ")";

        #endregion

        #region Delete

        private const string DELETE = "DELETE FROM" + TABLENAME;

        #endregion

        #region Update Query

        private const string UPDATE = "UPDATE" + TABLENAME
                    + "SET [" + PREREQMASID + "] = " + PREREQMASID_PA + ", "
                    + "[" + NODE_COURSE_ID + "] = " + NODE_COURSE_ID_PA + ", "
                    + "[" + PREREQ_NODECOURSE_ID + "] = " + PREREQ_NODECOURSE_ID_PA + ", "
                    + "[" + OPERATORID + "] = " + OPERATORID_PA + ", "
                    + "[" + OPERATORID_MINOCCURANCE + "] = " + OPERATORID_MINOCCURANCE_PA + ", "
                    + "[" + REQCREDITS + "] = " + REQCREDITS_PA + ", "
                    + "[" + NODEID + "] = " + NODEID_PA + ","
                    + "[" + PREREQNODEID + "] = " + PREREQNODEID_PA + ", "
                    + "[" + CREATORID + "] = " + CREATORID_PA + ", "
                    + "[" + CREATEDDATE + "] = " + CREATEDDATE_PA + ", "
                    + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ", "
                    + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA;

        #endregion

        #endregion

        #region Methods

        private static PreReqDetailEntity Mapper(SQLNullHandler nullHandler)
        {
            PreReqDetailEntity obj = new PreReqDetailEntity();

            obj.Id = nullHandler.GetInt32(PREREQDETAILID);
            obj.PrereqMasterID = nullHandler.GetInt32(PREREQMASID);
            obj.NodeCourseID = nullHandler.GetInt32(NODE_COURSE_ID);
            obj.PreReqNodeCourseID = nullHandler.GetInt32(PREREQ_NODECOURSE_ID);
            obj.OperatorID = nullHandler.GetInt32(OPERATORID);
            obj.OperatorIDMinOccurance = nullHandler.GetInt32(OPERATORID_MINOCCURANCE);
            obj.ReqCredits = nullHandler.GetDecimal(REQCREDITS);
            obj.Node_ID = nullHandler.GetInt32(NODEID);
            obj.PreReqNodeID = nullHandler.GetInt32(PREREQNODEID);
            obj.CreatorID = nullHandler.GetInt32(CREATORID);//12
            obj.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);//13
            obj.ModifierID = nullHandler.GetInt32(MODIFIERID);//14
            obj.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);//15

            return obj;
        }
        private static PreReqDetailEntity MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            PreReqDetailEntity obj = null;
            if (theReader.Read())
            {
                obj = new PreReqDetailEntity();
                obj = Mapper(nullHandler);
            }

            return obj;
        }
        private static List<PreReqDetailEntity> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            List<PreReqDetailEntity> collection = null;
            while (theReader.Read())
            {
                if (collection == null)
                {
                    collection = new List<PreReqDetailEntity>();
                }
                PreReqDetailEntity obj = Mapper(nullHandler);
                collection.Add(obj);
            }
            return collection;
        }
        /// <summary>
        /// using store procedure(sp_GetPreReqDetails), prereqdetails are fetched for node_course
        /// </summary>
        /// <param name="nodeID"></param>
        /// <param name="courseID"></param>
        /// <param name="versionID"></param>
        /// <param name="progID"></param>
        /// <returns></returns>
        public static List<PreReqDetailEntity> GetPreReqDetails(int nodeID, int courseID, int versionID, int progID)
        {
            try
            {
                string command = "DECLARE @return_value int EXEC @return_value = [dbo].[sp_GetPreReqDetails] @nodeID = @ndID, @CourseID = @csID, @VersionID = @verID,@progID = @programID";

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter("@ndID", nodeID);
                dps.AddParameter("@csID", courseID);
                dps.AddParameter("@verID", versionID);
                dps.AddParameter("@programID", progID);

                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                SqlDataReader theReader = QueryHandler.ExecuteSelectQuery(command, ps); //MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

                List<PreReqDetailEntity> collection = MapCollection(theReader);
                theReader.Close();

                return collection;
            }
            catch (Exception ex)
            {
                //fixMe
                throw ex;
            }
        }
        /// <summary>
        /// using store procedure(sp_GetPreReqDetails), prereqdetails are fetched for node
        /// </summary>
        /// <param name="nodeID"></param>
        /// <param name="progID"></param>
        /// <returns></returns>
        public static List<PreReqDetailEntity> GetPreReqDetails(int nodeID, int progID)
        {
            try
            {
                string command = "DECLARE @return_value int EXEC @return_value = [dbo].[sp_GetPreReqDetails] @nodeID = @ndID, @CourseID = @csID, @VersionID = @verID,@progID = @programID";

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter("@ndID", nodeID);
                dps.AddParameter("@csID", -1);
                dps.AddParameter("@verID", -1);
                dps.AddParameter("@programID", progID);

                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                SqlDataReader theReader = QueryHandler.ExecuteSelectQuery(command, ps); //MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

                List<PreReqDetailEntity> collection = MapCollection(theReader);
                theReader.Close();

                return collection;
            }
            catch (Exception ex)
            {
                //fixMe
                throw ex;
            }
        }

        #endregion
    }
}
