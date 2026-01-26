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
    public partial class SQLPersonBlockRepository : IPersonBlockRepository
    {
        Database db = null;

        private string sqlInsert = "PersonBlockInsert";
        private string sqlUpdate = "PersonBlockUpdate";
        private string sqlDelete = "PersonBlockDelete";
        private string sqlGetById = "PersonBlockGetById";
        private string sqlGetAll = "PersonBlockGetAll";
        private string sqlGetByPersonID = "PersonBlockGetByPersonID";
        private string sqlDeleteByPerson = "PersonBlockDeleteByPersonId";

        public int Insert(PersonBlock personblock)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, personblock, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "PersonBlockId");

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

        public bool Update(PersonBlock personblock)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, personblock, isInsert);

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

                db.AddInParameter(cmd, "PersonBlockId", DbType.Int32, id);
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

        public PersonBlock GetById(int id)
        {
            PersonBlock _personblock = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PersonBlock> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<PersonBlock>(sqlGetById, rowMapper);
                _personblock = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _personblock;
            }

            return _personblock;
        }

        public List<PersonBlock> GetAll()
        {
            List<PersonBlock> personblockList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PersonBlock> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<PersonBlock>(sqlGetAll, mapper);
                IEnumerable<PersonBlock> collection = accessor.Execute();

                personblockList = collection.ToList();
            }

            catch (Exception ex)
            {
                return personblockList;
            }

            return personblockList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, PersonBlock personblock, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "PersonBlockId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "PersonBlockId", DbType.Int32, personblock.PersonBlockId);
            }


            db.AddInParameter(cmd, "PersonId", DbType.Int32, personblock.PersonId);
            db.AddInParameter(cmd, "StartDateAndTime", DbType.DateTime, personblock.StartDateAndTime);
            db.AddInParameter(cmd, "EndDateAndTime", DbType.DateTime, personblock.EndDateAndTime);
            db.AddInParameter(cmd, "Remarks", DbType.String, personblock.Remarks);

            db.AddInParameter(cmd, "IsAdmitCardBlock", DbType.Boolean, personblock.IsAdmitCardBlock);
            db.AddInParameter(cmd, "IsRegistrationBlock", DbType.Boolean, personblock.IsRegistrationBlock);
            db.AddInParameter(cmd, "IsMasterBlock", DbType.Boolean, personblock.IsMasterBlock);
            db.AddInParameter(cmd, "IsResultBlock", DbType.Boolean, personblock.IsResultBlock);

            db.AddInParameter(cmd, "IsProbationBlock", DbType.Boolean, personblock.IsProbationBlock);
            db.AddInParameter(cmd, "IsBlock2", DbType.Boolean, personblock.IsBlock2);
            db.AddInParameter(cmd, "IsBlock3", DbType.Boolean, personblock.IsBlock3);

            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, personblock.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, personblock.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, personblock.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, personblock.ModifiedDate);

            return db;
        }

        private IRowMapper<PersonBlock> GetMaper()
        {
            IRowMapper<PersonBlock> mapper = MapBuilder<PersonBlock>.MapAllProperties()

            .Map(m => m.PersonBlockId).ToColumn("PersonBlockId")
            .Map(m => m.PersonId).ToColumn("PersonId")
            .Map(m => m.StartDateAndTime).ToColumn("StartDateAndTime")
            .Map(m => m.EndDateAndTime).ToColumn("EndDateAndTime")
            .Map(m => m.Remarks).ToColumn("Remarks")

            .Map(m => m.IsAdmitCardBlock).ToColumn("IsAdmitCardBlock")
            .Map(m => m.IsRegistrationBlock).ToColumn("IsRegistrationBlock")
            .Map(m => m.IsMasterBlock).ToColumn("IsMasterBlock")
             .Map(m => m.IsResultBlock).ToColumn("IsResultBlock")


            .Map(m => m.IsProbationBlock).ToColumn("IsProbationBlock")
            .Map(m => m.IsBlock2).ToColumn("IsBlock2")
            .Map(m => m.IsBlock3).ToColumn("IsBlock3")

            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")

            .Build();

            return mapper;
        }
        #endregion

        public PersonBlock GetByPersonId(int PersonID)
        {
            PersonBlock _personblock = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PersonBlock> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<PersonBlock>(sqlGetByPersonID, rowMapper);
                _personblock = accessor.Execute(PersonID).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _personblock;
            }

            return _personblock;
        }

        public bool DeleteByPerson(int personId)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlDeleteByPerson);

                db.AddInParameter(cmd, "PersonId", DbType.Int32, personId);
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

        public List<PersonBlockDTO> GetAllByProgramOrBatchOrRoll(int programId, int batchId, string roll, int dueUptoSession)
        {
            List<PersonBlockDTO> personblockList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PersonBlockDTO> mapper = MapBuilder<PersonBlockDTO>.MapAllProperties()

                .Map(m => m.BatchNO).ToColumn("BatchNO")
                .Map(m => m.CGPA).ToColumn("CGPA")
                .Map(m => m.IsAdmitCardBlock).ToColumn("IsAdmitCardBlock")
                .Map(m => m.Dues).ToColumn("Dues")
                .Map(m => m.IsRegistrationBlock).ToColumn("IsRegistrationBlock")
                .Map(m => m.IsActive).ToColumn("IsActive")
                .Map(m => m.IsMasterBlock).ToColumn("IsMasterBlock")
                .Map(m => m.Name).ToColumn("Name")
                .Map(m => m.IsProbationBlock).ToColumn("IsProbationBlock")
                .Map(m => m.programId).ToColumn("programId")
                .Map(m => m.Remarks).ToColumn("Remarks")
                .Map(m => m.Roll).ToColumn("Roll")
                .Map(m => m.StudentID).ToColumn("StudentID")
                .Build();

                var accessor = db.CreateSprocAccessor<PersonBlockDTO>("PersonBlockDTOGetByProgramOrBatchOrRoll", mapper);
                IEnumerable<PersonBlockDTO> collection = accessor.Execute(programId, batchId, roll, dueUptoSession);

                personblockList = collection.ToList();
            }

            catch (Exception ex)
            {
                return personblockList;
            }

            return personblockList;
        }

        public List<PersonBlockDTO> GetAllByProgramOrBatchOrRoll(int programId, int batchId, string roll,int registrationSession,int dueUptoSession)
        {
            List<PersonBlockDTO> personblockList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PersonBlockDTO> mapper = MapBuilder<PersonBlockDTO>.MapAllProperties()

                .Map(m => m.BatchNO).ToColumn("BatchNO")
                .Map(m => m.CGPA).ToColumn("CGPA")
                .Map(m => m.IsAdmitCardBlock).ToColumn("IsAdmitCardBlock")
                .Map(m => m.Dues).ToColumn("Dues")
                .Map(m => m.IsRegistrationBlock).ToColumn("IsRegistrationBlock")
                .Map(m => m.IsActive).ToColumn("IsActive")
                .Map(m => m.IsMasterBlock).ToColumn("IsMasterBlock")
                .Map(m => m.Name).ToColumn("Name")
                .Map(m => m.IsProbationBlock).ToColumn("IsProbationBlock")
                .Map(m => m.programId).ToColumn("programId")
                .Map(m => m.Remarks).ToColumn("Remarks")
                .Map(m => m.Roll).ToColumn("Roll")
                .Map(m => m.StudentID).ToColumn("StudentID")
                .Build();

                var accessor = db.CreateSprocAccessor<PersonBlockDTO>("PersonBlockDTOGetByProgramOrBatchOrRollSession", mapper);
                IEnumerable<PersonBlockDTO> collection = accessor.Execute(programId, batchId, roll, registrationSession, dueUptoSession);

                personblockList = collection.ToList();
            }

            catch (Exception ex)
            {
                return personblockList;
            }

            return personblockList;
        }


        public PersonBlockDTO GetByRoll(string roll)
        {
            PersonBlockDTO person = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PersonBlockDTO> mapper = MapBuilder<PersonBlockDTO>.MapAllProperties()

                .Map(m => m.BatchNO).ToColumn("BatchNO")
                .Map(m => m.CGPA).ToColumn("CGPA")
                .Map(m => m.IsAdmitCardBlock).ToColumn("IsAdmitCardBlock")
                .Map(m => m.Dues).ToColumn("Dues")
                .Map(m => m.IsRegistrationBlock).ToColumn("IsRegistrationBlock")
                .Map(m => m.IsActive).ToColumn("IsActive")
                .Map(m => m.IsMasterBlock).ToColumn("IsMasterBlock")
                .Map(m => m.Name).ToColumn("Name")
                .Map(m => m.IsProbationBlock).ToColumn("IsProbationBlock")
                .Map(m => m.programId).ToColumn("programId")
                .Map(m => m.Remarks).ToColumn("Remarks")
                .Map(m => m.Roll).ToColumn("Roll")
                .Map(m => m.StudentID).ToColumn("StudentID")
                .Build();

                var accessor = db.CreateSprocAccessor<PersonBlockDTO>("PersonBlockDTOGetByRoll", mapper);
                person = accessor.Execute(roll).SingleOrDefault();

            }

            catch (Exception ex)
            {
                return person;
            }

            return person;
        }

    }
}

