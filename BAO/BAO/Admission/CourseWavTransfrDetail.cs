using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessObject
{
    [Serializable]
    public class CourseWavTransfrDetail:Base
    {
        private int _courseWavTransfrMasterID;
        private int _ownerNodeCourseID;
        private string _againstCourseInfo;

        public int CourseWavTransfrMasterID
        {
            get { return _courseWavTransfrMasterID; }
            set { _courseWavTransfrMasterID = value; }
        }
        public int OwnerNodeCourseID
        {
            get
            {
                return this._ownerNodeCourseID;
            }
            set
            {
                this._ownerNodeCourseID = value;
            }
        }
        public string AgainstCourseInfo
        {
            get
            {
                return this._againstCourseInfo;
            }
            set
            {
                this._againstCourseInfo = value;
            }
        }
    }
}
