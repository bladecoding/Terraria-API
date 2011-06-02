using System;

namespace TerrariaAPI.Hooks.Classes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    class MethodHookAttribute : Attribute
    {
    }
}