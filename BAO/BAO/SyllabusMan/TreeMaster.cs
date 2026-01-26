using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class TreeMaster : Base
    {
        #region MyRegion
        //TreeMasterID	        int	            Unchecked
        //ProgramID	            int	            Unchecked
        //RootNodeID	        int	            Unchecked
        //StartTrimesterID	    int	            Checked
        //RequiredUnits	        int	            Checked
        //PassingGPA	        numeric(18, 2)	Checked
        //CreatedBy	            int	            Unchecked
        //CreatedDate	        datetime	    Unchecked
        //ModifiedBy	        int	            Checked
        //ModifiedDate	        datetime	    Checked
        #endregion

        #region Variables
        private int _ownerProgramID;
        private Program _ownerProgram = null;
        private int _rootNodeID;
        private Node _rootNode = null;
        private decimal _reqCredits;
        private decimal _passingGPA;
        //private int _calenderUnitID;
        //private List<TreeDetail> _tranDetails = null;
        #endregion

        #region Constants

        private const string TREEMASTERID = "TreeMasterID";

        private const string PROGRAMID = "ProgramID";
        private const string PROGRAMID_PA = "@ProgramID";

        private const string ROOTNODEID = "RootNodeID";
        private const string ROOTNODEID_PA = "@RootNodeID";

        private const string REQCREDITS = "RequiredUnits";
        private const string REQCREDITS_PA = "@RequiredUnits";

        private const string PASSINGGPA = "PassingGPA";
        private const string PASSINGGPA_PA = "@PassingGPA";


        private const string ALLCOLUMNS = "[" + TREEMASTERID + "], "
                                        + "[" + PROGRAMID + "], "
                                        + "[" + ROOTNODEID + "], "
                                        + "[" + REQCREDITS + "], "
                                        + "[" + PASSINGGPA + "], ";

        private const string NOPKCOLUMNS = "[" + PROGRAMID + "], "
                                        + "[" + ROOTNODEID + "], "
                                        + "[" + REQCREDITS + "], "
                                        + "[" + PASSINGGPA + "], ";

        private const string TABLENAME = " [TreeMaster] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        private const string INSERT = "INSERT INTO" + TABLENAME
                            + "("
                            + NOPKCOLUMNS
                            + BASECOLUMNS
                            + ") "
                            + "VALUES "
                            + "("
                            + PROGRAMID_PA + ", "
                            + ROOTNODEID_PA + ", "
                            + REQCREDITS_PA + ", "
                            + PASSINGGPA_PA + ", "
                            + CREATORID_PA + ", "
                            + CREATEDDATE_PA + ", "
                            + MODIFIERID_PA + ", "
                            + MOIDFIEDDATE_PA
                            + ")";

        private const string UPDATE = "UPDATE" + TABLENAME
                            + "SET [" + PROGRAMID + "] = " + PROGRAMID_PA + ", "
                            + "[" + ROOTNODEID + "] = " + ROOTNODEID_PA + ", "
                            + "[" + REQCREDITS + "] = " + REQCREDITS_PA + ","//6
                            + "[" + PASSINGGPA + "] = " + PASSINGGPA_PA + ","//6
                            + "[" + CREATORID + "] = " + CREATORID_PA + ","//7
                            + "[" + CREATEDDATE + "] = " + CREATEDDATE_PA + ","//8
                            + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ","//9
                            + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA;//10

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Constructor
        public TreeMaster()
            : base()
        {
            _ownerProgramID = 0;
            _rootNodeID = 0;
            _reqCredits = 0;
            _passingGPA = 0;
        }
        #endregion

        #region Properties

        public int OwnerProgramID
        {
            get { return _ownerProgramID; }
            set { _ownerProgramID = value; }
        }
        private SqlParameter OwnerProgramIDPram
        {
            get
            {
                SqlParameter ownerProgramIDParam = new SqlParameter();
                ownerProgramIDParam.ParameterName = PROGRAMID_PA;

                ownerProgramIDParam.Value = _ownerProgramID;

                return ownerProgramIDParam;
            }
        }
        public Program OwnerProgram
        {
            get
            {
                if (_ownerProgram == null)
                {
                    if (this.OwnerProgramID > 0)
                    {
                        _ownerProgram = Program.GetProgram(this.OwnerProgramID);
                    }
                }
                return _ownerProgram;
            }
        }

        public int RootNodeID
        {
            get { return _rootNodeID; }
            set { _rootNodeID = value; }
        }
        private SqlParameter RootNodeIDParam
        {
            get
            {
                SqlParameter rootNodeIDParam = new SqlParameter();
                rootNodeIDParam.ParameterName = ROOTNODEID_PA;

                rootNodeIDParam.Value = _rootNodeID;

                return rootNodeIDParam;
            }
        }
        public Node RootNode
        {
            get
            {
                if (_rootNode == null)
                {
                    if (this.RootNodeID > 0)
                    {
                        _rootNode = Node.GetNode(this.RootNodeID);
                    }
                }
                return _rootNode;
            }
        }

        public decimal ReqCredits
        {
            get { return _reqCredits; }
            set { _reqCredits = value; }
        }
        private SqlParameter ReqCreditsParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = REQCREDITS_PA;

                if (true)
                {
                    sqlParam.Value = ReqCredits;
                }
                else
                {
                }

                return sqlParam;
            }
        }

        public decimal PassingGPA
        {
            get { return _passingGPA; }
            set { _passingGPA = value; }
        }
        private SqlParameter PassingGPAParam
        {
            get
            {
                SqlParameter passingGPAParam = new SqlParameter();
                passingGPAParam.ParameterName = PASSINGGPA_PA;
                if (PassingGPA > 0)
                {
                    passingGPAParam.Value = PassingGPA;
                }
                else
                {
                    passingGPAParam.Value = DBNull.Value;
                }
                return passingGPAParam;
            }
        }
        #endregion

        #region Functions
        private static TreeMaster treeMasterMapper(SQLNullHandler nullHandler)
        {
            TreeMaster treeMaster = new TreeMaster();

            treeMaster.Id = nullHandler.GetInt32(TREEMASTERID);
            treeMaster.OwnerProgramID = nullHandler.GetInt32(PROGRAMID);
            treeMaster.RootNodeID = nullHandler.GetInt32(ROOTNODEID);
            treeMaster.ReqCredits = nullHandler.GetDecimal(REQCREDITS);
            treeMaster.PassingGPA = nullHandler.GetDecimal(PASSINGGPA);
            treeMaster.CreatorID = nullHandler.GetInt32(CREATORID);
            treeMaster.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            treeMaster.ModifierID = nullHandler.GetInt32(MODIFIERID);
            treeMaster.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);
            return treeMaster;
        }
        private static List<TreeMaster> mapTreeMasters(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<TreeMaster> treeMasters = null;

            while (theReader.Read())
            {
                if (treeMasters == null)
                {
                    treeMasters = new List<TreeMaster>();
                }
                TreeMaster treeMaster = treeMasterMapper(nullHandler);
                treeMasters.Add(treeMaster);
            }

            return treeMasters;
        }
        private static TreeMaster mapTreeMaster(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            TreeMaster treeMaster = null;
            if (theReader.Read())
            {
                treeMaster = new TreeMaster();
                treeMaster = treeMasterMapper(nullHandler);
            }

            return treeMaster;
        }


        public static TreeMaster Get(int treeMasterID)
        {
            TreeMaster treeMaster = null;

            string command = SELECT
                            + "WHERE TreeMasterID = @TreeMasterID";
            SqlParameter treeMasterIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(treeMasterID, "@TreeMasterID");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { treeMasterIDparam });

            treeMaster = mapTreeMaster(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return treeMaster;
        }

        public static List<TreeMaster> GetByProgram(int programID)
        {
            List<TreeMaster> treeMasters = null;
            string command = string.Empty;
            if (programID == 0)
            {
                command = SELECT;
            }
            else
            {
                command = SELECT
                                + "WHERE ProgramID = @ProgramID";
            }
            SqlParameter programIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(programID, "@ProgramID");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { programIDparam });

            treeMasters = mapTreeMasters(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return treeMasters;
        }
        internal static List<TreeMaster> GetByProgram(int programID, SqlConnection sqlConn)
        {
            List<TreeMaster> treeMasters = null;

            string command = SELECT
                            + "WHERE ProgramID = @ProgramID";
            SqlParameter programIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(programID, "@ProgramID");

            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { programIDparam });

            treeMasters = mapTreeMasters(theReader);
            theReader.Close();

            return treeMasters;
        }

        public static TreeMaster GetByProgram(int programID, int rootNodeID)
        {
            TreeMaster treeMaster = null;
            string command = string.Empty;
            if (programID == 0)
            {
                command = SELECT + "WHERE RootNodeID = @RootNodeID";
            }
            else
            {
                command = SELECT
                            + "WHERE ProgramID = @ProgramID AND RootNodeID = @RootNodeID";
            }
            SqlParameter programIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(programID, "@ProgramID");
            SqlParameter rootNodeIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(rootNodeID, "@RootNodeID");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { programIDparam, rootNodeIDparam });

            treeMaster = mapTreeMaster(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return treeMaster;
        }
        internal static TreeMaster GetByProgram(int programID, int rootNodeID, SqlConnection sqlConn)
        {
            TreeMaster treeMaster = null;

            string command = SELECT
                            + "WHERE ProgramID = @ProgramID AND RootNodeID = @RootNodeID";
            SqlParameter programIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(programID, "@ProgramID");
            SqlParameter rootNodeIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(rootNodeID, "@RootNodeID");


            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { programIDparam, rootNodeIDparam });

            treeMaster = mapTreeMaster(theReader);
            theReader.Close();

            return treeMaster;
        }

        #region Old
        //public static List<TemplateHierarchy> getByParentTemplateNRST(int parentTemplateID)
        //{
        //    List<TemplateHierarchy> templateHierarchys = new List<TemplateHierarchy>();

        //    string command = "SELECT "
        //                    + "TplHierarchyID, "
        //                    + "ParentTplID, "
        //                    + "ChildTplID "
        //                    + "FROM dbo.TplHierarchy "
        //                    + "WHERE ParentTplID = " + parentTemplateID.ToString().Trim();

        //    SqlConnection sqlConn = SqlConnectionHandler.GetConnection();
        //    SqlDataReader theReader = SqlConnectionHandler.MSSqlExecuteQuerryCommand(command, sqlConn);

        //    templateHierarchys = mapTemplateHeirarchy(theReader);
        //    theReader.Close();

        //    foreach (TemplateHierarchy templateHierarchy in templateHierarchys)
        //    {
        //        if (templateHierarchy.ParentTplID != 0)
        //        {
        //            templateHierarchy._ParentTemplate = Template.getTemplate(templateHierarchy.ParentTplID, sqlConn);
        //        }
        //        if (templateHierarchy.ChildTplID != 0)
        //        {
        //            templateHierarchy._ChildTemplate = Template.getTemplate(templateHierarchy.ChildTplID, sqlConn);
        //        }
        //    }

        //    SqlConnectionHandler.CloseDbConnection();

        //    return templateHierarchys;
        //}
        //internal static List<TemplateHierarchy> getByParentTemplateNRST(int parentTemplateID, SqlConnection sqlConn)
        //{
        //    List<TemplateHierarchy> templateHierarchys = new List<TemplateHierarchy>();

        //    string command = "SELECT "
        //                    + "TplHierarchyID, "
        //                    + "ParentTplID, "
        //                    + "ChildTplID "
        //                    + "FROM dbo.TplHierarchy "
        //                    + "WHERE ParentTplID = " + parentTemplateID.ToString().Trim();

        //    SqlDataReader theReader = SqlConnectionHandler.MSSqlExecuteQuerryCommand(command, sqlConn);

        //    templateHierarchys = mapTemplateHeirarchy(theReader);
        //    theReader.Close();

        //    foreach (TemplateHierarchy templateHierarchy in templateHierarchys)
        //    {
        //        if (templateHierarchy.ParentTplID != 0)
        //        {
        //            templateHierarchy._ParentTemplate = Template.getTemplate(templateHierarchy.ParentTplID, sqlConn);
        //        }
        //        if (templateHierarchy.ChildTplID != 0)
        //        {
        //            templateHierarchy._ChildTemplate = Template.getTemplate(templateHierarchy.ChildTplID, sqlConn);
        //        }
        //    }

        //    return templateHierarchys;
        //} 
        #endregion

        public static int SaveTreeMaster(TreeMaster treeMaster)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (treeMaster.Id == 0)
                {
                    command = INSERT;
                    sqlParams = new SqlParameter[] { 
                    treeMaster.OwnerProgramIDPram, 
                    treeMaster.RootNodeIDParam,
                    treeMaster.ReqCreditsParam,
                    treeMaster.PassingGPAParam,
                    treeMaster.CreatorIDParam,
                    treeMaster.CreatedDateParam,
                    treeMaster.ModifierIDParam,
                    treeMaster.ModifiedDateParam};
                }
                else
                {
                    command = UPDATE
                            + " WHERE [" + TREEMASTERID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { 
                    treeMaster.OwnerProgramIDPram, 
                    treeMaster.RootNodeIDParam,
                    treeMaster.ReqCreditsParam,
                    treeMaster.PassingGPAParam,
                    treeMaster.CreatorIDParam,
                    treeMaster.CreatedDateParam,
                    treeMaster.ModifierIDParam,
                    treeMaster.ModifiedDateParam, 
                    treeMaster.IDParam };
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteAction(command, sqlConn, sqlParams);

                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        public static int SaveTreeMasterWithRootNode(Node rootNode, TreeMaster treeMaster)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                Node.SaveNode(rootNode, sqlConn, sqlTran);
                treeMaster.RootNodeID = rootNode.Id;

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (treeMaster.Id == 0)
                {
                    command = INSERT;
                    sqlParams = new SqlParameter[] { 
                    treeMaster.OwnerProgramIDPram, 
                    treeMaster.RootNodeIDParam,
                    treeMaster.ReqCreditsParam,
                    treeMaster.PassingGPAParam,
                    treeMaster.CreatorIDParam,
                    treeMaster.CreatedDateParam,
                    treeMaster.ModifierIDParam,
                    treeMaster.ModifiedDateParam};
                }
                else
                {
                    command = UPDATE
                            + " WHERE [" + TREEMASTERID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { 
                    treeMaster.OwnerProgramIDPram, 
                    treeMaster.RootNodeIDParam,
                    treeMaster.ReqCreditsParam,
                    treeMaster.PassingGPAParam,
                    treeMaster.CreatorIDParam,
                    treeMaster.CreatedDateParam,
                    treeMaster.ModifierIDParam,
                    treeMaster.ModifiedDateParam, 
                    treeMaster.IDParam };
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        public static int DeleteTreeMaster(int treeMasterID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                TreeDetail.DeleteTreeDetailByMaster(treeMasterID, sqlConn, sqlTran);

                string command = DELETE
                                + "WHERE [" + TREEMASTERID + "] = " + ID_PA;
                SqlParameter treeMasterIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(treeMasterID, "@TreeMasterID");

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { treeMasterIDparam });

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        public static int DeleteTreeMaster(int treeMasterID, string childNodeIDs, int rootNodeID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                if (childNodeIDs.Trim().Length > 0)
                {
                    TreeDetail.DeleteTreeDetailByMaster(treeMasterID, sqlConn, sqlTran, childNodeIDs);
                }

                string command = DELETE
                                + "WHERE [" + TREEMASTERID + "] = " + ID_PA;
                SqlParameter treeMasterIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(treeMasterID, ID_PA);

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { treeMasterIDparam });

                Node.DeleteNode(rootNodeID, sqlConn, sqlTran);

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        #endregion
    }
}
