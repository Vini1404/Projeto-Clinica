using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X500;
using ProjetoCLinica.Dados;
using ProjetoCLinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace ProjetoCLinica.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        clEsp modEsp = new clEsp();
        clEspAcoes acEsp = new clEspAcoes();
        clMedico modMedico = new clMedico();
        clMedicoAcoes acMedico = new clMedicoAcoes();
        clPaciente modPaciente = new clPaciente();
        clPacienteAcoes acPaciente = new clPacienteAcoes();
        clAtendimentoAcoes acAtend = new clAtendimentoAcoes();



        public void carregarMedicos()
        {
            List<SelectListItem> medicos = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;port=3306; DataBase=bdClin0408; user id=root;password=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbMedico", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    medicos.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
            }
            ViewBag.med = new SelectList(medicos, "Value", "Text");
        }

        public void carregarPacientes()
        {
            List<SelectListItem> paciente = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;port=3306; DataBase=bdClin0408; user id=root;password=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbPaciente", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    paciente.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                    
                }
                con.Close();
            }
            ViewBag.pac = new SelectList(paciente, "Value", "Text");
        }

        public void carregarEsp()
        {
            List<SelectListItem> medicos = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;port=3306; DataBase=bdClin0408; user id=root;password=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbEsp", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    medicos.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
            }
            ViewBag.esp = new SelectList(medicos, "Value", "Text");
        }
        public ActionResult cadEsp()
        {

            return View();

        }
        [HttpPost]
        public ActionResult cadEsp(FormCollection frm)
        {
            modEsp.especialidade = frm["txtEsp"];
            acEsp.inserirEsp(modEsp);
            ViewBag.msg = "Cadastro Realizado com sucesso!";
            return View();
        }

        public ActionResult cadMedico()
        {
            carregarEsp();
            return View();
        }

        [HttpPost]
        public ActionResult cadMedico(FormCollection frm)
        {
            carregarEsp();

            modMedico.nomeMedico = frm["txtNmMedico"];
            modMedico.codEspecialidade = Request["esp"];

            acMedico.inserirMedico(modMedico);
            ViewBag.msg = "Cadastro Realizado com sucesso!";

            return View();
        }

        public ActionResult cadPaciente()
        {
            return View();

        }

        [HttpPost]
        public ActionResult cadPaciente(FormCollection frm)
        {
            modPaciente.nomePac = frm["txtNmPaciente"];
            modPaciente.telPac = frm["txtTelefone"];
            modPaciente.celPac = frm["txtCelular"];
            modPaciente.emailPac = frm["txtemail"];

            acPaciente.inserirPaciente(modPaciente);

            ViewBag.msg = "Cadastro Realizado com sucesso!";
            return View();

        }

        public ActionResult cadAtendimento()
        {
            carregarMedicos();
            carregarPacientes();
            return View();
        }
        [HttpPost]
        public ActionResult cadAtendimento(clAtendimento modeloAtend)
        {
            carregarMedicos();
            carregarPacientes();
            acAtend.TestarAgenda(modeloAtend);

            if (modeloAtend.confAgendamento == "1")
            {

                modeloAtend.codPac = Request["pac"];
                modeloAtend.codMedico = Request["med"];

                acAtend.inserirAgenda(modeloAtend);
                ViewBag.msg = "Agendanento realizado com sucesso";
            }
            else
            {
                ViewBag.msg = "Horário indisponivel, por favor escolher outra data/hora";
                
            }
            return View();
        }

        public ActionResult consAgenda()
        {
            GridView gvAtend = new GridView();
            gvAtend.DataSource = acAtend.selecionaAgenda();
            gvAtend.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvAtend.RenderControl(htw);
            ViewBag.GridvieaString = sw.ToString();
            return View();
        }

        public ActionResult consAgendaBusca() { return View(); }

        [HttpPost]
        public ActionResult consAgendaBusca(clAtendimento modeloAtend)
        {
            GridView gvAtend = new GridView();
            gvAtend.DataSource = acAtend.selecionaAgendaData(modeloAtend);
            gvAtend.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvAtend.RenderControl(htw);
            ViewBag.GridViewString = sw.ToString();
            return View();
        }

    }
}