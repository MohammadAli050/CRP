using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class DiscountWorksheetEntity : BaseEntity
    {
        /*         
        bw.BillWorkSheetId, 
	    bw.StudentId, 
	    s.Roll, 
	    bw.CalCourseProgNodeID, 			
	    bw.AcaCalSectionID, 
	    bw.SectionTypeId, 
	    bw.AcaCalId, 
	    bw.CourseId, 
	    bw.VersionId, 
	    c.FormalCode, 
	    c.VersionCode, 
	    c.Title,
	    bw.CourseTypeId, 
	    bw.ProgramId, 
	    bw.RetakeNo, 
	    bw.PreviousBestGrade, 
	    bw.FeeSetupId, 
	    bw.Fee, 
	    bw.DiccountTypeId, 
	    bw.DiscountPercentage,
	    bw.CreatedBy, 
	    bw.CreatedDate, 
	    bw.ModifiedBy, 
	    bw.ModifiedDate
        */

        private int _studentID;
        public int StudentID
        {
            get { return _studentID; }
            set { _studentID = value; }
        }

        private string _roll;
        public string Roll
        {
            get { return _roll; }
            set { _roll = value; }
        }

        private int _calCourseProgNodeID;
        public int CalCourseProgNodeID
        {
            get { return _calCourseProgNodeID; }
            set { _calCourseProgNodeID = value; }
        }

        private int _acaCalSectionID;
        public int AcaCalSectionID
        {
            get { return _acaCalSectionID; }
            set { _acaCalSectionID = value; }
        }

        private int _sectionTypeID;
        public int SectionTypeID
        {
            get { return _sectionTypeID; }
            set { _sectionTypeID = value; }
        }

        private int _acaCalID;
        public int AcaCalID
        {
            get { return _acaCalID; }
            set { _acaCalID = value; }
        }

        private int _courseID;
        public int CourseID
        {
            get { return _courseID; }
            set { _courseID = value; }
        }

        private int _versionID;
        public int VersionID
        {
            get { return _versionID; }
            set { _versionID = value; }
        }

        private string _formalCode;
        public string FormalCode
        {
            get { return _formalCode; }
            set { _formalCode = value; }
        }

        private string _versionCode;
        public string VersionCode
        {
            get { return _versionCode; }
            set { _versionCode = value; }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private int _courseType;
        public int CourseType
        {
            get { return _courseType; }
            set { _courseType = value; }
        }

        private int _programID;
        public int ProgramID
        {
            get { return _programID; }
            set { _programID = value; }
        }

        private int _retakeNo;
        public int RetakeNo
        {
            get { return _retakeNo; }
            set { _retakeNo = value; }
        }

        private string _previousBestGrade;
        public string PreviousBestGrade
        {
            get { return _previousBestGrade; }
            set { _previousBestGrade = value; }
        }

        private int _feeSetupID;
        public int FeeSetupID
        {
            get { return _feeSetupID; }
            set { _feeSetupID = value; }
        }

        //private int _perCreditAmountAccID;
        //public int PerCreditAmountAccID
        //{
        //    get { return _perCreditAmountAccID; }
        //    set { _perCreditAmountAccID = value; }
        //}

        private decimal _fee;
        public decimal Fee
        {
            get { return _fee; }
            set { _fee = value; }
        }
        
        private int _discountTypeID;
        public int DiscountTypeID
        {
            get { return _discountTypeID; }
            set { _discountTypeID = value; }
        }

        private decimal _discountPercentage;
        public decimal DiscountPercentage
        {
            get { return _discountPercentage; }
            set { _discountPercentage = value; }
        }

        private string _remarks;
        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }

        public DiscountWorksheetEntity()
        {
            StudentID = 0;
            Roll = string.Empty;
            CalCourseProgNodeID = 0;
            AcaCalSectionID = 0;
            SectionTypeID = 0;
            AcaCalID = 0;
            CourseID = 0;
            VersionID = 0;
            FormalCode = string.Empty;
            VersionCode = string.Empty;
            Title = string.Empty;            
            RetakeNo = 0;
            PreviousBestGrade = string.Empty;
            //PerCreditAmountAccID = 0;
            FeeSetupID = 0;
            Fee = 0;            
            ProgramID = 0;
            DiscountTypeID = 0;
            DiscountPercentage = 0;
            Remarks = string.Empty;
        }
    }
}
