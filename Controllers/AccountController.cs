using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trainee_projectmanagement.Data;
using trainee_projectmanagement.Models;

namespace trainee_projectmanagement.Controllers;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AccountController> _logger;

    public AccountController(ApplicationDbContext context, ILogger<AccountController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: Account/Signup
    public IActionResult Signup()
    {
        return View();
    }

    // POST: Account/Signup
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Signup(string fullname, string email, string password, string confirmpassword, 
        string schoolattended, string role, string coursesinterested, string interestedincertification)
    {
        try
        {
            // Validation
            if (string.IsNullOrWhiteSpace(fullname) || string.IsNullOrWhiteSpace(email) || 
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(schoolattended) ||
                string.IsNullOrWhiteSpace(role))
            {
                ModelState.AddModelError("", "Please fill all required fields");
                return View();
            }

            if (password != confirmpassword)
            {
                ModelState.AddModelError("", "Passwords do not match");
                return View();
            }

            // Check if email already exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email already registered");
                return View();
            }

            var user = new User
            {
                FullName = fullname,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                SchoolAttended = schoolattended,
                Role = role,
                CoursesInterested = coursesinterested ?? string.Empty,
                InterestedInCertification = interestedincertification == "True" || interestedincertification == "true",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Set session
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserName", user.FullName);
            HttpContext.Session.SetString("UserType", "User");

            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during signup");
            ModelState.AddModelError("", "An error occurred during signup");
            return View();
        }
    }

    // GET: Account/Signin
    public IActionResult Signin(string returnUrl = "")
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    // POST: Account/Signin
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Signin(string email, string password, string returnUrl = "")
    {
        try
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Please enter email and password");
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            // Check if admin
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == email);
            if (admin != null && BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash))
            {
                HttpContext.Session.SetInt32("AdminId", admin.Id);
                HttpContext.Session.SetString("AdminEmail", admin.Email);
                HttpContext.Session.SetString("AdminName", admin.FullName);
                HttpContext.Session.SetString("UserType", "Admin");
                return RedirectToAction("Dashboard", "Admin");
            }

            // Check if user
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserName", user.FullName);
                HttpContext.Session.SetString("UserType", "User");

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid email or password");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during signin");
            ModelState.AddModelError("", "An error occurred during signin");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
    }

    // GET: Account/Logout
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}

public class SignupViewModel
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string SchoolAttended { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public List<string> CoursesInterested { get; set; } = new();
    public bool InterestedInCertification { get; set; }
}

public class SigninViewModel
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
