﻿using Ignite.Data.Enums;
using Ignite.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ignite.Areas.Admin.ViewModels
{
    public class CourseNameViewModel
    {
        public CourseNameViewModel()
        {
            this.Users = new List<UserAssignedViewModel>();
        }

        public string Name { get; set; }

        public DateTime DueDate { get; set; }

        public bool Type { get; set; }

        public List<UserAssignedViewModel> Users { get; set; }

        public int CourseId { get; set; }

    }
}