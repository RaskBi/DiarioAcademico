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
    public partial class EditPerfilPage : ContentPage
    {
        public EditPerfilPage()
        {
            InitializeComponent();
            BindingContext = new EditPerfilViewModel();
        }

        public EditPerfilPage(Perfil _perfilModel)
        {
            InitializeComponent();
            BindingContext = new EditPerfilViewModel(_perfilModel);
        }
    }
}