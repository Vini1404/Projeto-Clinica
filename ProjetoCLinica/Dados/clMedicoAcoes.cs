using MySql.Data.MySqlClient;
using ProjetoCLinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoCLinica.Dados
{
    public class clMedicoAcoes
    {
        Conexao con = new Conexao();

        public void inserirMedico(clMedico cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbMedico(nomeMedico, codEspecialidade) values (@nomeMedico, @codEspecialidade)", con.conectarBD());
            cmd.Parameters.Add("@nomeMedico", MySqlDbType.VarChar).Value = cm.nomeMedico;
            cmd.Parameters.Add("@codEspecialidade", MySqlDbType.VarChar).Value = cm.codEspecialidade;

            cmd.ExecuteNonQuery();  
            con.desconectarBD();
        }
    }
}