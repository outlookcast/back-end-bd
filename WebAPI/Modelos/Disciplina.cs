using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Modelos
{
    public class Disciplina
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Ementa { get; set; }
        public string Coordenador { get; set; }
        public string Departamento { get; set; }
        public IEnumerable<Turma> Turmas { get; set; }
    }
}
