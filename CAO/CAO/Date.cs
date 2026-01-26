using System;
namespace Common 
{
    public class Date:IConvertible
    {
        private DateTime _d1;

        public Date(DateTime dateTime)
        {
            _d1 = dateTime;
        }

        public static Date operator +(Date date1, Date date2)
        {
            Date result = new Date(DateTime.Now);

            //Now, get the original DateTime Type of C#
            DateTime firstDate = Convert.ToDateTime(date1);
            DateTime secondDate = Convert.ToDateTime(date2);

            //result = firstDate.Add()
            return result;
        }

        public static bool operator <(Date date1, Date date2)
        {
            bool flag = false;


            //Now, get the original DateTime Type of C#
            DateTime firstDate = Convert.ToDateTime(date1);
            DateTime secondDate = Convert.ToDateTime(date2);

            //Now compare the two DateTime variables and assign the flag to true 
            //if the first date is smaller than the second date
            int result = DateTime.Compare(firstDate, secondDate);
            if (result < 0)
            {
                flag = true;
            }
            return flag;
        }

        public static bool operator >(Date date1, Date date2)
        {
            bool flag = false;

            //Now, get the original DateTime Type of C#
            DateTime firstDate = Convert.ToDateTime(date1);
            DateTime secondDate = Convert.ToDateTime(date2);

            //Now compare the two DateTime variables and assign the flag to true 
            //if the first date is Greater than the second date
            int result = DateTime.Compare(firstDate, secondDate);
            if (result > 0)
            {
                flag = true;
            }
            return flag;
        }

        public static bool operator <=(Date date1, Date date2)
        {
            bool flag = false;

            //Now, get the original DateTime Type of C#
            DateTime firstDate = Convert.ToDateTime(date1);
            DateTime secondDate = Convert.ToDateTime(date2);

            //Now compare the two DateTime variables and assign the flag to true 
            //if the first date is Greater than the second date
            int result = DateTime.Compare(firstDate, secondDate);
            if (result <= 0)
            {
                flag = true;
            }

            return flag;
        }

        public static bool operator >=(Date date1, Date date2)
        {
            bool flag = false;

            //Now, get the original DateTime Type of C#
            DateTime firstDate = Convert.ToDateTime(date1);
            DateTime secondDate = Convert.ToDateTime(date2);

            //Now compare the two DateTime variables and assign the flag to true 
            //if the first date is Greater than the second date
            int result = DateTime.Compare(firstDate, secondDate);
            if (result >= 0)
            {
                flag = true;
            }
            return flag;
        }

        public static bool operator ==(Date date1, Date date2)
        {
            bool flag = false;

            //Now, get the original DateTime Type of C#
            DateTime firstDate = Convert.ToDateTime(date1);
            DateTime secondDate = Convert.ToDateTime(date2);

            //Now compare the two DateTime variables and assign the flag to true 
            //if the first date is Greater than the second date
            int result = DateTime.Compare(firstDate, secondDate);
            if (result == 0)
            {
                flag = true;
            }
            return flag;
        }

        public static bool operator !=(Date date1, Date date2)
        {
            bool flag = false;

            //Now, get the original DateTime Type of C#
            DateTime firstDate = Convert.ToDateTime(date1);
            DateTime secondDate = Convert.ToDateTime(date2);

            //Now compare the two DateTime variables and assign the flag to true 
            //if the first date is Greater than the second date
            int result = DateTime.Compare(firstDate, secondDate);
            if (result != 0)
            {
                flag = true;
            }
            return flag;
        }

        

        #region IConvertible Members

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(_d1);
        }

        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        //public DateTime ToDateTime(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        #endregion
    }//end oc class Date
}//End of namespace
