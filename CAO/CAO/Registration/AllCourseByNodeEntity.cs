using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class AllCourseByNodeEntity
    {
        #region DBColumns
        /*         
         * NC.CourseID,         
         * NC.VersionID,		 
         * NC.Node_CourseID, 
         * C.FormalCode, 
         * C.VersionCode, 
         * C.Title as 'CourseTitle'
         */
        #endregion

        #region Variables
        private int _courseID;
        private int _versionID;
        private int _node_CourseID;
        private string _formalCode;
        private string _versionCode;
        private string _courseTitle; 
        #endregion

        #region Conustructor
        public AllCourseByNodeEntity()
        {
            _courseID = 0;
            _versionID = 0;
            _node_CourseID = 0;
            _formalCode = string.Empty;
            _versionCode = string.Empty;
            _courseTitle = string.Empty;
        } 
        #endregion

        #region Properties
        public int CourseID
        {
            get { return _courseID; }
            set { _courseID = value; }
        }
        public int VersionID
        {
            get { return _versionID; }
            set { _versionID = value; }
        }
        public int Node_CourseID
        {
            get { return _node_CourseID; }
            set { _node_CourseID = value; }
        }
        public string FormalCode
        {
            get { return _formalCode; }
            set { _formalCode = value; }
        }
        public string VersionCode
        {
            get { return _versionCode; }
            set { _versionCode = value; }
        }
        public string CourseTitle
        {
            get { return _courseTitle; }
            set { _courseTitle = value; }
        } 
        #endregion
    }
}
