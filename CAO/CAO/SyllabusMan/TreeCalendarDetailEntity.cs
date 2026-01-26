using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class TreeCalendarDetailEntity:BaseEntity
    {
        #region Variables
        private int _treeCalendarMasterID;
        //private TreeCalendarMaster _treeCalendarMaster;
        private int _treeMasterID;
        //private TreeMaster _treeMaster;
        private int _calendarMasterID;
        //private CalendarUnitMaster _calendarMaster;
        private int _calendarDetailID;
        //private CalenderUnitDistribution _calendarDetail;
        //private List<Cal_Course_Prog_Node> _cal_Course_Prog_Nodes;
        #endregion

        #region Properties
        public int TreeCalendarMasterID
        {
            get { return _treeCalendarMasterID; }
            set { _treeCalendarMasterID = value; }
        }
        //public TreeCalendarMaster TreeCalendarMaster
        //{
        //    get
        //    {
        //        if (_treeCalendarMaster == null)
        //        {
        //            if (this.TreeCalendarMasterID > 0)
        //            {
        //                _treeCalendarMaster = TreeCalendarMaster.Get(this.TreeCalendarMasterID);
        //            }
        //        }
        //        return _treeCalendarMaster;
        //    }
        //    set { _treeCalendarMaster = value; }
        //}

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
        //    private set { _treeMaster = value; }
        //}

        public int CalendarMasterID
        {
            get { return _calendarMasterID; }
            set { _calendarMasterID = value; }
        }
        //public CalendarUnitMaster CalendarMaster
        //{
        //    get
        //    {
        //        if (_calendarMaster == null)
        //        {
        //            if (this.CalendarMasterID > 0)
        //            {
        //                _calendarMaster = CalendarUnitMaster.GetCalendarMaster(this.CalendarMasterID);
        //            }
        //        }
        //        return _calendarMaster;
        //    }
        //    private set { _calendarMaster = value; }
        //}

        public int CalendarDetailID
        {
            get { return _calendarDetailID; }
            set { _calendarDetailID = value; }
        }
        //public CalenderUnitDistribution CalendarDetail
        //{
        //    get
        //    {
        //        if (_calendarDetail == null)
        //        {
        //            if (this.CalendarDetailID > 0)
        //            {
        //                _calendarDetail = CalenderUnitDistribution.GetCalendarDetail(this.CalendarDetailID);
        //            }
        //        }
        //        return _calendarDetail;
        //    }
        //    private set { _calendarDetail = value; }
        //}

        //public List<Cal_Course_Prog_Node> Cal_Course_Prog_Nodes
        //{
        //    get
        //    {
        //        if (_cal_Course_Prog_Nodes == null)
        //        {
        //            if (this.Id > 0)
        //            {
        //                _cal_Course_Prog_Nodes = Cal_Course_Prog_Node.GetByTreeCalDet(this.Id);
        //            }
        //        }
        //        return _cal_Course_Prog_Nodes;
        //    }
        //}
        #endregion

        #region Constructor
        public TreeCalendarDetailEntity():base()
        {
            _treeMasterID = 0;
            //_treeMaster = null;
            _calendarMasterID = 0;
            //_calendarMaster = null;
            //_cal_Course_Prog_Nodes = null;
            _treeCalendarMasterID = 0;
            _calendarDetailID = 0;
        } 
        #endregion
    }
}
