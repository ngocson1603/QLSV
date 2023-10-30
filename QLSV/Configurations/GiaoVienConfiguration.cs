using QLSV.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QLSV.Configurations
{
    public class GiaoVienConfiguration:IEntityTypeConfiguration<GiaoVien>
    {
        public void Configure(EntityTypeBuilder<GiaoVien> builder)
        {
            builder.ToTable(nameof(GiaoVien));
            builder.HasKey(o => o.Id);
            builder.HasOne(o => o.KhoaHoc).WithMany(y => y.GiaoViens).HasForeignKey(x => x.IdKhoaHoc).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
