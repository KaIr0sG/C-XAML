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
        string _email;
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

        public string email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = email;
            }
        }
        //Constructor(es)
        public Empleado(int idUsuario, string usuario, string nombre, string apellido, string email)
        {
            _idUsuario = IdUsuario;
            _usuario = usuario;
            _nombre = nombre;
            _apellido = apellido;
            _email = email;
        }

        //Metodos

    }
}