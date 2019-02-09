using System;
using System.Collections.Generic;
using System.Linq;
using Nota.Data.Generated.Talent;

namespace Nota.Data
{
    public class TalentReference
    {
        private readonly TalenteTalent item;

        public Check Check { get; }

        public string Description { get; }

        public Category Category { get; }

        public Compexety Compexety { get; }

        public DerivationCollection Derivation { get; private set; }
        public string Name { get; set; }



        internal TalentReference(TalenteTalent item)
        {
            this.item = item;
            this.Name = item.Name;

            if (item.BedingugenSpecified)
            {
                // Todo 
            }
            switch (item.Kategorie)
            {
                case Kategorie.Geist:
                    Category = Nota.Data.Category.Spirit;
                    break;
                case Kategorie.Körper:
                    Category = Nota.Data.Category.Body;
                    break;
                case Kategorie.Gesellschaft:
                    Category = Nota.Data.Category.Social;
                    break;
                case Kategorie.Kampf:
                    Category = Nota.Data.Category.Combat;
                    break;
                case Kategorie.Wissen:
                    Category = Nota.Data.Category.Knowlege;
                    break;
                case Kategorie.Handwerk:
                    Category = Nota.Data.Category.Craftsmanship;
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

            if (item.BedingugenSpecified)
                this.Description = item.Beschreibung;

            this.Check = new Check(item.Probe);


        }

        internal void InitilizeDerivation(Dictionary<string, TalentReference> talentLookup)
        {
            this.Derivation = GenerateDerivation(this.item.Ableitungen);


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
                        collection.Derivations.Add(new Derivation(SearchTalent(item.Name), item.Anzahl));

                return collection;

                DerivationMax GenerateDerivationMax(Max max)
                {
                    var collection2 = new DerivationMax();

                    if (max.AbleitungSpecified)
                        foreach (var item in max.Ableitung)
                            collection2.Derivations.Add(new Derivation(SearchTalent(item.Name), item.Anzahl));

                    if (max.MaxPropertySpecified)
                        foreach (var item in max.MaxProperty)
                            collection2.Derivations.Add(GenerateDerivationMax(item));

                    return collection2;

                }

                TalentReference SearchTalent(string name) => talentLookup[name];
            }
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
        A=1,
        B=2,
        C=3,
        D=4,
        E=5,
        F=6,
        G=7,
        H=8,
        I=9,
        J=10,
        K=11
    }
}