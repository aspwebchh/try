using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using System.Collections.ObjectModel;

namespace NetbarIpAddrImporter {
    enum NetbarItemStatus {
        Normal, Database, SyncSuccess, SyncError
    }

    class NetbarItem {
        public int ID {
            get; set;
        }

        public string Manager {
            get; set;
        }

        public string Address {
            get; set;
        }

        public string Tel {
            get; set;
        }

        public string Name {
            get; set;
        }

        public string IPString {
            get; set;
        }

        public NetbarItemStatus Status {
            get; set;
        }

        public string StatusDesc {
            get {
                if( Status == NetbarItemStatus.Normal ) {
                    return "未导入";
                } else if( Status == NetbarItemStatus.Database ) {
                    return "已导入数据库";
                } else if( Status == NetbarItemStatus.SyncSuccess ) {
                    return "同步成功";
                } else if( Status == NetbarItemStatus.SyncError ) {
                    return "同步失败";
                } else {
                    return "位置状态";
                }
                
            }
        }

        public string[] IP {
            get {
                var ips = Regex.Split( IPString, @"\||/" );
                return ips;
            }
        }

        public int IPCount {
            get {
                return IP.Count();
            }
        }

        public IPList IPList {
            get {
                return Convert2IPList( IP );
            }
        }

        private static IPList Convert2IPList( string[] ip ) {
            var list = ip.ToList();
            IPList ipList = new IPList();
            ipList.IP1 = list.Count >= 1 ? list[ 0 ] : null;
            ipList.IP2 = list.Count >= 2 ? list[ 1 ] : null;
            ipList.IP3 = list.Count >= 3 ? list[ 2 ] : null;
            ipList.IP4 = list.Count >= 4 ? list[ 3 ] : null;
            return ipList;
        }

        public static List<NetbarItem> Data2List( DataTable dt ) {
            var dataList = from row in dt.Rows.Cast<DataRow>()
                           select new NetbarItem {
                               Name = row[ "F1" ].ToString(),
                               Address = row[ "F2" ].ToString(),
                               Tel = row[ "F3" ].ToString(),
                               Manager = row[ "F4" ].ToString(),
                               IPString = row[ "F5" ].ToString(),
                               Status = NetbarItemStatus.Normal
                           };
            return dataList.ToList();
        }
    }
}
