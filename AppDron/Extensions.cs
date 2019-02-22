using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AppDron
{
    public static class ConvertibleExtensions
    {
        public static TOut ChangeType<TOut>(this object obj)
        {
            try
            {
                TOut result = default(TOut);
                Type outType = typeof(TOut);
                if (outType.IsAssignableFrom(obj.GetType()))
                {
                    result = (TOut)obj;
                }
                else if (typeof(TOut).IsEnum)
                {
                    result = obj != null ? (TOut)Enum.Parse(typeof(TOut), obj.ToString(), true) : result;
                }
                else if (outType.IsGenericType && outType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    result = obj != null && !string.IsNullOrEmpty(obj.ToString()) ? (TOut)Convert.ChangeType(obj, outType.GetGenericArguments()[0], CultureInfo.InvariantCulture) : result;
                }
                else
                {
                    result = obj != null ? (TOut)Convert.ChangeType(obj, outType, CultureInfo.InvariantCulture) : result;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidCastException(string.Format(@"Type {0} can't cast to {1} type with value: ""{2}"" --> Message: {3}", obj.GetType().Name, typeof(TOut).Name, obj, ex.Message));
            }
        }

        public static object ChangeType(this object obj, Type outType)
        {
            try
            {
                object result = null;

                if (outType.IsEnum)
                {
                    result = obj != null ? Enum.Parse(outType, obj.ToString(), true) : result;
                }
                else if (outType.IsGenericType && outType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    result = obj != null && !string.IsNullOrEmpty(obj.ToString()) ? Convert.ChangeType(obj, outType.GetGenericArguments()[0], CultureInfo.InvariantCulture) : result;
                }
                else
                {
                    result = obj != null ? Convert.ChangeType(obj, outType, CultureInfo.InvariantCulture) : result;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidCastException(string.Format(@"Type {0} can't cast to {1} type with value: ""{2}"" --> Message: {3}", obj.GetType().Name, outType.Name, obj, ex.Message));
            }
        }
    }
}
