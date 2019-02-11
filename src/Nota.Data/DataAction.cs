﻿using System;
namespace Nota.Data
{
    public struct DataAction<TParent, TInput>
    {
        private readonly CharacterData character;
        internal readonly TParent parent;
        private readonly Action<TParent, TInput> execute;
        private readonly Action<TParent, TInput> undoExecute;
        private readonly Func<TParent, TInput, bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public DataAction(CharacterData character, TParent parent, Action<TParent, TInput> execute, Action<TParent, TInput> undoExecute, Func<TParent, TInput, bool> canExecute = null)
        {
            this.character = character;
            this.parent = parent;
            this.execute = execute;
            this.undoExecute = undoExecute;
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

            return new DataActionReturn<TParent, TInput>(this, this.character, input, this.undoExecute);
        }

        internal void FireCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    }
    internal struct DataAction<TParent, TInput, TReturn>
    {
        private readonly CharacterData character;
        internal readonly TParent parent;
        private readonly Func<TParent, TInput, TReturn> execute;
        private readonly Action<TParent, TInput, TReturn> undoExecute;
        private readonly Func<TParent, TInput, bool> canExecute;

        internal DataAction(CharacterData character, TParent parent, Func<TParent, TInput, TReturn> execute, Action<TParent, TInput, TReturn> undoExecute, Func<TParent, TInput, bool> canExecute)
        {
            this.character = character;
            this.parent = parent;
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.undoExecute = undoExecute ?? throw new ArgumentNullException(nameof(undoExecute));
            this.canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
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

            return new DataActionReturn<TParent, TInput, TReturn>(this, this.character, input, result, this.undoExecute);
        }

        public event EventHandler CanExecuteChanged;
        internal void FireCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    }

    public abstract class DataActionReturn
    {
        internal abstract CharacterData Origin { get; }
        internal bool hasUndone { get; set; }
        internal abstract void Undo();
    }
    public class DataActionReturn<TParent, TInput> : DataActionReturn
    {
        private readonly DataAction<TParent, TInput> dataAction;
        private readonly CharacterData character;
        private readonly TInput input;
        private readonly Action<TParent, TInput> undoExecute;

        public DataActionReturn(DataAction<TParent, TInput> dataAction, CharacterData character, TInput input, Action<TParent, TInput> undoExecute)
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
    internal class DataActionReturn<TParent, TInput, TReturn> : DataActionReturn
    {
        private DataAction<TParent, TInput, TReturn> dataAction;
        private readonly CharacterData character;
        private TInput input;
        private TReturn result;
        private Action<TParent, TInput, TReturn> undoExecute;
        internal override CharacterData Origin => this.character;

        public DataActionReturn(DataAction<TParent, TInput, TReturn> dataAction, CharacterData character, TInput input, TReturn result, Action<TParent, TInput, TReturn> undoExecute)
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