﻿using System;
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
            private set
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
                    this.FirePropertyChanged(nameof(this.BestNextLevel));
                }
            }
        }

        public DataAction<TalentData, int> Increase { get; }

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
                    this.FirePropertyChanged(nameof(this.BestNextLevel));
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
                            var talentValue = this.Character.Talent[derivation.Talent]?.BaseLevel ?? 0;
                            return talentValue / derivation.Count;
                        default:
                            throw new NotImplementedException($"The type {derivations?.GetType().FullName ?? "<null>"} is not implemented. :/");
                    }
                }
            }
        }

        public struct NextLevelCost : IEquatable<NextLevelCost>
        {

            public NextLevelCost((TalentData talent, int level, int cost) value) : this(value.talent, value.level, value.cost) { }

            public NextLevelCost(TalentData talent, int level, int cost)
            {
                this.Talent = talent ?? throw new ArgumentNullException(nameof(talent));
                this.Level = level;
                this.Cost = cost;
            }

            public TalentData Talent { get; }
            public int Level { get; }
            public int Cost { get; }

            public override bool Equals(object obj)
            {
                return obj is NextLevelCost cost && this.Equals(cost);
            }

            public bool Equals(NextLevelCost other)
            {
                return EqualityComparer<TalentData>.Default.Equals(this.Talent, other.Talent) &&
                       this.Level == other.Level &&
                       this.Cost == other.Cost;
            }

            public override int GetHashCode()
            {
                var hashCode = 1751525848;
                hashCode = hashCode * -1521134295 + EqualityComparer<TalentData>.Default.GetHashCode(this.Talent);
                hashCode = hashCode * -1521134295 + this.Level.GetHashCode();
                hashCode = hashCode * -1521134295 + this.Cost.GetHashCode();
                return hashCode;
            }

            public static bool operator ==(NextLevelCost left, NextLevelCost right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(NextLevelCost left, NextLevelCost right)
            {
                return !(left == right);
            }
        }

        private IEnumerable<NextLevelCost> bestNextLevel;

        public IEnumerable<NextLevelCost> BestNextLevel
        {
            get
            {
                if (this.bestNextLevel == null)
                {
                    this.bestNextLevel = CostForBestNextLevel().ToArray();
                }
                return this.bestNextLevel;

                IEnumerable<NextLevelCost> CostForBestNextLevel()
                {
                    var target = GetCurrentIncrease(this.Reference.Derivation) + 1;
                    var nextSupportLevel = GetLavel(this.Reference.Derivation, target);

                    return nextSupportLevel.Concat(Enumerable.Repeat(new NextLevelCost(this.Character.Talent[this.Reference], this.BaseLevel + 1, this.ExpirienceToNextLevel), 1)).OrderBy(x => x.Cost);

                    IEnumerable<NextLevelCost> GetLavel(AbstractDerivation derivations, int targetPoints)
                    {
                        var currentPoints = GetCurrentIncrease(derivations);
                        var pointsToAdd = targetPoints - currentPoints;
                        if (pointsToAdd > 1)
                        {
                            //TODO: we can increase different talents instead of one to get cheaper.
                            // But I need to think how we do that. Maybe multiple callse with an increase of 1?
                            // We should do that recursive?
                        }
                        switch (derivations)
                        {
                            case DerivationAll all:

                                return all.Derivations.SelectMany(x =>
                                {
                                    var thisIncrease = GetCurrentIncrease(x);
                                    return GetLavel(x, thisIncrease + pointsToAdd);
                                });
                            case DerivationMax max:



                                var topValues = max.Derivations.OrderByDescending(GetCurrentIncrease).Take(max.Count).ToArray();
                                var bottomValues = max.Derivations.OrderByDescending(GetCurrentIncrease).Skip(max.Count).ToArray();

                                var minimumOfTop = topValues.Min(GetCurrentIncrease);

                                // increase for top
                                var topIncrease = topValues.SelectMany(x =>
                                {
                                    var thisIncrease = GetCurrentIncrease(x);
                                    return GetLavel(x, thisIncrease + pointsToAdd);
                                });

                                var bottomeIncrease = bottomValues.SelectMany(x =>
                                {
                                    return GetLavel(x, minimumOfTop + pointsToAdd);
                                });

                                return topIncrease.Concat(bottomeIncrease);
                            case Derivation derivation:

                                var currentBaseLevel = this.Character.Talent[derivation.Talent]?.BaseLevel ?? 0;
                                var currentInvestment = this.Character.Talent[derivation.Talent]?.ExpirienceSpent ?? 0;
                                var neededInvestment = TalentExperienceCost.CalculateTotalCostForLevel(derivation.Talent.Compexety, (targetPoints * derivation.Count));

                                return Enumerable.Repeat(new NextLevelCost(this.Character.Talent[derivation.Talent], targetPoints * derivation.Count, neededInvestment - currentBaseLevel), 1);
                            default:
                                throw new NotImplementedException($"The type {derivations?.GetType().FullName ?? "<null>"} is not implemented. :/");
                        }
                    }

                    int GetCurrentIncrease(AbstractDerivation derivations)
                    {
                        switch (derivations)
                        {
                            case DerivationAll all:
                                return all.Derivations.Select(GetCurrentIncrease).Sum();

                            case DerivationMax max:
                                return max.Derivations.Select(GetCurrentIncrease).OrderByDescending(x => x).Take(max.Count).Sum();

                            case Derivation derivation:
                                var currentBaseLevel = this.Character.Talent[derivation.Talent]?.BaseLevel ?? 0;
                                var currentBonus = (currentBaseLevel / derivation.Count);
                                return currentBonus;

                            default:
                                throw new NotImplementedException($"The type {derivations?.GetType().FullName ?? "<null>"} is not implemented. :/");
                        }
                    }
                }
            }
        }

        public TalentData(TalentReference reference, CharacterData character)
        {
            this.Reference = reference;
            this.Character = character;
            this.Character.TalentChanging += this.CharacterTalentChangin;
            this.Character.PropertyChanged += this.Character_PropertyChanged;
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

            Increase = new DataAction<TalentData, int>(this.Character, this,
                (p, increase) =>
                {
                    p.ExpirienceSpent += increase;
                },
                (p, decrease) =>
                {
                    p.ExpirienceSpent -= decrease;
                },
                (p, toIncrease) =>
                {
                    return toIncrease <= p.Character.ExpirienceAvailable;
                });
        }

        private void Character_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.Character.ExpirienceAvailable))
                Increase.FireCanExecuteChanged();
        }

        private void CharacterTalentChangin(TalentData obj)
        {
            if (obj == this)
                return;
            if (this.allDerivationsTalents.Contains(obj.Reference))
            {
                this.FirePropertyChanged(nameof(this.SupportLevel));
                this.FirePropertyChanged(nameof(this.BestNextLevel));
            }

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
                case nameof(this.BestNextLevel):
                    this.bestNextLevel = null;
                    _ = this.BestNextLevel;
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
