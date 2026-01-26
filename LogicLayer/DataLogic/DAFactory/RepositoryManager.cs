using LogicLayer.DataLogic.IRepository;
using LogicLayer.DataLogic.SQLRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using LogicLayer.DataLogic.IRepository;
//using LogicLayer.DataLogic.SQLRepository;
using LogicLayer.DataLogic;

namespace LogicLayer.DataLogic.DAFactory
{
    public class RepositoryManager
    {
        public static IGenerateWorksheetRepository GenerateWorksheet_Repository
        {
            get
            {
                IGenerateWorksheetRepository repository = new SQLGenerateWorksheetRepository();
                return repository;
            }
        }
        public static IRegistrationWorksheetRepository RegistrationWorksheet_Repository
        {
            get
            {
                IRegistrationWorksheetRepository repository = new SQLRegistrationWorksheetRepository();
                return repository;
            }
        }
        public static ISectionDTORepository SectionEntity_Repository
        {
            get
            {
                ISectionDTORepository repository = new SQLSectionDTORepository();
                return repository;
            }
        }
        public static IAcademicCalenderSectionRepository AcademicCalenderSection_Repository
        {
            get
            {
                IAcademicCalenderSectionRepository repository = new SQLAcademicCalenderSectionRepository();
                return repository;
            }
        }
        public static ITransferStudentRepository TransferStudent_Repository
        {
            get
            {
                ITransferStudentRepository repository = new SQLTransferStudentRepository();
                return repository;
            }
        }
        public static ITimeSlotPlanRepository TimeSlotPlan_Repository
        {
            get
            {
                ITimeSlotPlanRepository repository = new SQLTimeSlotPlanRepository();
                return repository;
            }
        }
        public static IUserAccessProgramRepository UserAccessProgram_Repository
        {
            get
            {
                IUserAccessProgramRepository repository = new SqlUserAccessProgramRepository();
                return repository;
            }
        }
        public static IPreregistrationCountDTORepository PreregistrationCountDTO_Repository
        {
            get
            {
                IPreregistrationCountDTORepository repository = new SQLPreregistrationCountDTORepository();
                return repository;
            }
        }
        public static IOfferedCourseRepository OfferedCourse_Repository
        {
            get
            {
                IOfferedCourseRepository repository = new SQLOfferedCourseRepository();
                return repository;
            }
        }
        public static IStudentCourseHistoryRepository StudentCourseHistory_Repository
        {
            get
            {
                IStudentCourseHistoryRepository repository = new SQLStudentCourseHistoryRepository();
                return repository;
            }
        }
        public static ICourseRepository Course_Repository
        {
            get
            {
                ICourseRepository repository = new SQLCourseRepository();
                return repository;
            }
        }
        public static IAcademicCalenderRepository AcademicCalender_Repository
        {
            get
            {
                IAcademicCalenderRepository repository = new SQLAcademicCalenderRepository();
                return repository;
            }
        }
        public static ICalenderUnitMasterRepository CalenderUnitMaster_Repository
        {
            get
            {
                ICalenderUnitMasterRepository repository = new SQLCalenderUnitMasterRepository();
                return repository;
            }
        }
        public static ICalenderUnitTypeRepository CalenderUnitType_Repository
        {
            get
            {
                ICalenderUnitTypeRepository repository = new SQLCalenderUnitTypeRepository();
                return repository;
            }
        }
        public static IDepartmentRepository Department_Repository
        {
            get
            {
                IDepartmentRepository repository = new SQLDepartmentRepository();
                return repository;
            }
        }
        public static INodeRepository Node_Repository
        {
            get
            {
                INodeRepository repository = new SQLNodeRepository();
                return repository;
            }
        }
        public static IProgramRepository Program_Repository
        {
            get
            {
                IProgramRepository repository = new SQLProgramRepository();
                return repository;
            }
        }
        public static IProgramTypeRepository ProgramType_Repository
        {
            get
            {
                IProgramTypeRepository repository = new SQLProgramTypeRepository();
                return repository;
            }
        }
        public static ISchoolRepository School_Repository
        {
            get
            {
                ISchoolRepository repository = new SQLSchoolRepository();
                return repository;
            }
        }
        public static IStudentRepository Student_Repository
        {
            get
            {
                IStudentRepository repository = new SQLStudentRepository();
                return repository;
            }
        }
        public static IStudentRepository RunningStudent_Repository
        {
            get
            {
                IStudentRepository repository = new SQLStudentRepository();
                return repository;
            }
        }
        public static ITreeDetailRepository TreeDetail_Repository
        {
            get
            {
                ITreeDetailRepository repository = new SQLTreeDetailRepository();
                return repository;
            }
        }
        public static ITreeMasterRepository TreeMaster_Repository
        {
            get
            {
                ITreeMasterRepository repository = new SQLTreeMasterRepository();
                return repository;
            }
        }
        public static ITreeCalendarMasterRepository TreeCalendarMaster_Repository
        {
            get
            {
                ITreeCalendarMasterRepository repository = new SQLTreeCalendarMasterRepository();
                return repository;
            }
        }
        public static IStudentPreCourseRepository StudentPreCourse_Repository
        {
            get
            {
                IStudentPreCourseRepository repository = new SQLStudentPreCourseRepository();
                return repository;
            }
        }
        public static IPersonRepository Person_Repository
        {
            get
            {
                IPersonRepository repository = new SQLPersonRepository();
                return repository;
            }
        }
        public static IEmployeeRepository Employee_Repository
        {
            get
            {
                IEmployeeRepository repository = new SQLEmployeeRepository();
                return repository;
            }
        }
        public static IExamRoutineRepository ExamRoutine_Repository
        {
            get
            {
                IExamRoutineRepository repository = new SQLExamRoutineRepository();
                return repository;
            }
        }
        public static IExamRoutineRepository GetExamRoutine_Repository
        {
            get
            {
                IExamRoutineRepository repository = new SQLExamRoutineRepository();
                return repository;
            }
        }
        public static IRoomInformationRepository RoomInformation_Repository
        {
            get
            {
                IRoomInformationRepository repository = new SQLRoomInformationRepository();
                return repository;
            }
        }
        public static ICourseListByNodeDTORepository CourseListByNodeDTO_Repository
        {
            get
            {
                ICourseListByNodeDTORepository repository = new SQLCourseListByNodeDTORepository();
                return repository;
            }
        }
        public static IValueRepository Value_Repository
        {
            get
            {
                IValueRepository repository = new SQLValueRepository();
                return repository;
            }
        }
        public static IValueSetRepository ValueSet_Repository
        {
            get
            {
                IValueSetRepository repository = new SQLValueSetRepository();
                return repository;
            }
        }
        public static IStudentACUDetailRepository StudentACUDetail_Repository
        {
            get
            {
                IStudentACUDetailRepository repository = new SQLStudentACUDetailRepository();
                return repository;
            }
        }
        public static IDeptRegSetUpRepository DeptRegSetUp_Repository
        {
            get
            {
                IDeptRegSetUpRepository repository = new SQLDeptRegSetUpRepository();
                return repository;
            }
        }
        public static IAccountHeadsRepository AccountHeads_Repository
        {
            get
            {
                IAccountHeadsRepository repository = new SQLAccountHeadsRepository();
                return repository;
            }
        }
        public static IFeeSetupRepository FeeSetup_Repository
        {
            get
            {
                IFeeSetupRepository repository = new SQLFeeSetupRepository();
                return repository;
            }
        }
        public static ISiblingSetupRepository SiblingSetup_Repository
        {
            get
            {
                ISiblingSetupRepository repository = new SQLSiblingSetupRepository();
                return repository;
            }
        }
        public static IReportRepository Report_Repository
        {
            get
            {
                IReportRepository repository = new SQLReportRepository();
                return repository;
            }
        }
        public static ITypeDefinitionRepository TypeDefinition_Repository
        {
            get
            {
                ITypeDefinitionRepository repository = new SQLTypeDefinitionRepository();
                return repository;
            }
        }
        public static IVoucherRepository Voucher_Repository
        {
            get
            {
                IVoucherRepository repository = new SQLVoucherRepository();
                return repository;
            }
        }
        public static IMenuRepository Menu_Repository
        {
            get
            {
                IMenuRepository repository = new SQLMenuRepository();
                return repository;
            }
        }
        public static IRoleRepository Role_Repository
        {
            get
            {
                IRoleRepository repository = new SQLRoleRepository();
                return repository;
            }
        }
        public static IRoleMenuRepository RoleMenu_Repository
        {
            get
            {
                IRoleMenuRepository repository = new SQLRoleMenuRepository();
                return repository;
            }
        }
        public static IUserRepository User_Repository
        {
            get
            {
                IUserRepository repository = new SQLUserRepository();
                return repository;
            }
        }
        public static IUsrPermsnRepository UsrPermsn_Repository
        {
            get
            {
                IUsrPermsnRepository repository = new SQLUsrPermsnRepository();
                return repository;
            }
        }
        public static IFrmDsnrDetailRepository FrmDsnrDetail_Repository
        {
            get
            {
                IFrmDsnrDetailRepository repository = new SQLFrmDsnrDetailRepository();
                return repository;
            }
        }
        public static IFrmDsnrMasterRepository FrmDsnrMaster_Repository
        {
            get
            {
                IFrmDsnrMasterRepository repository = new SQLFrmDsnrMasterRepository();
                return repository;
            }
        }
        public static IDiscountContinuationSetupRepository DiscountContinuationSetup_Repository
        {
            get
            {
                IDiscountContinuationSetupRepository repository = new SQLDiscountContinuationSetupRepository();
                return repository;
            }
        }
        public static ICourseBillableRepository CourseBillable_Repository
        {
            get
            {
                ICourseBillableRepository repository = new SQLCourseBillableRepository();
                return repository;
            }
        }
        public static IRelationBetweenDiscountCourseTypeRepository RelationBetweenDiscountCourseType_Repository
        {
            get
            {
                IRelationBetweenDiscountCourseTypeRepository repository = new SQLRelationBetweenDiscountCourseTypeRepository();
                return repository;
            }
        }
        public static IRelationBetweenDiscountRetakeRepository RelationBetweenDiscountRetake_Repository
        {
            get
            {
                IRelationBetweenDiscountRetakeRepository repository = new SQLRelationBetweenDiscountRetakeRepository();
                return repository;
            }
        }
        public static IRelationBetweenDiscountSectionTypeRepository RelationBetweenDiscountSectionType_Repository
        {
            get
            {
                IRelationBetweenDiscountSectionTypeRepository repository = new SQLRelationBetweenDiscountSectionTypeRepository();
                return repository;
            }
        }
        public static IStdCrsBillWorksheetRepository StdCrsBillWorksheet_Repository
        {
            get
            {
                IStdCrsBillWorksheetRepository repository = new SQLStdCrsBillWorksheetRepository();
                return repository;
            }
        }
        public static IStudentDiscountWorkSheetRepository StudentDiscountWorkSheet_Repository
        {
            get
            {
                IStudentDiscountWorkSheetRepository repository = new SQLStudentDiscountWorkSheetRepository();
                return repository;
            }
        }
        public static IAcademicCalenderExamSchedulerRepository AcademicCalenderExamScheduler_Repository
        {
            get
            {
                IAcademicCalenderExamSchedulerRepository repository = new SQLAcademicCalenderExamSchedulerRepository();
                return repository;
            }
        }
        public static ICalCourseProgNodeRepository CalCourseProgNode_Repository
        {
            get
            {
                ICalCourseProgNodeRepository repository = new SQLCalCourseProgNodeRepository();
                return repository;
            }
        }
        public static ICalenderUnitDistributionRepository CalenderUnitDistribution_Repository
        {
            get
            {
                ICalenderUnitDistributionRepository repository = new SQLCalenderUnitDistributionRepository();
                return repository;
            }
        }
        public static IClassRoutineRepository ClassRoutine_Repository
        {
            get
            {
                IClassRoutineRepository repository = new SQLClassRoutineRepository();
                return repository;
            }
        }

