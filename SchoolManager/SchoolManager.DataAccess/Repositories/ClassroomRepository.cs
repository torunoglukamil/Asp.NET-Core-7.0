using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Models.Models;

namespace SchoolManager.DataAccess.Repositories
{
    public class ClassroomRepository : ControllerBase
    {
        private readonly school_managerContext _db;
        private readonly IValidator<classroom> _validator;

        public ClassroomRepository(school_managerContext context, IValidator<classroom> validator)
        {
            _db = context;
            _validator = validator;
        }

        public async Task<IActionResult> CreateClassroom(classroom classroom)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(classroom);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors.FirstOrDefault()!.ErrorMessage);
                }
                classroom? _classroom = _db.classrooms.Where(x => x.grade == classroom.grade && x.branch == classroom.branch).FirstOrDefault();
                if (_classroom != null)
                {
                    return BadRequest("Sınıf zaten mevcut.");
                }
                _db.classrooms.Add(classroom);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        public async Task<IActionResult> UpdateClassroom(classroom classroom)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(classroom);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors.FirstOrDefault()!.ErrorMessage);
                }
                classroom? _classroom = _db.classrooms.Where(x => x.grade == classroom.grade && x.branch == classroom.branch).FirstOrDefault();
                if (_classroom != null && _classroom.id != classroom.id)
                {
                    return BadRequest("Sınıf zaten mevcut.");
                }
                _classroom = _db.classrooms.Where(x => x.id == classroom.id).FirstOrDefault();
                if (_classroom == null)
                {
                    return BadRequest("Sınıf bulunamadı.");
                }
                _classroom.grade = classroom.grade;
                _classroom.branch = classroom.branch;
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        public IActionResult DeleteClassroomById(int id)
        {
            try
            {
                classroom? classroom = _db.classrooms.Where(x => x.id == id).FirstOrDefault();
                if (classroom == null)
                {
                    return BadRequest("Sınıf bulunamadı.");
                }
                _db.classrooms.Remove(classroom);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
