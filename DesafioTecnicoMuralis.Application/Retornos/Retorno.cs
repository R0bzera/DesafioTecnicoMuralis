using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTecnicoMuralis.Application.Retornos
{
    public class Retorno<T>
    {
        public bool Sucesso { get; private set; }
        public string? Mensagem { get; private set; }
        public T? Dados { get; private set; }

        private Retorno(bool sucesso, string? mensagem, T? dados)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;
        }

        public static Retorno<T> Ok(T dados, string? mensagem = null)
            => new Retorno<T>(true, mensagem, dados);

        public static Retorno<T> Falha(string mensagem)
            => new Retorno<T>(false, mensagem, default);
    }
}
