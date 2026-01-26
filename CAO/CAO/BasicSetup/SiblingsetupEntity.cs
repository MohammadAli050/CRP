using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Common
{
	[Serializable]
	public class SiblingSetupEntity : BaseEntity
	{
	#region DBColumns
		//SiblingSetupId int identity,
		//GroupID int,
		//ApplicantId int,
		//CreatedBy int,
		//CreatedDate datetime,
		//ModifiedBy int,
		//ModifiedDate datetime
		#endregion

		#region Variables		
		private int _groupid;
		private int _applicantid;		
		#endregion

		#region Properties
        public int GroupId
		{
			get { return _groupid;}
			set {_groupid = value;}
		}

        public int ApplicantId
		{
			get { return _applicantid;}
			set {_applicantid = value;}
		}				
		#endregion

		#region Constructor
        public SiblingSetupEntity()
            : base()
		{		
			_groupid = 0;
			_applicantid = 0;			
		}
		#endregion
	
	}//End of Class
}//End of namesapce