using Microsoft.EntityFrameworkCore;
namespace AssignmentApp.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(AssignmentDbContext context)
        {
            context.Database.Migrate();
        }
    }
}
