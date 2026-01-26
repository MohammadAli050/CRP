using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BussinessObject
{
    public class ConflictResult_BAO
    {
        public static List<ConflictResultEntity> Load(int stdId, int courseID, int versionID, int sectionID)
        {
            return ConflictResult_DAO.Load(stdId, courseID, versionID, sectionID);
        }

        public static int Update(int id)
        {
            return ConflictResult_DAO.Update( id);
        }
    }
}
