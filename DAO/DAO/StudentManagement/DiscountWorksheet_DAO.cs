using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;

namespace DataAccess
{
    public class DiscountWorksheet_DAO
    {
        private static DiscountWorksheetEntity Mapper(SQLNullHandler nullHandler)
        {
            DiscountWorksheetEntity entity = new DiscountWorksheetEntity();

            entity.Id = nullHandler.GetInt32("BillWorkSheetId");

            entity.StudentID = nullHandler.GetInt32("StudentID");
            entity.Roll = nullHandler.GetString("Roll");
            entity.CalCourseProgNodeID = nullHandler.GetInt32("CalCourseProgNodeID");
            entity.AcaCalSectionID = nullHandler.GetInt32("AcaCalSectionID");
            entity.SectionTypeID = nullHandler.GetInt32("SectionTypeID");
            entity.AcaCalID = nullHandler.GetInt32("AcaCalID");
            entity.CourseID = nullHandler.GetInt32("CourseID");
            entity.VersionID = nullHandler.GetInt32("VersionID");
            entity.FormalCode = nullHandler.GetString("FormalCode");
            entity.VersionCode = nullHandler.GetString("VersionCode");
            entity.Title = nullHandler.GetString("Title");
            entity.RetakeNo = nullHandler.GetInt32("RetakeNo");
            entity.PreviousBestGrade = nullHandler.GetString("PreviousBestGrade");
            entity.FeeSetupID = nullHandler.GetInt32("FeeSetupID");
            //entity.PerCreditAmountAccID = nullHandler.GetInt32("PerCreditAmountAccId");
            entity.Fee = nullHandler.GetDecimal("Fee");
            // entity.AdmissionID = nullHandler.GetInt32("AdmissionID");
            entity.ProgramID = nullHandler.GetInt32("ProgramID");

            entity.DiscountTypeID = nullHandler.GetInt32("DiscountTypeID");
            entity.DiscountPercentage = nullHandler.GetDecimal("DiscountPercentage");
            entity.Remarks = nullHandler.GetString("Remarks");

            return entity;
        }

        private static List<DiscountWorksheetEntity> Maps(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            List<DiscountWorksheetEntity> entities = null;
            while (theReader.Read())
            {
                if (entities == null)
                {
                    entities = new List<DiscountWorksheetEntity>();
                }
                DiscountWorksheetEntity uiuemsAcFeesetup1Entity = Mapper(nullHandler);
                entities.Add(uiuemsAcFeesetup1Entity);
            }

            return entities;
        }


        private static DiscountWorksheetEntity Map(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            DiscountWorksheetEntity entity = null;
            if (theReader.Read())
            {
                entity = new DiscountWorksheetEntity();
                entity = Mapper(nullHandler);
            }

            return entity;
        }

