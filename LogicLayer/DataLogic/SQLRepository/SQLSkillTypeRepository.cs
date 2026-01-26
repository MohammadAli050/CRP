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
    partial class SQLSkillTypeRepository : ISkillTypeRepository
    {
        Database db = null;

        private string sqlInsert = "SkillTypeInsert";
        private string sqlUpdate = "SkillTypeUpdate";
        private string sqlDelete = "SkillTypeDeleteById";
        private string sqlGetById = "SkillTypeGetById";
        private string sqlGetAll = "SkillTypeGetAll";


        public int Insert(SkillType skillType)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, skillType, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "SkillTypeID");

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

        public bool Update(SkillType skillType)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, skillType, isInsert);

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

                db.AddInParameter(cmd, "SkillTypeID", DbType.Int32, id);
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

        public SkillType GetById(int? id)
        {
            SkillType _skillType = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<SkillType> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<SkillType>(sqlGetById, rowMapper);
                _skillType = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _skillType;
            }

            return _skillType;
        }

        public List<SkillType> GetAll()
        {
            List<SkillType> skillTypeList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<SkillType> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<SkillType>(sqlGetAll, mapper);
                IEnumerable<SkillType> collection = accessor.Execute();

                skillTypeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return skillTypeList;
            }

            return skillTypeList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, SkillType skillType, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "SkillTypeID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "SkillTypeID", DbType.Int32, skillType.SkillTypeID);
            }

            db.AddInParameter(cmd, "SkillTypeID", DbType.Int32, skillType.SkillTypeID);
            db.AddInParameter(cmd, "TypeDescription", DbType.String, skillType.TypeDescription);
            return db;
        }

        private IRowMapper<SkillType> GetMaper()
        {
            IRowMapper<SkillType> mapper = MapBuilder<SkillType>.MapAllProperties()
            .Map(m => m.SkillTypeID).ToColumn("SkillTypeID")
            .Map(m => m.TypeDescription).ToColumn("TypeDescription")
            .Build();

            return mapper;
        }
        #endregion
    }
}
