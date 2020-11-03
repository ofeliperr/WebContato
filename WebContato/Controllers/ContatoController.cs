using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebContato.Models;

namespace WebContato.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoDAL cont;
        public ContatoController(IContatoDAL contato)
        {
            cont = contato;
        }

        public IActionResult Index()
        {
            List<Contato> listaContatos = new List<Contato>();
            listaContatos = cont.GetAllContatos().ToList();
            return View(listaContatos);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Contato contato = cont.GetContato(id);
            if (contato == null)
            {
                return NotFound();
            }
            return View(contato);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Contato contato)
        {
            if (contato.TelefoneRes.Length < 8)
            {
                return Json(new { status = "error", message = "Telefone Residencial inválido!" });
            }
            if (contato.TelefoneCel is null)
            {
                contato.TelefoneCel = "";
            }

            if (ModelState.IsValid)
            {
                cont.AddContato(contato);
                return RedirectToAction("Index");
            }
            return View(contato);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Contato contato = cont.GetContato(id);
            if (contato == null)
            {
                return NotFound();
            }
            return View(contato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind]Contato contato)
        {
            if (id != contato.ContatoId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                cont.UpdateContato(contato);
                return RedirectToAction("Index");
            }
            return View(contato);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Contato contato = cont.GetContato(id);
            if (contato == null)
            {
                return NotFound();
            }
            return View(contato);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            cont.DeleteContato(id);
            return RedirectToAction("Index");
        }
    }
}