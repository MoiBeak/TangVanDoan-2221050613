using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using newMVC.Data;
using newMVC.Models.Entities;
using ClosedXML.Excel;
using newMVC.Helpers;


namespace newMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDBContext _context;

        public StudentController(ApplicationDBContext context)
        {
            _context = context;
        }

        // READ: Hiển thị danh sách sinh viên kèm tên khoa
        public IActionResult Index()
        {
            var students = _context.Students
                                   .Include(s => s.Faculty)
                                   .ToList();
            return View(students);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            ViewBag.FacultyId = new SelectList(_context.Faculties, "Id", "FacultyName");
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.FacultyId = new SelectList(_context.Faculties, "Id", "FacultyName", student.FacultyId);
            return View(student);
        }

        // EDIT (GET)
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();

            ViewBag.FacultyId = new SelectList(_context.Faculties, "Id", "FacultyName", student.FacultyId);
            return View(student);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Student student)
        {
            if (id != student.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Students.Update(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.FacultyId = new SelectList(_context.Faculties, "Id", "FacultyName", student.FacultyId);
            return View(student);
        }

        // DELETE (GET)
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var student = _context.Students
                                  .Include(s => s.Faculty)
                                  .FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        // DELETE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.Find(id);

            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
        // GET: Upload Excel
        public IActionResult Upload()
        {
            return View();
        }
       [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            Console.WriteLine("=== START UPLOAD ===");

            if (file == null || file.Length == 0)
            {
                Console.WriteLine("❌ File null hoặc rỗng");
                return Content("File không hợp lệ!");
            }

            Console.WriteLine($"✔ File: {file.FileName} - Size: {file.Length}");

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                Console.WriteLine("✔ Đã copy file vào stream");

                var columnMapping = new Dictionary<string, string>()
                {
                    { "mãsinhviên", "StudentCode" },
                    { "họvàtên", "FullName" },
                    { "tuổi", "Age" },
                    { "email", "Email" },
                    { "khoa", "FacultyName" }
                };

                var students = ExcelHelper.ReadExcel(stream, (row, col) =>
                {
                    Console.WriteLine("---- Đọc 1 dòng ----");

                    int age = 0;
                    if (col.ContainsKey("Age"))
                        int.TryParse(row.Cell(col["Age"]).GetValue<string>(), out age);

                    var student = new Student
                    {
                        StudentCode = col.ContainsKey("StudentCode")
                            ? row.Cell(col["StudentCode"]).GetValue<string>()
                            : null,

                        FullName = col.ContainsKey("FullName")
                            ? row.Cell(col["FullName"]).GetValue<string>()
                            : null,

                        Age = age,

                        Email = col.ContainsKey("Email")
                            ? row.Cell(col["Email"]).GetValue<string>()
                            : null
                    };

                    if (col.ContainsKey("FacultyName"))
                    {
                        student.Faculty = new Faculty
                        {
                            FacultyName = row.Cell(col["FacultyName"]).GetValue<string>()
                        };
                    }

                    Console.WriteLine($"StudentCode: {student.StudentCode}");

                    if (string.IsNullOrEmpty(student.StudentCode))
                    {
                        Console.WriteLine("⚠ Bỏ dòng vì thiếu StudentCode");
                        return null;
                    }

                    return student;

                }, columnMapping);

                Console.WriteLine($"✔ Tổng số student đọc được: {students.Count}");

                var faculties = _context.Faculties.ToList();

                foreach (var s in students)
                {
                    Console.WriteLine($"--- Xử lý student: {s.StudentCode} ---");

                    if (s.Faculty == null || string.IsNullOrEmpty(s.Faculty.FacultyName))
                    {
                        Console.WriteLine("⚠ Bỏ vì Faculty null");
                        continue;
                    }

                    var faculty = faculties.FirstOrDefault(f =>
                        f.FacultyName.ToLower() == s.Faculty.FacultyName.ToLower());

                    if (faculty == null)
                    {
                        Console.WriteLine($"➕ Tạo Faculty: {s.Faculty.FacultyName}");

                        faculty = new Faculty
                        {
                            FacultyName = s.Faculty.FacultyName
                        };

                        _context.Faculties.Add(faculty);
                        await _context.SaveChangesAsync();

                        faculties.Add(faculty);
                    }

                    s.FacultyId = faculty.Id;

                    var exists = _context.Students
                        .Any(x => x.StudentCode == s.StudentCode);

                    if (!exists)
                    {
                        Console.WriteLine("➕ Thêm student");
                        _context.Students.Add(s);
                    }
                    else
                    {
                        Console.WriteLine("⚠ Trùng student");
                    }
                }
            }

            await _context.SaveChangesAsync();

            Console.WriteLine("=== END UPLOAD ===");

            return RedirectToAction(nameof(Index));
        }

    }

}
