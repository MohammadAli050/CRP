using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessLogic;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class Student
    {
        public int StudentID { get; set; }
        public string Roll { get; set; }
        public int PersonID { get; set; }
        public int BatchId { get; set; }
        public int ProgramID { get; set; }
        public int SessionId { get; set; }
        public Nullable<int> GradeMasterId { get; set; }
        public Nullable<int> AccountHeadsID { get; set; }
        public Nullable<int> TreeMasterID { get; set; }
        public Nullable<int> TreeCalendarMasterID { get; set; }
        public Nullable<int> CandidateId { get; set; }
        public Nullable<int> Major1NodeID { get; set; }
        public Nullable<int> Major2NodeID { get; set; }
        public Nullable<int> Major3NodeID { get; set; }
        public Nullable<int> Minor1NodeID { get; set; }
        public Nullable<int> Minor2NodeID { get; set; }
        public Nullable<int> Minor3NodeID { get; set; }
        public string History { get; set; }
        public Nullable<bool> IsCompleted { get; set; }
        public Nullable<int> CompletedAcaCalId { get; set; }
        public string TranscriptSerial { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> Pre_Math { get; set; }
        public Nullable<bool> Pre_English { get; set; }
        public Nullable<bool> IsDiploma { get; set; }
        public bool IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsProvisionalAdmission { get; set; }
        public Nullable<DateTime> ValidUptoProAdmissionDate { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }


        #region Custom Property

        public Program Program
        {
            get
            {
                return ProgramManager.GetById(ProgramID);
            }
        }
        public Person BasicInfo
        {
            get
            {
                Person person = new Person();
                person = PersonManager.GetById(PersonID);
                return person;
            }
        }
        public string Name
        {
            get
            {
                return BasicInfo.FullName;
            }
        }
        public Batch Batch
        {
            get
            {
                return BatchManager.GetById(BatchId);
            }
        }
        public string Major1NodeName
        {
            get
            {
                if (Major1NodeID != null)
                {
                    Node node = NodeManager.GetById(Major1NodeID);
                    if (node != null)
                    {
                        return node.Name;
                    }
                    else
                        return " -- ";
                }
                else
                    return " -- ";
            }
        }
        public string Major2NodeName
        {
            get
            {
                if (Major2NodeID != null)
                {
                    Node node = NodeManager.GetById(Major2NodeID);
                    if (node != null)
                    {
                        return node.Name;
                    }
                    else
                        return " -- ";
                }
                else
                    return " -- ";
            }
        }
        public string Major3NodeName
        {
            get
            {
                if (Major3NodeID != null)
                {
                    Node node = NodeManager.GetById(Major3NodeID);
                    if (node != null)
                    {
                        return node.Name;
                    }
                    else
                        return " -- ";
                }
                else
                    return " -- ";
            }
        }
        public string Minor1NodeName
        {
            get
            {
                if (Minor1NodeID != null)
                {
                    Node node = NodeManager.GetById(Minor1NodeID);
                    if (node != null)
                    {
                        return node.Name;
                    }
                    else
                        return " -- ";
                }
                else
                    return " -- ";
            }
        }
        public string Minor2NodeName
        {
            get
            {
                if (Minor2NodeID != null)
                {
                    Node node = NodeManager.GetById(Minor2NodeID);
                    if (node != null)
                    {
                        return node.Name;
                    }
                    else
                        return " -- ";
                }
                else
                    return " -- ";
            }
        }
        public string Minor3NodeName
        {
            get
            {
                if (Minor3NodeID != null)
                {
                    Node node = NodeManager.GetById(Minor3NodeID);
                    if (node != null)
                    {
                        return node.Name;
                    }
                    else
                        return " -- ";
                }
                else
                    return " -- ";
            }
        }
        public string CompletedCr
        {
            get
            {
                List<StudentCourseHistory> list = StudentCourseHistoryManager.GetAllByStudentId(StudentID);

                //decimal waved = list.Where(l => l.CourseStatusID == 11 || l.CourseStatusID == 12).Sum(s => s.CompletedCredit);
                decimal completed = list.Where(l => Convert.ToInt32(l.CourseStatusID) == 8).Sum(s => Convert.ToInt32(s.CompletedCredit));
                decimal running = list.Where(l => Convert.ToInt32(l.CourseStatusID) == 9).Sum(s => Convert.ToInt32(s.CompletedCredit));

                decimal total = completed + running;

                return completed.ToString("0.0") + " + " + running.ToString("0.0") + " = " + total.ToString("0.0");
            }
        }
        public string EarnedCr
        {
            get
            {
                List<StudentCourseHistory> list = StudentCourseHistoryManager.GetAllByStudentId(StudentID);

                decimal EarnedCredit = list.Where(l => (l.CourseStatusID == 8 || l.CourseStatusID == 11 || l.CourseStatusID == 12) && l.IsConsiderGPA == true).Sum(s => s.CourseCredit);

                return EarnedCredit.ToString("0.0");
            }
        }
        public string CompletionSemester
        {
            get
            {
                StudentACUDetail stdACU = StudentACUDetailManager.GetLatestCGPAByStudentId(StudentID);
                return stdACU == null ? "" : stdACU.AcademicCalender.CalenderUnitType.TypeName + " " + stdACU.AcademicCalender.Year.ToString();
            }
        }
        public int PreCourseCount
        {
            get
            {
                List<StudentPreCourse> list = StudentPreCourseManager.GetByStudentId(StudentID);

                return list.Count();
            }
        }
        public string CourseTreeLinkCalendars
        {
            get
            {
                TreeMaster treeMaster = TreeMasterManager.GetById(TreeMasterID);
                if (treeMaster != null)
                {
                    Node node = NodeManager.GetById(treeMaster.RootNodeID);
                    TreeCalendarMaster treeCalendarMaster = TreeCalendarMasterManager.GetById(TreeCalendarMasterID);
                    if (treeCalendarMaster != null && node != null)
                        return node.Name + " »» " + treeCalendarMaster.Name;
                    else
                        return string.Empty;
                }
                else
                    return string.Empty;
            }
        }
        public StudentACUDetail CurrentSeccionResult
        {
            get
            {
                StudentACUDetail studentACUDetail = StudentACUDetailManager.GetCurrentCgpaByStudentId(StudentID);
                return studentACUDetail;
            }
        }
        public string AutoOpen
        {
            get
            {
                List<RegistrationWorksheet> list = RegistrationWorksheetManager.GetByStudentID(StudentID);

                if (list != null)
                {
                    if (list.Where(l => l.IsAutoOpen == true).Count() > 0)
                    {
                        decimal cr = list.Where(l => l.IsAutoOpen == true).Sum(s => s.Credits);

                        return "Opened - " + cr.ToString("0.0");
                    }
                    else
                        return "Closed";
                }
                else
                    return "Closed";
            }
        }
        public string AutoPreReg
        {
            get
            {
                List<RegistrationWorksheet> list = RegistrationWorksheetManager.GetByStudentID(StudentID);

                if (list != null)
                {
                    if (list.Where(l => l.IsAutoAssign == true).Count() > 0)
                    {
                        decimal cr = list.Where(l => l.IsAutoAssign == true).Sum(s => s.Credits);

                        return "Assign - " + cr.ToString("0.0");
                    }
                    else
                        return "Closed";
                }
                else
                    return "Closed";
            }
        }
        public string AutoMandaotry
        {

            get
            {
                List<RegistrationWorksheet> list = RegistrationWorksheetManager.GetByStudentID(StudentID);

                if (list != null)
                {
                    if (list.Where(l => l.IsMandatory == true).Count() > 0)
                    {
                        decimal cr = list.Where(l => l.IsMandatory == true).Sum(s => s.Credits);

                        return "Mandatory - " + cr.ToString("0.0");
                    }
                    else
                        return "Closed";
                }
                else
                    return "Closed";
            }
        }
        public bool IsGeneratedWorksheet
        {
            get
            {
                bool isGenerate = false;

                List<RegistrationWorksheet> list = RegistrationWorksheetManager.GetRegistrationSessionDataByStudentID(StudentID);
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        isGenerate = true;
                    }
                    else
                    {
                        isGenerate = false;
                    }
                }
                else
                {
                    isGenerate = false;
                }


                return isGenerate;
            }
        }
        public string OpenedCourse
        {
            get
            {
                string openedCourse = string.Empty;
                int i = 0;
                List<RegistrationWorksheet> collection = RegistrationWorksheetManager.GetByStudentID(StudentID);
                foreach (RegistrationWorksheet item in collection)
                {
                    if (item.IsAutoOpen)
                    {
                        openedCourse += ++i + ") " + item.FormalCode + "; ";
                    }
                }

                return openedCourse;
            }
        }
        public string AssignedCourse
        {
            get
            {
                string assignedCourse = string.Empty;
                int i = 0;
                List<RegistrationWorksheet> collection = RegistrationWorksheetManager.GetByStudentID(StudentID);
                foreach (RegistrationWorksheet item in collection)
                {
                    if (item.IsAutoAssign)
                    {
                        assignedCourse += ++i + ") " + item.FormalCode + "; ";
                    }
                }

                return assignedCourse;
            }
        }
        public string MandatoryCourse
        {
            get
            {
                string mandatoryCourse = string.Empty;
                int i = 0;
                List<RegistrationWorksheet> collection = RegistrationWorksheetManager.GetByStudentID(StudentID);
                foreach (RegistrationWorksheet item in collection)
                {
                    if (item.IsMandatory)
                    {
                        mandatoryCourse += ++i + ") " + item.FormalCode + "; ";
                    }
                }

                return mandatoryCourse;
            }
        }
        public string LoadStudentCourse(int p)
        {
            throw new NotImplementedException();
        } // discurrage to use this property        
        public bool IsBlock
        {
            get
            {
                bool isBlock = false;

                PersonBlock block = PersonBlockManager.GetByPersonId((int)PersonID);

                if (block == null)
                { isBlock = false; }
                else if (block.IsAdmitCardBlock)
                { isBlock = true; }
                else if (block.IsRegistrationBlock)
                { isBlock = true; }

                return isBlock;
            }
        }
        public PersonBlock Block
        {
            get
            {
                PersonBlock block = PersonBlockManager.GetByPersonId((int)PersonID);
                return block;
            }
        }
        public StudentDiscountMaster StudentDiscount
        {
            get
            {
                StudentDiscountMaster studentDiscount = StudentDiscountMasterManager.GetByStudentID(StudentID);
                return studentDiscount;
            }
        }
        public decimal LatestCGPA
        {
            get
            {
                StudentACUDetail stdACU = StudentACUDetailManager.GetLatestCGPAByStudentId(StudentID);
                return stdACU == null ? 0 : stdACU.CGPA;
            }
        }
        public bool IsRegistared
        {
            get
            {
                bool IsRegistared = false;
                AcademicCalender acal = AcademicCalenderManager.GetActiveRegistrationCalenderByProgramId(ProgramID);
                List<StudentCourseHistory> list = StudentCourseHistoryManager.GetAllByStudentIdAcaCalId(StudentID, acal.AcademicCalenderID);
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        IsRegistared = true;
                    }
                    else
                    {
                        IsRegistared = false;
                    }
                }
                else
                {
                    IsRegistared = false;
                }
                return IsRegistared;
            }
        }

        public StudentInstitution StudentInstitution
        {
            get
            {
                StudentInstitution stdIns = StudentInstitutionManager.GetByStudentId(StudentID);
                return stdIns;
            }
        }

        public string StudentRegistrationNo
        {
            get
            {
                string a = "";
                StudentRegistration stdreg = StudentRegistrationManager.GetByStudentId(StudentID);
                if (stdreg != null)
                    a = stdreg.RegistrationNo;
                return a;
            }
        }

        #endregion

        #region Temp Property
        public decimal CGPA { get; set; } // discurrage to use this property
        public decimal AutoOpenCr { get; set; } // discurrage to use this property
        public decimal AutoPreRegCr { get; set; } // discurrage to use this property
        public decimal AutoMandaotryCr { get; set; } // discurrage to use this property
        public bool IsChaked { get; set; } // discurrage to use this property
        public bool IsRegistered { get; set; } // discurrage to use this property
        public string FullName { get; set; } // discurrage to use this property 
        public string Gender
        {
            get
            {
                return BasicInfo.Gender;
            }
        }
        public string RegistrationNo { get; set; }
        public string RegSession { get; set; }
        #endregion
    }
}
