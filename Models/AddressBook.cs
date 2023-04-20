using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Address_Book.Models
{
    public class AddressBook
    {
        public int Id { get; set; }
        [MaxLength(170), Required]
        public string Name { get; set; }
        [MaxLength(17)]
        public string OfficeNumber { get; set; }
        [MaxLength(17), Required]
        public string MobileNumber { get; set; }
        [MaxLength(70)]
        public string Email { get; set; }
        [MaxLength(70)]
        public string Hometown { get; set; }


        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }
        [MaxLength(20)]
        public string FileType { get; set; }
        [MaxLength(225)]
        public string FilePath { get; set; }
        [MaxLength(225), Display(Name = "File Name")]
        public string FileName { get; set; }

        [NotMapped]
        public virtual HttpPostedFileBase file { get; set; }
    }
}