using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class RelationDiscountCourse_DAO :Base_DAO
    {
        #region constants
        private const string RELID = "RelationBetweenDiscountCourseTypeID";
        private const string ACACALID = "AcaCalID";
        private const string PROGRAMID = "ProgramID";
        private const string TYPEDEFDISCOUNTID = "TypeDefDiscountID";
        private const string TYPEDEFCOURSEID = "TypeDefCourseID";

        private const string RELID_PA = "@RelationBetweenDiscountCourseTypeID";
        private const string ACACALID_PA = "@AcaCalID";
        private const string PROGRAMID_PA = "@ProgramID";
        private const string TYPEDEFDISCOUNTID_PA = "@TypeDefDiscountID";
        private const string TYPEDEFCOURSEID_PA = "@TypeDefCourseID";

        private const string ALLCOLUMNS = "[RelationBetweenDiscountCourseTypeID], "
                                      +"[AcaCalID], "
                                      +"[ProgramID], "
                                      +"[TypeDefDiscountID], "
                                      +"[TypeDefCourseID], ";
        private const string NONPKCOLUMNS = "[AcaCalID], "
                                      + "[ProgramID], "
                                      + "[TypeDefDiscountID], "
                                      + "[TypeDefCourseID], ";
        private const string TABLENAME = " [RelationBetweenDiscountCourseType] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;
        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                             + NONPKCOLUMNS
                             + BASEMUSTCOLUMNS + ")"
                             + "VALUES ( "
                             + ACACALID_PA + ", "
                             + PROGRAMID_PA + ", "
                             + TYPEDEFDISCOUNTID_PA + ", "
                             + TYPEDEFCOURSEID_PA + ", "
                             + CREATORID_PA + ", "
                             + "getdate())";
        private const string DELETE = "DELETE FROM" + TABLENAME;
        
        #endregion

        #region methods

        private static RelationDiscountCourseEntity Mapper(SQLNullHandler nullHandler)
        {
            RelationDiscountCourseEntity sd = new RelationDiscountCourseEntity();

            sd.Id = nullHandler.GetInt32(RELID);
            sd.AcaCalID = nullHandler.GetInt32(ACACALID);
            sd.ProgramID = nullHandler.GetInt32(PROGRAMID);
            sd.TypeDefDiscountID = nullHandler.GetInt32(TYPEDEFDISCOUNTID);
            sd.TypeDefCourseID = nullHandler.GetInt32(TYPEDEFCOURSEID);
            sd.CreatorID = nullHandler.GetInt32("CreatedBy");
            sd.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            sd.ModifierID = nullHandler.GetInt32("ModifiedBy");
            sd.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");

            return sd;
        }
        private static List<RelationDiscountCourseEntity> Maps(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<RelationDiscountCourseEntity> sds = null;

            while (theReader.Read())
            {
                if (sds == null)
                {
                    sds = new List<RelationDiscountCourseEntity>();
                }
                RelationDiscountCourseEntity sd = Mapper(nullHandler);
                sds.Add(sd);
            }

            return sds;
        }
        private static RelationDiscountCourseEntity Map(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            RelationDiscountCourseEntity sd = null;
            if (theReader.Read())
            {
                sd = new RelationDiscountCourseEntity();
                sd = Mapper(nullHandler);
            }
            return sd;
        }
        private static List<SqlParameter> MakeSqlParameterList(RelationDiscountCourseEntity sde)
        {
            Common.DAOParameters dps = new Common.DAOParameters();
            dps.AddParameter(ACACALID_PA, sde.AcaCalID);
            dps.AddParameter(PROGRAMID_PA, sde.ProgramID);
            dps.AddParameter(TYPEDEFDISCOUNTID_PA, sde.TypeDefDiscountID);
            dps.AddParameter(TYPEDEFCOURSEID_PA, sde.TypeDefCourseID);
            dps.AddParameter(CREATORID_PA, sde.CreatorID);

            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
            return ps;
        }

        public static int Save(List<RelationDiscountCourseEntity> dcses)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();

                int acaID = dcses[0].AcaCalID;
                int progId = dcses[0].ProgramID;
                Delete(acaID, progId);

                string cmd = INSERT;

                foreach (RelationDiscountCourseEntity sd in dcses)
                {
                    counter = QueryHandler.ExecuteSelectBatchAction(cmd, MakeSqlParameterList(sd));
                }

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        internal static int Delete(int acaID, int progID)
        {
            int counter = 0;
            try
            {
                string cmd = DELETE + " Where AcaCalID = " + ACACALID_PA + " and ProgramID = " + PROGRAMID_PA;

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter(ACACALID_PA, acaID);
                dps.AddParameter(PROGRAMID_PA, progID);

                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                counter = QueryHandler.ExecuteDeleteBatchAction(cmd, ps);
                return counter;
            }
            catch (Exception ex)
            {
                //FixMe
                throw ex;
            }
        }
        public static List<RelationDiscountCourseEntity> GetRelations(int acaID, int progID)
        {
            try
            {
                List<RelationDiscountCourseEntity> sdes = null;

                string cmd = SELECT + " Where AcaCalID = " + ACACALID_PA + " and ProgramID = " + PROGRAMID_PA;

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter(ACACALID_PA, acaID);
                dps.AddParameter(PROGRAMID_PA, progID);
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader rd = QueryHandler.ExecuteSelectQuery(cmd, ps);
                sdes = Maps(rd);
                rd.Close();
                return sdes;
            }
            catch (Exception ex)
            {
                //FixMe
                throw ex;
            }
        }
        


        #endregion
    }
}
