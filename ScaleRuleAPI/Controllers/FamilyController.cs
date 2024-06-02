using ScaleRuleAPI.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ScaleRuleAPI.Services;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;

namespace ScaleRuleAPI.Controllers
{
    [ApiController]
    [Route("api/family")]
    public class FamilyController : ControllerBase
    {
        public FamilyController() { }

        // GET: api/family
        [DisableCors]
        [HttpGet()]
        public string Get()
        {
            List<Option> result = FamilyService.Instance.GetAll();
            string retVal = JsonConvert.SerializeObject(result);
            return retVal;
        }
    }
}
