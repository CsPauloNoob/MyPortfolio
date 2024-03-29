﻿using CurriculumWebAPI.Domain.Exceptions;
using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models.CurriculumBody;

namespace CurriculumWebAPI.Domain.Services
{
    public class FormacaoService
    {
        private readonly IRepositoryForCollections<Formacao> _repository;

        public FormacaoService(IRepositoryForCollections<Formacao> repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddFormacao(Formacao[] formacoes, string email)
        {
            var curriculumId = await _repository.GetCurriculumId(email);
            int result = 0;

            if (curriculumId is null)
                throw new DirectoryNotFoundException("Curriculo não encontrado no banco");

            foreach (var item in formacoes)
            {
                item.CurriculumId = curriculumId;
                result += await _repository.AddNew(item);
            }

            if (result == formacoes.Count())
                return true;

            return false;
        }

        public async Task<List<Formacao>> GetAllByEmail(string email)
        {
            var curricculumId = await _repository.GetCurriculumId(email);

            if (curricculumId is null)
                throw new NotFoundInDatabaseException("Id do curriculo não encontrado no banco");

            var formationList = await _repository.GetAllByCurriculumId(curricculumId);

            if(formationList.Count == 0 )
                throw new NotFoundInDatabaseException("ítens não encontrados no banco de dados");

            return formationList;
        }


        public async Task<Formacao> UpdateFormacao(int id, Formacao formacao, string email)
        {
            var curriculumId = await _repository.GetCurriculumId(email);
            var dbFormacao = await _repository.GetById(id);

            if (dbFormacao is null)
                throw new NotFoundInDatabaseException("Objeto não encontrado no banco de dados!");

            else if (dbFormacao.CurriculumId != curriculumId)
                throw new NotFoundInDatabaseException("id incorreto ou usuário não cadastrou um curriculo");

            #region Validações ruins

            if (dbFormacao.Instituicao != formacao.Instituicao)
                dbFormacao.Instituicao = formacao.Instituicao;

            if(dbFormacao.Curso != formacao.Curso)
                dbFormacao.Curso = formacao.Curso;

            if(dbFormacao.AnoConclusao != formacao.AnoConclusao)
                dbFormacao.AnoConclusao = formacao.AnoConclusao;

            #endregion

            if(await _repository.Update(dbFormacao))
                return dbFormacao;
            else
               throw new SaveFailedException("Erro ao salvar módulo de formação no banco!");
        }

        //PROBLEMA DE SEGURANÇA
        public async Task<bool> DeleteFormacao(int id, string email)
        {
            var curriculumId = await _repository.GetCurriculumId(email);

            var existingFormacao = await _repository.GetById(id);

            if (existingFormacao is null || curriculumId != existingFormacao.CurriculumId)
                throw new NotFoundInDatabaseException("Objeto não encontrado no banco");

            var result =await _repository.DeleteByItem(existingFormacao);

            if (result > 0)
                return true;

            else
                throw new SaveFailedException("Não foi possível salvar as alterações no banco");
        }
        

        public async Task<bool> DeleteAll(string email)
        {
            var formacaoList = await GetAllByEmail(email);

            var result = await _repository.DeleteAllItems(formacaoList.ToArray());

            if (result < 1)
                throw new SaveFailedException("Não foi possível salvar as alterações no banco");

            return true;
        }

    }
}