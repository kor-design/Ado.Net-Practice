using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkOrnek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ProductDal _productDal = new ProductDal();
        public void LoadProducts()
        { //veri tabanını gösterme işlemini çok yerde kullanacagımız için metot haline getirdim
            dgwProducts.DataSource = _productDal.GetAll();
        }
        public void SearchProducts(string key)
        //arama kutusuna girilen ifadeleri anında karsılastırıp karsılıgını çıkartır adım adım
        //a yazarsın içinde a geçen sonucu direk getirir gibi düsünebilirsiniz
        { //amaç aranan ifadeleri göstermek ürünün tamamı getall ile geliyor sonra where ile filtre belirliyoruz

            /* dgwProducts.DataSource = _productDal.GetAll().Where(p => p.Name.Contains(key)).ToList();
             tolist ile listeleriz ayrıca bunu sonuctan listeler yanı büyük küçük duyarlıdır 
             büyük küçük duyarlılıgı kaldırmak için bunu kaldırmak için  Where(p => p.Name.ToLower().Contains(key.ToLower())) yaparız
             bunu direkveri tabanına da sorgulatabiliriz onuda yazdım ProductDal dan bakabilirsiniz onun performansı büyük verilerde daha iyi olacaktır.
           */
            var result = _productDal.GetByName(key); //ProductDal daki sorgumuzu buraya çağırdık yukarıda bahsettiğim daha yüksek performans için böyle çalışıyoruz
            dgwProducts.DataSource = result; //bu şekilde veri tabanından çekersek büyük küçük harf duyarlı degildir
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productDal.Add(new Product
            {
                Name = tbxName.Text,
                UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                StockAmount = Convert.ToInt32(tbxStockAmount.Text)
            });
            LoadProducts();
            MessageBox.Show("Added!!");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _productDal.Update(new Product
            {
                Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value),
                Name = tbxNameUpdate.Text,
                UnitPrice = Convert.ToDecimal(tbxUnitPriceUpdate.Text),
                StockAmount = Convert.ToInt32(tbxStockAmountUpdate.Text)
            });
            LoadProducts();
            MessageBox.Show("Updated!!");
        }

        private void dgwProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbxNameUpdate.Text = dgwProducts.CurrentRow.Cells[1].Value.ToString();
            //DataGridView da tıklanan satırın name hücresindeki değerini seçer yazdırır
            tbxUnitPriceUpdate.Text = dgwProducts.CurrentRow.Cells[2].Value.ToString();
            //DataGridView da tıklanan satırın UnitPrice hücresindeki değerini seçer yazdırır
            tbxStockAmountUpdate.Text = dgwProducts.CurrentRow.Cells[3].Value.ToString();
            //DataGridView da tıklanan satırın StockAmount hücresindeki değerini seçer yazdırır
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            _productDal.Delete(new Product
            {
                Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value)
            });
            LoadProducts();
            MessageBox.Show("Deleted!!");
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            SearchProducts(tbxSearch.Text);
        }

    }
}
