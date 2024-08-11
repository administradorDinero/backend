﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repositorio;

#nullable disable

namespace Repositorio.Migrations
{
    [DbContext(typeof(PostgresContext))]
    [Migration("20240811174336_migrationnotfound7")]
    partial class migrationnotfound7
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Entidades.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoriaNo")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CategoriaContext");
                });

            modelBuilder.Entity("Entidades.Cuenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("descripcion")
                        .HasColumnType("text");

                    b.Property<int?>("personaId")
                        .HasColumnType("integer");

                    b.Property<double?>("valor")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("personaId");

                    b.ToTable("CuentaContext");
                });

            modelBuilder.Entity("Entidades.Persona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("clave")
                        .HasColumnType("text");

                    b.Property<string>("correo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PersonaContext");
                });

            modelBuilder.Entity("Entidades.Tipo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TipoContext");
                });

            modelBuilder.Entity("Entidades.Transaccion", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("CuentaId")
                        .HasColumnType("integer")
                        .HasColumnName("cuentaId");

                    b.Property<double>("cantidad")
                        .HasColumnType("double precision");

                    b.Property<int?>("categoriaId")
                        .HasColumnType("integer");

                    b.Property<string>("descripcion")
                        .HasColumnType("text");

                    b.Property<DateTime>("fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("tipoId")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("CuentaId");

                    b.HasIndex("categoriaId");

                    b.HasIndex("tipoId");

                    b.ToTable("TransaccionContext");
                });

            modelBuilder.Entity("Entidades.Cuenta", b =>
                {
                    b.HasOne("Entidades.Persona", "persona")
                        .WithMany("Cuentas")
                        .HasForeignKey("personaId");

                    b.Navigation("persona");
                });

            modelBuilder.Entity("Entidades.Transaccion", b =>
                {
                    b.HasOne("Entidades.Cuenta", "cuenta")
                        .WithMany("Transaccions")
                        .HasForeignKey("CuentaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entidades.Categoria", "categoria")
                        .WithMany()
                        .HasForeignKey("categoriaId");

                    b.HasOne("Entidades.Tipo", "tipo")
                        .WithMany()
                        .HasForeignKey("tipoId");

                    b.Navigation("categoria");

                    b.Navigation("cuenta");

                    b.Navigation("tipo");
                });

            modelBuilder.Entity("Entidades.Cuenta", b =>
                {
                    b.Navigation("Transaccions");
                });

            modelBuilder.Entity("Entidades.Persona", b =>
                {
                    b.Navigation("Cuentas");
                });
#pragma warning restore 612, 618
        }
    }
}
