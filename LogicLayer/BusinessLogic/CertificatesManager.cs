using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class CertificatesManager
    {
        public static int Insert(Certificates certificate)
        {
            int id = RepositoryManager.Certificates_Repository.Insert(certificate);
            return id;
        }

        public static bool Update(Certificates certificate)
        {
            bool isExecute = RepositoryManager.Certificates_Repository.Update(certificate);
            return isExecute;
        }

        public static int GenerateSerialNo(int typeId)
        {
            return RepositoryManager.Certificates_Repository.GenerateSerialNo(typeId);
        }

        public static Certificates GetAllByRoll(string roll,int type)
        {
            Certificates _certificates = RepositoryManager.Certificates_Repository.GetAllByRoll(roll, type);
            return _certificates;
        }

        public static CertificatesDTO CertificatesDTOGetByRoll(string roll)
        {
            CertificatesDTO obj = RepositoryManager.Certificates_Repository.CertificatesDTOGetByRoll(roll);
            return obj;
        }

        public static bool EarnedCreditAndRequiredCreditByRoll(string Roll)
        {
            bool isPassed = RepositoryManager.Certificates_Repository.EarnedCreditAndRequiredCreditByRoll(Roll);

            return isPassed;
        }

        public static bool CheckValidity(string Roll,int Serial,int TypeId)
        {
            bool isValid = RepositoryManager.Certificates_Repository.CheckValidity(Roll,Serial,TypeId);
            return isValid;
        }
    }
}
