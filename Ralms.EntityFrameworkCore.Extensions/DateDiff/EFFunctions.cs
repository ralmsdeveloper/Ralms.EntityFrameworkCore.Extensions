/*
 *          Copyright (c) 2018 Rafael Almeida (ralms@ralms.net)
 *
 *           Ralms.Microsoft.EntitityFrameworkCore.Extensions
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

namespace Microsoft.EntityFrameworkCore
{
    public static class EFFunctions
    {
        public static int DateDiffYear(
            DateTime startDate,
            DateTime endDate)
            => endDate.Year - startDate.Year;

        public static int? DateDiffYear(
            DateTime? startDate,
            DateTime? endDate)
            => (startDate.HasValue && endDate.HasValue)
                ? (int?)DateDiffYear(startDate.Value, endDate.Value)
                : null;

        public static int DateDiffYear(
            DateTimeOffset startDate,
            DateTimeOffset endDate)
            => DateDiffYear(startDate.UtcDateTime, endDate.UtcDateTime);

        public static int? DateDiffYear(
            DateTimeOffset? startDate,
            DateTimeOffset? endDate)
            => (startDate.HasValue && endDate.HasValue)
                ? (int?)DateDiffYear(startDate.Value, endDate.Value)
                : null;

        public static int DateDiffMonth(
            DateTime startDate,
            DateTime endDate)
            => 12 * (endDate.Year - startDate.Year) + endDate.Month - startDate.Month;

        public static int? DateDiffMonth(
            DateTime? startDate,
            DateTime? endDate)
            => (startDate.HasValue && endDate.HasValue)
                ? (int?)DateDiffMonth(startDate.Value, endDate.Value)
                : null;

        public static int DateDiffMonth(
            DateTimeOffset startDate,
            DateTimeOffset endDate)
            => DateDiffMonth(startDate.UtcDateTime, endDate.UtcDateTime);

        public static int? DateDiffMonth(
            DateTimeOffset? startDate,
            DateTimeOffset? endDate)
            => (startDate.HasValue && endDate.HasValue)
                ? (int?)DateDiffMonth(startDate.Value, endDate.Value)
                : null;

        public static int DateDiffDay(
            DateTime startDate,
            DateTime endDate)
            => (endDate.Date - startDate.Date).Days;

        public static int? DateDiffDay(
            DateTime? startDate,
            DateTime? endDate)
            => (startDate.HasValue && endDate.HasValue)
                ? (int?)DateDiffDay(startDate.Value, endDate.Value)
                : null;

        public static int DateDiffDay(
            DateTimeOffset startDate,
            DateTimeOffset endDate)
            => DateDiffDay(startDate.UtcDateTime, endDate.UtcDateTime);

        public static int? DateDiffDay(
            DateTimeOffset? startDate,
            DateTimeOffset? endDate)
            => (startDate.HasValue && endDate.HasValue)
                ? (int?)DateDiffDay(startDate.Value, endDate.Value)
                : null;

        public static int DateDiffHour(
            DateTime startDate,
            DateTime endDate)
        {
            checked
            {
                return DateDiffDay(startDate, endDate) * 24 + endDate.Hour - startDate.Hour;
            }
        }

        public static int? DateDiffHour(
            DateTime? startDate,
            DateTime? endDate)
            => (startDate.HasValue && endDate.HasValue)
                ? (int?)DateDiffHour(startDate.Value, endDate.Value)
                : null;

        public static int DateDiffHour(
            DateTimeOffset startDate,
            DateTimeOffset endDate)
            => DateDiffHour(startDate.UtcDateTime, endDate.UtcDateTime);

        public static int? DateDiffHour(
            DateTimeOffset? startDate,
            DateTimeOffset? endDate)
            => (startDate.HasValue && endDate.HasValue)
                ? (int?)DateDiffHour(startDate.Value, endDate.Value)
                : null;

        public static int DateDiffMinute(
            DateTime startDate,
            DateTime endDate)
        {
            checked
            {
                return DateDiffHour(startDate, endDate) * 60 + endDate.Minute - startDate.Minute;
            }
        }

        public static int? DateDiffMinute(
            DateTime? startDate,
            DateTime? endDate)
            => (startDate.HasValue && endDate.HasValue)
                ? (int?)DateDiffMinute(startDate.Value, endDate.Value)
                : null;

        public static int DateDiffMinute(
            DateTimeOffset startDate,
            DateTimeOffset endDate)
            => DateDiffMinute(startDate.UtcDateTime, endDate.UtcDateTime);

        public static int? DateDiffMinute(
            DateTimeOffset? startDate,
            DateTimeOffset? endDate)
            => (startDate.HasValue && endDate.HasValue)
                ? (int?)DateDiffMinute(startDate.Value, endDate.Value)
                : null;

        public static int DateDiffSecond(
            DateTime startDate,
            DateTime endDate)
        {
            checked
            {
                return DateDiffMinute(startDate, endDate) * 60 + endDate.Second - startDate.Second;
            }
        }

        public static int? DateDiffSecond(
            DateTime? startDate,
            DateTime? endDate)
            => (startDate.HasValue && endDate.HasValue)
                ? (int?)DateDiffSecond(startDate.Value, endDate.Value)
                : null;

        public static int DateDiffSecond(
            DateTimeOffset startDate,
            DateTimeOffset endDate)
            => DateDiffSecond(startDate.UtcDateTime, endDate.UtcDateTime);

        public static int? DateDiffSecond(
            DateTimeOffset? startDate,
            DateTimeOffset? endDate)
            => (startDate.HasValue && endDate.HasValue)
                ? (int?)DateDiffSecond(startDate.Value, endDate.Value)
                : null;

        public static int DateDiffMillisecond(
            DateTime startDate,
            DateTime endDate)
        {
            checked
            {
                return DateDiffSecond(startDate, endDate) * 1000 + endDate.Millisecond - startDate.Millisecond;
            }
        }

        public static int? DateDiffMillisecond(
            DateTime? startDate,
            DateTime? endDate)
            => (startDate.HasValue && endDate.HasValue)
                ? (int?)DateDiffMillisecond(startDate.Value, endDate.Value)
                : null;

        public static int DateDiffMillisecond(
            DateTimeOffset startDate,
            DateTimeOffset endDate)
            => DateDiffMillisecond(startDate.UtcDateTime, endDate.UtcDateTime);

        public static int? DateDiffMillisecond(
            DateTimeOffset? startDate,
            DateTimeOffset? endDate)
            => (startDate.HasValue && endDate.HasValue)
                ? (int?)DateDiffMillisecond(startDate.Value, endDate.Value)
                : null;

        public static int DateDiffMicrosecond(
            DateTime startDate,
            DateTime endDate)
        {
            checked
            {
                return (int)((endDate.Ticks - startDate.Ticks) / 10);
            }
        }

        public static int? DateDiffMicrosecond(
            DateTime? startDate,
            DateTime? endDate)
            => (startDate.HasValue && endDate.HasValue)
                ? (int?)DateDiffMicrosecond(startDate.Value, endDate.Value)
                : null;

        public static int DateDiffMicrosecond(
            DateTimeOffset startDate,
            DateTimeOffset endDate)
            => DateDiffMicrosecond(startDate.UtcDateTime, endDate.UtcDateTime);

        public static int? DateDiffMicrosecond(
            DateTimeOffset? startDate,
            DateTimeOffset? endDate)
            => (startDate.HasValue && endDate.HasValue)
                ? (int?)DateDiffMicrosecond(startDate.Value, endDate.Value)
                : null;

        public static int DateDiffNanosecond(
            DateTime startDate,
            DateTime endDate)
        {
            checked
            {
                return (int)((endDate.Ticks - startDate.Ticks) * 100);
            }
        }

        public static int? DateDiffNanosecond(
            DateTime? startDate,
            DateTime? endDate)
            => (startDate.HasValue && endDate.HasValue)
                ? (int?)DateDiffNanosecond(startDate.Value, endDate.Value)
                : null;

        public static int DateDiffNanosecond(
            DateTimeOffset startDate,
            DateTimeOffset endDate)
            => DateDiffNanosecond(startDate.UtcDateTime, endDate.UtcDateTime);

        public static int? DateDiffNanosecond(
            DateTimeOffset? startDate,
            DateTimeOffset? endDate)
            => (startDate.HasValue && endDate.HasValue)
                ? (int?)DateDiffNanosecond(startDate.Value, endDate.Value)
                : null;
    }
}
