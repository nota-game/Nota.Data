﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Nota.Data.Generated.Besonderheit;
using Nota.Data.Generated.Misc;
using Nota.Data.Generated.Talent;

namespace Nota.Data.Expressions
{

    public abstract class Result
    {
        private Result()
        {

        }


        public sealed class MissingTalent : Result
        {
            public MissingTalent(TalentReference talentReference, int? level, bool negate)
            {
                this.TalentReference = talentReference;
                this.Level = level;
                this.Negate = negate;
            }

            public bool Negate { get; }

            public TalentReference TalentReference { get; }

            public int? Level { get; }
        }

        public sealed class MissingFeature : Result
        {
            public MissingFeature(FeaturesReference featuresReference, bool negate)
            {
                this.FeaturesReference = featuresReference;
                this.Negate = negate;
            }

            public bool Negate { get; }

            public FeaturesReference FeaturesReference { get; }
        }

        public sealed class MissingCompetency : Result
        {
            public MissingCompetency(CompetencyReference competencyReference, bool negate)
            {
                this.CompetencyReference = competencyReference;
                this.Negate = negate;
            }

            public bool Negate { get; }

            public CompetencyReference CompetencyReference { get; }
        }

        public sealed class MissingTag : Result
        {
            public MissingTag(TagReference tagReference, bool negate)
            {
                this.TagReference = tagReference;
                this.Negate = negate;
            }

            public bool Negate { get; }

            public TagReference TagReference { get; }
        }

        public sealed class And : Result
        {
            public And(IEnumerable<Result> results)
            {
                this.Results = results.ToImmutableArray();
            }

            public ImmutableArray<Result> Results { get; }
        }

        public sealed class Or : Result
        {
            public Or(IEnumerable<Result> enumerable)
            {
                this.Results = enumerable.ToImmutableArray();
            }

            public ImmutableArray<Result> Results { get; }
        }

        public sealed class Passed : Result
        {

        }

        public static readonly Passed OK = new Passed();

        public static bool operator true(Result x) => x is Passed;
        public static bool operator false(Result x) => !(x is Passed);

        public static implicit operator bool(Result x) => x is Passed;


    }



    internal abstract class Expresion
    {

        public Result Evaluate(CharacterData character) => this.Evaluate(character, false);
        protected abstract Result Evaluate(CharacterData character, bool negate);

        public IEnumerable<TagReference> TagsInvolved => this.TagsInvolvedInternal.Distinct();
        public IEnumerable<TalentReference> TalentInvolved => this.TalentInvolvedInternal.Distinct();
        public IEnumerable<CompetencyReference> CompetencyInvolved => this.CompetencyInvolvedInternal.Distinct();
        public IEnumerable<FeaturesReference> FeaturesInvolved => this.FeaturesInvolvedInternal.Distinct();


        protected virtual IEnumerable<TagReference> TagsInvolvedInternal => Enumerable.Empty<TagReference>();
        protected virtual IEnumerable<TalentReference> TalentInvolvedInternal => Enumerable.Empty<TalentReference>();
        protected virtual IEnumerable<CompetencyReference> CompetencyInvolvedInternal => Enumerable.Empty<CompetencyReference>();
        protected virtual IEnumerable<FeaturesReference> FeaturesInvolvedInternal => Enumerable.Empty<FeaturesReference>();

        public static Expresion GetExpresion(Dictionary<string, TalentReference> directoryTalent, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Dictionary<string, GenusReference> directoryGenus, Dictionary<string, BeingReference> directoryBeing, Dictionary<string, PathGroupReference> directoryPath, Generated.Misc.BedingungsAuswahl bedingung)
        {
            if (bedingung == null)
                return new TrueExpresion();
            if (bedingung.Talent != null)
                return new TalentExpresion(directoryTalent[bedingung.Talent.Id], bedingung.Talent.Level, bedingung.Talent.LevelTyp);
            if (bedingung.Besonderheit != null)
                return new FeatureExpresion(directoryFeatures[bedingung.Besonderheit.Id]);
            if (bedingung.Fertigkeit != null)
                return new CompetencyExpresion(directoryCompetency[bedingung.Fertigkeit.Id]);
            if (bedingung.Tag != null)
                return new TagExpresion(directoryTags[bedingung.Tag.Id]);
            if (bedingung.Not != null)
                return new NotExpresion(GetExpresion(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath, bedingung.Not));
            if (bedingung.And != null)
                return new AndExpresion(GetExpresion(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath, bedingung.And));
            if (bedingung.Or != null)
                return new OrExpresion(GetExpresion(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath, bedingung.Or));

            throw new NotSupportedException();
        }

