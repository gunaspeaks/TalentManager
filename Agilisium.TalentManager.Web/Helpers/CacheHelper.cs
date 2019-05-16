using Agilisium.TalentManager.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Helpers
{
    public class CacheHelper
    {
        private static HttpContextBase contextBase;

        public static void SetContext(HttpContextBase context)
        {
            contextBase = context;
        }

        public static void AddOrUpdateItem(string key, object item, HttpContextBase context = null)
        {
            contextBase.Cache.Remove(key);
            contextBase.Cache.Insert(key, item);
        }

        public static object GetItem(string key, HttpContextBase context = null)
        {
            return contextBase.Cache[key];
        }

        public static bool HasDataForCollection(string key)
        {
            IEnumerable<ViewModelBase> items = (IEnumerable<ViewModelBase>)contextBase.Cache[key];

            if (items == null)
            {
                return false;
            }

            if (items != null && items.Count() == 0)
            {
                return false;
            }

            return true;
        }

        public static bool IsCached(string key, HttpContextBase context)
        {
            return !(contextBase.Cache[key] == null);
        }
    }
}