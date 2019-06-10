using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Helpers
{
    public class EnumHelper
    {
        public static string GetEnumDescription(Type enumType, string key)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(key))
            {
                FieldInfo fi;
                DescriptionAttribute da;
                foreach (var enumValue in Enum.GetValues(enumType))
                {
                    if (Convert.ToInt32(enumValue) == Convert.ToInt32(key))
                    {
                        fi = enumType.GetField((enumValue.ToString()));
                        da = (DescriptionAttribute)Attribute.GetCustomAttribute(fi,
                        typeof(DescriptionAttribute));
                        if (da != null)
                        {
                            result = da.Description;
                        }
                    }
                }
            }
            return result;
        }

        public static Dictionary<int, string> GetEnumAsDictionary(Type enumtype)
        {
            Dictionary<int, string> list = new Dictionary<int, string>();
            FieldInfo fi;
            DescriptionAttribute da;
            foreach (var enumValue in Enum.GetValues(enumtype))
            {
                fi = enumtype.GetField((enumValue.ToString()));
                da = (DescriptionAttribute)Attribute.GetCustomAttribute(fi,
                typeof(DescriptionAttribute));
                if (da != null)
                {
                    list.Add(Convert.ToInt32(enumValue), da.Description);
                }
            }
            return list;
        }

        public static int GetEnumDescription(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
