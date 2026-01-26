using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Common
{
	[Serializable]
	public class ViewNotSetBillableCoursesEntity//:BaseEntity
	{
	
		#region DBColumns
		//FormalCode varchar (50),
		//Title varchar (150),
		//CourseId int,
		//VersionId int,
		//Credits numeric,
		//IsCreditCourse bit
		#endregion

		#region Variables

	    private int _id;
        private string _formalcode;
		private string _title;
		private int _courseid;
		private int _versionid;
		private decimal _credits;
		private bool _iscreditcourse;
		#endregion

		#region Properties

	    public int Id
	    {
	        get { return _id; }
            set { _id = value; }
	    }

	    public string Formalcode
		{
			get { return _formalcode;}
			set {_formalcode = value;}
		}
		
		public string Title
		{
			get { return _title;}
			set {_title = value;}
		}
		
		public int Courseid
		{
			get { return _courseid;}
			set {_courseid = value;}
		}
		
		public int Versionid
		{
			get { return _versionid;}
			set {_versionid = value;}
		}
		
		public decimal Credits
		{
			get { return _credits;}
			set {_credits = value;}
		}
		
		public bool Iscreditcourse
		{
			get { return _iscreditcourse;}
			set {_iscreditcourse = value;}
		}
		#endregion

		#region Constructor
		public ViewNotSetBillableCoursesEntity()
		{

		    _id = 0;
            _formalcode = "";
			_title = "";
			_courseid = 0;
			_versionid = 0;
			_credits = 0;
		    _iscreditcourse = false;
		}
		#endregion

	
	}//End of Class
}//End of namesapce