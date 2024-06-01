using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MVC_CORE_WITHOUTENTTY.Models
{
    public class MVC_CORE_WITHOUTENTTYContext : DbContext
    {
        public MVC_CORE_WITHOUTENTTYContext (DbContextOptions<MVC_CORE_WITHOUTENTTYContext> options)
            : base(options)
        {
        }

        public DbSet<MVC_CORE_WITHOUTENTTY.Models.BookViewModel> BookViewModel { get; set; }
    }
}
