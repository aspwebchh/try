using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace NetbarIpAddrImporter {
    class LoadExcelCommand : ICommand {
        public event EventHandler CanExecuteChanged;

        private MainWindowViewModel viewModel;

        public LoadExcelCommand( MainWindowViewModel viewModel ) {
            this.viewModel = viewModel;
        }

        public bool CanExecute( object parameter ) {
            return true;
        }

        public void Execute( object parameter ) {
            var op = new System.Windows.Forms.OpenFileDialog();
            op.Multiselect = false;
            op.Filter = "Excel 2000|*.xls|Excel 2003|*.xlsx";
            if( op.ShowDialog() == System.Windows.Forms.DialogResult.OK ) {
                var dt = ExcelReader.Read( op.FileName );
                var list = NetbarItem.Data2List( dt );
                list.ForEach( viewModel.NetbarList.Add );
            }
        }
    }
}
