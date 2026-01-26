using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class VNodeSetEntity:BaseEntity
    {            
        #region Variables
        private int _vNodeSetMasID;
        //private VNodeSetMaster _vNodeSetMaster;

        private int _ownerNodeID;
        //private Node _ownerNode;

        private int _setNo;
        private int _operatorID;
        //private OperatorEntity _operator;

        private int _operandNodeID;
        //private Node _operandNode;

        private int _operandCourseID;
        private int _operandVersionID;
        //private Course _operandCourse;


        private int _nodeCourseID;
        //private Node_Course _nodeCourse;

        private string _wildCard;
        private bool _isStudntSpec;
        #endregion

        #region Constructor
        public VNodeSetEntity()
            : base()
        {
            _vNodeSetMasID = 0;
            //_vNodeSetMaster = null;

            _ownerNodeID = 0;
            //_ownerNode = null;

            _setNo = 0;
            _operatorID = 0;

            _operandNodeID = 0;
            //_operandNode = null;

            _operandCourseID = 0;
            _operandVersionID = 0;
            //_operandCourse = null;

            _nodeCourseID = 0;
            //_nodeCourse = null;

            _wildCard = string.Empty;
            _isStudntSpec = false;
        }
        #endregion

        #region Properties
        public int VNodeSetMasID
        {
            get { return _vNodeSetMasID; }
            set { _vNodeSetMasID = value; }
        }
        //public VNodeSetMaster SetMaster
        //{
        //    get
        //    {
        //        if (_vNodeSetMaster == null)
        //        {
        //            if (this.VNodeSetMasID > 0)
        //            {
        //                _vNodeSetMaster = VNodeSetMaster.Get(this.VNodeSetMasID);
        //            }
        //        }
        //        return _vNodeSetMaster;
        //    }
        //}

        public int OwnerNodeID
        {
            get { return _ownerNodeID; }
            set { _ownerNodeID = value; }
        }
        //public Node OwnerNode
        //{
        //    get
        //    {
        //        if (_ownerNode == null)
        //        {
        //            if (this.OwnerNodeID > 0)
        //            {
        //                _ownerNode = Node.GetNode(this.OwnerNodeID);
        //            }
        //        }
        //        return _ownerNode;
        //    }
        //}

        public int SetNo
        {
            get { return _setNo; }
            set { _setNo = value; }
        }
        
        public int OperatorID
        {
            get { return _operatorID; }
            set { _operatorID = value; }
        }
        //public OperatorEntity Operator
        //{
        //    get
        //    {
        //        if (_operator == null)
        //        {
        //            if (this.OperatorID > 0)
        //            {
        //                _operator = Operator.GetOperator(this.OperatorID);
        //            }
        //        }
        //        return _operator;
        //    }
        //}

        public int OperandNodeID
        {
            get { return _operandNodeID; }
            set { _operandNodeID = value; }
        }
        //public Node OperandNode
        //{
        //    get
        //    {
        //        if (_operandNode == null)
        //        {
        //            if (this.OperandNodeID > 0)
        //            {
        //                _operandNode = Node.GetNode(this.OperandNodeID);
        //            }
        //        }
        //        return _operandNode;
        //    }
        //}
        
        public int OperandCourseID
        {
            get { return _operandCourseID; }
            set { _operandCourseID = value; }
        }
        public int OperandVersionID
        {
            get { return _operandVersionID; }
            set { _operandVersionID = value; }
        }
        //public Course OperandCourse
        //{
        //    get
        //    {
        //        if (_operandCourse == null)
        //        {
        //            if ((_operandCourseID > 0) && (_operandVersionID > 0))
        //            {
        //                _operandCourse = Course.GetCourse(_operandCourseID, _operandVersionID);
        //            }
        //        }
        //        return _operandCourse;
        //    }
        //}

        public int NodeCourseID
        {
            get { return _nodeCourseID; }
            set { _nodeCourseID = value; }
        }
        //public Node_Course NodeCourse
        //{
        //    get
        //    {
        //        if (_nodeCourse == null)
        //        {
        //            if (this._nodeCourseID > 0)
        //            {
        //                _nodeCourse = Node_Course.GetNodeCourse(this.NodeCourseID);
        //            }
        //        }
        //        return _nodeCourse;
        //    }
        //    private set { _nodeCourse = value; }
        //}

        public string WildCard
        {
            get { return _wildCard; }
            set { _wildCard = value; }
        }
        
        public bool IsStudntSpec
        {
            get { return _isStudntSpec; }
            set { _isStudntSpec = value; }
        }
        #endregion
    }
}
