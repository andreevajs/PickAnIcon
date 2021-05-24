using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PickAnIcon.Models
{
    public class IconViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "LastEdited")]
        public DateTime LastEdited { get; set; }

        public List<IconPartViewModel> Parts { get; set; }
    }

    public class IconPartViewModel
    {
        public int Id { get; set; }
        public int PartId { get; set; }
        public string FileName { get; set; }
        public int Layer { get; set; }
    }
}
