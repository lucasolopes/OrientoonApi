using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Repositories;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;
using OrientoonApi.Services.Interfaces;
using System.Globalization;

namespace OrientoonApi.Services.Implementations
{
    public class OrientoonService : IOrientoonService
    {
        private readonly IOrientoonRepository _orientoonRepository;
        private readonly IArtistaRepository _artistaRepository;
        private readonly IAutorRepository _autorRepository;
        private readonly IContextRepository _contextRepository;

        public OrientoonService(IOrientoonRepository orientoonRepository, IArtistaRepository artistaRepository, IAutorRepository autorRepository, IContextRepository contextRepository)
        {
            _orientoonRepository = orientoonRepository;
            _artistaRepository = artistaRepository;
            _autorRepository = autorRepository;
            _contextRepository = contextRepository;
        }

        public async Task<OrientoonForm> CreateAsync(OrientoonDto orientoonDto)
        {
                OrientoonModel orientoonModel = orientoonDto.Converter();

                //orientoonModel id randon text somente letras e numeros
                orientoonModel.Id = Guid.NewGuid().ToString("N");

                //implmentar na propria controller depois
                orientoonModel.CBanner = "implementar depois a funcao";

                ArtistaModel artista = await _artistaRepository.FindByNomeAsync(orientoonDto.NomeArtista);
                if (artista == null)
                {
                    throw new Exception("Artista não encontrado");
                }
                AutorModel autor = await _autorRepository.FindByNomeAsync(orientoonDto.NomeAutor);
                if (autor == null)
                {
                    // return NotFound("Autor não encontrado");
                    throw new Exception("Autor não encontrado");
                }

                orientoonModel.Artista = artista;
                orientoonModel.Autor = autor;

                await _orientoonRepository.AddAsync(orientoonModel);
                await _contextRepository.SaveChangesAsync();
                //criar um response depois e retornar ele
                return orientoonModel.Converter();

        }



        public async Task<OrientoonForm> GetAsync(string id)
        {
            OrientoonModel orientoonModel = await _orientoonRepository.FindByIdAsync(id);

            if (orientoonModel == null)
            {
                return null;
            }
            orientoonModel.Artista = await _artistaRepository.FindByIdAsync(orientoonModel.ArtistaId);
            orientoonModel.Autor = await _autorRepository.FindByIdAsync(orientoonModel.AutorId);

            return orientoonModel.Converter();
        }

        public async Task<List<OrientoonForm>> GetListAsync(int batchSize, int pageNumber)
        {
            List<OrientoonModel> orientoonModel = await _orientoonRepository.GetByAmountAsync(batchSize,pageNumber);
            //converter orientoonModel para orientoonForm
            List<OrientoonForm> orientoonForm = new List<OrientoonForm>();
            foreach (var orientoon in orientoonModel)
            {
                orientoon.Artista = await _artistaRepository.FindByIdAsync(orientoon.ArtistaId);
                orientoon.Autor = await _autorRepository.FindByIdAsync(orientoon.AutorId);
                orientoonForm.Add(orientoon.Converter());
            }
            return orientoonForm;
        }

        public Task CreateListAsync(List<OrientoonDto> orientoon)
        {
            foreach (var orientoonDto in orientoon)
            {
                 CreateAsync(orientoonDto);
            } 
            return Task.CompletedTask;
        }

        public async Task<OrientoonForm> UpdateAsync(string id, OrientoonPutDto orientoon)
        {

            OrientoonModel oldOrientoon = await _orientoonRepository.FindByIdAsync(id);

            OrientoonModel newOrientoon = orientoon.Converter();

            if (oldOrientoon == null)
            {
                return null;
            }

            ArtistaModel oldArtista = await _artistaRepository.FindByIdAsync(oldOrientoon.ArtistaId);
            AutorModel oldAutor = await _autorRepository.FindByIdAsync(oldOrientoon.AutorId);


            if (orientoon.NomeAutor != "")
            {
                AutorModel newAutor = await _autorRepository.FindByNomeAsync(orientoon.NomeAutor);
                if (newAutor == null)
                {
                    throw new Exception("Autor não encontrado");
                }
            }
            
            if(orientoon.NomeArtista != "")
            {
                ArtistaModel newArtista = await _artistaRepository.FindByNomeAsync(orientoon.NomeArtista);
                if (newArtista == null)
                {
                    throw new Exception("Artista não encontrado");
                }
            }

            string newNomeAutor = orientoon.NomeAutor;
            string newNomeArtista = orientoon.NomeArtista;


            //verifique se os valores são diferentes e nao nulos e atualize
            if (newOrientoon.Titulo != "" && newOrientoon.Titulo != oldOrientoon.Titulo)
            {
                oldOrientoon.Titulo = newOrientoon.Titulo;
            }
            if (newOrientoon.Descricao != "" && newOrientoon.Descricao != oldOrientoon.Descricao)
            {
                oldOrientoon.Descricao = newOrientoon.Descricao;
            }
            if (newOrientoon.DataLancamento != null && newOrientoon.DataLancamento != oldOrientoon.DataLancamento)
            {
                oldOrientoon.DataLancamento = newOrientoon.DataLancamento;
            }
            if (newNomeArtista != "" && newNomeArtista != oldArtista.NomeArtista)
            {
                oldArtista.NomeArtista = newNomeArtista;
            }
            if (newNomeAutor != "" && newNomeAutor != oldAutor.NomeAutor)
            {
                oldAutor.NomeAutor = newNomeAutor;
            }
            if (newOrientoon.Status != "" && newOrientoon.Status != oldOrientoon.Status)
            {
                oldOrientoon.Status = newOrientoon.Status;
            }

            await _orientoonRepository.UpdateAsync(oldOrientoon);
            await _contextRepository.SaveChangesAsync();
            return oldOrientoon.Converter();
        }

        public async Task DeleteAsync(string id)
        {
            await _orientoonRepository.DeleteAsync(id);
            await _contextRepository.SaveChangesAsync();
        }
    }
}
