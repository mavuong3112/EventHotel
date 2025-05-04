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
    public partial class HD_DOITAC
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_DoiTac { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHD { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }

        public virtual DOI_TAC DOI_TAC { get; set; }

        public virtual HOAT_DONG HOAT_DONG { get; set; }
    }
}
