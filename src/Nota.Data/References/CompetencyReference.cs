using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using Nota.Data.Expressions;
using Nota.Data.Generated.Fertigkeit;
using Nota.Data.Generated.Talent;

namespace Nota.Data.References
{
    public class CompetencyReference : IReference
    {

        public CompetencyReference(FertigkeitenFertigkeit x, Data data)
        {
            this.origin = x;
            this.Description = x.Beschreibung;
            this.Id = x.Id;
            this.Name = x.Name;

            this.Cost = x.Kosten;
            this.Data = data;
        }

        private readonly FertigkeitenFertigkeit origin;

        public LocalizedString Description { get; }
        public string Id { get; }
        public LocalizedString Name { get; }
        public ImmutableArray<TagReference> Tags { get; private set; }
        public int Cost { get; }
        public CompetencyReference Replaces { get; private set; }
        public Data Data { get; }
        internal Expresion Expression { get; private set; }


        void IReference.Initilize(Dictionary<string, TalentReference> directoryTalent, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Dictionary<string, GenusReference> directoryGenus, Dictionary<string, BeingReference> directoryBeing, Dictionary<string, PathGroupReference> directoryPath)
        {
            if (this.origin.Ersetzt?.Fertigkeit != null)
                this.Replaces = directoryCompetency[this.origin.Ersetzt?.Fertigkeit.Id];

            this.Expression = Expresion.GetExpresion(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath, this.origin.Voraussetzung);
            this.Tags = this.origin.Tags.Select(y => directoryTags[y.Id]).ToImmutableArray();
        }

    }

}