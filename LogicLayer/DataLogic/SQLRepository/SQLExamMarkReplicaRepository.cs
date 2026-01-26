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
    public partial class SqlExamMarkReplicaRepository : IExamMarkReplicaRepository
    {

        Database db = null;

        private string sqlGetById = "ExamMarkReplicaGetById";
        private string sqlGetAll = "ExamMarkReplicaGetAll";

        private string sqlInsertByAcaCalAcaCalSecRoll = "ExamMarkReplicaInsertByAcaCalAcaCalSecRoll";

        public ExamMarkReplica GetById(int id)
        {
            ExamMarkReplica _exammarkreplica = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkReplica> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkReplica>(sqlGetById, rowMapper);
                _exammarkreplica = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _exammarkreplica;
            }

            return _exammarkreplica;
        }

        public List<ExamMarkReplica> GetAll()
        {
            List<ExamMarkReplica> exammarkreplicaList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkReplica> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkReplica>(sqlGetAll, mapper);
                IEnumerable<ExamMarkReplica> collection = accessor.Execute();

                exammarkreplicaList = collection.ToList();
            }

            catch (Exception ex)
            {
                return exammarkreplicaList;
            }

            return exammarkreplicaList;
        }
       
        #region Mapper
        private IRowMapper<ExamMarkReplica> GetMaper()
        {
            IRowMapper<ExamMarkReplica> mapper = MapBuilder<ExamMarkReplica>.MapAllProperties()

       	    .Map(m => m.SlNo).ToColumn("SlNo")
		    .Map(m => m.Id).ToColumn("Id")
		    .Map(m => m.CourseHistoryId).ToColumn("CourseHistoryId")
		    .Map(m => m.ExamId).ToColumn("ExamId")
		    .Map(m => m.Mark).ToColumn("Mark")
		    .Map(m => m.Status).ToColumn("Status")
		    .Map(m => m.IsFinalSubmit).ToColumn("IsFinalSubmit")
		    .Map(m => m.IsTransfer).ToColumn("IsTransfer")
		    .Map(m => m.Attribute1).ToColumn("Attribute1")
		    .Map(m => m.Attribute2).ToColumn("Attribute2")
		    .Map(m => m.Attribute3).ToColumn("Attribute3")
		    .Map(m => m.CreateBy).ToColumn("CreateBy")
		    .Map(m => m.CreatedDate).ToColumn("CreatedDate")
		    .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
		    .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            
            .Build();

            return mapper;
        }
        #endregion

        public int InsertByAcaCalAcaCalSecRoll(int acaCalId, int acaCalSecId, string roll)
        {
            int count = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsertByAcaCalAcaCalSecRoll);

                db.AddOutParameter(cmd, "TotalEntry", DbType.Int32, Int32.MaxValue);
                db.AddInParameter(cmd, "AcaCalID", DbType.Int32, acaCalId);
                db.AddInParameter(cmd, "AcaCalSecID", DbType.Int32, acaCalSecId);
                db.AddInParameter(cmd, "Roll", DbType.String, roll);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "TotalEntry");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out count);
                }
            }
            catch (Exception ex)
            {
                count = 0;
            }

            return count;
        }

    }
}

