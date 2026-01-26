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
    public partial class SQLProgramTypeRepository : IProgramTypeRepository
    {
        Database db = null;

        private string sqlInsert = "ProgramTypeInsert";
        private string sqlUpdate = "ProgramTypeUpdate";
        private string sqlDelete = "ProgramTypeDeleteById";
        private string sqlGetById = "ProgramTypeGetById";
        private string sqlGetAll = "ProgramTypeGetAll";


        public int Insert(ProgramType programType)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, programType, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ProgramTypeID");

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

        public bool Update(ProgramType programType)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, programType, isInsert);

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

                db.AddInParameter(cmd, "ProgramTypeID", DbType.Int32, id);
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

        public ProgramType GetById(int? id)
        {
            ProgramType _programType = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ProgramType> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ProgramType>(sqlGetById, rowMapper);
                _programType = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _programType;
            }

            return _programType;
        }

        public List<ProgramType> GetAll()
        {
            List<ProgramType> programTypeList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ProgramType> mapper = GetMaper();

                var accessor = db.CreateSqlStringAccessor<ProgramType>("Select * from ProgramType", mapper);
                IEnumerable<ProgramType> collection = accessor.Execute();

                programTypeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return programTypeList;
            }

            return programTypeList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ProgramType programType, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ProgramTypeID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ProgramTypeID", DbType.Int32, programType.ProgramTypeID);
            }

            db.AddInParameter(cmd, "ProgramTypeID", DbType.Int32, programType.ProgramTypeID);
            db.AddInParameter(cmd, "TypeDescription", DbType.String, programType.TypeDescription);

            return db;
        }

        private IRowMapper<ProgramType> GetMaper()
        {
            IRowMapper<ProgramType> mapper = MapBuilder<ProgramType>.MapAllProperties()
            .Map(m => m.ProgramTypeID).ToColumn("ProgramTypeID")
            .Map(m => m.TypeDescription).ToColumn("TypeDescription")
            .Build();

            return mapper;
        }
        #endregion
    }
}
