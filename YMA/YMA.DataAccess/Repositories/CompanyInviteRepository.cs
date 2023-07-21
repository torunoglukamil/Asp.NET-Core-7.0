using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.DataAccess.Queries;
using YMA.Entities.Converters;
using YMA.Entities.Entities;
using YMA.Entities.Models;
using YMA.Entities.Validators;

namespace YMA.DataAccess.Repositories
{
    public class CompanyInviteRepository
    {
        private readonly ymaContext _db;
        private readonly CompanyInviteQuery _companyInviteQuery;
        private readonly ResponseHelper _responseHelper;
        private readonly ValidationHelper<CompanyInviteModel> _companyInviteValidator;
        private readonly ValidationHelper<ReplyCompanyInviteModel> _replyCompanyInviteValidator;

        public CompanyInviteRepository(ymaContext db, CompanyInviteQuery companyInviteQuery, ResponseHelper responseHelper)
        {
            _db = db;
            _companyInviteQuery = companyInviteQuery;
            _responseHelper = responseHelper;
            _companyInviteValidator = new ValidationHelper<CompanyInviteModel>(new CompanyInviteValidator());
            _replyCompanyInviteValidator = new ValidationHelper<ReplyCompanyInviteModel>(new ReplyCompanyInviteValidator());
        }

        public ResponseModel CreateCompanyInvite(CompanyInviteModel companyInvite) => _responseHelper.TryCatch(
            "CompanyInviteRepository.CreateCompanyInvite",
            () =>
            {
                ResponseModel response = _companyInviteValidator.Validate(companyInvite);
                if (response.status_code == StatusCodes.Status400BadRequest)
                {
                    return response;
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

        public ResponseModel ReplyCompanyInvite(ReplyCompanyInviteModel replyCompanyInvite) => _responseHelper.TryCatch(
            "CompanyInviteRepository.ReplyCompanyInvite",
            () =>
            {
                ResponseModel response = _replyCompanyInviteValidator.Validate(replyCompanyInvite);
                if (response.status_code == StatusCodes.Status400BadRequest)
                {
                    return response;
                }
                response = _companyInviteQuery.GetCompanyInviteById(replyCompanyInvite.id);
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
