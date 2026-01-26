using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class ConflictResult_DAO : Base_DAO
    {
        #region Constants

        private const string ID = "ID";//0


        private const string SECTIONNAME = "SectionName";//1
        private const string SECTIONNAME_PA = "@SectionName";


        #endregion

        #region PKCOLUMNS
        private const string ALLCOLUMNS = "[" + ID + "]," +
                                        "[" + SECTIONNAME + "],";
        #endregion
        #region NOPKCOLUMNS
        private const string NOPKCOLUMNS = "[" + SECTIONNAME + "],";
        #endregion
        private const string TABLENAME = "SHOWCONFLICTRESULT";
        #region SELECT
        private const string SELECT = "SELECT "
                                 + ALLCOLUMNS
                                 + BASECOLUMNS
                                 + " FROM " + TABLENAME;
        #endregion
        #region INSERT

        private const string INSERT = "INSERT INTO" + TABLENAME
                                    + "("
                                     + NOPKCOLUMNS
                                     + BASECOLUMNS
                                     + ")"
                                    + " VALUES ("
                                     + SECTIONNAME_PA + ","
                                     + CREATORID_PA + ","
                                     + CREATEDDATE_PA + ","
                                     + MODIFIERID_PA + ","
                                     + MOIDFIEDDATE_PA + ")";
        #endregion
        #region UPDATE

        private const string UPDATE = "UPDATE" + TABLENAME + "SET"
                                     + SECTIONNAME + " = " + SECTIONNAME_PA + ","
                                     + CREATORID + " = " + CREATORID_PA + ","
                                     + CREATEDDATE + " = " + CREATEDDATE_PA + ","
                                     + MODIFIERID + " = " + MODIFIERID_PA + ","
                                     + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;
        #endregion
        #region DELETE

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion
        #region Methods
        private static ConflictResultEntity Mapper(SQLNullHandler nullHandler)
        {
            ConflictResultEntity showconflictresultEntity = new ConflictResultEntity();

            showconflictresultEntity.Id = nullHandler.GetInt32("ID");
            showconflictresultEntity.Session = nullHandler.GetString("Session");
            showconflictresultEntity.Sectionname = nullHandler.GetString("Sectionname");
            showconflictresultEntity.Obtainedtotalmarks = nullHandler.GetDecimal("Obtainedtotalmarks");
            showconflictresultEntity.Grade = nullHandler.GetString("Grade");
            showconflictresultEntity.IsConsiderGPA = nullHandler.GetBoolean("IsConsiderGPA");

            return showconflictresultEntity;
        }//end of method Mapper()

        private static List<ConflictResultEntity> Maps(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            List<ConflictResultEntity> showconflictresultEntities = null;
            while (theReader.Read())
            {
                if (showconflictresultEntities == null)
                {
                    showconflictresultEntities = new List<ConflictResultEntity>();
                }
                ConflictResultEntity showconflictresultEntity = Mapper(nullHandler);
                showconflictresultEntities.Add(showconflictresultEntity);
            }

            return showconflictresultEntities;
        }//end of method Map()


        private static ConflictResultEntity Map(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            ConflictResultEntity showconflictresultEntity = null;
            if (theReader.Read())
            {
                showconflictresultEntity = new ConflictResultEntity();
                showconflictresultEntity = Mapper(nullHandler);
            }

            return showconflictresultEntity;
        }//end of method Map()



        private static List<SqlParameter> MakeSqlParameterList(ConflictResultEntity showconflictresultEntity)
        {
            DAOParameters dps = new DAOParameters();
            //dps.AddParameter(CREATORID_PA,showconflictresultEntity.Creatorid);
            //dps.AddParameter(CREATEDDATE_PA,showconflictresultEntity.Createddate);
            //dps.AddParameter(MODIFIERID_PA,showconflictresultEntity.Modifierid);
            //dps.AddParameter(MOIDFIEDDATE_PA,showconflictresultEntity.Moidfieddate);
            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
            return ps;
        }

        public static int save(List<ConflictResultEntity> showconflictresultEntities)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();
                string cmd = "@return_value int,@result int"
                        + "EXEC @return_value = [dbo].[sp_]"
                    //Add the required Parameters here
                    + "SELECT	@result as '@result' ";
                foreach (ConflictResultEntity showconflictresultEntity in showconflictresultEntities)
                {
                    ConflictResultEntity tempEntity = new ConflictResultEntity();
                    //Assign the Paramerter here like empEntity.ID = showconflictresultEntity.ID;


                    counter = QueryHandler.ExecuteSelectBatchAction(cmd, MakeSqlParameterList(tempEntity));
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

        internal static int delete(int ID)
        {
            int counter = 0;
            try
            {
                string cmd = DELETE + "WHERE Id = " + SECTIONNAME_PA;
                DAOParameters dps = new DAOParameters();
                //dps.AddParameter( , ID);
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                counter = QueryHandler.ExecuteDeleteBatchAction(cmd, ps);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return counter;
        }

        public static List<ConflictResultEntity> GetCandidateStudents(int ID)
        {
            try
            {
                List<ConflictResultEntity> conflictresultEntities = null;
                string cmd = SELECT + "WHERE Id = " + SECTIONNAME_PA;
                DAOParameters dps = new DAOParameters();
                //dps.AddParameter( , );
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader dr = QueryHandler.ExecuteSelectQuery(cmd, ps);
                conflictresultEntities = Maps(dr);
                return conflictresultEntities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public static List<ConflictResultEntity> Load(int stdId, int courseID, int versionID, int sectionID)
        {
            try
            {
                List<ConflictResultEntity> conflictresultEntities = null;
                string cmd = @"
                                SELECT      sch.ID,cut.TypeName +'-'+ CONVERT(nvarchar(10), ac.Year) as Session, acs.SectionName, sch.ObtainedTotalMarks, gd.Grade , sch.IsConsiderGPA
                                FROM        StudentCourseHistory AS sch INNER JOIN
			                                AcademicCalenderSection AS acs ON sch.AcaCalSectionID = acs.AcaCal_SectionID INNER JOIN
                                            GradeDetails AS gd ON sch.GradeId = gd.GradeId inner join
			                                AcademicCalender ac on ac.AcademicCalenderID = sch.AcaCalID inner join 
			                                CalenderUnitType cut on cut.CalenderUnitTypeID = ac.CalenderUnitTypeID
                                 WHERE		(sch.CourseID = @CourseID) AND 
			                                (sch.VersionID = @VersionID) AND 
			                                (sch.StudentID = @StudentID) AND
                                            (sch.GradeId IS NOT NULL)
                                ";
                DAOParameters dps = new DAOParameters();
                dps.AddParameter("@StudentID", stdId);
                dps.AddParameter("@CourseID", courseID);
                dps.AddParameter("@VersionID", versionID);
                dps.AddParameter("@SectionID", sectionID);

                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader dr = QueryHandler.ExecuteSelectQuery(cmd, ps);

                conflictresultEntities = Maps(dr);
                dr.Close();

                return conflictresultEntities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int Update(int id)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();

                string cmd = @"
                                DECLARE @StdId int, @CourseID int, @VersionID int;

                                select @StdId = StudentID, @CourseID = CourseID, @VersionID = VersionID
                                from StudentCourseHistory
                                where ID = @ID

                                UPDATE StudentCourseHistory
                                SET IsConsiderGPA = 0      
                                WHERE StudentID = @StdId and CourseID = @CourseID and  VersionID = @VersionID 

                                UPDATE StudentCourseHistory   SET  IsConsiderGPA = 1  WHERE ID = @ID
                            ";

                DAOParameters dps = new DAOParameters();
                dps.AddParameter("@ID", id);
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                counter = QueryHandler.ExecuteSelectBatchAction(cmd, ps);

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
    }

}//End Of Namespace DataAccess
