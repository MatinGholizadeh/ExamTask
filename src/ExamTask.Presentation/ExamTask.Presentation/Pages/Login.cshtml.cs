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
        // Get the role from the query string if provided
        Role = Request.Query["role"].ToString().ToLower();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page(); // Return the page if the model is not valid
        }

        // Find user by email
        var user = await _userManager.FindByEmailAsync(Input.Email);

        // If user doesn't exist or password is incorrect
        if (user == null || !(await _userManager.CheckPasswordAsync(user, Input.Password)))
        {
            TempData["ErrorMessage"] = "ایمیل یا پسورد شما صحیح نمی‌باشد."; // Display error message
            return RedirectToPage(); // Redirect back to login page
        }

        // Check if the user role matches the selected role (either Teacher or Student)
        if (Role == "teacher" && !await _userManager.IsInRoleAsync(user, "Teacher"))
        {
            TempData["ErrorMessage"] = "کاربر معلم نمی‌باشد."; // Error message for wrong role
            return RedirectToPage();
        }

        if (Role == "student" && !await _userManager.IsInRoleAsync(user, "Student"))
        {
            TempData["ErrorMessage"] = "کاربر دانش‌آموز نمی‌باشد."; // Error message for wrong role
            return RedirectToPage();
        }

        // Sign in the user if everything is correct
        var result = await _signInManager.PasswordSignInAsync(user, Input.Password, isPersistent: false, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            // Set success message and redirect to homepage
            TempData["SuccessMessage"] = $"{user.FirstName} ورود شما با موفقیت انجام شد!";
            return RedirectToPage("/Index"); // Redirect to the home page
        }

        // If sign-in failed, add a generic error message
        TempData["ErrorMessage"] = "ورود ناموفق بود. لطفاً دوباره تلاش کنید.";
        return RedirectToPage();
    }

    // Input Model for Login
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
