using System;
using System.Collections.Generic;
using System.Text;

namespace AccDatos
{
    public class Empleado
    {
        //campos privados
        int _idUsuario;
        string _usuario;
        string _nombre;
        string _apellido;
        string _mail;
        //Propiedades publicas
        public int IdUsuario
        {
            get
            {
                return _idUsuario;
            }
            set
            {
                _idUsuario = value;
            }
        }
        public string Usuario
        {
            get
            {
                return _usuario;
            }
            set
            {
                _usuario = value;
            }
        }

        public string nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                _nombre = nombre;
            }
        }

        public string apellido
        {
            get
            {
                return _apellido;
            }
            set
            {
                _apellido = apellido;
            }
        }

        public string mail
        {
            get
            {
                return _mail;
            }
            set
            {
                _mail = mail;
            }
        }
        //Constructor(es)
        public Empleado(int idEmpleado, string usuario, int idTienda)
        {
            _idUsuario = IdUsuario;
            _usuario = usuario;
            _nombre = nombre;
            _apellido = apellido;
            _mail = mail;
        }

        //Metodos

    }
}