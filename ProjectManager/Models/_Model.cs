using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    public partial class Employee
    {
        [Display(Name = "شناسه")]
        public string Username { get; set; }

        [Display(Name = "نام")]
        public string Fullname { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }

    }
    public partial class Activity
    {
        [Display(Name = "شمارنده")]
        public System.Guid Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "عنوان فعالیت مورد نیاز است.")]
        public string Title { get; set; }

    }
    public partial class Project
    {
        [Display(Name = "مسلسل")]
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage="*")]
        public string Title { get; set; }

        [Display(Name = "ثبت")]
        public string End { get; set; }

        [Display(Name = "آغاز")]
        public string Start { get; set; }

        [Display(Name = "چکیده")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.MultilineText)]
        public string Abstract { get; set; }

        [Display(Name = "امتیاز")]
        [Required(ErrorMessage = "*")]
        public int Score { get; set; }

        [Display(Name = "شناسه کارمند")]
        [Required(ErrorMessage = "*")]
        public string EmployeeUsername { get; set; }

        [Display(Name = "فعالیت")]
        [Required(ErrorMessage = "*")]
        public System.Guid TypeId { get; set; }

    }
}