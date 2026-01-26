using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EMS.Module
{
    public class CommonFunctionForStudentMigration
    {

        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;


        BussinessObject.UIUMSUser BaseCurrentUserObj = null;

        public static StudentMigrationObj MigrateStudent(int ProgramId, int SessionId, string Level, string Term, string Id, string RegNo, string Name, string Mobile, string Email, string DOB, string GName, string GMobile, string PerAdd, string PresAdd, string BG, string SSCYear, string SSCGPA, string HSCYear, string HSCGPA, int MigratedBy, string LogInID, string _pageId, string _pageName, string _pageUrl, string AdmisstionRoll, int MeritPosition, string FatherName, string MotherName)
        {
            StudentMigrationObj MigrationObj = new StudentMigrationObj();
            try
            {
                MigrationObj.StudentID = Id;
                MigrationObj.Name = Name;


                bool RollExists = CheckRoll(Id);
                if (!RollExists) // New Student
                {
                    int PersonId = InsertIntoPersonTable(Name, Mobile, Email, DOB, GName, GMobile, BG, MigratedBy, FatherName, MotherName);
                    if (PersonId > 0)
                    {
                        int StudentId = InsertIntoStudentTable(ProgramId, SessionId, Id, PersonId, MigratedBy, RegNo, AdmisstionRoll, MeritPosition);

                        if (StudentId > 0)
                        {
                            int UserId = InsertIntoUserAndUserInPersonTable(Id, PersonId, MigratedBy);

                            MigrationObj.Status = 1;
                            MigrationObj.Reason = "Student inserted";


                            #region SSC and HSC Information
                            if (SSCGPA != "" || HSCGPA != "")
                                InsertIntoEducationTable(PersonId, SSCYear, SSCGPA, HSCYear, HSCGPA, MigratedBy);
                            #endregion

                            #region Address Information
                            if (PerAdd != "" || PresAdd != "")
                                InsertIntoAddressTable(PersonId, PerAdd, PresAdd, MigratedBy);
                            #endregion

                            #region Log Insert

                            try
                            {
                                InsertLog("New Student Migration", LogInID + " Migrated a new student with StudentID : " + Id + " and Name : " + Name, Id, LogInID, _pageId, _pageName, _pageUrl);
                            }
                            catch (Exception ex)
                            {
                            }

                            #endregion

                        }
                        else
                        {
                            Person PerObj = PersonManager.GetById(PersonId);
                            if (PerObj != null)
                                PersonManager.Delete(PerObj.PersonID);

                            MigrationObj.Status = 0;
                            MigrationObj.Reason = "Student not inserted";
                        }

                    }
                    else
                    {
                        MigrationObj.Status = 0;
                        MigrationObj.Reason = "Person not inserted";
                    }
                }
                else
                {
                    MigrationObj.Status = 0;
                    MigrationObj.Reason = "Student Id already exists";
                }



            }
            catch (Exception ex)
            {
            }
            return MigrationObj;
        }

        private static void InsertIntoYearAndSemesterTable(int StudentId, string Level, string Term, int MigratedBy)
        {
            try
            {
                //DAL.NITER_UCAMEntities ucamContext = new DAL.NITER_UCAMEntities();

                //int lvl = 0, trm = 0;
                //if (Level != "")
                //    lvl = Convert.ToInt32(Level);
                //if (Term != "")
                //    trm = Convert.ToInt32(Term);

                //var ExistingObj = ucamContext.StudentYearSemesterHistories.Where(x => x.StudentId == StudentId && x.IsActive == true).ToList();
                //if (ExistingObj == null)
                //{
                //    DAL.StudentYearSemesterHistory sys = new DAL.StudentYearSemesterHistory();


                //}
            }
            catch (Exception ex)
            {
            }
        }

        private static void InsertIntoAddressTable(int PersonId, string PerAdd, string PresAdd, int MigratedBy)
        {
            try
            {
                int PermanentId = 1, PresentId = 2;

                DAL.NITER_UCAMEntities ucamContext = new DAL.NITER_UCAMEntities();
                var AddressList = ucamContext.Addresses.AsNoTracking().Where(x => x.PersonId == PersonId).ToList();

                if (AddressList != null)
                {
                    var PermanentObj = AddressList.Where(x => x.AddressTypeId == PermanentId).FirstOrDefault();
                    if (PermanentObj != null)
                    {
                        if (PerAdd.ToLower() != "n/a")
                            PermanentObj.AddressLine = PerAdd;

                        PermanentObj.CreatedBy = MigratedBy;
                        PermanentObj.CreatedDate = DateTime.Now;

                        ucamContext.Entry(PermanentObj).State = EntityState.Modified;
                        ucamContext.SaveChanges();

                    }
                    else
                    {
                        DAL.Address NewPermanentObj = new DAL.Address();
                        NewPermanentObj.PersonId = PersonId;
                        NewPermanentObj.AddressTypeId = PermanentId;
                        NewPermanentObj.AddressLine = PerAdd;
                        NewPermanentObj.CreatedBy = MigratedBy;
                        NewPermanentObj.CreatedDate = DateTime.Now;

                        ucamContext.Addresses.Add(NewPermanentObj);
                        ucamContext.SaveChanges();
                    }


                    var PresentObj = AddressList.Where(x => x.AddressTypeId == PresentId).FirstOrDefault();
                    if (PresentObj != null)
                    {
                        if (PresAdd.ToLower() != "n/a")
                            PresentObj.AddressLine = PresAdd;

                        PresentObj.ModifiedBy = MigratedBy;
                        PresentObj.ModifiedDate = DateTime.Now;

                        ucamContext.Entry(PresentObj).State = EntityState.Modified;
                        ucamContext.SaveChanges();
                    }
                    else
                    {
                        DAL.Address NewPresentObj = new DAL.Address();
                        NewPresentObj.PersonId = PersonId;
                        NewPresentObj.AddressTypeId = PresentId;
                        NewPresentObj.AddressLine = PresAdd;

                        NewPresentObj.CreatedBy = MigratedBy;
                        NewPresentObj.CreatedDate = DateTime.Now;

                        ucamContext.Addresses.Add(NewPresentObj);
                        ucamContext.SaveChanges();
                    }

                }
                else
                {
                    DAL.Address PerObj = new DAL.Address();
                    PerObj.PersonId = PersonId;
                    PerObj.AddressTypeId = PermanentId;

                    if (PerAdd.ToLower() != "n/a")
                        PerObj.AddressLine = PerAdd;

                    PerObj.CreatedBy = MigratedBy;
                    PerObj.CreatedDate = DateTime.Now;

                    ucamContext.Addresses.Add(PerObj);
                    ucamContext.SaveChanges();


                    DAL.Address PresObj = new DAL.Address();
                    PresObj.PersonId = PersonId;
                    PresObj.AddressTypeId = PresentId;

                    if (PresAdd.ToLower() != "n/a")
                        PresObj.AddressLine = PresAdd;

                    PresObj.CreatedBy = MigratedBy;
                    PresObj.CreatedDate = DateTime.Now;

                    ucamContext.Addresses.Add(PresObj);
                    ucamContext.SaveChanges();
                }


            }
            catch (Exception ex)
            {
            }
        }

        private static void InsertIntoEducationTable(int PersonId, string SSCYear, string SSCGPA, string HSCYear, string HSCGPA, int MigratedBy)
        {
            try
            {
                int SSCId = 69, HSCId = 70;

                DAL.NITER_UCAMEntities ucamContext = new DAL.NITER_UCAMEntities();
                var PreviousEdu = ucamContext.PreviousEducations.Where(x => x.PersonId == PersonId).ToList();

                if (PreviousEdu != null)
                {
                    var SSCObj = PreviousEdu.Where(x => x.EducationCategoryId == SSCId).FirstOrDefault();
                    if (SSCObj != null)
                    {
                        if (SSCYear.ToLower() != "n/a")
                            SSCObj.PassingYear = Convert.ToInt32(SSCYear);
                        if (SSCGPA.ToLower() != "n/a")
                            SSCObj.MarksOrGPA = SSCGPA;

                        SSCObj.CreatedBy = MigratedBy;
                        SSCObj.CreatedDate = DateTime.Now;


                        ucamContext.Entry(SSCObj).State = EntityState.Modified;
                        ucamContext.SaveChanges();

                    }
                    else
                    {
                        DAL.PreviousEducation NewSSCObj = new DAL.PreviousEducation();
                        NewSSCObj.PersonId = PersonId;
                        NewSSCObj.EducationTypeId = 1;
                        NewSSCObj.EducationCategoryId = SSCId;
                        if (SSCYear.ToLower() != "n/a")
                            NewSSCObj.PassingYear = Convert.ToInt32(SSCYear);
                        if (SSCGPA.ToLower() != "n/a")
                            NewSSCObj.MarksOrGPA = SSCGPA;

                        NewSSCObj.CreatedBy = MigratedBy;
                        NewSSCObj.CreatedDate = DateTime.Now;

                        ucamContext.PreviousEducations.Add(NewSSCObj);
                        ucamContext.SaveChanges();
                    }


                    var HSCObj = PreviousEdu.Where(x => x.EducationCategoryId == HSCId).FirstOrDefault();
                    if (HSCObj != null)
                    {
                        if (HSCYear.ToLower() != "n/a")
                            HSCObj.PassingYear = Convert.ToInt32(HSCYear);
                        if (HSCGPA.ToLower() != "n/a")
                            HSCObj.MarksOrGPA = HSCGPA;

                        HSCObj.ModifiedBy = MigratedBy;
                        HSCObj.ModifiedDate = DateTime.Now;

                        ucamContext.Entry(HSCObj).State = EntityState.Modified;
                        ucamContext.SaveChanges();
                    }
                    else
                    {
                        DAL.PreviousEducation NewHSCObj = new DAL.PreviousEducation();
                        NewHSCObj.PersonId = PersonId;
                        NewHSCObj.EducationTypeId = 1;
                        NewHSCObj.EducationCategoryId = HSCId;
                        if (HSCYear.ToLower() != "n/a")
                            NewHSCObj.PassingYear = Convert.ToInt32(HSCYear);
                        if (HSCGPA.ToLower() != "n/a")
                            NewHSCObj.MarksOrGPA = HSCGPA;

                        NewHSCObj.CreatedBy = MigratedBy;
                        NewHSCObj.CreatedDate = DateTime.Now;

                        ucamContext.PreviousEducations.Add(NewHSCObj);
                        ucamContext.SaveChanges();
                    }

                }
                else
                {
                    DAL.PreviousEducation SSCObj = new DAL.PreviousEducation();
                    SSCObj.PersonId = PersonId;
                    SSCObj.EducationTypeId = 1;
                    SSCObj.EducationCategoryId = SSCId;
                    if (SSCYear.ToLower() != "n/a")
                        SSCObj.PassingYear = Convert.ToInt32(SSCYear);
                    if (SSCGPA.ToLower() != "n/a")
                        SSCObj.MarksOrGPA = SSCGPA;

                    SSCObj.ModifiedBy = MigratedBy;
                    SSCObj.ModifiedDate = DateTime.Now;

                    ucamContext.PreviousEducations.Add(SSCObj);
                    ucamContext.SaveChanges();


                    DAL.PreviousEducation HSCObj = new DAL.PreviousEducation();
                    HSCObj.PersonId = PersonId;
                    HSCObj.EducationTypeId = 1;
                    HSCObj.EducationCategoryId = HSCId;
                    if (HSCYear.ToLower() != "n/a")
                        HSCObj.PassingYear = Convert.ToInt32(HSCYear);
                    if (HSCGPA.ToLower() != "n/a")
                        HSCObj.MarksOrGPA = HSCGPA;

                    HSCObj.CreatedBy = MigratedBy;
                    HSCObj.CreatedDate = DateTime.Now;

                    ucamContext.PreviousEducations.Add(HSCObj);
                    ucamContext.SaveChanges();
                }


            }
            catch (Exception ex)
            {
            }
        }

        private static int InsertIntoUserAndUserInPersonTable(string Roll, int PersonId, int MigratedBy)
        {
            int UserId = 0;
            try
            {
                User usr = UserManager.GetByLogInId(Roll);
                if (usr == null)
                {

                    User NewObj = new User();

                    NewObj.LogInID = Roll;
                    NewObj.Password = "123456@#";
                    NewObj.RoleID = Convert.ToInt32(CommonUtility.CommonEnum.Role.Student);
                    NewObj.RoleExistStartDate = DateTime.Now;
                    NewObj.RoleExistEndDate = DateTime.Now.AddYears(6);
                    NewObj.IsActive = true;
                    NewObj.CreatedBy = MigratedBy;
                    NewObj.CreatedDate = DateTime.Now;
                    NewObj.ModifiedBy = MigratedBy;
                    NewObj.ModifiedDate = DateTime.Now;

                    UserId = UserManager.Insert(NewObj);

                    if (UserId > 0)
                    {
                        UserInPerson uip = new UserInPerson();

                        uip.User_ID = UserId;
                        uip.PersonID = PersonId;
                        uip.CreatedBy = MigratedBy;
                        uip.ModifiedDate = DateTime.Now;

                        UserInPersonManager.Insert(uip);
                    }
                }
                else
                    UserId = -1;
            }
            catch (Exception ex)
            {
            }
            return UserId;
        }

        private static int InsertIntoStudentTable(int ProgramId, int SessionId, string Roll, int PersonId, int MigratedBy, string RegNo, string AdmisstionRoll, int MeritPosition)
        {
            int StudentId = 0;
            try
            {
                Student stdObj = StudentManager.GetByRoll(Roll);
                if (stdObj == null)
                {
                    int BatchId = 0;

                    try
                    {
                        DAL.NITER_UCAMEntities ucamContext = new DAL.NITER_UCAMEntities();
                        var BatchInfo = ucamContext.Batches.Where(x => x.ProgramId == ProgramId && x.AcaCalId == SessionId).FirstOrDefault();
                        if (BatchInfo != null)
                            BatchId = BatchInfo.BatchId;
                    }
                    catch (Exception ex)
                    {
                    }

                    Student NewObj = new Student();

                    NewObj.ProgramID = ProgramId;
                    NewObj.StudentAdmissionAcaCalId = SessionId;
                    NewObj.BatchId = BatchId;
                    NewObj.Roll = Roll;
                    NewObj.IsActive = true;
                    NewObj.PersonID = PersonId;
                    NewObj.AdmissionRoll = AdmisstionRoll;
                    NewObj.MeritPosition = MeritPosition;
                    NewObj.CreatedBy = MigratedBy;
                    NewObj.CreatedDate = DateTime.Now;
                    NewObj.RegistrationNo = RegNo;
                    StudentId = StudentManager.Insert(NewObj);

                }
            }
            catch (Exception ex)
            {
            }

            return StudentId;
        }

        private static int InsertIntoPersonTable(string Name, string Mobile, string Email, string DOB, string GName, string GMobile, string BG, int MigratedBy, string FatherName, string MotherName)
        {
            int Id = 0;
            try
            {
                Person NewObj = new Person();


                NewObj.FullName = Name;
                NewObj.Phone = Mobile;
                NewObj.SMSContactSelf = Mobile;
                NewObj.Email = Email;
                if (DOB.ToLower() != "")
                {
                    try
                    {
                        DateTime DateInDateTime = Convert.ToDateTime(DOB); //DateTime.ParseExact(DOB.Replace("/", string.Empty), "ddMMyyyy", null);
                        NewObj.DOB = Convert.ToDateTime(DateInDateTime);
                    }
                    catch (Exception ex)
                    {
                    }

                }
                NewObj.GuardianName = GName;
                NewObj.SMSContactGuardian = GMobile;
                NewObj.FatherName = FatherName;
                NewObj.MotherName = MotherName;
                NewObj.CreatedBy = MigratedBy;
                NewObj.CreatedDate = DateTime.Now;

                Id = PersonManager.Insert(NewObj);

            }
            catch (Exception ex)
            {
            }

            return Id;
        }

        private static bool CheckRoll(string Roll)
        {
            bool IsExists = false;

            Student std = StudentManager.GetByRoll(Roll);

            if (std != null)
                IsExists = true;

            return IsExists;

        }

        private static void InsertLog(string EventName, string Message, string Roll, string LoginId, string _pageId, string _pageName, string _pageUrl)
        {
            LogGeneralManager.Insert(
                                      DateTime.Now,
                                      "",
                                      "",
                                      LoginId,
                                      "",
                                      "",
                                      EventName,
                                      Message,
                                      "Normal",
                                       _pageId.ToString(),
                                      _pageName.ToString(),
                                      _pageUrl,
                                      Roll);
        }


    }
}