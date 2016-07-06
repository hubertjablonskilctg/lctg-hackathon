using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using SolrNet;
using SolrNet.Commands.Parameters;
using SolrNet.Impl;
using SolrNet.Mapping.Validation;
using SolrNet.Schema;

namespace Common.Solr
{
    public class SolrConnector<T> : ISolrOperations<T> where T : SolrBaseItem
    {
        private ISolrOperations<T> _solrOperations;

        public SolrConnector(string connectionString = null)
        {
            Startup.Init<T>(connectionString ?? "http://localhost:8989/solr/test_core");
            _solrOperations = ServiceLocator.Current.GetInstance<ISolrOperations<T>>();
        }

        public ResponseHeader Commit()
        {
            return _solrOperations.Commit();
        }

        public ResponseHeader Rollback()
        {
            throw new System.NotImplementedException();
        }

        public ResponseHeader Optimize()
        {
            throw new System.NotImplementedException();
        }

        ResponseHeader ISolrOperations<T>.Add(T doc)
        {
            return _solrOperations.Add(doc);
        }

        public ResponseHeader Add(T doc, AddParameters parameters)
        {
            throw new System.NotImplementedException();
        }

        public ResponseHeader AddWithBoost(T doc, double boost)
        {
            throw new System.NotImplementedException();
        }

        public ResponseHeader AddWithBoost(T doc, double boost, AddParameters parameters)
        {
            throw new System.NotImplementedException();
        }

        public ExtractResponse Extract(ExtractParameters parameters)
        {
            throw new System.NotImplementedException();
        }

        public ResponseHeader Add(IEnumerable<T> docs)
        {
            throw new System.NotImplementedException();
        }

        public ResponseHeader AddRange(IEnumerable<T> docs)
        {
            throw new System.NotImplementedException();
        }

        public ResponseHeader Add(IEnumerable<T> docs, AddParameters parameters)
        {
            throw new System.NotImplementedException();
        }

        public ResponseHeader AddRange(IEnumerable<T> docs, AddParameters parameters)
        {
            throw new System.NotImplementedException();
        }

        public ResponseHeader AddWithBoost(IEnumerable<KeyValuePair<T, double?>> docs)
        {
            throw new System.NotImplementedException();
        }

        public ResponseHeader AddRangeWithBoost(IEnumerable<KeyValuePair<T, double?>> docs)
        {
            throw new System.NotImplementedException();
        }

        public ResponseHeader AddWithBoost(IEnumerable<KeyValuePair<T, double?>> docs, AddParameters parameters)
        {
            throw new System.NotImplementedException();
        }

        public ResponseHeader AddRangeWithBoost(IEnumerable<KeyValuePair<T, double?>> docs, AddParameters parameters)
        {
            throw new System.NotImplementedException();
        }

        ResponseHeader ISolrOperations<T>.Delete(T doc)
        {
            return _solrOperations.Delete(doc);
        }

        public ResponseHeader Delete(IEnumerable<T> docs)
        {
            throw new System.NotImplementedException();
        }

        public ResponseHeader Delete(ISolrQuery q)
        {
            throw new System.NotImplementedException();
        }

        public ResponseHeader Delete(string id)
        {
            return _solrOperations.Delete(id);
        }

        public ResponseHeader Delete(IEnumerable<string> ids)
        {
            return _solrOperations.Delete(ids);
        }

        public ResponseHeader Delete(IEnumerable<string> ids, ISolrQuery q)
        {
            throw new System.NotImplementedException();
        }

        public ResponseHeader BuildSpellCheckDictionary()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ValidationResult> EnumerateValidationResults()
        {
            throw new System.NotImplementedException();
        }

        public SolrQueryResults<T> Query(ISolrQuery query, QueryOptions options)
        {
            throw new System.NotImplementedException();
        }

        public SolrMoreLikeThisHandlerResults<T> MoreLikeThis(SolrMLTQuery query, MoreLikeThisHandlerQueryOptions options)
        {
            throw new System.NotImplementedException();
        }

        public ResponseHeader Ping()
        {
            throw new System.NotImplementedException();
        }

        public SolrSchema GetSchema()
        {
            throw new System.NotImplementedException();
        }

        public SolrDIHStatus GetDIHStatus(KeyValuePair<string, string> options)
        {
            throw new System.NotImplementedException();
        }

        public SolrQueryResults<T> Query(string q)
        {
            throw new System.NotImplementedException();
        }

        public SolrQueryResults<T> Query(string q, ICollection<SortOrder> orders)
        {
            throw new System.NotImplementedException();
        }

        public SolrQueryResults<T> Query(string q, QueryOptions options)
        {
            throw new System.NotImplementedException();
        }

        public SolrQueryResults<T> Query(ISolrQuery q)
        {
            throw new System.NotImplementedException();
        }

        public SolrQueryResults<T> Query(ISolrQuery query, ICollection<SortOrder> orders)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<KeyValuePair<string, int>> FacetFieldQuery(SolrFacetFieldQuery facets)
        {
            throw new System.NotImplementedException();
        }
    }
}