using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Common
{
	[Serializable]
	public class RbProgramEntity : BaseEntity
	{
	
		#region DBColumns
		//PROGRAMID int,
		//CODE varchar (50),
		//SHORTNAME varchar (100),
		//TOTALCREDIT money,
		//PROGRAMTYPEID int,
		//DEPTID int,
		//DETAILNAME varchar (100),
		//CREATEDBY int,
		//CREATEDDATE datetime,
		//MODIFIEDBY int,
		//MODIFIEDDATE datetime
		#endregion

		#region Variables

		//private int _programid;
		private string _code;
		private string _shortname;
		private Decimal _totalcredit;
		private int _programtypeid;
		private int _deptid;
		private string _detailname;
        //private int _createdby;
        //private DateTime _createddate;
        //private int _modifiedby;
        //private DateTime _modifieddate;
		#endregion

		#region Properties
		
        //public int Programid
        //{
        //    get { return _programid;}
        //    set {_programid = value;}
        //}
		
		public string Code
		{
			get { return _code;}
			set {_code = value;}
		}
		
		public string Shortname
		{
			get { return _shortname;}
			set {_shortname = value;}
		}
		
		public Decimal Totalcredit
		{
			get { return _totalcredit;}
			set {_totalcredit = value;}
		}
		
		public int Programtypeid
		{
			get { return _programtypeid;}
			set {_programtypeid = value;}
		}
		
		public int Deptid
		{
			get { return _deptid;}
			set {_deptid = value;}
		}
		
		public string Detailname
		{
			get { return _detailname;}
			set {_detailname = value;}
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
		#endregion
		#region Constructor
		public RbProgramEntity(): base ()
		{
		
            //_programid = 0;
			_code = "";
			_shortname = "";
			_totalcredit = decimal.MinValue;
			_programtypeid = 0;
			_deptid = 0;
			_detailname = "";
            //_createdby = 0;
            //_createddate = DateTime.MinValue;
            //_modifiedby = 0;
            //_modifieddate = DateTime.MinValue;
		}
		#endregion

	
	}//End of Class
}//End of namesapce