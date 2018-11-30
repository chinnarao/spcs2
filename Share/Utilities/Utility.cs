using GeoAPI.Geometries;
using NetTopologySuite;
using System;
using System.Collections.Generic;
using Share.Enums;

namespace Share.Utilities
{
    public class Utility
    {
        public static string GetCacheCode(int index, string CountryCode, string Code, string Name)
        {
            if (index <= 1 || index >= 6 || string.IsNullOrWhiteSpace(CountryCode) || string.IsNullOrWhiteSpace(Code) || string.IsNullOrWhiteSpace(Name))
                return null;
            switch ((TerritoryTypeEnum)index)
            {
                case TerritoryTypeEnum.State:
                    return string.Format("{0}2", CountryCode);
                case TerritoryTypeEnum.CountyOrProvince:
                    return string.Format("{0}3:{1}_{2}", CountryCode, Code, Name);
                case TerritoryTypeEnum.Community:
                    return string.Format("{0}4:{1}_{2}", CountryCode, Code, Name);
                case TerritoryTypeEnum.Place:
                    return string.Format("{0}5:{1}_{2}", CountryCode, Code, Name);
                default:
                    throw new Exception(nameof(GetCacheCode));
            }
        }

        public static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
        {
            {".txt",  "text/plain"},
            {".pdf",  "application/pdf"},
            {".doc",  "application/vnd.ms-word"},
            {".docx", "application/vnd.ms-word"},
            {".xls",  "application/vnd.ms-excel"},
            {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
            {".png",  "image/png"},
            {".jpg",  "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".gif",  "image/gif"},
            {".csv",  "text/csv"},
            { ".zip", "application/x-rar-compressed"},
            { ".json", " application/json"},
            { ".htm", "text/html"},
            { ".html", "text/html"}
        };
        }

        /// <summary>
        /// Casting from parameter to parameter  {  var first = new { Id = 1, Name = "Bob" }; var second = new { Id = 0, Name = "" }; second = utility.utility.Cast(second, first); }
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="to">future upcoming anonymous object, could be empty</param>
        /// <param name="from">original with data</param>
        /// <returns>anonymous to type object</returns>
        public static T Cast<T>(T to, T from)
        {
            return (T)from;
        }

        //10 million didn’t create a duplicate
        public static string GenerateStringId()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        //https://madskristensen.net/blog/generate-unique-strings-and-numbers-in-c/
        public static long GenerateLongId()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }

        public static T Parse<T>(string input)
        {
            return (T)Enum.Parse(typeof(T), input, true);
        }

        //https://www.latlong.net/convert-address-to-lat-long.html
        public static IPoint CreatePoint(double longitude, double lattitude)
        {
            //var r = new NetTopologySuite.IO.WKTReader { DefaultSRID = 4326, HandleOrdinates = GeoAPI.Geometries.Ordinates.XY };

            //Location = LocationManager.GeometryFactory.CreatePoint(new Coordinate(rnd.NextDouble() * 90.0, rnd.NextDouble() * 90.0))
            //https://github.com/cryptograch/backend/blob/c9f2666d909f577d9d98d41133b7ab08f1cab6b2/Taxi/Helpers/Location.cs
            //Longitude and Latitude [https://docs.microsoft.com/en-us/ef/core/modeling/spatial]
            //Coordinates in NTS are in terms of X and Y values. To represent longitude and latitude, use X for longitude and Y for latitude.Note that this is backwards from the latitude, longitude format in which you typically see these values.
            // glendale ca, Latitude and longitude coordinates are: 34.142509, -118.255074.
            //if (longitude == 0)
            //    longitude = -118.255074;
            //if (lattitude == 0)
            //    lattitude = 34.142509;
            // verify later : very imp: https://github.com/Hinaar/KutyApp/blob/4564904bbb4397d66a7375461eb4df337aa8bc58/KutyApp.Services.Environment.Bll/Mapping/KutyAppServiceProfile.cs
            return NtsGeometryServices.Instance.CreateGeometryFactory(4326).CreatePoint(new Coordinate(longitude, lattitude));
        }

        public static IPoint CreatePoint(string longitude, string latitude)
        {
            double longi;
            if (!double.TryParse(longitude, out longi))
                longi = 1.0;
            double lati;
            if (!double.TryParse(longitude, out lati))
                lati = 1.0;
            return NtsGeometryServices.Instance.CreateGeometryFactory(4326).CreatePoint(new Coordinate(longi, lati));
        }
    }
}

