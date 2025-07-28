using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ExamTask.Presentation.Pages;

public class LoginModel : PageModel
{
    [BindProperty]
    public LoginInputModel Input { get; set; } = new();
    public string? Role { get; set; }

    public string? ErrorMessage { get; set; }

    public void OnGet()
    {
        Role = Request.Query["role"].ToString().ToLower();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        ErrorMessage = "Invalid credentials.";
        return Page();
    }

    public class LoginInputModel
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
