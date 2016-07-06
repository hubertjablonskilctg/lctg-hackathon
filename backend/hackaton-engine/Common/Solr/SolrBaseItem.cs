using SolrNet.Attributes;

namespace Common.Solr
{
    public class SolrBaseItem
    {
        [SolrUniqueKey("id")]
        public string Id { get; set; }

        [SolrField("title")]
        public string Title { get; set; }

        [SolrField("body")]
        public string Body { get; set; }
    }
}