using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BussinessObject
{
    public class Student_CalCourseProgNode_BAO
    {
        public static int Save(List<StudentEntity> stds)
        {
            int counter = 0;

            try
            {
                counter = Student_CalCourseProgNode_DAO.Save(stds);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return counter;
        }

        public static List<Student_CalCourseProgNodeEntity> LoadByStuID(int stuId)
        {
            return Student_CalCourseProgNode_DAO.GetWorkSheetByStuId(stuId);
        }

        public static int UpdateRow(Student_CalCourseProgNodeEntity student_CalCourseProgNodeEntity)
        {
            return Student_CalCourseProgNode_DAO.UpdateRow(student_CalCourseProgNodeEntity);
        }

        public static int UpdateMultySpanData(List<Student_CalCourseProgNodeEntity> _sccpnMultySpanEntities)
        {
            return Student_CalCourseProgNode_DAO.UpdateMultySpanData(_sccpnMultySpanEntities);
        }

        public static int UpdateRequisitionData(List<Student_CalCourseProgNodeEntity> _sccpnEntities)
        {
            return Student_CalCourseProgNode_DAO.UpdateRequisitionData(_sccpnEntities);
        }

        public static int UndoRow(Student_CalCourseProgNodeEntity student_CalCourseProgNodeEntity)
        {
            return Student_CalCourseProgNode_DAO.UndoRow(student_CalCourseProgNodeEntity);
        }

        public static int ChkVacant(int sectionId)
        {
            return Section_DAO.ChkVacant(sectionId);
        }
    }
}
