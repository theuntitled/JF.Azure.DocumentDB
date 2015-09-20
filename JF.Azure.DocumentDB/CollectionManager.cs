using System;
using System.Linq;
using System.Reflection;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace JF.Azure.DocumentDB {

	/// <summary>
	///     Manages all ICollections properties
	/// </summary>
	public class CollectionManager : ICollectionManager {

		private readonly Type _iCollectionType = typeof (ICollection<>);

		/// <summary>
		///     The DocumentDB database instance
		/// </summary>
		protected readonly Database Database;

		/// <summary>
		///     The id of the database
		/// </summary>
		protected readonly string DatabaseId;

		/// <summary>
		///     The DocumentDB DocumentClient instance
		/// </summary>
		protected readonly DocumentClient DocumentClient;

		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="documentClient"></param>
		/// <param name="databaseId"></param>
		/// <param name="createDatabaseIfNonexistent"></param>
		public CollectionManager( DocumentClient documentClient ,
								  string databaseId ,
								  bool createDatabaseIfNonexistent = false ) {
			DocumentClient = documentClient;
			DatabaseId = databaseId;

			Database = DocumentClient.CreateDatabaseQuery().FirstOrDefault( item => item.Id == databaseId );

			Initialize( createDatabaseIfNonexistent );
		}

		/// <summary>
		///     Dispose
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose() {
		}

		/// <summary>
		///     Checks if the property implements the ICollection interface
		/// </summary>
		/// <param name="propertyInfo"></param>
		/// <returns></returns>
		protected bool IsCollection( PropertyInfo propertyInfo ) {
			return propertyInfo.PropertyType.IsClass &&
				   propertyInfo.PropertyType.GetInterfaces()
							   .Any(
								   interfaceInfo => interfaceInfo.IsGenericType && interfaceInfo.GetGenericTypeDefinition() == _iCollectionType );
		}

		/// <summary>
		///     Prepares all ICollection properties of this class, creates the collections in the DocumentDB if necessary.
		/// </summary>
		protected async void Initialize( bool createDatabaseIfNonexistent ) {
			if ( createDatabaseIfNonexistent && Database == null ) {
				await DocumentClient.CreateDatabaseAsync( new Database {
					Id = DatabaseId
				} );
			}

			var properties = GetType().GetProperties().Where( IsCollection ).ToList();

			foreach ( var property in properties ) {
				var documentCollection =
					DocumentClient.CreateDocumentCollectionQuery( $"dbs/{DatabaseId}" )
								  .FirstOrDefault( collection => collection.Id == property.Name );

				if ( documentCollection == null ) {
					var result = await DocumentClient.CreateDocumentCollectionAsync( $"dbs/{DatabaseId}" ,
																					 new DocumentCollection {
																						 Id = property.Name
																					 } );

					documentCollection = result.Resource;
				}

				property.SetValue( this ,
								   Activator.CreateInstance( property.PropertyType , DocumentClient , documentCollection ) );
			}
		}

	}

}
