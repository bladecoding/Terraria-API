using System;

namespace TerrariaAPI.Hooks.Classes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    internal class MethodHookAttribute : Attribute
    {
    }
}