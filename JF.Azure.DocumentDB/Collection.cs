using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace JF.Azure.DocumentDB {

	/// <summary>
	///     Access wrapper for the Azure DocumentDB
	/// </summary>
	/// <typeparam name="TModel"></typeparam>
	public class Collection<TModel> : ICollection<TModel>
		where TModel : Resource , new() {

		private readonly string _collectionLink;

		private readonly DocumentClient _documentClient;

		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="documentClient"></param>
		/// <param name="documentCollection"></param>
		public Collection( DocumentClient documentClient , DocumentCollection documentCollection ) {
			_documentClient = documentClient;

			_collectionLink = documentCollection.SelfLink;
		}

		/// <summary>
		///     Creates a document query
		/// </summary>
		/// <returns></returns>
		public IOrderedQueryable<TModel> AsQueryable() {
			return _documentClient.CreateDocumentQuery<TModel>( _collectionLink );
		}

		/// <summary>
		///     Inserts or replaces an entity
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public async Task<ResourceResponse<Document>> AddOrUpdateAsync( TModel entity ) {
			var document = await FindAsync( entity.Id );

			if ( document != null ) {
				return await _documentClient.ReplaceDocumentAsync( document.SelfLink , entity );
			}

			return await _documentClient.CreateDocumentAsync( _collectionLink , entity );
		}

		/// <summary>
		///     Inserts or replaces entities
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		public async Task<IList<ResourceResponse<Document>>> AddOrUpdateAsync(
			IList<TModel> entities ) {
			var result = new List<ResourceResponse<Document>>();

			foreach ( var entity in entities ) {
				result.Add( await AddOrUpdateAsync( entity ) );
			}

			return result;
		}

		/// <summary>
		///     Finds an entity by its id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Task<TModel> FindAsync( string id ) {
			return Task.FromResult( AsQueryable().FirstOrDefault( model => model.Id == id ) );
		}

		/// <summary>
		///     Removes a single entity identified by its id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Task<ResourceResponse<Document>> RemoveAsync( string id ) {
			return _documentClient.DeleteDocumentAsync( $"{_collectionLink}/docs/{id}" );
		}

		/// <summary>
		///     Removes a single entity
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public Task<ResourceResponse<Document>> RemoveAsync( TModel entity ) {
			return RemoveAsync( entity.Id );
		}

		/// <summary>
		///     Removes a list of entities
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		public async Task<IList<ResourceResponse<Document>>> RemoveRangeAsync( List<TModel> entities ) {
			var result = new List<ResourceResponse<Document>>();

			foreach ( var entity in entities ) {
				result.Add( await RemoveAsync( entity ) );
			}

			return result;
		}

	}

}
