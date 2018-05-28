using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NetbarIpAddrImporter {
    /// <summary>
    /// DeleteNetbar.xaml 的交互逻辑
    /// </summary>
    public partial class DeleteNetbar : Window {
        public DeleteNetbar() {
            InitializeComponent();
        }

        private void Button_Click( object sender, RoutedEventArgs e ) {
            var s_netbarName = NetbarName.Text.Trim();
            var s_iNetbarId = iNetbarID.Text.Trim();
            var s_iBarId = iBarID.Text.Trim();

            if( string.IsNullOrEmpty( s_netbarName ) ) {
                MessageBox.Show( "请输入网吧名称" );
                return; 
            }

            if( string.IsNullOrEmpty( s_iNetbarId ) ) {
                MessageBox.Show( "请输入iNetbarId" );
                return;
            }

            if( string.IsNullOrEmpty( s_iBarId ) ) {
                MessageBox.Show( "请输入iBarId" );
                return;
            }

            var netbarName = s_netbarName;
            var iNetbarId = Convert.ToInt32( s_iNetbarId );
            var iBarId = Convert.ToInt32( s_iBarId );

            var msg = Data.Delete( iBarId, iNetbarId, netbarName );
            MessageBox.Show( msg );
        }
    }
}
