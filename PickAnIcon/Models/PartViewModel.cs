using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PickAnIcon.Models
{
    public class PartViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Image")]
        public string FileName { get; set; }

        [Display(Name = "IsFree")]
        public bool IsFree { get; set; }
    }
}
