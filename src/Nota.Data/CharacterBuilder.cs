using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Nota.Data.References;

namespace Nota.Data
{
    public class CharacterBuilder
    {
        internal CharacterBuilder(Guid guid, Data data)
        {
            this.Character = new CharacterData(guid, data);

            this.choosers = new ObservableCollection<AbstractChooser>();
            this.Choosers = new ReadOnlyObservableCollection<AbstractChooser>(this.choosers);

            this.Organism = new OrganismChooser(data.Beings, this);
            this.pathes = data.Path.Select(x => new PathChooser(this, x)).ToImmutableArray();

            this.choosers.Add(this.Organism);
            foreach (var item in this.pathes)
            {
                this.choosers.Add(item);
                item.Attach();
            }
        }
        internal CharacterData Character { get; }

        private readonly ObservableCollection<AbstractChooser> choosers;
        public ReadOnlyObservableCollection<AbstractChooser> Choosers { get; }

        public OrganismChooser Organism { get; }

        private readonly ImmutableArray<PathChooser> pathes;

        public abstract class AbstractChooser : NotifyPropertyChangedBase
        {
            protected readonly CharacterBuilder builder;
            private readonly List<AbstractChooser> childrean = new List<AbstractChooser>();
            private protected AbstractChooser Parent { get; private set; }

            internal AbstractChooser PreviousChoose
            {
                get
                {
                    var current = this;
                    while (current.Parent != null)
                        current = current.Parent;

                    var previousIndex = this.builder.Choosers.IndexOf(current) - 1;
                    if (previousIndex < 0)
                        return null;
                    return this.builder.Choosers[previousIndex];

                }
            }

            private protected AbstractChooser(CharacterBuilder builder)
            {
                this.builder = builder ?? throw new ArgumentNullException(nameof(builder));
            }

            public void AddChild(AbstractChooser child)
            {
                if (child.Parent != null)
                    throw new ArgumentException("Chooser already Child.", nameof(child));
                child.Parent = this;
                var insertAfter = this.childrean.LastOrDefault() ?? this;
                this.childrean.Add(child);
                var index = this.builder.choosers.IndexOf(insertAfter) + 1;
                this.builder.choosers.Insert(index, child);
                child.Attach();
            }

            public void RemoveChild(AbstractChooser child)
            {
                if (child.Parent != this)
                    throw new ArgumentException("Chooser not Child of this Chooser.", nameof(child));

                child.Parent = null;
                this.childrean.Remove(child);
                this.builder.choosers.Remove(child);
                child.Detach();
            }

            public virtual void Attach() { }
            public virtual void Detach() { }
        }

        public abstract class SelectionChooser<T> : AbstractChooser
        {
            private T selected;
            public T Selected
            {
                get => this.selected; set
                {
                    if (!Object.Equals(this.selected, value))
                    {
                        this.OnSelectionChanging(this.selected, value);
                        this.selected = value;
                        this.FirePropertyChanged();
                    }
                }
            }

            protected virtual void OnSelectionChanging(T oldValue, T newValue) { }

            private protected SelectionChooser(CharacterBuilder builder) : base(builder)
            {
            }
        }


        public sealed class PathChooser : SelectionChooser<PathChooser.Path>, IDisposable
        {
            private PathChooser previosChooser;
            private PathChooser nextChooser;

            public ReadOnlyObservableCollection<Path> Pathes { get; }
            private readonly ObservableCollection<Path> pathes;


            internal void AddPath(PathReference reference)
            {
                System.Diagnostics.Debug.Assert(!this.pathes.Any(x => x.Reference == reference));
                this.pathes.Add(new Path(reference, this));
            }
            internal void RemovePath(PathReference reference)
            {
                this.pathes.Remove(new Path(reference, this));
            }

            public bool canAdditionalPathChange = true;
            public bool CanAdditionalPathChange
            {
                get => this.canAdditionalPathChange;
                private set
                {
                    if (this.canAdditionalPathChange != value)
                    {
                        this.canAdditionalPathChange = value;
                        this.FirePropertyChanged();
                    }
                }
            }
            public bool additionalPath;
            public bool AdditionalPath
            {
                get => this.additionalPath;
                set
                {
                    if (!this.CanAdditionalPathChange)
                        throw new InvalidOperationException("Can not change");
                    if (this.additionalPath != value)
                    {

                        this.additionalPath = value;
                        this.FirePropertyChanged();
                        if (this.additionalPath)
                        {
                            this.nextChooser = new PathChooser(this.builder, this.Reference, this);
                            if (this.previosChooser != null)
                                this.previosChooser.CanAdditionalPathChange = false;

                            this.AddChild(this.nextChooser);
                        }
                        else
                        {
                            // if we delete the next Path, we allow the selected value to be choosen again.
                            if (this.nextChooser.Selected != null)
                            {
                                var current = this;
                                while (current != null)
                                {
                                    current.AddPath(this.nextChooser.Selected.Reference);
                                    current = this.previosChooser;
                                }
                            }
                            if (this.previosChooser != null)
                                this.previosChooser.CanAdditionalPathChange = this.previosChooser.PathNumber >= this.Reference.MinimumSelection;
                            this.RemoveChild(this.nextChooser);
                            this.nextChooser.Dispose();
                            this.nextChooser = null;

                        }
                    }
                }
            }

