using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace ArquivoSilvaMagalhaes.ViewModels
{
    public class TranslatedViewModel<TEntity, TTranslation> where TTranslation : EntityTranslation
    {
        public TranslatedViewModel(TEntity entity)
        {
            this.Entity = entity;

            var type = typeof(TEntity);

            var translations = entity.GetType()
                                     .GetProperty("Translations")
                                     .GetValue(entity) as IList<TTranslation>;

            var code = Thread.CurrentThread.CurrentUICulture.Name;

            var translation = translations.FirstOrDefault(t => t.LanguageCode == code);

            if (translation == null)
            {
                translation = translations.FirstOrDefault(t => t.LanguageCode == LanguageDefinitions.DefaultLanguage);
            }

            this.Translation = translation;
        }


        public TEntity Entity { get; set; }
        public TTranslation Translation { get; set; }
    }
}