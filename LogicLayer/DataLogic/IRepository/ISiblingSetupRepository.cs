using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ISiblingSetupRepository
    {
        int Insert(SiblingSetup siblingSetup);
        bool Update(SiblingSetup siblingSetup);
        bool Delete(int id);
        SiblingSetup GetById(int? id);
        List<SiblingSetup> GetAll();

        SiblingSetup GetByApplicantId(int applicantId);

        bool DeleteByApplicantIdGroupId(int applicant, int groupId);
    }
}
