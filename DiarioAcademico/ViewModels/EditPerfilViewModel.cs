using DiarioAcademico.Models;
using DiarioAcademico.Services;
using DiarioAcademico.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DiarioAcademico.ViewModels
{
    class EditPerfilViewModel : BaseViewModel
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        #region Attributes
        private Guid iD;
        public string per_nickName;
        public string per_nombre;
        public string per_apellido;
        public string per_edad;
        public string per_institucion;
        #endregion

        #region Properties
        public Guid IDTxt
        {
            get { return this.iD; }
            set { SetValue(ref this.iD, value); }
        }
        public string NickNameTxt
        {
            get { return this.per_nickName; }
            set { SetValue(ref this.per_nickName, value); }
        }
        public string NombreTxt
        {
            get { return this.per_nombre; }
            set { SetValue(ref this.per_nombre, value); }
        }

        public string ApellidoTxt
        {
            get { return this.per_apellido; }
            set { SetValue(ref this.per_apellido, value); }
        }
        public string EdadTxt
        {
            get { return this.per_edad; }
            set { SetValue(ref this.per_edad, value); }
        }
        public string InstitucionTxt
        {
            get { return this.per_institucion; }
            set { SetValue(ref this.per_institucion, value); }
        }

        #endregion

        #region Commands
        public ICommand UpdateCommand
        {
            get
            {
                return new RelayCommand(UpdateMethod);
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(DeleteMethod);
            }
        }
        #endregion

        #region Methods

        private async void UpdateMethod()
        {
            var person = new Perfil
            {
                perfilId = IDTxt,
                per_nickName = per_nickName,
                per_nombre = per_nombre,
                per_apellido = per_apellido,
                per_edad = int.Parse(per_edad),
                per_institucion = per_institucion
            };

            await firebaseHelper.UpdatePerfil(person);

            await App.Current.MainPage.Navigation.PushAsync(new ListViewPage());
        }
        private async void DeleteMethod()
        {
            await firebaseHelper.DeletePerfil(IDTxt);
            await App.Current.MainPage.Navigation.PushAsync(new ListViewPage());

        }
        #endregion

        #region Constructor
        public EditPerfilViewModel(Perfil _perfilModel)
        {
            IDTxt = _perfilModel.perfilId;
            NickNameTxt = _perfilModel.per_nickName;
            NombreTxt = _perfilModel.per_nombre;
            ApellidoTxt = _perfilModel.per_apellido;
            EdadTxt = _perfilModel.per_edad.ToString();
            InstitucionTxt = _perfilModel.per_institucion;
        }

        public EditPerfilViewModel()
        {

        }
        #endregion
    }
}
