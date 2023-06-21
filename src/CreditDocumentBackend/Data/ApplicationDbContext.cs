// CreditDocumentBackend/Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using SharedModels;

namespace CreditDocumentBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CreditDocument> CreditDocuments { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<DocumentAttachment> DocumentAttachments { get; set; }
    }
}