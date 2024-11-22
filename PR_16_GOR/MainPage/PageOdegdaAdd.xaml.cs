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
    /// Логика взаимодействия для PageOdegdaAdd.xaml
    /// </summary>
    public partial class PageOdegdaAdd : Page
    {
        private Clothes _currenttovar = new Clothes();
        public PageOdegdaAdd(Clothes selectedtovar)
        {
            InitializeComponent();
            if (selectedtovar != null)
                _currenttovar = selectedtovar;

            DataContext = _currenttovar;
            ComboMaterial.ItemsSource = ALISAPOMOGIEntities1.GetContext().Material.ToList();
            ComboStrana.ItemsSource = ALISAPOMOGIEntities1.GetContext().Strana.ToList();
            ComboBrend.ItemsSource = ALISAPOMOGIEntities1.GetContext().Brend.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //проверка заполнения полей
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currenttovar.naimenov))
                errors.AppendLine("Укажите название товара");
            if (_currenttovar.kolichestvo <= 0)
                errors.AppendLine("Количество товара не может быть меньше или равно 0");
            if (_currenttovar.cena <= 0)
                errors.AppendLine("Цена не може быть меньше или равно 0");
            if (_currenttovar.Material == null)
                errors.AppendLine("Выберете материал");
            if (_currenttovar.Brend == null)
                errors.AppendLine("Выберете бренд");
            if (_currenttovar.Strana == null)
                errors.AppendLine("Выберете страну");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            //добавление
            if (_currenttovar.ClothesID == 0)
                ALISAPOMOGIEntities1.GetContext().Clothes.Add(_currenttovar);

            //обработаем вариант выпада/Записи данных
            try
            {
                ALISAPOMOGIEntities1.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ButStrelka_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.GoBack();
        }
    }
}
