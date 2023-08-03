using CurriculumWebAPI.App.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumWebAPI.App.Controllers
{
    [ApiController]
    [Route("V1/[controller]")]
    public class FormacaoController : ControllerBase
    {
        public FormacaoController()
        {

        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddFormacao([FromBody] FormacaoInputModel educacaoViewModel)
        {
            try
            {

            }

            catch(Exception)
            {

            }
        }
    }
}