        public static Expresion[] GetExpresion(Dictionary<string, TalentReference> directoryTalent, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Dictionary<string, GenusReference> directoryGenus, Dictionary<string, BeingReference> directoryBeing, Dictionary<string, PathGroupReference> directoryPath, Generated.Misc.BedingungsAuswahlen bedingung)
        {

            if (bedingung == null)
                return new[] { new TrueExpresion() };
            if (bedingung.TalentSpecified)
                return bedingung.Talent.Select(x => new TalentExpresion(directoryTalent[x.Id], x.Level, x.LevelTyp)).ToArray();
            if (bedingung.BesonderheitSpecified)
                return bedingung.Besonderheit.Select(x => new FeatureExpresion(directoryFeatures[x.Id])).ToArray();
            if (bedingung.FertigkeitSpecified)
                return bedingung.Fertigkeit.Select(x => new CompetencyExpresion(directoryCompetency[x.Id])).ToArray();
            if (bedingung.TagSpecified)
                return bedingung.Tag.Select(x => new TagExpresion(directoryTags[x.Id])).ToArray();
            if (bedingung.NotSpecified)
                return bedingung.Not.Select(x => new NotExpresion(GetExpresion(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath, x))).ToArray();
            if (bedingung.AndSpecified)
                return bedingung.And.Select(x => new AndExpresion(GetExpresion(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath, x))).ToArray();
            if (bedingung.OrSpecified)
                return bedingung.Or.Select(x => new OrExpresion(GetExpresion(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath, x))).ToArray();

            throw new NotSupportedException();
        }

        internal static Expresion GetExpresion(Dictionary<string, TalentReference> talentLookup, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Generated.Besonderheit.BedingungsAuswahl bedingung)
        {
            if (bedingung == null)
                return new TrueExpresion();
            if (bedingung.Besonderheit != null)
                return new FeatureExpresion(directoryFeatures[bedingung.Besonderheit.Id]);
            if (bedingung.Tag != null)
                return new TagExpresion(directoryTags[bedingung.Tag.Id]);
            if (bedingung.Not != null)
                return new NotExpresion(GetExpresion(talentLookup, directoryCompetency, directoryFeatures, directoryTags, bedingung.Not));
            if (bedingung.And != null)
                return new AndExpresion(GetExpresion(talentLookup, directoryCompetency, directoryFeatures, directoryTags, bedingung.And));
            if (bedingung.Or != null)
                return new OrExpresion(GetExpresion(talentLookup, directoryCompetency, directoryFeatures, directoryTags, bedingung.Or));

            throw new NotSupportedException();
        }

        public static Expresion[] GetExpresion(Dictionary<string, TalentReference> talentLookup, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Generated.Besonderheit.BedingungsAuswahlen bedingung)
        {
            if (bedingung == null)
                return new[] { new TrueExpresion() };
            if (bedingung.BesonderheitSpecified)
                return bedingung.Besonderheit.Select(x => new FeatureExpresion(directoryFeatures[x.Id])).ToArray();
            if (bedingung.TagSpecified)
                return bedingung.Tag.Select(x => new TagExpresion(directoryTags[x.Id])).ToArray();
            if (bedingung.NotSpecified)
                return bedingung.Not.Select(x => new NotExpresion(GetExpresion(talentLookup, directoryCompetency, directoryFeatures, directoryTags, x))).ToArray();
            if (bedingung.AndSpecified)
                return bedingung.And.Select(x => new AndExpresion(GetExpresion(talentLookup, directoryCompetency, directoryFeatures, directoryTags, x))).ToArray();
            if (bedingung.OrSpecified)
                return bedingung.Or.Select(x => new OrExpresion(GetExpresion(talentLookup, directoryCompetency, directoryFeatures, directoryTags, x))).ToArray();

            throw new NotSupportedException();
        }

