using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Biblioteca.Models
{
    public class UsuarioService
    {


        
        public void incluirUsuario(Usuario user){

            using(BibliotecaContext bc = new BibliotecaContext()){

                bc.Usuarios.Add(user);
                bc.SaveChanges();
            }

      
        }

        public void editarUsuario(Usuario user){


            using(BibliotecaContext bc = new BibliotecaContext()){

                Usuario u = bc.Usuarios.Find(user.Id);

                u.Login = user.Login;
                u.Nome = user.Nome;
                u.Senha = user.Senha;
                u.Tipo = user.Tipo;

                bc.SaveChanges();
            }

        }

        public void excluirUsuario(int Id){

        using(BibliotecaContext bc = new BibliotecaContext()){

                bc.Remove(bc.Usuarios.Find(Id));
                bc.SaveChanges();
            }
        }


        public Usuario Listar(int Id){

            using(BibliotecaContext bc = new BibliotecaContext()){

                return bc.Usuarios.Find(Id);
            }
        }
            
        public List<Usuario> Listar(){

            using(BibliotecaContext bc = new BibliotecaContext()){

                return bc.Usuarios.ToList(); 
            }

        }
    }
}
