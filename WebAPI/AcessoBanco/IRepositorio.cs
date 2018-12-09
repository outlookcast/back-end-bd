using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Modelos;
namespace WebAPI.AcessoBanco
{
    public interface IRepositorio
    {
        Task<IEnumerable<Aluno>> GetTodosAlunos();
        Task<IEnumerable<Disciplina>> GetTodasDisciplinas();
    }
}
