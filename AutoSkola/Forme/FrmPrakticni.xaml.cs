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
    /// Interaction logic for FrmPrakticni.xaml
    /// </summary>
    public partial class FrmPrakticni : Window
    {
        Konekcija con = new Konekcija();
        SqlConnection connection = new SqlConnection();
        private bool update;
        private DataRowView row;

        public FrmPrakticni()
        {
            InitializeComponent();
            connection = con.KreirajKonekciju();
        }

        public FrmPrakticni(bool update, DataRowView row)
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
                cmd.Parameters.Add("@polozenIspitP", SqlDbType.Bit).Value = Convert.ToInt32(chbPolozio.IsChecked);
                cmd.Parameters.Add("@brojBodova", SqlDbType.Int).Value = txtBodovi.Text;
                cmd.Parameters.Add("@brojPokusaja", SqlDbType.Int).Value = txtPokusaji.Text;
                if (update)
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = row["ID"];
                    cmd.CommandText = @"update tblPrakticni set polozenIspitP = @polozenIspitP, brojBodova = @brojBodova, brojPokusaja = @brojPokusaja where prakticniID = @ID";

                    row = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblPrakticni(polozenIspitP, brojBodova, brojPokusaja) values (@polozenIspitP, @brojBodova, @brojPokusaja)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Pogresan unos podataka!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
