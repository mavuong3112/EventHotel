namespace quanlyeventskhachsan.MODEL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using StackExchange.Redis;
    public partial class NHAN_VIEN
    {
        //NHAN_VIEN.cs
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHAN_VIEN()
        {
            DANH_GIA = new HashSet<DANH_GIA>();
            HD_NHAN_VIEN = new HashSet<HD_NHAN_VIEN>();
        }

        public NHAN_VIEN(string mSNV, string hoTen, string coso, string tencoso)
        {
            MSNV = mSNV;
            HoTen = hoTen;
            CoSo = coso;
            CO_SO1.TenCoSo = tencoso ;
        }

        [Key]
        [StringLength(10)]
        public string MSNV { get; set; }

        [StringLength(100)]
        public string HoTen { get; set; }

        [StringLength(50)]
        public string CoSo { get; set; }

        public bool? Hide { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DANH_GIA> DANH_GIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HD_NHAN_VIEN> HD_NHAN_VIEN { get; set; }

        public virtual CO_SO CO_SO1 { get; set; }
    }
}
