using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class StudentFeedbackManager
    {
        public static int Insert(StudentFeedback studentfeedback)
        {
            int id = RepositoryManager.StudentFeedback_Repository.Insert(studentfeedback);
            return id;
        }

        public static bool Update(StudentFeedback studentfeedback)
        {
            bool isExecute = RepositoryManager.StudentFeedback_Repository.Update(studentfeedback);
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StudentFeedback_Repository.Delete(id);
            return isExecute;
        }

        public static StudentFeedback GetById(int? id)
        {
            StudentFeedback studentfeedback = RepositoryManager.StudentFeedback_Repository.GetById(id);
            return studentfeedback;
        }

        public static List<StudentFeedback> GetAll()
        {
            List<StudentFeedback> list = RepositoryManager.StudentFeedback_Repository.GetAll();
            return list;
        }

        public static List<StudentFeedback> GetAllByStdentId(int StudentId)
        {
            List<StudentFeedback> list = RepositoryManager.StudentFeedback_Repository.GetAllByStdentId(StudentId);
            return list;
        }
    }
}

