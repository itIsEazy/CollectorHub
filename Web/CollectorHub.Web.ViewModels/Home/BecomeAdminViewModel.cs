﻿namespace CollectorHub.Web.ViewModels.Home
{
    using System.ComponentModel.DataAnnotations;

    public class BecomeAdminViewModel
    {
        public BecomeAdminViewModel()
        {
        }

        [Required]
        [MaxLength(30)]
        [MinLength(10)]
        [Display(Name = "Unique Password")]
        public string UniquePassword { get; set; }
    }
}
