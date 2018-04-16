using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace NetbarIpAddrImporter {
    class ImportCommand : ICommand {
        public event EventHandler CanExecuteChanged;

        private MainWindowViewModel viewModel;

        public ImportCommand( MainWindowViewModel viewModel ) {
            this.viewModel = viewModel;
        }

        public bool CanExecute( object parameter ) {
            return true;
        }

        public void Execute( object parameter ) {
            var result = Data.Import( viewModel.NetbarList.ToList() );
            viewModel.UpdateNetbarList( result );
            Data.SyncToGame( result );
            MessageBox.Show( "导入游戏完成" );
        }
    }
}
