using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessLogic;

namespace LogicLayer.BusinessLogic
{
    public class AcademicCalenderSectionManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "AcademicCalenderSectionCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<AcademicCalenderSection> GetCacheAsList(string rawKey)
        {
            List<AcademicCalenderSection> list = (List<AcademicCalenderSection>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static AcademicCalenderSection GetCacheItem(string rawKey)
        {
            AcademicCalenderSection item = (AcademicCalenderSection)HttpRuntime.Cache[GetCacheKey(rawKey)];
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
            DataCache.Insert(GetCacheKey(rawKey), value, dependency, DateTime.Now.AddSeconds(CacheDuration), System.Web.Caching.Cache.NoSlidingExpiration);
        }



        public static void InvalidateCache()
        {
            // Remove the cache dependency
            HttpRuntime.Cache.Remove(MasterCacheKeyArray[0]);
        }

        #endregion

        public static int Insert(AcademicCalenderSection academicCalenderSection)
        {
            int id = RepositoryManager.AcademicCalenderSection_Repository.Insert(academicCalenderSection);
            InvalidateCache();
            return id;
        }

        public static bool Update(AcademicCalenderSection academicCalenderSection)
        {
            bool isExecute = RepositoryManager.AcademicCalenderSection_Repository.Update(academicCalenderSection);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.AcademicCalenderSection_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static AcademicCalenderSection GetById(int id)
        {
            AcademicCalenderSection academicCalenderSection = RepositoryManager.AcademicCalenderSection_Repository.GetById(id);

            return academicCalenderSection;
        }

        public static List<AcademicCalenderSection> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "AcademicCalenderSectionGetAll";

            List<AcademicCalenderSection> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.AcademicCalenderSection_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<AcademicCalenderSection> GetAllByAcaCalId(int id)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "AcademicCalenderSectionGetAllByAcaCalId" + id;

            List<AcademicCalenderSection> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.AcademicCalenderSection_Repository.GetAllByAcaCalId(id);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<AcademicCalenderSection> GetAllByAcaCalIdStudentRoll(int id, string studentRoll)
        {
            return RepositoryManager.AcademicCalenderSection_Repository.GetAllByAcaCalIdStudentRoll(id, studentRoll);
        }

        public static List<AcademicCalenderSection> GetAllByAcaCalIdState(int id, string state)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "AcademicCalenderSectionGetAllByAcaCalIdState" + id + state;

            List<AcademicCalenderSection> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.AcademicCalenderSection_Repository.GetAllByAcaCalIdState(id, state);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<AcademicCalenderSection> GetAllByRoomDayTime(int Room1, int Room2, int Day1, int Day2, int Time1, int Time2)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "AcademicCalenderSectionGetByRoomDayTime" + Room1 + Room2 + Day1 + Day2 + Time1 + Time2;

            List<AcademicCalenderSection> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.AcademicCalenderSection_Repository.GetAllByRoomDayTime(Room1, Room2, Day1, Day2, Time1, Time2);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static AcademicCalenderSection GetByCourseVersionSecFac(int courseId, int versionId, string section, int facultyId)
        {
            string rawKey = "AcademicCalenderSectionByCourseVersionSecFac" + courseId + versionId + section + facultyId;
            AcademicCalenderSection academicCalenderSection = GetCacheItem(rawKey);

            if (academicCalenderSection == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                academicCalenderSection = RepositoryManager.AcademicCalenderSection_Repository.GetByCourseVersionSecFac(courseId, versionId, section, facultyId);
                if (academicCalenderSection != null)
                    AddCacheItem(rawKey, academicCalenderSection);
            }

            return academicCalenderSection;
        }

        public static AcademicCalenderSection GetByAcaCalCourseVersionSection(int acaCalId, int courseId, int versionId, string sectionName)
        {
            string rawKey = "AcademicCalenderSectionByAcaCalCourseVersionSection" + acaCalId + courseId + versionId + sectionName;
            AcademicCalenderSection academicCalenderSection = GetCacheItem(rawKey);

            if (academicCalenderSection == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                academicCalenderSection = RepositoryManager.AcademicCalenderSection_Repository.GetByAcaCalCourseVersionSection(acaCalId, courseId, versionId, sectionName);
                if (academicCalenderSection != null)
                    AddCacheItem(rawKey, academicCalenderSection);
            }

            return academicCalenderSection;
        }

        public static List<AcademicCalenderSection> GetAll(int studentId, int acaCalId)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "AcademicCalenderSectionGetAllByStudentAcaCal" + studentId + acaCalId;

            List<AcademicCalenderSection> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.AcademicCalenderSection_Repository.GetAll(studentId, acaCalId);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<AcademicCalenderSection> GetAllByRoomInRegSession(int roomId)
        {
            string rawKey = "AcademicCalenderSectionGetAllByRoom" + roomId;

            List<AcademicCalenderSection> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.AcademicCalenderSection_Repository.GetAllByRoomInRegSession(roomId);
                if (list != null)
                {
                    AddCacheItem(rawKey, list);
                }
            }

            return list;
        }

        public static void CheckConflictByRoom(AcademicCalenderSection academicCalenderSection, int roomId)
        {
            int f = 0;

            List<AcademicCalenderSection> acaCalSectionListByRoom = AcademicCalenderSectionManager.GetAllByRoomInRegSession(roomId);
            #region Remove Prime Section
            if (acaCalSectionListByRoom.Count > 0)
            {
                acaCalSectionListByRoom = acaCalSectionListByRoom.Where(x => x.AcaCal_SectionID != academicCalenderSection.AcaCal_SectionID).ToList();
            }
            #endregion

            foreach (AcademicCalenderSection item in acaCalSectionListByRoom)
            {
                #region Conflict By Room
                bool isConflict = AcademicCalenderSectionManager.IsSectionConflictByRoom(
                                                                                            academicCalenderSection.RoomInfoOneID,
                                                                                            academicCalenderSection.DayOne,
                                                                                            academicCalenderSection.DayTwo,
                                                                                            academicCalenderSection.TimeSlotPlanOneID,
                                                                                            academicCalenderSection.TimeSlotPlanTwoID,

                                                                                            item.RoomInfoOneID,
                                                                                            item.RoomInfoOneID,
                                                                                            item.DayOne,
                                                                                            item.DayTwo,
                                                                                            item.TimeSlotPlanOneID,
                                                                                            item.TimeSlotPlanTwoID
                                                                                        );
                if (isConflict)
                {
                    f = 1;
                    if (academicCalenderSection.RoomConflict != null)
                    {
                        if (!academicCalenderSection.RoomConflict.Contains(item.Course.FormalCode.Trim()))
                        {
                            academicCalenderSection.RoomConflict += item.SectionName + " - " + item.Course.FormalCode;
                            AcademicCalenderSectionManager.Update(academicCalenderSection);
                        }
                    }

                    if (item.RoomConflict != null)
                    {
                        if (!item.RoomConflict.Contains(academicCalenderSection.Course.FormalCode.Trim()))
                        {
                            item.RoomConflict = academicCalenderSection.SectionName + " - " + academicCalenderSection.Course.FormalCode;
                            AcademicCalenderSectionManager.Update(item);
                        }
                    }
                }
                #endregion
            }
            if (acaCalSectionListByRoom.Count == 0 || f == 0)
            {
                academicCalenderSection.RoomConflict = string.Empty;
                AcademicCalenderSectionManager.Update(academicCalenderSection);
            }
        }

        public static void CheckConflictByFaculty(AcademicCalenderSection academicCalenderSection, int teacherId)
        {
            int f = 0;

            List<AcademicCalenderSection> acaCalSectionListByFaculty = AcademicCalenderSectionManager.GetAllByTeacherInRegSession(teacherId);
            #region Remove Prime Section
            if (acaCalSectionListByFaculty.Count > 0)
            {
                acaCalSectionListByFaculty = acaCalSectionListByFaculty.Where(x => x.AcaCal_SectionID != academicCalenderSection.AcaCal_SectionID).ToList();
            }
            #endregion

            foreach (AcademicCalenderSection item in acaCalSectionListByFaculty)
            {
                #region Conflict By Room
                bool isConflict = AcademicCalenderSectionManager.IsSectionConflictByFaculty(
                                                                                            academicCalenderSection.TeacherOneID,
                                                                                            academicCalenderSection.DayOne,
                                                                                            academicCalenderSection.DayTwo,
                                                                                            academicCalenderSection.TimeSlotPlanOneID,
                                                                                            academicCalenderSection.TimeSlotPlanTwoID,

                                                                                            item.TeacherOneID,
                                                                                            item.TeacherTwoID,
                                                                                            item.DayOne,
                                                                                            item.DayTwo,
                                                                                            item.TimeSlotPlanOneID,
                                                                                            item.TimeSlotPlanTwoID
                                                                                        );
                if (isConflict)
                {
                    f = 1;
                    if (academicCalenderSection.FacultyConflict != null)
                    {
                        if (!academicCalenderSection.FacultyConflict.Contains(item.Course.FormalCode.Trim()))
                        {
                            academicCalenderSection.FacultyConflict += item.SectionName + " - " + item.Course.FormalCode;
                            AcademicCalenderSectionManager.Update(academicCalenderSection);
                        }
                    }

                    if (item.FacultyConflict != null)
                    {
                        if (!item.FacultyConflict.Contains(academicCalenderSection.Course.FormalCode.Trim()))
                        {
                            item.FacultyConflict = academicCalenderSection.SectionName + " - " + academicCalenderSection.Course.FormalCode;
                            AcademicCalenderSectionManager.Update(item);
                        }
                    }
                }
                #endregion
            }

            if (acaCalSectionListByFaculty.Count == 0 || f == 0)
            {
                academicCalenderSection.FacultyConflict = string.Empty;
                AcademicCalenderSectionManager.Update(academicCalenderSection);
            }
        }

        private static List<AcademicCalenderSection> GetAllByTeacherInRegSession(int teacherId)
        {
            string rawKey = "AcademicCalenderSectionGetAllByteacherId" + teacherId;

            List<AcademicCalenderSection> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.AcademicCalenderSection_Repository.GetAllByTeacherInRegSession(teacherId);
                if (list != null)
                {
                    AddCacheItem(rawKey, list);
                }
            }

            return list;
        }

