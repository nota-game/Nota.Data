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
        private Data(IReadOnlyList<TalentReference> talents)
        {
            this.Talents = talents;
        }

        public static Task<Data> LoadAsync(System.IO.Stream stream)
        {
            return Task.Run(() =>
            {
                var serializer = new XmlSerializer(typeof(Generated.Core.Daten));
                var data = serializer.Deserialize(stream) as Generated.Core.Daten;
                var talentList = new List<TalentReference>();



                var output = new Data(talentList.AsReadOnly());
                var directory = new Dictionary<string, TalentReference>();
                foreach (var item in data.Talente.Select(x => new TalentReference(x)).OrderBy(x => x.Category).ThenBy(x => x.Id).Reverse())
                {
                    talentList.Add(item);
                    directory.Add(item.Id, item);
                }
                foreach (var item in output.Talents)
                    item.InitilizeDerivation(directory);




                return output;
            });

        }

        public IReadOnlyList<TalentReference> Talents { get; }

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
                var idLookup = this.Talents.ToDictionary(x => x.Id, x => x);

                return root.Characters.Select(cs =>
                {
                    var c = new CharacterData(cs.Id, this)
                    {
                        Name = cs.Name
                    };

                    foreach (var item in cs.AdventureEntrys)
                        c.adventureEntries.Add(new AdventureEntry(item.Title, item.GainedExp, item.Description));

                    foreach (var item in cs.Talents)
                        c.Talent[idLookup[item.Id]].ExpirienceSpent = item.SpentExperience;

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
