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
    public partial class HD_QUANLY
    {
        //HD_QUANLY.cs
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHD { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string MaQL { get; set; }

        [StringLength(50)]
        public string VaiTro { get; set; }

        public virtual QUAN_LY QUAN_LY { get; set; }

        public virtual HOAT_DONG HOAT_DONG { get; set; }
    }
}
