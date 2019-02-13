using System;
namespace Nota.Data
{
    public interface IDataAction<TParent, TInput>
    {
        event EventHandler CanExecuteChanged;

        bool CanExecute(TInput input);
        void Execute(TInput input);
    }

    public class DataAction<TParent, TInput> : IDataAction<TParent, TInput>
    {
        private readonly CharacterData character;
        internal readonly TParent parent;
        private readonly Action<TParent, TInput> execute;
        private readonly Action<TParent, TInput> undoExecute;
        private readonly Func<TParent, TInput, string> description;
        private readonly Func<TParent, TInput, bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public DataAction(CharacterData character, TParent parent, Action<TParent, TInput> execute, Action<TParent, TInput> undoExecute, Func<TParent, TInput, string> description, Func<TParent, TInput, bool> canExecute = null)
        {
            this.character = character ?? throw new ArgumentNullException(nameof(character));
            this.parent = parent;
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.undoExecute = undoExecute ?? throw new ArgumentNullException(nameof(undoExecute));
            this.description = description ?? throw new ArgumentNullException(nameof(description));
            this.canExecute = canExecute;
            CanExecuteChanged = null;
        }



        public bool CanExecute(TInput input)
        {
            return this.canExecute?.Invoke(this.parent, input) ?? true;
        }

        public DataActionReturn<TParent, TInput> Execute(TInput input)
        {
            if (!this.CanExecute(input))
                return default;
            this.execute(this.parent, input);
            var dataActionReturn = new DataActionReturn<TParent, TInput>(this.description(this.parent, input), this, this.character, input, this.undoExecute);
            this.character.AddToUndo(dataActionReturn);
            return dataActionReturn;
        }

        internal void FireCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);



        void IDataAction<TParent, TInput>.Execute(TInput input) => this.Execute(input);
    }
    public class DataAction<TParent, TInput, TReturn> : IDataAction<TParent, TInput>
    {
        private readonly CharacterData character;
        internal readonly TParent parent;
        private readonly Func<TParent, TInput, TReturn> execute;
        private readonly Action<TParent, TInput, TReturn> undoExecute;
        private readonly Func<TParent, TInput, bool> canExecute;
        private readonly Func<TParent, TInput, TReturn, string> description;
        internal DataAction(CharacterData character, TParent parent, Func<TParent, TInput, TReturn> execute, Action<TParent, TInput, TReturn> undoExecute, Func<TParent, TInput, TReturn, string> description, Func<TParent, TInput, bool> canExecute = null)
        {
            this.character = character;
            this.parent = parent;
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.undoExecute = undoExecute ?? throw new ArgumentNullException(nameof(undoExecute));
            this.description = description ?? throw new ArgumentNullException(nameof(description)); ;
            this.canExecute = canExecute;
            CanExecuteChanged = null;
        }

        public bool CanExecute(TInput input)
        {
            return this.canExecute?.Invoke(this.parent, input) ?? true;
        }

        public DataActionReturn<TParent, TInput, TReturn> Execute(TInput input)
        {
            if (!this.CanExecute(input))
                return default;
            var result = this.execute(this.parent, input);

            var dataActionReturn = new DataActionReturn<TParent, TInput, TReturn>(this.description(this.parent, input, result), this, this.character, input, result, this.undoExecute);
            this.character.AddToUndo(dataActionReturn);
            return dataActionReturn;
        }

        public event EventHandler CanExecuteChanged;
        internal void FireCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        void IDataAction<TParent, TInput>.Execute(TInput input) => this.Execute(input);

    }

    public abstract class DataActionReturn
    {
        public string Description { get; }
        internal abstract CharacterData Origin { get; }
        internal bool hasUndone { get; private protected set; }
        internal abstract void Undo();
        public DataActionReturn(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("message", nameof(description));
            this.Description = description;
        }
    }
    public class DataActionReturn<TParent, TInput> : DataActionReturn
    {
        private readonly DataAction<TParent, TInput> dataAction;
        private readonly CharacterData character;
        private readonly TInput input;
        private readonly Action<TParent, TInput> undoExecute;

        public DataActionReturn(string description, DataAction<TParent, TInput> dataAction, CharacterData character, TInput input, Action<TParent, TInput> undoExecute)
            : base(description)
        {
            this.dataAction = dataAction;
            this.character = character;
            this.input = input;
            this.undoExecute = undoExecute;
        }

        internal override CharacterData Origin => this.character;

        internal override void Undo()
        {
            if (this.hasUndone)
                return;
            this.hasUndone = true;
            this.undoExecute(this.dataAction.parent, this.input);
        }

    }
    public class DataActionReturn<TParent, TInput, TReturn> : DataActionReturn
    {
        private DataAction<TParent, TInput, TReturn> dataAction;
        private readonly CharacterData character;
        private TInput input;
        private TReturn result;
        private Action<TParent, TInput, TReturn> undoExecute;
        internal override CharacterData Origin => this.character;

        public DataActionReturn(string description, DataAction<TParent, TInput, TReturn> dataAction, CharacterData character, TInput input, TReturn result, Action<TParent, TInput, TReturn> undoExecute)
            : base(description)
        {
            this.dataAction = dataAction;
            this.character = character;
            this.input = input;
            this.result = result;
            this.undoExecute = undoExecute;
        }


        internal override void Undo()
        {
            if (this.hasUndone)
                return;
            this.hasUndone = true;
            this.undoExecute(this.dataAction.parent, this.input, this.result);
        }
    }
}