using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModularMonolith.Modules.Speakers.Core.Entities;

namespace ModularMonolith.Modules.Speakers.Core.DAL.Configurations
{
    public class SpeakerConfiguration : IEntityTypeConfiguration<Speaker>
    {
        public void Configure(EntityTypeBuilder<Speaker> builder)
        {
        }
    }
}