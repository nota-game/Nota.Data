using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Nota.Data
{
    internal sealed class DelegateCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;


        /// <summary>
        /// Implement a generic version of ICommand
        /// </summary>
        /// <param name="execute">The Action performed when the command is invoked.</param>
        /// <param name="canExecute">The callback that checks if the action can be performed. Should execute fast.</param>
        public DelegateCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }


        internal void FireExecuteChaged() => this.FireExecuteChaged(this, EventArgs.Empty);
        private void FireExecuteChaged(object sender, EventArgs e) => CanExecuteChanged?.Invoke(sender, e);

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => this.canExecute?.Invoke() ?? true;

        public void Execute(object parameter) => this.execute();

    }
}
