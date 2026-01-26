using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;

namespace DataAccess
{
    public class GradeSheet_DAO
    {
        private static GradeSheetEntity Mapper(SQLNullHandler nullHandler)
        {
            GradeSheetEntity entity = new GradeSheetEntity();

            entity.Id = nullHandler.GetInt32("GradeSheetId");
            entity.ExamMarksAllocationID = nullHandler.GetInt32("ExamMarksAllocationID");
            entity.ProgramID = nullHandler.GetInt32("ProgramID");
            entity.AcademicCalenderID = nullHandler.GetInt32("AcademicCalenderID");
            entity.CourseID = nullHandler.GetInt32("CourseID");
            entity.VersionID = nullHandler.GetInt32("VersionID");
            entity.StudentID = nullHandler.GetInt32("StudentID");
            entity.AcaCal_SectionID = nullHandler.GetInt32("AcaCal_SectionID");
            entity.TeacherID = nullHandler.GetInt32("TeacherID");
            entity.ObtainMarks = nullHandler.GetDecimal("ObtainMarks");
            entity.Grade = nullHandler.GetString("ObtainGrade");
            entity.GradeId = nullHandler.GetInt32("GradeId");
           // entity.IsFinalSubmit = nullHandler.GetBoolean("IsFinalSubmit");
            
            entity.CreatorID = nullHandler.GetInt32("CreatedBy");
            entity.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            entity.ModifierID = nullHandler.GetInt32("ModifiedBy");
            entity.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");

            return entity;
        }

        private static GradeSheetEntity1 Mapper1(SQLNullHandler nullHandler)
        {
            GradeSheetEntity1 entity = new GradeSheetEntity1();

            entity.Id = nullHandler.GetInt32("GradeSheetId");
            entity.ExamMarksAllocationID = nullHandler.GetInt32("ExamMarksAllocationID");
            entity.ProgramID = nullHandler.GetInt32("ProgramID");
            entity.AcademicCalenderID = nullHandler.GetInt32("AcademicCalenderID");
            entity.CourseID = nullHandler.GetInt32("CourseID");
            entity.VersionID = nullHandler.GetInt32("VersionID");
            entity.StudentID = nullHandler.GetInt32("StudentID");
            entity.AcaCal_SectionID = nullHandler.GetInt32("AcaCal_SectionID");
            entity.TeacherID = nullHandler.GetInt32("TeacherID");
            entity.ObtainMarks = nullHandler.GetDecimal("ObtainMarks");
            entity.ObatinGrade = nullHandler.GetString("ObtainGrade");
            entity.GradeId1 = nullHandler.GetInt32("GradeId1");
            entity.GradeId2 = nullHandler.GetInt32("GradeId2");
            entity.GradeId3 = nullHandler.GetInt32("GradeId3");
            entity.GradeId4 = nullHandler.GetInt32("GradeId4");
            entity.GradeId5 = nullHandler.GetInt32("GradeId5");
            entity.GradeId6 = nullHandler.GetInt32("GradeId6");
          //  entity.IsTransfer = nullHandler.GetBoolean("IsTransfer");
            entity.CreatorID = nullHandler.GetInt32("CreatedBy");
            entity.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            entity.ModifierID = nullHandler.GetInt32("ModifiedBy");
            entity.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");

            return entity;
        }

        private static List<GradeSheetEntity> Maps(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<GradeSheetEntity> entities = null;

            while (theReader.Read())
            {
                if (entities == null)
                {
                    entities = new List<GradeSheetEntity>();
                }
                GradeSheetEntity entity = Mapper(nullHandler);
                entities.Add(entity);
            }

            return entities;
        }

        private static List<GradeSheetEntity1> Maps1(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<GradeSheetEntity1> entities = null;

            while (theReader.Read())
            {
                if (entities == null)
                {
                    entities = new List<GradeSheetEntity1>();
                }
                GradeSheetEntity1 entity = Mapper1(nullHandler);
                entities.Add(entity);
            }

            return entities;
        }


