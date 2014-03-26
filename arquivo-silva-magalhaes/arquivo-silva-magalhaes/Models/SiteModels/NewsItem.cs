using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class NewsItem
    {
        public int Id { get; set; }
       
        public DateTime PublishDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool HideAfterExpiry { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }

        public virtual ICollection<ReferencedLink> Links { get; set; }
        public virtual ICollection<NewsItem> ReferencedNewsItems { get; set; }
        public int MyProperty { get; set; }
    }
}