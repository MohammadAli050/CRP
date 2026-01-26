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
    public class ExamCenterManager
    {
        public static int Insert(ExamCenter examcenter)
        {
            int id = RepositoryManager.ExamCenter_Repository.Insert(examcenter);
            return id;
        }

        public static bool Update(ExamCenter examcenter)
        {
            bool isExecute = RepositoryManager.ExamCenter_Repository.Update(examcenter);
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamCenter_Repository.Delete(id);
            return isExecute;
        }

        public static ExamCenter GetById(int? id)
        {
            ExamCenter examcenter = RepositoryManager.ExamCenter_Repository.GetById(id);

            return examcenter;
        }

        public static List<ExamCenter> GetAll()
        {
            List<ExamCenter> list = RepositoryManager.ExamCenter_Repository.GetAll();

            return list;
        }
    }
}