        public static List<DiscountWorksheetEntity> GetBy(int acaCalId, int programId, int studentId, int courseId, int versionId)
        {
            try
            {
                List<DiscountWorksheetEntity> entities = null;

                string cmd = @"select 0'DiscountWorkSheetId', sch.StudentID, s.Roll, sch.CalCourseProgNodeID, sch.AcaCalSectionID,
                            acs.TypeDefinitionID as'SectionTypeID', 
                            sch.AcaCalID, sch.CourseID, sch.VersionID,c.FormalCode,c.VersionCode,c.Title, 
                            fs.FeeSetupId,fs.PerCreditAmountAccId, fs.PerCreditAmount, s.ProgramID 

                            from StudentCourseHistory as sch inner join 
                            Admission as a on sch.StudentID = a.StudentID inner join 
                            (SELECT distinct(AdmissionID), EffectiveAcaCalID FROM StdDiscountCurrent) 
                            AS sdc on a.AdmissionID = sdc.AdmissionID inner join 
                            Student as s on sch.StudentID = s.StudentID inner join
                            AcademicCalenderSection as acs on sch.AcaCalSectionID = acs.AcaCal_SectionID inner join 
                            Course as c on sch.CourseID = c.CourseID and sch.VersionID = c.VersionID inner join
                            FeeSetup as fs on s.ProgramID = fs.ProgramId and fs.AcaCalId = 
                            (select AcademicCalenderID from AcademicCalender where IsNext = 1) where sch.AcaCalID = " + acaCalId;

                if (programId > 0)
                {
                    cmd += " and s.ProgramID = " + programId;
                }
                if (studentId > 0)
                {
                    cmd += " and sch.StudentID = " + studentId;
                }
                if (courseId > 0)
                {
                    cmd += " and sch.CourseID = " + courseId + " and sch.VersionID = " + versionId;
                }

                DAOParameters dps = new DAOParameters();
                dps.AddParameter("@AcaCalID", acaCalId);
                dps.AddParameter("@ProgramID", programId);
                dps.AddParameter("@StudentID", studentId);
                dps.AddParameter("@CourseID", courseId);
                dps.AddParameter("@VersionID", versionId);

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

        public static int Save(List<DiscountWorksheetEntity> dwEntities)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();

                int effectedRows = Delete(dwEntities);

                string cmd = @"
                                INSERT INTO StdCrsBillWorksheet
                                           (StudentId
                                           ,CalCourseProgNodeID
                                           ,AcaCalSectionID
                                           ,SectionTypeId
                                           ,AcaCalId
                                           ,CourseId
                                           ,VersionId 
                                           ,ProgramId
                                           ,RetakeNo 
                                           ,PreviousBestGrade
                                           ,FeeSetupId 
                                           ,Fee
                                           ,DiscountTypeId
                                           ,DiscountPercentage
                                           ,Remarks

                                           ,CreatedBy
                                           ,CreatedDate)
                                     VALUES
                                           (@StudentId
                                           ,@CalCourseProgNodeID
                                           ,@AcaCalSectionID
                                           ,@SectionTypeId
                                           ,@AcaCalId
                                           ,@CourseId
                                           ,@VersionId                                           
                                           ,@ProgramId
                                           ,@RetakeNo 
                                           ,@PreviousBestGrade
                                           ,@FeeSetupId 
                                           ,@Fee
                                           ,@DiscountTypeId
                                           ,@DiscountPercentage
                                           ,@Remarks

                                           ,@CreatedBy
                                           ,@CreatedDate)
                                            ";

                foreach (DiscountWorksheetEntity dwe in dwEntities)
                {
                    Common.DAOParameters dps = new Common.DAOParameters();
                    dps.AddParameter("@StudentId", dwe.StudentID);
                    dps.AddParameter("@CalCourseProgNodeID", dwe.CalCourseProgNodeID);
                    dps.AddParameter("@AcaCalSectionID", dwe.AcaCalSectionID);
                    dps.AddParameter("@SectionTypeId", dwe.SectionTypeID);
                    dps.AddParameter("@AcaCalId", dwe.AcaCalID);
                    dps.AddParameter("@CourseId", dwe.CourseID);
                    dps.AddParameter("@VersionId", dwe.VersionID);
                    //dps.AddParameter("@FeeSetupId", dwe.FeeSetupID);
                    //dps.AddParameter("@PerCreditAmountAccID", dwe.PerCreditAmountAccID);
                    //dps.AddParameter("@PercreditAmount", dwe.Fee);
                    dps.AddParameter("@ProgramId", dwe.ProgramID);
                    dps.AddParameter("@RetakeNo", dwe.RetakeNo);
                    dps.AddParameter("@PreviousBestGrade", dwe.PreviousBestGrade);
                    dps.AddParameter("@FeeSetupId", dwe.FeeSetupID);
                    dps.AddParameter("@Fee", dwe.Fee);
                    dps.AddParameter("@DiscountTypeId", dwe.DiscountTypeID);
                    dps.AddParameter("@DiscountPercentage", dwe.DiscountPercentage);
                    dps.AddParameter("@Remarks", dwe.Remarks);

                    dps.AddParameter("@CreatedBy", dwe.CreatorID);
                    dps.AddParameter("@CreatedDate", dwe.CreatedDate);

                    List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                    counter = QueryHandler.ExecuteSelectBatchAction(cmd, ps);
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

        private static int Delete(List<DiscountWorksheetEntity> dwEntities)
        {
            int counter = 0;

            foreach (DiscountWorksheetEntity dwe in dwEntities)
            {
                string cmd = @"
                                DELETE FROM StdCrsBillWorksheet 
                                WHERE StudentId = @StudentId 
                                and CalCourseProgNodeID = @CalCourseProgNodeID
                            ";
                if (dwe.FeeSetupID != 0)
                {
                    cmd += " and FeeSetupId = @FeeSetupId ";
                }
                else if (dwe.DiscountTypeID != 0)
                {
                    cmd += " and DiscountTypeId = @DiscountTypeId ";
                }

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter("@StudentId", dwe.StudentID);
                dps.AddParameter("@CalCourseProgNodeID", dwe.CalCourseProgNodeID);
                if (dwe.FeeSetupID != 0)
                {
                    dps.AddParameter("@FeeSetupId", dwe.FeeSetupID);
                }
                else if (dwe.DiscountTypeID != 0)
                {
                    dps.AddParameter("@DiscountTypeId", dwe.DiscountTypeID);
                }

                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                counter = QueryHandler.ExecuteSelectBatchAction(cmd, ps);
            }
            return counter;
        }

        public static List<DiscountWorksheetEntity> LoadForEdit(int acaCalId, int programId, int studentId, int courseId, int versionId)
        {
            try
            {
                List<DiscountWorksheetEntity> entities = null;

                string cmd = @"SELECT     
	                        bw.BillWorkSheetId, 
	                        bw.StudentId, 
	                        s.Roll, 
	                        bw.CalCourseProgNodeID, 			
	                        bw.AcaCalSectionID, 
	                        bw.SectionTypeId, 
	                        bw.AcaCalId, 
	                        bw.CourseId, 
	                        bw.VersionId, 
	                        c.FormalCode, 
	                        c.VersionCode, 
	                        c.Title,
	                        bw.CourseTypeId, 
	                        bw.ProgramId, 
	                        bw.RetakeNo, 
	                        bw.PreviousBestGrade, 
	                        bw.FeeSetupId, 
	                        bw.Fee, 
	                        bw.DiscountTypeId, 
	                        bw.DiscountPercentage,
                            bw.Remarks,
	                        bw.CreatedBy, 
	                        bw.CreatedDate, 
	                        bw.ModifiedBy, 
	                        bw.ModifiedDate
                        FROM         
	                        StdCrsBillWorksheet AS bw 
	                        INNER JOIN Course AS c ON c.CourseID = bw.CourseId AND c.VersionID = bw.VersionId 
	                        INNER JOIN Student AS s ON s.StudentID = bw.StudentId where bw.AcaCalID = " + acaCalId;

                if (programId > 0)
                {
                    cmd += " and bw.ProgramID = " + programId;
                }
                if (studentId > 0)
                {
                    cmd += " and bw.StudentID = " + studentId;
                }
                if (courseId > 0)
                {
                    cmd += " and bw.CourseID = " + courseId + " and bw.VersionID = " + versionId;
                }

                DAOParameters dps = new DAOParameters();
                dps.AddParameter("@AcaCalID", acaCalId);
                dps.AddParameter("@ProgramID", programId);
                dps.AddParameter("@StudentID", studentId);
                dps.AddParameter("@CourseID", courseId);
                dps.AddParameter("@VersionID", versionId);

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
    }
}
