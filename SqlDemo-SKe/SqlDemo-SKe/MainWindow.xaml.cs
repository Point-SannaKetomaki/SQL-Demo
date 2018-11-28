using System;
using System.Collections.Generic;
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

namespace SqlDemo_SKe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //www.connectionstrings.com
            //sql Server / Trusted Connection

            string connStr = @"Server=SKE-DESKTOP-D91\SQLEXSANNAKE;Database=northwind;Trusted_Connection=True;";
            SqlConnection conn = new SqlConnection(connStr);  //Connection-objektille parametriksi se DB, mihin halutaan olla yhteydessä
            conn.Open(); //avataan tietokantayhteys

            //SQL-komentojen ajaminen, toteutetaan DataReader
            string sql = "SELECT * FROM Customers WHERE Country = 'Finland'";
            SqlCommand cmd = new SqlCommand(sql, conn); //parametriksi sql-komento + mihin kysely kohdistuu
            SqlDataReader reader = cmd.ExecuteReader();

            //while => luetaan rivi kerrallaan ja toteutetaan niin kauan kuin rivejä riittää
            while (reader.Read())
            {
                string companyName = reader.GetString(1); //GetString => yhden datarivin i:s sarake, tässä sarake 2, indeksi 1
                string contactName = reader.GetString(2);
                MessageBox.Show("Löytyi rivi: " + companyName + " " + contactName);
            }

            //vapautetaan resurssit (Tiedostot, verkkoyhteydet, tietokantaoliot ja käyttöliittymäkomponentit)
            reader.Close();
            cmd.Dispose();
            conn.Dispose();

        }
    }
}
