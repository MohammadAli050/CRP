using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility
{
    public static class DataTableMethods
    {
        // Filter DataTable By Like Operation
        // DataTable LeaveDT = new DataTable();
        //DataRow[] tempDataRows= dt.Select("LeaveType Like '%" + "Leave" + "%'");
        //if (tempDataRows.Length > 0)
        //    LeaveDT = tempDataRows.CopyToDataTable();

        #region Filter a datatable

        //string expression = "ShortName NOT ='" + "NULL" + "' AND ShortName NOT ='" + "0" + "' AND ShortName NOT ='" + "" + "'";

        public static DataTable FilterDataTable(DataTable dt, string expression)
        {
            DataTable dt1 = new DataTable();

            if (dt.Rows.Count > 0)
            {

                DataRow[] filteredrows = dt.Select(expression);
                if (filteredrows.Length > 0)
                    dt1 = filteredrows.CopyToDataTable();
                else
                    dt1.Clear();
            }

            return dt1;
        }

        #endregion

        #region List To DataTable
        public static DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        #endregion

        #region DataTable To List
        //Calling it and converting it to List. 
        //List< Student > studentDetails = new List< Student >();  
        //studentDetails = ConvertDataTable< Student >(dt);

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    T item = GetItem<T>(row);
                    data.Add(item);
                }
                catch (Exception ex)
                {
                }

            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        #endregion

        #region DataLength Coloumn Add For Sorting
        public static DataTable AddNewColoumnForLengthSorting(DataTable dt, string LengthField)
        {
            try
            {
                /// Add Column
                DataColumn dc = new DataColumn();
                dc.ColumnName = "Length";
                dc.DataType = typeof(int);
                dt.Columns.Add(dc);

                //Add Row Value in new column 
                foreach (DataRow row in dt.Rows)
                {
                    int length = Convert.ToInt32(row[LengthField].ToString().Length);

                    row["Length"] = length;
                }


                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        #endregion

        #region Remove Column From a datatable

        //string[] Filter = new string[4];

        //                Filter[0] = "AcacalSectionId";
        //                Filter[1] = "ExamRoutineId";
        //                Filter[2] = "DayNo";
        //                Filter[3] = "TImeSlotNo";

        //                dt = CommonUtility.DataTableMethods.ColumnRemoveFromDataTable(dt, Filter);

        public static DataTable ColumnRemoveFromDataTable(DataTable dt, string[] ColumnList)
        {

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < ColumnList.Length; i++)
                {

                    string value = ColumnList[i].ToString();

                    dt.Columns.Remove(value);

                }
            }

            return dt;
        }

        #endregion

        #region Data Table To JSON

        public static string DataTableToJsonConvert(DataTable table)
        {
            string jsonString = string.Empty;
            jsonString = JsonConvert.SerializeObject(table);
            return jsonString;
        }

        #endregion

        #region Get Distinct Value From DataTable (Single Column)
        public static DataTable GetDistinct(DataTable SourceTable, string FieldName)
        {

            DataTable dt = new DataTable(SourceTable.TableName);
            dt.Columns.Add(FieldName, SourceTable.Columns[FieldName].DataType);
            // Loop each row & compare each value with one another  
            // Add it to datatable if the values are mismatch  
            object LastValue = null;
            foreach (DataRow dr in SourceTable.Select("", FieldName))
            {
                if (LastValue == null || !(ColumnEqual(LastValue, dr[FieldName])))
                {
                    LastValue = dr[FieldName];
                    dt.Rows.Add(new object[] { LastValue });
                }
            }
            return dt;
        }

        public static bool ColumnEqual(object A, object B)
        {
            // Compares two values to see if they are equal. Also compares DBNULL.Value.             
            if (A == DBNull.Value && B == DBNull.Value) //  both are DBNull.Value  
                return true;
            if (A == DBNull.Value || B == DBNull.Value) //  only one is BNull.Value  
                return false;
            return (A.Equals(B)); // value type standard comparison  
        }
        #endregion

        #region Select Distinct Value From Datatable (Multiple Column)

        //DataTable distinctStudents = dt.DefaultView.ToTable(true, "StudentProgram", "Batch", "Roll", "FullName", "Email", "Phone");


        //var distinctValues = dt.AsEnumerable()
        //                       .Select(row => new
        //                       {
        //                           AcaCal_SectionID = row.Field<int>("AcaCal_SectionID"),
        //                           RegisteredStudent = row.Field<int>("RegisteredStudent"),
        //                           ProgramName = row.Field<string>("ProgramName"),
        //                           FormalCode = row.Field<string>("FormalCode"),
        //                           VersionCode = row.Field<string>("VersionCode"),
        //                           Title = row.Field<string>("Title"),
        //                           Credits = row.Field<decimal>("Credits"),
        //                           SectionName = row.Field<string>("SectionName"),
        //                           RoomOne = row.Field<string>("RoomOne"),
        //                           RoomTwo = row.Field<string>("RoomTwo"),
        //                           TimeSlotOne = row.Field<string>("TimeSlotOne"),
        //                           TimeSlotTwo = row.Field<string>("TimeSlotTwo"),
        //                           DayOne = row.Field<string>("DayOne"),
        //                           DayTwo = row.Field<string>("DayTwo")
        //                       })
        //                   .Distinct();



        #endregion

        //if (dt != null && dt.Rows.Count > 0)
        //       {
        //           decimal TotalBill = 0, TotalPaid = 0;
        //           foreach (DataRow row in dt.Select("InstallmentNo<=2"))
        //           {
        //               TotalBill = TotalBill + Convert.ToDecimal(row["BillingAmount"]);
        //               TotalPaid = TotalPaid + Convert.ToDecimal(row["PaidAmount"]);
        //           }

        //           if (TotalBill <= TotalPaid && TotalPaid > 0)
        //               IsClear = true;


        //       }

        #region Group By And Count

        //var query = from row in dt.AsEnumerable()
        //            group row by row.Field<string>("Grade") into sales
        //            select new
        //            {
        //                Grade = sales.Key,
        //                TotalNo = sales.Count()
        //            };

        #endregion
    }
}
