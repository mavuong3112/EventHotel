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
    public partial class PHU_TRACH
    {
        //PHU_TRACH.cs
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string MaQL { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string SuKien { get; set; }

        [Column(TypeName = "ntext")]
        public string GhiChu { get; set; }

        public virtual QUAN_LY QUAN_LY { get; set; }
    }
}
