using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;
using BussinessObject;

namespace BussinessObject
{
    public class FormSaleSubmit_BAO
    {
        public static List<FormGridEntity> GetAllCandidateInformation()
        {
            return FormGrid_DAO.GetAllInfoForGrid();
        }

        public static List<FormGridEntity> GetAllCandidateInfoByProgram(string shortName)
        {
            return FormGrid_DAO.GetAllCandidatesByProgram(shortName);
        }

        public static FormGridEntity GetGridInfoByFormSL(string formSL)
        {
            return FormGrid_DAO.GetCandidateByForm(formSL);
        }

        public static List<RbProgramEntity> GetAllPrograms()
        {
            return rbProgram_DAO.GetPrograms();
        }

        public static CcAcademiccalenderEntity GetAcademicCalenderEntity()
        {
            return rbAcademiccalenderDao.GetNextAcademicCalenderEntity();
        }

        public static int GetMaxCandidateID()
        {
            return EmsCandidate_DAO.GetMaxCandidateId();
        }

        public static int GetMaxFormID()
        {
            return AdmissionForm_DAO.GetMaxFormID();
        }

        public static string MakeFormSerial(string batchCode, int formId)
        {
            
            return AdmissionForm_DAO.GenerateFormSerial(batchCode, formId);
        }

        public static CcAcademiccalenderEntity GetNextTrymesterInfo()
        {
            return rbAcademiccalenderDao.GetNextAcademicCalenderEntity();
        }

        public static string MakeAdmissionRollforCandidate(string programCode, int id)
        {
            return EmsCandidate_DAO.MakeAdmissionRoll(programCode, id);
        }

        //public static int SaveCndidates(List<EmsCandidateEntity> candidateEntities)
        //{
        //    return EmsCandidate_DAO.save(List<EmsCandidateEntity>);
        //}

        public static int SaveCndidate(EmsCandidateEntity candidateEntity)
        {
            return EmsCandidate_DAO.save(candidateEntity);
        }

        public static int SaveForm(AdmissionFormEntity formEntity)
        {
            return AdmissionForm_DAO.save(formEntity);
        }

        public static EmsCandidateEntity GetCandidateByID(int ID)
        {
            return EmsCandidate_DAO.GetCandidateByID(ID);
        }

        public static AdmissionFormEntity GetAdmissionFormEntityByCandidateID(int candidateID)
        {
            return AdmissionForm_DAO.GetFormInformationOfCandidate(candidateID);
        }

        public static int GetMaxMRN()
        {
            return EmsCandidate_DAO.GetMaxMRN();
        }

        public static string ChangeRollofCandidate(string programCode,string currentRoll)
        {
            string changedRoll = string.Empty;
            try
            {
                string rollWithoutProgramCode = currentRoll.Substring(3);
                int roll = Convert.ToInt32(rollWithoutProgramCode);
                changedRoll = EmsCandidate_DAO.MakeAdmissionRoll(programCode, roll);
            }
            catch (Exception exception)
            {

                throw exception;
            }
            return changedRoll;
        }
    }
}
