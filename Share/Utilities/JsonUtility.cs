﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace Share.Utilities
{
    /// <summary>
    /// Json utility (with newtonsoft)
    /// </summary>
    public static class JsonUtility
    {
        /// <summary>
        /// Snake case json serializer settings
        /// </summary>
        public static JsonSerializerSettings SnakeCaseJsonSerializerSettings
        {
            get
            {
                var settings = new JsonSerializerSettings();

                settings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                settings.Converters.Add(new StringEnumConverter());
                settings.NullValueHandling = NullValueHandling.Ignore;

                return settings;
            }
        }

        /// <summary>
        /// Camel case json serializer settings
        /// </summary>
        public static JsonSerializerSettings CamelCaseJsonSerializerSettings
        {
            get
            {
                var settings = new JsonSerializerSettings();

                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                settings.Converters.Add(new StringEnumConverter());
                settings.NullValueHandling = NullValueHandling.Ignore;

                return settings;
            }
        }

        /// <summary>
        /// Lower case json serializer settings
        /// </summary>
        public static JsonSerializerSettings LowerCaseJsonSerializerSettings
        {
            get
            {
                var settings = new JsonSerializerSettings();

                settings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new LowerCaseNamingResolver()
                };
                settings.Converters.Add(new StringEnumConverter());
                settings.NullValueHandling = NullValueHandling.Ignore;

                return settings;
            }
        }

        /// <summary>
        /// Camel case json serialize
        /// </summary>
        public static JsonSerializer CamelCaseJsonSerializer
        {
            get
            {
                var serializer = new JsonSerializer();
                serializer.NullValueHandling = NullValueHandling.Ignore;
                serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
                serializer.Converters.Add(new StringEnumConverter());

                return serializer;
            }
        }

        /// <summary>
        /// Snake case json serialize
        /// </summary>
        public static JsonSerializer SnakeCaseJsonSerializer
        {
            get
            {
                var serializer = new JsonSerializer();
                serializer.NullValueHandling = NullValueHandling.Ignore;
                serializer.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                serializer.Converters.Add(new StringEnumConverter());

                return serializer;
            }
        }

        /// <summary>
        /// Lower case json serialize
        /// </summary>
        public static JsonSerializer LowerCaseJsonSerializer
        {
            get
            {
                var serializer = new JsonSerializer();
                serializer.NullValueHandling = NullValueHandling.Ignore;
                serializer.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new LowerCaseNamingResolver()
                };
                serializer.Converters.Add(new StringEnumConverter());

                return serializer;
            }
        }

        /// <summary>
        /// Deserialize json in recursive Dictionary[string,object]
        /// </summary>
        /// <param name="token"></param>
        /// <param name="blackList"></param>
        public static object DeserializeAsObject(this string json)
        {
            return DeserializeAsObjectCore(JToken.Parse(json));
        }

        /// <summary>
        /// Cast jtoken to object
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private static object DeserializeAsObjectCore(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    return token.Children<JProperty>()
                                .ToDictionary(prop => prop.Name,
                                              prop => DeserializeAsObjectCore(prop.Value));

                case JTokenType.Array:
                    return token.Select(DeserializeAsObjectCore).ToList();

                default:
                    return ((JValue)token).Value;
            }
        }

        /// <summary>
        /// Mask fields
        /// </summary>
        /// <param name="json"></param>
        /// <param name="blacklist"></param>
        /// <returns></returns>
        public static string MaskFields(string json, string[] blacklist)
        {
            return MaskFields(json, blacklist, "******");
        }

        /// <summary>
        /// Mask fields
        /// </summary>
        /// <param name="json"></param>
        /// <param name="blacklist"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static string MaskFields(string json, string[] blacklist, string mask)
        {
            return json.MaskFields(blacklist, mask);
        }
    }

    /// <summary>
    /// Resolve property names to lowercase only
    /// </summary>
    public class LowerCaseNamingResolver : NamingStrategy
    {
        protected override string ResolvePropertyName(string name)
        {
            return name.ToLowerInvariant();
        }
    }
}
