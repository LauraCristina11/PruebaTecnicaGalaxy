

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnicaGalaxy.Modelos.Entidades
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "02184cf0–9412–4cfe-afbf-51f706h72cf6",
                    Name = "Administrador",
                    NormalizedName = "ADMINISTRADOR"
                });

        var appUser = new IdentityUser
        {
            Id = "0h174cfb–4418–1c3e-a2bf-89f716w72cu3",
            Email = "laucris.jb@gmail.com",
            NormalizedEmail = "LAUCRIS.JB@GMAIL.COM",
            UserName = "laucris.jb@gmail.com",
            NormalizedUserName = "LAUCRIS.JB@GMAIL.COM",
            EmailConfirmed = true
        };

        //encriptacion password
        PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
        appUser.PasswordHash = ph.HashPassword(appUser, "1234567890");
            //se guarda el user
            modelBuilder.Entity<IdentityUser>().HasData(appUser);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() {  UserId = "0h174cfb–4418–1c3e-a2bf-89f716w72cu3", RoleId = "02184cf0–9412–4cfe-afbf-51f706h72cf6" }
                );


        }
    }
}
