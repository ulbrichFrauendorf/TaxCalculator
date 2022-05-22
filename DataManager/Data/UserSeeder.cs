using DataManager.Models.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DataManager.Data
{
    internal static class UserSeeder
    {
        internal static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApiUser>().HasData(
                        new ApiUser
                        {
                            ApiUserId = 1,
                            Email = "admin@testsite.com",
                            Password = BCrypt.Net.BCrypt.HashPassword("P@ssw0rd")
                        });
        }
    }
}
