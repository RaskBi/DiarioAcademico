using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiarioAcademico
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class viewPerfil : ContentPage
    {
        private const string Url = "http://186.101.162.131/agendax/tablas/";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<DiarioAcademico.Models.Perfil> _postPerf;
        public int _idReg;
        public int _idPrefil;
        public viewPerfil(int idReg)
        {
            InitializeComponent();
            _idReg = idReg;
            getPerfil(idReg);

        }

        private async void getPerfil(int id)
        {

            var content = await client.GetStringAsync(Url + "Perfil/get_email.php?idRegistro=" + id);
            //var post = JsonConvert.DeserializeObject<DiarioAcademico.Models.Registro>(content).idRegistro;
            var post = JsonConvert.DeserializeObject<DiarioAcademico.Models.Perfil>(content);
            _idPrefil = post.idPerfil;
            string nickname = post.per_nickname;
            string nombre = post.per_nombre;
            string apellido = post.per_apellido;
            int edad = post.per_edad;
            string institucion = post.per_institucion;
            txtUsuario.Text = nickname;
            txtNombre.Text = nombre;
            txtApellido.Text = apellido;
            txtEdad.Text = edad.ToString();
            txtInstitucion.Text = institucion;
            //_postPerf = new ObservableCollection<DiarioAcademico.Models.Perfil>(post);
            //string nickname = ((DiarioAcademico.Models.Perfil)_postPerf[0]).per_nickname;

            //MyListView.ItemsSource = _postPerf;
        }

        private void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void btnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                WebClient cliente = new WebClient();
                var parametrosR = new System.Collections.Specialized.NameValueCollection();
                cliente.UploadValues(Url + "Perfil/postPerfil.php?per_nickname=" + txtUsuario.Text + "&per_edad=" + txtEdad.Text + "&per_institucion=" + txtInstitucion.Text + "&idPerfil=" + _idPrefil, "PUT", parametrosR);
                DisplayAlert("Alert", "Guardado", "OK");
                getPerfil(_idReg);
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", ex.Message, "OK");
            }
            
        }
    }
}