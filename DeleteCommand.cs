using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace NetbarIpAddrImporter {
    class DeleteCommand : ICommand {
        public event EventHandler CanExecuteChanged;

        private MainWindowViewModel viewModel;

        public DeleteCommand( MainWindowViewModel viewModel ) {
            this.viewModel = viewModel;
        }

        public bool CanExecute( object parameter ) {
            return true;
        }

        public void Execute( object parameter ) {
            var deleteNetbar = new DeleteNetbar();
            deleteNetbar.Owner = MainWindow.instance;
            deleteNetbar.Show();
        }
    }
}
