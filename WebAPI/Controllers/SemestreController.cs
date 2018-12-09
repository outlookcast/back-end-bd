using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.AcessoBanco;
using WebAPI.Modelos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class SemestreController : Controller
    {
        private readonly IRepositorio _repositorio;

        public SemestreController(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosSemestres()
        {
            try
            {
                return Ok(await _repositorio.GetTodosSemestres());
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error => {e.Message}");
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CriarSemestre([FromBody]Semestre semestre)
        {
            try
            {
                return Ok(await _repositorio.CriarSemestre(semestre));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error => {e.Message}");
                return BadRequest(e.Message);
            }
        }
    }
}