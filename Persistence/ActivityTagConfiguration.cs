using System;
using System.Diagnostics;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Migrations;

public class ActivityTagConfiguration : IEntityTypeConfiguration<ActivityTag>
{
    public void Configure(EntityTypeBuilder<ActivityTag> builder)
    {
         builder.HasKey(at => new { at.ActivityId, at.TagId });
        
    }    
}
