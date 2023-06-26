using CurriculumWebAPI.App.ViewModel;
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



        [HttpPost]
        public async Task<IActionResult> AddFormacao([FromBody] FormacaoInputModel educacaoViewModel)
        {
            if(ModelState.IsValid)
            {

            }

            return CreatedAtAction(nameof(AddFormacao), educacaoViewModel);
        }
    }
}
