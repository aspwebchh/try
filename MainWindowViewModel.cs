using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetbarIpAddrImporter {
    class MainWindowViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<NetbarItem> netbarList = new ObservableCollection<NetbarItem>();

        private LoadExcelCommand loadExcelCommand;

        private ImportCommand importCommand;

        public MainWindowViewModel() {
            loadExcelCommand = new LoadExcelCommand( this );
            importCommand = new ImportCommand( this );
        }

        public ObservableCollection<NetbarItem> NetbarList {
            get {
                return netbarList;
            }
        }

        public LoadExcelCommand LoadExcelCommand {
            get {
                return loadExcelCommand;
            }
        }

        public ImportCommand ImportCommand {
            get {
                return importCommand;
            }
        }

        public void UpdateNetbarList( List<NetbarItem> netbarList ) {
            this.netbarList.Clear();
            netbarList.ForEach( this.netbarList.Add );
        }
    }
}
