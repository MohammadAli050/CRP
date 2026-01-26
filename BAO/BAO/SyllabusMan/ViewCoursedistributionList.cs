using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;
using System.Data;
using System.Collections;
using BussinessObject.SyllabusMan;

namespace BussinessObject
{
    public class ViewCoursedistributionList
    {
        #region Constant Variables
        private const string STMT_SELECT_1 = "select Node.NodeID, Node.[Name] ProgStructureName, "+
                                             "tab.ParentNodeID, Node.IsLastLevel,Node.MinCredit, "+
                                             "Node.MaxCredit, Node.MinCourses, Node.MaxCourse, "+
                                             "Node.IsVirtual, Node.IsBundle, Node.StartTrimesterID, "+
                                             "Node.OperatorID, Node.OperandNodes "+
                                             "from Node, ("+
                                                            "select ChildNodeID, ParentNodeID "+
                                                            "from TreeDetail where TreeMasterID = ";
        private const string STMT_SELECT_2 = ")tab "+
                                             "where Node.NodeID=tab.ChildNodeID";
        private const string STMT_COURSE_SELECT_1 = "select tab.NodeID, tab.ProgStructName, Course.CourseID, Course.VersionID, " +
                                                    "Course.FormalCode, Course.VersionCode, Course.Title, tab.PassingGPA, tab.Priority, "+
                                                    "Course.AssocCourseID, Course.AssocVersionID, Course.StartTrimesterID, Course.ProgramID, Course.CourseContent, "+
                                                    "Course.IsCreditCourse, Course.Credits, Course.IsThesis, Course.IsProject, Course.HasEquivalents "+
                                                    "from (select nc.*, Node.[Name] ProgStructName from Node_Course nc, Node where nc.NodeID = ";

        private const string STMT_COURSE_SELECT_2 = " and nc.NodeID = Node.NodeID)tab, Course " +
                                                    "where tab.CourseID = Course.CourseID and tab.VersionID = Course.VersionID "+
                                                    "order by Course.FormalCode, Course.VersionCode";
        private const string STMT_VNODE_SELECT_1 = "select vn.VNodeSetID, vn.NodeID, vn.SetNo, vn.OperandNodeID, vn.OperandCourseID, vn.OperandVersionID, opID.[Name] OperatorName, vn.WildCard, vn.IsStudntSpec "+
                                                   "from VNodeSet vn, "+
                                                            "(select * from Operator)opID "+
                                                   "where vn.NodeID = ";
        private const string STMT_VNODE_SELECT_2 = " and vn.OperatorID = opID.OperatorID "+
                                                    "order by SetNo";
        private const string STMT_VNODE_COURSE_SELECT_1 = "select Course.CourseID, Course.VersionID, "+
                                                           "Course.FormalCode, Course.VersionCode, Course.Title, "+
                                                            "Course.AssocCourseID, Course.AssocVersionID, Course.StartTrimesterID, "+
                                                            "Course.ProgramID, Course.CourseContent,Course.IsCreditCourse, "+
                                                            "Course.Credits, Course.IsThesis, Course.IsProject, Course.HasEquivalents "+
                                                            "from Course where CourseID = ";

        private const string STMT_VNODE_COURSE_SELECT_2 = " and VersionID = "; 
        #endregion

        #region Static Variables
        private static ViewCourseDistribution ds = new ViewCourseDistribution();
        private static SortedList sList = new SortedList();
        private static ArrayList arrListVnode = new ArrayList();
        private static bool boolCourseAdded = false;
        #endregion

