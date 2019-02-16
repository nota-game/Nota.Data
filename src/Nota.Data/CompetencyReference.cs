using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Nota.Data.Expressions;
using Nota.Data.Generated.Fertigkeit;
using Nota.Data.Generated.Talent;

namespace Nota.Data
{
    public class CompetencyReference : IReference
    {

        public CompetencyReference(FertigkeitenFertigkeit x, Data data)
        {
            this.origin = x;
            this.Description = x.Beschreibung;
            this.Id = x.Id;
            this.Name = x.Name;
            this.Tags = x.Tags.Select(y => data.Tags.First(z => z.Id == y.Id)).ToList().AsReadOnly();

            this.Cost = x.Kosten;
            this.Data = data;
        }

        private readonly FertigkeitenFertigkeit origin;

        public LocalizedString Description { get; }
        public string Id { get; }
        public LocalizedString Name { get; }
        public ReadOnlyCollection<TagReference> Tags { get; }
        public int Cost { get; }
        public CompetencyReference Replaces { get; private set; }
        public Data Data { get; }
        internal Expresion Expression { get; private set; }


        void IReference.Initilize(Dictionary<string, TalentReference> talentLookup, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags)
        {
            if (this.origin.Ersetzt?.Fertigkeit != null)
                this.Replaces = directoryCompetency[this.origin.Ersetzt?.Fertigkeit.Id];

            this.Expression = Expresion.GetExpresion(talentLookup, directoryCompetency, directoryFeatures, directoryTags, this.origin.Voraussetzung);



        }

    }

}