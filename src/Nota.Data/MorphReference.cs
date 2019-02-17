using System;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using Nota.Data.Generated.Misc;

namespace Nota.Data
{
    public class MorphReference
    {

        public MorphReference(Generated.Lebewesen.Morph morph, Data data)
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
            
            this.Modification = new Modification(morph.Mods);
            morph.StandardPfade;
            
        }

        private readonly Generated.Lebewesen.Morph origin;

        public Data Data { get; }
        public Sex Sex { get; }
        public LocalizedString Description { get; }
        public string Id { get; }
        public LocalizedString Name { get; }
        public ImmutableArray<LifePeriodReference> LifePeriods { get; }
        internal Modification Modification { get; }
    }
}