using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using Nota.Data.Generated.Misc;

namespace Nota.Data
{
    public class MorphReference : IReference
    {

        internal MorphReference(Generated.Lebewesen.Morph morph, Data data)
        {
            this.origin = morph;
            this.Data = data;

            switch (morph.Geschlecht)
            {
                case Generated.Misc.Geschlecht.Neutral:
                    this.Sex = Sex.Neutral;
                    break;
                case Generated.Misc.Geschlecht.Mänlich:
                    this.Sex = Sex.Male;
                    break;
                case Generated.Misc.Geschlecht.Weiblich:
                    this.Sex = Sex.Female;
                    break;
                default:
                    throw new NotSupportedException();
            }

            this.Description = morph.Beschreibung;
            this.Id = morph.Id;
            this.Name = morph.Name;
            this.LifePeriods = morph.Lebensabschnitte.Select(x => new LifePeriodReference(x, data)).ToImmutableArray();

            this.Modification = new ModificationReference(morph.Mods);

        }

        private readonly Generated.Lebewesen.Morph origin;

        public Data Data { get; }
        public Sex Sex { get; }
        public LocalizedString Description { get; }
        public string Id { get; }
        public LocalizedString Name { get; }
        public ImmutableArray<LifePeriodReference> LifePeriods { get; }
        internal ModificationReference Modification { get; }
        public ImmutableArray<PathGroupReference> DefaultPathes { get; private set; }


        void IReference.Initilize(Dictionary<string, TalentReference> directoryTalent, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Dictionary<string, GenusReference> directoryGenus, Dictionary<string, BeingReference> directoryBeing, Dictionary<string, PathGroupReference> directoryPath)
        {
            this.DefaultPathes = this.origin.StandardPfade.Select(x => directoryPath[x.Id]).ToImmutableArray();
            ((IReference)this.Modification).Initilize(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath);
            foreach (IReference item in this.LifePeriods)
                item.Initilize(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath);
        }
    }
}