        public static IClassRoutineRepository ClassRoutine_RepositoryByProgram
        {
            get
            {
                IClassRoutineRepository repository = new SQLClassRoutineRepository();
                return repository;
            }
        }
        public static IPrerequisiteDetailRepository PrerequisiteDetail_Repository
        {
            get
            {
                IPrerequisiteDetailRepository repository = new SQLPrerequisiteDetailRepository();
                return repository;
            }
        }
        public static IPrerequisiteMasterRepository PrerequisiteMaster_Repository
        {
            get
            {
                IPrerequisiteMasterRepository repository = new SQLPrerequisiteMasterRepository();
                return repository;
            }
        }
        public static IGradeSheetTemplateRepository GradeSheetTemplate_Repository
        {
            get
            {
                IGradeSheetTemplateRepository repository = new SQLGradeSheetTemplateRepository();
                return repository;
            }
        }
        public static ICourseACUSpanDtlRepository CourseACUSpanDtl_Repository
        {
            get
            {
                ICourseACUSpanDtlRepository repository = new SQLCourseACUSpanDtlRepository();
                return repository;
            }
        }
        public static ICourseACUSpanMasRepository CourseACUSpanMas_Repository
        {
            get
            {
                ICourseACUSpanMasRepository repository = new SQLCourseACUSpanMasRepository();
                return repository;
            }
        }
        public static IEquiCourseRepository EquiCourse_Repository
        {
            get
            {
                IEquiCourseRepository repository = new SQLEquiCourseRepository();
                return repository;
            }
        }
        public static IRoomTypeRepository RoomType_Repository
        {
            get
            {
                IRoomTypeRepository repository = new SQLRoomTypeRepository();
                return repository;
            }
        }
        public static IExamMarksAllocationRepository ExamMarksAllocation_Repository
        {
            get
            {
                IExamMarksAllocationRepository repository = new SQLExamMarksAllocationRepository();
                return repository;
            }
        }
        public static IExamTypeNameRepository ExamTypeName_Repository
        {
            get
            {
                IExamTypeNameRepository repository = new SQLExamTypeNameRepository();
                return repository;
            }
        }
        public static IGradeDetailsRepository GradeDetails_Repository
        {
            get
            {
                IGradeDetailsRepository repository = new SQLGradeDetailsRepository();
                return repository;
            }
        }
        public static IGradeSheetRepository GradeSheet_Repository
        {
            get
            {
                IGradeSheetRepository repository = new SQLGradeSheetRepository();
                return repository;
            }
        }
        public static INode_CourseRepository Node_Course_Repository
        {
            get
            {
                INode_CourseRepository repository = new SQLNode_CourseRepository();
                return repository;
            }
        }
        public static IOperatorRepository Operator_Repository
        {
            get
            {
                IOperatorRepository repository = new SQLOperatorRepository();
                return repository;
            }
        }
        public static ITreeCalendarDetailRepository TreeCalendarDetail_Repository
        {
            get
            {
                ITreeCalendarDetailRepository repository = new SQLTreeCalendarDetailRepository();
                return repository;
            }
        }
        public static IVNodeSetRepository VNodeSet_Repository
        {
            get
            {
                IVNodeSetRepository repository = new SQLVNodeSetRepository();
                return repository;
            }
        }
        public static IVNodeSetMasterRepository VNodeSetMaster_Repository
        {
            get
            {
                IVNodeSetMasterRepository repository = new SQLVNodeSetMasterRepository();
                return repository;
            }
        }
        public static IAddressRepository Address_Repository
        {
            get
            {
                IAddressRepository repository = new SQLAddressRepository();
                return repository;
            }
        }
        public static IAdmissionRepository Admission_Repository
        {
            get
            {
                IAdmissionRepository repository = new SQLAdmissionRepository();
                return repository;
            }
        }
        public static ICourseWavTransfrRepository CourseWavTransfr_Repository
        {
            get
            {
                ICourseWavTransfrRepository repository = new SQLCourseWavTransfrRepository();
                return repository;
            }
        }
        public static ICourseWavTransfrDetailRepository CourseWavTransfrDetail_Repository
        {
            get
            {
                ICourseWavTransfrDetailRepository repository = new SQLCourseWavTransfrDetailRepository();
                return repository;
            }
        }
        public static ICourseStatusRepository CourseStatus_Repository
        {
            get
            {
                ICourseStatusRepository repository = new SQLCourseStatusRepository();
                return repository;
            }
        }
        public static IStudentCalCourseProgNodeRepository StudentCalCourseProgNode_Repository
        {
            get
            {
                IStudentCalCourseProgNodeRepository repository = new SQLStudentCalCourseProgNodeRepository();
                return repository;
            }
        }
        public static IStudentSkillTypeRepository StudentSkillType_Repository
        {
            get
            {
                IStudentSkillTypeRepository repository = new SQLStudentSkillTypeRepository();
                return repository;
            }
        }
        public static IStdAdmissionDiscountRepository StdAdmissionDiscount_Repository
        {
            get
            {
                IStdAdmissionDiscountRepository repository = new SQLStdAdmissionDiscountRepository();
                return repository;
            }
        }
        public static IStdDiscountCurrentRepository StdDiscountCurrent_Repository
        {
            get
            {
                IStdDiscountCurrentRepository repository = new SQLStdDiscountCurrentRepository();
                return repository;
            }
        }
        public static IStdDiscountHistoryRepository StdDiscountHistory_Repository
        {
            get
            {
                IStdDiscountHistoryRepository repository = new SQLStdDiscountHistoryRepository();
                return repository;
            }
        }
        public static ICourseWavTransfrMasterRepository CourseWavTransfrMaster_Repository
        {
            get
            {
                ICourseWavTransfrMasterRepository repository = new SQLCourseWavTransfrMasterRepository();
                return repository;
            }
        }
        public static ISkillTypeRepository SkillType_Repository
        {
            get
            {
                ISkillTypeRepository repository = new SQLSkillTypeRepository();
                return repository;
            }
        }
        public static IStatusTypeRepository StatusType_Repository
        {
            get
            {
                IStatusTypeRepository repository = new SQLStatusTypeRepository();
                return repository;
            }
        }
        public static IStdAcademicCalenderRepository Std_AcademicCalender_Repository
        {
            get
            {
                IStdAcademicCalenderRepository repository = new SQLStdAcademicCalenderRepository();
                return repository;
            }
        }
        public static IStdEducationInfoRepository StdEducationInfo_Repository
        {
            get
            {
                IStdEducationInfoRepository repository = new SQLStdEducationInfoRepository();
                return repository;
            }
        }
        public static IStudentCourseRepository Student_Course_Repository
        {
            get
            {
                IStudentCourseRepository repository = new SQLStudentCourseRepository();
                return repository;
            }
        }
        public static IStudentOldRepository Student_Old_Repository
        {
            get
            {
                IStudentOldRepository repository = new SQLStudentOldRepository();
                return repository;
            }
        }
        public static IUserInPersonRepository UserInPerson_Repository
        {
            get
            {
                IUserInPersonRepository repository = new SQLUserInPersonRepository();
                return repository;
            }
        }
        public static IRegistrationDateTimeLimitRepository RegistrationDateTimeLimit_Repository
        {
            get
            {
                IRegistrationDateTimeLimitRepository repository = new SQLRegistrationDateTimeLimitRepository();
                return repository;
            }
        }
        public static IRegistrationDateTimeLimitInBatchRepository RegistrationDateTimeLimitInBatch_Repository
        {
            get
            {
                IRegistrationDateTimeLimitInBatchRepository repository = new SQLRegistrationDateTimeLimitInBatchRepository();
                return repository;
            }
        }
        public static IActiveUserRepository ActiveUser_Repository
        {
            get
            {
                IActiveUserRepository repository = new SQLActiveUserRepository();
                return repository;
            }
        }
        public static IPickStudentAndShowRepository PickStudentAndShow_Repository
        {
            get
            {
                IPickStudentAndShowRepository repository = new SQLPickStudentAndShowRepository();
                return repository;
            }
        }
        public static IClassForceOperationRepository ClassForceOperation_Repository
        {
            get
            {
                IClassForceOperationRepository repository = new SQLClassForceOperationRepository();
                return repository;
            }
        }
        public static IPersonByUserTypeAndUserCodeRepository PersonByUserTypeAndUserCode_Repository
        {
            get
            {
                IPersonByUserTypeAndUserCodeRepository repository = new SQLPersonByUserTypeAndUserCodeRepository();
                return repository;
            }
        }
        public static IFAQRepository FAQ_Repository
        {
            get
            {
                IFAQRepository repository = new SQLFAQRepository();
                return repository;
            }
        }
        //public static IStudentDiscountInitialRepository StudentDiscountInitial_Repository
        //{
        //    get
        //    {
        //        IStudentDiscountInitialRepository repository = new SqlStudentDiscountInitialRepository();
        //        return repository;
        //    }
        //}
        public static IUserMenuRepository UserMenu_Repository
        {
            get
            {
                IUserMenuRepository repository = new SQLUserMenuRepository();
                return repository;
            }
        }
        public static IUserObjectControlRepository UserObjectControl_Repository
        {
            get
            {
                IUserObjectControlRepository repository = new SQLUserObjectControlRepository();
                return repository;
            }
        }
        public static IPersonBlockRepository PersonBlock_Repository
        {
            get
            {
                IPersonBlockRepository repository = new SQLPersonBlockRepository();
                return repository;
            }
        }
        public static IEvaluationFormRepository EvaluationForm_Repository
        {
            get
            {
                IEvaluationFormRepository repository = new SQLEvaluationFormRepository();
                return repository;
            }
        }
        public static IBillViewRepository BillView_Repository
        {
            get
            {
                IBillViewRepository repository = new SqlBillViewRepository();
                return repository;
            }
        }
        public static IStudentDiscountCurrentDetailsRepository StudentDiscountCurrentDetails_Repository
        {
            get
            {
                IStudentDiscountCurrentDetailsRepository repository = new SqlStudentDiscountCurrentDetailsRepository();
                return repository;
            }
        }
        public static IStudentDiscountInitialDetailsRepository StudentDiscountInitialDetails_Repository
        {
            get
            {
                IStudentDiscountInitialDetailsRepository repository = new SqlStudentDiscountInitialDetailsRepository();
                return repository;
            }
        }
        public static IStudentDiscountMasterRepository StudentDiscountMaster_Repository
        {
            get
            {
                IStudentDiscountMasterRepository repository = new SqlStudentDiscountMasterRepository();
                return repository;
            }
        }
        public static IGradeWiseRetakeDiscountRepository GradeWiseRetakeDiscount_Repository
        {
            get
            {
                IGradeWiseRetakeDiscountRepository repository = new SQLGradeWiseRetakeDiscountRepository();
                return repository;
            }
        }
        public static ITranscriptRepository Transcript_Repository
        {
            get
            {
                ITranscriptRepository repository = new SQLTranscriptRepository();
                return repository;
            }
        }
        public static IScholarshipListRepository ScholarshipList_Repository
        {
            get
            {
                IScholarshipListRepository repository = new SQLScholarshipListRepository();
                return repository;
            }
        }
        public static ISchemeSetupRepository SchemeSetup_Repository
        {
            get
            {
                ISchemeSetupRepository repository = new SQLSchemeSetupRepository();
                return repository;
            }
        }
        public static IStudentDiscountAndScholarshipPerSessionRepository StudentDiscountAndScholarshipPerSession_Repository
        {
            get
            {
                IStudentDiscountAndScholarshipPerSessionRepository repository = new SQLStudentDiscountAndScholarshipPerSessionRepository();
                return repository;
            }
        }
        public static IChartReportPreRegistrationRepository ChartReportPreRegistration_Repository
        {
            get
            {
                IChartReportPreRegistrationRepository repository = new SQLChartReportPreRegistrationRepository();
                return repository;
            }
        }
        public static IProbationRepository Probation_Repository
        {
            get
            {
                IProbationRepository repository = new SQLProbationRepository();
                return repository;
            }
        }
        public static IChartReportPreRegistrationMaleFemaleRepository ChartReportPreRegistrationMaleFemale_Repository
        {
            get
            {
                IChartReportPreRegistrationMaleFemaleRepository repository = new SQLChartReportPreRegistrationMaleFemaleRepository();
                return repository;
            }
        }
        public static IBatchRepository Batch_Repository
        {
            get
            {
                IBatchRepository repository = new SQLBatchRepository();
                return repository;
            }
        }
        public static IStudentGradeDetailRepository StudentGradeDetail_Repository
        {
            get
            {
                IStudentGradeDetailRepository repository = new SQLStudentGradeDetailRepository();
                return repository;
            }
        }

