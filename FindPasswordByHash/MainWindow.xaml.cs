using System;
using System.Collections.Generic;
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
using System.Security.Cryptography;
using System.Numerics;
using System.Data;
using System.Collections.ObjectModel;

namespace FindPasswordByHash
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool isStopped { get; set; } = false;
        public ObservableCollection<FindedPassword> passArray { get; set; }
        public delegate void WasAddedVal();
        public event WasAddedVal addToArrayPass;
        public MainWindow()
        {
            passArray = new ObservableCollection<FindedPassword>();

            addToArrayPass += writeCountToLabel;
            InitializeComponent();
        }

        public void writeCountToLabel()
        {
            valCount.Content = passArray.Count.ToString();
        }

        public async void FindPassword(string hash, TypeHashAlgoritm typeHashAlgoritm)
        {
            await Task.CompletedTask;
            switch (typeHashAlgoritm)
            {
                case TypeHashAlgoritm.MD5: Find_Password(hash, TypeHashAlgoritm.MD5);  break;
                case TypeHashAlgoritm.SHA256: Find_Password(hash, TypeHashAlgoritm.SHA256); break;
                case TypeHashAlgoritm.SHA512: Find_Password(hash, TypeHashAlgoritm.SHA512); break;
                default: break;
            }
        }

        public async void Find_Password(string hash, TypeHashAlgoritm type)
        {
            isStopped = false;
            byte[] arr = Encoding.UTF8.GetBytes(hash);
            SearcherPass searcherPass = new SearcherPass();
            Hasher hasher = new Hasher(type);

            while (!isStopped)
            {
                MD5 mD5 = new MD5CryptoServiceProvider();
                byte[] arrFromText = searcherPass.GetByteArray();

                byte [] computeHash = hasher.ComputeHash(arrFromText);
                StringBuilder result = new StringBuilder();
                foreach (var b in computeHash)
                {
                    result.Append( b.ToString("x2"));
                }

                if (hash == result.ToString())
                {
                    string tempPas = searcherPass.GetString();
                    this.passArray.Add(new FindedPassword(tempPas));
                    this.addToArrayPass();
                    await Task.CompletedTask;
                    break;
                }
                searcherPass.IncrementByteArray();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isStopped = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(hashTextVal.Text))
            {
                MessageBox.Show("Вы не ввели хеш");
                return;
            }
            int selectedVal = selectionAlgoritm.SelectedIndex;
            string hashText = hashTextVal.Text;

            switch (selectedVal)
            {
                case 0: FindPassword(hashText, TypeHashAlgoritm.SHA256); break;
                case 1: FindPassword(hashText, TypeHashAlgoritm.SHA512); break;
                case 2: FindPassword(hashText, TypeHashAlgoritm.MD5); break;
                default: break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //tableResultPassword.DataContext = passArray;
            tableResultPassword.ItemsSource = passArray;
        }
    }
}
