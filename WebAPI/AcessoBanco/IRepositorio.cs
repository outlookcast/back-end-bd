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
        Task<IEnumerable<Semestre>> GetTodosSemestres();
        Task<bool> DeletarSemestrePeloId(int id);
        Task<bool> CriarSemestre(Semestre semestre);
        Task<IEnumerable<Professor>> GetTodosProfessores();
        Task<bool> CriarProfessor(Professor professor);
        Task<bool> CriarAluno(Aluno aluno);
        Task<IEnumerable<string>> GetTodosNomesDosDepartamentos();
    }
}