        #region ExamTemplate By Jahid

        public static ICriteriaTypeRepository CriteriaType_Repository
        {
            get
            {
                ICriteriaTypeRepository repository = new SQLCriteriaTypeRepository();
                return repository;
            }
        }
        public static IExamRepository Exam_Repository
        {
            get
            {
                IExamRepository repository = new SQLExamRepository();
                return repository;
            }
        }
        public static IExamSetItemRepository ExamSetItem_Repository
        {
            get
            {
                IExamSetItemRepository repository = new SQLExamSetItemRepository();
                return repository;
            }
        }
        public static IExamSetRepository ExamSet_Repository
        {
            get
            {
                IExamSetRepository repository = new SQLExamSetRepository();
                return repository;
            }
        }
        public static ITemplateGroupRepository TemplateGroup_Repository
        {
            get
            {
                ITemplateGroupRepository repository = new SQLTemplateGroupRepository();
                return repository;
            }
        }
        public static IExamTemplateRepository ExamTemplate_Repository
        {
            get
            {
                IExamTemplateRepository repository = new SQLExamTemplateRepository();
                return repository;
            }
        }
        public static ICourseSectionRepository CourseSection_Repository
        {
            get
            {
                ICourseSectionRepository repository = new SQLCourseSectionRepository();
                return repository;
            }
        }
        public static ICourseTemplateRepository CourseTemplate_Repository
        {
            get
            {
                ICourseTemplateRepository repository = new SQLCourseTemplateRepository();
                return repository;
            }
        }
        public static ITeacherCourseSectionRepository TeacherCourseSection_Repository
        {
            get
            {
                ITeacherCourseSectionRepository repository = new SQLTeacherCourseSectionRepository();
                return repository;
            }
        }
        public static ITeacherRepository Teacher_Repository
        {
            get
            {
                ITeacherRepository repository = new SQLTeacherRepository();
                return repository;
            }
        }
        public static IExamMarkRepository StudentResult_Repository
        {
            get
            {
                IExamMarkRepository repository = new SQLExamMarkRepository();
                return repository;
            }
        }