        internal static Expresion GetExpresion(Dictionary<string, TalentReference> directoryTalent, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Dictionary<string, GenusReference> directoryGenus, Dictionary<string, BeingReference> directoryBeing, Dictionary<string, PathGroupReference> directoryPath, ImmutableArray<LevelReference> levels, LevelAuswahl bedingung)
        {
            if (bedingung == null)
                return new TrueExpresion();
            if (bedingung.Level != null)
                return new LevelExpresion(levels.First(x => x.Id == bedingung.Level.Id));
            if (bedingung.Not != null)
                return new NotExpresion(GetExpresion(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath, levels, bedingung.Not));
            if (bedingung.And != null)
                return new AndExpresion(GetExpresion(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath, levels, bedingung.And));
            if (bedingung.Or != null)
                return new OrExpresion(GetExpresion(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath, levels, bedingung.Or));

            throw new NotSupportedException();
        }

        internal static Expresion[] GetExpresion(Dictionary<string, TalentReference> directoryTalent, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Dictionary<string, GenusReference> directoryGenus, Dictionary<string, BeingReference> directoryBeing, Dictionary<string, PathGroupReference> directoryPath, ImmutableArray<LevelReference> levels, LevelAuswahlen bedingung)
        {
            if (bedingung == null)
                return new[] { new TrueExpresion() };


            if (bedingung.LevelSpecified)
                return bedingung.Level.Select(x => new TagExpresion(directoryTags[x.Id])).ToArray();
            if (bedingung.NotSpecified)
                return bedingung.Not.Select(x => new NotExpresion(GetExpresion(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath, levels, x))).ToArray();
            if (bedingung.AndSpecified)
                return bedingung.And.Select(x => new AndExpresion(GetExpresion(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath, levels, x))).ToArray();
            if (bedingung.OrSpecified)
                return bedingung.Or.Select(x => new OrExpresion(GetExpresion(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath, levels, x))).ToArray();

            throw new NotSupportedException();
        }


        private class TalentExpresion : Expresion
        {
            private readonly TalentReference talentReference;
            private readonly int level;
            private readonly LevelBedingungsTyp LevelKind;

            protected override IEnumerable<TalentReference> TalentInvolvedInternal => new[] { this.talentReference };
            public TalentExpresion(TalentReference talentReference, int level, LevelBedingungsTyp levelTyp)
            {
                this.talentReference = talentReference;
                this.level = level;
                this.LevelKind = levelTyp;
            }


            protected override Result Evaluate(CharacterData character, bool negate)
            {
                switch (LevelKind)
                {
                    case LevelBedingungsTyp.Basis:
                        if (negate ^ (character.Talent[this.talentReference].BaseLevel >= this.level))
                            return Result.OK;
                        break;
                    case LevelBedingungsTyp.Effektiv:
                        if (negate ^ (character.Talent[this.talentReference].Level >= this.level))
                            return Result.OK;
                        break;
                    case LevelBedingungsTyp.Unterstützung:
                        if (negate ^ (character.Talent[this.talentReference].SupportLevel >= this.level))
                            return Result.OK;
                        break;
                    default:
                        throw new NotSupportedException();
                }
                return new Result.MissingTalent(this.talentReference, this.level, negate);
            }
        }
        private class TrueExpresion : Expresion
        {


            protected override Result Evaluate(CharacterData character, bool negate) => Result.OK;
        }

        private class FeatureExpresion : Expresion
        {
            private FeaturesReference featuresReference;

            protected override IEnumerable<FeaturesReference> FeaturesInvolvedInternal => new[] { this.featuresReference };
            public FeatureExpresion(FeaturesReference featuresReference)
            {
                this.featuresReference = featuresReference;
            }

            protected override Result Evaluate(CharacterData character, bool negate)
            {
                if (negate ^ (character.Features[this.featuresReference].IsAcquired))
                    return Result.OK;
                return new Result.MissingFeature(this.featuresReference, negate);
            }
        }

        private class CompetencyExpresion : Expresion
        {
            private CompetencyReference competencyReference;

            protected override IEnumerable<CompetencyReference> CompetencyInvolvedInternal => new[] { this.competencyReference };

            public CompetencyExpresion(CompetencyReference competencyReference)
            {
                this.competencyReference = competencyReference;
            }

            protected override Result Evaluate(CharacterData character, bool negate)
            {
                if (negate ^ (character.Competency[this.competencyReference].IsAcquired))
                    return Result.OK;
                return new Result.MissingCompetency(this.competencyReference, negate);
            }
        }

        private class TagExpresion : Expresion
        {
            private TagReference tagReference;

            protected override IEnumerable<TagReference> TagsInvolvedInternal => new[] { this.tagReference };
            public TagExpresion(TagReference tagReference)
            {
                this.tagReference = tagReference;
            }

