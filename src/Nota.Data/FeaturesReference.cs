using Nota.Data.Generated.Besonderheit;
using Nota.Data.Generated.Misc;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Nota.Data
{
    public class FeaturesReference : IReference
    {
        internal FeaturesReference(BesonderheitenBesonderheit x, Data data)
        {
            this.origin = x;
            this.Data = data;
            this.Description = x.Beschreibung;
            this.Id = x.Id;
            this.Name = x.Name;
            this.Tags = x.Tags?.Select(y => data.Tags.First(z => z.Id == y.Id)).ToList().AsReadOnly() ?? new List<TagReference>().AsReadOnly();

            this.Cost = x.Kosten;
            this.Hidden = x.Gebunden;

        }

        private readonly BesonderheitenBesonderheit origin;

        public Data Data { get; }
        public LocalizedString Description { get; }
        public string Id { get; }
        public LocalizedString Name { get; }
        public ReadOnlyCollection<TagReference> Tags { get; }
        public int Cost { get; }


        /// <summary>
        /// Hidden Features can't be bought activly. They come with species like a tail.
        /// </summary>
        public bool Hidden { get; }
        public FeaturesReference Replaces { get; private set; }
        internal Expressions.Expresion Expression { get; private set; }

        void IReference.Initilize(Dictionary<string, TalentReference> talentLookup, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags)
        {
            if (this.origin.Ersetzt?.Besonderheit.Id != null)
                this.Replaces = directoryFeatures[this.origin.Ersetzt?.Besonderheit.Id];

            this.Expression = Expressions.Expresion.GetExpresion(talentLookup, directoryCompetency, directoryFeatures, directoryTags, this.origin.Bedingung);


        }

    }
}