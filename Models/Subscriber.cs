using MyWebApp.Models;
using MyWebApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWebApp;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models;
public class Subscriber
{
    [Required]
    [StringLength(20, ErrorMessage = "Name cannot exceed 20 characters")]
    public string? Name { get; set; }

    [Required]
    [EmailAddress]
    [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Missing top level domain")]
    public string? Email { get; set; }
}