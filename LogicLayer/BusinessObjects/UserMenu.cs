using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class UserMenu
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int UserId { get; set; }
        public Nullable<DateTime> ValidFrom { get; set; }
        public Nullable<DateTime> ValidTo { get; set; }
        public Nullable<int> AddRemove { get; set; }
        public Nullable<int> ProgramId { get; set; }
        public Nullable<int> DeptId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        #region Empty Custom Property
        public string MenuName { get; set; }

        public Menu Menu
        {
            get
            {
                Menu menu = MenuManager.GetById(MenuId);
                return menu;
            }
        }
        #endregion
    }
}
