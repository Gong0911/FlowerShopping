//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlowerShopping.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public int CommentID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public string CommContent { get; set; }
        public Nullable<System.DateTime> CommentTime { get; set; }
    
        public virtual UserInfo UserInfo { get; set; }
    }
}
