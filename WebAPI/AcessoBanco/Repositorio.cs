using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Modelos;
using Npgsql;
using Dapper;
namespace WebAPI.AcessoBanco
{
    public class Repositorio : IRepositorio
    {
        private readonly string _connectionString;
        private readonly IConfiguration _config;

        public Repositorio(IConfiguration config)
        {
            _config = config;
            _connectionString = _config["ConnectionString"];
        }

        public async Task<IEnumerable<Disciplina>> GetTodasDisciplinas()
        {
            using (var conexao = new NpgsqlConnection(_connectionString))
            {
                string query = @"SELECT
                                    d.id_disciplina AS Id,
                                    d.nome_disciplina AS Nome,
                                    d.ementa AS Ementa,
                                    (SELECT p.nome_professor FROM trabalho_bd_vsampaio.professor p WHERE p.id_professor = d.id_coornedador) AS Coordenador,
                                    (SELECT dd.nome_departamento FROM trabalho_bd_vsampaio.departamento dd WHERE d.id_departamento = dd.id_departamento) AS Departamento
                                FROM
                                    trabalho_bd_vsampaio.disciplina d;";
                var disciplinas = await conexao.QueryAsync<Disciplina>(query);
                string queryEmTurmas = @"SELECT
                                            t.id_turma AS Id,
                                            t.letra AS Letra,
                                            t.observacao AS Observacao,
                                            t.numero_de_vagas AS NumeroDeVagas,
                                            (SELECT s.nome_semestre FROM trabalho_bd_vsampaio.semestre s WHERE s.id_semestre = t.id_semestre) AS Semestre,
                                            (SELECT d.nome_disciplina FROM trabalho_bd_vsampaio.disciplina d WHERE d.id_disciplina = t.id_disciplina) AS Disciplina,
                                            (SELECT p.nome_professor FROM trabalho_bd_vsampaio.professor p WHERE p.id_professor = t.id_professor) AS Professor,
                                            t.vagas_ocupadas AS VagasOcupadas
                                        FROM
                                            trabalho_bd_vsampaio.turma t
                                        WHERE
                                            t.id_disciplina = @Id;";
                foreach (var disciplina in disciplinas)
                {
                    disciplina.Turmas = await conexao.QueryAsync<Turma>(queryEmTurmas, new { disciplina.Id });
                }
                return disciplinas;
            }
        }

        public async Task<IEnumerable<Aluno>> GetTodosAlunos()
        {
            using (var conexao = new NpgsqlConnection(_connectionString))
            {
                string query = @"SELECT
                                    id_aluno AS Id,
                                    matricula AS Matricula,
                                    nome_aluno AS Nome,
                                    cpf AS CPF,
                                    curso AS Curso
                                FROM
                                    trabalho_bd_vsampaio.aluno;";
                return await conexao.QueryAsync<Aluno>(query);
            }
        }

        public async Task<IEnumerable<Semestre>> GetTodosSemestres()
        {
            using (var conexao = new NpgsqlConnection(_connectionString))
            {
                string query = @"SELECT
                                    id_semestre AS Id,
                                    nome_semestre AS Nome,
                                    data_inicial AS DataInicial,
                                    data_final AS DataFinal
                                FROM
                                    trabalho_bd_vsampaio.semestre;";
                return await conexao.QueryAsync<Semestre>(query);
            }
        }

        public async Task<bool> DeletarSemestrePeloId(int id)
        {
            //TODO: Deletar referência em todas as outras tabelas antes...
            using (var conexao = new NpgsqlConnection(_connectionString))
            {
                string query = @"DELETE
                                FROM
                                    trabalho_bd_vsampaio.semestre s
                                WHERE
                                    s.id_semestre = @id";
                return 1 == (await conexao.ExecuteAsync(query, new { id }));
            }
        }

        public async Task<bool> CriarSemestre(Semestre semestre)
        {
            using (var conexao = new NpgsqlConnection(_connectionString))
            {
                string query = @"INSERT INTO trabalho_bd_vsampaio.semestre (
                                    id_semestre,
                                    nome_semestre,
                                    data_inicial,
                                    data_final)
                                VALUES (
                                    @Id,
                                    @Nome,
                                    @DataInicial,
                                    @DataFinal);";
                return 1 == (await conexao.ExecuteAsync(query, new { semestre.Id, semestre.Nome, semestre.DataInicial, semestre.DataFinal }));
            }
        }

        public async Task<IEnumerable<Professor>> GetTodosProfessores()
        {
            using (var conexao = new NpgsqlConnection(_connectionString))
            {
                string query = @"SELECT
                                    id_professor AS Id,
                                    nome_professor AS Nome,
                                    cpf AS CPF,
                                    siape AS Siape,
                                    (SELECT nome_departamento FROM trabalho_bd_vsampaio.departamento d WHERE d.id_departamento = p.id_departamento) AS Departamento
                                FROM
                                    trabalho_bd_vsampaio.professor p;";
                return await conexao.QueryAsync<Professor>(query);
            }
        }

        public async Task<bool> CriarProfessor(Professor professor)
        {
            using (var conexao = new NpgsqlConnection(_connectionString))
            {
                string query = @"INSERT INTO trabalho_bd_vsampaio.professor (
                                    id_professor,
                                    nome_professor,
                                    siape,
                                    cpf,
                                    id_departamento)
                                VALUES (
                                    @Id,
                                    @Nome,
                                    @Siape,
                                    @CPF,
                                    (SELECT d.id_departamento FROM trabalho_bd_vsampaio.departamento d WHERE d.nome_departamento = @Departamento));";
                return 1 == (await conexao.ExecuteAsync(query, professor));
            }
        }
    }
}
