using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessLogic
{
    public class DiagonsticManager
    {
        public static DataTable GetData(int queryNo, int acaCalId)
        {
            DataTable dt = RepositoryManager.Diagonstic_Repository.GetData( queryNo,  acaCalId);
            return dt;
        }
    }
}
