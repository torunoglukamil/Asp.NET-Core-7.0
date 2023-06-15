using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Models.Models;

namespace SchoolManager.DataAccess.Repositories
{
    public class StudentRepository : ControllerBase
    {
        private readonly school_managerContext _db;
        private readonly IValidator<Student> _validator;

        public StudentRepository(school_managerContext context, IValidator<Student> validator)
        {
            _db = context;
            _validator = validator;
        }

        public async Task<IActionResult> CreateStudent(Student student)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(student);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors.FirstOrDefault()!.ErrorMessage);
                }
                Student? _student = _db.students.Where(x => x.email == student.email).FirstOrDefault();
                if (_student != null)
                {
                    return BadRequest("E-posta adresi zaten kullanımda.");
                }
                if (student.phone != null)
                {
                    _student = _db.students.Where(x => x.phone == student.phone).FirstOrDefault();
                    if (_student != null)
                    {
                        return BadRequest("Telefon numarası zaten kullanımda.");
                    }
                }
                Classroom? classroom = _db.classrooms.Where(x => x.id == student.classroom_id).FirstOrDefault();
                if (classroom == null)
                {
                    return BadRequest("Sınıf bulunamadı.");
                }
                _db.students.Add(student);
                _db.SaveChanges();
                return Ok("Öğrenci başarıyla oluşturuldu.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public async Task<IActionResult> UpdateStudent(Student student)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(student);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors.FirstOrDefault()!.ErrorMessage);
                }
                Student? _student = _db.students.Where(x => x.email == student.email).FirstOrDefault();
                if (_student != null && _student.id != student.id)
                {
                    return BadRequest("E-posta adresi zaten kullanımda.");
                }
                if (student.phone != null)
                {
                    _student = _db.students.Where(x => x.phone == student.phone).FirstOrDefault();
                    if (_student != null && _student.id != student.id)
                    {
                        return BadRequest("Telefon numarası zaten kullanımda.");
                    }
                }
                _student = _db.students.Where(x => x.id == student.id).FirstOrDefault();
                if (_student == null)
                {
                    return BadRequest("Öğrenci bulunamadı.");
                }
                Classroom? classroom = _db.classrooms.Where(x => x.id == student.classroom_id).FirstOrDefault();
                if (classroom == null)
                {
                    return BadRequest("Sınıf bulunamadı.");
                }
                _student.first_name = student.first_name;
                _student.last_name = student.last_name;
                _student.age = student.age;
                _student.email = student.email;
                _student.phone = student.phone;
                _student.classroom_id = student.classroom_id;
                _db.SaveChanges();
                return Ok("Öğrenci başarıyla güncellendi.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult DeleteStudentById(int id)
        {
            try
            {
                Student? student = _db.students.Where(x => x.id == id).FirstOrDefault();
                if (student == null)
                {
                    return BadRequest("Öğrenci bulunamadı.");
                }
                _db.students.Remove(student);
                _db.SaveChanges();
                return Ok("Öğrenci başarıyla silindi.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
