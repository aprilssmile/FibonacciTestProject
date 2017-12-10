using System.Net;
using System.Net.Http;
using System.Web.Http;
using FibbonacciCalculations;
using FibbonacciCalculations.DTO;

namespace WebApiApp1
{
    public class FibonacciController : ApiController
    {
        private readonly FibonacciService _fibonacciService;

        public FibonacciController(FibonacciService fibonacciService)
        {
            _fibonacciService = fibonacciService;
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]CalculationDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            _fibonacciService.Handle(dto);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
