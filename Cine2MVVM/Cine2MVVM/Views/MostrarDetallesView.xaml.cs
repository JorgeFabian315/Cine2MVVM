using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cine2MVVM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MostrarDetallesView : ContentPage
    {
        public MostrarDetallesView()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await MenuEliminar.TranslateTo(0,0,250,Easing.SinIn);
        }

        private async void btnNo_Clicked(object sender, EventArgs e)
        {
            await MenuEliminar.TranslateTo(0, 150, 250, Easing.SinIn);
        }

        private void regresar_Tapped(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }
    }
}