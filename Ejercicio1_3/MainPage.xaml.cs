using Ejercicio1_3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ejercicio1_3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            llenarDatos();
        }

        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        { 
            if (validarDatos())
            {
                Personas pers = new Personas
                {
                    nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    edad = int.Parse(txtEdad.Text),
                    correo = txtCorreo.Text,
                    direccion = txtDireccion.Text,
                };
                await App.SQLiteDB.GuardarPersona(pers);
                LimpiarControles();
                llenarDatos();
                await DisplayAlert("Registro", "Guardado Exitosamente", "OK");
            }
            else
            {
                await DisplayAlert("Advertencia", "Campos Vacias", "OK");
            }
        }

        public async void llenarDatos()
        {
            var personaList = await App.SQLiteDB.GetPersonas();
            if (personaList != null)
            {
                listPersonas.ItemsSource = personaList;
            }
        }

        public bool validarDatos()
        {
            bool respuesta;

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtApellido.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEdad.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtCorreo.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                Personas persona = new Personas()
                {
                    id = Convert.ToInt32(txtId.Text),
                    nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    edad = Convert.ToInt32(txtEdad.Text),
                    correo = txtCorreo.Text,
                    direccion = txtDireccion.Text,
                };
                await App.SQLiteDB.GuardarPersona(persona);
                LimpiarControles();
                txtId.IsVisible = false;
                btnEliminar.IsVisible = false;
                btnCancelar.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnRegistrar.IsVisible = true;
                llenarDatos();
                await DisplayAlert("Registro", "Registro Actualizado Exitosamente", "OK");
            }
        }

        private async void listPersonas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Personas)e.SelectedItem;
            btnRegistrar.IsVisible = false;
            txtId.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;
            btnCancelar.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.id.ToString()))
            {
                var persona = await App.SQLiteDB.GetPersonasByIdAsync(obj.id);
                if (persona != null)
                {
                    txtId.Text = persona.id.ToString();
                    txtNombre.Text = persona.nombre;
                    txtApellido.Text = persona.Apellido;
                    txtEdad.Text = persona.edad.ToString();
                    txtCorreo.Text = persona.correo;
                    txtDireccion.Text = persona.direccion;
                }
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var persona = await App.SQLiteDB.GetPersonasByIdAsync(Convert.ToInt32(txtId.Text));

            if (persona != null)
            {
                await App.SQLiteDB.EliminarPersona(persona);
                LimpiarControles();
                llenarDatos();
                await DisplayAlert("Aviso", "Registro Eliminado Exitosamente", "Ok");
                txtId.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnRegistrar.IsVisible = true;
                btnEliminar.IsVisible = false;
                btnCancelar.IsVisible = false;
            }
        }

        public void LimpiarControles()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEdad.Text = "";
            txtCorreo.Text = "";
            txtDireccion.Text = "";

        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            btnEliminar.IsVisible = false;
            btnActualizar.IsVisible = false;
            btnCancelar.IsVisible = false;
            txtId.IsVisible = false;
            LimpiarControles();
            llenarDatos();
        }
    }
}
