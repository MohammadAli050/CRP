using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace CommonUtility
{
    public  class DataCaching
    {
        public static bool IsEnabled
        {
            get
            {
                return true;
            }
        }

        protected  IDictionary GetItems()
        {
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                return current.Items;
            }

            return null;
        }

        public  object Get(string key)
        {
            var items = GetItems();
            if (items == null)
                return null;

            return items[key];
        }

        public  void Add(string key, object obj)
        {
            var items = GetItems();
            if (items == null)
                return;

            if (IsEnabled && (obj != null))
            {
                items.Add(key, obj);
            }
        }

        public  void Remove(string key)
        {
            var items = GetItems();
            if (items == null)
                return;

            items.Remove(key);
        }

        public  void RemoveByPattern(string pattern)
        {
            var items = GetItems();
            if (items == null)
                return;

            IDictionaryEnumerator enumerator = items.GetEnumerator();
            Regex regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<String>();
            while (enumerator.MoveNext())
            {
                if (regex.IsMatch(enumerator.Key.ToString()))
                {
                    keysToRemove.Add(enumerator.Key.ToString());
                }
            }

            foreach (string key in keysToRemove)
            {
                items.Remove(key);
            }
        }

    }
}
