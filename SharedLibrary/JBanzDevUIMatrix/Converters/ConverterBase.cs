using System;
using System.Windows;

namespace JBanzDevUIMatrix.Converters
{
    public abstract class ConverterBase
    {
        protected bool ToBoolean(object value)
        {
            var result = ToNullableBoolean(value);

            if (result.HasValue)
            {
                return result.Value;
            }

            return false;
        }

        protected bool? ToNullableBoolean(object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is bool)
            {
                return (bool)value;
            }

            if (value is string)
            {
                if (bool.TryParse(value.ToString(), out bool result))
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }

            if (value is int || value is decimal)
            {
                return Convert.ToBoolean(value);
            }

            return null;
        }

        protected DateTime? ToDateTime(object value)
        {
            if (value != null)
            {
                return DateTime.Parse(value.ToString());
            }
            return null;
        }

        protected string ToStringValue(object value)
        {
            if (value != null)
            {
                return value.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        protected Thickness? ToNullableThickness(object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is string s)
            {
                var ar = s.Split(",".ToCharArray());
                if (ar.Length == 1)
                {
                    if (double.TryParse(ar[0], out double result))
                    {
                        return new Thickness(result);
                    }
                }
                else if (ar.Length == 4)
                {
                    double[] dAr = new double[4];
                    for (int i = 0; i < 4; i++)
                    {
                        if (double.TryParse(ar[i], out double result))
                        {
                            dAr[i] = result;
                        }
                        else
                        {
                            return null;
                        }
                    }

                    return new Thickness(dAr[0], dAr[1], dAr[2], dAr[3]);
                }
            }

            return null;
        }

        protected double? ToNullableDouble(object value)
        {
            if (value is double v)
            {
                return (double)v;
            }
            if (value is string s)
            {
                if (double.TryParse(s, out double result))
                {
                    return result;
                }
            }

            return null;
        }
    }
}
