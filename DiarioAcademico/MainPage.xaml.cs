using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using DiarioAcademico.Models;
using SQLite;
using System.IO;
using System.Net.Http;
using System.Collections.ObjectModel;
using System.Net;

namespace DiarioAcademico
{
    public partial class MainPage : ContentPage
    {
        private const string Url = "http://186.101.162.131/agendax/tablas/";
        private readonly HttpClient client = new HttpClient();
        public string WebAPIkey = "AIzaSyA8ily5_Cy1fSt-mVlpkeepZXzH8byGPHA";
        private int registroId= 0;
        public MainPage()
        {
            InitializeComponent();
        }

        async private void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(txtNEmail.Text, txtNcontra.Text);
                string gettoken = auth.FirebaseToken;

                //Ingreso a la base de datos
                
                WebClient cliente = new WebClient();
                var parametrosR = new System.Collections.Specialized.NameValueCollection();
                parametrosR.Add("reg_email", txtNEmail.Text);
                parametrosR.Add("reg_contrasenia", txtNcontra.Text);
                cliente.UploadValues(Url + "Registro/postRegistro.php", "POST", parametrosR);

                //get ID
                
                var content = await client.GetStringAsync(Url + "Registro/get_id.php?email=" + txtNEmail.Text + "&contra=" + txtNcontra.Text);
                var post = JsonConvert.DeserializeObject<DiarioAcademico.Models.Registro>(content).idRegistro;

                //post perfil

                var parametrosP = new System.Collections.Specialized.NameValueCollection();
                parametrosP.Add("per_nickname", txtNUsuario.Text);
                parametrosP.Add("per_nombre", txtNNombre.Text);
                parametrosP.Add("per_apellido", txtNApellido.Text);
                parametrosP.Add("per_edad", txtNEdad.Text);
                parametrosP.Add("per_institucion", txtNInstitucion.Text);
                parametrosP.Add("Registro_idRegistro", post.ToString());//
                cliente.UploadValues(Url + "Perfil/postPerfil.php", "POST", parametrosP);
                await DisplayAlert("Alert", "Guardado", "OK");

            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert Registro", ex.Message, "OK");
            }
        }
        private async void btnIrLog_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }
    }
}
