using System.Collections.Generic;
using System.Collections.ObjectModel;
using Nota.Data.Generated.Lebewesen;
using Nota.Data.Generated.Misc;

namespace Nota.Data
{
    internal class LifePeriodReference : IReference
    {
        private readonly Lebensabschnitt origin;

        public LifePeriodReference(Lebensabschnitt x, Data data)
        {
            this.origin = x;
            this.Data = data;

            this.Modifications = new Modification(x.Mods);
            this.Description = x.Beschreibung;
            this.Age = new Range(x.StartAlter, x.EndAlter);
            this.BMI = new Range(x.MinBMI, x.MaxBMI);
            this.Size = new Range(x.MinGröße, x.MaxGröße);
            this.Id = x.Id;
            this.Name = x.Name;
            //this. = x.Spielbar.;

        }

        public static LifePeriodReference Create(Lebensabschnitt x, Data data)
        {
            if (x.Spielbar != null)
                return new LifePeriodPlayableReference(x, data);
            else
                return new LifePeriodReference(x, data);
        }

        public Data Data { get; }
        public Modification Modifications { get; }
        public LocalizedString Description { get; }
        public Range Age { get; }
        public Range BMI { get; }
        public Range Size { get; }
        public string Id { get; }
        public LocalizedString Name { get; }

        void IReference.Initilize(Dictionary<string, TalentReference> directoryTalent, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Dictionary<string, GenusReference> directoryGenus, Dictionary<string, BeingReference> directoryBeing)
        {
        }
    }

    internal class LifePeriodPlayableReference : LifePeriodReference
    {
        public LifePeriodPlayableReference(Lebensabschnitt x, Data data) : base(x, data)
        {
            this.GenerationCost = x.Spielbar.Kosten;
        }

        public int GenerationCost { get; }
    }
}