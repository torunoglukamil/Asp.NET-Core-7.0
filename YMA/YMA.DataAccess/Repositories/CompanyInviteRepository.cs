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
        private readonly CompanyQuery _companyQuery;
        private readonly ResponseHelper _responseHelper;
        private readonly ValidationHelper<CompanyInviteModel> _companyInviteValidator;

        public CompanyInviteRepository(ymaContext db, CompanyInviteQuery companyInviteQuery, CompanyQuery companyQuery, ResponseHelper responseHelper)
        {
            _db = db;
            _companyInviteQuery = companyInviteQuery;
            _companyQuery = companyQuery;
            _responseHelper = responseHelper;
            _companyInviteValidator = new ValidationHelper<CompanyInviteModel>(new CompanyInviteValidator());
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
                response = _companyInviteQuery.GetCompanyInviteList(companyInvite.receiver_id!, companyInvite.sender_id!);
                List<CompanyInviteModel> companyInviteList = response.data!;
                foreach (CompanyInviteModel companyInvite_ in companyInviteList)
                {
                    if (companyInvite_.is_accepted == null)
                    {
                        string message = string.Empty;
                        if (companyInvite_.sender_id == companyInvite.sender_id)
                        {
                            message = "İlgili firmaya gönderilen bir davet zaten mevcut. Lütfen bu davetin yanıtlanmasını bekleyiniz.";
                        }
                        else
                        {
                            message = "İlgili firmadan gelen bir davet mevcut. Yeni bir davet göndermeden önce gelen daveti yanıtlayınız.";
                        }
                        response = _companyQuery.GetCompanyById(companyInvite.receiver_id!, companyInvite.sender_id!);
                        return new ResponseModel()
                        {
                            message = message,
                            type = "invite-is-already-exist",
                            data = response.data,
                        };
                    }
                }
                _db.company_invites.Add(CompanyInviteConverter.ToCompanyInvite(companyInvite));
                _db.SaveChanges();
                response = _companyQuery.GetCompanyById(companyInvite.receiver_id!, companyInvite.sender_id!);
                if (response.status_code == StatusCodes.Status400BadRequest)
                {
                    return response;
                }
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    message = "Davet başarıyla gönderildi.",
                    data = response.data,
                };
            }
        );

        public ResponseModel AcceptCompanyInviteById(string companyInviteId) => _responseHelper.TryCatch(
            "CompanyInviteRepository.AcceptCompanyInviteById",
            () =>
            {
                ResponseModel response = _companyInviteQuery.GetCompanyInviteById(companyInviteId);
                if (response.status_code == StatusCodes.Status400BadRequest)
                {
                    return response;
                }
                company_invite companyInvite = response.data!;
                if (companyInvite.is_accepted == false)
                {
                    return new ResponseModel()
                    {
                        message = companyInvite.reply_date == null ? "Kabul etmek istediğiniz daveti firma önceden iptal etti." : "Kabul etmek istediğiniz daveti önceden reddettiniz.",
                    };
                }
                if (companyInvite.is_accepted == null)
                {
                    companyInvite.is_accepted = true;
                    companyInvite.reply_date = DateTime.Now;
                    _db.SaveChanges();
                }
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    message = "Gelen davet kabul edildi.",
                };
            }
        );

        public ResponseModel RejectCompanyInviteById(string companyInviteId) => _responseHelper.TryCatch(
            "CompanyInviteRepository.RejectCompanyInviteById",
            () =>
            {
                ResponseModel response = _companyInviteQuery.GetCompanyInviteById(companyInviteId);
                if (response.status_code == StatusCodes.Status400BadRequest)
                {
                    return response;
                }
                company_invite companyInvite = response.data!;
                if (companyInvite.is_accepted == true)
                {
                    return new ResponseModel()
                    {
                        message = "Reddetmek istediğiniz daveti önceden kabul ettiniz.",
                    };
                }
                if (companyInvite.is_accepted == null)
                {
                    companyInvite.is_accepted = false;
                    companyInvite.reply_date = DateTime.Now;
                    _db.SaveChanges();
                }
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    message = "Gelen davet reddedildi.",
                };
            }
        );

        public ResponseModel CancelCompanyInviteById(string companyInviteId) => _responseHelper.TryCatch(
            "CompanyInviteRepository.CancelCompanyInviteById",
            () =>
            {
                ResponseModel response = _companyInviteQuery.GetCompanyInviteById(companyInviteId);
                if (response.status_code == StatusCodes.Status400BadRequest)
                {
                    return response;
                }
                company_invite companyInvite = response.data!;
                if (companyInvite.is_accepted == true)
                {
                    return new ResponseModel()
                    {
                        message = "İptal etmek istediğiniz daveti firma önceden kabul etti.",
                    };
                }
                if (companyInvite.is_accepted == null)
                {
                    companyInvite.is_accepted = false;
                    _db.SaveChanges();
                }
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    message = "Gönderilen davet iptal edildi.",
                };
            }
        );
    }
}
