﻿using KariyerPortali.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KariyerPortali.Admin.ViewModels
{
    public class FormInfoFormViewModel
    {
        public int FormInfoId { get; set; }

        public string FormInfoDescription { get; set; }

        public bool Required { get; set; }

        public string Value { get; set; }

        public FormType FormType { get; set; }
    }
}