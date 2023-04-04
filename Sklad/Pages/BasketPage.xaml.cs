using sklad.Classes;
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
using sklad.Pages;
using sklad.Classes;

namespace sklad.Pages
{
    /// <summary>
    /// Логика взаимодействия для BasketPage.xaml
    /// </summary>
    public partial class BasketPage : Page
    {
        
        private Basket _currentbasket = new Basket();
        public BasketPage(Medicament selectedMedicament)
        {
            InitializeComponent();
            if (selectedMedicament != null)
            {
                _currentbasket.IDmedicament = selectedMedicament.IDmedicament;
                _currentbasket.Sessoin = ClassSession.idSession + 1;
                
                _currentbasket.Kolvo = 1;

            }

            DataContext = _currentbasket;
            SkladEntities.GetContext().Basket.Add(_currentbasket);
            SkladEntities.GetContext().SaveChanges();
            ListOrder.ItemsSource = SkladEntities.GetContext().Basket.Where(x=>x.Sessoin == _currentbasket.Sessoin).ToList();
            CountItems.Text = ListOrder.Items.Count.ToString();

        } 
         private void Back_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new ViewingPage1());
        }
        private void DeleteMed_Click(object sender, RoutedEventArgs e)
        {
            var personForRemoving = ListOrder.SelectedItems.Cast<Basket>().ToList();
            var resMessage = MessageBox.Show("Удалить запись?", "Подтверждение",
             MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resMessage == MessageBoxResult.Yes)
            {
                try
                {
                    SkladEntities.GetContext().Basket.RemoveRange(personForRemoving);
                    SkladEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    ListOrder.ItemsSource = SkladEntities.GetContext().Basket.ToList();
                }
                catch (Exception ex)
                {
                    
                }
            }
        }

        private void AddPharm_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Itog_TextChanged(object sender, TextChangedEventArgs e)
        {
         


        }

        private void ListOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
