using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AccDatos
{
    public class DummyManager
    {
        AccesoDatos aData;
        //Campos privados
        private int _filas;
        private int _columnas;
        private int _altura;
        private int _ancho;
        private double _p; //probabilidad nacimiento arbol en tierra fertil
        private double _q; //probabilidad combustion espontanea de arbol

        private char[,] _tableroActual;
        private char[,] _tableroSiguiente;
        //"x" Tierra NO fertil
        //"_" Tierra fertil
        //"o" Arbol
        //"*" Arbol en llamas
        //"w" Agua/rio

        //Propiedades publicas
        public int altura
        {
            get
            {
                return _altura;
            }
            set
            {
                _altura = value;
            }
        }

        public int ancho
        {
            get
            {
                return _ancho;
            }
            set
            {
                _ancho = value;
            }
        }

        public double P
        {
            get
            {
                return _p;
            }
            set
            {
                _p = value;
                //[TO DO]: Binding para visualizar
            }
        }
        public double Q
        {
            get
            {
                return _q;
            }
            set
            {
                _q = value;
                //[TO DO]: Binding para visualizar
            }
        }

        //Constructor(es)
        public DummyManager()
        {
            aData = new AccesoDatos();

        }

        //Metodos

        //public void CambiarApellidoActor(int id_actor, string nuevoApellido)
        //{
        //    aData.SP_UpdateActor(id_actor, nuevoApellido);
        //}

        public void Login(string usuario, string pass)
        {
            int respuesta = aData.SP_Login(usuario, Md5(pass));
            if (respuesta == 0)
            {
                MessageBox.Show("Bienvenid@");
            }
            else if (respuesta == -1)
            {
                MessageBox.Show("No existe este usuario en la base de datos");
            }
            else if (respuesta == -2)
            {
                MessageBox.Show("Contraseña incorrecta");
            }
            else
            {
                MessageBox.Show("Error desconocido");
            }
        }
        public Empleado VerDatosUsuario(int idEmpleado)
        {
            aData.SP_StaffDetail(idEmpleado); // Ver esto

            return null;
        }

        public void AltaUsuario(string nombre, string apellido, string email, string usuario, string pass)
        {
            if (aData.SP_AddUser(nombre, apellido, email, usuario, Md5(pass)) == 0)
            {
                MessageBox.Show("El usuario ha sido dado de alta CORRECTAMENTE");
            }
        }
        public int CargarDatosNuevaSimulacion(int filas, int columnas, double p, double q)
        {
            _filas = filas;
            _columnas = columnas;
            P = p;
            Q = q;

            _tableroActual = new char[_filas, _columnas];
            _tableroSiguiente = new char[_filas, _columnas];

            //Primera generación vacía por defecto (celdas fertiles)
            for (int i = 0; i < _filas; i++)
                for (int j = 0; j < _columnas; j++)
                {
                    _tableroActual[i, j] = '_';
                    _tableroSiguiente[i, j] = '_';
                }

            return 0;
        }
        public int CargarEjemploPrestablecido()
        {
            _filas = 3;
            _columnas = 3;
            P = 0.001;
            Q = 0.01;

            _tableroActual = new char[_filas, _columnas];
            _tableroSiguiente = new char[_filas, _columnas];

            _tableroActual[0, 0] = '_';
            _tableroActual[0, 1] = 'w';
            _tableroActual[0, 2] = '_';
            _tableroActual[1, 0] = 'o';
            _tableroActual[1, 1] = '*';
            _tableroActual[1, 2] = 'x';
            _tableroActual[2, 0] = 'w';
            _tableroActual[2, 1] = 'o';
            _tableroActual[2, 2] = 'w';

            for (int i = 0; i < _filas; i++)
                for (int j = 0; j < _columnas; j++)
                {
                    _tableroSiguiente[i, j] = '_';
                }


            return 0;
        }
        public Grid PintarTablero()
        {
            Grid tablero = new Grid();

            tablero.Width = 600;
            tablero.Height = 600;
            //tablero.ShowGridLines = true;

            for (int i = 0; i < _columnas; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                tablero.ColumnDefinitions.Add(cd);
            }
            for (int i = 0; i < _filas; i++)
            {
                RowDefinition rd = new RowDefinition();
                tablero.RowDefinitions.Add(rd);
            }

            // Casilla Vacia para cada rincon 
            Uri imgUri = new Uri("./img/fertil.png", UriKind.Relative);
            for (int i = 0; i < _filas; i++)
            {
                for (int j = 0; j < _columnas; j++)
                {
                    Image item = new Image();
                    item.Name = "c_" + i + "_" + j;
                    item.Source = new BitmapImage(imgUri);
                    item.Margin = new Thickness(1);
                    Grid.SetRow(item, i);
                    Grid.SetColumn(item, j);
                    tablero.Children.Add(item);
                }
            }

            return tablero;
        }
        public Grid PintarTableroModelo()
        {
            Grid tablero = new Grid();

            tablero.Width = 600;
            tablero.Height = 600;

            for (int i = 0; i < _columnas; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                tablero.ColumnDefinitions.Add(cd);
            }
            for (int i = 0; i < _filas; i++)
            {
                RowDefinition rd = new RowDefinition();
                tablero.RowDefinitions.Add(rd);
            }
            for (int i = 0; i < _filas; i++)
            {
                for (int j = 0; j < _columnas; j++)
                {
                    Image item = new Image();
                    item.PreviewMouseDown += new MouseButtonEventHandler(click_celda);
                    Uri imgUri = new Uri("./img/fertil.png", UriKind.Relative);
                    if (_tableroActual[i, j] == '_')
                        imgUri = new Uri("./img/fertil.png", UriKind.Relative);

                    if (_tableroActual[i, j] == 'x')
                        imgUri = new Uri("./img/nofertil.png", UriKind.Relative);

                    if (_tableroActual[i, j] == 'o')
                        imgUri = new Uri("./img/arbol.png", UriKind.Relative);

                    if (_tableroActual[i, j] == '*')
                        imgUri = new Uri("./img/fuego.png", UriKind.Relative);

                    if (_tableroActual[i, j] == 'w')
                        imgUri = new Uri("./img/agua.gif", UriKind.Relative);

                    item.Name = "c_" + i + "_" + j;
                    item.Source = new BitmapImage(imgUri);
                    item.Margin = new Thickness(1);
                    Grid.SetRow(item, i);
                    Grid.SetColumn(item, j);
                    tablero.Children.Add(item);

                }
            }

            return tablero;
        }
        private void click_celda(object sender, RoutedEventArgs e)
        {
            Image celda_clickada = (Image)e.Source;
            Uri imgUri = new Uri("./img/fertil.png", UriKind.Relative);
            //c_0_0
            int i = int.Parse(celda_clickada.Name.Split('_')[1]);
            int j = int.Parse(celda_clickada.Name.Split('_')[2]);

            //cambiar el estado de una celda (irlo rotando...)
            if (_tableroActual[i, j] == 'x')
            {
                _tableroActual[i, j] = '_';
                imgUri = new Uri("./img/fertil.png", UriKind.Relative);
                celda_clickada.Source = new BitmapImage(imgUri);
            }
            else if (_tableroActual[i, j] == '_')
            {
                _tableroActual[i, j] = 'o';
                imgUri = new Uri("./img/arbol.png", UriKind.Relative);
                celda_clickada.Source = new BitmapImage(imgUri);
            }
            else if (_tableroActual[i, j] == 'o')
            {
                _tableroActual[i, j] = '*';
                imgUri = new Uri("./img/fuego.png", UriKind.Relative);
                celda_clickada.Source = new BitmapImage(imgUri);
            }
            else if (_tableroActual[i, j] == '*')
            {
                _tableroActual[i, j] = 'w';
                imgUri = new Uri("./img/agua.gif", UriKind.Relative);
                celda_clickada.Source = new BitmapImage(imgUri);
            }
            else if (_tableroActual[i, j] == 'w')
            {
                _tableroActual[i, j] = 'x';
                imgUri = new Uri("./img/nofertil.png", UriKind.Relative);
                celda_clickada.Source = new BitmapImage(imgUri);
            }
        }
        public void Avanzar1()
        {
            Random r = new Random((int)DateTime.Now.Ticks);

            for (int i = 0; i < _filas; i++)
            {
                for (int j = 0; j < _columnas; j++)
                {
                    //Si la casilla tiene suelo infértil,
                    //la siguiente iteración pasará a ser
                    //de suelo fértil si tenía al lado suelo fértil.
                    if (_tableroActual[i, j] == 'x')
                    {
                        if (((i - 1 >= 0) && _tableroActual[i - 1, j] == '_') ||
                            ((i + 1 < _filas) && _tableroActual[i + 1, j] == '_') ||
                            ((j - 1 >= 0) && _tableroActual[i, j - 1] == '_') ||
                            ((j + 1 < _columnas) && _tableroActual[i, j + 1] == '_')
                            )
                        {
                            _tableroSiguiente[i, j] = '_';
                        }
                    }
                    //Si la casilla tiene suelo fértil, existe una probabilidad P
                    //de que la siguiente iteración nazca un árbol.
                    if (_tableroActual[i, j] == '_')
                    {
                        if (r.NextDouble() < P)
                            _tableroSiguiente[i, j] = 'o';
                        else
                            _tableroSiguiente[i, j] = '_';
                    }
                    //Si la casilla tiene árbol, existe una probabilidad Q de que el árbol prenda espontáneamente(durante la siguiente iteración, esa casilla será de fuego)
                    if (_tableroActual[i, j] == 'o')
                    {
                        if (((i - 1 >= 0) && _tableroActual[i - 1, j] == '*') ||
                            ((i + 1 < _filas) && _tableroActual[i + 1, j] == '*') ||
                            ((j - 1 >= 0) && _tableroActual[i, j - 1] == '*') ||
                            ((j + 1 < _columnas) && _tableroActual[i, j + 1] == '*')
                            )
                        {
                            _tableroSiguiente[i, j] = '*';
                        }
                        else
                        {
                            if (r.NextDouble() < Q)
                                _tableroSiguiente[i, j] = '*';
                            else
                                _tableroSiguiente[i, j] = 'o';
                        }

                    }

                    //Si la casilla está en fuego, la siguiente iteración será de suelo infértil y las casillas adyacentes que tengan árbol, se prenderán en la siguiente iteración.
                    if (_tableroActual[i, j] == '*')
                    {
                        //Las casillas adyacentes a los ríos
                        //NUNCA pueden estar en estado infértil,
                        //como poco estarán en estado fértil.
                        if (((i - 1 >= 0) && _tableroActual[i - 1, j] == 'w') ||
                            ((i + 1 < _filas) && _tableroActual[i + 1, j] == 'w') ||
                            ((j - 1 >= 0) && _tableroActual[i, j - 1] == 'w') ||
                            ((j + 1 < _columnas) && _tableroActual[i, j + 1] == 'w')
                            )
                        {
                            _tableroSiguiente[i, j] = '_';
                        }
                        else
                        {
                            _tableroSiguiente[i, j] = 'x';
                        }
                    }
                    if (_tableroActual[i, j] == 'w')
                    {
                        _tableroSiguiente[i, j] = 'w';
                    }

                }
            }
            for (int i = 0; i < _filas; i++)
                for (int j = 0; j < _columnas; j++)
                    _tableroActual[i, j] = _tableroSiguiente[i, j];

        }

        public static string Md5(string input, bool isLowercase = false)
        {
            using (var md5 = MD5.Create())
            {
                var byteHash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var hash = BitConverter.ToString(byteHash).Replace("-", "");
                return (isLowercase) ? hash.ToLower() : hash;
            }
        }
    }
}
