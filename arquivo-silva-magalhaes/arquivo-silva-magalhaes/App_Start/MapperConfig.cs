using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ArquivoSilvaMagalhaes.Models.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;

namespace ArquivoSilvaMagalhaes.App_Start
{
    public class MapperConfig
    {
        public static void RegisterMappings()
        {

        }

        private static void RegisterAuthorMappings()
        {
            //Mapper.CreateMap<Author, AuthorEditViewModel>()
            //      .ConvertUsing(a => new AuthorEditViewModel
            //      {
            //          Id = a.Id,
            //          FirstName = a.FirstName,
            //          LastName = a.LastName,
            //          BirthDate = a.BirthDate,
            //          Translations = a.Translations.Select(at => new AuthorTranslationEditViewModel
            //          {
            //              AuthorId = a.Id,
            //              LanguageCode = at.LanguageCode,
            //              Nationality = at.Nationality,
            //              Biography = at.Biography,
            //              Curriculum = at.Curriculum
            //          }).ToList()
            //      });
        }
    }
}