            protected override Result Evaluate(CharacterData character, bool negate)
            {
                if (negate ^ (character.Tags.Contains(this.tagReference)))
                    return Result.OK;
                return new Result.MissingTag(this.tagReference, negate);
            }

        }

        private class NotExpresion : Expresion
        {
            private Expresion expresion;

            protected override IEnumerable<TagReference> TagsInvolvedInternal => this.expresion.TagsInvolvedInternal;
            protected override IEnumerable<CompetencyReference> CompetencyInvolvedInternal => this.expresion.CompetencyInvolvedInternal;
            protected override IEnumerable<FeaturesReference> FeaturesInvolvedInternal => this.expresion.FeaturesInvolvedInternal;
            protected override IEnumerable<TalentReference> TalentInvolvedInternal => this.expresion.TalentInvolvedInternal;

            public NotExpresion(Expresion expresion)
            {
                this.expresion = expresion;
            }

            protected override Result Evaluate(CharacterData character, bool negate)
            {
                var result = this.expresion.Evaluate(character, !negate);
                return result;

            }
        }

        private class AndExpresion : Expresion
        {
            private Expresion[] expresion;

            protected override IEnumerable<TagReference> TagsInvolvedInternal => this.expresion.SelectMany(x => x.TagsInvolvedInternal);
            protected override IEnumerable<CompetencyReference> CompetencyInvolvedInternal => this.expresion.SelectMany(x => x.CompetencyInvolvedInternal);
            protected override IEnumerable<FeaturesReference> FeaturesInvolvedInternal => this.expresion.SelectMany(x => x.FeaturesInvolvedInternal);
            protected override IEnumerable<TalentReference> TalentInvolvedInternal => this.expresion.SelectMany(x => x.TalentInvolvedInternal);


            public AndExpresion(Expresion[] expresion)
            {
                this.expresion = expresion;
            }

            protected override Result Evaluate(CharacterData character, bool negate)
            {
                if (negate)
                {
                    var results = this.expresion.Select(x => x.Evaluate(character, true)).ToArray();
                    if (results.Any(x => x == Result.OK))
                        return Result.OK;

                    return new Result.Or(results.Where(x => x != Result.OK));

                }
                else
                {
                    var results = this.expresion.Select(x => x.Evaluate(character, false)).Where(x => x != Result.OK).ToArray();
                    if (results.Length == 0)
                        return Result.OK;
                    else if (results.Length == 1)
                        return results.First();
                    else
                        return new Result.And(results);
                }
            }

        }

        private class OrExpresion : Expresion
        {
            private Expresion[] expresion;

            protected override IEnumerable<TagReference> TagsInvolvedInternal => this.expresion.SelectMany(x => x.TagsInvolvedInternal);
            protected override IEnumerable<CompetencyReference> CompetencyInvolvedInternal => this.expresion.SelectMany(x => x.CompetencyInvolvedInternal);
            protected override IEnumerable<FeaturesReference> FeaturesInvolvedInternal => this.expresion.SelectMany(x => x.FeaturesInvolvedInternal);
            protected override IEnumerable<TalentReference> TalentInvolvedInternal => this.expresion.SelectMany(x => x.TalentInvolvedInternal);



            public OrExpresion(Expresion[] expresion)
            {
                this.expresion = expresion;
            }
            protected override Result Evaluate(CharacterData character, bool negate)
            {
                if (negate)
                {
                    var results = this.expresion.Select(x => x.Evaluate(character, true)).Where(x => x != Result.OK).ToArray();
                    if (results.Length == 0)
                        return Result.OK;
                    else if (results.Length == 1)
                        return results.First();
                    else
                        return new Result.And(results);
                }
                else
                {
                    var results = this.expresion.Select(x => x.Evaluate(character, false)).ToArray();
                    if (results.Any(x => x == Result.OK))
                        return Result.OK;

                    return new Result.Or(results.Where(x => x != Result.OK));
                }

            }
        }

        private class LevelExpresion : Expresion
        {
            private readonly LevelReference levelReference;

            public LevelExpresion(LevelReference levelReference)
            {
                this.levelReference = levelReference;
            }

            protected override Result Evaluate(CharacterData character, bool negate)
            {
                if (negate ^ (character.Competency[this.competencyReference].IsAcquired))
                    return Result.OK;
                return new Result.MissingCompetency(this.competencyReference, negate);
            }
        }
    }

}