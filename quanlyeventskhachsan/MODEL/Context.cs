using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace quanlyeventskhachsan.MODEL
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<DANH_GIA> DANH_GIA { get; set; }
        public virtual DbSet<DANHGIA_DETAILS> DANHGIA_DETAILS { get; set; }
        public virtual DbSet<DOI_TAC> DOI_TAC { get; set; }
        public virtual DbSet<QUAN_LY> QUAN_LY { get; set; }
        public virtual DbSet<HANG_MUC> HANG_MUC { get; set; }
        public virtual DbSet<HD_DOITAC> HD_DOITAC { get; set; }
        public virtual DbSet<HD_QUANLY> HD_QUANLY { get; set; }
        public virtual DbSet<HD_NHAN_VIEN> HD_NHAN_VIEN { get; set; }
        public virtual DbSet<HD_TAITRO> HD_TAITRO { get; set; }
        public virtual DbSet<HOAT_DONG> HOAT_DONG { get; set; }
        public virtual DbSet<CO_SO> CO_SOs { get; set; }
        public virtual DbSet<PHU_TRACH> PHU_TRACH { get; set; }
        public virtual DbSet<NHAN_VIEN> NHAN_VIEN { get; set; }
        public virtual DbSet<TAI_CHINH> TAI_CHINH { get; set; }
        public virtual DbSet<TAI_TRO> TAI_TRO { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DANH_GIA>()
                .Property(e => e.MaDanhGia)
                .IsFixedLength();

            modelBuilder.Entity<DANH_GIA>()
                .Property(e => e.MSNV)
                .IsFixedLength();

            modelBuilder.Entity<DANH_GIA>()
                .HasMany(e => e.DANHGIA_DETAILS)
                .WithRequired(e => e.DANH_GIA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DANHGIA_DETAILS>()
                .Property(e => e.MaDanhGia)
                .IsFixedLength();

            modelBuilder.Entity<DOI_TAC>()
                .Property(e => e.SDT)
                .IsFixedLength();

            modelBuilder.Entity<DOI_TAC>()
                .HasMany(e => e.HD_DOITAC)
                .WithRequired(e => e.DOI_TAC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QUAN_LY>()
                .Property(e => e.MaQL)
                .IsUnicode(false);

            modelBuilder.Entity<QUAN_LY>()
                .Property(e => e.CoSo)
                .IsUnicode(false);

            modelBuilder.Entity<QUAN_LY>()
                .HasMany(e => e.HD_QUANLY )
                .WithRequired(e => e.QUAN_LY)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QUAN_LY>()
                .HasMany(e => e.PHU_TRACH)
                .WithRequired(e => e.QUAN_LY)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HANG_MUC>()
                .HasMany(e => e.DANHGIA_DETAILS)
                .WithRequired(e => e.HANG_MUC)
                .HasForeignKey(e => e.HangMuc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HD_QUANLY>()
                .Property(e => e.MaQL)
                .IsUnicode(false);

            modelBuilder.Entity<HD_NHAN_VIEN>()
                .Property(e => e.MSNV)
                .IsFixedLength();

            modelBuilder.Entity<HOAT_DONG>()
                .HasMany(e => e.HD_DOITAC)
                .WithRequired(e => e.HOAT_DONG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HOAT_DONG>()
                .HasMany(e => e.HD_QUANLY)
                .WithRequired(e => e.HOAT_DONG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HOAT_DONG>()
                .HasMany(e => e.HD_NHAN_VIEN)
                .WithRequired(e => e.HOAT_DONG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HOAT_DONG>()
                .HasMany(e => e.HD_TAITRO)
                .WithRequired(e => e.HOAT_DONG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CO_SO>()
                .Property(e => e.MaCoSo)
                .IsUnicode(false);

            modelBuilder.Entity<CO_SO>()
                .Property(e => e.SDT)
                .IsFixedLength();

            modelBuilder.Entity<CO_SO>()
                .HasMany(e => e.QUAN_LY)
                .WithOptional(e => e.CO_SO1)
                .HasForeignKey(e => e.CoSo);

            modelBuilder.Entity<CO_SO>()
                .HasMany(e => e.NHAN_VIEN)
                .WithOptional(e => e.CO_SO1)
                .HasForeignKey(e => e.CoSo);

            modelBuilder.Entity<PHU_TRACH>()
                .Property(e => e.MaQL)
                .IsUnicode(false);

            modelBuilder.Entity<NHAN_VIEN>()
                .Property(e => e.MSNV)
                .IsFixedLength();

            modelBuilder.Entity<NHAN_VIEN>()
                .Property(e => e.CoSo)
                .IsUnicode(false);

            modelBuilder.Entity<NHAN_VIEN>()
                .HasMany(e => e.HD_NHAN_VIEN)
                .WithRequired(e => e.NHAN_VIEN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAI_CHINH>()
                .Property(e => e.UEF)
                .HasPrecision(12, 0);

            modelBuilder.Entity<TAI_CHINH>()
                .Property(e => e.TaiTro)
                .HasPrecision(12, 0);

            modelBuilder.Entity<TAI_TRO>()
                .Property(e => e.SDT)
                .IsFixedLength();

            modelBuilder.Entity<TAI_TRO>()
                .HasMany(e => e.HD_TAITRO)
                .WithRequired(e => e.TAI_TRO)
                .WillCascadeOnDelete(false);
        }
    }
}
