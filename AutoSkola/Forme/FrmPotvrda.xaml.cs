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
    /// Interaction logic for FrmPotvrda.xaml
    /// </summary>
    public partial class FrmPotvrda : Window
    {
        Konekcija con = new Konekcija();
        SqlConnection connection = new SqlConnection();
        private bool update;
        private DataRowView row;

        public FrmPotvrda()
        {
            InitializeComponent();
            cbKandidat.Focus();
            connection = con.KreirajKonekciju();
            fillComboBox();
        }

        public FrmPotvrda (bool update, DataRowView row)
        {
            InitializeComponent();
            connection = con.KreirajKonekciju();
            cbKandidat.Focus();
            fillComboBox();
            this.update = update;
            this.row = row;
        }

        private void fillComboBox()
        {
            try
            {
                connection.Open();
                string PopuniKandidat = @"select kandidatID, ime from dbo.tblKandidat";
                SqlDataAdapter daKandidat = new SqlDataAdapter(PopuniKandidat, connection);
                DataTable dtKandidat = new DataTable();
                daKandidat.Fill(dtKandidat);
                cbKandidat.ItemsSource = dtKandidat.DefaultView;
                daKandidat.Dispose();
                dtKandidat.Dispose();
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
                cmd.Parameters.Add("@kandidat", SqlDbType.Int).Value = cbKandidat.SelectedValue;
                cmd.Parameters.Add("@potvrdaTeorijski", SqlDbType.Bit).Value = Convert.ToInt32(chbTeorijski.IsChecked);
                cmd.Parameters.Add("@potvrdaLekarski", SqlDbType.Bit).Value = Convert.ToInt32(chbLekarski.IsChecked);
                cmd.Parameters.Add("@potvrdaPP", SqlDbType.Bit).Value = Convert.ToInt32(chbPP.IsChecked);
                cmd.Parameters.Add("@potvrdaPrakticni", SqlDbType.Bit).Value = Convert.ToInt32(chbPrakticni.IsChecked);
                if (update)
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = row["ID"];
                    cmd.CommandText = @"update tblPotvrda set kandidatID = @kandidat, potvrdaTeorijski = @potvrdaTeorijski, potvrdaLekarski = @potvrdaLekarski, potvrdaPP = @potvrdaPP, potvrdaPrakticni = @potvrdaPrakticni where izdavanjePotvrdeID = @ID";
                    row = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblPotvrda(kandidatID, potvrdaTeorijski, potvrdaLekarski, potvrdaPP, potvrdaPrakticni) values (@kandidat, @potvrdaTeorijski, @potvrdaLekarski, @potvrdaPP, @potvrdaPrakticni)";
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
                {
                    connection.Close();
                }
            }
        }

        private void BtnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
