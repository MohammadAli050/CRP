using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using LogicLayer.BusinessObjects.DTO;

namespace LogicLayer.BusinessLogic
{
    public class BillViewManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "BillViewCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<BillView> GetCacheAsList(string rawKey)
        {
            List<BillView> list = (List<BillView>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static BillView GetCacheItem(string rawKey)
        {
            BillView item = (BillView)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return item;
        }

        public static void AddCacheItem(string rawKey, object value)
        {
            System.Web.Caching.Cache DataCache = HttpRuntime.Cache;

            // Make sure MasterCacheKeyArray[0] is in the cache - if not, add it
            if (DataCache[MasterCacheKeyArray[0]] == null)
                DataCache[MasterCacheKeyArray[0]] = DateTime.Now;

            // Add a CacheDependency
            System.Web.Caching.CacheDependency dependency = new System.Web.Caching.CacheDependency(null, MasterCacheKeyArray);
            DataCache.Insert(GetCacheKey(rawKey), value, dependency, DateTime.Now.AddMinutes(CacheDuration), System.Web.Caching.Cache.NoSlidingExpiration);
        }



        public static void InvalidateCache()
        {
            // Remove the cache dependency
            HttpRuntime.Cache.Remove(MasterCacheKeyArray[0]);
        }

        #endregion


        public static int Insert(BillView billview)
        {
            int id = RepositoryManager.BillView_Repository.Insert(billview);
            InvalidateCache();
            return id;
        }

        public static bool Update(BillView billview)
        {
            bool isExecute = RepositoryManager.BillView_Repository.Update(billview);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.BillView_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        private static bool Delete(int studentID, int sessionId, int courseID, int versionID)
        {
            bool isExecute = RepositoryManager.BillView_Repository.Delete(studentID,  sessionId,  courseID,  versionID);
            InvalidateCache();
            return isExecute;
        }

        public static BillView GetById(int id)
        {
            string rawKey = "BillViewByID" + id;
            BillView billview = GetCacheItem(rawKey);

            if (billview == null)
            {
                billview = RepositoryManager.BillView_Repository.GetById(id);
                if (billview != null)
                    AddCacheItem(rawKey, billview);
            }

            return billview;
        }

        public static List<BillView> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "BillViewGetAll";

            List<BillView> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.BillView_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static BillView GetBy(int studentId, int accountsID, int sessionId)
        {
            BillView billview = RepositoryManager.BillView_Repository.GetBy(studentId, accountsID, sessionId);
            return billview;
        }

        public static List<BillView> GetBy(int studentId, int sessionId)
        {
            List<BillView> billviewList = RepositoryManager.BillView_Repository.GetBy(studentId, sessionId);
            return billviewList;
        }

        public static List<BillView> GetBy(int studentId)
        {
            List<BillView> billviewList = RepositoryManager.BillView_Repository.GetBy(studentId);
            return billviewList;
        }

        public static BillView GetBy(int studentId, int accountsID, int sessionId, int courseId, int versionId)
        {
            BillView billview = RepositoryManager.BillView_Repository.GetBy(studentId, accountsID, sessionId, courseId, versionId);
            return billview;
        }

        public static string GenerateBill(Student student, int sessionId)
        {
            List<StudentCourseHistory> studentCourseHistoryList = StudentCourseHistoryManager.GetAllByStudentId(student.StudentID);
            List<StudentCourseHistory> BillableCourseList = studentCourseHistoryList.Where(ch => ch.AcaCalID == sessionId && (ch.CourseStatusID == 6 || ch.CourseStatusID == 7)).ToList();
            List<GradeWiseRetakeDiscount> gradeWiseRetakeDiscounts = GradeWiseRetakeDiscountManager.GetAllBy(student.ProgramID, student.BatchId);
            decimal perCrAmount = FeeSetupManager.GetByTypeDefinationSessionProgram(12, student.BatchId, student.ProgramID).Amount;
            List<TypeDefinition> discountList = TypeDefinitionManager.GetAll("Discount");

            decimal[] totalDiscount = new decimal[discountList.Count];
            decimal totalRetakeDiscount = 0;
            string waiverAll = string.Empty;

            StudentDiscountMaster stdDiscountMaster = StudentDiscountMasterManager.GetByStudentID(student.StudentID);
            List<StudentDiscountCurrentDetails> stdDiscountCurrentList = null;
            if (stdDiscountMaster != null)
            {
                stdDiscountCurrentList = StudentDiscountCurrentDetailsManager.GetByStudentDiscountAndAcaCalSession(stdDiscountMaster.StudentDiscountId, sessionId);
            }

            List<Voucher> voucherList = new List<Voucher>();

            CalculateFreshBill(BillableCourseList, student, perCrAmount, sessionId, voucherList);
            totalRetakeDiscount = CalculateRetakeDiscount(studentCourseHistoryList, BillableCourseList, gradeWiseRetakeDiscounts, student, perCrAmount, sessionId, voucherList);
            waiverAll = CalculateAllWaiver(studentCourseHistoryList, BillableCourseList, gradeWiseRetakeDiscounts, stdDiscountCurrentList, discountList, student, perCrAmount, sessionId, voucherList);

            int count = VoucherManager.Insert(voucherList);

            return "Retake Discount : " + totalRetakeDiscount.ToString("0.00") + waiverAll;
        }

        private static string CalculateAllWaiver(List<StudentCourseHistory> studentCourseHistoryList,
                                                    List<StudentCourseHistory> BillableCourseList,
                                                    List<GradeWiseRetakeDiscount> gradeWiseRetakeDiscounts,
                                                    List<StudentDiscountCurrentDetails> stdDiscountCurrentList,
                                                    List<TypeDefinition> discountList,
                                                    Student student, decimal perCrAmount, int sessionId,
                                                    List<Voucher> voucherList)
        {
            decimal[] totalDiscount = new decimal[discountList.Count];

            foreach (StudentCourseHistory item in BillableCourseList)
            {
                Course course = CourseManager.GetByCourseIdVersionId(item.CourseID, item.VersionID);
                decimal waiver = 0;
                decimal courseAmount = perCrAmount * item.CourseCredit;
                int tdIndex = 0;
                decimal tmpAmount = courseAmount;
                decimal retakeDiscount = 0;


                if (isCourseWithdraw(course, student, studentCourseHistoryList))
                {
                    continue;
                }

                if (isCourseRetakeAndBillable(course, student, studentCourseHistoryList))
                {
                    if (Convert.ToBoolean(student.IsDiploma))
                    {
                        retakeDiscount = CalculateRetakeDiscountPerCourse(studentCourseHistoryList, gradeWiseRetakeDiscounts, student, course, perCrAmount, sessionId);
                        tmpAmount = tmpAmount - retakeDiscount;
                    }
                    else
                    {
                        continue;
                    }
                }


                foreach (TypeDefinition td in discountList)
                {
                    if (stdDiscountCurrentList != null)
                    {
                        // stdDiscountCurrentList = stdDiscountCurrentList.d

                        StudentDiscountCurrentDetails studentDiscountCurrent = stdDiscountCurrentList.Where(dc => dc.TypeDefinitionId == td.TypeDefinitionID).SingleOrDefault();
                        if (studentDiscountCurrent != null)
                        {
                            waiver = studentDiscountCurrent.TypePercentage;
                        }
                    }

                    if (course.HasMultipleACUSpan != true && waiver != 0 && !Convert.ToBoolean(student.IsDiploma))
                    {
                        tmpAmount = ((waiver / 100) * tmpAmount);
                        totalDiscount[tdIndex] += tmpAmount;
                        waiver = 0;
                    }
                    else if (waiver != 0 && Convert.ToBoolean(student.IsDiploma))
                    {
                        tmpAmount = ((waiver / 100) * tmpAmount);
                        totalDiscount[tdIndex] += tmpAmount;
                        waiver = 0;
                    }

                    tdIndex++;
                }
            }


            string msg = "";
            int index = 0;
            foreach (TypeDefinition td in discountList)
            {
                if (totalDiscount[index] > 0)
                {
                    msg += "; " + td.Definition + " : " + totalDiscount[index].ToString("0.00");

                    #region Save Retake Discount
                    bool boo = StudentDiscountAndScholarshipPerSessionManager.Delete(student.StudentID, sessionId, td.TypeDefinitionID); 

                    StudentDiscountAndScholarshipPerSession sdsPerSession = new StudentDiscountAndScholarshipPerSession();
                    sdsPerSession.StudentId = student.StudentID;
                    sdsPerSession.AcaCalSession = sessionId;
                    sdsPerSession.Discount = totalDiscount[index];
                    sdsPerSession.TypeDefinitionId = td.TypeDefinitionID;
                    sdsPerSession.ModifiedBy = -1;
                    sdsPerSession.ModifiedDate = DateTime.Now;
                    sdsPerSession.CreatedBy = -1;
                    sdsPerSession.CreatedDate = DateTime.Now;

                    int id = StudentDiscountAndScholarshipPerSessionManager.Insert(sdsPerSession);
                    #endregion


                    Voucher voucher = new Voucher();
                    voucher.AcaCalID = sessionId;
                    voucher.AccountHeadsID = Convert.ToInt32(student.AccountHeadsID);
                    voucher.AccountTypeID = 1;
                    voucher.Amount = totalDiscount[index];
                    voucher.Prefix = "CL";
                    voucher.Remarks = "System Entry";
                    voucher.CreatedBy = -1;
                    voucher.CreatedDate = DateTime.Now;

                    voucherList.Add(voucher);
                }
                index++;
            }

            return msg;
        }


        private static decimal CalculateRetakeDiscount(List<StudentCourseHistory> studentCourseHistoryList,
                                                        List<StudentCourseHistory> BillableCourseList,
                                                        List<GradeWiseRetakeDiscount> gradeWiseRetakeDiscounts,
                                                        Student student, decimal perCrAmount, int sessionId,
                                                        List<Voucher> voucherList)
        {
            decimal totalRetakeDiscount = 0;

            foreach (StudentCourseHistory item in BillableCourseList)
            {
                Course course = CourseManager.GetByCourseIdVersionId(item.CourseID, item.VersionID);
                decimal retakeDiscount = 0;

                //bool YesNo = false;                               
                //YesNo = isCourseDiscountable(course, student);
                //YesNo = isCourseSectionDiscountable(course, student);

                if (isCourseWithdraw(course, student, studentCourseHistoryList))
                {
                    continue;
                }

                if (isCourseRetakeAndBillable(course, student, studentCourseHistoryList))
                {
                    List<StudentCourseHistory> RetakeCourseCount = studentCourseHistoryList.Where(ch => ch.CourseID == item.CourseID &&
                                                                                                  ch.VersionID == item.VersionID &&
                                                                                                  ch.GradeId != 0 && ch.GradeId != 11)
                                                                                                  .OrderBy(s => s.Session.Code).ToList();


                    if (RetakeCourseCount.Count > 0)
                    {
                        if (gradeWiseRetakeDiscounts != null)
                        {
                            GradeWiseRetakeDiscount gwrd = gradeWiseRetakeDiscounts.Where(grd => grd.GradeId == RetakeCourseCount[RetakeCourseCount.Count - 1].GradeId).SingleOrDefault();
                            if (gwrd != null)
                            {
                                decimal retakeDiscountPercent = gwrd.RetakeDiscount;
                                retakeDiscount = (retakeDiscountPercent / 100) * (perCrAmount * item.CourseCredit);
                                totalRetakeDiscount += retakeDiscount;

                                continue;
                            }
                        }
                    }
                }
            }

            #region Save Retake Discount
           bool boo = StudentDiscountAndScholarshipPerSessionManager.Delete(student.StudentID, sessionId,28); 

            StudentDiscountAndScholarshipPerSession sdsPerSession = new StudentDiscountAndScholarshipPerSession();
            sdsPerSession.StudentId = student.StudentID;
            sdsPerSession.AcaCalSession = sessionId;
            sdsPerSession.Discount = totalRetakeDiscount;
            sdsPerSession.TypeDefinitionId = 28;//hard code
            sdsPerSession.ModifiedBy = -1;
            sdsPerSession.ModifiedDate = DateTime.Now;
            sdsPerSession.CreatedBy = -1;
            sdsPerSession.CreatedDate = DateTime.Now;

            int id = StudentDiscountAndScholarshipPerSessionManager.Insert(sdsPerSession); 
            #endregion


            #region Return Voucher
            Voucher voucher = new Voucher();
            voucher.AcaCalID = sessionId;
            voucher.AccountHeadsID = Convert.ToInt32(student.AccountHeadsID);
            voucher.AccountTypeID = 1;
            voucher.Amount = totalRetakeDiscount;
            voucher.Prefix = "CL";
            voucher.Remarks = "System Entry";
            voucher.CreatedBy = -1;
            voucher.CreatedDate = DateTime.Now;

            voucherList.Add(voucher);
            #endregion

            return totalRetakeDiscount;
        }


        private static decimal CalculateRetakeDiscountPerCourse(List<StudentCourseHistory> studentCourseHistoryList,
                                                               List<GradeWiseRetakeDiscount> gradeWiseRetakeDiscounts,
                                                               Student student,
                                                               Course course, decimal perCrAmount, int sessionId)
        {
            decimal retakeDiscount = 0;

            if (isCourseRetakeAndBillable(course, student, studentCourseHistoryList))
            {
                List<StudentCourseHistory> RetakeCourseCount = studentCourseHistoryList.Where(ch => ch.CourseID == course.CourseID &&
                                                                                              ch.VersionID == course.VersionID &&
                                                                                              ch.GradeId != 0 && ch.GradeId != 11)
                                                                                              .OrderBy(s => s.Session.Code).ToList();

                if (gradeWiseRetakeDiscounts != null)
                {
                    GradeWiseRetakeDiscount gwrd = gradeWiseRetakeDiscounts.Where(grd => grd.GradeId == RetakeCourseCount[RetakeCourseCount.Count - 1].GradeId).SingleOrDefault();
                    if (gwrd != null)
                    {
                        decimal retakeDiscountPercent = gwrd.RetakeDiscount;
                        retakeDiscount = (retakeDiscountPercent / 100) * (perCrAmount * course.Credits);
                    }
                }
            }           

            return retakeDiscount;
        }

        private static bool isCourseSectionDiscountable(Course course, Student student)
        {
            throw new NotImplementedException();
        }

        private static bool isCourseDiscountable(Course course, Student student)
        {
            throw new NotImplementedException();
        }

        private static bool isCourseRetakeAndBillable(Course course, Student student, List<StudentCourseHistory> studentCourseHistoryList)
        {
            List<StudentCourseHistory> RetakeCourseCount = studentCourseHistoryList.Where(ch => ch.CourseID == course.CourseID &&
                                                                                                   ch.VersionID == course.VersionID &&
                                                                                                   ch.GradeId != 0).ToList();
            if (RetakeCourseCount.Count > 1)
            {
                RetakeCourseCount = RetakeCourseCount.OrderBy(r => r.Session.Code).ToList();

                int firstIndex = RetakeCourseCount.FindIndex(r => !r.GradeId.Equals(11));//-----------

                if (RetakeCourseCount.Count - 1 > firstIndex)
                    return false;
                else
                    return true;

            }
            else if (RetakeCourseCount.Count == 1)
            {
                return true;
            }
            else
                return false;
        }

        private static bool isCourseWithdraw(Course course, Student student, List<StudentCourseHistory> studentCourseHistoryList)
        {
            if (studentCourseHistoryList.Where(ch => ch.CourseID == course.CourseID &&
                                                     ch.VersionID == course.VersionID &&
                                                     ch.CourseStatusID == 5).ToList().Count > 0)
            {
                return true;
            }
            else return false;

        }

        private static void CalculateFreshBill( List<StudentCourseHistory> BillableCourseList,
                                                Student student, decimal perCrAmount, int sessionId,
                                                List<Voucher> voucherList)
        {
            decimal totalFee = 0;

            foreach (StudentCourseHistory item in BillableCourseList)
            {
                BillView billView = null;
                //billView = BillViewManager.GetBy(studentID, 8, sessionId, item.CourseID, item.VersionID);
                bool result = BillViewManager.Delete(student.StudentID, sessionId, item.CourseID, item.VersionID);

                if (billView == null)
                {
                    billView = new BillView();
                    billView.AccountsID = 8;
                    billView.CourseId = item.CourseID;
                    billView.VersionId = item.VersionID;
                    billView.Amount = perCrAmount * item.CourseCredit;
                    billView.AmountByCollectiveDiscount = 0;
                    billView.AmountByIterativeDiscount = 0;
                    billView.Purpose = "Tution Fee";
                    billView.StudentId = student.StudentID;
                    billView.TrimesterId = sessionId;
                    BillViewManager.Insert(billView);

                    totalFee +=billView.Amount;
                }
            }

            #region Save Tution Fee In StudentDiscountAndScholarshipPerSession table
            bool boo = StudentDiscountAndScholarshipPerSessionManager.Delete(student.StudentID, sessionId, 12);

            StudentDiscountAndScholarshipPerSession sdsPerSession = new StudentDiscountAndScholarshipPerSession();
            sdsPerSession.StudentId = student.StudentID;
            sdsPerSession.AcaCalSession = sessionId;
            sdsPerSession.Discount = totalFee;
            sdsPerSession.TypeDefinitionId = 12;//hard code
            sdsPerSession.ModifiedBy = -1;
            sdsPerSession.ModifiedDate = DateTime.Now;
            sdsPerSession.CreatedBy = -1;
            sdsPerSession.CreatedDate = DateTime.Now;

            int id = StudentDiscountAndScholarshipPerSessionManager.Insert(sdsPerSession);
            #endregion


            #region Return Voucher
            Voucher voucher = new Voucher();
            voucher.AcaCalID = sessionId;
            voucher.AccountHeadsID = Convert.ToInt32(student.AccountHeadsID);
            voucher.AccountTypeID = 1;
            voucher.Amount = totalFee;
            voucher.Prefix = "BL";
            voucher.Remarks = "System Entry";
            voucher.CreatedBy = -1;
            voucher.CreatedDate = DateTime.Now;

            voucherList.Add(voucher);
            #endregion
        }
        
    }
}

