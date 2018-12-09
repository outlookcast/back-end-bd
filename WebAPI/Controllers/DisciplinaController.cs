using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.AcessoBanco;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class DisciplinaController : Controller
    {
        private readonly IRepositorio _repositorio;

        public DisciplinaController(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosDisciplinas()
        {
            try
            {
                return Ok(await _repositorio.GetTodasDisciplinas());
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error => {e.Message}");
                return BadRequest(e.Message);
            }
        }
    }
}