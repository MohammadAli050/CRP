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
    public partial class SQLContactDetailsManager:IContactDetailsRepository
    {
        Database db = null;

        private string sqlInsert = "ContactDetailsInsert";
        private string sqlUpdate = "ContactDetailsUpdate";
        private string sqlDelete = "ContactDetailsDeleteById";
        private string sqlGetAll = "ContactDetailsGetAll";
        private string sqlGetByPersonId = "ContactDetailsGetByPersonId";
        
        public int Insert(ContactDetails contact)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, contact, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "PersonID");

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

        public bool Update(ContactDetails contact)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, contact, isInsert);

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

        public bool Delete(int id)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlDelete);

                db.AddInParameter(cmd, "PersonID", DbType.Int32, id);
                int rowsAffected = db.ExecuteNonQuery(cmd);

                if (rowsAffected > 0)
                {
                    result = true;
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public List<ContactDetails> GetAll()
        {
            List<ContactDetails> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ContactDetails> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ContactDetails>(sqlGetAll, mapper);
                IEnumerable<ContactDetails> collection = accessor.Execute();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public ContactDetails GetContactDetailsByPersonID(int personId)
        {
            ContactDetails contact = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ContactDetails> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ContactDetails>(sqlGetByPersonId, mapper);
                IEnumerable<ContactDetails> collection = accessor.Execute(personId).ToList();

                contact = collection.Single();
            }

            catch (Exception ex)
            {
                return contact;
            }

            return contact;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ContactDetails contact, bool isInsert)
        {

            db.AddInParameter(cmd, "Mobile1", DbType.String, contact.Mobile1);
            db.AddInParameter(cmd, "Mobile2", DbType.String, contact.Mobile2);
            db.AddInParameter(cmd, "PhoneResidential", DbType.String, contact.PhoneResidential);
            db.AddInParameter(cmd, "PhoneOffice", DbType.String, contact.PhoneOffice);
            db.AddInParameter(cmd, "PhoneEmergency", DbType.String, contact.PhoneEmergency);
            db.AddInParameter(cmd, "PersonID", DbType.Int32, contact.PersonID);
            db.AddInParameter(cmd, "EmailOffice", DbType.String, contact.EmailOffice);
            db.AddInParameter(cmd, "EmailOther", DbType.String, contact.EmailOther);
            db.AddInParameter(cmd, "EmailPersonal", DbType.String, contact.EmailPersonal);
       
            
            return db;
        }

        private IRowMapper<ContactDetails> GetMaper()
        {
            IRowMapper<ContactDetails> mapper = MapBuilder<ContactDetails>.MapAllProperties()
            .Map(m => m.EmailOffice).ToColumn("EmailOffice")
            .Map(m => m.EmailOther).ToColumn("EmailOther")
            .Map(m => m.EmailPersonal).ToColumn("EmailPersonal")
            .Map(m => m.PhoneEmergency).ToColumn("PhoneEmergency")
            .Map(m => m.PhoneOffice).ToColumn("PhoneOffice")
            .Map(m => m.PhoneResidential).ToColumn("PhoneResidential")
            .Map(m => m.Mobile1).ToColumn("Mobile1")
            .Map(m => m.Mobile2).ToColumn("Mobile2")
            .Map(m => m.PersonID).ToColumn("PersonID")
            .Build();

            return mapper;
        }
        #endregion
    }
}
