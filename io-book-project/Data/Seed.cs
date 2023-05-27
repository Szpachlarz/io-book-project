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

                if (!context.Users.Any())
                {
                    context.Users.AddRange(new List<User>()
                    {
                        new User()
                        {
                            Username = "Admin",
                            Password = "87654321",
                            Email = "admin@mail.com",
                            Role = Role.admin,
                            Status = Status.active,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                         },
                        new User()
                        {
                            Username = "User",
                            Password = "87654321",
                            Email = "user@mail.com",
                            Role = Role.user,
                            Status = Status.active,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                        }
                    });
                    context.SaveChanges();
                }
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

        /*public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "teddysmithdeveloper@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "teddysmithdev",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }*/
    }
}