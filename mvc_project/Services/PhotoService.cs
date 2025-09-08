using mvc_project.Models;

namespace mvc_project.Services;
using MongoDB.Driver;

public class PhotoService
{
    private readonly IMongoCollection<Photo> _photos;

    public PhotoService(IConfiguration config)
    {
        var  client = new MongoClient(config.GetConnectionString("MongoDB"));
        var database = client.GetDatabase(config.GetConnectionString("MongoDB"));
        _photos = database.GetCollection<Photo>(config.GetConnectionString("PhotoCollection"));
    }
    
    public List<Photo> GetAll() => _photos.Find(photo => true).ToList();
    
    public async Task<List<Photo>> GetByBookId(string photoId) => await _photos.Find( id => id.Id == photoId).ToListAsync();
}