        public static IExamMarkReplicaRepository ExamMarkReplica_Repository
        {
            get
            {
                IExamMarkReplicaRepository repository = new SqlExamMarkReplicaRepository();
                return repository;
            }
        }

        #endregion ExamTemplate By Jahid

        public static ICourseListByProgram CourseListByProgram_Repository
        {
            get
            {
                ICourseListByProgram repository = new SQLCourseByProgram();
                return repository;
            }
        }

        #region ExamRoutine[Sajib]

        public static IExamScheduleSetRepository ExamScheduleSet_Repository
        {
            get
            {
                IExamScheduleSetRepository repository = new SQLExamScheduleSetRepository();
                return repository;
            }
        }

        public static IExamScheduleDayRepository ExamScheduleDay_Repository
        {
            get
            {
                IExamScheduleDayRepository repository = new SqlExamScheduleDayRepository();
                return repository;
            }
        }

        public static IExamScheduleTimeSlotRepository ExamScheduleTimeSlot_Repository
        {
            get
            {
                IExamScheduleTimeSlotRepository repository = new SqlExamScheduleTimeSlotRepository();
                return repository;
            }
        }

        public static ICoursePredictMasterRepository CoursePredictMaster_Repository
        {
            get
            {
                ICoursePredictMasterRepository repository = new SqlCoursePredictMasterRepository();
                return repository;
            }
        }

