using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trainee_projectmanagement.Data;

namespace trainee_projectmanagement.Controllers;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AdminController> _logger;

    public AdminController(ApplicationDbContext context, ILogger<AdminController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Check if user is admin
    private bool IsAdmin()
    {
        var userType = HttpContext.Session.GetString("UserType");
        return userType == "Admin";
    }

    // GET: Admin/Dashboard
    public async Task<IActionResult> Dashboard()
    {
        if (!IsAdmin())
            return RedirectToAction("Signin", "Account");

        try
        {
            var totalUsers = await _context.Users.CountAsync();
            var certificationInterestedCount = await _context.Users.Where(u => u.InterestedInCertification).CountAsync();
            var certificationNotInterestedCount = totalUsers - certificationInterestedCount;

            // Get course distribution
            var allCourses = new Dictionary<string, int>();
            var users = await _context.Users.ToListAsync();

            foreach (var user in users)
            {
                if (!string.IsNullOrEmpty(user.CoursesInterested))
                {
                    var courses = user.CoursesInterested.Split(",");
                    foreach (var course in courses)
                    {
                        var cleanCourse = course.Trim();
                        if (allCourses.ContainsKey(cleanCourse))
                            allCourses[cleanCourse]++;
                        else
                            allCourses[cleanCourse] = 1;
                    }
                }
            }

            // Get role distribution
            var roleDistribution = await _context.Users
                .GroupBy(u => u.Role)
                .Select(g => new { Role = g.Key, Count = g.Count() })
                .ToListAsync();

            ViewBag.TotalUsers = totalUsers;
            ViewBag.CertificationInterested = certificationInterestedCount;
            ViewBag.CertificationNotInterested = certificationNotInterestedCount;
            ViewBag.Courses = allCourses;
            ViewBag.RoleDistribution = roleDistribution;

            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading dashboard");
            return View();
        }
    }

    // GET: Admin/Users
    public async Task<IActionResult> Users()
    {
        if (!IsAdmin())
            return RedirectToAction("Signin", "Account");

        var users = await _context.Users.OrderByDescending(u => u.CreatedAt).ToListAsync();
        return View(users);
    }
}
