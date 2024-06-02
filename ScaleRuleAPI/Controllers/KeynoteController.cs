using ScaleRuleAPI.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ScaleRuleAPI.Services;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;


namespace ScaleRuleAPI.Controllers
{
    [ApiController]
    [Route("api/keynotes")]
    public class KeynoteController : ControllerBase
    {
        public KeynoteController() { }  

        // GET: api/keynotes
        [DisableCors]
        [HttpGet()]
        public string Get()
        {
            List<Option> result = KeynoteService.Instance.GetAll(); 
            string retVal = JsonConvert.SerializeObject(result);
            return retVal;
        }
    }
}
