using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.DataAccess.Queries;
using YMA.Entities.Converters;
using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.DataAccess.Repositories
{
    public class CompanyInviteRepository
    {
        private readonly ymaContext _db;
        private readonly CompanyInviteQuery _companyInviteQuery;
        private readonly ResponseHelper _responseHelper;
        private readonly IValidator<CompanyInviteModel> _companyInviteValidator;
        private readonly IValidator<ReplyCompanyInviteModel> _replyCompanyInviteValidator;

        public CompanyInviteRepository(ymaContext db, CompanyInviteQuery companyInviteQuery, ResponseHelper responseHelper, IValidator<CompanyInviteModel> companyInviteValidator, IValidator<ReplyCompanyInviteModel> replyCompanyInviteValidator)
        {
            _db = db;
            _companyInviteQuery = companyInviteQuery;
            _responseHelper = responseHelper;
            _companyInviteValidator = companyInviteValidator;
            _replyCompanyInviteValidator = replyCompanyInviteValidator;
        }

        public async Task<ResponseModel> CreateCompanyInvite(CompanyInviteModel companyInvite) => await _responseHelper.TryCatch(
            "CompanyInviteRepository.CreateCompanyInvite",
            async () =>
            {
                ValidationResult validationResult = await _companyInviteValidator.ValidateAsync(companyInvite);
                if (!validationResult.IsValid)
                {
                    return new ResponseModel()
                    {
                        message = validationResult.Errors.FirstOrDefault()!.ErrorMessage,
                    };
                }
                company_invite _companyInvite = CompanyInviteConverter.ToCompanyInvite(companyInvite);
                _companyInvite.create_date = DateTime.Now;
                _db.company_invites.Add(_companyInvite);
                _db.SaveChanges();
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    message = "Davet başarıyla gönderildi.",
                };
            }
        );

        public async Task<ResponseModel> ReplyCompanyInvite(ReplyCompanyInviteModel replyCompanyInvite) => await _responseHelper.TryCatch(
            "CompanyInviteRepository.ReplyCompanyInvite",
            async () =>
            {
                ValidationResult validationResult = await _replyCompanyInviteValidator.ValidateAsync(replyCompanyInvite);
                if (!validationResult.IsValid)
                {
                    return new ResponseModel()
                    {
                        message = validationResult.Errors.FirstOrDefault()!.ErrorMessage,
                    };
                }
                ResponseModel response = _companyInviteQuery.GetCompanyInviteById(replyCompanyInvite.id);
                if (response.status_code == StatusCodes.Status400BadRequest)
                {
                    return response;
                }
                company_invite companyInvite = response.data!;
                companyInvite.is_accepted = replyCompanyInvite.is_accepted;
                companyInvite.reply_date = DateTime.Now;
                _db.SaveChanges();
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    message = "Davet başarıyla yanıtlandı.",
                };
            }
        );
    }
}
