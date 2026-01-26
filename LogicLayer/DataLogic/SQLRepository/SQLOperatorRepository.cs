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
    partial class SQLOperatorRepository : IOperatorRepository
    {
        Database db = null;

        private string sqlInsert = "OperatorInsert";
        private string sqlUpdate = "OperatorUpdate";
        private string sqlDelete = "OperatorDeleteById";
        private string sqlGetById = "OperatorGetById";
        private string sqlGetAll = "OperatorGetAll";


        public int Insert(Operator objOperator)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, objOperator, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "OperatorID");

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

        public bool Update(Operator objOperator)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, objOperator, isInsert);

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

                db.AddInParameter(cmd, "OperatorID", DbType.Int32, id);
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

        public Operator GetById(int? id)
        {
            Operator _Operator = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Operator> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Operator>(sqlGetById, rowMapper);
                _Operator = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _Operator;
            }

            return _Operator;
        }

        public List<Operator> GetAll()
        {
            List<Operator> OperatorList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Operator> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Operator>(sqlGetAll, mapper);
                IEnumerable<Operator> collection = accessor.Execute();

                OperatorList = collection.ToList();
            }

            catch (Exception ex)
            {
                return OperatorList;
            }

            return OperatorList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, Operator objOperator, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "OperatorID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "OperatorID", DbType.Int32, objOperator.OperatorID);
            }

            db.AddInParameter(cmd, "OperatorID", DbType.Int32, objOperator.OperatorID);
            db.AddInParameter(cmd, "Name", DbType.String, objOperator.Name);
            return db;
        }

        private IRowMapper<Operator> GetMaper()
        {
            IRowMapper<Operator> mapper = MapBuilder<Operator>.MapAllProperties()
            .Map(m => m.OperatorID).ToColumn("OperatorID")
            .Map(m => m.Name).ToColumn("Name")
            .Build();

            return mapper;
        }
        #endregion
    }
}
