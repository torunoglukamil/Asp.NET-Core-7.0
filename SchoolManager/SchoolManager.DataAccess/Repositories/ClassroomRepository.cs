using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Models.Models;

namespace SchoolManager.DataAccess.Repositories
{
    public class ClassroomRepository : ControllerBase
    {
        private readonly school_managerContext _db;
        private readonly IValidator<Classroom> _validator;

        public ClassroomRepository(school_managerContext context, IValidator<Classroom> validator)
        {
            _db = context;
            _validator = validator;
        }

        public async Task<IActionResult> CreateClassroom(Classroom classroom)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(classroom);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors.FirstOrDefault()!.ErrorMessage);
                }
                Classroom? _classroom = _db.classrooms.Where(x => x.grade == classroom.grade && x.branch == classroom.branch).FirstOrDefault();
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

        public async Task<IActionResult> UpdateClassroom(Classroom classroom)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(classroom);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors.FirstOrDefault()!.ErrorMessage);
                }
                Classroom? _classroom = _db.classrooms.Where(x => x.grade == classroom.grade && x.branch == classroom.branch).FirstOrDefault();
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
                Classroom? classroom = _db.classrooms.Where(x => x.id == id).FirstOrDefault();
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
