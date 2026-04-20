//using Finbuckle.MultiTenant.EntityFrameworkCore.Extensions;
using FSH.Modules.StudentManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FSH.Modules.StudentManagement.Persistence.Configurations;

public class EleveConfiguration : IEntityTypeConfiguration<Eleve>
{
    public void Configure(EntityTypeBuilder<Eleve> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.ToTable("Eleves", "student");
        //builder.IsMultiTenant();
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nom).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Prenom).HasMaxLength(100).IsRequired();
        builder.HasIndex(x => x.Matricule).IsUnique();
        builder.Property(x => x.LieuNaissance).HasMaxLength(200);
        builder.Property(x => x.Adresse).HasMaxLength(500);
        builder.Property(x => x.Telephone).HasMaxLength(20);
        builder.Property(x => x.Email).HasMaxLength(256);
        builder.Property(x => x.NomParent).HasMaxLength(200);
        builder.Property(x => x.TelephoneParent).HasMaxLength(20);
        builder.Property(x => x.Sexe).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Property(x => x.Statut).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.HasIndex(x => x.TenantId);
    }

   
}
