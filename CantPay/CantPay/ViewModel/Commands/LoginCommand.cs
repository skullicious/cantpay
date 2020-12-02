using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CantPay.ViewModel.Commands
{
    public class LoginCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        LoginVM loginVM;

        public LoginCommand(LoginVM LoginVM)
        {
            loginVM = LoginVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            loginVM.Login();
        }
    }
}
