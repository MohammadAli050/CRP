using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Common
{
	[Serializable]
	public class ConflictResultEntity : BaseEntity
	{		

		#region Variables
		private int _id;
        private string _sessionName;
		private string _sectionName;
		private decimal _obtainedTotalmarks;
		private string _grade;
        private bool _isConsiderGPA;
		#endregion

		#region Properties
		
		public int Id
		{
			get { return _id;}
			set {_id = value;}
		}

        public string Session
        {
            get { return _sessionName; }
            set { _sessionName = value; }
        }

		public string Sectionname
		{
			get { return _sectionName;}
			set {_sectionName = value;}
		}
		
		public decimal Obtainedtotalmarks
		{
			get { return _obtainedTotalmarks;}
			set {_obtainedTotalmarks = value;}
		}
		
		public string Grade
		{
			get { return _grade;}
			set {_grade = value;}
		}

        public bool IsConsiderGPA
        {
            get { return _isConsiderGPA; }
            set { _isConsiderGPA = value; }
        }
		#endregion
		#region Constructor
		public ConflictResultEntity(): base ()
		{		
			_id = 0;
            _sessionName = "";
			_sectionName = "";
            _obtainedTotalmarks = 0;
			_grade = "";
            _isConsiderGPA = false;
		}
		#endregion

	
	}//End of Class
}//End of namesapce