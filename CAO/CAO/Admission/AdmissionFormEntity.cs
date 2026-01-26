using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Common
{
    [Serializable]
    public class AdmissionFormEntity : BaseEntity
    {

        #region DBColumns
        //ID int,
        //FORM_SERIAL varchar (20),
        //Amount money,
        //FORM_STATUS varchar (10),
        //CANDIDATE_TABLE_ID int,
        //PROGRAMID int,
        //ACADEMICCALENDERID int,
        //CREATEDBY int,
        //CREATEDDATE datetime,
        //MODIFIEDBY int,
        //MODIFIEDDATE datetime
        #endregion

        #region Variables

        //private string _formId;
        private string _formSerial;
        private decimal _formPrice;
        private string _formStatus;
        private int _candidateTableId;
        private int _programid;
        private int _academiccalenderid;

        #endregion

        #region Properties

        

        public string FormSerial
        {
            get { return _formSerial; }
            set { _formSerial = value; }
        }

        public decimal FormPrice
        {
            get { return _formPrice; }
            set { _formPrice = value; }
        }
        public string FormStatus
        {
            get { return _formStatus; }
            set { _formStatus = value; }
        }

        public int CandidateTableId
        {
            get { return _candidateTableId; }
            set { _candidateTableId = value; }
        }

        public int Programid
        {
            get { return _programid; }
            set { _programid = value; }
        }

        public int Academiccalenderid
        {
            get { return _academiccalenderid; }
            set { _academiccalenderid = value; }
        }


        #endregion
        #region Constructor
        public AdmissionFormEntity()
            : base()
        {
            _formSerial = "";
            _formPrice = 0;
            _formStatus = "";
            _candidateTableId = 0;
            _programid = 0;
            _academiccalenderid = 0;
        }
        #endregion


    }//End of Class
}//End of namesapce