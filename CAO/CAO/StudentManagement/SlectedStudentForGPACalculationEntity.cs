using System;

namespace Common
{
    public class SelectedStudentForGPACalculationEntity
    {
        private int _studentID;
        public int StudentID
        {
            get { return _studentID; }
            set { _studentID = value; }
        }

        private string _studentRoll;
        public string StudentRoll
        {
            get { return _studentRoll; }
            set { _studentRoll = value; }
        }

        private string _studentName;
        public string StudentName
        {
            get { return _studentName; }
            set { _studentName = value; }
        }

        public SelectedStudentForGPACalculationEntity()
        {
            StudentID = 0;
            StudentRoll = String.Empty;
            StudentName = String.Empty;
        }

    }
}
