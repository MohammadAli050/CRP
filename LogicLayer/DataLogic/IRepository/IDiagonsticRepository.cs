using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IDiagonsticRepository
    {
        DataTable GetData(int queryNo, int acaCalId);
    }
}
