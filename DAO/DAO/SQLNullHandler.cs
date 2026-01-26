using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class SQLNullHandler
    {
        #region Declaration
		private IDataReader _reader;
        private DataTable _dataTable;
        #endregion

        #region Constructor
        public  SQLNullHandler()
        {
        }

        public SQLNullHandler(IDataReader reader)
		{
			_reader = reader;
		}
		public SQLNullHandler(DataTable dataTable)
		{
		    _dataTable = dataTable;
		}
        #endregion

		#region Get Null value
		public static object GetNullValue(int Value)
		{
			if(Value==0)
			{
                return null;
            }
			else
			{
                return Value;
            }
		}

        public static object GetNullValue(double Value)
        {
            if (Value == 0)
            { 
                return null; 
            }
            else
            { 
                return Value; 
            }
        }

        public static object GetNullValue(decimal Value)
        {
            if (Value == 0)
            { 
                return null; 
            }
            else
            { 
                return Value; 
            }
        }

		public static object GetNullValue(DateTime Value)
		{
			if(DateTime.MinValue==Value)
			{
                return null;
            }
			else
			{
                return Value;
            }
		}
		
		public static object GetNullValue(string Value)
		{
			if(Value.Length<=0)
			{
                return null;
            }
			else
			{
                return Value;
            }
		}
		#endregion

		#region Default Null Values
		public IDataReader  Reader
		{
			get{return _reader;}
		}

		public bool IsNull(int index)
		{
			return _reader.IsDBNull(index);
		}

		public int GetInt32(int i)
		{
			return _reader.IsDBNull(i)? 0 : _reader.GetInt32(i);
		}

		public byte GetByte(int i)
		{
			return _reader.IsDBNull(i)? (byte)0 : _reader.GetByte(i);
		}

		public decimal GetDecimal(int i)
		{
			return _reader.IsDBNull(i)? 0 : _reader.GetDecimal(i);
		}

		public long GetInt64(int i)
		{
			return _reader.IsDBNull(i)? (long) 0 : _reader.GetInt64(i);
		}

		public double GetDouble(int i)
		{
			return _reader.IsDBNull(i)? 0 : _reader.GetDouble(i);
		}

		public bool GetBoolean(int i)
		{
			return _reader.IsDBNull(i)? false : _reader.GetBoolean(i);
		}

		public Guid GetGuid(int i)
		{
			return _reader.IsDBNull(i)? Guid.Empty : _reader.GetGuid(i);
		}

		public DateTime GetDateTime(int i)
		{
			return _reader.IsDBNull(i)? DateTime.MinValue : _reader.GetDateTime(i);
		}

		public float GetFloat(int i)
		{
			return _reader.IsDBNull(i)? 0 : _reader.GetFloat(i);
		}

		public string GetString(int i)
		{
			return _reader.IsDBNull(i)? null : _reader.GetString(i);
		}

		public char GetChar(int i)
		{
			return _reader.IsDBNull(i)? '\0' : _reader.GetChar(i);
		}
		

		public short GetInt16(String sFieldName)
		{
            if (_reader[sFieldName]==null)
            {
                throw new Exception("Invalid column name:" + sFieldName+".");
            }
            return (_reader[sFieldName] == DBNull.Value) ? (short)0 : _reader.GetInt16(_reader.GetOrdinal(sFieldName));
		}
		
		public int GetInt32(String sFieldName)
		{
            return (_reader[sFieldName] == DBNull.Value) ? 0 : _reader.GetInt32(_reader.GetOrdinal(sFieldName));
		}

		public byte GetByte(String sFieldName)
		{
            return _reader[sFieldName] == DBNull.Value ? byte.MinValue : _reader.GetByte(_reader.GetOrdinal(sFieldName));
		}

		public long GetInt64(String sFieldName)
		{
            return (_reader[sFieldName] == DBNull.Value) ? 0L : _reader.GetInt64(_reader.GetOrdinal(sFieldName));
		}
		
		public Double GetDouble(String sFieldName)
		{
            return (_reader[sFieldName] == DBNull.Value) ? 0.0D : _reader.GetDouble(_reader.GetOrdinal(sFieldName));
		}

        public float GetFloat(String sFieldName)
        {
            return (_reader[sFieldName] == DBNull.Value) ? 0.0F : _reader.GetFloat(_reader.GetOrdinal(sFieldName));
        }

        public decimal GetDecimal(String sFieldName)
        {
            return (_reader[sFieldName] == DBNull.Value) ? 0.0M : _reader.GetDecimal(_reader.GetOrdinal(sFieldName));
        }

		public bool GetBoolean(String sFieldName)
		{
            return (_reader[sFieldName] == DBNull.Value) ? false : _reader.GetBoolean(_reader.GetOrdinal(sFieldName));
		}

		public DateTime GetDateTime(String sFieldName)
		{
            return (_reader[sFieldName] == DBNull.Value) ? DateTime.MinValue : _reader.GetDateTime(_reader.GetOrdinal(sFieldName));
		}

		public char GetChar(String sFieldName)
		{
            return (_reader[sFieldName] == DBNull.Value) ? char.MinValue : _reader.GetChar(_reader.GetOrdinal(sFieldName));//'\0'
		}

		public string GetString(String sFieldName)
		{
            return (_reader[sFieldName] == DBNull.Value) ? string.Empty : _reader.GetString(_reader.GetOrdinal(sFieldName)).Trim();
		}
        
        #endregion
    }
}
