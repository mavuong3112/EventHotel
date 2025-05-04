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
    public partial class QUAN_LY
    {
        //GIAO_VIEN.cs
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QUAN_LY()
        {
            HD_QUANLY = new HashSet<HD_QUANLY>();
            PHU_TRACH = new HashSet<PHU_TRACH>();
        }
        [Key]
        [StringLength(50)]
        public string MaQL { get; set; }

        [StringLength(50)]
        public string HoTenLot { get; set; }

        [StringLength(50)]
        public string Ten { get; set; }

        [StringLength(50)]
        public string CoSo { get; set; }

        public bool Hide { get; set; }

        public virtual CO_SO CO_SO1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HD_QUANLY> HD_QUANLY { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHU_TRACH> PHU_TRACH { get; set; }
    }
}
