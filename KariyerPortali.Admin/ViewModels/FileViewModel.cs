﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace KariyerPortali.Admin.ViewModels
{
    public class FileViewModel
    {
        public int FileId { get; set; }
        [DisplayName("Başlık")]
        public string Title { get; set; }

        [DisplayName("Dosya Adı")]
        //  [Required(ErrorMessage = "Dosya uzantısı doğru değil")]
        public string FileName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        [DisplayName("Açıklama")]
        public string Description { get; set; }
        public double Size { get; set; }
    }
}