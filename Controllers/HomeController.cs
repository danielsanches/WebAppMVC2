using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json.Nodes;
using WebAppMVC2.Models;

namespace WebAppMVC2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static ModelTest modelTest = new ModelTest();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult NovaTela()
        {
            return View(modelTest);
        }

        [HttpPost]
        public IActionResult NovaTela(ModelTest model)
        {
            if (modelTest == null)
                modelTest = new ModelTest();

            PreencherItensGrid(model);

            PreencherFiltroPorNome();

            return View(modelTest);
        }

        private static void PreencherItensGrid(ModelTest model)
        {
            var id = 1;
            if (modelTest.ItensGrid.Any())
                id = modelTest.ItensGrid.Max(c => c.Id) + 1;

            if (model.Quantidade > 0 && modelTest.ItensGrid.Count(c => c.Nome == model.Nome && c.Quantidade == model.Quantidade) == 0)
            {
                modelTest.ItensGrid.Add(new ItensGrid { Id = id, Nome = model.Nome, Quantidade = model.Quantidade });
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
        public IActionResult Editar(ItensGrid model)
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