        public static ICoursePredictDetailsRepository CoursePredictDetails_Repository
        {
            get
            {
                ICoursePredictDetailsRepository repository = new SqlCoursePredictDetailsRepository();
                return repository;
            }
        }

        public static IExamScheduleRepository ExamSchedule_Repository
        {
            get
            {
                IExamScheduleRepository repository = new SqlExamScheduleRepository();
                return repository;
            }
        }

        public static IExamScheduleSectionRepository ExamScheduleSection_Repository
        {
            get
            {
                IExamScheduleSectionRepository repository = new SqlExamScheduleSectionRepository();
                return repository;
            }
        }

        public static IExamScheduleSeatPlanRepository ExamScheduleSeatPlan_Repository
        {
            get
            {
                IExamScheduleSeatPlanRepository repository = new SqlExamScheduleSeatPlanRepository();
                return repository;
            }
        }

        public static IExamScheduleSeatPlanRepository ExamSeatPlan_Repository
        {
            get
            {
                IExamScheduleSeatPlanRepository repository = new SqlExamScheduleSeatPlanRepository();
                return repository;
            }
        }

        public static IExamMarkEquationColumnOrderRepository ExamMarkEquationColumnOrder_Repository
        {
            get
            {
                IExamMarkEquationColumnOrderRepository repository = new SQLExamMarkEquationColumnOrderRepository();
                return repository;
            }
        }

