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
    public class AlunoController : Controller
    {
        private readonly IRepositorio _repositorio;

        public AlunoController(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosAlunos()
        {
            try
            {
                return Ok(await _repositorio.GetTodosAlunos());
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error => {e.Message}");
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarAluno([FromBody]Aluno aluno)
        {
            try
            {
                return Ok(await _repositorio.CriarAluno(aluno));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error => {e.Message}");
                return BadRequest(e.Message);
            }
        }

    }
}
