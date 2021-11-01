using UnityEngine;

namespace Minerva.Module
{
    public sealed class BoolDependentAttribute : PropertyAttribute
    {
        public string keyName;
        public string booleanName;

        public BoolDependentAttribute(string booleanName, string keyName)
        {
            this.booleanName = booleanName;
            this.keyName = keyName;
        }
    }
}
