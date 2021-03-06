﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using Nota.Data.Generated.Lebewesen;
using Nota.Data.Generated.Misc;

namespace Nota.Data.References
{
    public class LifePeriodReference : IReference
    {
        protected readonly Lebensabschnitt origin;

        private protected LifePeriodReference(Lebensabschnitt x, Data data, MorphReference morphReference)
        {
            this.origin = x;
            this.Data = data;
            this.MorphReference = morphReference;
            this.Modifications = new ModificationReference(x.Mods);
            this.Description = x.Beschreibung;
            this.Age = new Range(x.StartAlter, x.EndAlter);
            this.BMI = new Range(x.MinBMI, x.MaxBMI);
            this.Size = new Range(x.MinGröße, x.MaxGröße);
            this.Id = x.Id;
            this.Name = x.Name;
            
        }

        public static LifePeriodReference Create(Lebensabschnitt x, Data data, MorphReference morphReference)
        {
            if (x.Spielbar != null)
                return new LifePeriodPlayableReference(x, data, morphReference);
            else
                return new LifePeriodReference(x, data, morphReference);
        }

        void IReference.Initilize(Dictionary<string, TalentReference> directoryTalent, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Dictionary<string, GenusReference> directoryGenus, Dictionary<string, OrganismReference> directoryBeing, Dictionary<string, PathGroupReference> directoryPathGroup, Dictionary<string, PathReference> directoryPath)
        {
            ((IReference)this.Modifications).Initilize(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPathGroup, directoryPath);
        }

        public Data Data { get; }
        public MorphReference MorphReference { get; }
        public ModificationReference Modifications { get; }
        public LocalizedString Description { get; }
        public Range Age { get; }
        public Range BMI { get; }
        public Range Size { get; }
        public string Id { get; }
        public LocalizedString Name { get; }

    }

    public class LifePeriodPlayableReference : LifePeriodReference, IReference
    {
        public LifePeriodPlayableReference(Lebensabschnitt x, Data data, MorphReference morphReference) : base(x, data,morphReference)
        {
            this.GenerationCost = x.Spielbar.Kosten;
            
        }

        public int GenerationCost { get; }
        public ImmutableDictionary<PathGroupReference, int> PathPoints { get; private set; }

        void IReference.Initilize(Dictionary<string, TalentReference> directoryTalent, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Dictionary<string, GenusReference> directoryGenus, Dictionary<string, OrganismReference> directoryBeing, Dictionary<string, PathGroupReference> directoryPathGroup, Dictionary<string, PathReference> directoryPath)
        {
            if (this.origin.Spielbar.PfadPunkteSpecified)
                this.PathPoints = this.origin.Spielbar.PfadPunkte.ToImmutableDictionary(x => directoryPathGroup[x.Pfade], x => x.Punkte);
            else
                this.PathPoints = ImmutableDictionary.Create<PathGroupReference, int>();
        }
    }
}