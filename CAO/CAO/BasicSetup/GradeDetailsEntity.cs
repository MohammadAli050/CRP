using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Common
{
	[Serializable]
	public class GradeDetailsEntity : BaseEntity
	{
	
		#region DBColumns
		//GradeId int,
		//AcaCalId int,
		//ProgramId int,
		//Grade nvarchar (100),
		//RetakeDiscount numeric,
		//GradePoint numeric,
		//MinMarks int,
		//MaxMarks int,
		//CreatedBy int,
		//CreatedDate datetime,
		//ModifiedBy int,
		//ModifiedDate datetime
		#endregion

		#region Variables

		private int _gradeid;
		private int _acacalid;
		private int _programid;
		private string _grade;
		private decimal _retakediscount;
		private decimal _gradepoint;
		private int _minmarks;
		private int _maxmarks;
		
		#endregion

		#region Properties
		
        public int Gradeid
        {
            get { return _gradeid;}
            set {_gradeid = value;}
        }
		
		public int Acacalid
		{
			get { return _acacalid;}
			set {_acacalid = value;}
		}
		
		public int Programid
		{
			get { return _programid;}
			set {_programid = value;}
		}
		
		public string Grade
		{
			get { return _grade;}
			set {_grade = value;}
		}
		
		public decimal Retakediscount
		{
			get { return _retakediscount;}
			set {_retakediscount = value;}
		}
		
		public decimal Gradepoint
		{
			get { return _gradepoint;}
			set {_gradepoint = value;}
		}
		
		public int Minmarks
		{
			get { return _minmarks;}
			set {_minmarks = value;}
		}
		
		public int Maxmarks
		{
			get { return _maxmarks;}
			set {_maxmarks = value;}
		}
				
		#endregion
		#region Constructor
		public GradeDetailsEntity(): base ()
		{		
			_gradeid = 0;
			_acacalid = 0;
			_programid = 0;
			_grade = "";
			_retakediscount = 0;
			_gradepoint = 0;
			_minmarks = 0;
			_maxmarks = 0;			
		}
		#endregion

	
	}//End of Class
}//End of namesapce