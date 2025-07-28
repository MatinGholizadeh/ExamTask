using ExamTask.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ExamTask.Presentation.Pages;

public class LoginModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    [BindProperty]
    public LoginInputModel Input { get; set; } = new();
    public string? Role { get; set; }
    public string? ErrorMessage { get; set; }
    public string? WelcomeMessage { get; set; }  // Welcome message to show

    // Inject SignInManager and UserManager
    public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public void OnGet()
    {
        Role = Request.Query["role"].ToString().ToLower();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Authenticate the user
        var user = await _userManager.FindByEmailAsync(Input.Email);
        if (user == null || !(await _userManager.CheckPasswordAsync(user, Input.Password)))
        {
            ErrorMessage = "Invalid credentials.";
            return Page();
        }

        // Sign in the user
        var result = await _signInManager.PasswordSignInAsync(user, Input.Password, false, false);

        if (result.Succeeded)
        {
            // Generate the welcome message
            WelcomeMessage = $"Welcome {user.FirstName}!";

            // Redirect to home page with success message
            TempData["SuccessMessage"] = $"Welcome back, {user.FirstName}!";
            return RedirectToPage("/Index");
        }
        else
        {
            ErrorMessage = "Login failed. Please try again.";
            return Page();
        }
    }

    public class LoginInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}