/*
 *          Copyright (c) 2018 Rafael Almeida (ralms@ralms.net)
 *
 *           Ralms.Microsoft.EntitityFrameworkCore.Extensions
 *
 * THIS MATERIAL IS PROVIDED AS IS, WITH ABSOLUTELY NO WARRANTY EXPRESSED
 * OR IMPLIED.  ANY USE IS AT YOUR OWN RISK.
 *
 * Permission is hereby granted to use or copy this program
 * for any purpose,  provided the above notices are retained on all copies.
 * Permission to modify the code and to distribute modified code is granted,
 * provided the above notices are retained, and a notice that the code was
 * modified is included with the above copyright notice.
 *
 */

namespace Microsoft.EntityFrameworkCore
{
    public static class SqlServerHints
    {
        public static readonly string FORCESCAN = "FORCESCAN";
        public static readonly string HOLDLOCK = "HOLDLOCK";
        public static readonly string NOLOCK = "NOLOCK";
        public static readonly string NOWAIT = "NOWAIT";
        public static readonly string PAGLOCK = "PAGLOCK";
        public static readonly string READCOMMITTED = "READCOMMITTED";
        public static readonly string READCOMMITTEDLOCK = "READCOMMITTEDLOCK";
        public static readonly string READPAST = "READPAST";
        public static readonly string READUNCOMMITTED = "READUNCOMMITTED";
        public static readonly string REPEATABLEREAD = "REPEATABLEREAD";
        public static readonly string ROWLOCK = "ROWLOCK";
        public static readonly string SERIALIZABLE = "SERIALIZABLE";
        public static readonly string SNAPSHOT = "SNAPSHOT";
        public static readonly string TABLOCK = "TABLOCK";
        public static readonly string UPDLOCK = "UPDLOCK";
        public static readonly string XLOCK = "XLOCK"; 
    } 
}
