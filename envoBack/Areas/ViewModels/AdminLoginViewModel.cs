using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace envoBack.Areas.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required]
        [StringLength(maximumLength:30)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 30)]
        
        public string Password { get; set; }
    }
}
