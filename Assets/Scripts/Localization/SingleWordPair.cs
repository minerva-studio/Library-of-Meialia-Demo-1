using Minerva.Module;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minerva.Localization
{
    [Serializable]
    public class SingleWordPair : IComparable<SingleWordPair>
    {
        public string id;
        public List<Argument> content = new List<Argument>();

        public int CompareTo(SingleWordPair other)
        {
            if (other is null) return 1;
            return id.CompareTo(other.id);
        }

        public string GetValue(string key)
        {
            Func<Argument, bool> predicate = (a) =>
            {
                return a.Key == key;
            };

            var val = content.Where(predicate);
            return val.Count() > 0 ? val.First().Value : "";
        }
    }
}