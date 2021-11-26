using Newtonsoft.Json;
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
    public partial class viewTareasIngresar : ContentPage
    {
        private const string Url = "http://186.101.162.131/agendax/tablas/";
        private int _cod;
        public viewTareasIngresar(int cod)
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

                string dateI = JsonConvert.SerializeObject(dtpFechaI.Date);
                string dateF = JsonConvert.SerializeObject(dtpFechaF.Date);

                await DisplayAlert("Alert", dateF + " medio " + dateI, "ok");

                parametros.Add("tar_nombre", txtnombre.Text);
                parametros.Add("tar_desc", txtLongDesc.Text);
                parametros.Add("tar_fecha_inicio", dateI);
                parametros.Add("tar_fecha_fin", dateF);
                parametros.Add("Materias_idMaterias", _cod.ToString());
                cliente.UploadValues(Url + "Tareas/postTareas.php", "POST", parametros);
                txtnombre.Text = "";
                txtLongDesc.Text = "";
                await DisplayAlert("Guardado", "Apunte guardado", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }
    }
}