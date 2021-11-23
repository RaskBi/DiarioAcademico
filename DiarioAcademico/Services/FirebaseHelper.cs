using DiarioAcademico.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioAcademico.Services
{
    class FirebaseHelper
    {
        public async Task<List<Perfil>> GetAllPerfil()
        {

            return (await firebase
              .Child("Perfil")
              .OnceAsync<Perfil>()).Select(item => new Perfil
              {
                  per_nombre = item.Object.per_nombre,
                  perfilId = item.Object.perfilId,
                  per_apellido = item.Object.per_apellido,
                  per_nickName = item.Object.per_nickName,
                  per_edad = item.Object.per_edad,
                  per_institucion = item.Object.per_institucion
              }).ToList();
        }

       
        public async Task AddPerfil(Perfil _perfil)
        {
            await firebase
            .Child("Perfil")
            .PostAsync(new Perfil()
            {
                perfilId = Guid.NewGuid(),
                per_nombre = _perfil.per_nombre,
                per_apellido = _perfil.per_apellido,
                per_nickName = _perfil.per_nickName,
                per_edad = _perfil.per_edad,
                per_institucion = _perfil.per_institucion
            });
        }


        public async Task UpdatePerfil(Perfil _perfil)
        {
            var toUpdatePerfil = (await firebase
              .Child("Perfil")
              .OnceAsync<Perfil>()).Where(a => a.Object.perfilId == _perfil.perfilId).FirstOrDefault();

            await firebase
              .Child("Perfil")
              .Child(toUpdatePerfil.Key)
              .PutAsync(new Perfil() { perfilId = _perfil.perfilId, per_nombre = _perfil.per_nombre, per_apellido = _perfil.per_apellido,
                  per_nickName = _perfil.per_nickName, per_edad = _perfil.per_edad, per_institucion = _perfil.per_institucion});
        }


        public async Task DeletePerfil(Guid personId)
        {
            var toDeletePerfil = (await firebase
              .Child("Perfil")
              .OnceAsync<Perfil>()).Where(a => a.Object.perfilId == personId).FirstOrDefault();
            await firebase.Child("Perfil").Child(toDeletePerfil.Key).DeleteAsync();

        }

        FirebaseClient firebase;
        public FirebaseHelper()
        {
            firebase = new FirebaseClient("https://diarioacademico-default-rtdb.firebaseio.com/");
        }
    }
}
