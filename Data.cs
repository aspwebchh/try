using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using NetBarAPI = NetbarIpAddrImporter.com.m3guocdn.api.app;
using DBUtility;

namespace NetbarIpAddrImporter {
    partial class Data {
        private const string AUDIT_SUCCESS = "S10000";
        private const string AUDIT_IP_EXISTS = "S10005";
        private const string AUDIT_UNKDOW = "S10007";

        private const int STATE_SYNC_SUCCESS = 1;
        private const int STATE_SYNC_FAIL = 2;
        private const int STATE_DELETE_SUCCESS = 3;
        private const int STATE_DELETE_FAIL = 4;
        private const int STATE_OTHER = -1;

        private static NetBarAPI.MSGetUserDatumApi api = new NetBarAPI.MSGetUserDatumApi();

        public static List<NetbarItem> Import( List<NetbarItem> dataList ) {
            var tasks = dataList.Select( item => {
                return Task.Factory.StartNew( () => {
                    return HandleItem( item );
                } );
            } );
            return tasks.Select( item => item.Result ).ToList();
        }

        public static List<Tuple<int, string, string>> SyncToGame(List<Tuple<int, string, IPList>> dataList) {
            var tasks = dataList.Select( item => {
                return Task.Factory.StartNew( delegate {
                    return SyncItem( item );
                } );
            } );
            return tasks.Select( task => task.Result ).ToList();
        }

        public static Tuple<int, string, string> SyncItem( Tuple<int, string, IPList> item) {
            var id = item.Item1;
            var name = item.Item2;
            var ipList = item.Item3;

            var themebar = new NetBarAPI.ThemeBar();

            themebar.acBarName = name;
            themebar.iBarID = 0;
            themebar.iLevel = 1;
            themebar.iOprType = 0;
            themebar.acIP1 = ipList.IP1;
            themebar.acIP2 = ipList.IP2;
            themebar.acIP3 = ipList.IP3;
            themebar.acIP4 = ipList.IP4;
            themebar.iNetBarId = id;
            themebar = api.iBindNetBar( themebar );

            int level = 0;
            int apply = 0;
            int audit = 0;
            if( themebar.Error == AUDIT_SUCCESS || themebar.Error == AUDIT_IP_EXISTS ) {
                level = 1;
                apply = 1;
                audit = 1;
            }
            string sql = "update tb_netbar set _tmp_state = '" + themebar.Error + "',ibarid = " + themebar.iValue + " , ilevel = " + level + ", iapply = " + apply + ", iaudit=" + audit + "  where id = " + id;
            DbHelperSQL.Query( sql );

            return Tuple.Create( item.Item1, item.Item2, themebar.Error );
        }
    }
}
