using System;
using UnityEngine;

namespace Minerva.Module
{
    /// 这不重要（这没niao用）
    /// <summary> This is a Temporary Method/class/whatever </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public sealed class TemporaryAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        private readonly string positionalString;

        // This is a positional argument
        public TemporaryAttribute(string positionalString = "This is a temporary method") : this()
        {
            this.positionalString = positionalString;

            // TODO: Implement code here

            Debug.Log(positionalString);
        }
        private TemporaryAttribute()
        {
            Debug.Log(positionalString);
        }

        public string PositionalString => positionalString;

        // This is a named argument
        public int NamedInt { get; set; }
    }
}