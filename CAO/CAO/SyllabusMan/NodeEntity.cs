using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class NodeEntity:BaseEntity
    {
        #region DBColumns
        //NodeID	            int	            Unchecked
        //Name	                varchar(150)	Unchecked
        //IsLastLevel	        bit	            Unchecked
        //MinCredit	            numeric(18, 2)	Checked
        //MaxCredit	            numeric(18, 2)	Checked
        //MinCourses	        int	            Checked
        //MaxCourses	        int	            Checked
        //IsActive	            bit	            Unchecked
        //IsVirtual 	        bit	            Unchecked
        //IsBundle	            bit	            Unchecked
        //IsAssociated	        bit	            Unchecked
        //StartTrimesterID	    int	            Checked
        //OperatorID	        int	            Checked
        //OperandNodes	        int	            Checked
        //CreatedBy	            int	            Unchecked
        //CreatedDate	        datetime	    Unchecked
        //ModifiedBy	        int	            Checked
        //ModifiedDate	        datetime	    Checked
        #endregion

        #region Variables
        private string _name;
        private bool _isLastLevel;
        private decimal _minCredit;
        private decimal _maxCredit;
        private int _maxCourses;
        private int _minCourses;
        private bool _isVirtual;
        private bool _isBundle;
        private bool _isAssociated;
        private int _operatorID;
        private bool _isActive;
        //private OperatorEntity _operator;
        //private List<VNodeSet> _vNodeSets = null;
        //private List<VNodeSetMaster> _vNodeSetMases = null;
        //private List<Node_Course> _node_Courses = null;
        //private List<PreRequisiteMaster> _preReqMasters = null;
        //private bool _hasPreriquisite = false;
        #endregion

        #region Constructor
        public NodeEntity()
            : base()
        {
            _name = string.Empty;
            _isVirtual = false;
            _isBundle = false;
            _isLastLevel = false;
            _isAssociated = false;
            _minCredit = 0;
            _maxCredit = 0;
            _maxCourses = 0;
            _minCourses = 0;
            _operatorID = 0;
            _isActive = true;

            //_vNodeSets = null;
            //_vNodeSetMases = null;
            //_node_Courses = null;
            //_preReqMasters = null;
        }
        #endregion

        #region Properties
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        public bool IsLastLevel
        {
            get { return _isLastLevel; }
            set { _isLastLevel = value; }
        }
        
        public decimal MinCredit
        {
            get { return _minCredit; }
            set { _minCredit = value; }
        }
        
        public decimal MaxCredit
        {
            get { return _maxCredit; }
            set { _maxCredit = value; }
        }
        
        public int MaxCourses
        {
            get { return _maxCourses; }
            set { _maxCourses = value; }
        }
        
        public int MinCourses
        {
            get { return _minCourses; }
            set { _minCourses = value; }
        }
        
        public bool IsVirtual
        {
            get { return _isVirtual; }
            set { _isVirtual = value; }
        }
        
        public bool IsBundle
        {
            get { return _isBundle; }
            set { _isBundle = value; }
        }
        
        public bool IsAssociated
        {
            get { return _isAssociated; }
            set { _isAssociated = value; }
        }
        
        public int OperatorID
        {
            get { return _operatorID; }
            set { _operatorID = value; }
        }
        //public Operator Operator
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

        #region Is Active
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        #endregion

        //public List<VNodeSet> VNodeSets
        //{
        //    get
        //    {
        //        if (_vNodeSets == null)
        //        {
        //            if (this.Id > 0)
        //            {
        //                _vNodeSets = VNodeSet.GetByOwnerNode(this.Id);
        //            }
        //        }
        //        return _vNodeSets;
        //    }
        //}
        //public List<VNodeSetMaster> VNodeSetMasters
        //{
        //    get
        //    {
        //        if (_vNodeSetMases == null)
        //        {
        //            if (this.Id > 0)
        //            {
        //                _vNodeSetMases = VNodeSetMaster.GetByOwnerNode(this.Id);
        //            }
        //        }
        //        return _vNodeSetMases;
        //        //return VNodeSetMaster.GetVNodeSetMasters(this.VNodeSets);
        //    }
        //}
        //public List<Node_Course> Node_Courses
        //{
        //    get
        //    {
        //        if (_node_Courses == null)
        //        {
        //            if (this.Id > 0)
        //            {
        //                _node_Courses = Node_Course.GetByParentNode(this.Id);
        //            }
        //        }
        //        return _node_Courses;
        //    }
        //}

        //public List<PreRequisiteMaster> PreReqMasters
        //{
        //    get
        //    {
        //        if (_preReqMasters == null)
        //        {
        //            _preReqMasters = PreRequisiteMaster.GetMastersByNode(this.Id);
        //        }
        //        return _preReqMasters;
        //    }
        //    set { _preReqMasters = value; }
        //}

        //public bool HasPreriquisite
        //{
        //    get
        //    {
        //        if (_preReqMasters != null)
        //        {
        //            if (_preReqMasters.Count > 0)
        //            {
        //                _hasPreriquisite = true;
        //            }
        //        }
        //        return _hasPreriquisite;
        //    }
        //    set { _hasPreriquisite = value; }
        //}
        #endregion

    }
}
