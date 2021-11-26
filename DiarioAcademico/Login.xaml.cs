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
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private const string Url = "http://186.101.162.131/agendax/tablas/";
        private readonly HttpClient client = new HttpClient();
        public string WebAPIkey = "AIzaSyA8ily5_Cy1fSt-mVlpkeepZXzH8byGPHA";
        public Login()
        {
            InitializeComponent();
        }

        async void btnLogin_Clicked(object sender, EventArgs e)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(txtEmail.Text, txtPass.Text);
                var content = await auth.GetFreshAuthAsync();
                var serializedcontnet = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);
                var contenido = await client.GetStringAsync(Url + "Registro/get_idReg.php?email=" + txtEmail.Text);
                var post = JsonConvert.DeserializeObject<DiarioAcademico.Models.Registro>(contenido).idRegistro;
                await Navigation.PushAsync(new Menu(post));
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Usuario o contraseña invalido", "OK");
            }
        }

        async private void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}