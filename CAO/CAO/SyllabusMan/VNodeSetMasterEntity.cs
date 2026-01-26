using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class VNodeSetMasterEntity:BaseEntity
    {
        #region DBcloumns
        //VNodeSetMasterID	int	        Unchecked
        //SetNo	            int	        Unchecked
        //NodeID	        int	        Unchecked
        //RequiredUnits	    money	    Checked
        //CreatedBy	        int	        Unchecked
        //CreatedDate	    datetime	Unchecked
        //ModifiedBy	    int	        Checked
        //ModifiedDate	    datetime	Checked
        #endregion

        #region Variables
        private decimal _requiredUnits;
        private int _ownerNodeID;
        //private Node _ownerNode;

        private int _setNo;
        //private List<VNodeSet> _vNodeSets;
        #endregion

        #region Constructor
        public VNodeSetMasterEntity()
            : base()
        {
            _requiredUnits = 0;
            _ownerNodeID = 0;
            //_ownerNode = null;

            _setNo = 0;
            //_vNodeSets = null;
        } 
        #endregion
        #region Properties

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
        
        public string SetName
        {
            get
            {
                return "Set " + _setNo.ToString();
            }
        }

        public decimal RequiredUnits
        {
            get { return _requiredUnits; }
            set { _requiredUnits = value; }
        }
        
        #endregion
    }
}
