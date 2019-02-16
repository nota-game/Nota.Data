using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Nota.Data.Generated.Talent;

namespace Nota.Data
{
    public class Data
    {
        private Data(IReadOnlyList<TalentReference> talents, IReadOnlyList<CompetencyReference> competency, IReadOnlyList<TagReference> tags, IReadOnlyList<FeaturesReference> features)
        {
            this.Talents = talents;
            this.Competency = competency;
            this.Tags = tags;
            this.Features = features;
        }

        public static Task<Data> LoadAsync(System.IO.Stream stream)
        {
            return Task.Run(() =>
            {
                var serializer = new XmlSerializer(typeof(Generated.Core.Daten));
                var data = serializer.Deserialize(stream) as Generated.Core.Daten;
                var talentList = new List<TalentReference>();
                var competencyList = new List<CompetencyReference>();
                var tagList = new List<TagReference>();
                var featuresList = new List<FeaturesReference>();



                var output = new Data(talentList.AsReadOnly(), competencyList.AsReadOnly(), tagList.AsReadOnly(), featuresList.AsReadOnly());
                var directoryTalent = new Dictionary<string, TalentReference>();
                foreach (var item in data.Talente.Select(x => new TalentReference(x, output)))
                {
                    talentList.Add(item);
                    directoryTalent.Add(item.Id, item);
                }

                var directoryCompetency = new Dictionary<string, CompetencyReference>();
                foreach (var item in data.Fertigkeiten.Select(x => new CompetencyReference(x, output)))
                {
                    competencyList.Add(item);
                    directoryCompetency.Add(item.Id, item);
                }

                var directoryFeatures = new Dictionary<string, FeaturesReference>();
                foreach (var item in data.Besonderheiten.Select(x => new FeaturesReference(x, output)))
                {
                    featuresList.Add(item);
                    directoryFeatures.Add(item.Id, item);
                }

                var directoryTags = new Dictionary<string, TagReference>();
                foreach (var item in data.Tags.Select(x => new TagReference(x, output)))
                {
                    tagList.Add(item);
                    directoryTags.Add(item.Id, item);
                }


                foreach (var item in output.Features
                    .Concat<IReference>(output.Competency)
                    .Concat(output.Talents)
                    .Concat(output.Tags))
                    item.Initilize(directoryTalent, directoryCompetency, directoryFeatures, directoryTags);






                return output;
            });

        }

        public IReadOnlyList<TalentReference> Talents { get; }
        public IReadOnlyList<CompetencyReference> Competency { get; }
        public IReadOnlyList<TagReference> Tags { get; }
        public IReadOnlyList<FeaturesReference> Features { get; }

        public Task SaveCharacters(Stream stream, IEnumerable<CharacterData> characters)
        {
            return Task.Run(() =>
            {
                var root = new CharacterSerelizer() { Characters = characters.Select(x => x.GetSerelizer()).ToArray() };
                var serelizer = new DataContractSerializer(typeof(CharacterSerelizer));
                serelizer.WriteObject(stream, root);
            });
        }

        public Task<IEnumerable<CharacterData>> LoadCharaters(Stream stream)
        {
            return Task.Run(() =>
            {
                var serelizer = new DataContractSerializer(typeof(CharacterSerelizer));
                var root = serelizer.ReadObject(stream) as CharacterSerelizer;
                var idLookupTalents = this.Talents.ToDictionary(x => x.Id, x => x);
                var idLookupCompetency = this.Competency.ToDictionary(x => x.Id, x => x);

                return root.Characters.Select(cs =>
                {
                    var c = new CharacterData(cs.Id, this)
                    {
                        Name = cs.Name
                    };

                    foreach (var item in cs.AdventureEntrys)
                        c.adventureEntries.Add(new AdventureEntry(item.Title, item.GainedExp, item.Description));

                    foreach (var item in cs.Talents)
                        c.Talent[idLookupTalents[item.Id]].ExpirienceSpent = item.SpentExperience;

                    foreach (var item in cs.Competency)
                        c.Competency[idLookupCompetency[item.Id]].NumberOfAcquisition = item.NumberOfAcquisition;

                    return c;
                }).ToArray() as IEnumerable<CharacterData>;
            });
        }

        [DataContract(Name = "Characters")]
        private class CharacterSerelizer
        {
            [DataMember]
            public CharacterData.Serelizer[] Characters { get; set; }
        }

        public CharacterData CreateCharacter()
        {
            return new CharacterData(Guid.NewGuid(), this);
        }

    }
}
