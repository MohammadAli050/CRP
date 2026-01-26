using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Collections;

namespace DataAccess
{
    public class Methods_DAO
    {
        /// <summary>
        /// created by saima
        /// description: step wise break down
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="strType"></param>
        /// <returns></returns>
        public static Hashtable GetChildren(int ID, string strType)
        {
            Hashtable hs = null;
            MSSqlConnectionHandler.GetConnection();
            if (strType == "NOD")
            {
                NodeEntity node = Node_DAO.GetNode(ID);

                if (node == null)
                {
                    return hs;
                }
                if (node.IsLastLevel)
                {
                    //find node coures
                    List<CourseEntity> courses = Course_DAO.GetCoursesByNode(ID);
                    if (courses == null)
                    {
                        return hs;
                    }
                    hs = new Hashtable();
                    hs.Add("Course", courses);
                }
                else if (node.IsVirtual)
                {
                    List<VNodeSetMasterEntity> vsMases = VNodeSetMaster_DAO.GetByOwnerNode(ID);
                    if (vsMases == null)
                    {
                        return hs;
                    }
                    hs.Add("VNodeSetMaster", vsMases);                    
                }
                else
                {
                    //find child nodes
                    List<NodeEntity> nodes = Node_DAO.GetChildrenByParentNode(ID);
                    if (nodes == null)
                    {
                        return hs;
                    }
                    hs.Add("NOD", nodes);
                }
            }
            else if (strType == "SETMAS")
            {
                List<VNodeSetEntity> sets = VNodeSet_DAO.GetByMasterID(ID);
                if (sets == null)
                {
                    return hs;
                }

                List<NodeEntity> nodes = new List<NodeEntity>();
                List<CourseEntity> courses = new List<CourseEntity>();
                foreach (VNodeSetEntity ve in sets)
                {
                    if (ve.OperandNodeID != 0)
                    {
                        NodeEntity nd = Node_DAO.GetNode(ve.OperandNodeID);
                        if (nd != null)
                        {
                            nodes.Add(nd);
                        }
                    }
                    else
                    {
                        CourseEntity cs = Course_DAO.GetCourse(ve.OperandCourseID, ve.OperandVersionID);
                        if (cs != null)
                        {
                            courses.Add(cs);
                        }
                    }
                }
                hs.Add("SETMAS", sets);
                if (nodes.Count > 0)
                {
                    hs.Add("NOD", nodes);
                }
                if (courses.Count > 0)
                {
                    hs.Add("COURSE", courses);
                }
            }
            else if (strType == "VNODSET")
            {
                List<NodeEntity> nodes = null;
                CourseEntity cs = null;
                VNodeSetEntity set = VNodeSet_DAO.Get(ID);
                if (set.OperandNodeID != 0)
                {
                    nodes = Node_DAO.GetChildrenByParentNode(set.OperandNodeID);
                }
                else
                {
                    cs = Course_DAO.GetCourse(set.OperandCourseID, set.OperandVersionID);
                }
                hs.Add("VNODSET", set);
                if (nodes != null)
                {
                    hs.Add("NOD", nodes);
                }
                if (cs != null)
                {
                    hs.Add("Course", cs);
                }
            }
            MSSqlConnectionHandler.CloseDbConnection();
            return hs;
        }
        //private static void GetChildren(int id, string strType)
        //{
        //    MSSqlConnectionHandler.GetConnection();

        //    if (strType == "Node")
        //    {

        //    }
        //    else if (strType == "Vnode")
        //    {

        //    }

        //    MSSqlConnectionHandler.CloseDbConnection();
        //}
        public static Hashtable GetPreRequisits(int nodeID, int progID)
        {
            try
            {
                Hashtable hs = null;
                MSSqlConnectionHandler.GetConnection();

                List<PreReqDetailEntity> details = PreReqDetail_DAO.GetPreReqDetails(nodeID, progID);
                if (details == null)
                {
                    return hs;
                }
                hs = GetResultantPreRequisits(details);

                MSSqlConnectionHandler.CloseDbConnection();
                return hs;
            }
            catch (Exception ex)
            {
                //FixMe
                MSSqlConnectionHandler.CloseDbConnection();
                throw ex;
            }
        }
        public static Hashtable GetPreRequisits(int nodeID, int courseID, int versionID, int progID)
        {
            try
            {
                Hashtable hs = null;
                MSSqlConnectionHandler.GetConnection();

                List<PreReqDetailEntity> details = PreReqDetail_DAO.GetPreReqDetails(nodeID, courseID, versionID, progID);
                if (details == null)
                {
                    return hs;
                }
                hs = GetResultantPreRequisits(details);

                MSSqlConnectionHandler.CloseDbConnection();

                return hs;
            }
            catch (Exception ex)
            {
                //FixMe
                MSSqlConnectionHandler.CloseDbConnection();
                throw ex;
            }

        }

        private static Hashtable GetResultantPreRequisits(List<PreReqDetailEntity> details)
        {
            Hashtable hs = null;
            foreach (PreReqDetailEntity ent in details)
            {
                if (ent.NodeCourseID != 0)
                {
                    Utilities.StructCourse strctCourse = new Utilities.StructCourse();
                    strctCourse.Cs = Course_DAO.GetCourseByNodeCourse(ent.PreReqNodeCourseID);
                    strctCourse.Op = Operator_DAO.GetOperator(ent.OperatorID);
                    strctCourse.OpMinOccurence = ent.OperatorIDMinOccurance;
                    strctCourse.ReqCredits = ent.ReqCredits;

                    if (hs == null)
                    {
                        hs = new Hashtable();
                    }
                    hs.Add("Course#" + ent.PreReqNodeCourseID, strctCourse);
                }
                else if (ent.Node_ID != 0)
                {
                    Utilities.StructNode strctNode = new Utilities.StructNode();
                    strctNode.Node = Node_DAO.GetNode(ent.PreReqNodeID);
                    strctNode.Op = Operator_DAO.GetOperator(ent.OperatorID);
                    strctNode.OpMinOccurence = ent.OperatorIDMinOccurance;
                    strctNode.ReqCredits = ent.ReqCredits;

                    if (hs == null)
                    {
                        hs = new Hashtable();
                    }
                    hs.Add("Node#" + ent.PreReqNodeID, strctNode);
                }
            }
            return hs;
        }
    }
}
