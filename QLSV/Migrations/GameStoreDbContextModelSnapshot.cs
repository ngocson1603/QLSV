﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QLSV;

namespace QLSV.Migrations
{
    [DbContext(typeof(GameStoreDbContext))]
    partial class GameStoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QLSV.Models.AddFundTransaction", b =>
                {
                    b.Property<string>("TransactionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("FundId")
                        .HasColumnType("int");

                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.HasIndex("FundId");

                    b.HasIndex("UserId");

                    b.ToTable("AddFundTransaction");
                });

            modelBuilder.Entity("QLSV.Models.Admin", b =>
                {
                    b.Property<string>("TaiKhoan")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("HoTen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TaiKhoan");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("QLSV.Models.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ad_Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contents")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.Property<string>("TContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Ad_Username");

                    b.ToTable("Blog");
                });

            modelBuilder.Entity("QLSV.Models.DiemHocSinh", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdHocSinh")
                        .HasColumnType("int");

                    b.Property<int>("IdKhoaHoc")
                        .HasColumnType("int");

                    b.Property<string>("NhanXet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SoDiem")
                        .HasColumnType("int");

                    b.Property<int?>("SoLan")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdHocSinh");

                    b.HasIndex("IdKhoaHoc");

                    b.ToTable("DiemHocSinh");
                });

            modelBuilder.Entity("QLSV.Models.Fund", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<float>("Tax")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Fund");
                });

            modelBuilder.Entity("QLSV.Models.GiaoVien", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("IdKhoaHoc")
                        .HasColumnType("int");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("date_of_birth")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("full_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone_number")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdKhoaHoc");

                    b.ToTable("GiaoVien");
                });

            modelBuilder.Entity("QLSV.Models.HocSinh", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Balance")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Gmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("date_of_birth")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("HocSinh");
                });

            modelBuilder.Entity("QLSV.Models.KhoaHoc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayKetThuc")
                        .HasColumnType("datetime2");

                    b.Property<string>("course_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("gia")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("KhoaHoc");
                });

            modelBuilder.Entity("QLSV.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatePurchase")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("QLSV.Models.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id", "ProductID");

                    b.HasIndex("ProductID");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("QLSV.Models.Refund", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatePurchase")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.HasIndex("UserID");

                    b.ToTable("Refund");
                });

            modelBuilder.Entity("QLSV.Models.AddFundTransaction", b =>
                {
                    b.HasOne("QLSV.Models.Fund", "Fund")
                        .WithMany("AddFundTransactions")
                        .HasForeignKey("FundId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("QLSV.Models.HocSinh", "User")
                        .WithMany("AddFundTransactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Fund");

                    b.Navigation("User");
                });

            modelBuilder.Entity("QLSV.Models.Blog", b =>
                {
                    b.HasOne("QLSV.Models.Admin", "Admin")
                        .WithMany("Blogs")
                        .HasForeignKey("Ad_Username")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("QLSV.Models.DiemHocSinh", b =>
                {
                    b.HasOne("QLSV.Models.HocSinh", "HocSinh")
                        .WithMany("DiemHocSinhs")
                        .HasForeignKey("IdHocSinh")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("QLSV.Models.KhoaHoc", "KhoaHoc")
                        .WithMany("DiemHocSinhs")
                        .HasForeignKey("IdKhoaHoc")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("HocSinh");

                    b.Navigation("KhoaHoc");
                });

            modelBuilder.Entity("QLSV.Models.GiaoVien", b =>
                {
                    b.HasOne("QLSV.Models.KhoaHoc", "KhoaHoc")
                        .WithMany("GiaoViens")
                        .HasForeignKey("IdKhoaHoc")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("KhoaHoc");
                });

            modelBuilder.Entity("QLSV.Models.Order", b =>
                {
                    b.HasOne("QLSV.Models.HocSinh", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("QLSV.Models.OrderDetail", b =>
                {
                    b.HasOne("QLSV.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("QLSV.Models.KhoaHoc", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("QLSV.Models.Refund", b =>
                {
                    b.HasOne("QLSV.Models.Order", "Order")
                        .WithMany("Refunds")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("QLSV.Models.KhoaHoc", "Product")
                        .WithMany("Refunds")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("QLSV.Models.HocSinh", "User")
                        .WithMany("Refunds")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("QLSV.Models.Admin", b =>
                {
                    b.Navigation("Blogs");
                });

            modelBuilder.Entity("QLSV.Models.Fund", b =>
                {
                    b.Navigation("AddFundTransactions");
                });

            modelBuilder.Entity("QLSV.Models.HocSinh", b =>
                {
                    b.Navigation("AddFundTransactions");

                    b.Navigation("DiemHocSinhs");

                    b.Navigation("Orders");

                    b.Navigation("Refunds");
                });

            modelBuilder.Entity("QLSV.Models.KhoaHoc", b =>
                {
                    b.Navigation("DiemHocSinhs");

                    b.Navigation("GiaoViens");

                    b.Navigation("OrderDetails");

                    b.Navigation("Refunds");
                });

            modelBuilder.Entity("QLSV.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("Refunds");
                });
#pragma warning restore 612, 618
        }
    }
}