        public static int Save(List<GradeSheetEntity> _gsEntities)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();

                int effectedRows = Delete(_gsEntities);

                string cmd = @" INSERT INTO GradeSheet
                               (
                                [CourseID]
                               ,[VersionID]
                               ,[StudentID]
                               ,[AcaCal_SectionID]
                               ,[AcademicCalenderID]
                               ,[TeacherID]
                               ,[ObtainMarks]
                               ,[ObtainGrade]
                               ,[IsTransfer]
                               ,[GradeId]
                               ,[IsFinalSubmit]
                               ,[CreatedBy]
                               ,[CreatedDate]                               
                               )                               
                         VALUES
                               (
                                @CourseID
                               ,@VersionID
                               ,@StudentId
                               ,@AcaCal_SectionID
                               ,@AcademicCalenderID
                               ,@TeacherID
                               ,@ObtainMarks
                               ,@ObtainGrade
                               ,@IsTransfer
                               ,@GradeId
                               ,@IsFinalSubmit
                               ,@CreatedBy
                               ,@CreatedDate
                               ) ";

                foreach (GradeSheetEntity gse in _gsEntities)
                {
                    Common.DAOParameters dps = new Common.DAOParameters();
                    dps.AddParameter("@StudentId", gse.StudentID);
                    dps.AddParameter("@CourseID", gse.CourseID);
                    dps.AddParameter("@VersionID", gse.VersionID);
                    dps.AddParameter("@AcaCal_SectionID", gse.AcaCal_SectionID);
                    dps.AddParameter("@AcademicCalenderID", gse.AcademicCalenderID);
                    dps.AddParameter("@TeacherID", gse.TeacherID);
                    dps.AddParameter("@ObtainMarks", gse.ObtainMarks);
                    dps.AddParameter("@ObtainGrade", gse.Grade);
                   // dps.AddParameter("@IsFinalSubmit", gse.IsFinalSubmit);
                    dps.AddParameter("@GradeId", gse.GradeId);
                    dps.AddParameter("@IsTransfer", 0);
                    dps.AddParameter("@CreatedBy", gse.CreatorID);
                    dps.AddParameter("@CreatedDate", gse.CreatedDate);

                    List<SqlParameter> ps = Methods.GetSQLParameters(dps);

                    counter += QueryHandler.ExecuteSelectBatchAction(cmd, ps);
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

        private static int Delete(List<GradeSheetEntity> _gsEntities)
        {
            try
            {
                int counter = 0;

                foreach (GradeSheetEntity gse in _gsEntities)
                {
                    string cmd = " DELETE FROM GradeSheet " +
                                    "WHERE CourseID = " + gse.CourseID +
                                        " and VersionID = " + gse.VersionID +
                                        " and StudentID = " + gse.StudentID+
                                        " and AcaCal_SectionID = " + gse.AcaCal_SectionID +
                                        " and TeacherID = " + gse.TeacherID;


                    Common.DAOParameters dps = new Common.DAOParameters();
                    dps.AddParameter("@CourseID", gse.CourseID);
                    dps.AddParameter("@VersionID", gse.VersionID);
                    dps.AddParameter("@StudentID", gse.StudentID);
                    dps.AddParameter("@AcaCal_SectionID", gse.AcaCal_SectionID);
                    dps.AddParameter("@TeacherID", gse.TeacherID);

                    List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                    counter = QueryHandler.ExecuteSelectBatchAction(cmd, ps);
                }
                return counter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static int Delete1(List<GradeSheetEntity> _gsEntities)
        {
            try
            {
                int counter = 0;

                foreach (GradeSheetEntity gse in _gsEntities)
                {
                    string cmd = " DELETE FROM StudentCourseHistory " +
                                    "WHERE CourseID = " + gse.CourseID +
                                        " and VersionID = " + gse.VersionID +
                                        " and StudentID = " + gse.StudentID;
                                      //  " and AcaCal_SectionID = " + gse.AcaCal_SectionID +
                                      //  " and TeacherID = " + gse.TeacherID;


                    Common.DAOParameters dps = new Common.DAOParameters();
                    dps.AddParameter("@CourseID", gse.CourseID);
                    dps.AddParameter("@VersionID", gse.VersionID);
                    dps.AddParameter("@StudentID", gse.StudentID);
                   // dps.AddParameter("@AcaCal_SectionID", gse.AcaCal_SectionID);
                  //  dps.AddParameter("@TeacherID", gse.TeacherID);

                    List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                    counter = QueryHandler.ExecuteSelectBatchAction(cmd, ps);
                }
                return counter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<GradeSheetEntity> GetsBy(int acaCalId, int courseID, int versionID, int teacherId, int sectionID)
        {
            try
            {
                List<GradeSheetEntity> entities = null;

                string cmd = @"SELECT 
                               GradeSheetId
                              ,ExamMarksAllocationID
                              ,ProgramID
                              ,AcademicCalenderID
                              ,CourseID
                              ,VersionID
                              ,StudentID
                              ,AcaCal_SectionID
                              ,TeacherID
                              ,ObtainMarks
                              ,ObtainGrade
                              ,GradeId
                              ,CreatedBy
                              ,CreatedDate
                              ,ModifiedBy
                              ,ModifiedDate
                          FROM GradeSheet
                          WHERE AcademicCalenderID = @AcaCalID 
		                        and CourseID = @CourseID
		                        and VersionID = @VersionID
		                        and TeacherID = @TeacherID 
                                and AcaCal_SectionID = @AcaCal_SectionID";

                DAOParameters dps = new DAOParameters();
                dps.AddParameter("@AcaCalID", acaCalId);
                dps.AddParameter("@CourseID", courseID);
                dps.AddParameter("@VersionID", versionID);
                dps.AddParameter("@TeacherID", teacherId);
                dps.AddParameter("@AcaCal_SectionID", sectionID);

                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader dr = QueryHandler.ExecuteSelectQuery(cmd, ps);

                entities = Maps(dr);
                dr.Close();
                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<GradeSheetEntity1> GetsBy1(int acaCalId, int courseID, int versionID, int sectionID)
        {
            try
            {
                List<GradeSheetEntity1> entities = null;

                string cmd = @"Select * from
(SELECT                       
                      
                               GradeSheetId
                              ,ExamMarksAllocationID
                              ,ProgramID
                              ,AcademicCalenderID
                              ,CourseID
                              ,VersionID
                              ,StudentID
                              ,seca as AcaCal_SectionID
                              ,TeacherID
                              ,ObtainMarks
                              ,ObtainGrade
                              ,GradeId1
                              ,GradeId2
                              ,GradeId3
                              ,GradeId4
                              ,GradeId5
                              ,GradeId6
                              ,CreatedBy
                              ,CreatedDate
                              ,ModifiedBy
                              ,ModifiedDate 
                              ,ROW_NUMBER() OVER (PARTITION BY StudentID ORDER BY StudentID) as RowNumber
             from                 
                    (  select  a.GradeSheetId
                              ,a.ExamMarksAllocationID
                              ,a.ProgramID
                              ,a.AcademicCalenderID
                              ,a.CourseID
                              ,a.VersionID
                              ,a.StudentID
                              ,a.AcaCal_SectionID as seca
                              ,b.AcaCal_SectionID as secb
                              ,c.AcaCal_SectionID as secc
                              ,d.AcaCal_SectionID as secd
                              ,e.AcaCal_SectionID as sece
                              ,f.AcaCal_SectionID as secf
                              ,a.TeacherID
                              ,a.ObtainMarks
                              ,a.ObtainGrade
                              ,a.GradeId as GradeId1
                              ,b.GradeId as GradeId2
                              ,c.GradeId as GradeId3
                              ,d.GradeId as GradeId4
                              ,e.GradeId as GradeId5
                              ,f.GradeId as GradeId6
                              ,a.CreatedBy
                              ,a.CreatedDate
                              ,a.ModifiedBy
                              ,a.ModifiedDate
                          
           From    (Select *      FROM GradeSheet 
									   where CourseID=@CourseID and VersionID=@VersionID and IsFinalSubmit=1 and IsTransfer=0) as a
           left Outer join (Select *      FROM GradeSheet 
									   where CourseID=@CourseID and VersionID=@VersionID and IsFinalSubmit=1 and IsTransfer=0) as b
        on 
           a.StudentID=b.StudentID 
           and a.AcaCal_SectionID!=b.AcaCal_SectionID
           left Outer join (Select *      FROM GradeSheet 
									   where CourseID=@CourseID and VersionID=@VersionID and IsFinalSubmit=1 and IsTransfer=0) as c
        on 
           c.StudentID=b.StudentID 
           and c.AcaCal_SectionID!=b.AcaCal_SectionID
           and c.AcaCal_SectionID!=a.AcaCal_SectionID
           left Outer join (Select *      FROM GradeSheet 
									   where CourseID=@CourseID and VersionID=@VersionID and IsFinalSubmit=1 and IsTransfer=0) as d
        on 
           c.StudentID=d.StudentID 
           and d.AcaCal_SectionID!=c.AcaCal_SectionID
           and d.AcaCal_SectionID!=b.AcaCal_SectionID 
           and d.AcaCal_SectionID!=a.AcaCal_SectionID
           left Outer join (Select *      FROM GradeSheet 
									   where CourseID=@CourseID and VersionID=@VersionID and IsFinalSubmit=1 and IsTransfer=0) as e
        on 
           e.StudentID=d.StudentID 
           and e.AcaCal_SectionID!=a.AcaCal_SectionID
           and e.AcaCal_SectionID!=b.AcaCal_SectionID 
           and e.AcaCal_SectionID!=c.AcaCal_SectionID
           and e.AcaCal_SectionID!=d.AcaCal_SectionID
           left Outer join (Select *      FROM GradeSheet 
									   where CourseID=@CourseID and VersionID=@VersionID and IsFinalSubmit=1 and IsTransfer=0) as f
        on 
           e.StudentID=f.StudentID 
           and f.AcaCal_SectionID!=a.AcaCal_SectionID
           and f.AcaCal_SectionID!=b.AcaCal_SectionID 
           and f.AcaCal_SectionID!=c.AcaCal_SectionID
           and f.AcaCal_SectionID!=d.AcaCal_SectionID
           and f.AcaCal_SectionID!=e.AcaCal_SectionID) as abcdef
           
           where AcademicCalenderID=@AcaCalID
           and seca=@AcaCal_SectionID) as top1 where top1.RowNumber=1";

                DAOParameters dps = new DAOParameters();
                dps.AddParameter("@AcaCalID", acaCalId);
                dps.AddParameter("@CourseID", courseID);
                dps.AddParameter("@VersionID", versionID);
                dps.AddParameter("@AcaCal_SectionID", sectionID);

                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader dr = QueryHandler.ExecuteSelectQuery(cmd, ps);

                entities = Maps1(dr);
                dr.Close();
                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<GradeSheetEntity> GetsConflictRows(int acaCalId, int courseID, int versionID, int sectionID)
        {
            try
            {
                List<GradeSheetEntity> entities = null;

                string cmd = @"SELECT 
                               GradeSheetId
                              ,ExamMarksAllocationID
                              ,ProgramID
                              ,AcademicCalenderID
                              ,CourseID
                              ,VersionID
                              ,StudentID
                              ,AcaCal_SectionID
                              ,TeacherID
                              ,ObtainMarks
                              ,ObtainGrade
                              ,GradeId
                              ,CreatedBy
                              ,CreatedDate
                              ,ModifiedBy
                              ,ModifiedDate
                          FROM GradeSheet
                          WHERE AcademicCalenderID = @AcaCalID 
		                        and CourseID = @CourseID
		                        and VersionID = @VersionID 
                                and AcaCal_SectionID = @AcaCal_SectionID
                        ";
                               // and IsConflictWithRetake = 1";

                DAOParameters dps = new DAOParameters();
                dps.AddParameter("@AcaCalID", acaCalId);
                dps.AddParameter("@CourseID", courseID);
                dps.AddParameter("@VersionID", versionID);
                dps.AddParameter("@AcaCal_SectionID", sectionID);

                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader dr = QueryHandler.ExecuteSelectQuery(cmd, ps);

                entities = Maps(dr);
                dr.Close();


                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<GradeSheetEntity> GetsStudents()
        {
            try
            {
                List<GradeSheetEntity> entities = null;

                string cmd = @"SELECT 
                              
                               Distinct StudentID
                              
                          FROM GradeSheet";
                // and IsConflictWithRetake = 1";

                DAOParameters dps = new DAOParameters();
               

                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader dr = QueryHandler.ExecuteSelectQuery(cmd, ps);

                entities = Maps(dr);
                dr.Close();


                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int UpdateByReg(List<GradeSheetEntity> gsEntities)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();

                string cmd = @"UPDATE GradeSheet
                               SET 
                                  ObtainMarks = @ObtainMarks
                                  ,ObtainGrade = @ObtainGrade 
                                  ,GradeId = @GradeId      
                                  ,ModifiedBy = @ModifiedBy
                                  ,ModifiedDate = @ModifiedDate
                             WHERE GradeSheetId = @GradeSheetId";

                foreach (GradeSheetEntity gse in gsEntities)
                {
                    Common.DAOParameters dps = new Common.DAOParameters();

                    dps.AddParameter("@GradeSheetId", gse.Id);
                    dps.AddParameter("@ObtainMarks", gse.ObtainMarks);
                    dps.AddParameter("@ObtainGrade", gse.Grade);
                    dps.AddParameter("@GradeId", gse.GradeId);
                    dps.AddParameter("@ModifiedBy", gse.ModifierID);
                    dps.AddParameter("@ModifiedDate", gse.ModifiedDate);

                    List<SqlParameter> ps = Methods.GetSQLParameters(dps);

                    counter += QueryHandler.ExecuteSelectBatchAction(cmd, ps);
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

        public static int UpdateTransfer(List<GradeSheetEntity> _gsEntities)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();

                string cmd = @"UPDATE GradeSheet
                               SET 
                                  IsTransfer = 1
                                  
                             WHERE AcademicCalenderID = @AcaCalID 
		                        and CourseID = @CourseID
		                        and VersionID = @VersionID 
                                and AcaCal_SectionID = @AcaCal_SectionID
                                and StudentID=@StudentID";

                foreach (GradeSheetEntity gse in _gsEntities)
                {
                    Common.DAOParameters dps = new Common.DAOParameters();

                    dps.AddParameter("@AcaCalID", gse.AcademicCalenderID);
                    dps.AddParameter("@CourseID", gse.CourseID);
                    dps.AddParameter("@VersionID", gse.VersionID);
                    dps.AddParameter("@AcaCal_SectionID", gse.AcaCal_SectionID);
                    dps.AddParameter("@StudentID", gse.StudentID);

                    List<SqlParameter> ps = Methods.GetSQLParameters(dps);

                    counter += QueryHandler.ExecuteSelectBatchAction(cmd, ps);

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



//        public static int TransferByReg(List<GradeSheetEntity> gsEntities)
//        {
//            try
//            {
//                int counter = 0;
//                MSSqlConnectionHandler.GetConnection();
//                MSSqlConnectionHandler.StartTransaction();

//                string cmd = @"DECLARE	@return_value int
//
//                            EXEC	@return_value = [dbo].[usp_FinalGradeSubmission]
//		                            @AcademicCalenderID = @P_AcademicCalenderID,
//		                            @CourseID = @P_CourseID,
//		                            @VersionID = @P_VersionID,
//		                            @StudentID = @P_StudentID,
//		                            @AcaCal_SectionID = @P_AcaCal_SectionID
//
//                            SELECT	'Return Value' = @return_value";

//                foreach (GradeSheetEntity gse in gsEntities)
//                {
//                    Common.DAOParameters dps = new Common.DAOParameters();

//                    dps.AddParameter("@P_AcademicCalenderID", gse.AcademicCalenderID);
//                    dps.AddParameter("@P_CourseID", gse.CourseID);
//                    dps.AddParameter("@P_VersionID", gse.VersionID);
//                    dps.AddParameter("@P_StudentID", gse.StudentID);
//                    dps.AddParameter("@P_AcaCal_SectionID", gse.AcaCal_SectionID);

//                    List<SqlParameter> ps = Methods.GetSQLParameters(dps);

//                    counter += QueryHandler.ExecuteSelectBatchAction(cmd, ps);
//                }

//                MSSqlConnectionHandler.CommitTransaction();
//                MSSqlConnectionHandler.CloseDbConnection();
//                return counter;
//            }
//            catch (Exception ex)
//            {
//                MSSqlConnectionHandler.RollBackAndClose();
//                throw ex;
//            }
//        }


        public static int TransferByReg(List<GradeSheetEntity> _gsEntities)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();

                int effectedRows = Delete1(_gsEntities);

                string cmd = @" INSERT INTO StudentCourseHistory
                               (
                                [CourseID]
                               ,[VersionID]
                               ,[StudentID]
                               ,[AcaCalSectionID]
                               ,[AcaCalID]
                               
                               ,[ObtainedTotalMarks]
                               ,[ObtainedGrade]
                               ,[GradeId]
                               
                               ,[CreatedBy]
                               ,[CreatedDate]  
                               ,[isConsiderGPA]                             
                               )                               
                         VALUES
                               (
                                @CourseID
                               ,@VersionID
                               ,@StudentId
                               ,@AcaCal_SectionID
                               ,@AcademicCalenderID
                               
                               ,@ObtainTotal
                               ,@ObtainGrade
                               ,@GradeId
                               
                               ,@CreatedBy
                               ,@CreatedDate
                               ,@isConsiderGPA
                               ) ";

                foreach (GradeSheetEntity gse in _gsEntities)
                {
                    Common.DAOParameters dps = new Common.DAOParameters();
                    dps.AddParameter("@StudentId", gse.StudentID);
                    dps.AddParameter("@CourseID", gse.CourseID);
                    dps.AddParameter("@VersionID", gse.VersionID);
                    dps.AddParameter("@AcaCal_SectionID", gse.AcaCal_SectionID);
                    dps.AddParameter("@AcademicCalenderID", gse.AcademicCalenderID);
                    // dps.AddParameter("@TeacherID", gse.TeacherID);
                    dps.AddParameter("@ObtainTotal", gse.ObtainMarks);
                    dps.AddParameter("@ObtainGrade", gse.Grade);
                    dps.AddParameter("@GradeId", gse.GradeId);
                   // dps.AddParameter("@IsTransfer", gse.IsTransfer);
                    dps.AddParameter("@CreatedBy", gse.CreatorID);
                    dps.AddParameter("@CreatedDate", gse.CreatedDate);
                    dps.AddParameter("@isConsiderGPA", 1);

                    //try
                    //{
                    //    UpdateTransfer(gse.AcademicCalenderID, gse.CourseID, gse.VersionID, gse.AcaCal_SectionID, gse.StudentID);
                    //}
                    //catch (Exception ex)
                    //{ throw ex; }
                    List<SqlParameter> ps = Methods.GetSQLParameters(dps);

                    counter += QueryHandler.ExecuteSelectBatchAction(cmd, ps);
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

        public static int UpdateConflictRetake(int gradeSheetId)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();

                string cmd = @"UPDATE GradeSheet   SET  IsConflictWithRetake = 0  WHERE GradeSheetId = @GradeSheetId";

                Common.DAOParameters dps = new Common.DAOParameters();

                dps.AddParameter("@GradeSheetId", gradeSheetId);

                List<SqlParameter> ps = Methods.GetSQLParameters(dps);

                counter += QueryHandler.ExecuteSelectBatchAction(cmd, ps);

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
}
