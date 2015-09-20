using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace JF.Azure.DocumentDB {

	/// <summary>
	///     Access wrapper for the Azure DocumentDB
	/// </summary>
	/// <typeparam name="TModel"></typeparam>
	public interface ICollection<TModel>
		where TModel : Resource , new() {

		/// <summary>
		///     Creates a document query
		/// </summary>
		/// <returns></returns>
		IOrderedQueryable<TModel> AsQueryable();

		/// <summary>
		///     Inserts or replaces an entity
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task<ResourceResponse<Document>> AddOrUpdateAsync( TModel entity );

		/// <summary>
		///     Inserts or replaces entities
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		Task<IList<ResourceResponse<Document>>> AddOrUpdateAsync( IList<TModel> entities );

		/// <summary>
		///     Finds an entity by its id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<TModel> FindAsync( string id );

		/// <summary>
		///     Removes a single entity identified by its id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<ResourceResponse<Document>> RemoveAsync( string id );

		/// <summary>
		///     Removes a single entity
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task<ResourceResponse<Document>> RemoveAsync( TModel entity );

		/// <summary>
		///     Removes a list of entities
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		Task<IList<ResourceResponse<Document>>> RemoveRangeAsync( List<TModel> entities );

	}

}
