using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.BusinessLogic
{
    public class DegreeCompletionManager
    {
        public static int Insert(DegreeCompletion degreeCompletion)
        {
            int id = RepositoryManager.DegreeCompletion_Repository.Insert(degreeCompletion);
            return id;
        }

        public static bool Update(DegreeCompletion degreeCompletion)
        {
            bool isUpdate = RepositoryManager.DegreeCompletion_Repository.Update(degreeCompletion);
            return isUpdate;
        }

        public static bool Delete(int id)
        {
            bool isDelete = RepositoryManager.DegreeCompletion_Repository.Delete(id);
            return isDelete;
        }

        public static List<DegreeCompletion> GetAll()
        {
            List<DegreeCompletion> degreeCom = RepositoryManager.DegreeCompletion_Repository.GetAll();
            return degreeCom;
        }

        public static DegreeCompletion GetById(int id)
        {
            DegreeCompletion degreeCompletion = null;
            degreeCompletion = RepositoryManager.DegreeCompletion_Repository.GetById(id);
            return degreeCompletion;
        }

        public static DegreeCompletion GetByStudentId(int studentId)
        {
            DegreeCompletion degreeCompletion = null;
            degreeCompletion = RepositoryManager.DegreeCompletion_Repository.GetByStudentId(studentId);
            return degreeCompletion;
        }
    }
}
