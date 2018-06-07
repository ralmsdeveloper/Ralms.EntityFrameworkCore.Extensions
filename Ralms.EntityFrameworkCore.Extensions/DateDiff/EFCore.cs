/* 
 *          Copyright (c) 2017-2018 Rafael Almeida (ralms@ralms.net)
 *
 *                    Ralms.EntityFrameworkCore.Extensions
 *
 * THIS MATERIAL IS PROVIDED AS IS, WITH ABSOLUTELY NO WARRANTY EXPRESSED
 * OR IMPLIED.  ANY USE IS AT YOUR OWN RISK.
 *
 * Permission is hereby granted to use or copy this program
 * for any purpose,  provided the above notices are retained on all copies.
 * Permission to modify the code and to distribute modified code is granted,
 * provided the above notices are retained, and a notice that the code was
 * modified is included with the above copyright notice.
 *
 */

 using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace Microsoft.EntityFrameworkCore
{
    public static class EFCore
    {
        public static void EnableSqlServerDateDIFF(this ModelBuilder builder)
        {
            builder.HasDbFunction(typeof(EFCore)
              .GetMethod(nameof(EFCore.DateDiff)))
              .HasTranslation(args =>
              {
                  var arguments = args.ToList();
                  arguments[0] = new SqlFragmentExpression(((ConstantExpression)arguments.First()).Value.ToString());
                  return new SqlFunctionExpression(
                      "DATEDIFF",
                      typeof(int),
                      arguments);
              });
        }

        public static int? DateDiff(DatePart datePart, object start, object end)
        {
            var startDate = start.GetType() == typeof(DateTime)
                ? (DateTime)start
                : (DateTimeOffset)start;

            var endDate = end.GetType() == typeof(DateTime)
                ? (DateTime)end
                : (DateTimeOffset)end;

            switch (datePart)
            {
                case DatePart.day:
                    return EFFunctions.DateDiffDay(startDate, endDate);
                case DatePart.month:
                    return EFFunctions.DateDiffMonth(startDate, endDate);
                case DatePart.year:
                    return EFFunctions.DateDiffYear(startDate, endDate);
                case DatePart.hour:
                    return EFFunctions.DateDiffHour(startDate, endDate);
                case DatePart.minute:
                    return EFFunctions.DateDiffMinute(startDate, endDate);
                case DatePart.second:
                    return EFFunctions.DateDiffSecond(startDate, endDate);
                case DatePart.millisecond:
                    return EFFunctions.DateDiffMillisecond(startDate, endDate);
                case DatePart.microsecond:
                    return EFFunctions.DateDiffMicrosecond(startDate, endDate);
                case DatePart.nanosecond:
                    return EFFunctions.DateDiffNanosecond(startDate, endDate);
                default:
                    throw new Exception("Please enter a valid DATEPART!");
            }
        }
    }
}
