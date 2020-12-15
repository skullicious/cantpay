using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CantPay.ViewModel.Commands
{
    public class GetDataCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        MenuVM menuVM;

        public GetDataCommand(MenuVM MenuVM)
        {
            menuVM = MenuVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
