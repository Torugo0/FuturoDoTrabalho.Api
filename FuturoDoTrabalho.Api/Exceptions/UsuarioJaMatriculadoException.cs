namespace FuturoDoTrabalho.Api.Exceptions
{
    public class UsuarioJaMatriculadoException : DomainException
    {
        public UsuarioJaMatriculadoException(long usuarioId, long trilhaId)
            : base($"Usuário {usuarioId} já está matriculado na trilha {trilhaId}.")
        {
        }
    }
}