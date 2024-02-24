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
    /// Interaction logic for FrmLekarskiPregled.xaml
    /// </summary>
    public partial class FrmLekarskiPregled : Window
    {
        Konekcija con = new Konekcija();
        SqlConnection connection = new SqlConnection();
        private bool update;
        private DataRowView row;

        public FrmLekarskiPregled()
        {
            InitializeComponent();
            chbPregled.Focus();
            connection = con.KreirajKonekciju();
        }

        public FrmLekarskiPregled (bool update, DataRowView row)
        {
            InitializeComponent();
            connection = con.KreirajKonekciju();
            chbPregled.Focus();
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
                cmd.Parameters.Add(@"polozenOpstiPregled", SqlDbType.Bit).Value = Convert.ToInt32(chbPregled.IsChecked);
                if (update)
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = row["ID"];
                    cmd.CommandText = @"update tblLekarskiPregled set polozenOpstiPregled = @polozenOpstiPregled where lekarskiID = @ID";
                    row = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblLekarskiPregled(polozenOpstiPregled) values (@polozenOpstiPregled)";
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
