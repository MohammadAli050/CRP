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
    public class StudentInstitutionManager
    {
        public static int Insert(StudentInstitution studentinstitution)
        {
            int id = RepositoryManager.StudentInstitution_Repository.Insert(studentinstitution);

            return id;
        }

        public static bool Update(StudentInstitution studentinstitution)
        {
            bool isExecute = RepositoryManager.StudentInstitution_Repository.Update(studentinstitution);

            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StudentInstitution_Repository.Delete(id);

            return isExecute;
        }

        public static StudentInstitution GetById(int? id)
        {
            StudentInstitution studentinstitution = RepositoryManager.StudentInstitution_Repository.GetById(id);

            return studentinstitution;
        }

        public static StudentInstitution GetByStudentId(int StudentId)
        {
            StudentInstitution studentinstitution = RepositoryManager.StudentInstitution_Repository.GetByStudentId(StudentId);

            return studentinstitution;
        }

        public static List<StudentInstitution> GetAll()
        {
            List<StudentInstitution> list = RepositoryManager.StudentInstitution_Repository.GetAll();


            return list;
        }
    }
}

