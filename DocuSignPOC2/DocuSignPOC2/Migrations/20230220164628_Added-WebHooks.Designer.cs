﻿// <auto-generated />
using System;
using DocuSignPOC2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DocuSignPOC2.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230220164628_Added-WebHooks")]
    partial class AddedWebHooks
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DocuSignPOC2.Models.ESignDocument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DocuSignId")
                        .HasColumnType("text");

                    b.Property<Guid?>("EnvelopeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EnvelopeId");

                    b.ToTable("ESignDocuments");
                });

            modelBuilder.Entity("DocuSignPOC2.Models.Envelope", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DocuSignId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Envleopes");
                });

            modelBuilder.Entity("DocuSignPOC2.Models.Party", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("DocuSignId")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Parties");
                });

            modelBuilder.Entity("DocuSignPOC2.Models.WebHook", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Json")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("WebHooks");
                });

            modelBuilder.Entity("EnvelopeParty", b =>
                {
                    b.Property<Guid>("EnvelopesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PartiesId")
                        .HasColumnType("uuid");

                    b.HasKey("EnvelopesId", "PartiesId");

                    b.HasIndex("PartiesId");

                    b.ToTable("EnvelopeParty");
                });

            modelBuilder.Entity("DocuSignPOC2.Models.ESignDocument", b =>
                {
                    b.HasOne("DocuSignPOC2.Models.Envelope", "Envelope")
                        .WithMany("ESignDocuments")
                        .HasForeignKey("EnvelopeId");

                    b.Navigation("Envelope");
                });

            modelBuilder.Entity("EnvelopeParty", b =>
                {
                    b.HasOne("DocuSignPOC2.Models.Envelope", null)
                        .WithMany()
                        .HasForeignKey("EnvelopesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DocuSignPOC2.Models.Party", null)
                        .WithMany()
                        .HasForeignKey("PartiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DocuSignPOC2.Models.Envelope", b =>
                {
                    b.Navigation("ESignDocuments");
                });
#pragma warning restore 612, 618
        }
    }
}
