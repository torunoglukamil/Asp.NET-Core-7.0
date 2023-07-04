using Microsoft.AspNetCore.Mvc;
using YMA.Business.Services;
using YMA.Entities.Models;

namespace YMA.Controllers
{
    [ApiController]
    public class ExchangeApiController
    {
        private readonly ExchangeService _exchangeService;

        public ExchangeApiController(ExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        [HttpGet]
        [Route("api/[controller]/GetExchangeList")]
        public ResponseModel GetExchangeList() => _exchangeService.Query.GetExchangeList();
    }
}
