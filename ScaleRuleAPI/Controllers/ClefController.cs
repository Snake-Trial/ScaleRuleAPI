using ScaleRuleAPI.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ScaleRuleAPI.Services;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;

namespace ScaleRuleAPI.Controllers
{
    [ApiController]
    public class ClefController : ControllerBase
    {
        public ClefController(){ }
        
        // GET: api/clefs
        [Route("api/clefs")]
        [DisableCors]
        [HttpGet()]
        public string Get()
        {
            List<Option> result = ClefService.Instance.GetAllOptions();
            string retVal = JsonConvert.SerializeObject(result);
            return retVal;
        }

        // GET: api/clefs
        [Route("api/clefs/{id}")]
        [DisableCors]
        [HttpGet()]
        public string GetById(int id)
        {
            Clef? result = ClefService.Instance.GetById(id);
            string retVal = JsonConvert.SerializeObject(result);
            return retVal;
        }

    }
}
