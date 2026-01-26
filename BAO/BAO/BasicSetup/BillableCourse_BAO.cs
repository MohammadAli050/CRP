using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BussinessObject
{
    public class BillableCourse_BAO
    {
        public static int Save(List<BillableCourseEntity> bles)
        {
            int counter = 0;
            if (Gets(bles[0].AcaCalID.ToString(), bles[0].ProgramID.ToString()) != null)
            {
                return -1;
            }
            if (bles != null)
            {
                counter = BillableCourse_DAO.Save(bles);
            }

            return counter;
        }
        public static int Update(List<BillableCourseEntity> bles)
        {
            int counter = 0;

            if (bles != null)
            {
                counter = BillableCourse_DAO.Update(bles);
            }

            return counter;
        }
        public static List<BillableCourseEntity> Gets(string acaID, string progID)
        {
            List<BillableCourseEntity> dcses = BillableCourse_DAO.GetDiscounts(Int32.Parse(acaID), Int32.Parse(progID));

            return dcses;

        }

        public static List<RbProgramEntity> GetAllPrograms()
        {
            return rbProgram_DAO.GetPrograms();
        }

        public static int Save(BillableCourseEntity billableCourse)
        {
            int counter = 0;
            try
            {
                if (Variables.SaveMode == SaveMode.Add)
                {
                    counter = BillableCourse_DAO.Save(billableCourse);
                }
                else
                {
                    counter = BillableCourse_DAO.Update(billableCourse);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return counter;
        }

        public static List<BillableCourseEntity> GetAllBillableCourses(string billableCourseCode)
        {
            try
            {
                List<BillableCourseEntity> entities = new List<BillableCourseEntity>();
                if (String.IsNullOrEmpty(billableCourseCode))
                {
                    entities = BillableCourse_DAO.GetAllBillableCourseInfo(); 
                }
                else
                {
                    entities = BillableCourse_DAO.GetAllCoursesByCourseCode(billableCourseCode);
                }
                
                return entities;
            }
            catch (Exception exception)
            {
                
                throw exception;
            }
        }

        public static List<BillableCourseEntity> GetAllBillableCourses()
        {
            try
            {
                List<BillableCourseEntity> entities = new List<BillableCourseEntity>();
                entities = BillableCourse_DAO.GetAllBillableCourseInfo();
                return entities;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static List<ViewNotSetBillableCoursesEntity> GetAllNotSetBillableCourses()
        {
            try
            {
                List<ViewNotSetBillableCoursesEntity> billableNotSetCourses = new List<ViewNotSetBillableCoursesEntity>();
                billableNotSetCourses = ViewNotSetBillableCourses_DAO.GetAllNotSetBillableCources();
                return billableNotSetCourses;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static BillableCourseEntity GetCourseByCourseIdVersionID(int courseId, int versionId)
        {
            try
            {
                BillableCourseEntity billableCourse = new BillableCourseEntity();
                billableCourse = BillableCourse_DAO.GetCourseByCourseIdVersionId(courseId, versionId);
                return billableCourse;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
