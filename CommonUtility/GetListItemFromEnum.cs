using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
namespace CommonUtility
{
    public static class GetListItemFromEnum 
    {
        public static ListItemCollection GetListItems(Type enumType)
       {
           ListItemCollection items = new ListItemCollection();

           items.Add(new ListItem("-Select-", "0"));

           foreach (int value in Enum.GetValues(enumType))
           {
               items.Add(new ListItem(Enum.GetName(enumType, value),value.ToString()));
           } 

           return items;
       }
    }
}
