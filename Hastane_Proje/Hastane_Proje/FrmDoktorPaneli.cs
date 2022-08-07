using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hastane_Proje
{
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select *From Tbl_Doktorlar", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //branşları comboboxa aktarma
            SqlCommand komut2 = new SqlCommand("Select BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                comboBox1.Items.Add(dr2[0]);

            }
            bgl.baglanti().Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Doktorlar (DoktorAd,DoktorSoyad,DoktorBrans,DoktorTC,DoktorSifre) values (@d1,@d2,@d3,@d4,@d5) ", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", textBox1.Text);
            komut.Parameters.AddWithValue("@d2", textBox2.Text);
            komut.Parameters.AddWithValue("@d3", comboBox1.Text);
            komut.Parameters.AddWithValue("@d4", maskedTextBox3.Text);
            komut.Parameters.AddWithValue("@d5", textBox3.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

      

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            maskedTextBox3.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            textBox3.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete from Tbl_Doktorlar where DoktorTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", maskedTextBox3.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt silindi ", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update from Tbl_Doktorlar set DoktorAd=@d1,,DoktorSoyad=@d2,DoktorBrans=@d3,DoktorTC=@d4,DoktorSifre=@d5 where DoktorTC=@d4 ",bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", textBox1.Text);
            komut.Parameters.AddWithValue("@d2", textBox2.Text);
            komut.Parameters.AddWithValue("@d3", comboBox1.Text);
            komut.Parameters.AddWithValue("@d4", maskedTextBox3.Text);
            komut.Parameters.AddWithValue("@d5", textBox3.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
    }
}
