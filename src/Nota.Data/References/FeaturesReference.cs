using Nota.Data.Generated.Besonderheit;
using Nota.Data.Generated.Misc;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;

namespace Nota.Data.References
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

            this.Cost = x.Kosten;
            this.Hidden = x.Gebunden;

        }

        private readonly BesonderheitenBesonderheit origin;

        public Data Data { get; }
        public LocalizedString Description { get; }
        public string Id { get; }
        public LocalizedString Name { get; }
        public ImmutableArray<TagReference> Tags { get; private set; }
        public int Cost { get; }


        /// <summary>
        /// Hidden Features can't be bought activly. They come with species like a tail.
        /// </summary>
        public bool Hidden { get; }
        public FeaturesReference Replaces { get; private set; }
        internal Expressions.Expresion<CharacterData> Expression { get; private set; }

        void IReference.Initilize(Dictionary<string, TalentReference> directoryTalent, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Dictionary<string, GenusReference> directoryGenus, Dictionary<string, OrganismReference> directoryBeing, Dictionary<string, PathGroupReference> directoryPathGroup, Dictionary<string, PathReference> directoryPath)
        {
            if (this.origin.Ersetzt?.Besonderheit.Id != null)
                this.Replaces = directoryFeatures[this.origin.Ersetzt?.Besonderheit.Id];

            this.Expression = Expressions.Expresion.GetExpresion(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPathGroup, directoryPath, this.origin.Bedingung);
            this.Tags = this.origin.Tags.Select(y => directoryTags[y.Id]).ToImmutableArray();


        }

    }
}