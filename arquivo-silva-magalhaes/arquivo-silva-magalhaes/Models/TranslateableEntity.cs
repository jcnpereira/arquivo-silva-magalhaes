using ArquivoSilvaMagalhaes.Utilitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ArquivoSilvaMagalhaes.Models
{
    public class EntityTranslation
    {
        public virtual string LanguageCode { get; set; }
    }

    public class TranslateableEntity<T> where T : EntityTranslation
    {
        public TranslateableEntity()
        {
            Translations = new HashSet<T>();
        }

        /// <summary>
        /// <para>
        /// Gets the translated value for the specified key,
        /// in the (optionally specified) language code.
        /// </para>
        /// <para>
        /// By default, the language code will be the current thread's
        /// language code.
        /// </para>
        /// <para>
        /// If no translation is available for the current thread's language,
        /// the default language code is then attempted.
        /// </para>
        /// <para>
        /// Null is returned if no translation is available.
        /// </para>
        /// <para>
        /// InvalidOperationException is thrown if key is null or if no
        /// property with the specified key exists in the entity.
        /// </para>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="languageCode"></param>
        /// <param name="defaultLanguageCode"></param>
        /// <returns></returns>
        public string GetTranslatedValueOrDefault(
            string key,
            string languageCode = null,
            string defaultLanguageCode = LanguageDefinitions.DefaultLanguage)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("key cannot be null or empty.");
            }

            languageCode = languageCode ?? Thread.CurrentThread.CurrentCulture.Name;

            // var db = new ArchiveDataContext();

            

            var translation = Translations.FirstOrDefault(t => t.LanguageCode == languageCode) ??
                              Translations.FirstOrDefault(t => t.LanguageCode == defaultLanguageCode);

            if (translation == null)
            {
                return null;

                //throw new Exception(String.Format(
                //    "No entry for language code '{0}' or the default, which is '{1}'.", 
                //    languageCode, defaultLanguageCode));
            }

            var prop = translation.GetType().GetProperty(key);

            if (prop == null)
            {
                throw new InvalidOperationException(String.Format("No property named '{0}' exists in this entity.", key));
            }

            return (string)prop.GetValue(translation);
        }

        /// <summary>
        /// <para>
        /// Sets a translated value to this entity, for the given
        /// language code.
        /// </para>
        /// <para>
        /// If a translation doesn't exist for the given language code,
        /// a new translation is then created and the value is assigned.
        /// </para>
        /// <para>
        /// If key is null, InvalidOperationException is thrown.
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="languageCode"></param>
        public void SetTranslatedValue(
            string key,
            string value,
            string languageCode = null
            )
        {
            languageCode = languageCode ?? Thread.CurrentThread.CurrentCulture.Name;

            if (String.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("key cannot be null or empty.");
            }

            T translation = 
                Translations.FirstOrDefault(tr => tr.LanguageCode == languageCode);


            if (translation == null)
            {
                translation = 
                    (T) typeof(T).GetConstructors()[0].Invoke(new object[] { });

                translation.LanguageCode = languageCode;

                Translations.Add(translation);
            }

            typeof(T).GetProperty(key).SetValue(translation, value);
        }

        public virtual ICollection<T> Translations { get; set; }
    }
}