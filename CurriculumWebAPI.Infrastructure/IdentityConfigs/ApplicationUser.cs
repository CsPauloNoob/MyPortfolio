﻿using CurriculumWebAPI.Domain;
using CurriculumWebAPI.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CurriculumWebAPI.Infrastructure.IdentityConfigs
{
    public class ApplicationUser : IdentityUser
    {
        public Curriculum? Curriculum { get; set; }
    }
}