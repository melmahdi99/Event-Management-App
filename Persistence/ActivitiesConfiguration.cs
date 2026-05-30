using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence;

public class ActivitiesConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.ToTable(t => t.HasCheckConstraint("CK_MIN_TITLE_LENGTH", "LEN(Title) > 4"));
        // creating composite key for n-m relationship
    }    
}
