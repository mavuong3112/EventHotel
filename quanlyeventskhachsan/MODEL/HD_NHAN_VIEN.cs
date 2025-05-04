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
    public partial class HD_NHAN_VIEN
    {
        //HD_SINHVIEN.cs
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHD { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MSNV { get; set; }

        [StringLength(50)]
        public string VaiTro { get; set; }

        [Column(TypeName = "ntext")]
        public string GhiChu { get; set; }

        public virtual HOAT_DONG HOAT_DONG { get; set; }

        public virtual NHAN_VIEN NHAN_VIEN { get; set; }
    }
}