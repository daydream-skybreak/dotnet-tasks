using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace mvc_project.Models;

public class Photo
{
    [Key]
    public int? Id { get; set; }
    
    public string path { get; set; }
}