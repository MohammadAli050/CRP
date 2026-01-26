using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IAffiliatedInstitutionRepository
    {
        int Insert(AffiliatedInstitution affiliatedinstitution);
        bool Update(AffiliatedInstitution affiliatedinstitution);
        bool Delete(int Id);
        AffiliatedInstitution GetById(int? Id);
        List<AffiliatedInstitution> GetAll();
    }
}

