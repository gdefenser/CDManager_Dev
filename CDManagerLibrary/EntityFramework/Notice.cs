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
    
    public partial class Notice
    {
        public string GGTM { get; set; }
        public string GGBT { get; set; }
        public string GGDX { get; set; }
        public string DXLX { get; set; }
        public string GGNR { get; set; }
        public string LKR { get; set; }
        public Nullable<bool> FJ { get; set; }
        public Nullable<System.DateTime> LKSJ { get; set; }
        public string GLYTM { get; set; }
    
        public virtual Admin Admin { get; set; }
    }
}
