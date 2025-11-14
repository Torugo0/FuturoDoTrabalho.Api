namespace FuturoDoTrabalho.Api.Exceptions
{
    public class TrilhaNaoEncontradaException : DomainException
    {
        public TrilhaNaoEncontradaException(long id)
            : base($"Trilha com id {id} não encontrada.")
        {
        }
    }
}