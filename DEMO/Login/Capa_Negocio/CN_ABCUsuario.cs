﻿using System;
using System.Text;
using MySql.Data.MySqlClient;
using Capa_Datos;
using System.Security.Cryptography;

namespace Capa_Negocio
{
    public class CN_ABCUsuario
    {
        private CD_ABCUsuario _ABCUsuario = new CD_ABCUsuario();
        string key = "mikey";
        public void RegistrarUsuario(String nombre, String apellidos, String contra, String cargo, String nickname, String correo)
        {
            _ABCUsuario.RegistrarUsuario(nombre,apellidos,contra,cargo,nickname,correo);
        }

        public void EditarUsuario(int id, String nombre, String apellidos, String contra, 
            String cargo, String nickname, String correo)
        {
            _ABCUsuario.EditarUsuario(id, nombre, apellidos, Encriptar(contra), cargo, 
                nickname, correo);
        }

        public System.Data.DataTable MostrarUsuarios()
        {
            return _ABCUsuario.MostrarUsuarios();
        }

        public string Encriptar(string texto)
        {
            //arreglo de bytes donde guardaremos la llave
            byte[] keyArray;
            //arreglo de bytes donde guardaremos el texto
            //que vamos a encriptar
            byte[] Arreglo_a_Cifrar =
            UTF8Encoding.UTF8.GetBytes(texto);

            //se utilizan las clases de encriptación
            //provistas por el Framework
            //Algoritmo MD5
            MD5CryptoServiceProvider hashmd5 =
            new MD5CryptoServiceProvider();
            //se guarda la llave para que se le realice
            //hashing
            keyArray = hashmd5.ComputeHash(
            UTF8Encoding.UTF8.GetBytes(key));

            hashmd5.Clear();

            //Algoritmo 3DAS
            TripleDESCryptoServiceProvider tdes =
            new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            //se empieza con la transformación de la cadena
            ICryptoTransform cTransform =
            tdes.CreateEncryptor();

            //arreglo de bytes donde se guarda la
            //cadena cifrada
            byte[] ArrayResultado =
            cTransform.TransformFinalBlock(Arreglo_a_Cifrar,
            0, Arreglo_a_Cifrar.Length);

            tdes.Clear();

            //se regresa el resultado en forma de una cadena
            return Convert.ToBase64String(ArrayResultado,
            0, ArrayResultado.Length);
        }
    }
}
