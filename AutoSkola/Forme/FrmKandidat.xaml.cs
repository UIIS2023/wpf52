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
    /// Interaction logic for FrmKandidat.xaml
    /// </summary>
    public partial class FrmKandidat : Window
    {
        Konekcija con = new Konekcija();
        SqlConnection connection = new SqlConnection();
        private bool update;
        private DataRowView row;

        public FrmKandidat()
        {
            InitializeComponent();
            txtIme.Focus();
            connection = con.KreirajKonekciju();
            fillComboBox();
        }

        public FrmKandidat (bool update, DataRowView row)
        {
            InitializeComponent();
            txtIme.Focus();
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
                string PopuniLekarski = @"select lekarskiID from dbo.tblLekarskiPregled";
                SqlDataAdapter daLekarski = new SqlDataAdapter(PopuniLekarski, connection);
                DataTable dtLekarski = new DataTable();
                daLekarski.Fill(dtLekarski);
                cbLekarski.ItemsSource = dtLekarski.DefaultView;
                daLekarski.Dispose();
                dtLekarski.Dispose();

                string PopuniPP = @"select prvaPomocID from dbo.tblPrvaPomoc";
                SqlDataAdapter daPP = new SqlDataAdapter(PopuniPP, connection);
                DataTable dtPP = new DataTable();
                daPP.Fill(dtPP);
                cbPP.ItemsSource = dtPP.DefaultView;
                daPP.Dispose();
                dtPP.Dispose();

                string PopuniTeorijski = @"select teorijskiID from dbo.tblTeorijski";
                SqlDataAdapter daTeo = new SqlDataAdapter(PopuniTeorijski, connection);
                DataTable dtTeo = new DataTable();
                daTeo.Fill(dtTeo);
                cbTeorijski.ItemsSource = dtTeo.DefaultView;
                daTeo.Dispose();
                dtTeo.Dispose();

                string PopuniPrakticni = @"select prakticniID from dbo.tblPrakticni";
                SqlDataAdapter daPrakticni = new SqlDataAdapter(PopuniPrakticni, connection);
                DataTable dtPrakticni = new DataTable();
                daPrakticni.Fill(dtPrakticni);
                cbPrakticni.ItemsSource = dtPrakticni.DefaultView;
                daPrakticni.Dispose();
                dtPrakticni.Dispose();
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
                cmd.Parameters.Add("@ime", SqlDbType.NVarChar).Value = txtIme.Text;
                cmd.Parameters.Add("@prezime", SqlDbType.NVarChar).Value = txtPrezime.Text;
                cmd.Parameters.Add("@kontakt", SqlDbType.VarChar).Value = txtKontakt.Text;
                cmd.Parameters.Add("@jmbg", SqlDbType.NVarChar).Value = txtJMBG.Text;
                cmd.Parameters.Add("@adresaStanovanja", SqlDbType.VarChar).Value = txtAdresa.Text;
                cmd.Parameters.Add("@datumRodjenja", SqlDbType.VarChar).Value = txtDatumRodjenja.Text;
                cmd.Parameters.Add("@lekarskiID", SqlDbType.Int).Value = cbLekarski.SelectedValue;
                cmd.Parameters.Add("@prvaPomocID", SqlDbType.Int).Value = cbPP.SelectedValue;
                cmd.Parameters.Add("@teorijskiID", SqlDbType.Int).Value = cbTeorijski.SelectedValue;
                cmd.Parameters.Add("@prakticniID", SqlDbType.Int).Value = cbPrakticni.SelectedValue;

                if (update)
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = row["ID"];
                    cmd.CommandText = @"update dbo.tblKandidat set ime = @ime, prezime = @prezime, kontakt = @kontakt, jmbg = @jmbg, adresaStanovanja = @adresaStanovanja, datumRodjenja = @datumRodjenja, lekarskiID = @lekarskiID, prvaPomocID = @prvaPomocID, teorijskiID = @teorijskiID, prakticniID = @prakticniID where kandidatID = @ID";
                    row = null;
                }
                else
                {
                    cmd.CommandText = @"insert into dbo.tblKandidat(ime, prezime, kontakt, jmbg, adresaStanovanja, datumRodjenja, lekarskiID, prvaPomocID, teorijskiID, prakticniID) values (@ime, @prezime, @kontakt, @jmbg, @adresaStanovanja, @datumRodjenja, @lekarskiID, @prvaPomocID, @teorijskiID, @prakticniID)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            /*catch
            {
                MessageBox.Show("Podaci su pogresno uneti.", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Exception: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
