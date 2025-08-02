using System;
using UnityEngine;

namespace Nrjwolf.Tools.AttachAttributes
{
    [AttributeUsage(AttributeTargets.Field)] public class GetComponentAttribute : AttachPropertyAttribute { }

    [AttributeUsage(AttributeTargets.Field)]
    public class GetComponentInChildrenAttribute : AttachPropertyAttribute
    {
        public string ChildName;

        public GetComponentInChildrenAttribute(bool includeInactive = false)
        {
            IncludeInactive = includeInactive;
        }

        public GetComponentInChildrenAttribute(string childName)
        {
            ChildName = childName;
        }

        public bool IncludeInactive{ get; private set; }
    }

    [AttributeUsage(AttributeTargets.Field)] public class AddComponentAttribute : AttachPropertyAttribute { }

    [AttributeUsage(AttributeTargets.Field)] public class FindObjectOfTypeAttribute : AttachPropertyAttribute { }

    [AttributeUsage(AttributeTargets.Field)] public class GetComponentInParent : AttachPropertyAttribute { }


    public class AttachPropertyAttribute : PropertyAttribute { }
}