        #region Methods
        /// <summary>
        /// date: dec 15, 2009
        /// programmer: SaimaH
        /// Description: getting all nodes and courses of that node
        /// </summary>
        /// <param name="treeMasterID"></param>
        /// <returns></returns>
        public static ViewCourseDistribution GetListViewofCourses(string treeMasterID)
        {
            try
            {
                ds.ProgStruct.Clear();
                ds.ProgStructCourse.Clear();
                ds.VnodeSet.Clear();

                GetDataFromServer(STMT_SELECT_1, treeMasterID, STMT_SELECT_2, ds, ds.ProgStruct);
                if (ds.ProgStruct.Rows.Count != 0)
                {
                    GetHierarchy();
                    GetHierarchyCourse();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        private static void GetDataFromServer(string strSelectStmt1, string strSearchValue, string strSelectStmt2, ViewCourseDistribution ds, ViewCourseDistribution.ProgStructDataTable dt)
        {
            try
            {
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                string command = strSelectStmt1 + strSearchValue + strSelectStmt2;
                SqlDataAdapter adapter = new SqlDataAdapter(command, sqlConn);
                adapter.Fill(ds, dt.ToString());
                MSSqlConnectionHandler.CloseDbConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static void GetDataFromServer(string strSelectStmt1, string strSearchValue, string strSelectStmt2, ViewCourseDistribution ds, ViewCourseDistribution.VnodeSetDataTable dt)
        {
            try
            {
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                string command = strSelectStmt1 + strSearchValue + strSelectStmt2;
                SqlDataAdapter adapter = new SqlDataAdapter(command, sqlConn);
                adapter.Fill(ds, dt.ToString());
                MSSqlConnectionHandler.CloseDbConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static void GetDataFromServer(string strSelectStmt1, string strSearchValue1, string strSelectStmt2, ViewCourseDistribution ds, ViewCourseDistribution.ProgStructCourseDataTable dt, string strSearchValue2)
        {
            try
            {                
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                string command;
                if (strSearchValue2.Equals(string.Empty))
                {
                    command = strSelectStmt1 + strSearchValue1 + strSelectStmt2;
                }
                else
                {
                    command = strSelectStmt1 + strSearchValue1 + strSelectStmt2 + strSearchValue2;
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command, sqlConn);
                adapter.Fill(ds, dt.ToString());
                MSSqlConnectionHandler.CloseDbConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static bool CheckExists(DataRow dr)
        {
            foreach (DictionaryEntry obj in sList)
            {
                DataRow[] drTemp = (DataRow[])obj.Value;
                foreach (DataRow r in drTemp)
                {
                    if (r[0].ToString() == dr[0].ToString())
                    {
                        foreach (DictionaryEntry dic in sList)
                        {
                            if (dic.Key.ToString() == dr[0].ToString())
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        private static void Manipulation(DataRow dr)
        {
            if (!CheckExists(dr))
            {
                if (dr[3].Equals(false))
                {
                    if (dr[8].Equals(false))
                    {
                        boolCourseAdded = false;
                        GetData(dr);
                    }
                    else
                    {
                        for (int i = 0; i < arrListVnode.Count; i++)
                        {
                            GetSetData(arrListVnode[i].ToString());
                        }
                        if (ds.VnodeSet.Rows.Count != 0)
                        {
                            GetNodeCourse();
                        }
                    }
                }
                else
                {
                    if (!boolCourseAdded)
                    {
                        GetCourse(dr[0].ToString());
                    }
                }
            } 
        }
        private static void GetHierarchyCourse()
        {
            try
            {
                boolCourseAdded = false;
                foreach (DataRow dr in ds.ProgStruct.Rows)
                {
                    Manipulation(dr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static void GetData(DataRow dr)
        {
            try
            {
                bool boolFlag = false;
                foreach (DictionaryEntry obj in sList)
                {
                    if (obj.Key.ToString() == dr[0].ToString())
                    {
                        boolFlag = true;

                        DataRow row = ds.ProgStructCourse.NewRow();
                        row[0] = dr[0];
                        row[1] = dr[1];
                        ds.ProgStructCourse.Rows.Add(row);

                        DataRow[] rowArr = (DataRow[])obj.Value;
                        foreach (DataRow r in rowArr)
                        {
                            GetData(r);
                        }
                        break;
                    }
                }
                if (!boolFlag)
                {
                    GetCourse(dr[0].ToString());
                    boolCourseAdded = true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private static void GetHierarchy()
        {
            sList.Clear();
            arrListVnode.Clear();
            try
            {
                foreach(DataRow dr in ds.ProgStruct)
                {
                    DataRow[] drFoundRows;
                    if (dr[3].Equals(false))
                    {
                        if(dr[8].Equals(false))
                        {
                            drFoundRows = ds.ProgStruct.Select("ParentNodeID = " + dr[0].ToString());
                            if (drFoundRows.Length != 0)
                            {
                                sList.Add(dr[0].ToString(), drFoundRows);
                            }
                        }                        
                        else 
                        {
                            arrListVnode.Add(dr[0].ToString());
                        }                                 
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private static void GetCourse(string strNodeID)
        {
            try
            {
                GetDataFromServer(STMT_COURSE_SELECT_1, strNodeID, STMT_COURSE_SELECT_2, ds, ds.ProgStructCourse, string.Empty);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private static void GetSetData(string strVirtualNodeID)
        {
            try
            {
                GetDataFromServer(STMT_VNODE_SELECT_1, strVirtualNodeID, STMT_VNODE_SELECT_2, ds, ds.VnodeSet);                
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        private static void GetNodeCourse()
        {
            try
            {                
                for (int i = 0; i < arrListVnode.Count; i++)
                {
                    DataRow rw = ds.ProgStructCourse.NewRow();
                    DataRow[] drRow = ds.ProgStruct.Select("NodeID = " + arrListVnode[i].ToString());
                    rw[0] = drRow[0].ItemArray.GetValue(0);
                    rw[1] = drRow[0].ItemArray.GetValue(1);
                    ds.ProgStructCourse.Rows.Add(rw);
                
                    foreach (DataRow drVnode in ds.VnodeSet)
                    {
                        DataRow row = ds.ProgStructCourse.NewRow();
                        row[0] = drVnode[1];
                        row[1] = ds.VnodeSet.Columns[2].Caption + drVnode[2] + "-" + drVnode[6].ToString();
                        ds.ProgStructCourse.Rows.Add(row);

                        if (drVnode[3].ToString()==string.Empty)
                        {
                            try
                            {
                                GetDataFromServer(STMT_VNODE_COURSE_SELECT_1, drVnode[4].ToString(), STMT_VNODE_COURSE_SELECT_2, ds, ds.ProgStructCourse, drVnode[5].ToString());                                
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        else
                        {
                            DataRow[] drVnodeArr = ds.ProgStruct.Select("NodeID = " + drVnode[3].ToString());
                            for (int j = 0; j < drVnodeArr.Length; j++)
                            {
                                boolCourseAdded = false;
                                Manipulation(drVnodeArr[j]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }    
        }

        #endregion
    }
}
