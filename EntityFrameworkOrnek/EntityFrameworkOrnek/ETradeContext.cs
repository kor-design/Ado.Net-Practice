using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkOrnek
{
    public class EtradeContext : DbContext //entittFramework kullanımı için DbContext ile ilişkilendiriyoruz

    {
        public DbSet<Product> Products { get; set; } //benim bir productım var ve bunu arayıp bul
        //contexti oluşturduk artık kodlarımızı yazabiliriz
    }
}
