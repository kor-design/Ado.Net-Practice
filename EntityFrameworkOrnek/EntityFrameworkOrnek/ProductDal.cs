using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkOrnek
{
    public class ProductDal
    {
        //ilk operasyonumuz bütün ürünleri çekmek üzerine olacak
        public List<Product> GetAll() //ben GetAll ı çağırdığımda ürünleri listelicek
        {
            using (EtradeContext context = new EtradeContext())
            {
                /*using şu; etradecontext nesnesi çok yer kaplıyor ve bu tip nesneleri using ile 
                kullanıldığı anda bellekten atıyoruz.Normalde sistein çöp toplayıcısı bunu yapıyor 
                ancak bunu programı sonuna kadar okuduktan sonra yapıyor usingle kullanıldığı anda 
                bellekten atıyoruz sistem rahatlıyor.Using içine almadanda yapabilirdik kısacası
                ama böyle yapmak daha profesyonel.
                */

                return context.Products.ToList();

                // uzun uzun yaptığımız o listeleme bu kadar kodla yazılıyor.
                /*
                 <connectionStrings>
		     <add name="ETradeContext" 
			 connectionString="server=
			 (localdb)\mssqllocaldb;initial catalog=Etrade;integrated security=true"
			 providerName="System.Data.SqlClient"/>
	        </connectionStrings>
                
            bu kısım bizim veri tabanımıza ulaşacağımız yer olacak onu <startup> ın tam üzerine
                bu şekilde yazabiliriz buraya da çözüm gezgininden App.config kısmına gelerek ulaşabiliriz.*/
            }
        }
        public List<Product> GetByName(string key)
        {
            using (EtradeContext context = new EtradeContext())
            {
                return context.Products.Where(p => p.Name.Contains(key)).ToList();
                //bunu direk veri tabanından sorgulamıs oluruz bu daha iyi performans sağlar
                //Form 1 de buttona da atama yaptık ama bu daha verimli olur.
            }
        }
        public List<Product> GetByUnitPrice(decimal price)
        {
            using (EtradeContext context = new EtradeContext())
            {
                return context.Products.Where(p => p.UnitPrice >= price).ToList();
                //veri tabanına direk sorguyu atarak fiyata göre filtreleme yapıyoruz.
                //daha önce dediğim gibi sorguyu direk veri tabanına yaptırıp onu çağırmak performans olarak daha fayda sağlar
            }
        }
        public List<Product> GetByUnitPrice(decimal min, decimal max)
        { //iki fiyat arasını getirticez bununla
            using (EtradeContext context = new EtradeContext())
            {
                return context.Products.Where(p => p.UnitPrice >= min && p.UnitPrice <= max).ToList();
                //veri tabanına direk sorguyu atarak fiyata göre filtreleme yapıyoruz.
                //daha önce dediğim gibi sorguyu direk veri tabanına yaptırıp onu çağırmak performans olarak daha fayda sağlar
            }
        }
        public void Add(Product product)
        {
            using (EtradeContext context = new EtradeContext())
            {
                //context.Products.Add(product); //gönderilen ürünü ekle bu basic yazımı alttaki iki satırla daha pro yapılır

                var entity = context.Entry(product); //contexte abone olmak yani gönderiğimiz productu veri tabanındakiyle eşliyor
                entity.State = EntityState.Added; //abone olunan producta ekleme yaparız

                context.SaveChanges(); //veri tabanına kaydet
            }
        }
        public void Update(Product product)
        {
            using (EtradeContext context = new EtradeContext())
            {
                var entity = context.Entry(product); //contexte abone olmak yani gönderiğimiz productu veri tabanındakiyle eşliyor
                entity.State = EntityState.Modified; //abone olunan product güncellenir
                context.SaveChanges();
            }
        }
        public void Delete(Product product)
        {
            using (EtradeContext context = new EtradeContext())
            {
                var entity = context.Entry(product); //contexte abone olmak yani gönderiğimiz productu veri tabanındakiyle eşliyor
                entity.State = EntityState.Deleted; //abone olunan product silinir
                context.SaveChanges();
            }
        }
        public Product GetById(int id) //bana bir ID ver ben sana onun Productunu vereyim tek ürün getirt o yüzden listeleme yapmayız
        {
            using (EtradeContext context = new EtradeContext())
            {
                return context.Products.FirstOrDefault(p => p.Id == id);
                //FirstOrDefault yerine SingleOrDefault kullanırsak birden fazla değer bulursa hata verir
                //tek ürün getireceği için
                //FirstOrDefault demek bu Idye uygun olan ilk Idyi getir kayıt yoksa Null getir
            }
        }

    }
}
