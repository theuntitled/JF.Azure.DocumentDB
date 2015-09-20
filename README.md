# JF.Azure.DocumentDB

This project aims to enable easy DocumentDB access.


## NuGet

This library can be installed via NuGet
https://www.nuget.org/packages/JF.Azure.DocumentDB/1.0.0

    PM> Install-Package JF.Azure.DocumentDB


## Usage

To manage collections inherit the CollectionManager class and add the collections as properties.
Your model classes must inherit the Microsoft.Azure.Documents.Resource class.

    
    public class MyCollections : CollectionManager {
	    
		public ICollection<MyEntityType> MyEntitites { get; set; }

		public MyCollections( DocumentClient documentClient , string databaseId , bool createDatabaseIfNonexistent = false ) : base( documentClient , databaseId , createDatabaseIfNonexistent ) {
		}

	}
    
 
