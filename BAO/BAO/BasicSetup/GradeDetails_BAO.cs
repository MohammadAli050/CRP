using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BussinessObject
{
    public class GradeDetails_BAO
    {
        public static int Save(List<GradeDetailsEntity> gradeDetailsEntities, int creatorId, int acaCalId, int programId)
        {
            return GradeDetails_DAO.save(gradeDetailsEntities, creatorId, acaCalId, programId);
        }

        public static List<GradeDetailsEntity> Load(int acaCalId, int programId)
        {
            return GradeDetails_DAO.Load(acaCalId,programId);
        }

        public static int Delete(int id)
        {
            return GradeDetails_DAO.Delete(id);
        }

        public static int Update(GradeDetailsEntity entity)
        {
            return GradeDetails_DAO.Update(entity);
        }

        public static bool CheckDuplicate(int acaCalId, int programId)
        {
            int counter = 0;
             counter =  GradeDetails_DAO.CheckDuplicate(acaCalId, programId);

             if (counter < 1)
             {
                 return false;
             }
             else
             {
                 return true;
             }
        }
        
    }
}
