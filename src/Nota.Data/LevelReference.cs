using System.Collections.Generic;
using Nota.Data.Generated.Misc;

namespace Nota.Data
{
    public class LevelReference : IReference
    {

        internal LevelReference(LevelsLevel origin, Data data)
        {
            this.orign = origin;
            this.Data = data;
            this.GenerationCost = this.orign.Kosten;
            this.PathCost = this.orign.PfadKosten;
            this.Id = this.orign.Id;

            orign.Bedingungen;
            orign.Beschreibung;
            orign.Name;


        }

        private readonly LevelsLevel orign;

        public Data Data { get; }
        public int GenerationCost { get; }
        public string PathCost { get; }
        public string Id { get; }

        void IReference.Initilize(Dictionary<string, TalentReference> directoryTalent, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Dictionary<string, GenusReference> directoryGenus, Dictionary<string, BeingReference> directoryBeing, Dictionary<string, PathGroupReference> directoryPath)
        {
            orign.Besonderheit;
            orign.Fertigkeit;
            orign.Tag;
            orign.Talent;
        }
    }
}