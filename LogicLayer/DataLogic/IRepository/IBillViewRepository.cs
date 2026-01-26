using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IBillViewRepository
    {
        int Insert(BillView billview);
        bool Update(BillView billview);
        bool Delete(int BillViewId);
        BillView GetById(int BillViewId);
        List<BillView> GetAll();
        BillView GetBy(int studentId, int accountsID, int sessionId);
        BillView GetBy(int studentId, int accountsID, int sessionId, int courseId, int versionId);
        List<BillView> GetBy(int studentId, int sessionId);
        List<BillView> GetBy(int studentId);
        bool Delete(int studentID, int sessionId, int courseID, int versionID);
    }
}

