//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CDManagerLibrary.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class ApplyLog
    {
        public string SQBH { get; set; }
        public Nullable<long> BookID { get; set; }
        public string DZTM { get; set; }
        public Nullable<System.DateTime> SQSJ { get; set; }
        public Nullable<int> SQZT { get; set; }
        public string CLCZY { get; set; }
        public Nullable<System.DateTime> CLSJ { get; set; }
        public string BZ { get; set; }
        public string IP { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual Reader Reader { get; set; }
    }
}
