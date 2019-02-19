using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Nota.Data.Expressions;
using Nota.Data.Generated.Talent;

namespace Nota.Data
{
    public class TalentReference : IEquatable<TalentReference>, IReference
    {
        private readonly TalenteTalent origin;
        private readonly TalenteTalent item;

        public Check Check { get; }

        public LocalizedString Description { get; }

        public Category Category { get; }

        public Compexety Compexety { get; }

        public DerivationCollection Derivation { get; private set; }
        public LocalizedString Name { get; }
        public string Id { get; }
        public Data Data { get; }
        internal ImmutableArray<LevelExpression> Expressions { get; private set; }

        internal TalentReference(TalenteTalent item, Data data)
        {
            this.origin = item;
            this.item = item;
            this.Data = data;
            this.Name = item.Name;
            this.Id = item.Id;
            if (item.BedingungenSpecified)
            {
                // Todo 
            }
            switch (item.Kategorie)
            {
                case Kategorie.Geist:
                    this.Category = Nota.Data.Category.Spirit;
                    break;
                case Kategorie.Körper:
                    this.Category = Nota.Data.Category.Body;
                    break;
                case Kategorie.Gesellschaft:
                    this.Category = Nota.Data.Category.Social;
                    break;
                case Kategorie.Kampf:
                    this.Category = Nota.Data.Category.Combat;
                    break;
                case Kategorie.Wissen:
                    this.Category = Nota.Data.Category.Knowlege;
                    break;
                case Kategorie.Handwerk:
                    this.Category = Nota.Data.Category.Craftsmanship;
                    break;
                default:
                    throw new NotImplementedException($"The Category {item.Kategorie} is not implemented");
            }

            switch (item.Komplexität)
            {
                case "A":
                    this.Compexety = Nota.Data.Compexety.A;
                    break;
                case "B":
                    this.Compexety = Nota.Data.Compexety.B;
                    break;
                case "C":
                    this.Compexety = Nota.Data.Compexety.C;
                    break;
                case "D":
                    this.Compexety = Nota.Data.Compexety.D;
                    break;
                case "E":
                    this.Compexety = Nota.Data.Compexety.E;
                    break;
                case "F":
                    this.Compexety = Nota.Data.Compexety.F;
                    break;
                case "G":
                    this.Compexety = Nota.Data.Compexety.G;
                    break;
                case "H":
                    this.Compexety = Nota.Data.Compexety.H;
                    break;
                case "I":
                    this.Compexety = Nota.Data.Compexety.I;
                    break;
                case "J":
                    this.Compexety = Nota.Data.Compexety.J;
                    break;
                case "K":
                    this.Compexety = Nota.Data.Compexety.K;
                    break;
                default:
                    break;
            }


            this.Description = item.Beschreibung;

            this.Check = new Check(item.Probe);


        }

        void IReference.Initilize(Dictionary<string, TalentReference> directoryTalent, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Dictionary<string, GenusReference> directoryGenus, Dictionary<string, BeingReference> directoryBeing, Dictionary<string, PathGroupReference> directoryPath)
        {
            this.Derivation = GenerateDerivation(this.item.Ableitungen);
            this.Expressions = ImmutableArray.Create(this.origin.Bedingungen.OrderBy(x => x.Wert).Select(x => new LevelExpression(level: x.Wert, expresion: Expresion.GetExpresion(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath, x))).ToArray());


            DerivationCollection GenerateDerivation(AbleitungsAuswahl ableitungen)
            {
                if (ableitungen == null)
                    return new DerivationAll();

                if (ableitungen.MaxSpecified && ableitungen.Max.Count == 1 && !ableitungen.AbleitungSpecified)
                    return GenerateDerivationMax(ableitungen.Max.First());

                var collection = new DerivationAll();
                if (ableitungen.MaxSpecified)
                    foreach (var item in ableitungen.Max)
                        collection.Derivations.Add(GenerateDerivationMax(item));



                if (ableitungen.AbleitungSpecified)
                    foreach (var item in ableitungen.Ableitung)
                        collection.Derivations.Add(new Derivation(SearchTalent(item.Id), item.Anzahl));

                return collection;

                DerivationMax GenerateDerivationMax(Max max)
                {
                    var collection2 = new DerivationMax() { Count = max.Anzahl };

                    if (max.AbleitungSpecified)
                        foreach (var item in max.Ableitung)
                            collection2.Derivations.Add(new Derivation(SearchTalent(item.Id), item.Anzahl));

                    if (max.MaxPropertySpecified)
                        foreach (var item in max.MaxProperty)
                            collection2.Derivations.Add(GenerateDerivationMax(item));

                    return collection2;

                }

                TalentReference SearchTalent(string name) => directoryTalent[name];
            }
        }

        internal class LevelExpression
        {
            public LevelExpression(int level, Expresion expresion)
            {
                this.Level = level;
                this.Expresion = expresion;
            }


            public int Level { get; }
            internal Expresion Expresion { get; }
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TalentReference);
        }

        public bool Equals(TalentReference other)
        {
            return other != null &&
                   EqualityComparer<TalenteTalent>.Default.Equals(this.item, other.item) &&
                   EqualityComparer<Check>.Default.Equals(this.Check, other.Check) &&
                   this.Description == other.Description &&
                   this.Category == other.Category &&
                   this.Compexety == other.Compexety &&
                   this.Name == other.Name;
        }

        public override int GetHashCode()
        {
            var hashCode = 1214502178;
            //hashCode = hashCode * -1521134295 + EqualityComparer<TalenteTalent>.Default.GetHashCode(this.item);
            hashCode = hashCode * -1521134295 + EqualityComparer<Check>.Default.GetHashCode(this.Check);
            //hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Description);
            hashCode = hashCode * -1521134295 + this.Category.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Compexety.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Id);
            return hashCode;
        }

        public static bool operator ==(TalentReference left, TalentReference right)
        {
            return EqualityComparer<TalentReference>.Default.Equals(left, right);
        }

        public static bool operator !=(TalentReference left, TalentReference right)
        {
            return !(left == right);
        }
    }
}

namespace Nota.Data
{
    public enum Attribute
    {
        Antipathie,
        Sypathy,
        Focus,
        Agility,
        Luck,
        Strength,
        Dexterety,
        Courage,
        Constitution,
        Intelligence,
        Intuition
    }
}

namespace Nota.Data
{
    public enum Compexety
    {
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        E = 5,
        F = 6,
        G = 7,
        H = 8,
        I = 9,
        J = 10,
        K = 11
    }
}