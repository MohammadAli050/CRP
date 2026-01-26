using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace BussinessObject
{
    [Serializable]
    public class AssignStudentToCalCrsProgNode : Base
    {
        #region DBColumns
        //[ID] [int] NOT NULL,
        //[StudentID] [int] NOT NULL,
        //[CalCourseProgNodeID] [int] NOT NULL,
        //[IsCompleted] [bit] NOT NULL,
        //[OriginalCalID] [int] NULL,
        //[IsAutoAssign] [bit] NOT NULL,
        //[IsAutoOpen] [bit] NOT NULL,
        //[Isrequisitioned] [bit] NOT NULL,
        //[IsMandatory] [bit] NOT NULL,
        //[IsManualOpen] [bit] NOT NULL,
        //[CreatedBy] [int] NOT NULL,
        //[CreatedDate] [datetime] NOT NULL,
        //[ModifiedBy] [int] NULL,
        //[ModifiedDate] [datetime] NULL
        #endregion

        #region Variables

        private int _studentID = 0;

        public int StudentID
        {
            get { return _studentID; }
            set { _studentID = value; }
        }
        private int _calCourseProgNodeID = 0;

        public int CalCourseProgNodeID
        {
            get { return _calCourseProgNodeID; }
            set { _calCourseProgNodeID = value; }
        }
        private bool _isCompleted = false;

        public bool IsCompleted
        {
            get { return _isCompleted; }
            set { _isCompleted = value; }
        }
        private int _originalCalID = 0;

        public int OriginalCalID
        {
            get { return _originalCalID; }
            set { _originalCalID = value; }
        }
        private bool _isAutoAssign = false;

        public bool IsAutoAssign
        {
            get { return _isAutoAssign; }
            set { _isAutoAssign = value; }
        }
        private bool _isAutoOpen = false;

        public bool IsAutoOpen
        {
            get { return _isAutoOpen; }
            set { _isAutoOpen = value; }
        }
        private bool _isRequisitioned = false;

        public bool IsRequisitioned
        {
            get { return _isRequisitioned; }
            set { _isRequisitioned = value; }
        }
        private bool _isMandatory = false;

        public bool IsMandatory
        {
            get { return _isMandatory; }
            set { _isMandatory = value; }
        }
        private bool _isManualOpen = false;

        public bool IsManualOpen
        {
            get { return _isManualOpen; }
            set { _isManualOpen = value; }
        }

        #endregion

        #region Constructor
        public AssignStudentToCalCrsProgNode()
            : base()
        {
            _studentID = 0;
            _calCourseProgNodeID = 0;
            _isCompleted = false;
            _originalCalID = 0;
            _isAutoAssign = false;
            _isAutoOpen = false;
            _isRequisitioned = false;
            _isMandatory = false;
            _isManualOpen = false;
        }
        #endregion

        #region Constants
        #region column constants

        private const string ID = "ID";

        private const string STUDENTID = "StudentID";
        private const string STUDENTID_PA = "@StudentID";

        private const string CALCOURSEPROGNODEID = "CalCourseProgNodeID";
        private const string CALCOURSEPROGNODEID_PA = "@CalCourseProgNodeID";

        private const string ISCOMPLETED = "IsCompleted";
        private const string ISCOMPLETED_PA = "@IsCompleted";

        private const string ORIGINALCALID = "OriginalCalID";
        private const string ORIGINALCALID_PA = "@OriginalCalID";

        private const string ISAUTOASSIGN = "IsAutoAssign";
        private const string ISAUTOASSIGN_PA = "@IsAutoAssign";

        private const string ISAUTOOPEN = "IsAutoOpen";
        private const string ISAUTOOPEN_PA = "@IsAutoOpen";

        private const string ISREQUISITIONED = "Isrequisitioned";
        private const string ISREQUISITIONED_PA = "@Isrequisitioned";

        private const string ISMANDATORY = "IsMandatory";
        private const string ISMANDATORY_PA = "@IsMandatory";

        private const string ISMANUALOPEN = "IsManualOpen";
        private const string ISMANUALOPEN_PA = "@IsManualOpen";

        #endregion

        #region PKColumns

        private const string ALLCOLUMNS = "[" + ID + "], "
                                        + "[" + STUDENTID + "], "
                                        + "[" + CALCOURSEPROGNODEID + "], "
                                        + "[" + ISCOMPLETED + "], "
                                        + "[" + ORIGINALCALID + "], "
                                        + "[" + ISAUTOASSIGN + "], "
                                        + "[" + ISAUTOOPEN + "], "
                                        + "[" + ISREQUISITIONED + "], "
                                        + "[" + ISMANDATORY + "], "
                                        + "[" + ISMANUALOPEN + "], ";
        #endregion

        #region NonPKColumns

        private const string NONPKCOLUMNS = "[" + STUDENTID + "], "
                                        + "[" + CALCOURSEPROGNODEID + "], "
                                        + "[" + ISCOMPLETED + "], "
                                        + "[" + ORIGINALCALID + "], "
                                        + "[" + ISAUTOASSIGN + "], "
                                        + "[" + ISAUTOOPEN + "], "
                                        + "[" + ISREQUISITIONED + "], "
                                        + "[" + ISMANDATORY + "], "
                                        + "[" + ISMANUALOPEN + "], ";

        #endregion

        #region tablemane
        private const string TABLENAME = " [StudentCalCourseProgNode] ";
        #endregion

        #region Select query

        private const string SELECT = "SELECT "
                    + ALLCOLUMNS
                    + BASECOLUMNS
                    + "FROM" + TABLENAME;
        #endregion

        #region INSERT query
        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                     + NONPKCOLUMNS
                     + BASEMUSTCOLUMNS + ")"
                     + "VALUES ( "
                     + STUDENTID_PA + ", "
                    + CALCOURSEPROGNODEID_PA + ", "
                    + ISCOMPLETED_PA + ", "
                    + ORIGINALCALID_PA + ", "
                    + ISAUTOASSIGN_PA + ", "
                    + ISAUTOOPEN_PA + ", "
                    + ISREQUISITIONED_PA + ", "
                    + ISMANDATORY_PA + ", "
                    + ISMANUALOPEN_PA + ", "
                    + CREATORID_PA + ", "
                    + CREATEDDATE_PA + ")";
        #endregion

        #region UPDATE query
        private const string UPDATE = " UPDATE " + TABLENAME
                    + "SET [" + STUDENTID + "] = " + STUDENTID_PA + ", "
                    + "[" + CALCOURSEPROGNODEID + "] = " + CALCOURSEPROGNODEID_PA + ", "
                    + "[" + ISCOMPLETED + "] = " + ISCOMPLETED_PA + ", "
                    + "[" + ORIGINALCALID + "] = " + ORIGINALCALID_PA + ", "
                    + "[" + ISAUTOASSIGN + "] = " + ISAUTOASSIGN_PA + ", "
                    + "[" + ISAUTOOPEN + "] = " + ISAUTOOPEN_PA + ", "
                    + "[" + ISREQUISITIONED + "] = " + ISREQUISITIONED_PA + ", "
                    + "[" + ISMANDATORY + "] = " + ISMANDATORY_PA + ", "
                    + "[" + ISMANUALOPEN + "] = " + ISMANUALOPEN_PA + ", "
                    + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ", "
                    + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA;
        #endregion

        #region Delete Query
        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion
        #endregion

        #region Methods
        public static int Save(int stdID, int modifierID, DateTime modifiedDate, List<Cal_Course_Prog_Node> ccpns, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;
            try
            {
                Delete(stdID, sqlConn, sqlTran);

                string cmd = INSERT;
                foreach (Cal_Course_Prog_Node ccpn in ccpns)
                {
                    AssignStudentToCalCrsProgNode sccpn = new AssignStudentToCalCrsProgNode();
                    sccpn.StudentID = stdID;
                    sccpn.CalCourseProgNodeID = ccpn.Id;

                    Common.DAOParameters dps = new Common.DAOParameters();
                    dps.AddParameter(STUDENTID_PA, sccpn.StudentID);
                    dps.AddParameter(CALCOURSEPROGNODEID_PA, sccpn.CalCourseProgNodeID);
                    dps.AddParameter(ISCOMPLETED_PA, sccpn.IsCompleted);
                    dps.AddParameter(ORIGINALCALID_PA, sccpn.OriginalCalID);
                    dps.AddParameter(ISAUTOASSIGN_PA, sccpn.IsAutoAssign);
                    dps.AddParameter(ISAUTOOPEN_PA, sccpn.IsAutoOpen);
                    dps.AddParameter(ISREQUISITIONED_PA, sccpn.IsRequisitioned);
                    dps.AddParameter(ISMANDATORY_PA, sccpn.IsMandatory);
                    dps.AddParameter(ISMANUALOPEN_PA, sccpn.IsManualOpen);

                    dps.AddParameter(CREATORID_PA, modifierID);
                    dps.AddParameter(CREATEDDATE_PA, modifiedDate);

                    List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                    counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(cmd, sqlConn, sqlTran, ps);
                }
            }
            catch (Exception ex)
            {
                //FixMe
            }
            return counter;
        }

        public static int Save2(int stdID, int treeMasterID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;
            try
            {
                Delete(stdID, sqlConn, sqlTran);

                String cmd = "sp_PrepareWorksheet";                

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter("@StuId", stdID);
                dps.AddParameter("@TreemasterID", treeMasterID);                

                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(cmd, CommandType.StoredProcedure, sqlConn, sqlTran, ps);

            }
            catch (Exception ex)
            {
                //FixMe
            }
            return counter;
        }

        internal static int Delete(int stdID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            try
            {
                string cmd = DELETE + " Where StudentID = " + stdID;

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter(STUDENTID_PA, stdID);

                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(cmd, sqlConn, sqlTran, ps);
            }
            catch (Exception ex)
            {
                //FixMe
            }
            return counter;
        }
        #endregion
    }
}
