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
    public partial class SqlTabulationResultRemarksRepository : ITabulationResultRemarksRepository
    {

        Database db = null;

        private string sqlInsert = "TabulationResultRemarksInsert";
        private string sqlUpdate = "TabulationResultRemarksUpdate";
        private string sqlDelete = "TabulationResultRemarksDelete";
        private string sqlGetById = "TabulationResultRemarksGetById";
        private string sqlGetAll = "TabulationResultRemarksGetAll";
        private string sqlGetAllByProgramSessionBatchStudentId = "TabulationResultRemarksGetByProgramIdSessionIdBatchIdStudentId";

        public int Insert(TabulationResultRemarks tabulationresultremarks)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, tabulationresultremarks, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "Id");

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

        public bool Update(TabulationResultRemarks tabulationresultremarks)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, tabulationresultremarks, isInsert);

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

                db.AddInParameter(cmd, "Id", DbType.Int32, id);
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

        public TabulationResultRemarks GetById(int? id)
        {
            TabulationResultRemarks _tabulationresultremarks = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TabulationResultRemarks> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TabulationResultRemarks>(sqlGetById, rowMapper);
                _tabulationresultremarks = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _tabulationresultremarks;
            }

            return _tabulationresultremarks;
        }

        public List<TabulationResultRemarks> GetAll()
        {
            List<TabulationResultRemarks> tabulationresultremarksList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TabulationResultRemarks> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TabulationResultRemarks>(sqlGetAll, mapper);
                IEnumerable<TabulationResultRemarks> collection = accessor.Execute();

                tabulationresultremarksList = collection.ToList();
            }

            catch (Exception ex)
            {
                return tabulationresultremarksList;
            }

            return tabulationresultremarksList;
        }

        public List<TabulationResultRemarksDOT> GetAllByProgramSessionBatchStudentId(int ProgramId, int SessionId, int BatchId, int StudentId)
        {
            List<TabulationResultRemarksDOT> tabulationresultremarksList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TabulationResultRemarksDOT> mapper = GetTabulationResultRemarksDOTMaper();

                var accessor = db.CreateSprocAccessor<TabulationResultRemarksDOT>(sqlGetAllByProgramSessionBatchStudentId, mapper);
                IEnumerable<TabulationResultRemarksDOT> collection = accessor.Execute(ProgramId, SessionId, BatchId, StudentId);

                tabulationresultremarksList = collection.ToList();
            }

            catch (Exception ex)
            {
                return tabulationresultremarksList;
            }

            return tabulationresultremarksList;
        }

        #region Mapper

        private Database addParam(Database db, DbCommand cmd, TabulationResultRemarks tabulationresultremarks, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, tabulationresultremarks.Id);
            }


            db.AddInParameter(cmd, "StudentId", DbType.Int32, tabulationresultremarks.StudentId);
            db.AddInParameter(cmd, "AcaCalId", DbType.Int32, tabulationresultremarks.AcaCalId);
            db.AddInParameter(cmd, "TabulationRemarks", DbType.String, tabulationresultremarks.TabulationRemarks);
            db.AddInParameter(cmd, "ResultRemarks", DbType.String, tabulationresultremarks.ResultRemarks);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, tabulationresultremarks.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, tabulationresultremarks.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, tabulationresultremarks.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, tabulationresultremarks.ModifiedDate);

            return db;
        }

        private IRowMapper<TabulationResultRemarks> GetMaper()
        {
            IRowMapper<TabulationResultRemarks> mapper = MapBuilder<TabulationResultRemarks>.MapAllProperties()

            .Map(m => m.Id).ToColumn("Id")
            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.AcaCalId).ToColumn("AcaCalId")
            .Map(m => m.TabulationRemarks).ToColumn("TabulationRemarks")
            .Map(m => m.ResultRemarks).ToColumn("ResultRemarks")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")

            .Build();

            return mapper;
        }

        private IRowMapper<TabulationResultRemarksDOT> GetTabulationResultRemarksDOTMaper()
        {
            IRowMapper<TabulationResultRemarksDOT> mapper = MapBuilder<TabulationResultRemarksDOT>.MapAllProperties()

            .Map(m => m.Id).ToColumn("Id")
            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.AcaCalId).ToColumn("AcaCalId")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.Name).ToColumn("FullName")
            .Map(m => m.CGPA).ToColumn("CGPA")
            .Map(m => m.SessionName).ToColumn("SessionName")
            .Map(m => m.TabulationRemarks).ToColumn("TabulationRemarks")
            .Map(m => m.ResultRemarks).ToColumn("ResultRemarks") 

            .Build();

            return mapper;
        }


        #endregion

    }
}

