using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using Nota.Data.Expressions;
using Nota.Data.References;

namespace Nota.Data
{
    public class TalentData : INotifyPropertyChanged, IInitilizable
    {

        private int expirienceSpent;

        public int ExpirienceSpent
        {
            get => this.expirienceSpent;
            internal set
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

        public DataAction<TalentData, int, IncreaseResult> Increase { get; }

        public int ExpirienceToNextLevel => this.TotalCostForNextLevel - this.ExpirienceSpent;

        private int? totalCostForNextLevel;
        public int TotalCostForNextLevel
        {
            get
            {
                if (this.totalCostForNextLevel == null)
                {
                    this.totalCostForNextLevel = TalentExperienceCost.CalculateTotalCostForLevel(this.Reference.Compexety, this.BaseLevel + 1);
                    this.FirePropertyChanged(nameof(this.ExpirienceToNextLevel));
                }
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

        public int BaseLevel
        {
            get
            {
                if (this.CurrentProblems.Any())
                    return this.CurrentProblems.First().Level - 1;
                return this.BaseLevelWithoutProblems;
            }
        }

        public int? baseLevelWithoutProblems;
        public int BaseLevelWithoutProblems
        {
            get
            {
                if (this.baseLevelWithoutProblems == null)
                {
                    this.baseLevelWithoutProblems = TalentExperienceCost.CalculateLevelFromSpentExpirience(this.Reference.Compexety, this.ExpirienceSpent);

                    this.FirePropertyChanged(nameof(this.BaseLevel));
                    this.FirePropertyChanged(nameof(this.TotalCostForNextLevel));
                    this.FirePropertyChanged(nameof(this.Level));

                }
                return this.baseLevelWithoutProblems.Value;
            }
        }

        public int? supportLevel;

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

        public readonly struct NextLevelCost : IEquatable<NextLevelCost>
        {


            public NextLevelCost(TalentData talent, int level, int cost, ImmutableArray<Expressions.Result> problem)
            {
                this.Talent = talent ?? throw new ArgumentNullException(nameof(talent));
                this.Level = level;
                this.Cost = cost;
                this.Problem = problem;
            }

            public TalentData Talent { get; }
            public int Level { get; }
            public int Cost { get; }
            public ImmutableArray<Expressions.Result> Problem { get; }

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

        private ImmutableArray<NextLevelCost>? bestNextLevel;

        public ImmutableArray<NextLevelCost> BestNextLevel
        {
            get
            {
                if (this.bestNextLevel == null)
                {
                    this.bestNextLevel = CostForBestNextLevel().Where(x => x.Cost > 0).ToImmutableArray();
                }
                return this.bestNextLevel.Value;

                IEnumerable<NextLevelCost> CostForBestNextLevel()
                {
                    var target = GetCurrentIncrease(this.Reference.Derivation) + 1;
                    var nextSupportLevel = GetLavel(this.Reference.Derivation, target);
                    var selfProbles = this.CurrentProblems;
                    if (this.NextProblem?.Level == this.BaseLevel + 1) // if the level is less then it will already be included in `CurrentProblems`
                        selfProbles = selfProbles.Add(this.NextProblem.Value);
                    var selfIncrease = new NextLevelCost(this.Character.Talent[this.Reference], this.BaseLevel + 1, this.ExpirienceToNextLevel, selfProbles.Select(x => x.Problem).ToImmutableArray());
                    return nextSupportLevel.Concat(Enumerable.Repeat(selfIncrease, 1)).OrderBy(x => x.Cost);

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
                                var currentProblems = this.Character.Talent[derivation.Talent]?.GetProblemsForLevel(targetPoints * derivation.Count).Select(x => x.Problem).ToImmutableArray() ?? ImmutableArray.Create<Result>();

                                return Enumerable.Repeat(new NextLevelCost(this.Character.Talent[derivation.Talent], targetPoints * derivation.Count, neededInvestment - currentBaseLevel, currentProblems), 1);
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

        public ProblemDetails? NextProblem
        {
            get
            {
                if (this.LevelProblem.Length > 0)
                    return this.LevelProblem[0];
                return null;
            }
        }

        private ImmutableArray<ProblemDetails>? currentProblems;
        public ImmutableArray<ProblemDetails> CurrentProblems
        {
            get
            {
                if (this.currentProblems == null)
                {

                    this.currentProblems = this.GetProblemsForLevel(this.BaseLevelWithoutProblems);

                    this.FirePropertyChanged(nameof(this.BaseLevel));
                }
                return this.currentProblems.Value;
            }
        }

        public ImmutableArray<ProblemDetails> GetProblemsForLevel(int level)
        {
            ImmutableArray<ProblemDetails> problemsForLevel;
            int? indexWithoutProblems = null;
            for (int i = 0; i < this.LevelProblem.Length; i++)
            {
                if (this.LevelProblem.ItemRef(i).Level > level)
                {
                    indexWithoutProblems = i;
                    break;
                }
            }
            if (indexWithoutProblems.HasValue)
                problemsForLevel = this.LevelProblem.RemoveRange(indexWithoutProblems.Value, this.LevelProblem.Length - indexWithoutProblems.Value);
            else
                problemsForLevel = this.LevelProblem;
            return problemsForLevel;
        }

        public readonly struct ProblemDetails
        {
            public ProblemDetails(Result problem, int level)
            {
                this.Problem = problem ?? throw new ArgumentNullException(nameof(problem));
                this.Level = level;
            }

            public Expressions.Result Problem { get; }
            public int Level { get; }

        }

        private ImmutableArray<ProblemDetails>? levelProblem;
        internal ImmutableArray<ProblemDetails> LevelProblem
        {
            get
            {
                if (this.levelProblem == null)
                {
                    this.levelProblem = this.Reference.Expressions
                        .Select(x => new ProblemDetails(x.Expresion.Evaluate(this.Character), x.Level))
                        .Where(x => !x.Problem)
                        .ToImmutableArray();
                    this.FirePropertyChanged(nameof(this.NextProblem));
                    this.FirePropertyChanged(nameof(this.CurrentProblems));
                    this.FirePropertyChanged(nameof(this.BaseLevel));
                    this.FirePropertyChanged(nameof(this.BestNextLevel));
                    this.FirePropertyChanged(nameof(this.ExpirienceToNextLevel));
                    this.FirePropertyChanged(nameof(this.TotalCostForNextLevel));
                    this.Increase.FireCanExecuteChanged();
                }
                return this.levelProblem.Value;
            }
        }

        public TalentData(TalentReference reference, CharacterData character)
        {
            this.Reference = reference;
            this.Character = character;
            this.Character.PropertyChanged += this.Character_PropertyChanged;


            this.Increase = new DataAction<TalentData, int, IncreaseResult>(this.Character, this,
                (p, increase) =>
                {
                    var oldLevel = p.Level;
                    p.ExpirienceSpent += increase;
                    var newLevel = p.Level;
                    return new IncreaseResult(oldLevel, newLevel);
                },
                (p, decrease, r) =>
                {
                    p.ExpirienceSpent -= decrease;
                },
                (p, toIncrease, r) =>
                {
                    string levelInformation;
                    if (r.OldLevel == r.NewLevel)
                        levelInformation = "kein Levelanstieg";
                    else
                        levelInformation = $"von {r.OldLevel} auf {r.NewLevel}";
                    return $"{toIncrease} AP in Talent {p.Reference.Name} investiert ({levelInformation})";
                },
                (p, toIncrease) =>
                {
                    var newLevel = TalentExperienceCost.CalculateLevelFromSpentExpirience(this.Reference.Compexety, toIncrease + this.ExpirienceSpent);
                    return toIncrease <= p.Character.ExpirienceAvailable
                           && (this.NextProblem?.Level ?? int.MaxValue) > newLevel;
                });
        }

        void IInitilizable.Initialize()
        {
            var allDerivationsTalents = new HashSet<TalentReference>(GetDerivations(this.Reference.Derivation));
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

            foreach (var item in allDerivationsTalents)
                this.Character.Talent[item].PropertyChanged += this.OtherTalent_PropertyChanged;

            foreach (var item in this.Reference.Expressions.SelectMany(x => x.Expresion.CompetencyInvolved))
                this.Character.Competency[item].PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == nameof(CompetencyData.IsActive))
                    {
                        this.Increase.FireCanExecuteChanged();
                        this.FirePropertyChanged(nameof(this.LevelProblem));
                    }
                };

            foreach (var item in this.Reference.Expressions.SelectMany(x => x.Expresion.FeaturesInvolved))
                this.Character.Features[item].PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == nameof(FeaturesData.IsAcquired))
                    {
                        this.Increase.FireCanExecuteChanged();
                        this.FirePropertyChanged(nameof(this.LevelProblem));
                    }
                };

            foreach (var item in this.Reference.Expressions.SelectMany(x => x.Expresion.TalentInvolved))
                this.Character.Talent[item].PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == nameof(TalentData.Level)) // well we also need to watch for BaseLevel and SupportLevel, But both will trigger also Level.
                    {
                        this.Increase.FireCanExecuteChanged();
                        this.FirePropertyChanged(nameof(this.LevelProblem));
                    }
                };

            if (this.Reference.Expressions.SelectMany(x => x.Expresion.TagsInvolved).Any())
                this.Character.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == nameof(CharacterData.Tags))
                    {
                        this.Increase.FireCanExecuteChanged();
                        this.FirePropertyChanged(nameof(this.LevelProblem));
                    }
                };

        }
        public class IncreaseResult
        {
            public IncreaseResult(int oldLevel, int newLevel)
            {
                this.OldLevel = oldLevel;
                this.NewLevel = newLevel;
            }

