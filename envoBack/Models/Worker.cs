using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace envoBack.Models
{
    public class Worker:BaseEntity
    {
        public string FullName { get; set; }
        public string Profession { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
