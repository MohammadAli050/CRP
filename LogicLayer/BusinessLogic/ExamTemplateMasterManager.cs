using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using LogicLayer.BusinessObjects.DTO;
using System.Data;

namespace LogicLayer.BusinessLogic
{
    public class ExamTemplateMasterManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ExamTemplateMasterCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamTemplateMaster> GetCacheAsList(string rawKey)
        {
            List<ExamTemplateMaster> list = (List<ExamTemplateMaster>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamTemplateMaster GetCacheItem(string rawKey)
        {
            ExamTemplateMaster item = (ExamTemplateMaster)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamTemplateMaster examtemplatemaster)
        {
            int id = RepositoryManager.ExamTemplateMaster_Repository.Insert(examtemplatemaster);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamTemplateMaster examtemplatemaster)
        {
            bool isExecute = RepositoryManager.ExamTemplateMaster_Repository.Update(examtemplatemaster);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamTemplateMaster_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamTemplateMaster GetById(int? id)
        {
            string rawKey = "ExamTemplateMasterByID" + id;
            ExamTemplateMaster examtemplatemaster = GetCacheItem(rawKey);

            if (examtemplatemaster == null)
            {
                examtemplatemaster = RepositoryManager.ExamTemplateMaster_Repository.GetById(id);
                if (examtemplatemaster != null)
                    AddCacheItem(rawKey,examtemplatemaster);
            }

            return examtemplatemaster;
        }

        public static List<ExamTemplateMaster> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ExamTemplateMasterGetAll";

            List<ExamTemplateMaster> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamTemplateMaster_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }


        public static bool GetExamTemplateMasterByName(string examTemplateMasterName)
        {
            ExamTemplateMaster examtemplatemaster = RepositoryManager.ExamTemplateMaster_Repository.GetExamTemplateMasterByName(examTemplateMasterName);
            if (examtemplatemaster != null) 
            {
                return false;
            }
            else { return true; }
        }

        public static List<ExamTemplateBasicCalculativeItemDTO> ExamTemplateItemGetByAcaCalSectionId(int acaCalSectionId)
        {
            List<ExamTemplateBasicCalculativeItemDTO> list = RepositoryManager.ExamTemplateMaster_Repository.ExamTemplateItemGetByAcaCalSectionId(acaCalSectionId);
            return list;
        }

        public static List<ExamMarkColumnWiseDTO> GetStudentExamMarkColumnWise(int courseId, int versionId, int acaCalId, int acaCalSectionId)
        {
            List<ExamMarkColumnWiseDTO> list = RepositoryManager.ExamTemplateMaster_Repository.GetStudentExamMarkColumnWise(courseId, versionId, acaCalId, acaCalSectionId);
            return list;
        }

        public static List<ExamMarkColumnWiseDTO> GetStudentExamMarkColumnWiseByStudentId(int courseId, int versionId, int acaCalId, int acaCalSectionId, int courseHistoryId) 
        {
            List<ExamMarkColumnWiseDTO> list = RepositoryManager.ExamTemplateMaster_Repository.GetStudentExamMarkColumnWiseByStudentId(courseId, versionId, acaCalId, acaCalSectionId, courseHistoryId);
            return list;
        }

        public static DataTable GetExamResultDataTable(int courseId, int versionId, int acaCalId, int acaCaSectionId)
        {
            DataTable table = new DataTable();
            if (acaCaSectionId > 0)
            {
                AcademicCalenderSection acaCalSectionObj = AcademicCalenderSectionManager.GetById(acaCaSectionId);
                if (acaCalSectionObj != null)
                {
                    List<ExamTemplateBasicCalculativeItemDTO> examTemplateBasicCalculativeItemList = ExamTemplateMasterManager.ExamTemplateItemGetByAcaCalSectionId(acaCalSectionObj.AcaCal_SectionID).ToList();

                    List<ExamMarkColumnWiseDTO> examMarkColumnWiseList = ExamTemplateMasterManager.GetStudentExamMarkColumnWise(courseId, versionId, acaCalId, acaCaSectionId);

                    List<int> studentIdList = examMarkColumnWiseList.Select(d => d.StudentCourseHistoryId).Distinct().ToList();

                    List<int> examSequenceList = examTemplateBasicCalculativeItemList.Select(d => d.ColumnSequence).Distinct().ToList();

                    ExamMetaType examMetaTypeTypeObj = ExamMetaTypeManager.GetAll().Where(d => d.ExamMetaTypeName == "Final Exam").FirstOrDefault();

                    
                    table.Columns.Add("Student Name", typeof(string));
                    table.Columns.Add("Roll", typeof(string));
                    decimal final = 0;
                    for (int j = 0; j < examTemplateBasicCalculativeItemList.Count; j++)
                    {
                        if (examTemplateBasicCalculativeItemList[j].ExamTemplateBasicItemName == "Final")
                        {
                            ExamTemplateBasicItemDetails examTemplateDetails = ExamTemplateBasicItemDetailsManager.GetById(examTemplateBasicCalculativeItemList[j].ExamTemplateBasicItemId);
                            table.Columns.Add(examTemplateBasicCalculativeItemList[j].ExamTemplateBasicItemName + " " + examTemplateDetails.ExamTemplateBasicItemMark.ToString("#") + "%", typeof(string));
                            final = examTemplateDetails.ExamTemplateBasicItemMark;
                        }
                        else
                            table.Columns.Add(examTemplateBasicCalculativeItemList[j].ExamTemplateBasicItemName, typeof(string));
                    }
                    decimal incourseMark = 100 - final;
                    if (final == 0)
                        table.Columns.Add("Total 100%", typeof(string));
                    else
                        table.Columns.Add("Total (" + final.ToString("#") + "+" + incourseMark.ToString("#") + ")%", typeof(string));
                    table.Columns.Add("Grade Point", typeof(string));
                    table.Columns.Add("Letter Grade", typeof(string));
                    table.Columns.Add("Student Roll", typeof(string));
                    table.Columns.Add("Incourse " + incourseMark.ToString("#") + "%", typeof(string));
                    table.Columns.Add("Final Mark", typeof(string));

                    for (int i = 0; i < studentIdList.Count; i++)
                    {
                        int studentCourseHistoryId = Convert.ToInt32(studentIdList[i]);
                        int studentId = examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId).Select(d => d.StudentId).FirstOrDefault();
                        LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetById(studentId);
                        List<GradeDetails> gradeDetailList = new List<GradeDetails>();
                        DataRow newRow;
                        if (studentObj != null)
                        {
                            gradeDetailList = GradeDetailsManager.GetByGradeMasterId(Convert.ToInt32(studentObj.GradeMasterId));
                        }

                        object[] rowArray = new object[examTemplateBasicCalculativeItemList.Count + 8];
                        int newRowCounter = 0;
                        rowArray[0] = studentObj.Name;
                        rowArray[1] = studentObj.Roll;
                        newRowCounter = 1;
                        decimal totalMark = 0;
                        decimal basicExamMark = 0;
                        decimal finalWithoutConvertMark = 0;

                        for (int j = 0; j < examSequenceList.Count; j++)
                        {
                            decimal examMarks = 0;
                            int sequenceNo = Convert.ToInt32(examSequenceList[j]);
                            ExamTemplateBasicCalculativeItemDTO examItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ColumnSequence == sequenceNo).FirstOrDefault();
                            if (examItemObj != null)
                            {
                                if (examItemObj.ExamTemplateMasterTypeId == (int)CommonUtility.CommonEnum.ExamTemplateType.Basic) //(int)CommonUtility.CommonEnum.ExamTemplateItemColumnType.Basic)
                                {
                                    decimal studentExamMark = Convert.ToDecimal(examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamTemplateBasicItemId == examItemObj.ExamTemplateBasicItemId && d.ExamTemplateBasicItemId > 0 && d.ColumnSequence == sequenceNo).Select(d => d.ConvertedMark).FirstOrDefault());
                                    examMarks = studentExamMark;
                                    examMarks = Math.Round(examMarks, 2);

                                    ExamTemplateBasicCalculativeItemDTO examCalculativeItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType != 0).FirstOrDefault();
                                    if (examCalculativeItemObj == null)
                                    {
                                        totalMark = totalMark + examMarks;
                                        if (examItemObj.ExamMetaTypeId != 8 || !examItemObj.ExamTemplateBasicItemName.Contains("Final"))
                                        {
                                            basicExamMark = basicExamMark + examMarks;
                                        }
                                        else
                                        {
                                            finalWithoutConvertMark = Convert.ToDecimal(examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamTemplateBasicItemId == examItemObj.ExamTemplateBasicItemId && d.ExamTemplateBasicItemId > 0 && d.ColumnSequence == sequenceNo).Select(d => d.Marks).FirstOrDefault());
                                        }
                                    }
                                }
                                else if (examItemObj.ExamTemplateMasterTypeId == (int)CommonUtility.CommonEnum.ExamTemplateType.Calculative)
                                {
                                    if (examItemObj.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.Average)
                                    {
                                        decimal marks = Convert.ToDecimal(examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamMetaTypeId == examItemObj.ExamMetaTypeId).Sum(d => d.ConvertedMark));
                                        int itemCount = Convert.ToInt32(examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType == 0).ToList().Count);
                                        examMarks = marks / itemCount;
                                        examMarks = Math.Round(examMarks, 2);

                                        ExamTemplateBasicCalculativeItemDTO examCalculativeItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType == 1).FirstOrDefault();
                                        if (examCalculativeItemObj != null)
                                        {
                                            totalMark = totalMark + examMarks;
                                        }
                                    }
                                    else if (examItemObj.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestOne)
                                    {
                                        decimal marks = Convert.ToDecimal(examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamMetaTypeId == examItemObj.ExamMetaTypeId).Max(d => d.ConvertedMark));

