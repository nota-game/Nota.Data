using Nota.Data.Generated.Besonderheit;
using Nota.Data.Generated.Misc;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Nota.Data
{
    public class FeaturesReference
    {
        internal FeaturesReference(BesonderheitenBesonderheit x, Data data)
        {
            this.Data = data;
            this.Description = x.Beschreibung;
            this.Id = x.Id;
            this.Name = x.Name;
            this.Tags = x.Tags?.Select(y => data.Tags.First(z => z.Id == y.Id)).ToList().AsReadOnly() ?? new List<TagReference>().AsReadOnly();

            this.Cost = x.Kosten;
            this.ersetzt = x.Ersetzt?.Besonderheit.Id;
            this.Hidden = x.Gebunden;

        }

        public Data Data { get; }
        public LocalizedString Description { get; }
        public string Id { get; }
        public LocalizedString Name { get; }
        public ReadOnlyCollection<TagReference> Tags { get; }
        public int Cost { get; }

        private readonly string ersetzt;

        /// <summary>
        /// Hidden Features can't be bought activly. They come with species like a tail.
        /// </summary>
        public bool Hidden { get; }
        public FeaturesReference Replaces { get; private set; }

        internal void InitilizeReplacement(Dictionary<string, FeaturesReference> directory)
        {
            if (this.ersetzt != null)
                this.Replaces = directory[this.ersetzt];
        }

    }
}