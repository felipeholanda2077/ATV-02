using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult ListaDeUsuarios()
        {

            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            List<Usuario> listagem = new UsuarioService().Listar();
            return View(listagem);
        }

        public IActionResult editarUsuario(int Id)
        {

            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            Usuario user = new UsuarioService().Listar(Id);
            return View(user);
        }

        [HttpPost]
        public IActionResult editarUsuario(Usuario userEditado)
        {

            userEditado.Senha = Criptografo.TextoCriptografado(userEditado.Senha);

            UsuarioService us = new UsuarioService();
            us.editarUsuario(userEditado);

            return RedirectToAction("ListadeUsuarios");
        }

        public IActionResult RegistrarUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuarios(Usuario novoUser)
        {

            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            novoUser.Senha = Criptografo.TextoCriptografado(novoUser.Senha);


            UsuarioService us = new UsuarioService();
            us.incluirUsuario(novoUser);

            return RedirectToAction("ListadeUsuarios");
        }

        public IActionResult ExcluirUsuario(int id)
        {

            UsuarioService us = new UsuarioService();
            us.excluirUsuario(id);
            return RedirectToAction("ListaDeUsuarios");
        }


        public IActionResult Sair()
        {



            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult NeedAdmin()
        {

            Autenticacao.CheckLogin(this);
            return View();
        }
    }
}