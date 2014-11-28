using System;
using System.Globalization;
using System.Threading;
using Infrastructure.CrossCutting.NetFramework.Enums;

namespace Infrastructure.CrossCutting.NetFramework.Util
{
    public class DateDiff
    {

        public DateDiff()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es-CO");
        }

        public static long DiffDate(DateInterval interval, DateTime dt1, DateTime dt2)
        {
            return DateTimeFormatInfo.CurrentInfo != null ? DiffDate(interval, dt1, dt2, DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek) : 0;
        }


        private static int GetQuarter(int nMonth)
        {
            if (nMonth <= 3)
                return 1;
            if (nMonth <= 6)
                return 2;
            return nMonth <= 9 ? 3 : 4;
        }



        public static long DiffDate(DateInterval interval, DateTime dt1, DateTime dt2, DayOfWeek eFirstDayOfWeek)
        {

            if (interval == DateInterval.Year)
                return dt2.Year - dt1.Year;

            if (interval == DateInterval.Month)
                return (dt2.Month - dt1.Month) + (12 * (dt2.Year - dt1.Year));

            var ts = dt2 - dt1;

            if (interval == DateInterval.Day || interval == DateInterval.DayOfYear)
                return Round(ts.TotalDays);

            if (interval == DateInterval.Hour)
                return Round(ts.TotalHours);

            if (interval == DateInterval.Minute)
                return Round(ts.TotalMinutes);

            if (interval == DateInterval.Second)
                return Round(ts.TotalSeconds);

            if (interval == DateInterval.Weekday)
                return Round(ts.TotalDays / 7.0);

            if (interval == DateInterval.WeekOfYear)
            {
                while (dt2.DayOfWeek != eFirstDayOfWeek)
                    dt2 = dt2.AddDays(-1);

                while (dt1.DayOfWeek != eFirstDayOfWeek)
                    dt1 = dt1.AddDays(-1);

                ts = dt2 - dt1;

                return Round(ts.TotalDays / 7.0);
            }



            if (interval == DateInterval.Quarter)
            {
                var d1Quarter = GetQuarter(dt1.Month);
                var d2Quarter = GetQuarter(dt2.Month);
                var d1 = d2Quarter - d1Quarter;
                var d2 = (4 * (dt2.Year - dt1.Year));
                return Round(d1 + d2);

            }
            return 0;
        }



        private static long Round(double dVal)
        {
            if (dVal >= 0)
                return (long)Math.Floor(dVal);
            return (long)Math.Ceiling(dVal);
        }

        public static string RetornarNombreMes(string numeroMes)
        {
            var mes = "";
            switch (numeroMes)
            {
                case "1":
                    mes = "Enero";
                    break;
                case "2":
                    mes = "Febrero";
                    break;
                case "3":
                    mes = "Marzo";
                    break;
                case "4":
                    mes = "Abril";
                    break;
                case "5":
                    mes = "Mayo";
                    break;
                case "6":
                    mes = "Junio";
                    break;
                case "7":
                    mes = "Julio";
                    break;
                case "8":
                    mes = "Agosto";
                    break;
                case "9":
                    mes = "Septiembre";
                    break;
                case "10":
                    mes = "Octubre";
                    break;
                case "11":
                    mes = "Noviembre";
                    break;
                case "12":
                    mes = "Diciembre";
                    break;
            }
            return mes;
        }


        public static string RetornarNumeroMes(string nomMes)
        {
            var mes = "";
            switch (nomMes)
            {
                case "Enero":
                    mes = "1";
                    break;
                case "Febrero":
                    mes = "2";
                    break;
                case "Marzo":
                    mes = "3";
                    break;
                case "Abril":
                    mes = "4";
                    break;
                case "Mayo":
                    mes = "5";
                    break;
                case "Junio":
                    mes = "6";
                    break;
                case "Julio":
                    mes = "7";
                    break;
                case "Agosto":
                    mes = "8";
                    break;
                case "Septiembre":
                    mes = "9";
                    break;
                case "Octubre":
                    mes = "10";
                    break;
                case "Noviembre":
                    mes = "11";
                    break;
                case "Diciembre":
                    mes = "12";
                    break;
            }
            return mes;
        }
    }
}