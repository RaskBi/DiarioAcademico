using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiarioAcademico
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : MasterDetailPage
    {
        public Menu(int idReg)
        {
            InitializeComponent();
            this.Master = new Master(idReg);
            this.Detail = new NavigationPage(new Detail());
            App.MAsterDet = this;

        }
    }
}