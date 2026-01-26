using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class TreeCalendarMasterEntity:BaseEntity
    {
        #region Variables
        private int _treeMasterID;
        //private TreeMaster _treeMaster;
        private int _calendarMasterID;
        //private CalendarUnitMaster _calendarMaster;
        //private List<TreeCalendarDetail> _treeCalendarDetails;
        private string _name = string.Empty;
        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

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

        //public List<TreeCalendarDetail> TreeCalendarDetails
        //{
        //    get
        //    {
        //        if (_treeCalendarDetails == null)
        //        {
        //            if (this.Id > 0)
        //            {
        //                _treeCalendarDetails = TreeCalendarDetail.GetByTreeCalMaster(this.Id);
        //            }
        //        }
        //        return _treeCalendarDetails;
        //    }
        //    set { _treeCalendarDetails = value; }
        //}
        #endregion

        #region Constructor
        public TreeCalendarMasterEntity()
            : base()
        {
            _treeMasterID = 0;
            //_treeMaster = null;
            _calendarMasterID = 0;
            //_calendarMaster = null;
            _name = string.Empty;
        } 
        #endregion
    }
}
