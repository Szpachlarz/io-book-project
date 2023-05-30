using Microsoft.AspNetCore.Identity;
using io_book_project.Data.Enum;
using io_book_project.Models;
using System.Diagnostics;
using System.Net;

namespace io_book_project.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();
                
                if (!context.Authors.Any())
                {
                    context.Authors.AddRange(new List<Author>()
                    {
                        new Author()
                        {
                            Names = "Arkadiusz",
                            Surname = "Gront",
                            Nationality = "żydowska",
                            DateOfBirth = DateTime.Parse("2000-02-26"),
                            DateOfDeath = null,
                        },
                        new Author()
                        {
                            Names = "Rafał",
                            Surname = "Gomola",
                            Nationality = "albańska",
                            DateOfBirth = DateTime.Parse("2000-11-13"),
                            DateOfDeath = DateTime.Parse("2023-05-27"),
                        }
                    });
                    context.SaveChanges();
                }
                if (!context.Publishers.Any())
                {
                    context.Publishers.AddRange(new List<Publisher>()
                    {
                        new Publisher()
                        {
                            Name = "Wydawnictwo Politechniki Śląskiej",
                            Country = "Polska",
                            City = "Gliwice",
                        },
                    });
                    context.SaveChanges();
                }
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(new List<Category>()
                    {
                        new Category()
                        {
                            Name = "Grzybiarstwo",
                        },
                    });
                    context.SaveChanges();
                }
                if (!context.Books.Any())
                {
                    context.Books.AddRange(new List<Book>()
                    {
                        new Book()
                        {
                            Title = "Atlas grzybów",
                            OriginalTitle = null,
                            ISBN = 1234567890123,
                            PublicationYear = DateTime.Parse("2022-01-01"),
                            FirstPublicationYear = null,
                            Language = "polski",
                            OriginalLanguage = null,
                            Translation = null,
                            PageCount = 123,
                            Series = null,
                            Description = "Wyjątkowy atlas grzybów jadalnych i trujących",
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            CoverImagePath = "https://cdn.discordapp.com/attachments/808020274691833907/1100042803389665360/20230424_140702.jpg",
                            PublisherId = 1,
                        },
                    });
                    context.SaveChanges();
                }
                if (!context.BookCategories.Any())
                {
                    context.BookCategories.AddRange(new List<BookCategory>()
                    {
                        new BookCategory()
                        {
                            BookId = 1,
                            CategoryId = 1,
                        },
                    });
                    context.SaveChanges();
                }
                if (!context.BookAuthors.Any())
                {
                    context.BookAuthors.AddRange(new List<BookAuthor>()
                    {
                        new BookAuthor()
                        {
                            BookId = 1,
                            AuthorId = 1,
                        },
                        new BookAuthor()
                        {
                            BookId = 1,
                            AuthorId = 2,
                        },
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(Role.Admin))
                    await roleManager.CreateAsync(new IdentityRole(Role.Admin));
                if (!await roleManager.RoleExistsAsync(Role.User))
                    await roleManager.CreateAsync(new IdentityRole(Role.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "admin@mail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        UserName = "administrator",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Status = 0,
                    };
                    await userManager.CreateAsync(newAdminUser, "Haslo@123");
                    await userManager.AddToRoleAsync(newAdminUser, Role.Admin);
                }

                string appUserEmail = "user@mail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new User()
                    {
                        UserName = "uzytkownik",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Status = 0,
                    };
                    await userManager.CreateAsync(newAppUser, "Haslo@456");
                    await userManager.AddToRoleAsync(newAppUser, Role.User);
                }
            }            
        }
    }
}