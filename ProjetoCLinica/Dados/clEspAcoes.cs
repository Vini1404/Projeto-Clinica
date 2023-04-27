using MySql.Data.MySqlClient;
using ProjetoCLinica.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ProjetoCLinica.Dados
{
    public class clEspAcoes
    {
        Conexao con = new Conexao();

        public void inserirEsp(clEsp cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbEsp(especialidade) values (@especialidade)", con.conectarBD());
            cmd.Parameters.Add("@especialidade", MySqlDbType.VarChar).Value = cm.especialidade;

            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }

        public DataTable consultaEsp()
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbEsp", con.conectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable Especialidade = new DataTable();
            da.Fill(Especialidade); 
            con.desconectarBD();

            return Especialidade;
        }
    }
    
    
}