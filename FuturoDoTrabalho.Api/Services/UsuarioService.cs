using FuturoDoTrabalho.Api.Domain.Entities;
using FuturoDoTrabalho.Api.DTOs;
using FuturoDoTrabalho.Api.Exceptions;
using FuturoDoTrabalho.Api.Repositories;

namespace FuturoDoTrabalho.Api.Services
{

    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITrilhaRepository _trilhaRepository;
        private readonly IMatriculaRepository _matriculaRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, ITrilhaRepository trilhaRepository, IMatriculaRepository matriculaRepository)
        {
            _usuarioRepository = usuarioRepository;
            _trilhaRepository = trilhaRepository;
            _matriculaRepository = matriculaRepository;

        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();

            return usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email,
                AreaAtuacao = u.AreaAtuacao,
                NivelCarreira = u.NivelCarreira,
                DataCadastro = u.DataCadastro
            });
        }

        public async Task<UsuarioDto?> GetByIdAsync(long id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return null;

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                AreaAtuacao = usuario.AreaAtuacao,
                NivelCarreira = usuario.NivelCarreira,
                DataCadastro = usuario.DataCadastro
            };
        }

        public async Task<UsuarioDto> CreateAsync(UsuarioCreateUpdateDto dto)
        {
            var existing = await _usuarioRepository.GetByEmailAsync(dto.Email);
            if (existing != null)
            {
                throw new DomainException("Já existe um usuário cadastrado com esse email.");
            }

            var entity = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                AreaAtuacao = dto.AreaAtuacao,
                NivelCarreira = dto.NivelCarreira,
                DataCadastro = DateTime.UtcNow
            };

            await _usuarioRepository.AddAsync(entity);

            return new UsuarioDto
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Email = entity.Email,
                AreaAtuacao = entity.AreaAtuacao,
                NivelCarreira = entity.NivelCarreira,
                DataCadastro = entity.DataCadastro
            };
        }

        public async Task<UsuarioDto?> UpdateAsync(long id, UsuarioCreateUpdateDto dto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return null;

            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;
            usuario.AreaAtuacao = dto.AreaAtuacao;
            usuario.NivelCarreira = dto.NivelCarreira;

            await _usuarioRepository.UpdateAsync(usuario);

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                AreaAtuacao = usuario.AreaAtuacao,
                NivelCarreira = usuario.NivelCarreira,
                DataCadastro = usuario.DataCadastro
            };
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return false;

            await _usuarioRepository.DeleteAsync(usuario);
            return true;
        }
        public async Task<IEnumerable<TrilhaDto>> GetTrilhasDoUsuarioAsync(long usuarioId)
        {
            var trilhas = await _usuarioRepository.GetTrilhasByUsuarioIdAsync(usuarioId);
            return trilhas.Select(t => new TrilhaDto
            {
                Id = t.Id,
                Nome = t.Nome,
                Descricao = t.Descricao,
                Nivel = t.Nivel,
                CargaHoraria = t.CargaHoraria,
                FocoPrincipal = t.FocoPrincipal
            });
        }
        public async Task MatricularAsync(long usuarioId, long trilhaId)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (usuario is null)
                throw new UsuarioNaoEncontradoException(usuarioId);

            var trilha = await _trilhaRepository.GetByIdAsync(trilhaId);
            if (trilha is null)
                throw new TrilhaNaoEncontradaException(trilhaId); // você já tem essa exception

            var jaExiste = await _matriculaRepository.JaExisteAsync(usuarioId, trilhaId);
            if (jaExiste)
                throw new UsuarioJaMatriculadoException(usuarioId, trilhaId);

            var matricula = new Matricula
            {
                UsuarioId = usuarioId,
                TrilhaId = trilhaId,
                DataMatricula = DateTime.UtcNow,
                Status = "ATIVA"
            };

            await _matriculaRepository.AddAsync(matricula);
        }
        public async Task RemoverTrilhaAsync(long usuarioId, long trilhaId)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (usuario is null)
                throw new UsuarioNaoEncontradoException(usuarioId);

            var trilha = await _trilhaRepository.GetByIdAsync(trilhaId);
            if (trilha is null)
                throw new TrilhaNaoEncontradaException(trilhaId);

            var matricula = await _matriculaRepository.GetAsync(usuarioId, trilhaId);
            if (matricula is null)
                throw new DomainException("Usuário não está matriculado nessa trilha.");

            await _matriculaRepository.RemoveAsync(matricula);
        }


    }
}