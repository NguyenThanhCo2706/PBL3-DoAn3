//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GUI
{
    using System;
    using System.Collections.Generic;
    
    public partial class HOA_DON_BAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOA_DON_BAN()
        {
            this.CHI_TIET_HOA_DON_BAN = new HashSet<CHI_TIET_HOA_DON_BAN>();
        }
    
        public string MaDonBan { get; set; }
        public string SDT_KH { get; set; }
        public string MaNhanVien { get; set; }
        public Nullable<System.DateTime> NgayBan { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHI_TIET_HOA_DON_BAN> CHI_TIET_HOA_DON_BAN { get; set; }
        public virtual NHAN_VIEN NHAN_VIEN { get; set; }
        public virtual KHACH_HANG KHACH_HANG { get; set; }
    }
}