            private int PathNumber => (this.previosChooser?.PathNumber + 1) ?? 1;

            public CharacterBuilder Builder { get; }

            public PathGroupReference Reference { get; }

            public PathChooser(CharacterBuilder builder, PathGroupReference reference, PathChooser previosChooser = null) : base(builder)
            {
                this.Builder = builder;
                this.Reference = reference;
                this.previosChooser = previosChooser;
                this.pathes = new ObservableCollection<Path>();
                this.Pathes = new ReadOnlyObservableCollection<Path>(this.pathes);

            }

            public override void Attach()
            {
                base.Attach();

                var current = this.previosChooser;
                var notToAddPathes = new HashSet<PathReference>();
                while (current != null)
                {
                    if (current.Selected != null)
                        notToAddPathes.Add(current.Selected.Reference);
                    current = current.previosChooser;
                }

                foreach (var item in this.Reference.Pathes.Except(notToAddPathes))
                    this.AddPath(item);
                this.AdditionalPath = this.PathNumber < this.Reference.MinimumSelection;
                this.CanAdditionalPathChange = !(this.PathNumber >= this.Reference.MaximumSelection || this.PathNumber < this.Reference.MinimumSelection);

            }

            public override void Detach()
            {
                base.Detach();
                this.previosChooser = null;
                this.pathes.Clear();
            }

            protected override void OnSelectionChanging(Path oldValue, Path newValue)
            {
                base.OnSelectionChanging(oldValue, newValue);

                var current = this.previosChooser;
                while (current != null)
                {
                    if (oldValue != null)
                        current.AddPath(oldValue.Reference);
                    if (newValue != null)
                        current.RemovePath(newValue.Reference);
                    current = current.previosChooser;
                }
                current = this.nextChooser;
                while (current != null)
                {
                    if (oldValue != null)
                        current.AddPath(oldValue.Reference);
                    if (newValue != null)
                        current.RemovePath(newValue.Reference);
                    current = current.nextChooser;
                }
            }

            public void Dispose()
            {
                // No dispose Pattern. See Path.Dispose().
                foreach (var item in this.Pathes)
                    item.Dispose();
            }

            public sealed class Path : NotifyPropertyChangedBase, IDisposable
            {
                private bool isApplicable;

                public Path(PathReference reference, PathChooser chooser)
                {
                    this.Reference = reference;
                    this.Chooser = chooser;

                    if (this.Chooser.PreviousChoose is OrganismChooser organism)
                        organism.PropertyChanged += this.Organism_PropertyChanged;
                    if (this.Chooser.PreviousChoose is PathChooser path)
                        path.PropertyChanged += this.Path_PropertyChanged;

                    this.UpdateApplicable();

                }

                private void UpdateApplicable()
                {
                    if (this.Chooser.PreviousChoose is OrganismChooser organism)
                        this.IsApplicable = this.Chooser.Builder.Organism.Selected != default
                            && this.Chooser.Builder.Organism.Selected.Reference.MorphReference.DefaultPathes.Contains(this.Reference);
                    if (this.Chooser.PreviousChoose is PathChooser path)
                        this.IsApplicable = (this.Chooser.PreviousChoose as PathChooser).Selected != null
                            && ((this.Chooser.PreviousChoose as PathChooser).Selected.Reference.DefaultPathes.Contains(this.Reference)
                                || (this.Chooser.PreviousChoose as PathChooser).Selected.Reference.DefaultPathes.Count == 0);

                }

                private void Path_PropertyChanged(object sender, PropertyChangedEventArgs e)
                {
                    if (e.PropertyName == nameof(OrganismChooser.Selected))
                        this.UpdateApplicable();
                }

                private void Organism_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
                {
                    if (e.PropertyName == nameof(OrganismChooser.Selected))
                        this.UpdateApplicable();

                }