            public int OldLevel { get; }
            public int NewLevel { get; }
        }

        private void Character_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.Character.ExpirienceAvailable))
                this.Increase.FireCanExecuteChanged();
        }

        private void OtherTalent_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.BaseLevel))
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

                case nameof(this.CurrentProblems):
                    this.currentProblems = null;
                    _ = this.CurrentProblems;
                    break;

                case nameof(this.TotalCostForThisLevel):
                    this.totalCostForThisLevel = null;
                    _ = this.TotalCostForThisLevel;
                    break;

                case nameof(this.BaseLevelWithoutProblems):
                    this.baseLevelWithoutProblems = null;
                    _ = this.BaseLevelWithoutProblems;
                    break;

                case nameof(this.SupportLevel):
                    this.supportLevel = null;
                    _ = this.SupportLevel;
                    break;

                case nameof(this.LevelProblem):
                    this.levelProblem = null;
                    _ = this.LevelProblem;
                    break;

                default:
                    break;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proeprty));
        }

        internal Serelizer GetSerelizer() => new Serelizer() { Id = this.Reference.Id, SpentExperience = this.ExpirienceSpent };

        [DataContract]
        internal class Serelizer
        {
            [DataMember]
            public string Id { get; set; }
            [DataMember]
            public int SpentExperience { get; set; }
        }
    }
}
