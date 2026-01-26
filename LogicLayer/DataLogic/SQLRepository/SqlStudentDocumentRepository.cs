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
    public partial class SqlStudentDocumentRepository : IStudentDocumentRepository
    {

        Database db = null;

        private string sqlInsert = "StudentDocumentInsert";
        private string sqlUpdate = "StudentDocumentUpdate";
        private string sqlDelete = "StudentDocumentDeleteById";
        private string sqlGetById = "StudentDocumentGetById";
        private string sqlGetAll = "StudentDocumentGetAll";
               
        public int Insert(StudentDocument studentdocument)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, studentdocument, isInsert);
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

        public bool Update(StudentDocument studentdocument)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, studentdocument, isInsert);

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

        public StudentDocument GetById(int? id)
        {
            StudentDocument _studentdocument = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDocument> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDocument>(sqlGetById, rowMapper);
                _studentdocument = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentdocument;
            }

            return _studentdocument;
        }

        public List<StudentDocument> GetAll()
        {
            List<StudentDocument> studentdocumentList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDocument> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDocument>(sqlGetAll, mapper);
                IEnumerable<StudentDocument> collection = accessor.Execute();

                studentdocumentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentdocumentList;
            }

            return studentdocumentList;
        }

        public StudentDocument GetByPersonIdImageType(int personId, int ImageType)
        {
            StudentDocument _studentDocument = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDocument> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDocument>("StudentDocumentGetByPersonIdImageType", rowMapper);
                _studentDocument = accessor.Execute(personId, ImageType).FirstOrDefault();

            }
            catch (Exception ex)
            {
                return _studentDocument;
            }

            return _studentDocument;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StudentDocument studentdocument, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, studentdocument.Id);
            }

            	
		db.AddInParameter(cmd,"PersonId",DbType.Int32,studentdocument.PersonId);
		db.AddInParameter(cmd,"ImageType",DbType.Int32,studentdocument.ImageType);
		db.AddInParameter(cmd,"PhotoPath",DbType.String,studentdocument.PhotoPath);
		db.AddInParameter(cmd,"Attribute1",DbType.String,studentdocument.Attribute1);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,studentdocument.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,studentdocument.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,studentdocument.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,studentdocument.ModifiedDate);
            
            return db;
        }

        private IRowMapper<StudentDocument> GetMaper()
        {
            IRowMapper<StudentDocument> mapper = MapBuilder<StudentDocument>.MapAllProperties()

       	   .Map(m => m.Id).ToColumn("Id")
		.Map(m => m.PersonId).ToColumn("PersonId")
		.Map(m => m.ImageType).ToColumn("ImageType")
		.Map(m => m.PhotoPath).ToColumn("PhotoPath")
		.Map(m => m.Attribute1).ToColumn("Attribute1")
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

