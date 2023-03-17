using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AccDatos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DummyManager manager;
        public MainWindow()
        {
            InitializeComponent();
            manager = new DummyManager();
            MediaPlayer mediaPlayerboton = new MediaPlayer();

            DataContext = manager;
            //Las funciones para los void

            mediaPlayerboton.Open(new Uri("D:/2º DAM/source/repos/KaIr0sG/ProyectoDI_AD - copia/AccDatos/stranger.mp3", UriKind.Relative));
            mediaPlayerboton.Play();

            OcultarTodo();
            MostrarLogIn();
            // Animacion(bt_usuarioAnonimo); // Si lo pongo aqui se hace siempre
        }
        //Aqui ocultamos todo 
        void OcultarTodo()
        {
            sp_FormRegistro.Visibility = Visibility.Collapsed;
            sp_LOGIN.Visibility = Visibility.Collapsed;
            sp_PreparacionSimulacion.Visibility = Visibility.Collapsed;
            sp_Atras.Visibility = Visibility.Collapsed;
        }
        //Aqui mostramos el login 
        void MostrarLogIn()
        {
            sp_LOGIN.Visibility = Visibility.Visible;
            // sp_Inicio.Visibility = Visibility.Collapsed;

        }
        // Nos enseña la simulacion y nos oculta el login y el registro
        private void MostrarPreparacionSimulacion()
        {
            sp_PreparacionSimulacion.Visibility = Visibility.Visible;
            sp_FormRegistro.Visibility = Visibility.Collapsed;
            sp_LOGIN.Visibility = Visibility.Collapsed;
            // sp_ivitado.Visibility = Visibility.Collapsed;
            sp_Atras.Visibility = Visibility.Visible;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    manager.CambiarApellidoActor(int.Parse(tb_idActor.Text), tb_nuevoApellido.Text);
        //}

        private void Button_Click_Datos(object sender, RoutedEventArgs e)
        {
            manager.VerDatosUsuario(1);
        }
        // Nos confirma si el usuario se ha dado de alta o no
        private void Button_Click_Alta(object sender, RoutedEventArgs e)
        {
            if (tb_pass.Password == tb_passConfirm.Password)
            {
                DummyManager manager = new DummyManager();
                manager.AltaUsuario(tb_nombre.Text,
                                     tb_apellido.Text,
                                     tb_mail.Text,
                                     tb_usuario.Text,
                                     tb_pass.Password
                                     );
            }
            else
            {
                MessageBox.Show("La contraseña no coincide con el campo de confrimación. Vuelva a intentarlo");
            }
        }

        private void bt_Juego_Click(object sender, RoutedEventArgs e)
        {
            sp_Inicio.Visibility = Visibility.Collapsed;
            sp_PreparacionSimulacion.Visibility = Visibility.Visible;
            sp_Atras.Visibility = Visibility.Visible;
            Aparecer(sp_PreparacionSimulacion);
            Aparecer(sp_Atras);
            Aparecer_Boton(btn_atras);
        }

        private void bt_Creditos_Click(object sender, RoutedEventArgs e)
        {
            sp_Inicio.Visibility = Visibility.Collapsed;
            sp_Creditos.Visibility = Visibility.Visible;
            sp_Atras.Visibility = Visibility.Visible;
            Aparecer(sp_Inicio);
            Aparecer(sp_Atras);
            Aparecer_Boton(btn_atras);
        }

        private void bt_Ajustes_Click(object sender, RoutedEventArgs e)
        {
            sp_Inicio.Visibility = Visibility.Collapsed;
            //sp_Atras.Visibility = Visibility.Visible;
            sp_Ajustes.Visibility = Visibility.Visible;
            sp_Atras.Visibility = Visibility.Visible;
            Aparecer(sp_Inicio);
            Aparecer(sp_Atras);
            Aparecer_Boton(btn_atras);
        }
        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            sp_Inicio.Visibility = Visibility.Collapsed;
            sp_LOGIN.Visibility = Visibility.Visible;
            sp_Atras.Visibility = Visibility.Visible;
        }

        //private void btn_atras_Click(object sender, RoutedEventArgs e)
        //{
        //    sp_Inicio.Visibility = Visibility.Collapsed;
        //    sp_Atras.Visibility = Visibility.Visible;
        //}

        // Nos oculta todo y nos muestra el registro
        private void bt_registro_Click(object sender, RoutedEventArgs e)
        {
            //OcultarTodo();
            //MostrarRegistro();
            sp_FormRegistro.Visibility = Visibility.Visible;
            Aparecer(sp_FormRegistro);
            Desparecer(sp_LOGIN);
        }

        //  Los TextBox que tenemos, nos los mandamos al manager que va a la BBDD
        private void bt_login_Click(object sender, RoutedEventArgs e)
        {
            manager.Login(tb_usuario.Text, pb_pass.Password);
        }

        // Inicio de invitado
        private void bt_usuarioAnonimo_Click(object sender, RoutedEventArgs e)
        {
            //OcultarTodo();
            //MostrarPreparacionSimulacion();
            Aparecer(sp_PreparacionSimulacion);
            Desparecer(sp_LOGIN);
            sp_PreparacionSimulacion.Visibility = Visibility.Visible;
            sp_Atras.Visibility = Visibility.Visible;
            Aparecer(sp_Atras);
            
        }
        // Al iniciar el tablero nos dara el tablero que indicamos 
        private void bt_iniciarTablero_Click(object sender, RoutedEventArgs e)
        {
            manager.CargarDatosNuevaSimulacion(int.Parse(tb_filas.Text),
                                               int.Parse(tb_columnas.Text),
                                               double.Parse(tb_P.Text),
                                               double.Parse(tb_Q.Text));

            sp_Tablero.Children.Clear();

            sp_Tablero.Children.Add(manager.PintarTableroModelo());

            sp_Tablero.Visibility = Visibility.Visible;

        }
        // Boton avanzar
        private void Avanzar1_Click(object sender, RoutedEventArgs e)
        {
            manager.Avanzar1();

            sp_Tablero.Children.Clear();
            sp_Tablero.Children.Add(manager.PintarTableroModelo());
        }
        // Ocultamos el tablero y la simulacion y enseñamos el login
        //private void bt_Cerrar_Sesion(object sender, RoutedEventArgs e)
        //{
        //    sp_Tablero.Visibility = Visibility.Collapsed;
        //    sp_PreparacionSimulacion.Visibility = Visibility.Collapsed;
        //    sp_LOGIN.Visibility = Visibility.Visible;
        //}

        // Ocultamos el Login y mostramos el registro
        //private void bt_Registrarse_Click(object sender, RoutedEventArgs e)
        //{
        //    sp_FormRegistro.Visibility = Visibility.Visible;
        //    sp_LOGIN.Visibility = Visibility.Collapsed;
        //}
        private void Resolucion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (ComboBoxItem)Resolucion.SelectedItem;
            var resolution = selectedItem.Content.ToString().Split('x');
            var width = int.Parse(resolution[0]);
            var height = int.Parse(resolution[1]);

            this.Height = height;
            this.Width = width;
        }
        private void Desparacer_Boton(Button target)
        {
            // object target = null;
            // Si no hay ningun target
            if (target == null)
            {
            }
            else
            {
                /* Creamos una animacion*/
                var animacion = new DoubleAnimation();
                animacion.From = 1.0;
                animacion.To = 0.0;
                animacion.Duration = new Duration(TimeSpan.FromSeconds(5));
                /* Creamos la Storyboard, donde va a ir la animacion*/
                Storyboard myStoryboard = new Storyboard();
                myStoryboard.Children.Add(animacion);
                /*        Asignamos quien va a hacer la animacion*/
                Storyboard.SetTargetName(animacion, target.Name);
                /*Ponemos que propiedad se va a animar*/
                Storyboard.SetTargetProperty(animacion, new PropertyPath(Button.OpacityProperty));
                /*Empieza la animacion*/
                myStoryboard.Begin(this); // No lo hace cuando estoy encima, lo hace al ejecutarse
            }
        }

        private void Aparecer_Boton(Button target)
        {
            // object target = null;
            // Si no hay ningun target
            if (target == null)
            {
            }
            else
            {
                /* Creamos una animacion*/
                var animacion = new DoubleAnimation();
                animacion.From = 0.0;
                animacion.To = 1.0;
                animacion.Duration = new Duration(TimeSpan.FromSeconds(5));
                /* Creamos la Storyboard, donde va a ir la animacion*/
                Storyboard myStoryboard = new Storyboard();
                myStoryboard.Children.Add(animacion);
                /*        Asignamos quien va a hacer la animacion*/
                Storyboard.SetTargetName(animacion, target.Name);
                /*Ponemos que propiedad se va a animar*/
                Storyboard.SetTargetProperty(animacion, new PropertyPath(Button.OpacityProperty));
                /*Empieza la animacion*/
                myStoryboard.Begin(this); // No lo hace cuando estoy encima, lo hace al ejecutarse
            }
        }

        private void Aparecer(StackPanel target)
        {
            // object target = null;
            // Si no hay ningun target
            if (target == null)
            {
            }
            else
            {
                //Creamos una animacion
                var Aparecer_Simulacion = new DoubleAnimation();
                Aparecer_Simulacion.From = 0.0;
                Aparecer_Simulacion.To = 1.0;
                Aparecer_Simulacion.Duration = new Duration(TimeSpan.FromSeconds(5));
                // Creamos la Storyboard, donde va a ir la animacion
                Storyboard myStoryboard = new Storyboard();
                myStoryboard.Children.Add(Aparecer_Simulacion);
                // Asignamos quien va a hacer la animacion
                Storyboard.SetTargetName(Aparecer_Simulacion, target.Name);
                // Ponemos que propiedad se va a animar
                Storyboard.SetTargetProperty(Aparecer_Simulacion, new PropertyPath(StackPanel.OpacityProperty));
                // Empieza la animacion
                myStoryboard.Begin(this); // No lo hace cuando estoy encima, lo hace al ejecutarse
            }
        }
        private void Desparecer(StackPanel target)
        {
            // object target = null;
            // Si no hay ningun target
            if (target == null)
            {
            }
            else
            {
                //Creamos una animacion
                var animacion = new DoubleAnimation();
                animacion.From = 1.0;
                animacion.To = 0.0;
                animacion.Duration = new Duration(TimeSpan.FromSeconds(5));
                // Creamos la Storyboard, donde va a ir la animacion
                Storyboard myStoryboard = new Storyboard();
                myStoryboard.Children.Add(animacion);
                // Asignamos quien va a hacer la animacion
                Storyboard.SetTargetName(animacion, target.Name);
                // Ponemos que propiedad se va a animar
                Storyboard.SetTargetProperty(animacion, new PropertyPath(StackPanel.OpacityProperty));
                // Empieza la animacion
                myStoryboard.Begin(this); // No lo hace cuando estoy encima, lo hace al ejecutarse
                Thread.Sleep(1000);
                target.Visibility = Visibility.Collapsed;
            }
        }

        //private void bt_Salir_Click(object sender, RoutedEventArgs e)
        //{
        //    Desparecer(sp_Atras);
        //    sp_Atras.Visibility = Visibility.Visible;
        //}
        private void btn_atras_Click(object sender, RoutedEventArgs e)
        {
            Desparecer(sp_Atras);
            Aparecer(sp_Inicio);
            Desparecer(sp_Tablero);
            Desparecer(sp_PreparacionSimulacion);
            sp_Creditos.Visibility = Visibility.Collapsed;
            sp_PreparacionSimulacion.Visibility = Visibility.Collapsed;
            sp_Tablero.Visibility = Visibility.Collapsed;
            sp_Ajustes.Visibility = Visibility.Collapsed;
            sp_Inicio.Visibility = Visibility.Visible;
            //btn_atras.Visibility = Visibility.Collapsed;
        }
        private void bt_Salir_Click(object sender, RoutedEventArgs e)
        {
            Desparecer(sp_Inicio);
            Aparecer(sp_LOGIN);
            sp_Inicio.Visibility = Visibility.Collapsed;
            sp_LOGIN.Visibility = Visibility.Visible;
            btn_atras.Visibility = Visibility.Collapsed;
        }

        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Volume = volumeSlider.Value / 100;
            //descomentar para que funcione la musica del menu
            mediaPlayer.Play();

        }
    }
}