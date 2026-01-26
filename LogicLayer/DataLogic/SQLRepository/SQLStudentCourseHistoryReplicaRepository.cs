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
    public partial class SQLStudentCourseHistoryReplicaRepository : IStudentCourseHistoryReplicaRepository
    {

        Database db = null;

        private string sqlInsert = "StudentCourseHistoryReplicaInsert";
        private string sqlUpdate = "StudentCourseHistoryReplicaUpdate";
        private string sqlDelete = "StudentCourseHistoryReplicaDelete";
        private string sqlGetById = "StudentCourseHistoryReplicaGetById";
        private string sqlGetAll = "StudentCourseHistoryReplicaGetAll";
        private string sqlGetAllByCourseHistoryID = "StudentCourseHistoryReplicaGetAllByCourseHistoryID";
               
        public int Insert(StudentCourseHistoryReplica studentcoursehistoryreplica)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, studentcoursehistoryreplica, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ID");

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

        public bool Update(StudentCourseHistoryReplica studentcoursehistoryreplica)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, studentcoursehistoryreplica, isInsert);

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

                db.AddInParameter(cmd, "ID", DbType.Int32, id);
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

        public StudentCourseHistoryReplica GetById(int? id)
        {
            StudentCourseHistoryReplica _studentcoursehistoryreplica = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourseHistoryReplica> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCourseHistoryReplica>(sqlGetById, rowMapper);
                _studentcoursehistoryreplica = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentcoursehistoryreplica;
            }

            return _studentcoursehistoryreplica;
        }

        public List<StudentCourseHistoryReplica> GetAll()
        {
            List<StudentCourseHistoryReplica> studentcoursehistoryreplicaList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourseHistoryReplica> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCourseHistoryReplica>(sqlGetAll, mapper);
                IEnumerable<StudentCourseHistoryReplica> collection = accessor.Execute();

                studentcoursehistoryreplicaList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentcoursehistoryreplicaList;
            }

            return studentcoursehistoryreplicaList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StudentCourseHistoryReplica studentcoursehistoryreplica, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ID", DbType.Int32, studentcoursehistoryreplica.ID);
            }

            	
		db.AddInParameter(cmd,"StudentCourseHistoryID",DbType.Int32,studentcoursehistoryreplica.StudentCourseHistoryID);
		db.AddInParameter(cmd,"StudentID",DbType.Int32,studentcoursehistoryreplica.StudentID);
		db.AddInParameter(cmd,"CalCourseProgNodeID",DbType.Int32,studentcoursehistoryreplica.CalCourseProgNodeID);
		db.AddInParameter(cmd,"AcaCalSectionID",DbType.Int32,studentcoursehistoryreplica.AcaCalSectionID);
		db.AddInParameter(cmd,"RetakeNo",DbType.Int32,studentcoursehistoryreplica.RetakeNo);
		db.AddInParameter(cmd,"ObtainedTotalMarks",DbType.Decimal,studentcoursehistoryreplica.ObtainedTotalMarks);
		db.AddInParameter(cmd,"ObtainedGPA",DbType.Decimal,studentcoursehistoryreplica.ObtainedGPA);
		db.AddInParameter(cmd,"ObtainedGrade",DbType.String,studentcoursehistoryreplica.ObtainedGrade);
		db.AddInParameter(cmd,"GradeId",DbType.Int32,studentcoursehistoryreplica.GradeId);
		db.AddInParameter(cmd,"CourseStatusID",DbType.Int32,studentcoursehistoryreplica.CourseStatusID);
		db.AddInParameter(cmd,"CourseStatusDate",DbType.DateTime,studentcoursehistoryreplica.CourseStatusDate);
		db.AddInParameter(cmd,"AcaCalID",DbType.Int32,studentcoursehistoryreplica.AcaCalID);
		db.AddInParameter(cmd,"CourseID",DbType.Int32,studentcoursehistoryreplica.CourseID);
		db.AddInParameter(cmd,"VersionID",DbType.Int32,studentcoursehistoryreplica.VersionID);
		db.AddInParameter(cmd,"CourseCredit",DbType.Decimal,studentcoursehistoryreplica.CourseCredit);
		db.AddInParameter(cmd,"CompletedCredit",DbType.Decimal,studentcoursehistoryreplica.CompletedCredit);
		db.AddInParameter(cmd,"Node_CourseID",DbType.Int32,studentcoursehistoryreplica.Node_CourseID);
		db.AddInParameter(cmd,"NodeID",DbType.Int32,studentcoursehistoryreplica.NodeID);
		db.AddInParameter(cmd,"IsMultipleACUSpan",DbType.Boolean,studentcoursehistoryreplica.IsMultipleACUSpan);
		db.AddInParameter(cmd,"IsConsiderGPA",DbType.Boolean,studentcoursehistoryreplica.IsConsiderGPA);
		db.AddInParameter(cmd,"CourseWavTransfrID",DbType.Int32,studentcoursehistoryreplica.CourseWavTransfrID);
		db.AddInParameter(cmd,"SemesterNo",DbType.Int32,studentcoursehistoryreplica.SemesterNo);
		db.AddInParameter(cmd,"YearNo",DbType.Int32,studentcoursehistoryreplica.YearNo);
		db.AddInParameter(cmd,"EqCourseHistoryId",DbType.Int32,studentcoursehistoryreplica.EqCourseHistoryId);
		db.AddInParameter(cmd,"Attribute1",DbType.String,studentcoursehistoryreplica.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,studentcoursehistoryreplica.Attribute2);
		db.AddInParameter(cmd,"Attribute3",DbType.String,studentcoursehistoryreplica.Attribute3);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,studentcoursehistoryreplica.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,studentcoursehistoryreplica.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,studentcoursehistoryreplica.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,studentcoursehistoryreplica.ModifiedDate);
		db.AddInParameter(cmd,"Remark",DbType.String,studentcoursehistoryreplica.Remark);
            
            return db;
        }

        private IRowMapper<StudentCourseHistoryReplica> GetMaper()
        {
            IRowMapper<StudentCourseHistoryReplica> mapper = MapBuilder<StudentCourseHistoryReplica>.MapAllProperties()

       	   .Map(m => m.ID).ToColumn("ID")
		.Map(m => m.StudentCourseHistoryID).ToColumn("StudentCourseHistoryID")
		.Map(m => m.StudentID).ToColumn("StudentID")
		.Map(m => m.CalCourseProgNodeID).ToColumn("CalCourseProgNodeID")
		.Map(m => m.AcaCalSectionID).ToColumn("AcaCalSectionID")
		.Map(m => m.RetakeNo).ToColumn("RetakeNo")
		.Map(m => m.ObtainedTotalMarks).ToColumn("ObtainedTotalMarks")
		.Map(m => m.ObtainedGPA).ToColumn("ObtainedGPA")
		.Map(m => m.ObtainedGrade).ToColumn("ObtainedGrade")
		.Map(m => m.GradeId).ToColumn("GradeId")
		.Map(m => m.CourseStatusID).ToColumn("CourseStatusID")
		.Map(m => m.CourseStatusDate).ToColumn("CourseStatusDate")
		.Map(m => m.AcaCalID).ToColumn("AcaCalID")
		.Map(m => m.CourseID).ToColumn("CourseID")
		.Map(m => m.VersionID).ToColumn("VersionID")
		.Map(m => m.CourseCredit).ToColumn("CourseCredit")
		.Map(m => m.CompletedCredit).ToColumn("CompletedCredit")
		.Map(m => m.Node_CourseID).ToColumn("Node_CourseID")
		.Map(m => m.NodeID).ToColumn("NodeID")
		.Map(m => m.IsMultipleACUSpan).ToColumn("IsMultipleACUSpan")
		.Map(m => m.IsConsiderGPA).ToColumn("IsConsiderGPA")
		.Map(m => m.CourseWavTransfrID).ToColumn("CourseWavTransfrID")
		.Map(m => m.SemesterNo).ToColumn("SemesterNo")
		.Map(m => m.YearNo).ToColumn("YearNo")
		.Map(m => m.EqCourseHistoryId).ToColumn("EqCourseHistoryId")
		.Map(m => m.Attribute1).ToColumn("Attribute1")
		.Map(m => m.Attribute2).ToColumn("Attribute2")
		.Map(m => m.Attribute3).ToColumn("Attribute3")
		.Map(m => m.CreatedBy).ToColumn("CreatedBy")
		.Map(m => m.CreatedDate).ToColumn("CreatedDate")
		.Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
		.Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
		.Map(m => m.Remark).ToColumn("Remark")
            
            .Build();

            return mapper;
        }
        #endregion



        public List<StudentCourseHistoryReplica> GetAllByCourseHistoryID(int id)
        {
            List<StudentCourseHistoryReplica> studentcoursehistoryreplicaList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourseHistoryReplica> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCourseHistoryReplica>(sqlGetAllByCourseHistoryID, mapper);
                IEnumerable<StudentCourseHistoryReplica> collection = accessor.Execute(id);

                studentcoursehistoryreplicaList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentcoursehistoryreplicaList;
            }

            return studentcoursehistoryreplicaList;
        }
    }
}

