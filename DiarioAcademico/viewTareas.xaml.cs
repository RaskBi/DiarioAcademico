using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
    public partial class viewTareas : ContentPage
    {
        private const string Url = "http://186.101.162.131/agendax/tablas/";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<DiarioAcademico.Models.Tareas> _postTar;
        private ObservableCollection<DiarioAcademico.Models.Materias> _Materias;
        private int _idPerfil;
        private int _idReg;
        private int cod = 0;
        public viewTareas(int idReg)
        {
            InitializeComponent();
            _idReg = idReg;
            getTar(idReg);
        }

        public async void getTar(int idReg)
        {
            //id perfil
            var contentID = await client.GetStringAsync(Url + "Perfil/get_email.php?idRegistro=" + idReg);
            var poste = JsonConvert.DeserializeObject<DiarioAcademico.Models.Perfil>(contentID);
            _idPerfil = poste.idPerfil;

            //materias con idPerfil
            var contentMat = await client.GetStringAsync(Url + "Materias/get_idMatWhereIDPer.php?idPerfil=" + _idPerfil);
            List<DiarioAcademico.Models.Materias> postMat = JsonConvert.DeserializeObject<List<DiarioAcademico.Models.Materias>>(contentMat);
            _Materias = new ObservableCollection<DiarioAcademico.Models.Materias>(postMat);

            pckTareas.ItemsSource = _Materias;
        }

        private void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }

        private async void pckTareas_SelectedIndexChanged(object sender, EventArgs e)
        {
            var code = pckTareas.SelectedItem as DiarioAcademico.Models.Materias;
            cod = code.idMaterias;

            //datos Tareas
            var content = await client.GetStringAsync(Url + "Tareas/get_tareaWhereIdMaretia.php?idMaterias=" + cod);

            var format = "dd/MM/yyyy"; // your datetime format
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };

            List<DiarioAcademico.Models.Tareas> post = JsonConvert.DeserializeObject<List<DiarioAcademico.Models.Tareas>>(content, dateTimeConverter);
            _postTar = new ObservableCollection<DiarioAcademico.Models.Tareas>(post);



            MyListView.ItemsSource = _postTar;
        }

        private async void btbGuardar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new viewTareasIngresar(cod));
        }
    }
}