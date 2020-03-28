﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EchoBlog.Infrastructures.Core.Extensions
{
    public static class GenericTypeExtension
    {
        /// <summary>
        /// 获取泛型类的名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetGenericTypeName(this Type type)
        {
            var typeName = string.Empty;

            if (type.IsGenericType)
            {
                var genericTypes = string.Join(",", type.GetGenericArguments().Select(s => s.Name).ToArray());
                typeName = $"{ type.Name.Remove(type.Name.IndexOf("`"))}<{genericTypes}>";
            }
            else
            {
                typeName = type.Name;
            }
            return typeName;
        }


        /// <summary>
        /// 获取泛型类的名称
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        public static string GetGenericTypeName(this object @object) => @object.GetType().GetGenericTypeName();
    }
}
