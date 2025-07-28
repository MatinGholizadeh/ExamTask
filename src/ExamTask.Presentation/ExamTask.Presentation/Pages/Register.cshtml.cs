using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ExamTask.Application.Features.Auth.Register.Commands;
using ExamTask.Domain.Identity;

namespace ExamTask.Presentation.Pages;

public class RegisterModel : PageModel
{
    private readonly IServiceProvider _serviceProvider;  // Added IServiceProvider

    [BindProperty]
    public RegisterInputModel Input { get; set; } = new();

    [BindProperty]
    public string Role { get; set; }
    public string? ErrorMessage { get; set; }

    private readonly IMediator _mediator;

    // Inject IServiceProvider into the constructor
    public RegisterModel(IMediator mediator, IServiceProvider serviceProvider)
    {
        _mediator = mediator;
        _serviceProvider = serviceProvider;
    }

    public void OnGet()
    {
        // Make sure that Role is properly set
        Role = Request.Query["role"].ToString().ToLower() ?? "student"; // Default to "student" if role is not provided
    }

    public async Task<IActionResult> OnPost()
    {
        // If model validation failed, stay on the page
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Map the input to the RegisterCommand with the selected role
        var command = new RegisterCommand(
            Input.FirstName,
            Input.LastName,
            Input.NationalCode,
            Input.Email,
            Input.PhoneNumber,
            Input.Password,
            Role ?? "student"); // Default to "student" if role is not provided

        try
        {
            // Try to register the user
            var user = await _mediator.Send(command);

            // Create a new scope to resolve services
            using (var scope = _serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<long>>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                // Assign the role based on the selected role
                var userToAddRole = await userManager.FindByEmailAsync(Input.Email);

                if (userToAddRole != null && !string.IsNullOrEmpty(Role))
                {
                    await userManager.AddToRoleAsync(userToAddRole, Role);
                }
            }

            // Store success message in TempData
            TempData["SuccessMessage"] = "Registration was successful! You will be redirected to the home page.";

            // After successful registration, redirect to the home page
            return RedirectToPage("/Index");
        }
        catch (Exception ex)
        {
            // In case of error, display the error message
            ErrorMessage = ex.Message;
            return Page();
        }
    }

    public class RegisterInputModel
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string NationalCode { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
