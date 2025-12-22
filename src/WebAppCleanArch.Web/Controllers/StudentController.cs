using Microsoft.AspNetCore.Mvc;
using WebAppCleanArch.Application.Students;
using WebAppCleanArch.Domain.Entities;

namespace WebAppCleanArch.Web.Controllers;

public class StudentController : Controller
{
    private readonly StudentService _studentService;

    public StudentController(StudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task<IActionResult> Index()
    {
        var students = await _studentService.GetAllAsync();
        return View(students);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student)
    {
        if (!ModelState.IsValid)
            return View(student);

        await _studentService.CreateAsync(student);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        if (student == null) return NotFound();

        return View(student);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Student student)
    {
        if (id != student.Id) return NotFound();
        if (!ModelState.IsValid) return View(student);

        await _studentService.UpdateAsync(student);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        if (student == null) return NotFound();

        return View(student);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _studentService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var student = await _studentService.GetDetailsAsync(id);
        if (student == null) return NotFound();

        return View(student);
    }
}