using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class ExamMarksAllocationEntity : BaseEntity
    {

        //aaa

        #region DBColumns
        //ExamMarksAllocationID int 
        //ExamTypeNameID int 
        //AllottedMarks int 
        //ExamName varchar(150) 
        #endregion

        #region Variables
        private int _examTypeNameID;
        private int _allottedMarks;
        private string _examName;
        #endregion

        #region Properties
        public int ExamTypeNameID 
        {
            get {return _examTypeNameID ;}
            set{_examTypeNameID =value;}
        }

        public int AllottedMarks
        {
            get { return _allottedMarks; }
            set { _allottedMarks = value; }
        }

        public string ExamName
        {
            get { return _examName; }
            set { _examName = value; }
        }

        #endregion

        #region Constructor
        public ExamMarksAllocationEntity()
        {
            ExamTypeNameID = 0;
            AllottedMarks = 0;
            ExamName = string.Empty;
        }
        #endregion
    }
}
