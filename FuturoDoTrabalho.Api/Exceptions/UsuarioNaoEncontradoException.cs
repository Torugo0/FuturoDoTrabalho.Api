using FuturoDoTrabalho.Api.Exceptions;

namespace FuturoDoTrabalho.Api.Exceptions
{
    public class UsuarioNaoEncontradoException : DomainException
    {
        public UsuarioNaoEncontradoException(long id)
            : base($"Usuário com id {id} não foi encontrado.")
        {
        }
    }
}