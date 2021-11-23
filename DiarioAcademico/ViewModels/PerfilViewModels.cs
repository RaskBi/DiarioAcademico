﻿using DiarioAcademico.Models;
using DiarioAcademico.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiarioAcademico.ViewModels
{
    class PerfilViewModels : BaseViewModel
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        #region Attributes
        public string per_nickName;
        public string per_nombre;
        public string per_apellido;
        public string per_edad;
        public string per_institucion;

        public bool isRefreshing = false;
        public object listViewSource;
        #endregion

        #region Properties
        public string NicknameTxt
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
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public object ListViewSource
        {

            get { return this.listViewSource; }
            set
            {
                SetValue(ref this.listViewSource, value);
            }
        }
        #endregion

        #region Commands
        public ICommand InsertCommand
        {
            get
            {
                return new RelayCommand(InsertMethod);
            }
        }
        #endregion

        #region Methods
        private async void InsertMethod()
        {
            var perfil = new Perfil
            {
                per_nickName = per_nickName,
                per_nombre = per_nombre,
                per_apellido = per_apellido,
                per_edad = int.Parse(per_edad),
                per_institucion = per_institucion
            };

            await firebaseHelper.AddPerfil(perfil);

            this.IsRefreshing = true;

            await Task.Delay(1000);

            LoadData();

            this.IsRefreshing = false;
        }

        public async Task LoadData()
        {
            this.ListViewSource = await firebaseHelper.GetAllPerfil();
        }

        #endregion

        #region .
        public ObservableCollection<Perfil> IngredientsCollection = new ObservableCollection<Perfil>();

        private async Task TestListViewBindingAsync()
        {
            var Ingredients = new List<Perfil>();

            {
                Ingredients = await firebaseHelper.GetAllPerfil();
            }
            foreach (var Ingredient in Ingredients)
            {
                IngredientsCollection.Add(Ingredient);
            }

        }
        #endregion
        #region Constructor
        public PerfilViewModels()
        {
            LoadData();
            TestListViewBindingAsync();

        }
        #endregion
    }
}
