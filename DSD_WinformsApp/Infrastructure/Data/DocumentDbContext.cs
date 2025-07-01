using System;
using System.Collections.Generic;
using DSD_WinformsApp.Core.DTOs;
using DSD_WinformsApp.Model;
using Microsoft.EntityFrameworkCore;

namespace DSD_WinformsApp.Infrastructure.Data;

public partial class DocumentDbContext : DbContext, IDocumentDbContext
{ 
    public DocumentDbContext()
    {
    }
    
    public DocumentDbContext(DbContextOptions<DocumentDbContext> options)
        : base(options)
    {
        Database.EnsureCreatedAsync().Wait();
    }
    public DbSet<DocumentModel> Documents { get; set; }
    public DbSet<BackUpFileModel> BackupFiles { get; set; }
    public DbSet<UserCredentialsModel> UserCredentials { get; set; }

    public void SetModified(object entity)
    {
        Entry(entity).State = EntityState.Modified;
    }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=DSMDb;Integrated Security=True;Trust Server Certificate=true");
   
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("localhostConnectionString");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
