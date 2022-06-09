using Cine2MVVM.Models;
using Cine2MVVM.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Cine2MVVM.ViewModels
{
    public class CineVM : INotifyPropertyChanged
    {
        public ObservableCollection<Pelicula> Cartelera { get; set; } = new ObservableCollection<Pelicula>();
        public Pelicula Pelicula { get; set; }
        public string Error { get; set; }
        //PAGINAS
        AgregarView agregarView;
        MostrarDetallesView detallesView;
        EditarView editarView;
        public ICommand AgregarCommand { get; set; }
        public ICommand CambiarVistaCommand { get; set; }
        public ICommand MostrarDetallesCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }

        public CineVM()
        {
            EditarCommand = new Command<Pelicula>(Editar);
            GuardarCommand = new Command(Guardar);
            MostrarDetallesCommand = new Command<Pelicula>(MostrarDetalles);
            AgregarCommand = new Command(Agregar);
            EliminarCommand = new Command<Pelicula>(Eliminar);
            CambiarVistaCommand = new Command<string>(CambiarVista);
            Desearilizar();
        }
        int indiceoriginal;
        private void Editar(Pelicula original)
        {
            indiceoriginal = Cartelera.IndexOf(original);
            Pelicula = new Pelicula()
            {
                Titulo = original.Titulo,
                Portada = original.Portada,
                Director = original.Director,
                Descripcion = original.Descripcion,
                Año = original.Año,
                Puntuacion = original.Puntuacion
            };
            editarView = new EditarView()
            {
                BindingContext = this
            };
            Application.Current.MainPage.Navigation.PushAsync(editarView);
        }

        private void Guardar()
        {
            Cartelera[indiceoriginal] = Pelicula;
            Serializar();
            Application.Current.MainPage.Navigation.PopToRootAsync();
       
        }
        
       

        private void Eliminar(Pelicula pelicula)
        {
            if (pelicula != null)
            {
                Cartelera.Remove(pelicula);
                Serializar();
                Application.Current.MainPage.Navigation.PopToRootAsync();

            }
        }
        private void MostrarDetalles(Pelicula a)
        {
            detallesView = new MostrarDetallesView() { BindingContext = this};
            this.Pelicula = a;
            Actualizar();
            Application.Current.MainPage.Navigation.PushAsync(detallesView);
        }

        private void CambiarVista(string vista)
        {
            if(vista == "Agregar")
            {
                Pelicula = new Pelicula();
                agregarView = new AgregarView() { BindingContext = this };
                Application.Current.MainPage.Navigation.PushAsync(agregarView);
            }
          
        }

        private void Agregar(object obj)
        {
            if (Pelicula != null)
            {
                Error = "";
                if (string.IsNullOrWhiteSpace(Pelicula.Titulo))
                {
                    Error = "El titulo no puede ser vacio";
                }
                if (string.IsNullOrWhiteSpace(Pelicula.Descripcion))
                {
                    Error = "la descripcion no puede ser vacio";
                }
                if (string.IsNullOrWhiteSpace(Error))
                {
                    Cartelera.Add(Pelicula);
                    Serializar();
                    Application.Current.MainPage.Navigation.PopToRootAsync();

                }
                Actualizar();
            }
        }
        public void Serializar()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "peliculas.json";
            File.WriteAllText(file, JsonConvert.SerializeObject(Cartelera));
        }
        public void Desearilizar()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "peliculas.json";

            if (File.Exists(file))
            {
                Cartelera = JsonConvert.DeserializeObject<ObservableCollection<Pelicula>>(File.ReadAllText(file));
            }
        }
        public void Actualizar(string act = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(act));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
