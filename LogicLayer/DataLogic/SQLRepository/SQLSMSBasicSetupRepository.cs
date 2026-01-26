using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.SQLRepository
{
    public class SQLSMSBasicSetupRepository:ISMSBasicSetupRepository
    {
        Database db = null;
        public SMSBasicSetup Get()
        {
            SMSBasicSetup _smsSetup = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<SMSBasicSetup> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<SMSBasicSetup>("SMSBasicSetupGet", rowMapper);
                _smsSetup = accessor.Execute().SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _smsSetup;
            }

            return _smsSetup;
        }

        public bool Update(SMSBasicSetup smsSetup)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("SMSBasicSetupUpdate");

                db = addParam(db, cmd, smsSetup, isInsert);

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

        #region Mapper

        private Database addParam(Database db, DbCommand cmd, SMSBasicSetup smsSetup, bool isInsert)
        {
            db.AddInParameter(cmd, "ID", DbType.String, smsSetup.ID);
            db.AddInParameter(cmd, "RemainingSMS", DbType.Int32, smsSetup.RemainingSMS);
            db.AddInParameter(cmd, "RegistrationStatus", DbType.Boolean, smsSetup.RegistrationStatus);
            db.AddInParameter(cmd, "ResultCorrectionStatus", DbType.Boolean, smsSetup.ResultCorrectionStatus);
            db.AddInParameter(cmd, "ResultPubStatus", DbType.Boolean, smsSetup.ResultPubStatus);
            db.AddInParameter(cmd, "LateFineStatus", DbType.Boolean, smsSetup.LateFineStatus);
            db.AddInParameter(cmd, "WaiverPostingStatus", DbType.Boolean, smsSetup.WaiverPostingStatus);
            db.AddInParameter(cmd, "AdmitCardStatus", DbType.Boolean, smsSetup.AdmitCardStatus);
            db.AddInParameter(cmd, "CustomSmsStatus", DbType.Boolean, smsSetup.CustomSmsStatus);
            db.AddInParameter(cmd, "BillCollectionStatus", DbType.Boolean, smsSetup.BillCollectionStatus);
            return db;
        }

        private IRowMapper<SMSBasicSetup> GetMaper()
        {
            IRowMapper<SMSBasicSetup> mapper = MapBuilder<SMSBasicSetup>.MapAllProperties()
            .Map(m => m.ID).ToColumn("ID")
            .Map(m => m.RemainingSMS).ToColumn("RemainingSMS")
            .Map(m => m.RegistrationStatus).ToColumn("RegistrationStatus")
            .Map(m => m.ResultCorrectionStatus).ToColumn("ResultCorrectionStatus")
            .Map(m => m.ResultPubStatus).ToColumn("ResultPubStatus")
            .Map(m => m.WaiverPostingStatus).ToColumn("WaiverPostingStatus")
            .Map(m => m.LateFineStatus).ToColumn("LateFineStatus")
            .Map(m => m.AdmitCardStatus).ToColumn("AdmitCardStatus")
            .Map(m => m.BillCollectionStatus).ToColumn("BillCollectionStatus")
            .Map(m => m.CustomSmsStatus).ToColumn("CustomSmsStatus")
            .Build();
            return mapper;
        }
        #endregion
    }
}
