using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Minerva.Module
{
    /// <summary>
    /// interface that gives an object a name tag
    /// </summary>
    public interface INameable
    {
        string Name { get; }
    }

    public static class Nameables
    {
        public static T Get<T>(this IEnumerable<T> ts, string name) where T : INameable
        {
            return ts.FirstOrDefault(i => i.Name == name);
        }
    }
}
