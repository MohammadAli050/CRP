using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility
{
    public static class CommonEnum
    {
        public enum ValueSet
        {
            Day = 1,
            ExamType = 2,
            PersonType = 3,
            SectionGender = 4
        }

        public enum ControlId
        {
            // Syntex
            //PageName_ControlName = value
            PreAdvising_lnkBtnOpenCourse = 1,
            Registration_lnkBtnOpenCourse = 2,
        }

        public enum PersonType
        {
            Student = 11,
            Faculty = 12,
            Employee = 13
        }

        public enum CourseStatus
        {
            I = 1, //Incomplete, No GPA, No T
            R = 2, //Retake
            U = 3, //Unauthorized Withdrawal
            W = 4, //Withdrawal, No GPA, No T
            Ad = 5, //Add course
            Dp = 6, //Dropped, NoGPA, NoT
            F = 7, //Fail
            Pt = 8, //Passed, T
            Rn = 9, //Running
            Rs = 10, //Registered
            Wv = 11, //Waiver
            Tr = 12, //Transfer
            T = 13, //Transcript
            Pn = 14, //Passed, NoT
            DR = 15, //Drop Request
            WR = 16 //Withdraw Request
        }

        public enum SectionGender
        {
            Male = 14,
            Female = 15,
            Mixed = 16
        }

        public enum DiscountType
        {
            Diploma = 29,
            FreedomFighter = 16,
            Others = 11,
            ParentChildren = 32,
            Scholarchip = 14,
            Sibling = 30,
            SpecialWaiver100 = 33,
            SpecialWaiver75 = 34,
            Spouse = 31,
            TuitionWaiver = 18,
            AdmissionFair = 35
        }

        public enum PaymentType
        {
            Cash = 1,
            Bank = 2
        }

        public enum CertificateType
        {
            Provisional = 1,
            Main = 2
        }

        public enum PageName
        {
            BillCollection = 100101,
            FeeSetup = 100102,
            LateFineEntry = 100103,
            BillManualEntry = 100104,
            StudentGeneralBill = 100105,
            StudentDiscountInitial = 100106,
            StudentDiscountCurrent = 100107,
            StudentGradeChange = 100108,
            StudentCourseDrop = 100109,
            Registration = 100110,
            ForceRegistration = 100111,
            Admin_CourseDropApprove = 100112,
            StudentDiscountInitialPage = 100113,
            StudentDiscountCurrentPage = 100114,
            StudentBlock = 100115,
            Admin_ClassRoutine = 100116,
            SMSSetup = 10117,
            ExamStudentPresent = 100118,
            ExamStatusEntry = 100119,
            LateRegistrationFineEntry = 100120,
            CurriculumDistributionNew = 100158,
            EquivalentCourseUI = 100123,
            PreRequisiteUI = 100124,
            CourseExplorer = 100132,
            ClassRoutine = 100133,
            TreeMaster = 100134,
            StudentCourseHistoryEdit = 100135,
            SectionChangeAfterReg = 100136,
            UnRegistration = 100137,
            DayScheduleMaster = 100138,
            CurriculumBuilder = 100139,
            ExamMarkSubmit = 100140,
            ResultPublishCourseWise = 100141,
            ResultPublishStudentWise = 100142,
            ClassAttendanceEntry = 100143,
            GradeSheetDownload = 100144,
            AttendanceSheetDownload = 100145,
            UploadResultXLFile = 100146,
            DegreeCompletion = 100147,
            StudentTranscriptInfoEntry = 100148,



            InstituteProgramRelationSetup = 100150,
            Batch = 100151,
            AcademicCalenderInformation = 100152,
            DateTimeSetupNew = 100153,
            UserControl = 100154,
            TeacherManagement = 100155,
            StudentYearSemesterPromotion = 100156,
            CourseRegistrationForwardByInstitute = 100157,
            AssignCourseTree = 100158,
            UserInstituteProgramAccessSetup = 100159,
            CourseRegistrationApprovedByCOE = 100160,
            ExamTemplateSetup=100161,
            ExamTemplateItemSetup=100162,
            MarksTemplateAndPersonAssign= 100163,
            ExamMarksUploadAndDownload=100164,
            ExamMarksPublishAndUnsubmit= 100165,
            ExamResultProcess=100166,
            SystemImageConfiguration=100167
        }

        public enum AddressType
        {
            PresentAddress = 1,
            PermanentAddress = 2,
            GuardianAddress = 3,
            MailingAddress = 4
        }

        public enum Degree
        {
            Under_Graduate = 1,
            Graduate = 2,
            Other = 3
        }

        public enum Gender
        {
            Male = 1,
            Female = 2,
            Other = 3
        }

        public enum MaritalStatus
        {
            single = 1,
            married = 2,
            divorced = 3,
            widowed = 4,
            Other = 5
        }

        public enum TeacherType
        {
            FullTime = 1,
            PartTime = 2,
            HalfTime = 3
        }

        public enum EducationBoard
        {
            Dhaka = 1,
            Rajshahi = 2,
            Comilla = 3,
            Jessore = 4,
            Chittagong = 5,
            Barisal = 6,
            Sylhet = 7,
            Dinajpur = 8,
            Madrasah = 9,
            Other = 10,
            Edexel = 11,
            Cambridge = 12
        }

        public enum EducationCategory
        {
            Secondary = 1,
            Higher_Secondary = 2,
            Undergraduate = 3,
            Graduate = 4,
            Other = 5
        }

        public enum Religion
        {
            Islam = 1,
            Hinduism = 2,
            Christianity = 3,
            Buddhism = 4,
            Other = 5
        }

        public enum ActivityType
        {
            Registration = 5,
            Evaluation = 6,
            CourseRegistrationForwardByInstitute = 7,
            CourseRegistrationApproveByInstitute = 8
        }

        public enum ExamTemplateType
        {
            Basic = 1,
            Calculative = 2
        }

        public enum ExamCalculationType
        {
            Average = 1,
            BestOne = 2,
            BestTwo = 3,
            BestThree = 4,
            Sum = 5
        }

        public enum Role
        {
            Admin = 1,
            Controller = 2,
            Coordinator = 3,
            Student = 4
        }
    }
}