        #endregion

        public static ICourseRepository FlatCourseListByProgram_Repository
        {
            get
            {
                ICourseRepository repository = new SQLCourseRepository();
                return repository;
            }
        }

        public static IShareProgramInSectionRepository ShareProgramInSection_Repository
        {
            get
            {
                IShareProgramInSectionRepository repository = new SQLShareProgramInSectionRepository();
                return repository;
            }
        }

        public static IAcademicCalenderScheduleRepository AcademicCalenderSchedule_Repository
        {
            get
            {
                IAcademicCalenderScheduleRepository repository = new SQLAcademicCalenderScheduleRepository();
                return repository;
            }
        }

        public static ICourseGroupRepository CourseGroup_Repository
        {
            get
            {
                ICourseGroupRepository repository = new SQLCourseGroupRepository();
                return repository;
            }
        }

        public static ILateFineScheduleRepository LateFineSchedule_Repository
        {
            get
            {
                ILateFineScheduleRepository repository = new SQLLateFineScheduleRepository();
                return repository;
            }
        }

        public static IReportRepository AdmitCard_Repository
        {
            get
            {
                IReportRepository repository = new SQLReportRepository();
                return repository;
            }
        }

        public static IReportRepository StudentRegSummary_Repository
        {
            get
            {
                IReportRepository repository = new SQLReportRepository();
                return repository;
            }
        }

        public static ICourseRepository TreeDistribution_Repository
        {
            get
            {
                ICourseRepository repository = new SQLCourseRepository();
                return repository;
            }
        }

        public static ICourseRepository CourseRegistrationForm_Repository
        {
            get
            {
                ICourseRepository repository = new SQLCourseRepository();
                return repository;
            }
        }

        public static ICourseRepository OfferedCourseByProgram_Repository
        {
            get
            {
                ICourseRepository repository = new SQLCourseRepository();
                return repository;
            }
        }

        public static IBillHistoryRepository BillHistory_Repository
        {
            get
            {
                IBillHistoryRepository repository = new SqlBillHistoryRepository();
                return repository;
            }
        }

        public static IOpeningDueRepository OpeningDue_Repository
        {
            get
            {
                IOpeningDueRepository repository = new SQLOpeningDueRepository();
                return repository;
            }
        }

        public static IShareBatchInSectionRepository ShareBatchInSection_Repository
        {
            get
            {
                IShareBatchInSectionRepository repository = new SQLShareBatchInSectionRepository();
                return repository;
            }
        }

        public static IAddressTypeRepository AddressType_Repository
        {
            get
            {
                IAddressTypeRepository repository = new SqlAddressTypeRepository();
                return repository;
            }
        }

        public static IRegistrationStatusRepository RegistrationStatus_Repository
        {
            get
            {
                IRegistrationStatusRepository repository = new SQLRegistrationStatusRepository();
                return repository;
            }
        }

        public static ICourseRepository CourseWiseStudentList_Repository
        {
            get
            {
                ICourseRepository repository = new SQLCourseRepository();
                return repository;
            }
        }

        public static ICampusRepository Campus_Repository
        {
            get
            {
                ICampusRepository repository = new SqlCampusRepository();
                return repository;
            }
        }

        public static IBuildingRepository Building_Repository
        {
            get
            {
                IBuildingRepository repository = new SqlBuildingRepository();
                return repository;
            }
        }

        public static IExamScheduleRoomInfoRepository ExamScheduleRoomInfo_Repository
        {
            get
            {
                IExamScheduleRoomInfoRepository repository = new SqlExamScheduleRoomInfoRepository();
                return repository;
            }
        }

        public static ICollectionHistoryRepository CollectionHistory_Repository
        {
            get
            {
                ICollectionHistoryRepository repository = new SQLCollectionHistoryRepository();
                return repository;
            }
        }

        public static ISetUpDateForProgramRepository SetUpDateForProgram_Repository
        {
            get
            {
                ISetUpDateForProgramRepository repository = new SqlSetUpDateForProgramRepository();
                return repository;
            }
        }

        public static IAdmissionRegistrationCountRepository AdmissionRegistrationCount_Repository
        {
            get
            {
                IAdmissionRegistrationCountRepository repository = new SqlAdmissionRegistrationCountRepository();
                return repository;
            }
        }

