﻿using System.ComponentModel.DataAnnotations;

namespace Belvoir.DAL.DTO
{
    public class TailorViewDTO
    {
        [Required] public string Name { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string Phone { get; set; }

        [Required] public string experince { get; set; }
    }
}
