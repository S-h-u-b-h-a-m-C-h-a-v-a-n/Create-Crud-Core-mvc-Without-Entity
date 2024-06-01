using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CORE_WITHOUTENTTY.Models
{
    public class BookViewModel
    {
        [Key]
        public int BookID { get; set; }

        [Required]
        public String Title { get; set; }

        [Required]
        public String Author { get; set; }

        [Range(1,int.MaxValue,ErrorMessage ="Should be greater then or equal to 1")]
        public int Price { get; set; }
    }
}
