using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyWebApp.Models
{
    public class Subscriber
    {
        // Primärnyckel för MongoDB, lagras som ObjectId
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        // Namn på prenumeranten
        [Required]
        [StringLength(20, ErrorMessage = "Name cannot exceed 20 characters")]
        [BsonElement("name")]
        public string? Name { get; set; }

        // E-post till prenumeranten
        [Required]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Missing top level domain")]
        [BsonElement("email")]
        public string? Email { get; set; }
    }
}