        public static List<AcademicCalenderSection> GetAllByAcaCalProgram(int acaCalId, int programId)
        {
            string rawKey = "AcademicCalenderSectionGetAllByAcaCalProgram" + acaCalId + programId;

            List<AcademicCalenderSection> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.AcademicCalenderSection_Repository.GetAllByAcaCalProgram(acaCalId, programId);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<AcademicCalenderSection> GetByAcaCalCourseVersion(int acaCalId, int courseId, int versionId)
        {
            string rawKey = "AcademicCalenderSectionByAcaCalCourseVersion" + acaCalId + courseId + versionId;

            List<AcademicCalenderSection> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.AcademicCalenderSection_Repository.GetByAcaCalCourseVersion(acaCalId, courseId, versionId);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<AcademicCalenderSectionWithCourse> GetAllCourseWithSectionByAcaCalAndProgramAndTeacher(int acaCalId, int programId, int teacherId)
        {
            return RepositoryManager.AcademicCalenderSection_Repository.GetAllCourseWithSectionByAcaCalAndProgramAndTeacher(acaCalId, programId, teacherId);
        }

        public static List<TeacherInfo> GetAllTeacherByAcaCalAndProgram(int acaCalId, int programId)
        {
            return RepositoryManager.AcademicCalenderSection_Repository.GetAllTeacherByAcaCalAndProgram(acaCalId, programId);
        }

        #region Conflict Algorithm
        public static bool IsSectionConflictByRoom(int r1, int Day1, int Day2, int TS1, int TS2, int r3, int r4, int Day3, int Day4, int TS3, int TS4)
        {
            if (r1 == r3)
            {
                return IsSectionConflict(Day1, TS1, Day2, TS2, Day3, TS3, Day4, TS4);
            }
            else if (r4 > 0)
            {
                if (r1 == r4)
                {
                    return IsSectionConflict(Day1, TS1, Day2, TS2, Day3, TS3, Day4, TS4);
                }
            }
            return false;
        }

        private static bool IsSectionConflictByFaculty(int t1, int Day1, int Day2, int TS1, int TS2, int t3, int t4, int Day3, int Day4, int TS3, int TS4)
        {
            if (t1 == t3)
            {
                return IsSectionConflict(Day1, TS1, Day2, TS2, Day3, TS3, Day4, TS4);
            }
            else if (t4 > 0)
            {
                if (t1 == t4)
                {
                    return IsSectionConflict(Day1, TS1, Day2, TS2, Day3, TS3, Day4, TS4);
                }
            }
            return false;
        }

        public static bool IsSectionConflict(int Day1, int TS1, int Day2, int TS2, int Day3, int TS3, int Day4, int TS4)
        {
            bool isTrue = false;

            if (Day1 == Day3)
            {
                if (TS1 == TS3)
                {
                    return true;
                }
                else
                {
                    isTrue = CheckConflictTimeSlotPlan(TS1, TS3);
                    if (isTrue)
                        return isTrue;
                }
            }
            if (Day4 > 0)
            {
                if (Day1 == Day4)
                {
                    if (TS1 == TS4)
                    {
                        return true;
                    }
                    else
                    {
                        isTrue = CheckConflictTimeSlotPlan(TS1, TS4);
                        if (isTrue)
                            return isTrue;
                    }
                }
            }
            if (Day2 > 0)
            {
                if (Day2 == Day3)
                {
                    if (TS2 == TS3)
                    {
                        return true;
                    }
                    else
                    {
                        isTrue = CheckConflictTimeSlotPlan(TS2, TS3);
                        if (isTrue)
                            return isTrue;
                    }
                }
            }
            if (Day2 > 0)
            {
                if (Day4 > 0)
                {
                    if (Day2 == Day4)
                    {
                        if (TS2 == TS4)
                        {
                            return true;
                        }
                        else
                        {
                            isTrue = CheckConflictTimeSlotPlan(TS2, TS4);
                            if (isTrue)
                                return isTrue;
                        }
                    }
                }
            }

            return isTrue;
        }

        private static bool CheckConflictTimeSlotPlan(int TS1, int TS2)
        {
            TimeSlotPlanNew timeSlotPlan1 = new TimeSlotPlanNew();
            TimeSlotPlanNew timeSlotPlan2 = new TimeSlotPlanNew();

            timeSlotPlan1 = TimeSlotPlanManager.GetById(TS1);
            timeSlotPlan2 = TimeSlotPlanManager.GetById(TS2);

            if (timeSlotPlan1 == null)
                return false;
            if (timeSlotPlan2 == null)
                return false;

            int StartTime1 = GetTimeAsInteger(timeSlotPlan1.StartHour, timeSlotPlan1.StartMin, timeSlotPlan1.StartAMPM);
            int EndTime1 = GetTimeAsInteger(timeSlotPlan1.EndHour, timeSlotPlan1.EndMin, timeSlotPlan1.EndAMPM);

            int StartTime2 = GetTimeAsInteger(timeSlotPlan2.StartHour, timeSlotPlan2.StartMin, timeSlotPlan2.StartAMPM);
            int EndTime2 = GetTimeAsInteger(timeSlotPlan2.EndHour, timeSlotPlan2.EndMin, timeSlotPlan2.EndAMPM);

            if ((StartTime1 >= StartTime2 && StartTime1 < EndTime2) ||
                (EndTime1 >= StartTime2 && EndTime1 < EndTime2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static int GetTimeAsInteger(int Hr, int Min, int AmPm)
        {
            if (AmPm == 2) // PM
            {
                if (Hr < 12)
                {
                    return (((12 + Hr) * 100) + Min);
                }
                else
                {
                    return ((Hr * 100) + Min);
                }
            }
            else //AM
            {
                return ((Hr * 100) + Min);
            }
        }

        #endregion

        public static List<AcademicCalenderSection> GetAllByAcaCalAndProgram(int programId, int sessionId)
        {
            string rawKey = "AcademicCalenderSectionGetAllByAcaCalAndProgram" + programId + "-" + sessionId;

            List<AcademicCalenderSection> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.AcademicCalenderSection_Repository.GetAllByAcaCalAndProgram(programId, sessionId);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