        public static ITCGPAByStudentRepository TCGPAByStudent_Repository
        {
            get
            {
                ITCGPAByStudentRepository repository = new SqlTCGPAByStudentRepositoryRepository();
                return repository;
            }
        }

        public static IDailyCollection DailyCollection_Repository
        {
            get
            {
                IDailyCollection repository = new SqlDailyCollectionRepository();
                return repository;
            }
        }

        public static IRegistrationInSemesterYear RegistrationInSemesterYear_Repository
        {
            get
            {
                IRegistrationInSemesterYear repository = new SqlRegistrationInSemesterYearRepository();
                return repository;
            }
        }

        public static ILogGeneralRepository LogGeneral_Repository
        {
            get
            {
                ILogGeneralRepository repository = new SQLLogGeneralRepository();
                return repository;
            }
        }

        public static ILogLoginLogoutRepository LogLoginLogout_Repository
        {
            get
            {
                ILogLoginLogoutRepository repository = new SqlLogLoginLogoutRepository();
                return repository;
            }
        }

        public static ILogSMSRepository LogSMS_Repository
        {
            get
            {
                ILogSMSRepository repository = new SQLLogSMSRepository();
                return repository;
            }
        }

        public static IPersonPreviousExamRepository PersonPreviousExam_Repository
        {
            get
            {
                IPersonPreviousExamRepository repository = new SQLPersonPreviousExamRepository();
                return repository;
            }
        }

        public static IPreviousExamRepository PreviousExam_Repository
        {
            get
            {
                IPreviousExamRepository repository = new SQLPreviousExamRepository();
                return repository;
            }
        }

        public static IPreviousExamTypeRepository PreviousExamType_Repository
        {
            get
            {
                IPreviousExamTypeRepository repository = new SQLPreviousExamTypeRepository();
                return repository;
            }
        }

        public static IContactDetailsRepository ContactDetails_Repository
        {
            get
            {
                IContactDetailsRepository repository = new SQLContactDetailsManager();
                return repository;
            }
        }

        public static IDiagonsticRepository Diagonstic_Repository
        {
            get
            {
                IDiagonsticRepository repository = new SQLDiagonsticRepository();
                return repository;
            }
        }

        public static ICertificatesRepository Certificates_Repository
        {
            get
            {
                ICertificatesRepository repository = new SQLCertificatesRepository();
                return repository;
            }
        }

        public static ISMSBasicSetupRepository SMSBasicSetup_Repository
        {
            get
            {
                ISMSBasicSetupRepository repository = new SQLSMSBasicSetupRepository();
                return repository;
            }
        }

        public static IClassAttendanceRepository ClassAttendance_Repository
        {
            get
            {
                IClassAttendanceRepository repository = new SqlClassAttendanceRepository();
                return repository;
            }
        }

        public static ICourseEvaluationRepository CourseEvaluation_Repository
        {
            get
            {
                ICourseEvaluationRepository repository = new SqlCourseEvaluationRepository();
                return repository;
            }
        }

        public static ITeacherInformationRepository TeacherInformation_Repository
        {
            get
            {
                ITeacherInformationRepository repository = new SQLTeacherInformationRepository();
                return repository;
            }
        }

        public static IEquiCourseDetailsRepository EquiCourseDetails_Repository
        {
            get
            {
                IEquiCourseDetailsRepository repository = new SqlEquiCourseDetailsRepository();
                return repository;
            }
        }

        public static IPrerequisiteMasterV2Repository PrerequisiteMasterV2_Repository
        {
            get
            {
                IPrerequisiteMasterV2Repository repository = new SqlPrerequisiteMasterV2Repository();
                return repository;
            }
        }

        public static IPrerequisiteDetailV2Repository PrerequisiteDetailV2_Repository
        {
            get
            {
                IPrerequisiteDetailV2Repository repository = new SqlPrerequisiteDetailV2Repository();
                return repository;
            }
        }

        public static IEquiCourseMasterRepository EquiCourseMaster_Repository
        {
            get
            {
                IEquiCourseMasterRepository repository = new SqlEquiCourseMasterRepository();
                return repository;
            }
        }

        public static IExamTemplateMasterRepository ExamTemplateMaster_Repository
        {
            get
            {
                IExamTemplateMasterRepository repository = new SqlExamTemplateMasterRepository();
                return repository;
            }
        }

        public static IExamTemplateBasicItemDetailsRepository ExamTemplateBasicItemDetails_Repository
        {
            get
            {
                IExamTemplateBasicItemDetailsRepository repository = new SqlExamTemplateBasicItemDetailsRepository();
                return repository;
            }
        }

        public static IExamTypeRepository ExamType_Repository
        {
            get
            {
                IExamTypeRepository repository = new SqlExamTypeRepository();
                return repository;
            }
        }

        public static IExamMetaTypeRepository ExamMetaType_Repository
        {
            get
            {
                IExamMetaTypeRepository repository = new SqlExamMetaTypeRepository();
                return repository;
            }
        }

        public static IExamMarkDetailsRepository ExamMarkDetails_Repository
        {
            get
            {
                IExamMarkDetailsRepository repository = new SqlExamMarkDetailsRepository();
                return repository;
            }
        }

        public static IExamMarkMasterRepository ExamMarkMaster_Repository
        {
            get
            {
                IExamMarkMasterRepository repository = new SqlExamMarkMasterRepository();
                return repository;
            }
        }

        public static IExamTemplateCalculativeFormulaRepository ExamTemplateCalculativeFormula_Repository
        {
            get
            {
                IExamTemplateCalculativeFormulaRepository repository = new SqlExamTemplateCalculativeFormulaRepository();
                return repository;
            }
        }

