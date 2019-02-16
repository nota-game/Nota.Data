using System.Collections.ObjectModel;
using Nota.Data.Generated.Misc;

namespace Nota.Data
{
    public class TagReference
    {

        internal TagReference(Tag x, Data data)
        {
            this.Id = x.Id;
            this.Name = x.Name;
            this.Description = x.Beschreibung;
            this.Data = data;
        }

        public string Id { get; }
        public LocalizedString Name { get; }
        public LocalizedString Description { get; }
        public Data Data { get; }
    }
}