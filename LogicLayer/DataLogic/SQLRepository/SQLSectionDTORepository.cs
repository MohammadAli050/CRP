using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLSectionDTORepository : ISectionDTORepository
    {

        Database db = null;


        private string sqlGetAll = "AcademicCalenderSectionDTOSelect";

        public List<SectionDTO> GetAllBy(int acaCalId,  int programId, int courseId, int versionId)
        {
            List<SectionDTO> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<SectionDTO> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<SectionDTO>(sqlGetAll, mapper);
                IEnumerable<SectionDTO> collection = accessor.Execute(acaCalId, programId, courseId, versionId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        #region Mapper
        private IRowMapper<SectionDTO> GetMaper()
        {
            IRowMapper<SectionDTO> mapper = MapBuilder<SectionDTO>.MapAllProperties()
           .Map(m => m.AcaCalSectionID).ToColumn("AcaCal_SectionID")
           .Map(m => m.SectionName).ToColumn("SectionName")
           .Map(m => m.TimeSlot1).ToColumn("TimeSlot_1")
           .Map(m => m.DayOne).ToColumn("DayOne")
           .Map(m => m.TimeSlot2).ToColumn("TimeSlot_2")
           .Map(m => m.DayTwo).ToColumn("DayTwo")
           .Map(m => m.Faculty1).ToColumn("Faculty_1")
           .Map(m => m.Faculty2).ToColumn("Faculty_2")
           .Map(m => m.RoomNo1).ToColumn("RoomNo_1")
           .Map(m => m.RoomNo2).ToColumn("RoomNo_2")
           .Map(m => m.Capacity).ToColumn("Capacity")
           .Map(m => m.Occupied).ToColumn("Occupied")
           .Map(m => m.SectionGender).ToColumn("SectionGender")
           .Build();

            return mapper;
        }
        #endregion
    }
}
