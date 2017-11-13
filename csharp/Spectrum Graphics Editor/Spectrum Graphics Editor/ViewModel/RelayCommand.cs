using System;
using System.Windows.Input;

namespace SloanKelly.Tools.SGE.ViewModel
{
    internal class RelayCommand : ICommand
    {
        private readonly Action _action;

        public RelayCommand(Action action)
        {
            if (action == null)
                throw new ArgumentNullException($"You must specify an action. '{nameof(action)}' cannot be null.");

            _action = action;
        }

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
