using ScaleRuleAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace ScaleRuleAPI.Controllers
{
    [ApiController]
    public class ScaleController : ControllerBase
    {
        public ScaleController() { }

        // GET: api/scale/notes
        [Route("api/scale/notes/{keynote}/{mode}/{clef}")]
        [DisableCors]
        [HttpGet()]
        public string GetNotes(int keynote, int mode, int clef)
        {
            //return $"{keynote}:{mode}";
            Scalemaker sm = new();
            return Scalemaker.GetScaleNotes(keynote, mode, clef);
        }

    }
}