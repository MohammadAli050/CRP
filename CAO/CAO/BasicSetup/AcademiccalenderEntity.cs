using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Common
{
	[Serializable]
	public class CcAcademiccalenderEntity : BaseEntity
	{
	
		#region DBColumns
		//ACADEMICCALENDERID int,
		//CALENDERUNITTYPEID int,
		//YEAR int,
		//BATCHCODE nvarchar (6),
		//ISCURRENT bit,
		//ISNEXT bit,
		//STARTDATE datetime,
		//ENDDATE datetime,
		//FULLPAYNOFINELSTDT datetime,
		//FIRSTINSTNOFINELSTDT datetime,
		//SECINSTNOFINELSTDT datetime,
		//THIRDINSTNOFINELSTDS datetime,
		//ADDDROPLASTDATEFULL datetime,
		//ADDDROPLASTDATEHALF datetime,
		//LASTDATEENROLLNOFINE datetime,
		//LASTDATEENROLLWFINE datetime,
		//CREATEDBY int,
		//CREATEDDATE datetime,
		//MODIFIEDBY int,
		//MODIFIEDDATE datetime,
		//ADMISSIONSTARTDATE datetime,
		//ADMISSIONENDDATE datetime,
		//ISACTIVEADMISSION bit,
		//REGISTRATIONSTARTDATE datetime,
		//REGISTRATIONENDDATE datetime,
		//ISACTIVEREGISTRATION bit
		#endregion

		#region Variables

		//private int _academiccalenderid;
		private int _calenderunittypeid;
		private int _year;
		private string _batchcode;
		private bool _iscurrent;
		private bool _isnext;
		private DateTime _startdate;
		private DateTime _enddate;
		private DateTime _fullpaynofinelstdt;
		private DateTime _firstinstnofinelstdt;
		private DateTime _secinstnofinelstdt;
		private DateTime _thirdinstnofinelstds;
		private DateTime _adddroplastdatefull;
		private DateTime _adddroplastdatehalf;
		private DateTime _lastdateenrollnofine;
		private DateTime _lastdateenrollwfine;
        //private int _createdby;
        //private DateTime _createddate;
        //private int _modifiedby;
        //private DateTime _modifieddate;
		private DateTime _admissionstartdate;
		private DateTime _admissionenddate;
		private bool _isactiveadmission;
		private DateTime _registrationstartdate;
		private DateTime _registrationenddate;
		private bool _isactiveregistration;
		#endregion

		#region Properties
		
        //public int Academiccalenderid
        //{
        //    get { return _academiccalenderid;}
        //    set {_academiccalenderid = value;}
        //}
		
		public int Calenderunittypeid
		{
			get { return _calenderunittypeid;}
			set {_calenderunittypeid = value;}
		}
		
		public int Year
		{
			get { return _year;}
			set {_year = value;}
		}
		
		public string Batchcode
		{
			get { return _batchcode;}
			set {_batchcode = value;}
		}
		
		public bool Iscurrent
		{
			get { return _iscurrent;}
			set {_iscurrent = value;}
		}
		
		public bool Isnext
		{
			get { return _isnext;}
			set {_isnext = value;}
		}
		
		public DateTime Startdate
		{
			get { return _startdate;}
			set {_startdate = value;}
		}
		
		public DateTime Enddate
		{
			get { return _enddate;}
			set {_enddate = value;}
		}
		
		public DateTime Fullpaynofinelstdt
		{
			get { return _fullpaynofinelstdt;}
			set {_fullpaynofinelstdt = value;}
		}
		
		public DateTime Firstinstnofinelstdt
		{
			get { return _firstinstnofinelstdt;}
			set {_firstinstnofinelstdt = value;}
		}
		
		public DateTime Secinstnofinelstdt
		{
			get { return _secinstnofinelstdt;}
			set {_secinstnofinelstdt = value;}
		}
		
		public DateTime Thirdinstnofinelstds
		{
			get { return _thirdinstnofinelstds;}
			set {_thirdinstnofinelstds = value;}
		}
		
		public DateTime Adddroplastdatefull
		{
			get { return _adddroplastdatefull;}
			set {_adddroplastdatefull = value;}
		}
		
		public DateTime Adddroplastdatehalf
		{
			get { return _adddroplastdatehalf;}
			set {_adddroplastdatehalf = value;}
		}
		
		public DateTime Lastdateenrollnofine
		{
			get { return _lastdateenrollnofine;}
			set {_lastdateenrollnofine = value;}
		}
		
		public DateTime Lastdateenrollwfine
		{
			get { return _lastdateenrollwfine;}
			set {_lastdateenrollwfine = value;}
		}
		
        //public int Createdby
        //{
        //    get { return _createdby;}
        //    set {_createdby = value;}
        //}
		
        //public DateTime Createddate
        //{
        //    get { return _createddate;}
        //    set {_createddate = value;}
        //}
		
        //public int Modifiedby
        //{
        //    get { return _modifiedby;}
        //    set {_modifiedby = value;}
        //}
		
        //public DateTime Modifieddate
        //{
        //    get { return _modifieddate;}
        //    set {_modifieddate = value;}
        //}
		
		public DateTime Admissionstartdate
		{
			get { return _admissionstartdate;}
			set {_admissionstartdate = value;}
		}
		
		public DateTime Admissionenddate
		{
			get { return _admissionenddate;}
			set {_admissionenddate = value;}
		}
		
		public bool Isactiveadmission
		{
			get { return _isactiveadmission;}
			set {_isactiveadmission = value;}
		}
		
		public DateTime Registrationstartdate
		{
			get { return _registrationstartdate;}
			set {_registrationstartdate = value;}
		}
		
		public DateTime Registrationenddate
		{
			get { return _registrationenddate;}
			set {_registrationenddate = value;}
		}
		
		public bool Isactiveregistration
		{
			get { return _isactiveregistration;}
			set {_isactiveregistration = value;}
		}
		#endregion
		#region Constructor
		public CcAcademiccalenderEntity(): base ()
		{
		
			//_academiccalenderid = 0;
			_calenderunittypeid = 0;
			_year = 0;
			_batchcode = "";
			_startdate = DateTime.MinValue;
			_enddate = DateTime.MinValue;
			_fullpaynofinelstdt = DateTime.MinValue;
			_firstinstnofinelstdt = DateTime.MinValue;
			_secinstnofinelstdt = DateTime.MinValue;
			_thirdinstnofinelstds = DateTime.MinValue;
			_adddroplastdatefull = DateTime.MinValue;
			_adddroplastdatehalf = DateTime.MinValue;
			_lastdateenrollnofine = DateTime.MinValue;
			_lastdateenrollwfine = DateTime.MinValue;
            //_createdby = 0;
            //_createddate = DateTime.MinValue;
            //_modifiedby = 0;
            //_modifieddate = DateTime.MinValue;
			_admissionstartdate = DateTime.MinValue;
			_admissionenddate = DateTime.MinValue;
		    _isactiveadmission = false;
			_registrationstartdate = DateTime.MinValue;
			_registrationenddate = DateTime.MinValue;
		    _isactiveregistration = false;
		}
		#endregion

	
	}//End of Class
}//End of namesapce