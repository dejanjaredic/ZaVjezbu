﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RadnoMjestoVjezba.Models;

namespace RadnoMjestoVjezba.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RadnoMjestoVjezba.Models.Kancelarija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Opis");

                    b.HasKey("Id");

                    b.ToTable("Kancelarije");
                });

            modelBuilder.Entity("RadnoMjestoVjezba.Models.KoriscenjeUrednjaja", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OsobaId");

                    b.Property<int>("UredjajId");

                    b.Property<DateTime?>("VrijemeDo");

                    b.Property<DateTime>("VrijemeOd");

                    b.HasKey("Id");

                    b.HasIndex("OsobaId");

                    b.HasIndex("UredjajId");

                    b.ToTable("KorisceniUredjaji");
                });

            modelBuilder.Entity("RadnoMjestoVjezba.Models.Osoba", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ime");

                    b.Property<int>("KancelarijaId");

                    b.Property<string>("Prezime");

                    b.HasKey("Id");

                    b.HasIndex("KancelarijaId");

                    b.ToTable("Osobe");
                });

            modelBuilder.Entity("RadnoMjestoVjezba.Models.Uredjaj", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Uredjaji");
                });

            modelBuilder.Entity("RadnoMjestoVjezba.Models.KoriscenjeUrednjaja", b =>
                {
                    b.HasOne("RadnoMjestoVjezba.Models.Osoba", "Osoba")
                        .WithMany("KoriscenjeUrednjaja")
                        .HasForeignKey("OsobaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RadnoMjestoVjezba.Models.Uredjaj", "Uredjaj")
                        .WithMany()
                        .HasForeignKey("UredjajId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RadnoMjestoVjezba.Models.Osoba", b =>
                {
                    b.HasOne("RadnoMjestoVjezba.Models.Kancelarija", "Kancelarija")
                        .WithMany("Osoba")
                        .HasForeignKey("KancelarijaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
