using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Loan.Application.Infrastructure.Data
{
    public static class Parser
    {
        public static T GetEnumFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new ArgumentException();

            #region using Linq

            //FieldInfo[] fields = type.GetFields();
            //
            //var field = fields
            //    .SelectMany
            //        (
            //            f => f.GetCustomAttributes(typeof(DescriptionAttribute), false),
            //            (f, a) => new { Field = f, Attribute = a }
            //        )
            //    .Where
            //        (
            //            a => ((DescriptionAttribute)a.Attribute).Description == description
            //        ).SingleOrDefault();
            //return field == null ? default(T) : (T)field.Field.GetRawConstantValue();

            #endregion

            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            
            throw new ArgumentException("Not found.", "description");
            //return default(T);
        }

        public static string GetDescriptionFromEnum(Object thisEnum)
        {
            DescriptionAttribute attribute = thisEnum.GetType()
                .GetField(thisEnum.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;

            return attribute == null ? string.Empty : attribute.Description;
        }
    }
}
