using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonUtility
{
    public static class UtilityManager
    {

        public static string randomStringGenerate(string prefix, int size, string suffix)
        {
            string allowedChars = "";
            allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "1,2,3,4,5,6,7,8,9,0";
            allowedChars += "@,#,$,*";

            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string randomString = "";
            string temp = "";

            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                randomString += temp;
            }
            randomString = prefix + randomString + suffix;
            return randomString;
        }
        
        public static string LoginId(string prefix, int size, string suffix)
        {
            string allowedChars = "";           
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";           

            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string randomString = "";
            string temp = "";

            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                randomString += temp;
            }
            randomString = prefix + randomString + suffix;
            return randomString;
        }

        public static string UserPassword(string prefix, int size, string suffix)
        {
            string allowedChars = "";
            allowedChars += "A,B,E,F,G,H,K,M,N,P,R,S,T,W,X,Y,Z,";  
            allowedChars += "1,2,3,4,5,6,7,8,9";

            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string randomString = "";
            string temp = "";

            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                randomString += temp;
            }
            randomString = prefix + randomString + suffix;
            return randomString;
        }
        
        public static string CreateAccountNumber(string number)
        {
            int length = 16;
            int different = length - number.Length;
            string tempZero = string.Empty;
            for (int j = 0; j < different; j++)
            {
                tempZero += "0";
            }
            return tempZero + number;
        }

        public static decimal ConvertEmptyToDecimal(string textContent)
        {
            return textContent != string.Empty ? Convert.ToDecimal(textContent) : 0;
        }

        public static DateTime ConvertEmptyToDateTime(string textContent)
        {
            return textContent != string.Empty ? Convert.ToDateTime(textContent) : DateTime.Now;
        }

        public static string GetVoucherRefNumber(string prefix, string suffix, string companyName, string voucherType, int length, int maxId)
        {
            int n = 0;
            n = length - prefix.Length - suffix.Length - (companyName.Length > 3 ? 3 : companyName.Length) - voucherType.Length - maxId.ToString().Length;

            string refNumber = string.Empty;
            refNumber = prefix + (companyName.Length > 3 ? companyName.Substring(0, 3) : companyName) + voucherType + maxId;
            refNumber += randomStringGenerate("", n, suffix);

            return refNumber;
        }

        public static string ConvertInWord(decimal amount)
        {
            return  Spell.SpellAmount.comma(amount);
        }

        public static string GetVoucherRefNumber(string prefix, string sp, string voucherType, int voucherID, DateTime voucherDate, int lastLength)
        {
            string voucherNumber = string.Empty;
            voucherNumber += prefix + sp + voucherDate.ToString().Split(' ')[0].ToString().Replace("/", "") + sp.ToString() + voucherType + sp.ToString() + GenerateLastNumber(voucherID, lastLength);
            return voucherNumber;
        }

        public static string GenerateLastNumber(int voucherID, int lastLength)
        {
            string newNumber = (voucherID + 1).ToString();
            int length = lastLength - newNumber.Length;
            string tempString = string.Empty;
            for (int i = 0; i < length; i++)
            {
                tempString += "0";
            }
            return tempString + newNumber;
        }

        public static string GetAdmissionFormPurchaseSL(int count)
        {
            Random rnd = new Random();
            string sl = string.Empty;

            string r1 = rnd.Next(1, 99).ToString().PadLeft(2, '0');
            Thread.Sleep(10);
            string r2 = rnd.Next(1, 99).ToString().PadLeft(2, '0');

            return sl = r1 +
                   DateTime.Now.Day.ToString().PadLeft(2, '0') +
                   DateTime.Now.Month.ToString().PadLeft(2, '0') +
                   DateTime.Now.Year.ToString().Remove(0, 2).PadLeft(2, '0') +
                   r2 +
                   count.ToString().PadLeft(5, '0');
        }
        
        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        #region Password related function
        public static string Encrypt(string sText)
        {
            int i = 0;
            string sEncrypt = "", sKey = "Cel.Admin"; //sKey="cel.abracadabra";
            char cTextChar, cKeyChar;
            char[] cTextData, cKey;

            //Save Length of Pass
            sText = (char)(sText.Length) + sText;

            //Pad Password with space upto 10 Characters
            if (sText.Length < 10)
            {
                sText = sText + sText.PadRight((10 - sText.Length), ' ');
            }
            cTextData = sText.ToCharArray();

            //Make the key big enough
            while (sKey.Length < sText.Length)
            {
                sKey = sKey + sKey;
            }
            sKey = sKey.Substring(0, sText.Length);
            cKey = sKey.ToCharArray();

            //Encrypting Data
            for (i = 0; i < sText.Length; i++)
            {
                cTextChar = (char)cTextData.GetValue(i);
                cKeyChar = (char)cKey.GetValue(i);
                sEncrypt = sEncrypt + IntToHex((int)(cTextChar) ^ (int)(cKeyChar));
            }

            return sEncrypt;
        }

        public static string Decrypt(string sText)
        {
            int j = 0, i = 0, nLen = 0;
            string sTextByte = "", sDecrypt = "", sKey = "Cel.Admin"; //sKey="cel.abracadabra";//
            char[] cTextData, cKey;
            char cTextChar, cKeyChar;

            //Taking Lenght, half of Encrypting data  
            nLen = sText.Length / 2;

            //Making key is big Enough
            while (sKey.Length < nLen)
            {
                sKey = sKey + sKey;
            }
            sKey = sKey.Substring(0, nLen);
            cKey = sKey.ToCharArray();
            cTextData = sText.ToCharArray();

            //Decripting data
            for (i = 0; i < nLen; i++)
            {
                sTextByte = "";
                for (j = i * 2; j < (i * 2 + 2); j++)
                {
                    sTextByte = sTextByte + cTextData.GetValue(j).ToString();
                }
                cTextChar = (char)HexToInt(sTextByte);
                cKeyChar = (char)cKey.GetValue(i);
                sDecrypt = sDecrypt + (char)((int)(cKeyChar) ^ (int)(cTextChar));
            }

            //Taking real password
            cTextData = sDecrypt.ToCharArray();
            sDecrypt = "";
            i = (int)(char)cTextData.GetValue(0);
            for (j = 1; j <= i; j++)
            {
                sDecrypt = sDecrypt + cTextData.GetValue(j).ToString();
            }

            return sDecrypt;
        }

        private static string IntToHex(int nIntData)
        {
            return Convert.ToString(nIntData, 16).PadLeft(2, '0');
        }

        private static int HexToInt(string sHexData)
        {
            return Convert.ToInt32(sHexData, 16);
        }
        #endregion
    }
}
