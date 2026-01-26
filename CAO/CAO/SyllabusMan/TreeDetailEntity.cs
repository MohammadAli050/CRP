using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class TreeDetailEntity:BaseEntity
    {
        #region Variables
        private int _treeMasterID;
        //private TreeMaster _treeMaster;
        private int _parentNodeID;
        //private Node _parentNode;
        private int _childNodeID;
        //private Node _childNode;
        #endregion

        #region Constructor
        public TreeDetailEntity()
            : base()
        {
            _treeMasterID = 0;
            _parentNodeID = 0;
            //_parentNode = null;
            _childNodeID = 0;
            //_childNode = null;
        } 
        #endregion

        #region Properties

        public int TreeMasterID
        {
            get { return _treeMasterID; }
            set { _treeMasterID = value; }
        }
        //public TreeMaster TreeMaster
        //{
        //    get
        //    {
        //        if (_treeMaster == null)
        //        {
        //            if (this.TreeMasterID > 0)
        //            {
        //                _treeMaster = TreeMaster.Get(this.TreeMasterID);
        //            }
        //        }
        //        return _treeMaster;
        //    }
        //}

        public int ParentNodeID
        {
            get { return _parentNodeID; }
            set { _parentNodeID = value; }
        }
        //public Node ParentNode
        //{
        //    get
        //    {
        //        if (_parentNode == null)
        //        {
        //            if (this.ParentNodeID > 0)
        //            {
        //                _parentNode = Node.GetNode(this.ParentNodeID);
        //            }
        //        }
        //        return _parentNode;
        //    }
        //}

        public int ChildNodeID
        {
            get { return _childNodeID; }
            set { _childNodeID = value; }
        }
        //public Node ChildNode
        //{
        //    get
        //    {
        //        if (_childNode == null)
        //        {
        //            if (this.ChildNodeID > 0)
        //            {
        //                _childNode = Node.GetNode(this.ChildNodeID);
        //            }
        //        }
        //        return _childNode;
        //    }
        //}
        #endregion
    }
}
