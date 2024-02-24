using AutoSkola.Forme;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoSkola
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Konekcija con = new Konekcija();
        SqlConnection connection = new SqlConnection();
        private bool update;
        private DataRowView row;
        private string CurrentTable;

        public MainWindow()
        {
            InitializeComponent();
            connection = con.KreirajKonekciju();
            loadData(SelectKandidat);
        }
        
        private void loadData(string SelectString)
        {
            try
            {
                connection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(SelectString, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                if (dataGridCentralni != null)
                {
                    dataGridCentralni.ItemsSource = dataTable.DefaultView;
                }
                CurrentTable = SelectString;
                dataAdapter.Dispose();
                dataTable.Dispose();
            }
            /*catch (SqlException)
            {
                MessageBox.Show("Podaci nisu uspesno ucitani", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
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

        #region Select Queries
            private static string SelectKandidat = @"select kandidatID as ID, ime as Ime, prezime as Prezime, kontakt as Kontakt, jmbg as JMBG, adresaStanovanja as 'Adresa Stanovanja', datumRodjenja as 'Datum Rodjenja', dbo.tblLekarskiPregled.lekarskiID, dbo.tblPrvaPomoc.prvaPomocID, dbo.tblTeorijski.teorijskiID, dbo.tblPrakticni.prakticniID from dbo.tblKandidat
                    join tblTeorijski on dbo.tblKandidat.teorijskiID = tblTeorijski.teorijskiID
                    join tblPrakticni on dbo.tblKandidat.prakticniID = tblPrakticni.prakticniID
                    join tblLekarskiPregled on dbo.tblKandidat.lekarskiID = tblLekarskiPregled.lekarskiID
                    join tblPrvaPomoc on dbo.tblKandidat.prvaPomocID = tblPrvaPomoc.prvaPomocID";

            private static string SelectInstruktor = @"select instruktorID as ID, ime as Ime, prezime as Prezime, brLicence as 'Broj Licence', jmbg as JMBG, kontakt as Kontakt, adresaStanovanja as 'Adresa Stanovanja' from tblInstruktor";

            private static string SelectZaposleni = @"select zaposleniID as ID, ime as Ime, prezime as Prezime, brLicence as 'Broj Licence', jmbg as JMBG, kontakt as Kontakt, adresaStanovanja as 'Adresa Stanovanja' from tblZaposleni";

            private static string SelectPotvrda = @"select izdavanjePotvrdeID as ID, potvrdaTeorijski as 'Potvrda Teorijski', potvrdaLekarski as 'Potvrda Lekarski', potvrdaPP as 'Potvrda Prva Pomoc', potvrdaPrakticni as 'Potvrda Prakticni', dbo.tblKandidat.ime as Ime from tblPotvrda
                    join dbo.tblKandidat on tblPotvrda.kandidatID = dbo.tblKandidat.kandidatID";

        private static string SelectTeorijski = @"select teorijskiID as ID, polozenIspitT as 'Polozen Teorijski', brojBodova as 'Broj Bodova', brojPokusaja as 'Broj Pokusaja' from tblTeorijski";

        private static string SelectPrakticni = @"select prakticniID as ID, polozenIspitP as 'Polozen Prakticni', brojBodova as 'Broj Bodova', brojPokusaja as 'Broj Pokusaja' from tblPrakticni";    

        private static string SelectLekarskiPregled = @"select lekarskiID as ID, polozenOpstiPregled as 'Polozen Opsti Pregled' from tblLekarskiPregled";

        private static string SelectPrvaPomoc = @"select prvaPomocID as ID, zavrsenaPPomoc as 'Zavrsena Prva Pomoc', stepenPP as 'Stepen Prve Pomoci' from tblPrvaPomoc";       

        private static string SelectRacun = @"select racunID as ID, iznos as Iznos, isplacenRacun as 'Isplacen Racun', dbo.tblZaposleni.zaposleniID as 'ID Zaposleni', dbo.tblPotvrda.izdavanjePotvrdeID as 'ID Potvrda' from dbo.tblRacun
                    join tblZaposleni on dbo.tblRacun.zaposleniID = tblZaposleni.zaposleniID
                    join tblPotvrda on dbo.tblRacun.izdavanjePotvrdeID = tblPotvrda.izdavanjePotvrdeID";

        private static string SelectVoznja = @"select voznjaID as ID, brCasova as 'Broj Casova', dbo.tblKandidat.kandidatID as 'ID Kandidata', dbo.tblInstruktor.instruktorID as 'ID Instruktora' from tblVoznja
                    join dbo.tblKandidat on tblVoznja.kandidatID = dbo.tblKandidat.kandidatID
                    join tblInstruktor on tblVoznja.instruktorID = tblInstruktor.instruktorID";
        #endregion

        #region Select With Statements
        private static string SelectStatementKandidat = @"select * from dbo.tblKandidat where kandidatID=";
        private static string SelectStatementInstruktor = @"select * from tblInstruktor where instruktorID=";
        private static string SelectStatementZaposleni = @"select * from tblZaposleni where zaposleniID=";
        private static string SelectStatementPotvrda = @"select * from tblPotvrda where izdavanjePotvrdeID=";
        private static string SelectStatementTeorijski = @"select * from tblTeorijski where teorijskiID=";
        private static string SelectStatementPrakticni = @"select * from tblPrakticni where prakticniID=";
        private static string SelectStatementLekarskiPregled = @"select * from tblLekarskiPregled where lekarskiID=";
        private static string SelectStatementPrvaPomoc = @"select * from tblPrvaPomoc where prvaPomocID=";
        private static string SelectStatementRacun = @"select * from tblRacun where racunID=";
        private static string SelectStatementVoznja = @"select * from tblVoznja where voznjaID=";
        #endregion

        #region Delete Queries
        private static string DeleteKandidat = @"delete from dbo.tblKandidat where kandidatID = ";
        private static string DeleteInstruktor = @"delete from tblInstruktor where instruktorID = ";
        private static string DeleteZaposleni = @"delete from tblZaposleni where zaposleniID = ";
        private static string DeletePotvrda = @"delete from tblPotvrda where izdavanjePotvrdeID = ";
        private static string DeleteTeorijski = @"delete from tblTeorijski where teorijskiID = ";
        private static string DeletePrakticni = @"delete from tblPrakticni where prakticniID = ";
        private static string DeleteLekarskiPregled = @"delete from tblLekarskiPregled where lekarskiID = ";
        private static string DeletePrvaPomoc = @"delete from tblPrvaPomoc where prvaPomocID = ";
        private static string DeleteRacun = @"delete from tblRacun where racunID = ";
        private static string DeleteVoznja = @"delete from tblVoznja where voznjaID = ";
        #endregion

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window form;
            if (CurrentTable.Equals(SelectKandidat))
            {
                form = new FrmKandidat();
                form.ShowDialog();
                loadData(SelectKandidat);
            }
            else if (CurrentTable.Equals(SelectInstruktor))
            {
                form = new FrmInstruktor();
                form.ShowDialog();
                loadData(SelectInstruktor);
            }
            else if (CurrentTable.Equals(SelectZaposleni))
            {
                form = new FrmZaposleni();
                form.ShowDialog();
                loadData(SelectZaposleni);
            }
            else if (CurrentTable.Equals(SelectPotvrda))
            {
                form = new FrmPotvrda();
                form.ShowDialog();
                loadData(SelectPotvrda);
            }
            else if (CurrentTable.Equals(SelectTeorijski))
            {
                form = new FrmTeorijski();
                form.ShowDialog();
                loadData(SelectTeorijski);
            }
            else if (CurrentTable.Equals(SelectPrakticni))
            {
                form = new FrmPrakticni();
                form.ShowDialog();
                loadData(SelectPrakticni);
            }
            else if (CurrentTable.Equals(SelectLekarskiPregled))
            {
                form = new FrmLekarskiPregled();
                form.ShowDialog();
                loadData(SelectLekarskiPregled);
            }
            else if (CurrentTable.Equals(SelectPrvaPomoc))
            {
                form = new FrmPrvaPomoc();
                form.ShowDialog();
                loadData(SelectPrvaPomoc);
            }
            else if (CurrentTable.Equals(SelectRacun))
            {
                form = new FrmRacun();
                form.ShowDialog();
                loadData(SelectRacun);
            }
            else if (CurrentTable.Equals(SelectVoznja))
            {
                form = new FrmVoznja();
                form.ShowDialog();
                loadData(SelectVoznja);
            }
        }

        private void BtnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentTable.Equals(SelectKandidat))
            {
                FillForm(SelectStatementKandidat);
                loadData(SelectKandidat);
            }
            else if (CurrentTable.Equals(SelectInstruktor))
            {
                FillForm(SelectStatementInstruktor);
                loadData(SelectInstruktor);
            }
            else if (CurrentTable.Equals(SelectZaposleni))
            {
                FillForm(SelectStatementZaposleni);
                loadData(SelectZaposleni);
            }
            else if (CurrentTable.Equals(SelectPotvrda))
            {
                FillForm(SelectStatementPotvrda);
                loadData(SelectPotvrda);
            }
            else if (CurrentTable.Equals(SelectTeorijski))
            {
                FillForm(SelectStatementTeorijski);
                loadData(SelectTeorijski);
            }
            else if (CurrentTable.Equals(SelectPrakticni))
            {
                FillForm(SelectStatementPrakticni);
                loadData(SelectPrakticni);
            }
            else if (CurrentTable.Equals(SelectLekarskiPregled))
            {
                FillForm(SelectStatementLekarskiPregled);
                loadData(SelectLekarskiPregled);
            }
            else if (CurrentTable.Equals(SelectPrvaPomoc))
            {
                FillForm(SelectStatementPrvaPomoc);
                loadData(SelectPrvaPomoc);
            }
            else if (CurrentTable.Equals(SelectRacun))
            {
                FillForm(SelectStatementRacun);
                loadData(SelectRacun);
            }
            else if (CurrentTable.Equals(SelectVoznja))
            {
                FillForm(SelectStatementVoznja);
                loadData(SelectVoznja);
            }
        }

        private void BtnIzbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentTable.Equals(SelectKandidat))
            {
                DeleteData(DeleteKandidat);
                loadData(SelectKandidat);
            }
            else if (CurrentTable.Equals(SelectInstruktor))
            {
                DeleteData(DeleteInstruktor);
                loadData(SelectInstruktor);
            }
            else if (CurrentTable.Equals(SelectTeorijski))
            {
                DeleteData(DeleteTeorijski);
                loadData(SelectTeorijski);
            }
            else if (CurrentTable.Equals(SelectPrakticni))
            {
                DeleteData(DeletePrakticni);
                loadData(SelectPrakticni);
            }
            else if (CurrentTable.Equals(SelectLekarskiPregled))
            {
                DeleteData(DeleteLekarskiPregled);
                loadData(SelectLekarskiPregled);
            }
            else if (CurrentTable.Equals(SelectPrvaPomoc))
            {
                DeleteData(DeletePrvaPomoc);
                loadData(SelectPrvaPomoc);
            }
            else if (CurrentTable.Equals(SelectRacun))
            {
                DeleteData(DeleteRacun);
                loadData(SelectRacun);
            }
            else if (CurrentTable.Equals(SelectVoznja))
            {
                DeleteData(DeleteVoznja);
                loadData(SelectVoznja);
            }
            else if (CurrentTable.Equals(SelectPotvrda))
            {
                DeleteData(DeletePotvrda);
                loadData(SelectPotvrda);
            }
            else if (CurrentTable.Equals(SelectZaposleni))
            {
                DeleteData(DeleteZaposleni);
                loadData(SelectZaposleni);
            }
        }

        private void BtnKandidat_Click(object sender, RoutedEventArgs e)
        {
            loadData(SelectKandidat);
        }

        private void BtnInstruktor_Click(object sender, RoutedEventArgs e)
        {
            loadData(SelectInstruktor);
        }

        private void BtnPotvrda_Click(object sender, RoutedEventArgs e)
        {
            loadData(SelectPotvrda);
        }

        private void BtnZaposleni_Click(object sender, RoutedEventArgs e)
        {
            loadData(SelectZaposleni);
        }

        private void BtnVoznja_Click(object sender, RoutedEventArgs e)
        {
            loadData(SelectVoznja);
        }

        private void BtnPrakticni_Click(object sender, RoutedEventArgs e)
        {
            loadData(SelectPrakticni);
        }

        private void BtnTeorijski_Click(object sender, RoutedEventArgs e)
        {
            loadData(SelectTeorijski);
        }

        private void BtnLekarski_Click(object sender, RoutedEventArgs e)
        {
            loadData(SelectLekarskiPregled);
        }

        private void BtnPrvaPomoc_Click(object sender, RoutedEventArgs e)
        {
            loadData(SelectPrvaPomoc);
        }

        private void BtnRacun_Click(object sender, RoutedEventArgs e)
        {
            loadData(SelectRacun);
        }

        private void FillForm(string selectStatement)
        {
            try
            {
                connection.Open();
                update = true;
                row = (DataRowView)dataGridCentralni.SelectedItems[0];
                SqlCommand cmd = new SqlCommand { Connection = connection };
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = row["ID"];
                cmd.CommandText = selectStatement + "@ID";
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (CurrentTable.Equals(SelectKandidat))
                    {
                        FrmKandidat FormKandidat = new FrmKandidat(update, row);
                        FormKandidat.txtIme.Text = reader["ime"].ToString();
                        FormKandidat.txtPrezime.Text = reader["prezime"].ToString();
                        FormKandidat.txtKontakt.Text = reader["kontakt"].ToString();
                        FormKandidat.txtJMBG.Text = reader["jmbg"].ToString();
                        FormKandidat.txtAdresa.Text = reader["adresaStanovanja"].ToString();
                        FormKandidat.txtDatumRodjenja.Text = reader["datumRodjenja"].ToString();
                        FormKandidat.cbLekarski.SelectedValue = reader["lekarskiID"].ToString();
                        FormKandidat.cbPP.SelectedValue = reader["prvaPomocID"].ToString();
                        FormKandidat.cbTeorijski.SelectedValue = reader["teorijskiID"].ToString();
                        FormKandidat.cbPrakticni.SelectedValue = reader["prakticniID"].ToString();
                        FormKandidat.ShowDialog();
                    }
                    else if (CurrentTable.Equals(SelectInstruktor))
                    {
                        FrmInstruktor FormInstruktor = new FrmInstruktor(update, row);
                        FormInstruktor.txtIme.Text = reader["ime"].ToString();
                        FormInstruktor.txtPrezime.Text = reader["prezime"].ToString();
                        FormInstruktor.txtLicenca.Text = reader["brLicence"].ToString();
                        FormInstruktor.txtJMBG.Text = reader["jmbg"].ToString();
                        FormInstruktor.txtKontakt.Text = reader["kontakt"].ToString();
                        FormInstruktor.txtAdresa.Text = reader["adresaStanovanja"].ToString();
                        FormInstruktor.ShowDialog();
                    }
                    else if (CurrentTable.Equals(SelectZaposleni))
                    {
                        FrmZaposleni FormZaposleni = new FrmZaposleni(update, row);
                        FormZaposleni.txtIme.Text = reader["ime"].ToString();
                        FormZaposleni.txtPrezime.Text = reader["prezime"].ToString();
                        FormZaposleni.txtLicenca.Text = reader["brLicence"].ToString();
                        FormZaposleni.txtJMBG.Text = reader["jmbg"].ToString();
                        FormZaposleni.txtKontakt.Text = reader["kontakt"].ToString();
                        FormZaposleni.txtAdresa.Text = reader["adresaStanovanja"].ToString();
                        FormZaposleni.ShowDialog();
                    }
                    else if (CurrentTable.Equals(SelectPotvrda))
                    {
                        FrmPotvrda FormPotvrda = new FrmPotvrda(update, row);
                        FormPotvrda.cbKandidat.SelectedValue = reader["kandidatID"].ToString();
                        FormPotvrda.chbLekarski.IsChecked = (bool)reader["potvrdaLekarski"];
                        FormPotvrda.chbPP.IsChecked = (bool)reader["potvrdaPP"];
                        FormPotvrda.chbPrakticni.IsChecked = (bool)reader["potvrdaPrakticni"];
                        FormPotvrda.chbTeorijski.IsChecked = (bool)reader["potvrdaTeorijski"];
                        FormPotvrda.ShowDialog();
                    }
                    else if (CurrentTable.Equals(SelectTeorijski))
                    {
                        FrmTeorijski FormTeorijski = new FrmTeorijski(update, row);
                        FormTeorijski.chbIspit.IsChecked = (bool)reader["polozenIspitT"];
                        FormTeorijski.txtBodovi.Text = reader["brojBodova"].ToString();
                        FormTeorijski.txtPokusaji.Text = reader["brojPokusaja"].ToString();
                        FormTeorijski.ShowDialog();
                    }
                    else if (CurrentTable.Equals(SelectPrakticni))
                    {
                        FrmPrakticni FormPrakticni = new FrmPrakticni(update, row);
                        FormPrakticni.chbPolozio.IsChecked = (bool)reader["polozenIspitP"];
                        FormPrakticni.txtBodovi.Text = reader["brojBodova"].ToString();
                        FormPrakticni.txtPokusaji.Text = reader["brojPokusaja"].ToString();
                        FormPrakticni.ShowDialog();
                    }
                    else if (CurrentTable.Equals(SelectLekarskiPregled))
                    {
                        FrmLekarskiPregled FormLekarskiPregled = new FrmLekarskiPregled(update, row);
                        FormLekarskiPregled.chbPregled.IsChecked = (bool)reader["polozenOpstiPregled"];
                        FormLekarskiPregled.ShowDialog();
                    }
                    else if (CurrentTable.Equals(SelectPrvaPomoc))
                    {
                        FrmPrvaPomoc FormPrvaPomoc = new FrmPrvaPomoc(update, row);
                        FormPrvaPomoc.chbIspit.IsChecked = (bool)reader["zavrsenaPPomoc"];
                        FormPrvaPomoc.txtStepen.Text = reader["stepenPP"].ToString();
                        FormPrvaPomoc.ShowDialog();
                    }
                    else if (CurrentTable.Equals(SelectRacun))
                    {
                        FrmRacun FormRacun = new FrmRacun(update, row);
                        FormRacun.txtIznos.Text = reader["iznos"].ToString();
                        FormRacun.chbRacun.IsChecked = (bool)reader["isplacenRacun"];
                        FormRacun.cbZaposleni.SelectedValue = reader["zaposleniID"].ToString();
                        FormRacun.cbPotvrda.SelectedValue = reader["izdavanjePotvrdeID"].ToString();
                        FormRacun.ShowDialog();
                    }
                    else if (CurrentTable.Equals(SelectVoznja))
                    {
                        FrmVoznja FormVoznja = new FrmVoznja(update, row);
                        FormVoznja.cbKandidat.SelectedValue = reader["kandidatID"].ToString();
                        FormVoznja.cbInstruktor.SelectedValue = reader["instruktorID"].ToString();
                        FormVoznja.txtCasovi.Text = reader["brCasova"].ToString();
                        FormVoznja.ShowDialog();
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Nije izabran ni jedan red", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        private void DeleteData(string deleteQuery)
        {
            try
            {
                connection.Open();
                row = (DataRowView)dataGridCentralni.SelectedItems[0];
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da obrisete izabranu stavku?", "UPOZORENJE", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = connection
                    };
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = row["ID"];
                    cmd.CommandText = deleteQuery + "@ID";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Odabrani red ima povezane podatke sa drugim tabelama! Nije moguce brisanje!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Nije izabran ni jedan red.", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }
    }
}
