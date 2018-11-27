﻿using System;
using System.Collections.Generic;

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
}

