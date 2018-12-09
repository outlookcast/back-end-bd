using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Modelos
{
    public class Turma
    {
        public int Id { get; set; }
        public char Letra { get; set; }
        public string Observacao { get; set; }
        public int NumeroDeVagas { get; set; }
        public string Semestre { get; set; }
        public string Disciplina { get; set; }
        public string Professor { get; set; }
        public int VagasOcupadas { get; set; }
    }
}
