using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SqlDailyCollectionRepository : IDailyCollection
    {
        Database db = null;


        private string sqlDailyCollectionGetByProgramAndDate = "RptDailyCollectionGetByProgramAndDate";
        private string sqlDailyBillHistoryGetByProgramAndDate = "RptDailyBillHistoryGetByProgramAndDate";

        public List<rDailyCollection> GetDailyCollectionByProgramAndDate(DateTime fromDate, DateTime toDate, int programId)
        {
             List<rDailyCollection> list =null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rDailyCollection> mapper = GetMaper();


                var accessor = db.CreateSprocAccessor<rDailyCollection>(sqlDailyCollectionGetByProgramAndDate, mapper);

                IEnumerable<rDailyCollection> collection = accessor.Execute(fromDate, toDate, programId).ToList();

                list = collection.ToList();

            }
            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<rDailyBillHistory> GetDailyBillHistoryByProgramAndDate(DateTime fromDate, DateTime toDate, int programId)
        {
            List<rDailyBillHistory> list = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rDailyBillHistory> mapper = GetBillHistoryMaper();


                var accessor = db.CreateSprocAccessor<rDailyBillHistory>(sqlDailyBillHistoryGetByProgramAndDate, mapper);

                IEnumerable<rDailyBillHistory> collection = accessor.Execute(fromDate, toDate, programId).ToList();

                list = collection.ToList();

            }
            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        #region Mapper

        private IRowMapper<rDailyCollection> GetMaper()
        {
            IRowMapper<rDailyCollection> mapper = MapBuilder<rDailyCollection>.MapAllProperties()
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.FullName).ToColumn("FullName")
            .Map(m => m.Semester).ToColumn("Semester")
            .Map(m => m.PaymentType).ToColumn("PaymentType")
            .Map(m => m.Amount).ToColumn("Amount")
            .Map(m => m.MoneyReceiptId).ToColumn("MoneyReciptId")
            .Map(m => m.CollectionDate).ToColumn("CollectionDate")
            .Map(m => m.ProgramName).ToColumn("ShortName")
            .DoNotMap(m => m.Bank)
            .DoNotMap(m => m.Cash)
            
            .Build();

            return mapper;
        }
        #endregion
        private IRowMapper<rDailyBillHistory> GetBillHistoryMaper()
        {
            IRowMapper<rDailyBillHistory> mapper = MapBuilder<rDailyBillHistory>.MapAllProperties()

            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.FullName).ToColumn("FullName")
            .Map(m => m.TypeName).ToColumn("TypeName")
            .Map(m => m.Year).ToColumn("Year")
            .Map(m => m.ProgramName).ToColumn("ShortName")
            .Map(m => m.TypeDefinition).ToColumn("Definition")
            .Map(m => m.Remark).ToColumn("Remark")
            .Map(m => m.Fees).ToColumn("Fees")
            .Map(m => m.BillingDate).ToColumn("BillingDate")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")

            .Build();

            return mapper;
        }
    }
}
