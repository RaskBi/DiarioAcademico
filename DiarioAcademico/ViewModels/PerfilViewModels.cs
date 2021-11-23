﻿using DiarioAcademico.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiarioAcademico.ViewModels
{
    class PerfilViewModels
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        #region Attributes
        public string per_nickName;
        public string per_nombre;
        public string per_apellido;
        public int per_edad;
        public string per_institucion;

        public bool isRefreshing = false;
        public object listViewSource;
        #endregion

        #region Properties
        public string NameTxt
        {
            get { return this.name; }
            set { SetValue(ref this.name, value); }
        }

        public string AgeTxt
        {
            get { return this.age; }
            set { SetValue(ref this.age, value); }
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
            var person = new PersonModel
            {
                NameField = name,
                AgeField = int.Parse(age),
            };

            await firebaseHelper.AddPerson(person);

            this.IsRefreshing = true;

            await Task.Delay(1000);

            LoadData();

            this.IsRefreshing = false;
        }

        public async Task LoadData()
        {
            this.ListViewSource = await firebaseHelper.GetAllPersons();
        }

        #endregion

        #region .
        public ObservableCollection<PersonModel> IngredientsCollection = new ObservableCollection<PersonModel>();

        private async Task TestListViewBindingAsync()
        {
            var Ingredients = new List<PersonModel>();

            {
                Ingredients = await firebaseHelper.GetAllPersons();
            }
            foreach (var Ingredient in Ingredients)
            {
                IngredientsCollection.Add(Ingredient);
            }

        }
        #endregion
        #region Constructor
        public PersonViewModels()
        {
            LoadData();
            TestListViewBindingAsync();

        }
        #endregion
    }
}
