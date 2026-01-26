using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessLogic
{
    public class TransferStudentManager
    {
        public static int Transfer(string batchCode)
        {
            int id = RepositoryManager.TransferStudent_Repository.Transfer(batchCode);
            return id;
        }
    }
}
