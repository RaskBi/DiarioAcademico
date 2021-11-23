using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
                await Navigation.PushAsync(new Menu());
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