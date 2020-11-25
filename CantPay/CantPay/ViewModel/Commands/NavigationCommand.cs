using CantPay.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CantPay.ViewModel.Commands
{
    public class NavigationCommand : ICommand
    {

        INavigable navigablePage;

        public event EventHandler CanExecuteChanged;

        public MenuVM MenuViewModel { get; set; }     


        public NavigationCommand(MenuVM menuVM)
        {
            navigablePage = menuVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {          
         
            navigablePage.Navigate(parameter);
        }
    }
}
