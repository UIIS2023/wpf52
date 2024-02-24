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
    /// Interaction logic for FrmVoznja.xaml
    /// </summary>
    public partial class FrmVoznja : Window
    {
        Konekcija con = new Konekcija();
        SqlConnection connection = new SqlConnection();
        private bool update;
        private DataRowView row;

        public FrmVoznja()
        {
            InitializeComponent();
            connection = con.KreirajKonekciju();
            fillComboBox();
        }

        public FrmVoznja(bool update, DataRowView row)
        {
            InitializeComponent();
            connection = con.KreirajKonekciju();
            fillComboBox();
            this.update = update;
            this.row = row;
        }

        private void fillComboBox()
        {
            try
            {
                connection.Open();
                string PopuniKandidat = @"select kandidatID from dbo.tblKandidat";
                SqlDataAdapter daKandidat = new SqlDataAdapter(PopuniKandidat, connection);
                DataTable dtKandidat = new DataTable();
                daKandidat.Fill(dtKandidat);
                cbKandidat.ItemsSource = dtKandidat.DefaultView;
                daKandidat.Dispose();
                dtKandidat.Dispose();

                string PopuniInstruktor = @"select instruktorID from dbo.tblInstruktor";
                SqlDataAdapter daInstruktor = new SqlDataAdapter(PopuniInstruktor, connection);
                DataTable dtInstruktor = new DataTable();
                daInstruktor.Fill(dtInstruktor);
                cbInstruktor.ItemsSource = dtInstruktor.DefaultView;
                daInstruktor.Dispose();
                dtInstruktor.Dispose();
            }
            catch
            {
                MessageBox.Show("Greška pri ucitavanju", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
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
                cmd.Parameters.Add("@kandidatID", SqlDbType.Int).Value = cbKandidat.SelectedValue;
                cmd.Parameters.Add("@instruktorID", SqlDbType.Int).Value = cbInstruktor.SelectedValue;
                cmd.Parameters.Add("@brCasova", SqlDbType.Int).Value = txtCasovi.Text;
                if (update)
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = row["ID"];
                    cmd.CommandText = @"update tblVoznja set kandidatID = @kandidatID, instruktorID = @instruktorID, brCasova = @brCasova where voznjaID = @ID";
                    row = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblVoznja(kandidatID, instruktorID, brCasova) values (@kandidatID, @instruktorID, @brCasova)";
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
