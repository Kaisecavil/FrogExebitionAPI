﻿using Castle.Core.Resource;
using FrogExebitionAPI.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrogExebitionAPI.Models
{
    public class Vote : BaseModel 
    {
        [Required]
        public string ApplicationUserId { get; set; }
        [Required]
        public Guid FrogOnExebitionId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;
        public virtual FrogOnExebition FrogOnExebition { get; set; } = null!;
    }
}
