using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ertis.Core.Collections;
using Ertis.MongoDB.Queries;
using Ertis.MongoDB.Repository;
using Ertis.Schema.Dynamics;
using ErtisAuth.Abstractions.Services.Interfaces;
using MongoDB.Bson;

namespace ErtisAuth.Infrastructure.Services
{
    public abstract class DynamicObjectCrudService : IDynamicObjectCrudService
    {
        #region Services

        private readonly IDynamicMongoRepository _repository;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        protected DynamicObjectCrudService(IDynamicMongoRepository repository)
        {
            this._repository = repository;
        }

        #endregion
        
        #region Read Methods

        public virtual async Task<DynamicObject> GetAsync(string id)
        {
            var item = await this._repository.FindOneAsync(id);
            return item == null ? null : new DynamicObject(item);
        }

        public virtual async Task<DynamicObject> FindOneAsync(params IQuery[] queries)
        {
            var query = QueryBuilder.Where(queries);
            var matches = await this._repository.FindAsync(query.ToString());
            var item = matches.Items.FirstOrDefault();
            return item == null ? null : new DynamicObject(item);
        }

        public virtual async Task<IPaginationCollection<DynamicObject>> GetAsync(
            IEnumerable<IQuery> queries,
            int? skip = null, 
            int? limit = null, 
            bool withCount = false, 
            string orderBy = null,
            SortDirection? sortDirection = null)
        {
            var query = QueryBuilder.Where(queries);
            var paginatedCollection = await this._repository.FindAsync(query.ToString(), skip, limit, withCount, orderBy, sortDirection);
            return new PaginationCollection<DynamicObject>
            {
                Count = paginatedCollection.Count,
                Items = paginatedCollection.Items.Select(x => new DynamicObject(x))
            };
        }

        public virtual async Task<IPaginationCollection<DynamicObject>> QueryAsync(
            string query, 
            int? skip = null, 
            int? limit = null, 
            bool? withCount = null, 
            string orderBy = null,
            SortDirection? sortDirection = null, 
            IDictionary<string, bool> selectFields = null)
        {
            var paginatedCollection = await this._repository.QueryAsync(query, skip, limit, withCount, orderBy, sortDirection, selectFields);
            return new PaginationCollection<DynamicObject>
            {
                Count = paginatedCollection.Count,
                Items = paginatedCollection.Items.Select(x => new DynamicObject(x))
            };
        }
        
        #endregion
        
        #region Crate Methods
        
        public virtual async Task<DynamicObject> CreateAsync(DynamicObject model)
        {
            var bsonDocument = BsonDocument.Create(model.ToDynamic());
            var insertedDocument = await this._repository.InsertAsync(bsonDocument) as BsonDocument;
            return DynamicObject.Create(BsonTypeMapper.MapToDotNetValue(insertedDocument) as Dictionary<string, object>);
        }

        #endregion
        
        #region Update Methods

        public virtual async Task<DynamicObject> UpdateAsync(string id, DynamicObject model)
        {
            var bsonDocument = BsonDocument.Create(model.ToDynamic());
            var updatedDocument = await this._repository.UpdateAsync(bsonDocument, id);
            return updatedDocument != null ? await this.GetAsync(id) : null;
        }

        #endregion
        
        #region Delete Methods

        public virtual async Task<bool> DeleteAsync(string id)
        {
            var isDeleted = await this._repository.DeleteAsync(id);
            return isDeleted;
        }

        #endregion
    }
}