using MySql.Data.MySqlClient;
using ProjetoCLinica.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ProjetoCLinica.Dados
{
    public class clPacienteAcoes
    {
        Conexao con = new Conexao();
        public void inserirPaciente(clPaciente cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbPaciente(nomePac, telPac, celPac, emailPac) values (@nomePac, @telPac, @celPac, @emailPac)", con.conectarBD());
            cmd.Parameters.Add("@nomePac", MySqlDbType.VarChar).Value = cm.nomePac;
            cmd.Parameters.Add("@telPac", MySqlDbType.VarChar).Value = cm.telPac;
            cmd.Parameters.Add("@celPac", MySqlDbType.VarChar).Value = cm.celPac;
            cmd.Parameters.Add("@emailPac", MySqlDbType.VarChar).Value = cm.emailPac;

            cmd.ExecuteNonQuery();  
            con.desconectarBD();
        }
}
}