using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Nota.Data.References;

namespace Nota.Data
{
    public class CharacterBuilder
    {
        internal CharacterBuilder(Guid guid, Data data)
        {
            this.Character = new CharacterData(guid, data);
            new OrganismChooser(data.Beings, this);

            new PathChooser(data.Path.First());
            //.Pathes.First().Levels.First().
        }
        internal CharacterData Character { get; }

        public abstract class AbstractChooser { }

        public sealed class PathChooser : AbstractChooser
        {
            private PathGroupReference reference;

            public Path Selected { get; }

            public PathChooser(PathGroupReference reference)
            {
                this.reference = reference;
                reference.Pathes.Select(x => new Path(x));
            }

            public class Path
            {
                public int Used { get; set; }

                public Path(PathReference reference)
                {
                    this.Reference = reference;

                }

                public PathReference Reference { get; }
            }

        }
        public sealed class LevelChooser
        {
            private readonly PathReference pathReference;

            public Dictionary<Level, int> SelectedLevels { get; }

            public LevelChooser(PathReference path)
            {
                this.pathReference = path;
                this.SelectedLevels = this.pathReference.Levels.Select(x => new Level(x)).ToDictionary(x => x, x => 0);
            }



            public readonly struct Level
            {
                public Level(LevelReference reference)
                {
                    this.Reference = reference;
                }

                public LevelReference Reference { get; }
            }

        }

        public sealed class OrganismChooser : AbstractChooser
        {
            private readonly CharacterBuilder characterBuilder;

            public LifePeriod Selected { get; }

            internal OrganismChooser(IReadOnlyList<BeingReference> beings, CharacterBuilder characterBuilder)
            {
                this.Organisms = beings.Select(x => new Organism(x)).Where(x => x.Morphs.Any()).ToImmutableArray();
                this.characterBuilder = characterBuilder;
            }

            public ImmutableArray<Organism> Organisms { get; }

            public readonly struct Organism
            {

                public Organism(BeingReference reference)
                {
                    this.Reference = reference;
                    this.Morphs = reference.Morphs.Select(x => new Morph(x)).Where(x => x.LifePeriods.Any()).ToImmutableArray();
                }

                public BeingReference Reference { get; }
                public ImmutableArray<Morph> Morphs { get; }
            }
            public readonly struct Morph
            {
                public Morph(MorphReference reference)
                {
                    this.Reference = reference;
                    this.LifePeriods = reference.LifePeriods.OfType<LifePeriodPlayableReference>().Select(x => new LifePeriod(x)).ToImmutableArray();
                }

                public MorphReference Reference { get; }
                public ImmutableArray<LifePeriod> LifePeriods { get; }
            }
            public readonly struct LifePeriod
            {
                public LifePeriod(LifePeriodReference reference)
                {
                    this.Reference = reference;
                }

                public LifePeriodReference Reference { get; }
            }

        }

    }
}
