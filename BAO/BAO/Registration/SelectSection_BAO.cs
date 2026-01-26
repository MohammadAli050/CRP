using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BussinessObject
{
    public class Section_BAO
    {
        public static List<SectionEntity> GetSections(int acId, int deptId, int proId, int couId, int verId)
        {
           return Section_DAO.GetSections(acId,  deptId,  proId,  couId,  verId);
        }
    }
}
