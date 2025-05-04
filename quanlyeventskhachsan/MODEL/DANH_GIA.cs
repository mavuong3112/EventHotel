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
    public partial class DANH_GIA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //DANH_GIA.cs
        public DANH_GIA()
        {
            DANHGIA_DETAILS = new HashSet<DANHGIA_DETAILS>();
        }

        [Key]
        [StringLength(10)]
        public string MaDanhGia { get; set; }

        public int? MaHD { get; set; }

        [StringLength(10)]
        public string MSNV { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }

        public bool? Hide { get; set; }

        public virtual HOAT_DONG HOAT_DONG { get; set; }

        public virtual NHAN_VIEN NHAN_VIEN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DANHGIA_DETAILS> DANHGIA_DETAILS { get; set; }
    }
}