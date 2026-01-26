using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;

namespace DataAccess
{
    public class SubmittedPapers_DAO : Base_DAO
    {
       private const string PERSONID = "PersonID";
       private const string PERSONID_PA = "@PersonID";
       
        private const string CANDIDATEID = "CandidateID";       
       private const string CANDIDATEID_PA = "@CandidateID";
       private const string SSC_C = "SSC_C";
       private const string SSC_C_PA = "@SSC_C";

       private const string SSC_M = "SSC_M";
       private const string SSC_M_PA = "@SSC_M";

       private const string SSC_T = "SSC_T";
       private const string SSC_T_PA = "@SSC_T";

       private const string HSC_C = "HSC_C";
       private const string HSC_C_PA = "@HSC_C";

       private const string HSC_M = "HSC_M";
       private const string HSC_M_PA = "@HSC_M";

       private const string HSC_T = "HSC_T";
       private const string HSC_T_PA = "@HSC_T";

       private const string BACHELOR_C = "Bachelor_C";
       private const string BACHELOR_C_PA = "@Bachelor_C";

       private const string BACHELOR_M = "Bachelor_M";
       private const string BACHELOR_M_PA = "@Bachelor_M";

       private const string BACHELOR_T = "Bachelor_T";
       private const string BACHELOR_T_PA = "@Bachelor_T";

       private const string MASTERS_C = "Masters_C";
       private const string MASTERS_C_PA = "@Masters_C";

       private const string MASTERS_M = "Masters_M";
       private const string MASTERS_M_PA = "@Masters_M";

       private const string MASTERS_T = "Masters_T";
       private const string MASTERS_T_PA = "@Masters_T";

       private const string PHOTO = "Photo";
       private const string PHOTO_PA = "@Photo";




        public static int SaveSubmittedPapers(Common.SubmittedPapersEntity submittedPapers, System.Data.SqlClient.SqlConnection sqlConn, System.Data.SqlClient.SqlTransaction sqlTran)
        {
            DAOParameters dParam = new DAOParameters();
            try
            {
                string command = @"INSERT INTO [SubmittedPapers]
                               ([PersonID]
                               ,[CandidateID]
                               ,[SSC_C]
                               ,[SSC_M]
                               ,[SSC_T]
                               ,[HSC_C]
                               ,[HSC_M]
                               ,[HSC_T]
                               ,[Bachelor_C]
                               ,[Bachelor_M]
                               ,[Bachelor_T]
                               ,[Masters_C]
                               ,[Masters_M]
                               ,[Masters_T]
                               ,[Photo]
                               ,[CreatedBy]
                               ,[CreatedDate]    
                               )
                         VALUES
                               ("+PERSONID_PA+
                               ","+CANDIDATEID_PA+
                               ","+SSC_C_PA+
                               ","+SSC_M_PA+
                               ","+SSC_T_PA+
                               ","+HSC_C_PA+
                               ","+HSC_M_PA+
                               ","+HSC_T_PA+
                               ","+BACHELOR_C_PA+
                               ","+BACHELOR_M_PA+
                               ","+BACHELOR_T_PA+
                               ","+MASTERS_C_PA+
                               ","+MASTERS_M_PA+
                               ","+MASTERS_T_PA+
                               ","+PHOTO_PA+
                               ","+CREATORID_PA+
                               ","+CREATEDDATE_PA+")";

                dParam.AddParameter(PERSONID_PA,submittedPapers.Personid);
                dParam.AddParameter(CANDIDATEID_PA, submittedPapers.Candidateid);
                dParam.AddParameter(SSC_C_PA, submittedPapers.Ssc_c);
                dParam.AddParameter(SSC_M_PA, submittedPapers.Ssc_m);
                dParam.AddParameter(SSC_T_PA, submittedPapers.Ssc_t);
                dParam.AddParameter(HSC_C_PA, submittedPapers.Hsc_c);
                dParam.AddParameter(HSC_M_PA, submittedPapers.Hsc_m);
                dParam.AddParameter(HSC_T_PA, submittedPapers.Hsc_t);
                dParam.AddParameter(BACHELOR_C_PA, submittedPapers.Bachelor_c);
                dParam.AddParameter(BACHELOR_M_PA, submittedPapers.Bachelor_m);
                dParam.AddParameter(BACHELOR_T_PA, submittedPapers.Bachelor_t);
                dParam.AddParameter(MASTERS_C_PA, submittedPapers.Masters_c);
                dParam.AddParameter(MASTERS_M_PA, submittedPapers.Masters_m);
                dParam.AddParameter(MASTERS_T_PA, submittedPapers.Masters_t);
                dParam.AddParameter(PHOTO_PA, submittedPapers.Photo);
                dParam.AddParameter(CREATORID_PA, submittedPapers.CreatorID);
                dParam.AddParameter(CREATEDDATE_PA, submittedPapers.CreatedDate);

                List<SqlParameter> sqlParams = Methods.GetSQLParameters(dParam);
                int i = QueryHandler.ExecuteSaveBatchAction(command, sqlParams, sqlConn, sqlTran);
               // MSSqlConnectionHandler.CommitTransaction();
                //MSSqlConnectionHandler.CloseDbConnection(); //Close DB Connection
                return i;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
    }
}
