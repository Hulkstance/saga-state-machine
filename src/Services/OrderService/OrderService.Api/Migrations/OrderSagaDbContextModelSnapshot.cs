﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OrderService.Api.Sagas;

#nullable disable

namespace OrderService.Api.Migrations
{
    [DbContext(typeof(OrderSagaDbContext))]
    partial class OrderSagaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OrderService.Api.Sagas.OrderState", b =>
                {
                    b.Property<Guid>("CorrelationId")
                        .HasColumnType("uuid");

                    b.Property<string>("CurrentState")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("CustomerEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("OrderTotal")
                        .HasColumnType("numeric");

                    b.Property<string>("PaymentIntentId")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("CorrelationId");

                    b.ToTable("OrderState");
                });
#pragma warning restore 612, 618
        }
    }
}
