﻿using io_book_project.Data.Enum;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace io_book_project.Models
{
    public class User : IdentityUser
    {
        public int Status { get; set; }

        //Favourites
        public List<UserFavourite> UserFavourites { get; set; }
    }
}