        public static IExamTemplateCalculationTypeRepository ExamTemplateCalculationType_Repository
        {
            get
            {
                IExamTemplateCalculationTypeRepository repository = new SqlExamTemplateCalculationTypeRepository();
                return repository;
            }
        }

        public static IExamMarkPublishAcaCalSectionRelationRepository ExamMarkPublishAcaCalSectionRelation_Repository
        {
            get
            {
                IExamMarkPublishAcaCalSectionRelationRepository repository = new SqlExamMarkPublishAcaCalSectionRelationRepository();
                return repository;
            }
        }

        public static IStudentCourseHistoryReplicaRepository StudentCourseHistoryReplica_Repository
        {
            get
            {
                IStudentCourseHistoryReplicaRepository repository = new SQLStudentCourseHistoryReplicaRepository();
                return repository;
            }
        } 

        public static IDayScheduleDetailRepository DayScheduleDetail_Repository
        {
            get
            {
                IDayScheduleDetailRepository repository = new SqlDayScheduleDetailRepository();
                return repository;
            }
        }

        public static IDayScheduleMasterRepository DayScheduleMaster_Repository
        {
            get
            {
                IDayScheduleMasterRepository repository = new SqlDayScheduleMasterRepository();
                return repository;
            }
        } 

        public static IEmployeeTypeRepository EmployeeType_Repository
        {
            get
            {
                IEmployeeTypeRepository repository = new SqlEmployeeTypeRepository();
                return repository;
            }
        } 
        public static IStudentDocumentRepository StudentDocument_Repository
        {
            get
            {
                IStudentDocumentRepository repository = new SqlStudentDocumentRepository();
                return repository;
            }
        }

        public static IDayLectureRepository DayLecture_Repository
        {
            get
            {
                IDayLectureRepository repository = new SqlDayLectureRepository();
                return repository;
            }
        }

        public static IFeeGroupDetailRepository FeeGroupDetail_Repository
        {
            get
            {
                IFeeGroupDetailRepository repository = new SqlFeeGroupDetailRepository();
                return repository;
            }
        }

        public static IFeeGroupMasterRepository FeeGroupMaster_Repository
        {
            get
            {
                IFeeGroupMasterRepository repository = new SqlFeeGroupMasterRepository();
                return repository;
            }
        }

        public static IFundTypeRepository FundType_Repository
        {
            get
            {
                IFundTypeRepository repository = new SqlFundTypeRepository();
                return repository;
            }
        }

        public static IFeeTypeRepository FeeType_Repository
        {
            get
            {
                IFeeTypeRepository repository = new SqlFeeTypeRepository();
                return repository;
            }
        }

        public static IBillHistoryMasterRepository BillHistoryMaster_Repository
        {
            get
            {
                IBillHistoryMasterRepository repository = new SqlBillHistoryMasterRepository();
                return repository;
            }
        }

        public static IStudentRegistrationRepository StudentRegistration_Repository
        {
            get
            {
                IStudentRegistrationRepository repository = new SqlStudentRegistrationRepository();
                return repository;
            }
        }

        public static IStudentSessionRepository StudentSession_Repository
        {
            get
            {
                IStudentSessionRepository repository = new SqlStudentSessionRepository();
                return repository;
            }
        }

        public static IStudentEnrollRepository StudentEnroll_Repository
        {
            get
            {
                IStudentEnrollRepository repository = new SQLStudentEnrollRepository();
                return repository;
            }
        }

        public static ITabulationResultRemarksRepository TabulationResultRemarks_Repository
        {
            get
            {
                ITabulationResultRemarksRepository repository = new SqlTabulationResultRemarksRepository();
                return repository;
            }
        } 

        public static IForwardCoursesRepository ForwardCourses_Repository
        {
            get
            {
                IForwardCoursesRepository repository = new SqlForwardCoursesRepository();
                return repository;
            }
        } 

        public static IUserInstitutionRepository UserInstitution_Repository
        {
            get
            {
                IUserInstitutionRepository repository = new SqlUserInstitutionRepository();
                return repository;
            }
        }

        public static IAffiliatedInstitutionRepository AffiliatedInstitution_Repository
        {
            get
            {
                IAffiliatedInstitutionRepository repository = new SqlAffiliatedInstitutionRepository();
                return repository;
            }
        }

        public static IExamCenterRepository ExamCenter_Repository
        {
            get
            {
                IExamCenterRepository repository = new SqlExamCenterRepository();
                return repository;
            }
        }

        public static IStudentInstitutionRepository StudentInstitution_Repository
        {
            get
            {
                IStudentInstitutionRepository repository = new SqlStudentInstitutionRepository();
                return repository;
            }
        }

        public static IStudentFeedbackRepository StudentFeedback_Repository
        {
            get
            {
                IStudentFeedbackRepository repository = new SqlStudentFeedbackRepository();
                return repository;
            }
        }

        public static IUserExamCenterRepository UserExamCenter_Repository
        {
            get
            {
                IUserExamCenterRepository repository = new SqlUserExamCenterRepository();
                return repository;
            }
        }



        public static IDegreeCompletionRepository DegreeCompletion_Repository
        {
            get
            {
                IDegreeCompletionRepository repository = new SQLDegreeCompletionRepository();
                return repository;
            }
        }

        public static IStudentTranscriptInfoRepository StudentTranscriptInfo_Repository
        {
            get
            {
                IStudentTranscriptInfoRepository repository = new SqlStudentTranscriptInfoRepository();
                return repository;
            }
        }

        #region DataTable
        public static IDataTableRepository DataTable_Repository
        {
            get
            {
                IDataTableRepository repository = new SqlDataTableRepository();
                return repository;
            }
        }
        #endregion
    }
}