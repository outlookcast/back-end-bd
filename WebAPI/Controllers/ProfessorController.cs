using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.AcessoBanco;
using WebAPI.Modelos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProfessorController : Controller
    {
        private readonly IRepositorio _repositorio;

        public ProfessorController(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosProfessores()
        {
            try
            {
                return Ok(await _repositorio.GetTodosProfessores());
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error => {e.Message}");
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarProfessor([FromBody]Professor professor)
        {
            try
            {
                return Ok(await _repositorio.CriarProfessor(professor));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error => {e.Message}");
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("departamento")]
        public async Task<IActionResult> GetNomeDosDepartamentos()
        {
            try
            {
                return Ok(await _repositorio.GetTodosNomesDosDepartamentos());
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error => {e.Message}");
                return BadRequest(e.Message);
            }
        }
    }
}