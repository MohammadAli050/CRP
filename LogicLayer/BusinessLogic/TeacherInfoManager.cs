using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogicLayer.BusinessLogic
{
    public class TeacherInfoManager
    {
      

        public static bool Insert(TeacherInfo teacher)
        {
            bool isInserted = RepositoryManager.TeacherInformation_Repository.Insert(teacher);

            return isInserted;
        }
        public static bool Update(TeacherInfo teacher)
        {
            bool isUpdated = RepositoryManager.TeacherInformation_Repository.Update(teacher);

            return isUpdated;
        }

        public static List<TeacherInfo> GetByNameOrId(string name,string id)
        {
            List<TeacherInfo> teacherList = RepositoryManager.TeacherInformation_Repository.GetByNameOrId(name, id);
            return teacherList;
        }
        public static bool ValidateTeacher(string id)
        {
            bool isValidate = RepositoryManager.TeacherInformation_Repository.VaildateTeacher(id);
            return isValidate;
        }

        /*
        public static bool Update(RoomInformation roominfo)
        {
            bool result = RepositoryManager.RoomInformation_Repository.Update(roominfo);

            return result;
        }
        public static bool Delete(int roomInfoID)
        {
            bool result = RepositoryManager.RoomInformation_Repository.Delete(roomInfoID);

            return result;
        }*/
        public static TeacherInfo GetById(string id)
        {
            
            TeacherInfo teacher = RepositoryManager.TeacherInformation_Repository.GetById(id);
            return teacher;
        }

        /*
        public static List<RoomInformation> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            // const string rawKey = "RoomInformationGetAll";

            //   List<RoomInformation> list = GetCacheAsList(rawKey);

            List<RoomInformation> list = RepositoryManager.RoomInformation_Repository.GetAll();

            return list;
        }*/
    }
}
