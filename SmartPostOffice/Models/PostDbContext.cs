using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SmartPostOffice.Models;

public partial class PostDbContext : DbContext
{
    public PostDbContext() { }

    public PostDbContext(DbContextOptions<PostDbContext> options) : base(options) { }

    public virtual DbSet<BungalowBooking> BungalowBookings { get; set; }
    public virtual DbSet<CashBook> CashBooks { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<MailItem> MailItems { get; set; }
    public virtual DbSet<Payment> Payments { get; set; }
    public virtual DbSet<Person> People { get; set; }
    public virtual DbSet<PostOfficer> PostOfficers { get; set; }
    public virtual DbSet<Receipt> Receipts { get; set; }
    public virtual DbSet<ServiceRequest> ServiceRequests { get; set; }
    public virtual DbSet<StampOrder> StampOrders { get; set; }
    public virtual DbSet<TeleMailDetail> TeleMailDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=post_db;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1. ServiceRequest (Sprint 1)
        modelBuilder.Entity<ServiceRequest>(entity => {
            entity.HasKey(e => e.Id); // මෙතන Id එක PK විදිහට දාන්න
            entity.ToTable("ServiceRequest");
            entity.Property(e => e.Weight).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
        });

        // 2. BungalowBooking (Error එක Fix කිරීම)
        modelBuilder.Entity<BungalowBooking>(entity => {
            entity.HasKey(e => e.BookingId); 
            entity.ToTable("BungalowBooking");
        });

        // 3. Customer & Person (One-to-One Fix)
        modelBuilder.Entity<Customer>(entity => {
            entity.HasKey(e => e.CustomerId);
            entity.ToTable("Customer");
            entity.HasOne(d => d.CustomerNavigation).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.CustomerId);
        });

        // 4. CashBook
        modelBuilder.Entity<CashBook>(entity => {
            entity.HasKey(e => e.EntryId);
            entity.ToTable("CashBook");
            entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
        });

        // 5. MailItem
        modelBuilder.Entity<MailItem>(entity => {
            entity.HasKey(e => e.ItemId);
            entity.ToTable("MailItem");
            entity.Property(e => e.Weight).HasColumnType("decimal(18,2)");
        });

        // 6. Payment
        modelBuilder.Entity<Payment>(entity => {
            entity.HasKey(e => e.PaymentId);
            entity.ToTable("Payment");
            entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
        });

        // 7. Person
        modelBuilder.Entity<Person>(entity => {
            entity.HasKey(e => e.PersonId);
            entity.ToTable("Person");
        });

        // 8. PostOfficer
        modelBuilder.Entity<PostOfficer>(entity => {
            entity.HasKey(e => e.OfficerId);
            entity.ToTable("PostOfficer");
            entity.HasOne(d => d.Officer).WithOne(p => p.PostOfficer)
                .HasForeignKey<PostOfficer>(d => d.OfficerId);
        });

        // 9. Receipt
        modelBuilder.Entity<Receipt>(entity => {
            entity.HasKey(e => e.ReceiptId);
            entity.ToTable("Receipt");
        });

        // 10. StampOrder
        modelBuilder.Entity<StampOrder>(entity => {
            entity.HasKey(e => e.OrderId);
            entity.ToTable("StampOrder");
        });

        // 11. TeleMailDetail
        modelBuilder.Entity<TeleMailDetail>(entity => {
            entity.HasKey(e => e.TeleMailId);
            entity.ToTable("TeleMailDetail");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}