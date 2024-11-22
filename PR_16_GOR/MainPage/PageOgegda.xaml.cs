using PR_16_GOR.ApplicationData;
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

namespace PR_16_GOR.MainPage
{
    /// <summary>
    /// Логика взаимодействия для PageOgegda.xaml
    /// </summary>
    public partial class PageOgegda : Page
    {
        public PageOgegda()
        {
            InitializeComponent();
            DtGridTovar.ItemsSource = ALISAPOMOGIEntities1.GetContext().Clothes.ToList();
        }


        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var tovarForRemiving = DtGridTovar.SelectedItems.Cast<Clothes>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить текущее {tovarForRemiving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ALISAPOMOGIEntities1.GetContext().Clothes.RemoveRange(tovarForRemiving);
                    ALISAPOMOGIEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    DtGridTovar.ItemsSource = ALISAPOMOGIEntities1.GetContext().Clothes.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new PageOdegdaAdd((sender as Button).DataContext as Clothes));
        }


        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new PageOdegdaAdd(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ALISAPOMOGIEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            DtGridTovar.ItemsSource = ALISAPOMOGIEntities1.GetContext().Clothes.ToList();
        }
    }
}

    
