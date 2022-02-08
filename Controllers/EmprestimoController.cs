using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;


namespace Biblioteca.Controllers
{

    public class EmprestimoController : Controller
    {
        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);

            LivroService livroService = new LivroService();
            EmprestimoService emprestimoService = new EmprestimoService();

            CadEmprestimoViewModel cadModel = new CadEmprestimoViewModel();
            cadModel.Livros = livroService.ListarTodos();
            return View(cadModel);
        }

        [HttpPost]
        public IActionResult Cadastro(CadEmprestimoViewModel viewModel)
        {

            if (!string.IsNullOrEmpty(viewModel.NomeUsuario) && !string.IsNullOrEmpty(viewModel.Telefone) && viewModel.Ano != 0)
            {
                EmprestimoService emprestimoService = new EmprestimoService();

                if (viewModel.Emprestimo.Id == 0)
                {
                    emprestimoService.Inserir(viewModel.Emprestimo);
                }
                else
                {
                    emprestimoService.Atualizar(viewModel.Emprestimo);
                }
                return RedirectToAction("Listagem");
            } else
            {
                ViewData["mensagem"] = "Preencha todos os campos";
                return View();
            }
        }

            public IActionResult Listagem(string tipoFiltro, string filtro, string itensPorPagina, int NumDaPagina, int PaginaAtual)
            {
                
                Autenticacao.CheckLogin(this);

                FiltrosEmprestimos objFiltro = null;
                if (!string.IsNullOrEmpty(filtro))
                {
                    objFiltro = new FiltrosEmprestimos();
                    objFiltro.Filtro = filtro;
                    objFiltro.TipoFiltro = tipoFiltro;
                }

                ViewData["livrosPorPagina"] = (string.IsNullOrEmpty(itensPorPagina) ? 10 : int.Parse(itensPorPagina));
                ViewData["PaginaAtual"] = (PaginaAtual != 0 ? PaginaAtual : 1);

                EmprestimoService emprestimoService = new EmprestimoService();
                return View(emprestimoService.ListarTodos(objFiltro));
            }

            public IActionResult Edicao(int id)
            {
                LivroService livroService = new LivroService();
                EmprestimoService em = new EmprestimoService();
                Emprestimo e = em.ObterPorId(id);

                CadEmprestimoViewModel cadModel = new CadEmprestimoViewModel();
                cadModel.Livros = livroService.ListarTodos();
                cadModel.Emprestimo = e;

                return View(cadModel);
            }
        }
    }