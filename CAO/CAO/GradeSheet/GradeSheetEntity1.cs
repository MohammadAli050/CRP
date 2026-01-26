using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class GradeSheetEntity1 : BaseEntity
    {
        #region Entity
        //private int _GradeSheetId;

        private int _ExamMarksAllocationID;

        private int _ProgramID;

        private int _AcademicCalenderID;

        private int _CourseID;

        private int _VersionID;

        private int _StudentID;

        private int _AcaCal_SectionID;

        private int _TeacherID;

        private decimal _ObtainMarks;

        private string _ObtainGrade;

        private int _GradeId1;
        private int _GradeId2;
        private int _GradeId3;
        private int _GradeId4;
        private int _GradeId5;
        private int _GradeId6;
        
        
        #endregion

        public GradeSheetEntity1()
            : base()
        {
            // _GradeSheetId = 0;

            _ExamMarksAllocationID = 0;

            _ProgramID = 0;

            _AcademicCalenderID = 0;

            _CourseID = 0;

            _VersionID = 0;

            _StudentID = 0;

            _AcaCal_SectionID = 0;

            _TeacherID = 0;

            _ObtainMarks = 0;

            _ObtainGrade = string.Empty;
           
            _GradeId1 = 0;
            _GradeId2 = 0;
            _GradeId3 = 0;
            _GradeId4 = 0;
            _GradeId5 = 0;
            _GradeId6 = 0;

        }

        #region Property

        //public int GradeSheetId
        //{
        //    get
        //    {
        //        return this._GradeSheetId;
        //    }
        //    set
        //    {
        //        this._GradeSheetId = value;
        //    }
        //}

        public int ExamMarksAllocationID
        {
            get
            {
                return this._ExamMarksAllocationID;
            }
            set
            {
                this._ExamMarksAllocationID = value;
            }
        }

        public int ProgramID
        {
            get
            {
                return this._ProgramID;
            }
            set
            {
                this._ProgramID = value;
            }
        }

        public int AcademicCalenderID
        {
            get
            {
                return this._AcademicCalenderID;
            }
            set
            {
                this._AcademicCalenderID = value;
            }
        }

        public int CourseID
        {
            get
            {
                return this._CourseID;
            }
            set
            {
                this._CourseID = value;
            }
        }

        public int VersionID
        {
            get
            {
                return this._VersionID;
            }
            set
            {
                this._VersionID = value;
            }
        }

        public int StudentID
        {
            get
            {
                return this._StudentID;
            }
            set
            {
                this._StudentID = value;
            }
        }

        public int AcaCal_SectionID
        {
            get
            {
                return this._AcaCal_SectionID;
            }
            set
            {
                this._AcaCal_SectionID = value;
            }
        }

        public int TeacherID
        {
            get
            {
                return this._TeacherID;
            }
            set
            {
                this._TeacherID = value;
            }
        }

        public decimal ObtainMarks
        {
            get
            {
                return this._ObtainMarks;
            }
            set
            {
                this._ObtainMarks = value;
            }
        }

        public string ObatinGrade
        {
            get
            {
                return this._ObtainGrade;
            }
            set
            {
                this._ObtainGrade = value;
            }
        }


        

        public int GradeId1
        {
            get
            {
                return this._GradeId1;
            }
            set
            {
                this._GradeId1 = value;
            }
        }

        public int GradeId2
        {
            get
            {
                return this._GradeId2;
            }
            set
            {
                this._GradeId2 = value;
            }
        }

        public int GradeId3
        {
            get
            {
                return this._GradeId3;
            }
            set
            {
                this._GradeId3 = value;
            }
        }
        public int GradeId4
        {
            get
            {
                return this._GradeId4;
            }
            set
            {
                this._GradeId4 = value;
            }
        }
        public int GradeId5
        {
            get
            {
                return this._GradeId5;
            }
            set
            {
                this._GradeId5 = value;
            }
        }
        public int GradeId6
        {
            get
            {
                return this._GradeId6;
            }
            set
            {
                this._GradeId6 = value;
            }
        }
        

        #endregion
    }

}
