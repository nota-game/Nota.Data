using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using Nota.Data.Generated.Lebewesen;
using Nota.Data.Generated.Misc;

namespace Nota.Data.References
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
                case Generated.Misc.Geschlecht.Unspezifiziert:
                    this.Sex = Sex.Unspecified;
                    break;
                default:
                    throw new NotSupportedException();
            }

            this.Description = morph.Beschreibung;
            this.Id = morph.Id;
            this.Name = morph.Name;
            this.LifePeriods = morph.Lebensabschnitte.Select(x => new LifePeriodReference(x, data)).ToImmutableArray();
            this.Attributes = new AttributeStore(morph.Eigenschaften);


        }

        private readonly Generated.Lebewesen.Morph origin;

        public Data Data { get; }
        public Sex Sex { get; }
        public LocalizedString Description { get; }
        public string Id { get; }
        public LocalizedString Name { get; }
        public ImmutableArray<LifePeriodReference> LifePeriods { get; }
        public AttributeStore Attributes { get; }
        internal ModificationReference Modification { get; }
        public ImmutableArray<PathGroupReference> DefaultPathes { get; private set; }
        public ImmutableArray<FeaturesReference> Features { get; private set; }

        void IReference.Initilize(Dictionary<string, TalentReference> directoryTalent, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Dictionary<string, GenusReference> directoryGenus, Dictionary<string, BeingReference> directoryBeing, Dictionary<string, PathGroupReference> directoryPath)
        {
            this.DefaultPathes = this.origin.StandardPfade.Select(x => directoryPath[x.Id]).ToImmutableArray();
            this.Features = this.origin.BesonderheitenSpecified
              ? this.origin.Besonderheiten.Select(x => directoryFeatures[x.Id]).ToImmutableArray()
              : ImmutableArray.Create<FeaturesReference>();

            ((IReference)this.Modification).Initilize(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath);
            foreach (IReference item in this.LifePeriods)
                item.Initilize(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath);
        }

        public sealed class AttributeStore
        {

            public AttributeStore(MorphEigenschaften eigenschaften)
            {
                this.Points = eigenschaften.Punkte;
                this.Sympathy = new AttributeRange(eigenschaften.Sympathie);
                this.Strength = new AttributeRange(eigenschaften.Stärke);
                this.Courage = new AttributeRange(eigenschaften.Mut);
                this.Constitution = new AttributeRange(eigenschaften.Konstitution);
                this.Intelegence = new AttributeRange(eigenschaften.Klugheit);
                this.Intuition = new AttributeRange(eigenschaften.Intuition);
                this.Luck = new AttributeRange(eigenschaften.Glück);
                this.Agility = new AttributeRange(eigenschaften.Gewandtheit);
                this.Dexterety = new AttributeRange(eigenschaften.Feinmotorik);
                this.Antipathy = new AttributeRange(eigenschaften.Antipathie);

            }

       

            public int Points { get; }
            public AttributeRange Sympathy { get; }
            public AttributeRange Strength { get; }
            public AttributeRange Courage { get; }
            public AttributeRange Constitution { get; }
            public AttributeRange Intelegence { get; }
            public AttributeRange Intuition { get; }
            public AttributeRange Luck { get; }
            public AttributeRange Agility { get; }
            public AttributeRange Dexterety { get; }
            public AttributeRange Antipathy { get; }

            public sealed class AttributeRange
            {

                public int Min { get; }
                public int Max { get; }
                public AttributeRange(EigenschaftsWert sympathie)
                {
                    this.Min = sympathie.Minimum;
                    this.Max = sympathie.Maximum;
                }
            }
        }

    }
}