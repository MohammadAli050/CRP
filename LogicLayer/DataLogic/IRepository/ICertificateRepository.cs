using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ICertificatesRepository
    {
        int Insert(Certificates certificate);
        bool Update(Certificates certificate);
        Certificates GetAllByRoll(string roll,int type);
        int GenerateSerialNo(int typeId);
        CertificatesDTO CertificatesDTOGetByRoll(string roll);
        bool EarnedCreditAndRequiredCreditByRoll(string Roll);
        bool CheckValidity(string Roll, int Serial, int TypeId);
    }
}
