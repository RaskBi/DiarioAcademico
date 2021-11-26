using DiarioAcademico.Models;
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
    public partial class viewMaterias : ContentPage
    {
        private const string Url = "http://186.101.162.131/agendax/tablas/";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<DiarioAcademico.Models.Materias> _postMat;
        private int _idPerfil;
        private int _idReg;
        public viewMaterias(int idReg)
        {
            InitializeComponent();
            _idReg = idReg;
            getMat();
            
        }

        public async void getMat()
        {
            var contentID = await client.GetStringAsync(Url + "Perfil/get_email.php?idRegistro=" + _idReg);
            //var post = JsonConvert.DeserializeObject<DiarioAcademico.Models.Registro>(content).idRegistro;
            var poste = JsonConvert.DeserializeObject<DiarioAcademico.Models.Perfil>(contentID);
            _idPerfil = poste.idPerfil;
            var content = await client.GetStringAsync(Url + "/Materias/get_idPerfilWhere.php?Perfil_idPerfil=" + _idPerfil);
            //var content = await client.GetStringAsync(Url + "Materias/postMaterias.php");

            if (content == "false") {

            }
            else
            {
                //var post = JsonConvert.DeserializeObject<DiarioAcademico.Models.Materias>(content);
                List<Materias> post = JsonConvert.DeserializeObject<List<Materias>>(content);
                _postMat = new ObservableCollection<Materias>(post);

                //string nombre = post.mat_nombre;
                //txtNombre.Text = nombre;
                MyListView.ItemsSource = _postMat;
            }
            
        }
        
        private void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var select = ((ListView)sender).SelectedItem as DiarioAcademico.Models.Materias;
            if (select == null)
                return;
            //id = select.codigo;
            
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            WebClient cliente = new WebClient();
            var parametrosR = new System.Collections.Specialized.NameValueCollection();
            parametrosR.Add("mat_nombre", txtMateria.Text);
            parametrosR.Add("Perfil_idPerfil", _idPerfil.ToString());
            cliente.UploadValues(Url + "Materias/postMaterias.php", "POST", parametrosR);
            DisplayAlert("Guardado", "Materia " + txtMateria.Text + " guardado", "OK");
            txtMateria.Text = "";
            getMat();

        }
    }
}