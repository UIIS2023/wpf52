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
    /// Interaction logic for FrmRacun.xaml
    /// </summary>
    public partial class FrmRacun : Window
    {
        Konekcija con = new Konekcija();
        SqlConnection connection = new SqlConnection();
        private bool update;
        private DataRowView row;

        public FrmRacun()
        {
            InitializeComponent();
            connection = con.KreirajKonekciju();
            fillComboBox();

        }

        public FrmRacun (bool update, DataRowView row)
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
                string PopuniZaposleni = @"select zaposleniID from dbo.tblZaposleni";
                SqlDataAdapter daZap = new SqlDataAdapter(PopuniZaposleni, connection);
                DataTable dtZap = new DataTable();
                daZap.Fill(dtZap);
                cbZaposleni.ItemsSource = dtZap.DefaultView;
                daZap.Dispose();
                dtZap.Dispose();

                string PopuniPot = @"select izdavanjePotvrdeID from dbo.tblPotvrda";
                SqlDataAdapter daPot = new SqlDataAdapter(PopuniPot, connection);
                DataTable dtPot = new DataTable();
                daPot.Fill(dtPot);
                cbPotvrda.ItemsSource = dtPot.DefaultView;
                daPot.Dispose();
                dtPot.Dispose();
            }
            /*catch
            {
                MessageBox.Show("Greška pri ucitavanju", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Exception: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                cmd.Parameters.Add("@iznos", SqlDbType.Int).Value = txtIznos.Text;
                cmd.Parameters.Add("@isplacenRacun", SqlDbType.Bit).Value = Convert.ToInt32(chbRacun.IsChecked);
                cmd.Parameters.Add("@zaposleniID", SqlDbType.Int).Value = cbZaposleni.SelectedValue;
                cmd.Parameters.Add("@izdavanjePotvrdeID", SqlDbType.Int).Value = cbPotvrda.SelectedValue;
                if (update)
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = row["ID"];
                    cmd.CommandText = @"update tblRacun set iznos = @iznos, isplacenRacun = @isplacenRacun, zaposleniID = @zaposleniID, izdavanjePotvrdeID = @izdavanjePotvrdeID where racunID = @ID";
                    row = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblRacun(iznos, isplacenRacun, zaposleniID, izdavanjePotvrdeID) values (@iznos, @isplacenRacun, @zaposleniID, @izdavanjePotvrdeID)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Pogresan unos podataka!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
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
