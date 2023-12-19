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

namespace KonyvtarAsztali
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Connection konyvek = new Connection();

		public MainWindow()
		{
			InitializeComponent();
			data.ItemsSource = konyvek.GetAll();
		}

		private void btn_delete_Click(object sender, RoutedEventArgs e)
		{
			Konyv selected = data.SelectedItem as Konyv;
			if(selected == null)
			{
				MessageBox.Show("Válasszon egy elemet");
			}
			else
			{
				if(MessageBoxResult.Yes == MessageBox.Show("Biztos törölni akarja ezt a könyvet?\n"+selected.Title,"Törlés",MessageBoxButton.YesNo,MessageBoxImage.Warning))
				{
					konyvek.Delete(selected);
				}
			}
			data.ItemsSource = konyvek.GetAll();
		}
	}
}
