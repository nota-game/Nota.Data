using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using Nota.Data.Generated.Lebewesen;
using Nota.Data.Generated.Misc;

namespace Nota.Data
{
    public class BeingReference : IReference
    {

        public BeingReference(Generated.Lebewesen.OrganismenOrganismus being, Data data)
        {
            this.origin = being;
            this.Data = data;
            this.Id = being.Id;
            this.Name = being.Name;
            this.Description = being.Beschreibung;

            this.Species = being.Art;

            this.Attributes = new AttributeStore(being.Eigenschaften);

            this.Morphs = being.Morphe.Select(morph => new MorphReference(morph, data)).ToImmutableArray();
        }

        private readonly Generated.Lebewesen.OrganismenOrganismus origin;

        public Data Data { get; }
        public string Id { get; }
        public LocalizedString Name { get; }
        public LocalizedString Description { get; }
        public LocalizedString Species { get; }
        private AttributeStore Attributes { get; }
        public ImmutableArray<MorphReference> Morphs { get; }
        public ImmutableArray<FeaturesReference> Features { get; private set; }
        public GenusReference Gattung { get; private set; }
        public ImmutableArray<PathGroupReference> DefaultPathes { get; private set; }

        void IReference.Initilize(Dictionary<string, TalentReference> directoryTalent, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Dictionary<string, GenusReference> directoryGenus, Dictionary<string, BeingReference> directoryBeing, Dictionary<string, PathGroupReference> directoryPath)
        {
            this.Features = this.origin.BesonderheitenSpecified
                ? this.origin.Besonderheiten.Select(x => directoryFeatures[x.Id]).ToImmutableArray()
                : ImmutableArray.Create<FeaturesReference>();

            foreach (IReference item in this.Morphs)
                item.Initilize(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath);

            this.Gattung = directoryGenus[this.origin.Gattung];
            this.DefaultPathes = this.origin.StandardPfade.Select(x => directoryPath[x.Id]).ToImmutableArray();
        }

        public sealed class AttributeStore
        {

            public AttributeStore(OrganismenOrganismusEigenschaften eigenschaften)
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