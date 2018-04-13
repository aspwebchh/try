using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetbarIpAddrImporter {
    partial class Data {
        private static NetbarItem HandleItem( NetbarItem netbarItem ) {
            var sql = @"INSERT INTO [dbo].[tb_NetBar]
           ([iBarId]
           ,[acName]
           ,[acContact]
           ,[acTel]
           ,[iProvince]
           ,[iCity]
           ,[iDistrict]
           ,[acAddress]
           ,[acPost]
           ,[acQQ]
           ,[acPicture]
           ,[acBulletin]
           ,[acInfo]
           ,[iLevel]
           ,[acUser]
           ,[iUserId]
           ,[dtApplyDate]
           ,[iApply]
           ,[dtAuditDate]
           ,[iAudit]
           ,[dtAddTime]
           ,[iSort]
           ,[dtSortTime]
           ,[flag]
           ,[comefrom]
           ,[score]
           ,[Jifenpay]
           ,[Regcount]
           ,[Logincount]
           ,[Sqvip]
           ,[Sqvipdate]
           ,[Sqrank]
           ,[Sqrankdate]
           ,[audit_deny_reason]
           ,[audit_deny]
           ,[_tmp_type]
           ,[_tmp_state]
           ,[_tmp_del_state]) output inserted.id
     VALUES
           (0
           ,'{netbar}'
           ,'{name}'
           ,'{tel}'
           ,11
           ,78
           ,789
           ,'{addr}'
           ,null
           ,null
           ,null
           ,null
           ,null
           ,0
           ,''
           ,0
           ,getdate()
           ,0
           ,getdate()
           ,0
           ,getdate()
           ,0
           ,getdate()
           ,0
           ,0
           ,0
           ,0
           ,0
           ,0
           ,0
           ,getdate()
           ,0
           ,getdate()
           ,null
           ,0
           ,1
           ,null
           ,null);";

            sql = sql.Replace( "{name}", netbarItem.Manager ).Replace( "{tel}", netbarItem.Tel ).Replace( "{netbar}", netbarItem.Name ).Replace( "{addr}", netbarItem.Address );
            var newId = Convert.ToInt32( DBUtility.DbHelperSQL.GetSingle( sql ) );
            HandleIP( newId, netbarItem.IP );
            netbarItem.ID = newId;
            netbarItem.Status = NetbarItemStatus.Database;
            return netbarItem;
        }



        private static void HandleIP( int newId, string[] ip ) {
            ip.ToList().ForEach( n => {
                var insertIpSQL = @"INSERT INTO [dbo].[tb_NetBarIP]
                                       ([nid]
                                       ,[ipAddress]
                                       ,[flag]
                                       ,[end_IP]
                                       ,[dremarks]
                                       ,[_tmp_type])
                                 VALUES
                                       ({nid}
                                       ,'{ip}'
                                       ,0
                                       ,'{ip}'
                                       ,''
                                       ,1)";
                insertIpSQL = insertIpSQL.Replace( "{nid}", newId.ToString() );
                insertIpSQL = insertIpSQL.Replace( "{ip}", n );
                DBUtility.DbHelperSQL.ExecuteSql( insertIpSQL );
            } );
        }
    }
}
