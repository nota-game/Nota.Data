using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using Nota.Data.Expressions;
using Nota.Data.Generated.Misc;

namespace Nota.Data
{
    public class LevelReference : IReference
    {

        internal LevelReference(LevelsLevel origin, Data data, PathReference pathReference)
        {
            this.origin = origin;
            this.Data = data;
            this.PathReference = pathReference;
            this.GenerationCost = this.origin.Kosten;
            this.PathCost = this.origin.PfadKosten;
            this.Id = this.origin.Id;


            this.Name = this.origin.Name;
            this.Description = this.origin.Beschreibung;


        }

        private readonly LevelsLevel origin;

        public Data Data { get; }
        public PathReference PathReference { get; }
        public int GenerationCost { get; }
        public string PathCost { get; }
        public string Id { get; }
        public Collection<LokalisierungenLokalisirung> Name { get; }
        public Collection<LokalisierungenLokalisirung> Description { get; }
        public ImmutableArray<FeaturesReference> Features { get; private set; }
        public ImmutableArray<CompetencyReference> Competency { get; private set; }
        public ImmutableArray<TagReference> Tags { get; private set; }
        public ImmutableArray<TalentReference> Talents { get; private set; }
        internal Expresion Expression { get; private set; }

        void IReference.Initilize(Dictionary<string, TalentReference> directoryTalent, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Dictionary<string, GenusReference> directoryGenus, Dictionary<string, BeingReference> directoryBeing, Dictionary<string, PathGroupReference> directoryPath)
        {
            this.Features = this.origin.BesonderheitSpecified
               ? this.origin.Besonderheit.Select(x => directoryFeatures[x.Id]).ToImmutableArray()
               : ImmutableArray.Create<FeaturesReference>();

            this.Competency = this.origin.FertigkeitSpecified
                ? this.origin.Fertigkeit.Select(x => directoryCompetency[x.Id]).ToImmutableArray()
               : ImmutableArray.Create<CompetencyReference>();

            this.Tags = this.origin.TagSpecified
                ? this.origin.Tag.Select(x => directoryTags[x.Id]).ToImmutableArray()
                : ImmutableArray.Create<TagReference>();

            this.Talents = this.origin.TalentSpecified
                ? this.origin.Talent.Select(x => directoryTalent[x.Id]).ToImmutableArray()
                : ImmutableArray.Create<TalentReference>();

            this.Expression = Expressions.Expresion.GetExpresion(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath, this.origin.Bedingungen.Zusätzlich);
            this.Expression = Expressions.Expresion.GetExpresion(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath, this.PathReference.Levels, this.origin.Bedingungen.LevelVoraussetzung);

        }
    }
}