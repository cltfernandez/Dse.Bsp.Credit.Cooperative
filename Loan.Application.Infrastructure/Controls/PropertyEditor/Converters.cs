using Jc.Scripts.Database;
using Loan.Application.Infrastructure.Enumerations.Sql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Globalization;

namespace Loan.Application.Infrastructure.Controls.PropertyEditor
{
    namespace Converters
    {
        public class MoneyConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(string);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                if (value is string)
                {
                    string s = (string)value;
                    return Decimal.Parse(s, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint , culture);
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return ((decimal)value).ToString("#,##0.00", culture);

                return base.ConvertTo(context, culture, value, destinationType);
            }
        }

        public class RateConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(string);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                if (value is string)
                {
                    string s = (string)value;
                    return Decimal.Parse(s, NumberStyles.AllowDecimalPoint, culture);
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return ((decimal)value).ToString("0.0000", culture);

                return base.ConvertTo(context, culture, value, destinationType);
            }
        }

        //public class LoanTypeConverter : TypeConverter
        //{
        //    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        //    {
        //        return true;
        //    }

        //    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        //    {
        //        List<string> list = Data.Database.GetDetails(Query.LoanType, "%").AsEnumerable().Select(x => x["LOAN_TYPE"].ToString()).ToList();
        //        return new StandardValuesCollection(list);
        //    }
        //}
    }
}
