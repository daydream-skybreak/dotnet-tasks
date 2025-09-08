using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mvc_project.Models;

public class Photo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    private string path { get; set; }
}