using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Newtonsoft.Json;
using Vovinam.Common;

namespace Vovinam.WebBackend.Common
{
    public static class EnumUtil
    {
        public static string GetDescription(this Enum value)
        {
            if (value == null)
            {
                return "";
            }
            FieldInfo field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                return "";
            }
            DescriptionAttribute attribute
                = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                    as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static IEnumerable<Permission> GetListPermission()
        {
            return Enum.GetValues(typeof(Permission)).Cast<Permission>();
        }

        public static MvcHtmlString ToTreeNode(Permission p, Dictionary<Permission, NodeStatus> selected)
        {
            var result = new
            {
                text = p.GetDescription(),
                id = (int) p,
                state = new
                {
                    selected = selected.Any(x=> x.Key == p && x.Value.Selected == true)
                }
            };
            selected.FirstOrDefault(x => x.Key == p).Value.Showed = true;
            return MvcHtmlString.Create(JsonConvert.SerializeObject(result));
        }

        public static MvcHtmlString ToTreeList(Dictionary<Permission, NodeStatus> selected, params Permission[] permissions)
        {
            return MvcHtmlString.Create(string.Join(",", permissions.Select(x => EnumUtil.ToTreeNode(x, selected))));
        }

        public class NodeStatus
        {
            public bool Selected { get; set; }
            public bool Showed { get; set; }
        }
    }

    public static class EnumUtilGroup
    {
        public static string GetDescriptionGroup(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                    as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static IEnumerable<Permission> GetListPermission()
        {
            return Enum.GetValues(typeof(Permission)).Cast<Permission>();
        }
    }
}