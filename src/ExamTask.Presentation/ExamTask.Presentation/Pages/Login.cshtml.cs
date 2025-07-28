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

        // Find user by email
        var user = await _userManager.FindByEmailAsync(Input.Email);

        // If user doesn't exist or password is incorrect
        if (user == null || !(await _userManager.CheckPasswordAsync(user, Input.Password)))
        {
            TempData["ErrorMessage"] = "Incorrect username or password.";
            return RedirectToPage(); // Redirect to the same page to display the error
        }

        // Check if the user role matches the selected role (either Teacher or Student)
        if (Role == "teacher" && !await _userManager.IsInRoleAsync(user, "Teacher"))
        {
            TempData["ErrorMessage"] = "Incorrect username or password.";
            return RedirectToPage();
        }

        if (Role == "student" && !await _userManager.IsInRoleAsync(user, "Student"))
        {
            TempData["ErrorMessage"] = "Incorrect username or password.";
            return RedirectToPage();
        }

        // Sign in the user if everything is correct
        var result = await _signInManager.PasswordSignInAsync(user, Input.Password, false, false);
        if (result.Succeeded)
        {
            TempData["SuccessMessage"] = $"{user.FirstName} ورود شما با موفقیت انجام شد!";
            return RedirectToPage("/Index"); // Redirect to the home page
        }

        // If sign-in failed, add a generic error message
        TempData["ErrorMessage"] = "Login failed. Please try again.";
        return RedirectToPage();
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