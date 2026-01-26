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
    public partial class SQLCourseSectionRepository : ICourseSectionRepository
    {

        Database db = null;

        private string sqlInsert = "CourseSectionInsert";
        private string sqlUpdate = "CourseSectionUpdate";
        private string sqlDelete = "CourseSectionDelete";
        private string sqlGetById = "CourseSectionGetById";
        private string sqlGetAll = "CourseSectionGetAll";

        private string sqlGetAllSectionByCourseId = "CourseSectionByCourseId";


        public List<CourseSectionByCourseIdDTO> GetSectionByCourseId(int courseId)
        {
            List<CourseSectionByCourseIdDTO> coursesectionList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseSectionByCourseIdDTO> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CourseSectionByCourseIdDTO>(sqlGetAllSectionByCourseId, mapper);
                IEnumerable<CourseSectionByCourseIdDTO> collection = accessor.Execute(courseId);

                coursesectionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return coursesectionList;
            }

            return coursesectionList;
        }

       
        #region Mapper
        private IRowMapper<CourseSectionByCourseIdDTO> GetMaper()
        {
            IRowMapper<CourseSectionByCourseIdDTO> mapper = MapBuilder<CourseSectionByCourseIdDTO>.MapAllProperties()

            .Map(m => m.SectionId).ToColumn("SectionId")
            .Map(m => m.SectionName).ToColumn("SectionName")

            .Build();

            return mapper;
        }

        #endregion

    }
}

