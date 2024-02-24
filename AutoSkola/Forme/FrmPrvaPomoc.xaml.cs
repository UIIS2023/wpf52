using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutoSkola.Forme
{
    /// <summary>
    /// Interaction logic for FrmPrvaPomoc.xaml
    /// </summary>
    public partial class FrmPrvaPomoc : Window
    {
        Konekcija con = new Konekcija();
        SqlConnection connection = new SqlConnection();
        private bool update;
        private DataRowView row;

        public FrmPrvaPomoc()
        {
            InitializeComponent();
            connection = con.KreirajKonekciju();
        }

        public FrmPrvaPomoc(bool update, DataRowView row)
        {
            InitializeComponent();
            connection = con.KreirajKonekciju();
            this.update = update;
            this.row = row;
        }

       

        private void BtnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand()
                {
                    Connection = connection
                };
                cmd.Parameters.Add("@zavrsenaPPomoc", SqlDbType.Bit).Value = Convert.ToInt32(chbIspit.IsChecked);
                cmd.Parameters.Add("@stepenPP", SqlDbType.Int).Value = txtStepen.Text;
                if (update)
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = row["ID"];
                    cmd.CommandText = @"update tblPrvaPomoc set zavrsenaPPomoc = @zavrsenaPPomoc, stepenPP = @stepenPP where prvaPomocID = @ID";
                    row = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblPrvaPomoc(zavrsenaPPomoc, stepenPP) values (@zavrsenaPPomoc, @stepenPP)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Pogresno uneti podaci!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        private void BtnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
