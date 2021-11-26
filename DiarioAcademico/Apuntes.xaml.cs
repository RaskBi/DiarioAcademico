using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiarioAcademico
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Apuntes : ContentPage
    {
        private const string Url = "http://186.101.162.131/agendax/tablas/";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<DiarioAcademico.Models.Apuntes> _postApu;
        private ObservableCollection<DiarioAcademico.Models.Materias> _Materias;
        private int _idPerfil;
        private int _idReg;
        private int cod = 0;

        private int idApu = 0;
        private string nombre = null;
        private string desc = null;

        public Apuntes(int idReg)
        {
            InitializeComponent();
            _idReg = idReg;
            getApu(idReg);
        }

        public async void getApu(int idReg)
        {
            //id perfil
            var contentID = await client.GetStringAsync(Url + "Perfil/get_email.php?idRegistro=" + idReg);
            var poste = JsonConvert.DeserializeObject<DiarioAcademico.Models.Perfil>(contentID);
            _idPerfil = poste.idPerfil;

            //materias con idPerfil
            var contentMat = await client.GetStringAsync(Url + "Materias/get_idMatWhereIDPer.php?idPerfil=" + _idPerfil);
            List<DiarioAcademico.Models.Materias> postMat = JsonConvert.DeserializeObject<List<DiarioAcademico.Models.Materias>>(contentMat);
            _Materias = new ObservableCollection<DiarioAcademico.Models.Materias>(postMat);

            pckMateria.ItemsSource = _Materias;
        }

        

        private void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var select = ((ListView)sender).SelectedItem as DiarioAcademico.Models.Apuntes;
            if (select == null)
                return;
            idApu = select.idApuntes;
            nombre = select.apu_nombre;
            desc = select.apu_descripcion;
        }

        private async void pckMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            var code = pckMateria.SelectedItem as DiarioAcademico.Models.Materias;
            cod = code.idMaterias;

            //datos apuntes
            var content = await client.GetStringAsync(Url + "Apuntes/get_perfilWhereIdMaretia.php?idMaterias=" + cod);
            List<DiarioAcademico.Models.Apuntes> post = JsonConvert.DeserializeObject<List<DiarioAcademico.Models.Apuntes>>(content);
            _postApu = new ObservableCollection<DiarioAcademico.Models.Apuntes>(post);

            MyListView.ItemsSource = _postApu;
        }

        private async void btnIngresar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ApuntesIngreso(cod));
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            if (idApu == 0 & nombre == null & desc == null)
            {
                await DisplayAlert("Error", "Selecione un Apunte", "Ok");
            }
            else
            {
                bool answer = await DisplayAlert("Eliminar", "Eliminar apunte: " + nombre + " " + desc, "Yes", "No");
                if (answer == true)
                {
                    try
                    {
                        if (idApu == 0)
                        {
                            await DisplayAlert("Error", "Error de ID", "Ok");
                        }
                        else
                        {
                            HttpClient client = new HttpClient();
                            await client.DeleteAsync(Url + "Apuntes/postApuntes.php?idApuntes=" + idApu);
                            await DisplayAlert("Eliminado", "Eliminado con exito", "Ok");

                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", ex.Message, "Ok");
                    }
                }
                else
                {

                }
            }
        }
    }
}