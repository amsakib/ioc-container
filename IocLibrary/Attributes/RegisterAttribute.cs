using System;
using System.Collections.Generic;
using System.Text;

namespace IocLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct)]
    public class RegisterAttribute : Attribute
    {
        public string Name { get; }
        public Type Type { get; }
        public RegisterAttribute()
        {
            Name = "";
        }

        public RegisterAttribute(Type type)
        {
            Name = type.Name;
            Type = type;
        }

        public RegisterAttribute(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }
}
