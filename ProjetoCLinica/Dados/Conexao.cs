using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace ProjetoCLinica.Dados
{
    public class Conexao
    {
        MySqlConnection cn = new MySqlConnection("Server=localhost; Database=bdClin0408; User=root; pwd=12345678");
        public static string msg;

        public MySqlConnection conectarBD()
        {
            try
            {
                cn.Open();
            }

            catch (Exception erro)
            {
                msg = "Ocorreu um erro ao se conectar" + erro.Message;
            }

            return cn;
        }


        public MySqlConnection desconectarBD()
        {
            try
            {
                cn.Close();
            }

            catch (Exception erro)
            {
                msg = "Ocorreu um erro ao se desconectar" + erro.Message;
            }

            return cn;
        }


    }
}