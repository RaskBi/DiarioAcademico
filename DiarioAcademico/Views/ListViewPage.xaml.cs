using DiarioAcademico.Models;
using DiarioAcademico.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiarioAcademico.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage : ContentPage
    {
        public ListViewPage()
        {
            InitializeComponent();
            BindingContext = new PerfilViewModels();
        }
        public async void ListViewName_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new EditPerfilPage(e.SelectedItem as Perfil));
            //
        }

        #region Test

        void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _container = BindingContext as PerfilViewModels;
            ListViewName.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                ListViewName.ItemsSource = _container.IngredientsCollection;
            }
            else
            {
                ListViewName.ItemsSource = _container.IngredientsCollection.Where(i => i.per_nombre.Contains(e.NewTextValue));
            }

            ListViewName.EndRefresh();
        }
        #endregion
    }
}