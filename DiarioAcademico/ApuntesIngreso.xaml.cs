using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiarioAcademico
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ApuntesIngreso : ContentPage
    {
        private const string Url = "http://186.101.162.131/agendax/tablas/";
        private int _cod;
        public ApuntesIngreso(int cod)
        {
            InitializeComponent();
            _cod = cod;
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                WebClient cliente = new WebClient();
                var parametros = new System.Collections.Specialized.NameValueCollection();
                parametros.Add("apu_nombre", txtNombre.Text);
                parametros.Add("apu_descripcion", txtLongDesc.Text);
                parametros.Add("Materias_idMaterias", _cod.ToString());
                cliente.UploadValues(Url + "Apuntes/postApuntes.php", "POST", parametros);
                txtNombre.Text = "";
                txtLongDesc.Text = "";
                await DisplayAlert("Guardado", "Apunte guardado", "OK");
            }
            catch(Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
            

        }
    }
}