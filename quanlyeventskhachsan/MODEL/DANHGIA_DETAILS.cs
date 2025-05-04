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
    public partial class DANHGIA_DETAILS
    {
        //DANHGIA_DETAILS.cs
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaDanhGia { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HangMuc { get; set; }

        public int? MucDo { get; set; }

        [Column(TypeName = "ntext")]
        public string GhiChu { get; set; }

        public virtual DANH_GIA DANH_GIA { get; set; }

        public virtual HANG_MUC HANG_MUC { get; set; }
    }
}
