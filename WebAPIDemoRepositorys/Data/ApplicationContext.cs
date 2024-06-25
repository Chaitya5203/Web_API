using Microsoft.EntityFrameworkCore;
namespace WebAPIDemoRepositorys.Data;
public partial class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }
    public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
    {
    }
    public virtual DbSet<Villainfo> Villainfos { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("User ID=postgres;Password=t19;Host=localhost;Port=5432;Database=WebAPI;Pooling=true;Integrated Security=true;");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Villainfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("villainfo_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}