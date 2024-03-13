using Microsoft.AspNetCore.Mvc;
using WebAppMVC2.Models.Teste;

namespace WebAppMVC2.Controllers
{
    public class TesteController : Controller
    {
        private static NovaTelaViewModel modelTest = new NovaTelaViewModel();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NovaTela()
        {
            return View(modelTest);
        }

        [HttpPost]
        public IActionResult NovaTela(NovaTelaViewModel model)
        {
            modelTest ??= new NovaTelaViewModel();

            PreencherItensGrid(model);

            PreencherFiltroPorNome();

            return View(modelTest);
        }

        private static void PreencherItensGrid(NovaTelaViewModel model)
        {
            var id = 1;
            if (modelTest.ItensGrid.Any())
                id = modelTest.ItensGrid.Max(c => c.Id) + 1;

            if (model.Quantidade > 0 && modelTest.ItensGrid.Count(c => c.Nome == model.Nome && c.Quantidade == model.Quantidade) == 0)
            {
                modelTest.ItensGrid.Add(new ItensGridViewModel { Id = id, Nome = model.Nome, Quantidade = model.Quantidade });
            }
        }

        private void PreencherFiltroPorNome()
        {
            var itensDrop = new Dictionary<int, string>();
            foreach (var item in modelTest.ItensGrid)
            {
                itensDrop.Add(item.Id, item.Nome);
            }

            ViewBag.Usuarios = itensDrop;
        }

        public IActionResult Editar(int id)
        {
            return View(modelTest.ItensGrid.FirstOrDefault(c => c.Id == id));
        }

        [HttpPost]
        public IActionResult Editar(ItensGridViewModel model)
        {
            var item = modelTest.ItensGrid.FirstOrDefault(c => c.Id == model.Id);
            item.Quantidade = model.Quantidade;
            item.Nome = model.Nome;

            return RedirectToAction("NovaTela");
        }

        public JsonResult Excluir(int id)
        {
            var itemRemover = modelTest.ItensGrid.First(c => c.Id == id);
            modelTest.ItensGrid.Remove(itemRemover);

            return Json("");
        }
    }
}
