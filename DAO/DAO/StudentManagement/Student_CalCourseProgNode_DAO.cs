using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class Student_CalCourseProgNode_DAO : Base_DAO
    {
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

        private const string TREECALENDARDETAILID = "TreeCalendarDetailID";
        private const string TREECALENDARDETAILID_PA = "@TreeCalendarDetailID";

        private const string TREECALENDARMASTERID = "TreeCalendarMasterID";
        private const string TREECALENDARMASTERID_PA = "@TreeCalendarMasterID";

        private const string TREEMASTERID = "TreeMasterID";
        private const string TREEMASTERID_PA = "@TreeMasterID";

        private const string CALENDARMASTERNAME = "CalendarMasterName";
        private const string CALENDARMASTERNAME_PA = "@CalendarMasterName";

        private const string CALENDARDETAILNAME = "CalendarDetailName";
        private const string CALENDARDETAILNAME_PA = "@CalendarDetailName";

        private const string FORMALCODE = "FormalCode";
        private const string FORMALCODE_PA = "@FormalCode";

        private const string VERSIONCODE = "VersionCode";
        private const string VERSIONCODE_PA = "@VersionCode";

        private const string COURSETITLE = "CourseTitle";
        private const string COURSETITLE_PA = "@CourseTitle";

        private const string NODELINKNAME = "NodeLinkName";
        private const string NODELINKNAME_PA = "@NodeLinkName";

        private const string PRIORITY = "Priority";
        private const string PRIORITY_PA = "@Priority";

        private const string RETAKENO = "RetakeNo";
        private const string RETAKENO_PA = "@RetakeNo";

        private const string OBTAINEDGPA = "ObtainedGPA";
        private const string OBTAINEDGPA_PA = "@ObtainedGPA";

        private const string OBTAINEDGRADE = "ObtainedGrade";
        private const string OBTAINEDGRADE_PA = "@ObtainedGrade";

        private const string ACACALYEAR = "AcaCalYear";
        private const string ACACALYEAR_PA = "@AcaCalYear";

        private const string BATCHCODE = "BatchCode";
        private const string BATCHCODE_PA = "@BatchCode";

        private const string ACACALTYPENAME = "AcaCalTypeName";
        private const string ACACALTYPENAME_PA = "@AcaCalTypeName";
        //..........................................................
        private const string ACACAL_SECTIONID = "AcaCal_SectionID";
        private const string ACACAL_SECTIONID_PA = "@AcaCal_SectionID";

        private const string SECTIONNAME = "SectionName";
        private const string SECTIONNAME_PA = "@SectionName";

        private const string COURSEID = "CourseID";
        private const string COURSEID_PA = "@CourseID";

        private const string VERSIONID = "VersionID";
        private const string VERSIONID_PA = "@VersionID";

        private const string NODE_COURSEID = "Node_CourseID";
        private const string NODE_COURSEID_PA = "@Node_CourseID";

        private const string NODEID = "NodeID";
        private const string NODEID_PA = "@NodeID";

        private const string PROGRAMID = "ProgramID";
        private const string PROGRAMID_PA = "@ProgramID";

        private const string DEPTID = "DeptID";
        private const string DEPTID_PA = "@DeptID";

        private const string ACADEMICCALENDERID = "AcademicCalenderID";
        private const string ACADEMICCALENDERID_PA = "@AcademicCalenderID";

        //........
        private const string ISMULTIPLEACUSPAN = "IsMultipleACUSpan";
        private const string ISMULTIPLEACUSPAN_PA = "@IsMultipleACUSpan";

        private const string COURSECREDIT = "CourseCredit";
        private const string COURSECREDIT_PA = "@CourseCredit";

        private const string COMPLETEDCREDIT = "CompletedCredit";
        private const string COMPLETEDCREDIT_PA = "@CompletedCredit";

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
                                        + "[" + ISMANUALOPEN + "], "
                                        + "[" + TREECALENDARDETAILID + "], "
                                        + "[" + TREECALENDARMASTERID + "], "
                                        + "[" + TREEMASTERID + "], "
                                        + "[" + CALENDARMASTERNAME + "], "
                                        + "[" + CALENDARDETAILNAME + "], "
                                        + "[" + FORMALCODE + "], "
                                        + "[" + VERSIONCODE + "], "
                                        + "[" + COURSETITLE + "], "
                                        + "[" + NODELINKNAME + "], "
                                        + "[" + PRIORITY + "], "
                                        + "[" + RETAKENO + "], "
                                        + "[" + OBTAINEDGPA + "], "
                                        + "[" + OBTAINEDGRADE + "], "
                                        + "[" + ACACALYEAR + "], "
                                        + "[" + BATCHCODE + "], "
                                        + "[" + ACACALTYPENAME + "], ";
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
                                        + "[" + ISMANUALOPEN + "], "
                                        + "[" + TREECALENDARDETAILID + "], "
                                        + "[" + TREECALENDARMASTERID + "], "
                                        + "[" + TREEMASTERID + "], "
                                        + "[" + CALENDARMASTERNAME + "], "
                                        + "[" + CALENDARDETAILNAME + "], "
                                        + "[" + FORMALCODE + "], "
                                        + "[" + VERSIONCODE + "], "
                                        + "[" + COURSETITLE + "], "
                                        + "[" + NODELINKNAME + "], "
                                        + "[" + PRIORITY + "], "
                                        + "[" + RETAKENO + "], "
                                        + "[" + OBTAINEDGPA + "], "
                                        + "[" + OBTAINEDGRADE + "], "
                                        + "[" + ACACALYEAR + "], "
                                        + "[" + BATCHCODE + "], "
                                        + "[" + ACACALTYPENAME + "], ";

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
                     + TREECALENDARDETAILID_PA + ", "
                     + TREECALENDARMASTERID_PA + ", "
                     + TREEMASTERID_PA + ", "
                     + CALENDARMASTERNAME_PA + ", "
                     + CALENDARDETAILNAME_PA + ", "
                     + FORMALCODE_PA + ", "
                     + VERSIONCODE_PA + ", "
                     + COURSETITLE_PA + ", "
                     + NODELINKNAME_PA + ", "
                     + PRIORITY_PA + ", "
                     + RETAKENO_PA + ", "
                     + OBTAINEDGPA_PA + ", "
                     + OBTAINEDGRADE_PA + ", "
                     + ACACALYEAR_PA + ", "
                     + BATCHCODE_PA + ", "
                     + ACACALTYPENAME_PA + ", "
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
                    + "[" + TREECALENDARDETAILID + "] = " + TREECALENDARDETAILID_PA + ", "
                    + "[" + TREECALENDARMASTERID + "] = " + TREECALENDARMASTERID_PA + ", "
                    + "[" + TREEMASTERID + "] = " + TREEMASTERID_PA + ", "
                    + "[" + CALENDARMASTERNAME + "] = " + CALENDARMASTERNAME_PA + ", "
                    + "[" + CALENDARDETAILNAME + "] = " + CALENDARDETAILNAME_PA + ", "
                    + "[" + FORMALCODE + "] = " + FORMALCODE_PA + ", "
                    + "[" + VERSIONCODE + "] = " + VERSIONCODE_PA + ", "
                    + "[" + COURSETITLE + "] = " + COURSETITLE_PA + ", "
                    + "[" + NODELINKNAME + "] = " + NODELINKNAME_PA + ", "
                    + "[" + PRIORITY + "] = " + PRIORITY_PA + ", "
                    + "[" + RETAKENO + "] = " + RETAKENO_PA + ", "
                    + "[" + OBTAINEDGPA + "] = " + OBTAINEDGPA_PA + ", "
                    + "[" + OBTAINEDGRADE + "] = " + OBTAINEDGRADE_PA + ", "
                    + "[" + ACACALYEAR + "] = " + ACACALYEAR_PA + ", "
                    + "[" + BATCHCODE + "] = " + BATCHCODE_PA + ", "
                    + "[" + ACACALTYPENAME + "] = " + ACACALTYPENAME_PA + ", "
                    + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ", "
                    + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA + ", "

                    + "[" + ACACAL_SECTIONID + "] = " + ACACAL_SECTIONID_PA + ", "
                    + "[" + SECTIONNAME + "] = " + SECTIONNAME_PA + ", "
                    + "[" + COURSEID + "] = " + COURSEID_PA + ", "
                    + "[" + VERSIONID + "] = " + VERSIONID_PA + ", "
                    + "[" + NODE_COURSEID + "] = " + NODE_COURSEID_PA + ", "
                    + "[" + NODEID + "] = " + NODEID_PA + ", "
                    + "[" + PROGRAMID + "] = " + PROGRAMID_PA + ", "
                    + "[" + DEPTID + "] = " + DEPTID_PA + ", "
                    + "[" + ACADEMICCALENDERID + "] = " + ACADEMICCALENDERID_PA + ", "

                    + "[" + ISMULTIPLEACUSPAN + "] = " + ISMULTIPLEACUSPAN_PA + ", "
                    + "[" + COURSECREDIT + "] = " + COURSECREDIT_PA + ", "
                    + "[" + COMPLETEDCREDIT + "] = " + COMPLETEDCREDIT_PA

                    + " WHERE [" + ID + "] = " + ID_PA;

        #endregion

        #region Delete Query
        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion
        #endregion

        #region variable : sql connection, transaction
        //private static SqlConnection sqlCon = null;
        //private static SqlTransaction sqlTran = null;
        #endregion
        #region global variables
        private static DeptRegSetUpEntity _progParameter = null;
        private static List<Cal_Course_Prog_NodeEntity> _ccpns = null;
        private static List<Student_CalCourseProgNodeEntity> _stdCalCrsProgNodes = null;
        #endregion

        #region Methods

        private static Student_CalCourseProgNodeEntity Mapper(SQLNullHandler nullHandler)
        {
            Student_CalCourseProgNodeEntity stdCcpn = new Student_CalCourseProgNodeEntity();

            stdCcpn.Id = nullHandler.GetInt32(ID);
            stdCcpn.StudentID = nullHandler.GetInt32(STUDENTID);
            stdCcpn.CalCourseProgNodeID = nullHandler.GetInt32(CALCOURSEPROGNODEID);
            stdCcpn.IsCompleted = nullHandler.GetBoolean(ISCOMPLETED);
            stdCcpn.OriginalCalID = nullHandler.GetInt32(ORIGINALCALID);
            stdCcpn.IsAutoAssign = nullHandler.GetBoolean(ISAUTOASSIGN);
            stdCcpn.IsAutoOpen = nullHandler.GetBoolean(ISAUTOOPEN);
            stdCcpn.IsRequisitioned = nullHandler.GetBoolean(ISREQUISITIONED);
            stdCcpn.IsMandatory = nullHandler.GetBoolean(ISMANDATORY);
            stdCcpn.IsManualOpen = nullHandler.GetBoolean(ISMANUALOPEN);
            stdCcpn.TreeCalendarDetailID = nullHandler.GetInt32(TREECALENDARDETAILID);
            stdCcpn.TreeCalendarMasterID = nullHandler.GetInt32(TREECALENDARMASTERID);
            stdCcpn.TreeMasterID = nullHandler.GetInt32(TREEMASTERID);
            stdCcpn.CalendarMasterName = nullHandler.GetString(CALENDARMASTERNAME);
            stdCcpn.CalendarDetailName = nullHandler.GetString(CALENDARDETAILNAME);
            stdCcpn.FormalCode = nullHandler.GetString(FORMALCODE);
            stdCcpn.VersionCode = nullHandler.GetString(VERSIONCODE);
            stdCcpn.CourseTitle = nullHandler.GetString(COURSETITLE);
            stdCcpn.NodeLinkName = nullHandler.GetString(NODELINKNAME);
            stdCcpn.Priority = nullHandler.GetInt32(PRIORITY);
            stdCcpn.RetakeNo = nullHandler.GetInt32(RETAKENO);
            stdCcpn.ObtainedGPA = nullHandler.GetDecimal(OBTAINEDGPA);
            stdCcpn.ObtainedGrade = nullHandler.GetString(OBTAINEDGRADE);
            stdCcpn.AcaCalYear = nullHandler.GetInt32(ACACALYEAR);
            stdCcpn.BatchCode = nullHandler.GetString(BATCHCODE);
            stdCcpn.AcaCalTypeName = nullHandler.GetString(ACACALTYPENAME);
            stdCcpn.CreatorID = nullHandler.GetInt32(CREATORID);
            stdCcpn.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            stdCcpn.ModifierID = nullHandler.GetInt32(MODIFIERID);
            stdCcpn.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);

            stdCcpn.AcaCal_SectionID = nullHandler.GetInt32(ACACAL_SECTIONID);
            stdCcpn.SectionName = nullHandler.GetString(SECTIONNAME);
            stdCcpn.CourseID = nullHandler.GetInt32(COURSEID);
            stdCcpn.VersionID = nullHandler.GetInt32(VERSIONID);
            stdCcpn.Node_CourseID = nullHandler.GetInt32(NODE_COURSEID);
            stdCcpn.NodeID = nullHandler.GetInt32(NODEID);
            stdCcpn.ProgramID = nullHandler.GetInt32(PROGRAMID);
            stdCcpn.DeptID = nullHandler.GetInt32(DEPTID);
            stdCcpn.AcademicCalenderID = nullHandler.GetInt32(ACADEMICCALENDERID);

            stdCcpn.IsMultipleACUSpan = nullHandler.GetBoolean(ISMULTIPLEACUSPAN);
            stdCcpn.CourseCredit = nullHandler.GetDecimal(COURSECREDIT);
            stdCcpn.CompletedCredit = nullHandler.GetDecimal(COMPLETEDCREDIT);

            return stdCcpn;
        }

        private static List<Student_CalCourseProgNodeEntity> MapEntities(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            List<Student_CalCourseProgNodeEntity> entities = new List<Student_CalCourseProgNodeEntity>();
            while (theReader.Read())
            {
                Student_CalCourseProgNodeEntity stdCcpn = new Student_CalCourseProgNodeEntity();
                stdCcpn = Mapper(nullHandler);
                entities.Add(stdCcpn);
            }
            return entities;
        }

        internal static int SaveWorksheetPreparation(StudentEntity std, List<Cal_Course_Prog_NodeEntity> ccpns)
        {
            int counter = 0;
            try
            {
                string cmd = INSERT;
                foreach (Cal_Course_Prog_NodeEntity ccpn in ccpns)
                {
                    Student_CalCourseProgNodeEntity sccpn = new Student_CalCourseProgNodeEntity();
                    sccpn.StudentID = std.Id;
                    sccpn.CalCourseProgNodeID = ccpn.Id;
                    sccpn.CreatorID = std.CreatorID;
                    sccpn.CreatedDate = std.CreatedDate;

                    counter += QueryHandler.ExecuteSelectBatchAction(cmd, MakeSqlParameterList(sccpn));
                }
            }
            catch (Exception ex)
            {
                //FixMe
            }
            return counter;
        }

        //public static List<Student_CalCourseProgNodeEntity> GetByStdID(int stdID)
        //{
        //    List<Student_CalCourseProgNodeEntity> stdCalcrsProgNodes = new List<Student_CalCourseProgNodeEntity>();

        //    string cmd = SELECT + " Where StudentID = " + stdID;
        //    Common.DAOParameters dps = new Common.DAOParameters();
        //    dps.AddParameter(STUDENTID_PA, stdID);

        //    List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

        //    counter = MSSqlConnectionHandler.MSSqlExecuteBatchAction(cmd, ps);

        //    return stdCalcrsProgNodes;
        //}

        internal static int Delete(int stdID)
        {
            int counter = 0;

            try
            {
                string cmd = DELETE + " Where StudentID = " + STUDENTID_PA;

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter(STUDENTID_PA, stdID);

                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                counter += QueryHandler.ExecuteDeleteBatchAction(cmd, ps);
            }
            catch (Exception ex)
            {
                //FixMe
            }
            return counter;
        }

        internal static int Prepare(StudentEntity std)
        {
            int counter = 0;

            try
            {
                TreeCalendarMasterEntity stdCcpn = TreeCalendarMaster_DAO.GetByTreeMasterID(std.TreeMasterID);
                List<TreeCalendarDetailEntity> treeCalDetails = TreeCalendarDetail_DAO.GetByTreeCalMaster(stdCcpn.Id);

                _ccpns = new List<Cal_Course_Prog_NodeEntity>();
                foreach (TreeCalendarDetailEntity tcd in treeCalDetails)
                {
                    List<Cal_Course_Prog_NodeEntity> ccpnsTemp = Cal_Course_Prog_Node_DAO.GetByTreeCalDet(tcd.Id);
                    foreach (Cal_Course_Prog_NodeEntity cc in ccpnsTemp)
                    {
                        _ccpns.Add(cc);
                    }
                }
                SaveWorksheetPreparation(std, _ccpns);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return counter;

        }

        internal static void GetProgParameterSetup(int progID)
        {
            try
            {
                _progParameter = null;
                _progParameter = DeptRegSetUpEntity_DAO.GetByProgramID(progID);
            }
            catch (Exception ex)
            {
                //FixMe
                throw ex;
            }
        }

        public static void GetPrioritizedWorkSheet(int stdID)
        {
            try
            {
                _stdCalCrsProgNodes = null;
                string cmd = "DECLARE @return_value int EXEC @return_value = [dbo].[sp_GetStdCalCrsProgNd] @stdID = " + STUDENTID_PA + " SELECT	'Return Value' = @return_value";

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter(STUDENTID_PA, stdID);
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                SqlDataReader rd = QueryHandler.ExecuteSelectBatchQuery(cmd, ps);
                _stdCalCrsProgNodes = MapEntities(rd);
                rd.Close();
            }
            catch (Exception ex)
            {
                //FixMe
                throw ex;
            }
        }

        private static void AutoAssign(int stdID)
        {
            try
            {
                Student_ACUDetailEntity last = Student_ACUDetailEntity_DAO.GetMaxByStudentID(stdID);//last will be null if he is a new student
                if (last == null)
                {
                    NewStudentAutoAssign(stdID);
                }
                else
                {
                    //OldStudentAutoAssign(stdID);
                }
            }
            catch (Exception ex)
            {
                //FixMe
                throw ex;
            }

        }

        internal static int NewStudentAutoAssign(int stdID)
        {
            try
            {
                List<Student_CalCourseProgNodeEntity> sccpns = GetStdCalCourseProgNodeForNewStudent(stdID);
                List<Student_CalCourseProgNodeEntity> entList = new List<Student_CalCourseProgNodeEntity>();

                foreach (Student_CalCourseProgNodeEntity sccpn in sccpns)
                {
                    sccpn.IsAutoAssign = true;
                    entList.Add(sccpn);
                }
                int counter = 0;
                foreach (Student_CalCourseProgNodeEntity ent in entList)
                {
                    counter = QueryHandler.ExecuteSelectBatchAction(UPDATE, MakeSqlParameterList(ent));
                }
                return counter;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal static List<Student_CalCourseProgNodeEntity> GetStdCalCourseProgNodeForNewStudent(int stdID)
        {
            try
            {
                List<Student_CalCourseProgNodeEntity> sccpn = null;
                string cmd = "DECLARE @return_value int EXEC @return_value = [dbo].[sp_GetNewStdCalCrsProgNd] @stdID = " + STUDENTID_PA + " SELECT 'Return Value' = @return_value";

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter("@StudentID", stdID);
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                SqlDataReader rd = QueryHandler.ExecuteSelectBatchQuery(cmd, ps);
                sccpn = MapEntities(rd);
                rd.Close();
                return sccpn;
            }
            catch (Exception ex)
            {
                //FixMe
                throw ex;
            }
        }

        public static List<Student_CalCourseProgNodeEntity> GetStdCalCourseProgNodeForStudent(int stdID)
        {
            try
            {
                List<Student_CalCourseProgNodeEntity> sccpn = null;
                string cmd = "select *  from StudentCalCourseProgNode where StudentID = " + stdID + " and SectionName != '' and AcademicCalenderID = (select AcademicCalenderID from dbo.AcademicCalender where IsCurrent = 1)";

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter("@StudentID", stdID);
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                SqlDataReader rd = QueryHandler.ExecuteSelectBatchQuery(cmd, ps);
                sccpn = MapEntities(rd);
                rd.Close();
                return sccpn;
            }
            catch (Exception ex)
            {
                //FixMe
                throw ex;
            }
        }

        private static List<SqlParameter> MakeSqlParameterList(Student_CalCourseProgNodeEntity sccpn)
        {
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

            if (sccpn.Id != 0)
            {
                dps.AddParameter(MODIFIERID_PA, sccpn.CreatorID);//do not change CreatorID, CreatedDate
                dps.AddParameter(MOIDFIEDDATE_PA, sccpn.CreatedDate);
                dps.AddParameter(ID_PA, sccpn.Id);
            }
            else
            {
                dps.AddParameter(CREATORID_PA, sccpn.CreatorID);
                dps.AddParameter(CREATEDDATE_PA, sccpn.CreatedDate);
            }

            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
            return ps;
        }

        public static int Save(List<StudentEntity> stds)
        {
            int counter = 0;
            try
            {
                _progParameter = null;
                _ccpns = null;
                _stdCalCrsProgNodes = null;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();

                foreach (StudentEntity se in stds)
                {
                    PrepareWorkSheet(se);
                    //UpdateWorksheet();
                    GetProgParameterSetup(se.ProgramID);
                    GetPrioritizedWorkSheet(se.Id);
                    AutoAssign(se.Id);
                    //AutoOpen();
                }

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                //fixMe
                throw ex;
            }
            return counter;

        }
        internal static int PrepareWorkSheet(StudentEntity std)
        {
            int counter = 0;
            try
            {
                Delete(std.Id);
                Prepare(std);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return counter;
        }

        #endregion


        public static List<Student_CalCourseProgNodeEntity> GetWorkSheet(int stdID)
        {
            try
            {
                _stdCalCrsProgNodes = null;
                string cmd = " Select * from StudentCalCourseProgNode where StudentID = " + stdID + "";

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter(STUDENTID_PA, stdID);
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                SqlDataReader rd = QueryHandler.ExecuteSelectBatchQuery(cmd, ps);
                _stdCalCrsProgNodes = MapEntities(rd);
                rd.Close();
            }
            catch (Exception ex)
            {
                //FixMe // need to make overload
                throw ex;
            }
            return _stdCalCrsProgNodes;
        }

        public static List<Student_CalCourseProgNodeEntity> GetWorkSheetByStuId(int stdID)
        {
            try
            {
                _stdCalCrsProgNodes = null;
                string cmd = " Select * from StudentCalCourseProgNode where StudentID = " + stdID + "";

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter(STUDENTID_PA, stdID);
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                SqlDataReader rd = QueryHandler.ExecuteSelectQuery(cmd, ps);
                _stdCalCrsProgNodes = MapEntities(rd);
                rd.Close();
            }
            catch (Exception ex)
            {
                //FixMe // need to make overload
                throw ex;
            }
            return _stdCalCrsProgNodes;
        }

        public static int UpdateRow(Student_CalCourseProgNodeEntity sccpn)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();
            DAOParameters dParam = new DAOParameters();

            int m = Section_DAO.DecreaseIfOccupied(sccpn.Id, sqlConn, sqlTran);
            int n = Section_DAO.IncreaseOccupied(sccpn.AcaCal_SectionID, sqlConn, sqlTran);

            string command = UPDATE;

            dParam.AddParameter(STUDENTID_PA, sccpn.StudentID);
            dParam.AddParameter(CALCOURSEPROGNODEID_PA, sccpn.CalCourseProgNodeID);
            dParam.AddParameter(ISCOMPLETED_PA, sccpn.IsCompleted);
            dParam.AddParameter(ORIGINALCALID_PA, sccpn.OriginalCalID);
            dParam.AddParameter(ISAUTOASSIGN_PA, sccpn.IsAutoAssign);
            dParam.AddParameter(ISAUTOOPEN_PA, sccpn.IsAutoOpen);
            dParam.AddParameter(ISREQUISITIONED_PA, sccpn.IsRequisitioned);
            dParam.AddParameter(ISMANDATORY_PA, sccpn.IsMandatory);
            dParam.AddParameter(ISMANUALOPEN_PA, sccpn.IsManualOpen);
            dParam.AddParameter(TREECALENDARDETAILID_PA, sccpn.TreeCalendarDetailID);
            dParam.AddParameter(TREECALENDARMASTERID_PA, sccpn.TreeCalendarMasterID);
            dParam.AddParameter(TREEMASTERID_PA, sccpn.TreeMasterID);
            dParam.AddParameter(CALENDARMASTERNAME_PA, sccpn.CalendarMasterName);
            dParam.AddParameter(CALENDARDETAILNAME_PA, sccpn.CalendarDetailName);
            dParam.AddParameter(FORMALCODE_PA, sccpn.FormalCode);
            dParam.AddParameter(VERSIONCODE_PA, sccpn.VersionCode);
            dParam.AddParameter(COURSETITLE_PA, sccpn.CourseTitle);
            dParam.AddParameter(NODELINKNAME_PA, sccpn.NodeLinkName);
            dParam.AddParameter(PRIORITY_PA, sccpn.Priority);
            dParam.AddParameter(RETAKENO_PA, sccpn.RetakeNo);
            dParam.AddParameter(OBTAINEDGPA_PA, sccpn.ObtainedGPA);
            dParam.AddParameter(OBTAINEDGRADE_PA, sccpn.ObtainedGrade);
            dParam.AddParameter(ACACALYEAR_PA, sccpn.AcaCalYear);
            dParam.AddParameter(BATCHCODE_PA, sccpn.BatchCode);
            dParam.AddParameter(ACACALTYPENAME_PA, sccpn.AcaCalTypeName);
            dParam.AddParameter(MODIFIERID_PA, sccpn.ModifierID);
            dParam.AddParameter(MOIDFIEDDATE_PA, sccpn.ModifiedDate);
            dParam.AddParameter(ACACAL_SECTIONID_PA, sccpn.AcaCal_SectionID);
            dParam.AddParameter(SECTIONNAME_PA, sccpn.SectionName);
            dParam.AddParameter(COURSEID_PA, sccpn.CourseID);
            dParam.AddParameter(VERSIONID_PA, sccpn.VersionID);
            dParam.AddParameter(NODE_COURSEID_PA, sccpn.Node_CourseID);
            dParam.AddParameter(NODEID_PA, sccpn.NodeID);
            dParam.AddParameter(PROGRAMID_PA, sccpn.ProgramID);
            dParam.AddParameter(DEPTID_PA, sccpn.DeptID);
            dParam.AddParameter(ACADEMICCALENDERID_PA, sccpn.AcademicCalenderID);
            dParam.AddParameter(ISMULTIPLEACUSPAN_PA, sccpn.IsMultipleACUSpan);
            dParam.AddParameter(COURSECREDIT_PA, sccpn.CourseCredit);
            dParam.AddParameter(COMPLETEDCREDIT_PA, sccpn.CompletedCredit);
            dParam.AddParameter(ID_PA, sccpn.Id);

            List<SqlParameter> sqlParams = Methods.GetSQLParameters(dParam);

            int i = QueryHandler.ExecuteSaveBatchAction(command, sqlParams, sqlConn, sqlTran);

            MSSqlConnectionHandler.CommitTransaction();
            MSSqlConnectionHandler.CloseDbConnection(); //Close DB Connection

            return i;
        }

        public static int UpdateMultySpanData(List<Student_CalCourseProgNodeEntity> _sccpnMultySpanEntities)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();
            DAOParameters dParam;// = new DAOParameters();
            int i = 0;
            string command = UPDATE;

            foreach (Student_CalCourseProgNodeEntity sccpn in _sccpnMultySpanEntities)
            {
                dParam = new DAOParameters();

                dParam.AddParameter(STUDENTID_PA, sccpn.StudentID);
                dParam.AddParameter(CALCOURSEPROGNODEID_PA, sccpn.CalCourseProgNodeID);
                dParam.AddParameter(ISCOMPLETED_PA, sccpn.IsCompleted);
                dParam.AddParameter(ORIGINALCALID_PA, sccpn.OriginalCalID);
                dParam.AddParameter(ISAUTOASSIGN_PA, sccpn.IsAutoAssign);
                dParam.AddParameter(ISAUTOOPEN_PA, sccpn.IsAutoOpen);
                dParam.AddParameter(ISREQUISITIONED_PA, sccpn.IsRequisitioned);
                dParam.AddParameter(ISMANDATORY_PA, sccpn.IsMandatory);
                dParam.AddParameter(ISMANUALOPEN_PA, sccpn.IsManualOpen);
                dParam.AddParameter(TREECALENDARDETAILID_PA, sccpn.TreeCalendarDetailID);
                dParam.AddParameter(TREECALENDARMASTERID_PA, sccpn.TreeCalendarMasterID);
                dParam.AddParameter(TREEMASTERID_PA, sccpn.TreeMasterID);
                dParam.AddParameter(CALENDARMASTERNAME_PA, sccpn.CalendarMasterName);
                dParam.AddParameter(CALENDARDETAILNAME_PA, sccpn.CalendarDetailName);
                dParam.AddParameter(FORMALCODE_PA, sccpn.FormalCode);
                dParam.AddParameter(VERSIONCODE_PA, sccpn.VersionCode);
                dParam.AddParameter(COURSETITLE_PA, sccpn.CourseTitle);
                dParam.AddParameter(NODELINKNAME_PA, sccpn.NodeLinkName);
                dParam.AddParameter(PRIORITY_PA, sccpn.Priority);
                dParam.AddParameter(RETAKENO_PA, sccpn.RetakeNo);
                dParam.AddParameter(OBTAINEDGPA_PA, sccpn.ObtainedGPA);
                dParam.AddParameter(OBTAINEDGRADE_PA, sccpn.ObtainedGrade);
                dParam.AddParameter(ACACALYEAR_PA, sccpn.AcaCalYear);
                dParam.AddParameter(BATCHCODE_PA, sccpn.BatchCode);
                dParam.AddParameter(ACACALTYPENAME_PA, sccpn.AcaCalTypeName);
                dParam.AddParameter(MODIFIERID_PA, sccpn.ModifierID);
                dParam.AddParameter(MOIDFIEDDATE_PA, sccpn.ModifiedDate);
                dParam.AddParameter(ACACAL_SECTIONID_PA, sccpn.AcaCal_SectionID);
                dParam.AddParameter(SECTIONNAME_PA, sccpn.SectionName);
                dParam.AddParameter(COURSEID_PA, sccpn.CourseID);
                dParam.AddParameter(VERSIONID_PA, sccpn.VersionID);
                dParam.AddParameter(NODEID_PA, sccpn.NodeID);
                dParam.AddParameter(PROGRAMID_PA, sccpn.ProgramID);
                dParam.AddParameter(DEPTID_PA, sccpn.DeptID);
                dParam.AddParameter(ACADEMICCALENDERID_PA, sccpn.AcademicCalenderID);
                dParam.AddParameter(ISMULTIPLEACUSPAN_PA, sccpn.IsMultipleACUSpan);
                dParam.AddParameter(COURSECREDIT_PA, sccpn.CourseCredit);
                dParam.AddParameter(COMPLETEDCREDIT_PA, sccpn.CompletedCredit);
                dParam.AddParameter(ID_PA, sccpn.Id);

                List<SqlParameter> sqlParams = Methods.GetSQLParameters(dParam);

                i += QueryHandler.ExecuteSaveBatchAction(command, sqlParams, sqlConn, sqlTran);

            }

            MSSqlConnectionHandler.CommitTransaction();
            MSSqlConnectionHandler.CloseDbConnection(); //Close DB Connection

            return i;
        }

        public static int UpdateRequisitionData(List<Student_CalCourseProgNodeEntity> _sccpnEntities)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();
            DAOParameters dParam;// = new DAOParameters();
            int i = 0;
            string command = UPDATE;

            foreach (Student_CalCourseProgNodeEntity sccpn in _sccpnEntities)
            {
                dParam = new DAOParameters();

                dParam.AddParameter(STUDENTID_PA, sccpn.StudentID);
                dParam.AddParameter(CALCOURSEPROGNODEID_PA, sccpn.CalCourseProgNodeID);
                dParam.AddParameter(ISCOMPLETED_PA, sccpn.IsCompleted);
                dParam.AddParameter(ORIGINALCALID_PA, sccpn.OriginalCalID);
                dParam.AddParameter(ISAUTOASSIGN_PA, sccpn.IsAutoAssign);
                dParam.AddParameter(ISAUTOOPEN_PA, sccpn.IsAutoOpen);
                dParam.AddParameter(ISREQUISITIONED_PA, sccpn.IsRequisitioned);
                dParam.AddParameter(ISMANDATORY_PA, sccpn.IsMandatory);
                dParam.AddParameter(ISMANUALOPEN_PA, sccpn.IsManualOpen);
                dParam.AddParameter(TREECALENDARDETAILID_PA, sccpn.TreeCalendarDetailID);
                dParam.AddParameter(TREECALENDARMASTERID_PA, sccpn.TreeCalendarMasterID);
                dParam.AddParameter(TREEMASTERID_PA, sccpn.TreeMasterID);
                dParam.AddParameter(CALENDARMASTERNAME_PA, sccpn.CalendarMasterName);
                dParam.AddParameter(CALENDARDETAILNAME_PA, sccpn.CalendarDetailName);
                dParam.AddParameter(FORMALCODE_PA, sccpn.FormalCode);
                dParam.AddParameter(VERSIONCODE_PA, sccpn.VersionCode);
                dParam.AddParameter(COURSETITLE_PA, sccpn.CourseTitle);
                dParam.AddParameter(NODELINKNAME_PA, sccpn.NodeLinkName);
                dParam.AddParameter(PRIORITY_PA, sccpn.Priority);
                dParam.AddParameter(RETAKENO_PA, sccpn.RetakeNo);
                dParam.AddParameter(OBTAINEDGPA_PA, sccpn.ObtainedGPA);
                dParam.AddParameter(OBTAINEDGRADE_PA, sccpn.ObtainedGrade);
                dParam.AddParameter(ACACALYEAR_PA, sccpn.AcaCalYear);
                dParam.AddParameter(BATCHCODE_PA, sccpn.BatchCode);
                dParam.AddParameter(ACACALTYPENAME_PA, sccpn.AcaCalTypeName);
                dParam.AddParameter(MODIFIERID_PA, sccpn.ModifierID);
                dParam.AddParameter(MOIDFIEDDATE_PA, sccpn.ModifiedDate);
                dParam.AddParameter(ACACAL_SECTIONID_PA, sccpn.AcaCal_SectionID);
                dParam.AddParameter(SECTIONNAME_PA, sccpn.SectionName);
                dParam.AddParameter(COURSEID_PA, sccpn.CourseID);
                dParam.AddParameter(VERSIONID_PA, sccpn.VersionID);
                dParam.AddParameter(NODE_COURSEID_PA, sccpn.Node_CourseID);
                dParam.AddParameter(NODEID_PA, sccpn.NodeID);
                dParam.AddParameter(PROGRAMID_PA, sccpn.ProgramID);
                dParam.AddParameter(DEPTID_PA, sccpn.DeptID);
                dParam.AddParameter(ACADEMICCALENDERID_PA, sccpn.AcademicCalenderID);
                dParam.AddParameter(ISMULTIPLEACUSPAN_PA, sccpn.IsMultipleACUSpan);
                dParam.AddParameter(COURSECREDIT_PA, sccpn.CourseCredit);
                dParam.AddParameter(COMPLETEDCREDIT_PA, sccpn.CompletedCredit);
                dParam.AddParameter(ID_PA, sccpn.Id);

                List<SqlParameter> sqlParams = Methods.GetSQLParameters(dParam);

                i += QueryHandler.ExecuteSaveBatchAction(command, sqlParams, sqlConn, sqlTran);

            }

            MSSqlConnectionHandler.CommitTransaction();
            MSSqlConnectionHandler.CloseDbConnection(); //Close DB Connection

            return i;
        }

        public static int UndoRow(Student_CalCourseProgNodeEntity sccpn)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();
            DAOParameters dParam = new DAOParameters();

            int m = Section_DAO.DecreaseIfOccupied(sccpn.Id, sqlConn, sqlTran);

            string command = UPDATE;

            dParam.AddParameter(STUDENTID_PA, sccpn.StudentID);
            dParam.AddParameter(CALCOURSEPROGNODEID_PA, sccpn.CalCourseProgNodeID);
            dParam.AddParameter(ISCOMPLETED_PA, sccpn.IsCompleted);
            dParam.AddParameter(ORIGINALCALID_PA, sccpn.OriginalCalID);
            dParam.AddParameter(ISAUTOASSIGN_PA, sccpn.IsAutoAssign);
            dParam.AddParameter(ISAUTOOPEN_PA, sccpn.IsAutoOpen);
            dParam.AddParameter(ISREQUISITIONED_PA, sccpn.IsRequisitioned);
            dParam.AddParameter(ISMANDATORY_PA, sccpn.IsMandatory);
            dParam.AddParameter(ISMANUALOPEN_PA, sccpn.IsManualOpen);
            dParam.AddParameter(TREECALENDARDETAILID_PA, sccpn.TreeCalendarDetailID);
            dParam.AddParameter(TREECALENDARMASTERID_PA, sccpn.TreeCalendarMasterID);
            dParam.AddParameter(TREEMASTERID_PA, sccpn.TreeMasterID);
            dParam.AddParameter(CALENDARMASTERNAME_PA, sccpn.CalendarMasterName);
            dParam.AddParameter(CALENDARDETAILNAME_PA, sccpn.CalendarDetailName);
            dParam.AddParameter(FORMALCODE_PA, sccpn.FormalCode);
            dParam.AddParameter(VERSIONCODE_PA, sccpn.VersionCode);
            dParam.AddParameter(COURSETITLE_PA, sccpn.CourseTitle);
            dParam.AddParameter(NODELINKNAME_PA, sccpn.NodeLinkName);
            dParam.AddParameter(PRIORITY_PA, sccpn.Priority);
            dParam.AddParameter(RETAKENO_PA, sccpn.RetakeNo);
            dParam.AddParameter(OBTAINEDGPA_PA, sccpn.ObtainedGPA);
            dParam.AddParameter(OBTAINEDGRADE_PA, sccpn.ObtainedGrade);
            dParam.AddParameter(ACACALYEAR_PA, sccpn.AcaCalYear);
            dParam.AddParameter(BATCHCODE_PA, sccpn.BatchCode);
            dParam.AddParameter(ACACALTYPENAME_PA, sccpn.AcaCalTypeName);
            dParam.AddParameter(MODIFIERID_PA, sccpn.ModifierID);
            dParam.AddParameter(MOIDFIEDDATE_PA, sccpn.ModifiedDate);
            dParam.AddParameter(ACACAL_SECTIONID_PA, sccpn.AcaCal_SectionID);
            dParam.AddParameter(SECTIONNAME_PA, sccpn.SectionName);
            dParam.AddParameter(COURSEID_PA, sccpn.CourseID);
            dParam.AddParameter(VERSIONID_PA, sccpn.VersionID);
            dParam.AddParameter(NODE_COURSEID_PA, sccpn.Node_CourseID);
            dParam.AddParameter(NODEID_PA, sccpn.NodeID);
            dParam.AddParameter(PROGRAMID_PA, sccpn.ProgramID);
            dParam.AddParameter(DEPTID_PA, sccpn.DeptID);
            dParam.AddParameter(ACADEMICCALENDERID_PA, sccpn.AcademicCalenderID);
            dParam.AddParameter(ISMULTIPLEACUSPAN_PA, sccpn.IsMultipleACUSpan);
            dParam.AddParameter(COURSECREDIT_PA, sccpn.CourseCredit);
            dParam.AddParameter(COMPLETEDCREDIT_PA, sccpn.CompletedCredit);
            dParam.AddParameter(ID_PA, sccpn.Id);

            List<SqlParameter> sqlParams = Methods.GetSQLParameters(dParam);

            int i = QueryHandler.ExecuteSaveBatchAction(command, sqlParams, sqlConn, sqlTran);

            MSSqlConnectionHandler.CommitTransaction();
            MSSqlConnectionHandler.CloseDbConnection(); //Close DB Connection

            return i;
        }

        
    }
}