                                        examMarks = marks;
                                        examMarks = Math.Round(examMarks, 2);

                                        ExamTemplateBasicCalculativeItemDTO examCalculativeItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestOne).FirstOrDefault();
                                        if (examCalculativeItemObj != null)
                                        {
                                            totalMark = totalMark + examMarks;
                                        }
                                    }
                                    else if (examItemObj.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestTwo)
                                    {
                                        decimal[] markArray = new decimal[] { };
                                        markArray = examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamMetaTypeId == examItemObj.ExamMetaTypeId).Select(d => d.ConvertedMark).ToArray();

                                        var maxArrayObj = markArray;
                                        var maxNumber = maxArrayObj.Max(z => z);
                                        var secondMax = maxArrayObj.OrderByDescending(z => z).Skip(1).First();

                                        examMarks = (maxNumber + secondMax) / 2;
                                        examMarks = Math.Round(examMarks, 2);

                                        ExamTemplateBasicCalculativeItemDTO examCalculativeItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestTwo).FirstOrDefault();
                                        if (examCalculativeItemObj != null)
                                        {
                                            totalMark = totalMark + examMarks;
                                        }
                                    }
                                    else if (examItemObj.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestThree)
                                    {
                                        decimal[] markArray = new decimal[] { };
                                        markArray = examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamMetaTypeId == examItemObj.ExamMetaTypeId).Select(d => d.ConvertedMark).ToArray();

                                        var maxArrayObj = markArray;
                                        var maxNumber = maxArrayObj.Max(z => z);
                                        var secondMax = maxArrayObj.OrderByDescending(z => z).Skip(1).First();
                                        var thirdMax = maxArrayObj.OrderByDescending(z => z).Skip(2).First();

                                        examMarks = (maxNumber + secondMax + thirdMax) / 3;
                                        examMarks = Math.Round(examMarks, 2);

                                        ExamTemplateBasicCalculativeItemDTO examCalculativeItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestThree).FirstOrDefault();
                                        if (examCalculativeItemObj != null)
                                        {
                                            totalMark = totalMark + examMarks;
                                        }
                                    }
                                    else if (examItemObj.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.Sum)
                                    {
                                        decimal mark = examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamMetaTypeId == examItemObj.ExamMetaTypeId).Sum(d => d.ConvertedMark);

                                        examMarks = mark;
                                        examMarks = Math.Round(examMarks, 2);

                                        ExamTemplateBasicCalculativeItemDTO examCalculativeItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestTwo).FirstOrDefault();
                                        if (examCalculativeItemObj != null)
                                        {
                                            totalMark = totalMark + examMarks;
                                        }
                                    }
                                }
                            }
                            rowArray[newRowCounter + 1] = examMarks;
                            newRowCounter = newRowCounter + 1;
                        }
                        totalMark = Math.Ceiling(totalMark);
                        rowArray[newRowCounter + 1] = totalMark;
                        newRowCounter = newRowCounter + 1;

                        decimal gradePoint = 0;
                        string gradeLetter = "Grading System Not Assigned";

                        if (gradeDetailList != null && gradeDetailList.Count > 0)
                        {
                            gradePoint = gradeDetailList.Where(d => d.MinMarks <= totalMark && d.MaxMarks >= totalMark).FirstOrDefault().GradePoint;
                            gradeLetter = gradeDetailList.Where(d => d.MinMarks <= totalMark && d.MaxMarks >= totalMark).FirstOrDefault().Grade;
                            //gradeId = gradeDetailList.Where(d => d.MinMarks <= totalMark && d.MaxMarks >= totalMark).FirstOrDefault().GradeId;
                        }
                        if (examMetaTypeTypeObj != null)
                        {
                            ExamMarkColumnWiseDTO examMarkColumnWiseObj = examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamMetaTypeId == examMetaTypeTypeObj.ExamMetaTypeId).FirstOrDefault();
                            if (examMarkColumnWiseObj!= null)
                            {
                                if (examMarkColumnWiseObj.ExamMarkTypeId == 2)
                                {
                                    gradePoint = Convert.ToDecimal(0.00);
                                    gradeLetter = "Absent";
                                }
                            }
                        }

                        //if(){}

                        rowArray[newRowCounter + 1] = gradePoint;
                        newRowCounter = newRowCounter + 1;
                        rowArray[newRowCounter + 1] = gradeLetter;
                        newRowCounter = newRowCounter + 1;
                        rowArray[newRowCounter + 1] = studentObj.Roll;
                        newRowCounter = newRowCounter + 1;
                        rowArray[newRowCounter + 1] = basicExamMark;
                        newRowCounter = newRowCounter + 1;
                        rowArray[newRowCounter + 1] = finalWithoutConvertMark;
                        newRowCounter = newRowCounter + 1;


                        newRow = table.NewRow();
                        newRow.ItemArray = rowArray;
                        table.Rows.Add(newRow);
                    }
                }
            }
            return table;
        }

        public static List<ExamResultDTO> GetResultFromTable(DataTable dt)
        {
            List<ExamResultDTO> examResultList = new List<ExamResultDTO>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 2; j < dt.Columns.Count; j++)
                {
                    ExamResultDTO examResultObj = new ExamResultDTO();
                    examResultObj.StudentName = dt.Rows[i].ItemArray[0].ToString();
                    examResultObj.Roll = dt.Rows[i].ItemArray[1].ToString();
                    examResultObj.ExamName = dt.Columns[j].ColumnName.ToString();
                    examResultObj.ColumnSequence = Convert.ToInt32(j - 1);
                    examResultObj.MarksOrGrade = dt.Rows[i].ItemArray[j].ToString();
                    examResultList.Add(examResultObj);
                }
            }
            return examResultList;
        }

        public static List<ExamResultDTO> GetExamResultDTO(int courseId, int versionId, int acaCalId, int acaCaSectionId)
        {
            DataTable dt = GetExamResultDataTable(courseId, versionId, acaCalId, acaCaSectionId);
            List<ExamResultDTO> examResultList = new List<ExamResultDTO>();
            examResultList = GetResultFromTable(dt);
            return examResultList;
        }
    }
}

