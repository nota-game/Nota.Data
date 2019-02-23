using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Nota.Data.Generated.Talent;
using Nota.Data.References;

namespace Nota.Data
{
    public class Data
    {
        private Data()
        {
            //this.Talents = talents;
            //this.Competency = competency;
            //this.Tags = tags;
            //this.Features = features;
            //this.Beings = beings;
            //this.Genus = genus;
            //this.Path = path;
        }

        private void Initilize(ImmutableArray<TalentReference> talents, ImmutableArray<CompetencyReference> competency, ImmutableArray<TagReference> tags, ImmutableArray<FeaturesReference> features, ImmutableArray<BeingReference> beings, ImmutableArray<GenusReference> genus, ImmutableArray<PathGroupReference> path)
        {
            this.Talents = talents;
            this.Competency = competency;
            this.Tags = tags;
            this.Features = features;
            this.Beings = beings;
            this.Genus = genus;
            this.Path = path;
        }

        public ImmutableArray<BeingReference> Beings { get; private set; }
        public ImmutableArray<GenusReference> Genus { get; private set; }
        public ImmutableArray<PathGroupReference> Path { get; private set; }

        public static Task<Data> LoadAsync(System.IO.Stream stream)
        {
            return Task.Run(() =>
            {
                var serializer = new XmlSerializer(typeof(Generated.Core.Daten));
                var data = serializer.Deserialize(stream) as Generated.Core.Daten;
                var talentList = new List<TalentReference>();
                var beingList = new List<BeingReference>();
                var competencyList = new List<CompetencyReference>();
                var tagList = new List<TagReference>();
                var genusList = new List<GenusReference>();
                var pathList = new List<PathGroupReference>();
                var featuresList = new List<FeaturesReference>();

                var output = new Data();

                var directoryBeing = new Dictionary<string, BeingReference>();
                foreach (var item in data.Organismen.Organismus.Select(x => new BeingReference(x, output)))
                {
                    beingList.Add(item);
                    directoryBeing.Add(item.Id, item);
                }

                var directoryGenus = new Dictionary<string, GenusReference>();
                foreach (var item in data.Organismen.Gattung.Select(x => new GenusReference(x, output)))
                {
                    genusList.Add(item);
                    directoryGenus.Add(item.Id, item);
                }

                var directoryPath = new Dictionary<string, PathGroupReference>();
                foreach (var item in data.PfadGruppen.Select(x => new PathGroupReference(x, output)))
                {
                    pathList.Add(item);
                    directoryPath.Add(item.Id, item);
                }

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

                output.Initilize(talentList.ToImmutableArray(), competencyList.ToImmutableArray(), tagList.ToImmutableArray(), featuresList.ToImmutableArray(), beingList.ToImmutableArray(), genusList.ToImmutableArray(), pathList.ToImmutableArray());

                foreach (var item in output.Features
                    .Concat<IReference>(output.Competency)
                    .Concat(output.Talents)
                    .Concat(output.Tags)
                    .Concat(output.Beings)
                    .Concat(output.Genus)
                    .Concat(output.Path))
                    item.Initilize(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath);

                return output;
            });

        }

        public ImmutableArray<TalentReference> Talents { get; private set; }
        public ImmutableArray<CompetencyReference> Competency { get; private set; }
        public ImmutableArray<TagReference> Tags { get; private set; }
        public ImmutableArray<FeaturesReference> Features { get; private set; }

        public CharacterBuilder CreateCharacter()
        {
            return new CharacterBuilder(Guid.NewGuid(), this);
        }

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
                    try
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
                    }
                    catch (Exception)
                    {

                        return null;
                    }
                }).Where(x => x != null).ToArray() as IEnumerable<CharacterData>;
            });
        }

        [DataContract(Name = "Characters")]
        private class CharacterSerelizer
        {
            [DataMember]
            public CharacterData.Serelizer[] Characters { get; set; }
        }



    }
}
