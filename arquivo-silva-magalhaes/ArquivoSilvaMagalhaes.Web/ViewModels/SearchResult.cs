using System.Collections.Generic;

namespace ArquivoSilvaMagalhaes.ViewModels
{
    public class SearchResultItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class SearchResult
    {
        public string Query { get; set; }
        public IEnumerable<SearchResultItem> Results { get; set; }
    }
}