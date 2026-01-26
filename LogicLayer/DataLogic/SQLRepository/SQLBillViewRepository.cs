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
    public partial class SqlBillViewRepository : IBillViewRepository
    {
        Database db = null;

        private string sqlInsert = "BillViewInsert";
        private string sqlUpdate = "BillViewUpdate";
        private string sqlDelete = "BillViewDelete";
        private string sqlDeleteByStdSessCrsVer = "BillViewDeleteByStdSessCrsVer";
        private string sqlGetById = "BillViewGetById";
        private string sqlGetAll = "BillViewGetAll";
        private string sqlGetByStudentSession = "BillViewGetByStudentSession";
        private string sqlGetByStudentAccountSession = "BillViewGetByStudentAccountSession";
        private string sqlGetByStudent = "BillViewGetByStudent";
        private string sqlGetByStudentAccountSessionCourseVersion = "BillViewGetByStudentAccountSessionCourseVersion";


        public int Insert(BillView billview)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, billview, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "BillViewId");

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

        public bool Update(BillView billview)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, billview, isInsert);

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

                db.AddInParameter(cmd, "BillViewId", DbType.Int32, id);
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

        public BillView GetById(int id)
        {
            BillView _billview = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<BillView> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<BillView>(sqlGetById, rowMapper);
                _billview = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _billview;
            }

            return _billview;
        }

        public List<BillView> GetAll()
        {
            List<BillView> billviewList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<BillView> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<BillView>(sqlGetAll, mapper);
                IEnumerable<BillView> collection = accessor.Execute();

                billviewList = collection.ToList();
            }

            catch (Exception ex)
            {
                return billviewList;
            }

            return billviewList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, BillView billview, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "BillViewId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "BillViewId", DbType.Int32, billview.BillViewId);
            }


            db.AddInParameter(cmd, "StudentId", DbType.Int32, billview.StudentId);
            db.AddInParameter(cmd, "CourseId", DbType.Int32, billview.CourseId);
            db.AddInParameter(cmd, "VersionId", DbType.Int32, billview.VersionId);
            db.AddInParameter(cmd, "TrimesterId", DbType.Int32, billview.TrimesterId);
            db.AddInParameter(cmd, "Amount", DbType.Int32, billview.Amount);
            db.AddInParameter(cmd, "AccountsID", DbType.Int32, billview.AccountsID);
            db.AddInParameter(cmd, "Purpose", DbType.String, billview.Purpose);
            db.AddInParameter(cmd, "AmountByCollectiveDiscount", DbType.String, billview.AmountByCollectiveDiscount);
            db.AddInParameter(cmd, "AmountByIterativeDiscount", DbType.String, billview.AmountByIterativeDiscount);

            return db;
        }

        private IRowMapper<BillView> GetMaper()
        {
            IRowMapper<BillView> mapper = MapBuilder<BillView>.MapAllProperties()

            .Map(m => m.BillViewId).ToColumn("BillViewId")
            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.CourseId).ToColumn("CourseId")
            .Map(m => m.VersionId).ToColumn("VersionId")
            .Map(m => m.TrimesterId).ToColumn("TrimesterId")
            .Map(m => m.Amount).ToColumn("Amount")
            .Map(m => m.Purpose).ToColumn("Purpose")
            .Map(m => m.AccountsID).ToColumn("AccountsID")
            .Map(m => m.AmountByCollectiveDiscount).ToColumn("AmountByCollectiveDiscount")
            .Map(m => m.AmountByIterativeDiscount).ToColumn("AmountByIterativeDiscount")
            .Build();

            return mapper;
        }
        #endregion

        public BillView GetBy(int studentId, int accountsID, int sessionId)
        {
            BillView _billview = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<BillView> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<BillView>(sqlGetByStudentAccountSession, rowMapper);
                _billview = accessor.Execute(studentId, accountsID, sessionId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                return _billview;
            }

            return _billview;
        }
        
        public BillView GetBy(int studentId, int accountsID, int sessionId, int courseId, int versionId)
        {
            BillView _billview = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<BillView> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<BillView>(sqlGetByStudentAccountSessionCourseVersion, rowMapper);
                _billview = accessor.Execute(studentId, accountsID, sessionId, courseId, versionId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                return _billview;
            }

            return _billview;
        }


        public List<BillView> GetBy(int studentId, int sessionId)
        {
            List<BillView> _billview = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<BillView> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<BillView>(sqlGetByStudentSession, rowMapper);
                _billview = accessor.Execute(studentId, sessionId).ToList();
            }
            catch (Exception ex)
            {
                return _billview;
            }

            return _billview;
        }

        public List<BillView> GetBy(int studentId)
        {
            List<BillView> _billview = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<BillView> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<BillView>(sqlGetByStudent, rowMapper);
                _billview = accessor.Execute(studentId).ToList();
            }
            catch (Exception ex)
            {
                return _billview;
            }

            return _billview;
        }

        public bool Delete(int studentID, int sessionId, int courseID, int versionID)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlDeleteByStdSessCrsVer);

                db.AddInParameter(cmd, "StudentID", DbType.Int32, studentID);
                db.AddInParameter(cmd, "SessionId", DbType.Int32, sessionId);
                db.AddInParameter(cmd, "CourseID", DbType.Int32, courseID);
                db.AddInParameter(cmd, "VersionID", DbType.Int32, versionID);
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
    }
}

