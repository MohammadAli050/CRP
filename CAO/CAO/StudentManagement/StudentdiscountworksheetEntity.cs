using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Common
{
    [Serializable]
    public class StudentDiscountWorksheetEntity : BaseEntity
    {

        #region DBColumns
        //StdDiscountWorksheetID int identity,
        //StudentID int,
        //ProgramID int,
        //AcaCalID int,
        //AdmissionCalId int,
        //TotalCrRegInPreviousSession numeric,
        //GPAinPreviousSession numeric,
        //CGPAupToPreviousSaession numeric,
        //TotalCrRegInCurrentSession numeric,
        //DiscountTypeId int,
        //DiscountPercentage numeric,
        //Remarks ntext,
        //CreatedBy int,
        //CreatedDate datetime,
        //ModifiedBy int,
        //ModifiedDate datetime
        #endregion

        #region Variables
        private int _studentId;
        private string _roll;
        private string _name;
        private int _programId;
        private int _acaCalId;
        private int _admissionCalId;
        private decimal _totalCrRegInPreviousSession;
        private decimal _gpaInPreviousSession;
        private decimal _cgpaUptoPreviousSession;
        private decimal _totalCrRegInCurrentSession;
        private int _discountTypeId;
        private decimal _discountPercentage;
        private string _remarks;
        #endregion

        #region Properties

        public int StudentId
        {
            get { return _studentId; }
            set { _studentId = value; }
        }

        public string Roll
        {
            get { return _roll; }
            set { _roll = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int ProgramId
        {
            get { return _programId; }
            set { _programId = value; }
        }

        public int AcacalId
        {
            get { return _acaCalId; }
            set { _acaCalId = value; }
        }

        public int AdmissionCalId
        {
            get { return _admissionCalId; }
            set { _admissionCalId = value; }
        }

        public decimal TotalCrRegInpreviousSession
        {
            get { return _totalCrRegInPreviousSession; }
            set { _totalCrRegInPreviousSession = value; }
        }

        public decimal GpaInPreviousSession
        {
            get { return _gpaInPreviousSession; }
            set { _gpaInPreviousSession = value; }
        }

        public decimal CgpaUptoPreviousSession
        {
            get { return _cgpaUptoPreviousSession; }
            set { _cgpaUptoPreviousSession = value; }
        }

        public decimal TotalCrRegInCurrentSession
        {
            get { return _totalCrRegInCurrentSession; }
            set { _totalCrRegInCurrentSession = value; }
        }

        public int DiscountTypeId
        {
            get { return _discountTypeId; }
            set { _discountTypeId = value; }
        }

        public decimal Discountpercentage
        {
            get { return _discountPercentage; }
            set { _discountPercentage = value; }
        }

        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }

        #endregion

        #region Constructor
        public StudentDiscountWorksheetEntity()
            : base()
        {
            _studentId = 0;
            _roll = string.Empty;
            _name = string.Empty;
            _programId = 0;
            _acaCalId = 0;
            _admissionCalId = 0;
            _totalCrRegInPreviousSession = 0;
            _gpaInPreviousSession = 0;
            _cgpaUptoPreviousSession = 0;
            _totalCrRegInCurrentSession = 0;
            _discountTypeId = 0;
            _discountPercentage = 0;
            _remarks = string.Empty;
        }
        #endregion
    }//End of Class
}//End of namesapce