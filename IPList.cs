using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NetbarIpAddrImporter {
    class IPList {
        public string IP1 {
            get;
            set;
        }
        public string IP2 {
            get;
            set;
        }
        public string IP3 {
            get;
            set;
        }
        public string IP4 {
            get;
            set;
        }

        public UInt32 IntIP1 {
            get {
                return IP2Int( IP1 );
            }
        }

        public UInt32 IntIP2 {
            get {
                return IP2Int( IP2 );
            }
        }

        public UInt32 IntIP3 {
            get {
                return IP2Int( IP3 );
            }
        }

        public UInt32 IntIP4 {
            get {
                return IP2Int( IP4 );
            }
        }

        public static UInt32 IP2Int( string ipStr ) {
            if( string.IsNullOrEmpty( ipStr ) ) {
                return 0;
            }
            string[] ip = ipStr.Split( '.' );
            if( ip.Length == 4 ) {
                uint ipCode = 0xFFFFFF00 | byte.Parse( ip[ 3 ] );
                ipCode = ipCode & 0xFFFF00FF | ( uint.Parse( ip[ 2 ] ) << 0x8 );
                ipCode = ipCode & 0xFF00FFFF | ( uint.Parse( ip[ 1 ] ) << 0x10 );
                ipCode = ipCode & 0x00FFFFFF | ( uint.Parse( ip[ 0 ] ) << 0x18 );
                return ipCode;
            }
            return 0;
        }

        public static Byte[] StructToBytes( Object structure, Byte[] bytes, int iByteLen ) {
            Int32 size = Marshal.SizeOf( structure );
            IntPtr buffer = Marshal.AllocHGlobal( size );
            try {
                Marshal.StructureToPtr( structure, buffer, false );
                Marshal.Copy( buffer, bytes, iByteLen, size );
                return bytes;
            } finally {
                Marshal.FreeHGlobal( buffer );
            }
        }
    }
}
