using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Nota.Data.Generated.Fertigkeit;
using Nota.Data.Generated.Misc;

namespace Nota.Data
{
    public class CompetencyReference
    {

        public CompetencyReference(FertigkeitenFertigkeit x)
        {

            this.Description = x.Beschreibung;
            this.Id = x.Id;
            this.Name = x.Name;
            this.Tags = x.Tags.Select(y => y.Id).ToList().AsReadOnly();

            this.Cost = x.Kosten;
            this.ersetzt = x.Ersetzt?.Fertigkeit.Id;
        }

        public LocalizedString Description { get; }
        public string Id { get; }
        public LocalizedString Name { get; }
        public ReadOnlyCollection<string> Tags { get; }
        public int Cost { get; }
        public CompetencyReference Replaces { get; private set; }

        private readonly string ersetzt;

        internal void InitilizeReplacement(Dictionary<string, CompetencyReference> directoryCompetency)
        {
            if (this.ersetzt != null)
                this.Replaces = directoryCompetency[this.ersetzt];
        }
    }
}