                public PathReference Reference { get; }
                public PathChooser Chooser { get; }
                public bool IsApplicable
                {
                    get => this.isApplicable; private set
                    {
                        if (this.IsApplicable != value)
                        {
                            this.isApplicable = value;
                            this.FirePropertyChanged();
                        }
                    }
                }



                public void Dispose()
                {
                    // we don't need to implement the correct Dispose pattern. This class is seald and calling unregister on event multiple times shold do no harm.

                    if (this.Chooser.PreviousChoose is OrganismChooser organism)
                        organism.PropertyChanged -= this.Organism_PropertyChanged;
                    if (this.Chooser.PreviousChoose is PathChooser path)
                        path.PropertyChanged -= this.Path_PropertyChanged;
                }

                public override bool Equals(object obj)
                {
                    return obj is Path path &&
                           EqualityComparer<PathReference>.Default.Equals(this.Reference, path.Reference) &&
                           EqualityComparer<PathChooser>.Default.Equals(this.Chooser, path.Chooser);
                }

                public override int GetHashCode()
                {
                    var hashCode = 92689824;
                    hashCode = hashCode * -1521134295 + EqualityComparer<PathReference>.Default.GetHashCode(this.Reference);
                    hashCode = hashCode * -1521134295 + EqualityComparer<PathChooser>.Default.GetHashCode(this.Chooser);
                    return hashCode;
                }

                public static bool operator ==(Path left, Path right)
                {
                    return EqualityComparer<Path>.Default.Equals(left, right);
                }

                public static bool operator !=(Path left, Path right)
                {
                    return !(left == right);
                }
            }

        }
        public sealed class LevelChooser
        {
            private readonly PathReference pathReference;

            public ImmutableArray<Level> SelectedLevels { get; }

            public LevelChooser(PathReference path)
            {
                this.pathReference = path;
                this.SelectedLevels = this.pathReference.Levels.Select(x => new Level(x)).ToImmutableArray();
            }



            public class Level : NotifyPropertyChangedBase
            {
                private int taken;
                public int Taken
                {
                    get => this.taken;
                    private set
                    {
                        if (this.taken != value)
                        {
                            this.taken = value;
                            this.FirePropertyChanged();
                        }
                    }
                }
                public ICommand Increase { get; }
                public ICommand Decrease { get; }

                public Level(LevelReference reference)
                {
                    this.Reference = reference;
                    this.Increase = new DelegateCommand(() => this.Taken++, () => this.Taken < this.Reference.Repeation);
                    this.Decrease = new DelegateCommand(() => this.Taken--, () => this.Taken > 0);
                }

                private protected override void FirePropertyChanged([CallerMemberName] string proeprty = null)
                {
                    (this.Increase as DelegateCommand).FireExecuteChaged();
                    (this.Decrease as DelegateCommand).FireExecuteChaged();
                    base.FirePropertyChanged(proeprty);
                }
                public LevelReference Reference { get; }
            }

        }

        public sealed class OrganismChooser : SelectionChooser<OrganismChooser.LifePeriod>
        {



            internal OrganismChooser(IReadOnlyList<OrganismReference> beings, CharacterBuilder characterBuilder) : base(characterBuilder)
            {
                this.Organisms = beings.Select(x => new Organism(x)).Where(x => x.Morphs.Any()).ToImmutableArray();
            }

            public ImmutableArray<Organism> Organisms { get; }

            public readonly struct Organism
            {

                public Organism(OrganismReference reference)
                {
                    this.Reference = reference;
                    this.Morphs = reference.Morphs.Select(x => new Morph(x)).Where(x => x.LifePeriods.Any()).ToImmutableArray();
                }

                public OrganismReference Reference { get; }
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
            public readonly struct LifePeriod : IEquatable<LifePeriod>
            {
                public LifePeriod(LifePeriodPlayableReference reference)
                {
                    this.Reference = reference;
                }

                public LifePeriodPlayableReference Reference { get; }

                public override bool Equals(object obj)
                {
                    return obj is LifePeriod period && this.Equals(period);
                }

                public bool Equals(LifePeriod other)
                {
                    return EqualityComparer<LifePeriodPlayableReference>.Default.Equals(this.Reference, other.Reference);
                }

                public override int GetHashCode()
                {
                    return -1304721846 + EqualityComparer<LifePeriodPlayableReference>.Default.GetHashCode(this.Reference);
                }

                public static bool operator ==(LifePeriod left, LifePeriod right)
                {
                    return left.Equals(right);
                }

                public static bool operator !=(LifePeriod left, LifePeriod right)
                {
                    return !(left == right);
                }
            }

        }

    }
}
