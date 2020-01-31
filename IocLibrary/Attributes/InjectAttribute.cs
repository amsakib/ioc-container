using System;
using System.Collections.Generic;
using System.Text;

namespace IocLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Constructor)]
    public class InjectAttribute : Attribute
    {
    }
}
