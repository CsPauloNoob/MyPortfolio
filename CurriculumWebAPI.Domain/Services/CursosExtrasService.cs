﻿using CurriculumWebAPI.Domain.Exceptions;
using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Services
{
    public class CursosExtrasService
    {
        private readonly IRepositoryForCollections<Cursos_Extras> _repository;

        public CursosExtrasService(IRepositoryForCollections<Cursos_Extras> repository)
        {
            _repository = repository; 
        }



        public async Task<bool> AddCursosExtras(Cursos_Extras[] cursosExtras, string email)
        {
            var curriculumId = await _repository.GetCurriculumId(email);
            int result = 0;

            if (curriculumId is null)
                throw new DirectoryNotFoundException("Curriculo não encontrado no banco");

            foreach (var item in cursosExtras)
            {
                item.CurriculumId = curriculumId;
                result += await _repository.AddNew(item);
            }

            if (result == cursosExtras.Count())
                return true;

            return false;
        }


        public async Task<List<Cursos_Extras>> GetAllByEmail(string email)
        {
            var curricculumId = await _repository.GetCurriculumId(email);

            if (curricculumId is null)
                throw new NotFoundInDatabaseException("Id do curriculo não encontrado no banco");

            var cursosExtrasList = await _repository.GetAllByCurriculumId(curricculumId);

            if (cursosExtrasList.Count == 0)
                throw new NotFoundInDatabaseException("ítens não encontrados no banco de dados");

            return cursosExtrasList;
        }


        public async Task<Cursos_Extras> GetById(int id, string email)
        {
            var cursoExtra = await _repository.GetById(id);

            if (cursoExtra is null)
                throw new NotFoundInDatabaseException("id não corresponde a nenhum objeto no banco");

            var curriculumId = await _repository.GetCurriculumId(email);

            if (curriculumId == cursoExtra.CurriculumId)
                return cursoExtra;

            else throw new NotFoundInDatabaseException("Id incorreto");
        }


        public async Task<bool> UpdateById(int id, string email, Cursos_Extras cursoExtra)
        {
            var curriculumId = await _repository.GetCurriculumId(email);
            var existingCursoExtra = await _repository.GetById(id);

            if (existingCursoExtra is null)
                throw new NotFoundInDatabaseException("Objeto não encontrado no banco de dados!");

            else if (existingCursoExtra.CurriculumId != curriculumId)
                throw new NotFoundInDatabaseException("id incorreto ou usuário não cadastrou um curriculo");

            #region Validações ruins

            if (existingCursoExtra.Nome_Curso != cursoExtra.Nome_Curso)
                existingCursoExtra.Nome_Curso = cursoExtra.Nome_Curso;

            if (existingCursoExtra.Organizacao != cursoExtra.Organizacao)
                existingCursoExtra.Organizacao = cursoExtra.Organizacao;

            #endregion

            if (await _repository.Update(existingCursoExtra))
                return true;

            else
                throw new SaveFailedException("Erro ao salvar módulo de formação no banco!");
        }


        public async Task<bool> DeleteAllItems(string email)
        {
            var cursosExtras = await GetAllByEmail(email);

            var result = await _repository.DeleteAllItems(cursosExtras.ToArray());

            if (result < 1)
                throw new SaveFailedException("Não foi possível salvar as alterações no banco");

            else return true;
        }


        public async Task<bool> DeleteById(int id, string email)
        {
            var curriculumId = await _repository.GetCurriculumId(email);

            var existingCursoExtra = await _repository.GetById(id);

            if (existingCursoExtra is null || curriculumId != existingCursoExtra.CurriculumId)
                throw new NotFoundInDatabaseException("Objeto não encontrado no banco");

            var result = await _repository.DeleteByItem(existingCursoExtra);

            if (result > 0)
                return true;

            else
                throw new SaveFailedException("Não foi possível salvar as alterações no banco");
        }
    }
}