using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Nota.Data
{
    public class TalentData : INotifyPropertyChanged
    {
        private int expirienceSpent;

        public int ExpirienceSpent
        {
            get => this.expirienceSpent;
            set
            {
                if (this.expirienceSpent != value)
                {
                    var levelChanges = value >= this.TotalCostForNextLevel || value < this.TotalCostForThisLevel;
                    this.expirienceSpent = value;
                    this.FirePropertyChanged();
                    if (levelChanges)
                    {
                        this.FirePropertyChanged(nameof(this.BaseLevel));
                        this.FirePropertyChanged(nameof(this.TotalCostForNextLevel));
                        this.FirePropertyChanged(nameof(this.TotalCostForThisLevel));
                    }
                    this.FirePropertyChanged(nameof(this.ExpirienceToNextLevel));
                }
            }
        }

        public int ExpirienceToNextLevel => this.TotalCostForNextLevel - this.ExpirienceSpent;

        private int? totalCostForNextLevel;
        public int TotalCostForNextLevel
        {
            get
            {
                if (this.totalCostForNextLevel == null)
                    this.totalCostForNextLevel = TalentExperienceCost.CalculateTotalCostForLevel(this.Reference.Compexety, this.BaseLevel + 1);
                return this.totalCostForNextLevel.Value;
            }
            set => this.totalCostForNextLevel = value;
        }
        private int? totalCostForThisLevel;
        public int TotalCostForThisLevel
        {
            get
            {
                if (this.totalCostForThisLevel == null)
                    this.totalCostForThisLevel = TalentExperienceCost.CalculateTotalCostForLevel(this.Reference.Compexety, this.BaseLevel);
                return this.totalCostForThisLevel.Value;
            }
            set => this.totalCostForNextLevel = value;
        }

        public int Level => this.SupportLevel + this.BaseLevel;

        public int? baseLevel;
        public int BaseLevel
        {
            get
            {
                if (this.baseLevel == null)
                {
                    this.baseLevel = TalentExperienceCost.CalculateLevelFromSpentExpirience(this.Reference.Compexety, this.ExpirienceSpent);
                    this.FirePropertyChanged(nameof(this.Level));
                    this.FirePropertyChanged(nameof(this.TotalCostForNextLevel));
                }
                return this.baseLevel.Value;
            }
        }

        public int? supportLevel;
        private readonly HashSet<TalentReference> allDerivationsTalents;

        public int SupportLevel
        {
            get
            {
                if (this.supportLevel == null)
                {
                    this.supportLevel = GetLavel(this.Reference.Derivation);
                    this.FirePropertyChanged(nameof(this.Level));
                }
                return this.supportLevel.Value;

                int GetLavel(AbstractDerivation derivations)
                {
                    switch (derivations)
                    {
                        case DerivationAll all:
                            return all.Derivations.Select(GetLavel).Sum();
                        case DerivationMax max:
                            return max.Derivations.Select(GetLavel).OrderByDescending(x => x).Take(max.Count).Sum();
                        case Derivation derivation:
                            if (!this.Character.Talent.ContainsKey(derivation.Talent))
                                return 0;
                            var talentValue = this.Character.Talent[derivation.Talent]?.BaseLevel ?? 0;
                            return talentValue / derivation.Count;
                        default:
                            throw new NotImplementedException($"The type {derivations?.GetType().FullName ?? "<null>"} is not implemented. :/");
                    }
                }
            }
        }



        public TalentData(TalentReference reference, CharacterData character)
        {
            this.Reference = reference;
            this.Character = character;
            this.Character.TalentChanging += this.CharacterTalentChangin;

            this.allDerivationsTalents = new HashSet<TalentReference>(GetDerivations(reference.Derivation));

            IEnumerable<TalentReference> GetDerivations(AbstractDerivation derivations)
            {
                switch (derivations)
                {
                    case DerivationAll all:
                        return all.Derivations.SelectMany(GetDerivations);
                    case DerivationMax max:
                        return max.Derivations.SelectMany(GetDerivations);
                    case Derivation derivation:

                        return Enumerable.Repeat(derivation.Talent, 1);
                    default:
                        throw new NotImplementedException($"The type {derivations?.GetType().FullName ?? "<null>"} is not implemented. :/");
                }
            }
        }

        private void CharacterTalentChangin(TalentData obj)
        {
            if (obj == this)
                return;
            if (this.allDerivationsTalents.Contains(obj.Reference))
                this.FirePropertyChanged(nameof(this.SupportLevel));

        }

        public TalentReference Reference { get; }
        public CharacterData Character { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void FirePropertyChanged([CallerMemberName]string proeprty = null)
        {
            // When a cached property changes, we want to get its new value to guarantee that other Property Changes will fireie that are triggerd on recalculating the cached values..
            switch (proeprty)
            {
                case nameof(this.TotalCostForNextLevel):
                    this.totalCostForNextLevel = null;
                    _ = this.TotalCostForNextLevel;
                    break;
                case nameof(this.TotalCostForThisLevel):
                    this.totalCostForThisLevel = null;
                    _ = this.TotalCostForThisLevel;
                    break;
                case nameof(this.BaseLevel):
                    this.baseLevel = null;
                    _ = this.BaseLevel;
                    break;
                case nameof(this.SupportLevel):
                    this.supportLevel = null;
                    _ = this.SupportLevel;
                    break;

                default:
                    break;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proeprty));
        }

    }
}
