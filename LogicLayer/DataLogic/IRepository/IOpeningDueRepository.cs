using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IOpeningDueRepository
    {
        int Insert(OpeningDue openingdue);
        bool Update(OpeningDue openingdue);
        bool Delete(int OpeningDueId);
        OpeningDue GetById(int? OpeningDueId);
        List<OpeningDue> GetAll();
        OpeningDue GetByStudentId(int studentID);
    }
}

