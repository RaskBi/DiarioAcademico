using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiarioAcademico
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : ContentPage
    {
        private const string Url = "http://186.101.162.131/agendax/tablas/";
        private readonly HttpClient client = new HttpClient();
        public string WebAPIkey = "AIzaSyA8ily5_Cy1fSt-mVlpkeepZXzH8byGPHA";
        public int _idReg;
        public Master(int idReg)
        {
            InitializeComponent();
            _idReg = idReg;
            GetProfileInformationAndRefreshToken();
        }

     

        async private void GetProfileInformationAndRefreshToken()
        {

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            try
            {
                //This is the saved firebaseauthentication that was saved during the time of login
                var savedfirebaseauth = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                //Here we are Refreshing the token
                var RefreshedContent = await authProvider.RefreshAuthAsync(savedfirebaseauth);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(RefreshedContent));
                //Now lets grab user information
                MyUserName.Text = savedfirebaseauth.User.Email;
                if (_idReg == 0)
                {
                    var contenido = await client.GetStringAsync(Url + "Registro/get_idReg.php?email=" + savedfirebaseauth.User.Email);
                    var post = JsonConvert.DeserializeObject<DiarioAcademico.Models.Registro>(contenido).idRegistro;
                    _idReg = post;
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await App.Current.MainPage.DisplayAlert("Alert", "Oh no !  Token expired", "OK");
            }
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Remove("MyFirebaseRefreshToken");
            App.Current.MainPage = new NavigationPage(new Login());
        }

        async private void Perfil_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new viewPerfil(_idReg));
        }

        async private void Materias_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new viewMaterias(_idReg));
        }

        async private void Apuntes_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Apuntes(_idReg));
        }

        private async void Tareas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new viewTareas(_idReg));
        }

        private void Tareas_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}