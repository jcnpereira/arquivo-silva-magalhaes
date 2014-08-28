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
    /// <summary>
    /// View model that automatically chooses the most appropriate
    /// translation for a translateable entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity that is translateable.</typeparam>
    /// <typeparam name="TTranslation">The type of the translation.</typeparam>
    public class TranslatedViewModel<TEntity, TTranslation> 
        where TTranslation : EntityTranslation
    {
        public TranslatedViewModel(TEntity entity)
        {
            this.Entity = entity;

            var type = typeof(TEntity);

            // Get the translations.
            // TODO: Probably use interfaces.
            var translations = entity.GetType()
                                     .GetProperty("Translations")
                                     .GetValue(entity) as IList<TTranslation>;

            // Get the current language code.
            var code = Thread.CurrentThread.CurrentUICulture.Name;

            // Try to get a translation for the current language.
            var translation = translations.FirstOrDefault(t => t.LanguageCode == code);

            // If none exists, get the default translation.
            if (translation == null)
            {
                translation = translations.FirstOrDefault(t => t.LanguageCode == LanguageDefinitions.DefaultLanguage);
            }

            // If (for some reason!) no translation was found, throw an error.
            if (translation == null)
            {
                throw new InvalidOperationException("Unable to find a translation.");
            }

            this.Translation = translation;
        }

        /// <summary>
        /// Non-translateable fields.
        /// </summary>
        public TEntity Entity { get; set; }

        /// <summary>
        /// Translated fields.
        /// </summary>
        public TTranslation Translation { get; set; }
    }
}