using FuturoDoTrabalho.Api.Domain.Entities;
using FuturoDoTrabalho.Api.DTOs;
using FuturoDoTrabalho.Api.Exceptions;
using FuturoDoTrabalho.Api.Repositories;

namespace FuturoDoTrabalho.Api.Services
{
    public class TrilhaService : ITrilhaService
    {
        private readonly ITrilhaRepository _trilhaRepository;

        public TrilhaService(ITrilhaRepository trilhaRepository)
        {
            _trilhaRepository = trilhaRepository;
        }

        public async Task<IEnumerable<TrilhaDto>> GetAllAsync()
        {
            var trilhas = await _trilhaRepository.GetAllAsync();

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

        public async Task<TrilhaDto?> GetByIdAsync(long id)
        {
            var trilha = await _trilhaRepository.GetByIdAsync(id);
            if (trilha == null) return null;

            return new TrilhaDto
            {
                Id = trilha.Id,
                Nome = trilha.Nome,
                Descricao = trilha.Descricao,
                Nivel = trilha.Nivel,
                CargaHoraria = trilha.CargaHoraria,
                FocoPrincipal = trilha.FocoPrincipal
            };
        }

        public async Task<TrilhaDto> CreateAsync(TrilhaCreateUpdateDto dto)
        {
            var entity = new Trilha
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Nivel = dto.Nivel,
                CargaHoraria = dto.CargaHoraria,
                FocoPrincipal = dto.FocoPrincipal
            };

            await _trilhaRepository.AddAsync(entity);

            return new TrilhaDto
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Descricao = entity.Descricao,
                Nivel = entity.Nivel,
                CargaHoraria = entity.CargaHoraria,
                FocoPrincipal = entity.FocoPrincipal
            };
        }

        public async Task<TrilhaDto?> UpdateAsync(long id, TrilhaCreateUpdateDto dto)
        {
            var trilha = await _trilhaRepository.GetByIdAsync(id);
            if (trilha == null) return null;

            trilha.Nome = dto.Nome;
            trilha.Descricao = dto.Descricao;
            trilha.Nivel = dto.Nivel;
            trilha.CargaHoraria = dto.CargaHoraria;
            trilha.FocoPrincipal = dto.FocoPrincipal;

            await _trilhaRepository.UpdateAsync(trilha);

            return new TrilhaDto
            {
                Id = trilha.Id,
                Nome = trilha.Nome,
                Descricao = trilha.Descricao,
                Nivel = trilha.Nivel,
                CargaHoraria = trilha.CargaHoraria,
                FocoPrincipal = trilha.FocoPrincipal
            };
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var trilha = await _trilhaRepository.GetByIdAsync(id);
            if (trilha == null) return false;

            await _trilhaRepository.DeleteAsync(trilha);
            return true;
        }
    }
}