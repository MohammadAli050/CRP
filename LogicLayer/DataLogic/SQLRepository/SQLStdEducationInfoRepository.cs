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
    partial class SQLStdEducationInfoRepository : IStdEducationInfoRepository
    {
        Database db = null;

        private string sqlInsert = "StdEducationInfoInsert";
        private string sqlUpdate = "StdEducationInfoUpdate";
        private string sqlDelete = "StdEducationInfoDeleteById";
        private string sqlGetById = "StdEducationInfoGetById";
        private string sqlGetAll = "StdEducationInfoGetAll";


        public int Insert(StdEducationInfo stdEducationInfo)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, stdEducationInfo, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "StdEducationInfoID");

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

        public bool Update(StdEducationInfo stdEducationInfo)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, stdEducationInfo, isInsert);

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

                db.AddInParameter(cmd, "StdEducationInfoID", DbType.Int32, id);
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

        public StdEducationInfo GetById(int? id)
        {
            StdEducationInfo _stdEducationInfo = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StdEducationInfo> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StdEducationInfo>(sqlGetById, rowMapper);
                _stdEducationInfo = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _stdEducationInfo;
            }

            return _stdEducationInfo;
        }

        public List<StdEducationInfo> GetAll()
        {
            List<StdEducationInfo> stdEducationInfoList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StdEducationInfo> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StdEducationInfo>(sqlGetAll, mapper);
                IEnumerable<StdEducationInfo> collection = accessor.Execute();

                stdEducationInfoList = collection.ToList();
            }

            catch (Exception ex)
            {
                return stdEducationInfoList;
            }

            return stdEducationInfoList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StdEducationInfo stdEducationInfo, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "StdEducationInfoID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "StdEducationInfoID", DbType.Int32, stdEducationInfo.StdEducationInfoID);
            }

            db.AddInParameter(cmd, "StdEducationInfoID", DbType.Int32, stdEducationInfo.StdEducationInfoID);
            db.AddInParameter(cmd, "DregreeName", DbType.String, stdEducationInfo.DregreeName);
            db.AddInParameter(cmd, "GroupName", DbType.String, stdEducationInfo.GroupName);
            db.AddInParameter(cmd, "InstitutionName", DbType.String, stdEducationInfo.InstitutionName);
            db.AddInParameter(cmd, "TotalMarks", DbType.Decimal, stdEducationInfo.TotalMarks);
            db.AddInParameter(cmd, "ObtainedMarks", DbType.Decimal, stdEducationInfo.ObtainedMarks);
            db.AddInParameter(cmd, "Division", DbType.String, stdEducationInfo.Division);
            db.AddInParameter(cmd, "TotalCGPA", DbType.Decimal, stdEducationInfo.TotalCGPA);
            db.AddInParameter(cmd, "ObtainedCGPA", DbType.Decimal, stdEducationInfo.ObtainedCGPA);
            db.AddInParameter(cmd, "StudentID", DbType.Int32, stdEducationInfo.StudentID);
            db.AddInParameter(cmd, "AddressID", DbType.Int32, stdEducationInfo.AddressID);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, stdEducationInfo.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, stdEducationInfo.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, stdEducationInfo.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, stdEducationInfo.ModifiedDate);

            return db;
        }

        private IRowMapper<StdEducationInfo> GetMaper()
        {
            IRowMapper<StdEducationInfo> mapper = MapBuilder<StdEducationInfo>.MapAllProperties()
            .Map(m => m.StdEducationInfoID).ToColumn("StdEducationInfoID")
            .Map(m => m.DregreeName).ToColumn("DregreeName")
            .Map(m => m.GroupName).ToColumn("GroupName")
            .Map(m => m.InstitutionName).ToColumn("InstitutionName")
            .Map(m => m.TotalMarks).ToColumn("TotalMarks")
            .Map(m => m.ObtainedMarks).ToColumn("ObtainedMarks")
            .Map(m => m.Division).ToColumn("Division")
            .Map(m => m.TotalCGPA).ToColumn("TotalCGPA")
            .Map(m => m.ObtainedCGPA).ToColumn("ObtainedCGPA")
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.AddressID).ToColumn("AddressID")
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
