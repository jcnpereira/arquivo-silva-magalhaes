using ArquivoSilvaMagalhaes.Models.SiteModels;
using System.Collections.Generic;

namespace ArquivoSilvaMagalhaes.ViewModels
{
    /// <summary>
    /// ViewModel que contem atributos do model banner utilizados na página de indexView do arquivo
    /// </summary>
    public class IndexViewModel
    {
        public IList<TranslatedViewModel<Banner, BannerTranslation>> Banners { get; set; }

        public string VideoId { get; set; }

    }
}