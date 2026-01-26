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
    public class StudentTranscriptInfoManager
    {


        public static int Insert(StudentTranscriptInfo studenttranscriptinfo)
        {
            int id = RepositoryManager.StudentTranscriptInfo_Repository.Insert(studenttranscriptinfo);

            return id;
        }

        public static bool Update(StudentTranscriptInfo studenttranscriptinfo)
        {
            bool isExecute = RepositoryManager.StudentTranscriptInfo_Repository.Update(studenttranscriptinfo);

            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StudentTranscriptInfo_Repository.Delete(id);

            return isExecute;
        }

        public static StudentTranscriptInfo GetById(int? id)
        {
            StudentTranscriptInfo studenttranscriptinfo = new StudentTranscriptInfo();

            studenttranscriptinfo = RepositoryManager.StudentTranscriptInfo_Repository.GetById(id);

            return studenttranscriptinfo;
        }

        public static StudentTranscriptInfo GetByStudentId(int studentId)
        {
            //string rawKey = "StudentTranscriptInfoByStudentID" + studentId;
            StudentTranscriptInfo studenttranscriptinfo = new StudentTranscriptInfo();

            studenttranscriptinfo = RepositoryManager.StudentTranscriptInfo_Repository.GetByStudentId(studentId);


            return studenttranscriptinfo;
        }

        public static List<StudentTranscriptInfo> GetByProgramIdBatchIdRoll(int ProgramId, int BatchId, string Roll)
        {
            List<StudentTranscriptInfo> list = new List<StudentTranscriptInfo>();
            list = RepositoryManager.StudentTranscriptInfo_Repository.GetByProgramIdBatchIdRoll(ProgramId, BatchId, Roll);

            return list;
        }

        public static List<StudentTranscriptInfo> GetAll()
        {

            List<StudentTranscriptInfo> list = new List<StudentTranscriptInfo>();

            list = RepositoryManager.StudentTranscriptInfo_Repository.GetAll();

            return list;
        }
    }
}
