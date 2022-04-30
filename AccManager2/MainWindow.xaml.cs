using System;
using System.Collections.Generic;
using System.IO;
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

namespace AccManager2
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int selected;
        public string path = "test.txt";


        public MainWindow()
        {
            InitializeComponent();
            RefreshCBOX();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtUsername.Text);
        }

        private void copyPassword(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtPassword.Text);
        }
        private void copyUsername(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtUsername.Text);
        }
        private void copyEmail(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtEmail.Text);
        }
        private void copyEmailPass(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtEmailPass.Text);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedItem = cbox.SelectedIndex;
            selected = selectedItem;
            if (selectedItem >= 0)
            {
                string[] lines = File.ReadAllLines(path);
                string[] words = lines[selectedItem].Split(':');
                txtUsername.Text = words[0];
                txtPassword.Text = words[1];
                txtNickname.Text = words[2];
                txtEmail.Text = words[3];
                txtEmailPass.Text = words[4];
                serverLbl.Content = words[5];
            }
            else
            {
                txtUsername.Text = "-";
                txtPassword.Text = "-";
                txtNickname.Text = "-";
                txtEmail.Text = "-";
                txtEmailPass.Text = "-";
                serverLbl.Content = "-";
            }
        }

        private void RefreshCBOX()
        {
            cbox.Items.Clear();
            //int lineCount = File.ReadLines(path).Count();
            int lineco = File.ReadAllLines(path).Count();

            if(lineco > 0)
            {
                for (int i = 0; i < lineco; i++)
                {
                    string[] lines = File.ReadAllLines(path);
                    string[] words = lines[i].Split(':');
                    cbox.Items.Add(words[2]);
                }
            }
            else
            {
                cbox.IsReadOnly = true;
            }
        }

        //crashuje bez kurwea powodu

        private void Refreshbtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshCBOX();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            string addNick, addLogin, addPass, addEmail, addEmailPass, addServer;

            addNick = addNicknametxt.Text;
            addServer = addServertxt.Text;
            addLogin = addLogintxt.Text;
            addPass = addPasswordtxt.Text;
            addEmail = addEmailtxt.Text;
            addEmailPass = addEmailPasstxt.Text;

            string newLine = (addLogin + ":" + addPass + ":" + addNick + ":" + addEmail + ":" + addEmailPass + ":" + addServer);

            using (StreamWriter file = File.AppendText(path))
            {
                file.WriteLine(newLine);
            }

            addNicknametxt.Text = "";
            addLogintxt.Text = "";
            addPasswordtxt.Text = "";
            addEmailtxt.Text = "";
            addEmailPasstxt.Text = "";
            addServertxt.Text = "";

            RefreshCBOX();
        }

        private void Opggbtn_Click(object sender, RoutedEventArgs e)
        {
            if(txtNickname.Text != "-")
            {
                string opggNick = txtNickname.Text.Replace(" ", "+");
                string opggPage = "https://" + serverLbl.Content + ".op.gg/summoner/userName=" + txtNickname.Text;
                System.Diagnostics.Process.Start(opggPage);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int lineco = File.ReadAllLines(path).Count();

            if(lineco > 0)
            {
                if(selected >= 0)
                {
                    var file = new List<string>(System.IO.File.ReadAllLines(path));
                    file.RemoveAt(selected);
                    File.WriteAllLines(path, file.ToArray());
                    RefreshCBOX();
                }
            }
        }
    }
}
