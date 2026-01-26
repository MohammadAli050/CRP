using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLCertificatesRepository :ICertificatesRepository
    {
        Database db = null;

        private string sqlInsert = "CertificatesInsert";
        private string sqlUpdate = "CertificatesUpdate";
        private string sqlGetAllByRoll = "CertificatesGetAllByRoll";
        private string sqlGenerateSerialNo = "CertificatesGenerateSerialNo";
        private string sqlCertificatesDTOGetAllByRoll = "CertificatesDTOGetByRoll";

        public int Insert(Certificates certificate)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, certificate, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ID");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out id);
                }
            }
            catch (Exception ex)
            {
                id = 0;
            }

            return id;
        }

        public bool Update(Certificates certificate)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, certificate, isInsert);

                int rowsAffected = db.ExecuteNonQuery(cmd);

                if (rowsAffected > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public Certificates GetAllByRoll(string roll,int type)
        {
            Certificates _certificates = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Certificates> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Certificates>(sqlGetAllByRoll, rowMapper);
                _certificates = accessor.Execute(roll, type).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _certificates;
            }

            return _certificates;
        }

        public int GenerateSerialNo(int typeId)
        {
            int serial=0;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlGenerateSerialNo);

                db.AddOutParameter(cmd, "SerialNo", DbType.Int32, Int32.MaxValue);
                db.AddInParameter(cmd, "TypeID", DbType.Int32, typeId);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "SerialNo");
                
                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out serial);
                }
            }
            catch (Exception ex)
            {
                return serial;
            }

            return serial;
        }

        public bool EarnedCreditAndRequiredCreditByRoll(string Roll)
        {
            int id = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("RequiredCreditAndEarnedCreditByRoll");

                db.AddOutParameter(cmd, "Passed", DbType.Int32, Int32.MaxValue);
                db.AddInParameter(cmd, "Roll", DbType.String, Roll);
                db.ExecuteNonQuery(cmd);
                object obj = db.GetParameterValue(cmd, "Passed");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out id);
                }
            }
            catch (Exception ex)
            {
                id = 0;
            }
            if (id == 0)
            {
                return false;
            }
            else
                return true;

        }

        public bool CheckValidity(string Roll, int Serial,int TypeID)
        {
            int id = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("CertificatesSerialNoValidityByRollTypeID");

                db.AddOutParameter(cmd, "IsValid", DbType.Int32, Int32.MaxValue);
                db.AddInParameter(cmd, "Roll", DbType.String, Roll);
                db.AddInParameter(cmd, "Serial", DbType.Int32, Serial);
                db.AddInParameter(cmd, "TypeID", DbType.Int32, TypeID);
                db.ExecuteNonQuery(cmd);
                object obj = db.GetParameterValue(cmd, "IsValid");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out id);
                }
            }
            catch (Exception ex)
            {
                id = 0;
            }
            if (id == 0)
            {
                return false;
            }
            else
                return true;
        }

        public CertificatesDTO CertificatesDTOGetByRoll(string roll)
        {
            CertificatesDTO obj = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CertificatesDTO> rowMapper = MapBuilder<CertificatesDTO>.MapAllProperties()
                .Map(m => m.FullName).ToColumn("FullName")
                .Map(m => m.StudentID).ToColumn("StudentID")
                .Map(m => m.Gender).ToColumn("Gender")
                .Map(m => m.ProgramName).ToColumn("ProgramName")
                .Map(m => m.TypeName).ToColumn("TypeName")
                .Map(m => m.Year).ToColumn("Year")
                .Map(m => m.CGPA).ToColumn("CGPA")
                .Map(m => m.DegreeName).ToColumn("DegreeName")
                .Map(m => m.Duration).ToColumn("Duration")
                .Build();

                var accessor = db.CreateSprocAccessor<CertificatesDTO>(sqlCertificatesDTOGetAllByRoll, rowMapper);
                obj = accessor.Execute(roll).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return obj;
            }

            return obj;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, Certificates certificate, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ID", DbType.Int32, certificate.ID);
            }

            db.AddInParameter(cmd, "SerialNo", DbType.Int32, certificate.SerialNo);
            db.AddInParameter(cmd, "StudentID", DbType.Int32, certificate.StudentID);
            db.AddInParameter(cmd, "TypeID", DbType.Int32, certificate.TypeID);
            db.AddInParameter(cmd, "IsCancelled", DbType.Boolean, certificate.IsCancelled);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, certificate.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, certificate.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, certificate.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, certificate.ModifiedDate);

            return db;
        }

        private IRowMapper<Certificates> GetMaper()
        {
            IRowMapper<Certificates> mapper = MapBuilder<Certificates>.MapAllProperties()
            .Map(m => m.ID).ToColumn("ID")
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.SerialNo).ToColumn("SerialNo")
            .Map(m => m.TypeID).ToColumn("TypeID")
            .Map(m => m.IsCancelled).ToColumn("IsCancelled")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")

            .Build();

            return mapper;
        }
        #endregion
       
    }
}
