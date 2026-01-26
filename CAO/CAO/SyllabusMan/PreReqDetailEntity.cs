using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class PreReqDetailEntity : BaseEntity
    {
        #region DBColumns

        /*
         [PrerequisiteID] [int] IDENTITY(1,1) NOT NULL,
	    [PrerequisiteMasterID] [int] NULL,
	    [NodeCourseID] [int] NULL,
	    [PreReqNodeCourseID] [int] NULL,
	    [OperatorID] [int] NULL,
	    [OperatorIDMinOccurance] [int] NULL,
	    [ReqCredits] [numeric](18, 2) NULL,
	    [NodeID] [int] NULL,
	    [PreReqNodeID] [int] NULL,
	    [CreatedBy] [int] NOT NULL,
	    [CreatedDate] [datetime] NOT NULL,
	    [ModifiedBy] [int] NULL,
	    [ModifiedDate] [datetime] NULL,
        */

        #endregion

        #region Variables

        private int _prereqMasterID;
        private int _nodeCourseID;
        private int _preReqNodeCourseID;
        private int _operatorID;
        private int _operatorIDMinOccurance;
        private decimal _reqCredits;
        private int _nodeID;
        private int _preReqNodeID;
        //private Node_Course _nodeCourse = null;
        //private Node _node = null;
        //private Operator _operator = null;

        #endregion

        #region Properties
        //public Node_Course NodeCourse
        //{
        //    get
        //    {
        //        if (_nodeCourse == null)
        //        {
        //            _nodeCourse = Node_Course.GetNodeCourse(PreReqNodeCourseID);
        //        }
        //        return _nodeCourse;
        //    }
        //}
        //public Node Node
        //{
        //    get
        //    {
        //        if (_node == null)
        //        {
        //            _node = Node.GetNode(PreReqNodeID);
        //        }
        //        return _node;
        //    }
        //}
        //public Operator Operator
        //{
        //    get
        //    {
        //        if (_operator == null)
        //        {
        //            _operator = Operator.GetOperator(_operatorID);
        //        }
        //        return _operator;
        //    }
        //}

        public int PrereqMasterID
        {
            get { return _prereqMasterID; }
            set { _prereqMasterID = value; }
        }

        public int NodeCourseID
        {
            get { return _nodeCourseID; }
            set { _nodeCourseID = value; }
        }

        public int PreReqNodeCourseID
        {
            get { return _preReqNodeCourseID; }
            set { _preReqNodeCourseID = value; }
        }

        public int OperatorID
        {
            get { return _operatorID; }
            set { _operatorID = value; }
        }

        public int OperatorIDMinOccurance
        {
            get { return _operatorIDMinOccurance; }
            set { _operatorIDMinOccurance = value; }
        }

        public decimal ReqCredits
        {
            get { return _reqCredits; }
            set { _reqCredits = value; }
        }

        public int Node_ID
        {
            get { return _nodeID; }
            set { _nodeID = value; }
        }

        public int PreReqNodeID
        {
            get { return _preReqNodeID; }
            set { _preReqNodeID = value; }
        }

        #endregion
    }
}
