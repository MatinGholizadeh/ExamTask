using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ExamTask.Application.Features.Auth.Register.Commands;

namespace ExamTask.Presentation.Pages;

public class RegisterModel : PageModel
{
    [BindProperty]
    public RegisterInputModel Input { get; set; } = new();
    public string? Role { get; set; }

    public string? ErrorMessage { get; set; }

    private readonly IMediator _mediator;

    public RegisterModel(IMediator mediator)
    {
        _mediator = mediator;
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

        // Map the input to the RegisterCommand with the role
        var command = new RegisterCommand(
            Input.FirstName,
            Input.LastName,
            Input.NationalCode,
            Input.Email,
            Input.PhoneNumber,
            Input.Password,
            Role ?? "student"); // Default to "student" if role is null

        try
        {
            // Simulate successful registration
            var token = await _mediator.Send(command);

            // Store success message in TempData
            TempData["SuccessMessage"] = "ثبت نام با موفقیت انجام شد! به صفحه اصلی هدایت می‌شوید.";

            // Redirect to home page
            return RedirectToPage("/Index");
        }
        catch (Exception ex)
        {
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
