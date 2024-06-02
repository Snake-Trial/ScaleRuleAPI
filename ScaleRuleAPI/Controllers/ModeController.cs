using ScaleRuleAPI.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ScaleRuleAPI.Services;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using System;

namespace ScaleRuleAPI.Controllers
{
    [ApiController]
    public class ModeController : ControllerBase
    {
        public ModeController() { }

        // GET: api/modes
        [Route("api/modes")]
        [DisableCors]
        [HttpGet()]
        public string Get()
        {
            List<Option> result = ModeService.Instance.GetAllOptions();
            string retVal = JsonConvert.SerializeObject(result);
            return retVal;
        }

        // GET: api/modes/family
        [Route("api/modes/{familyid}")]
        [DisableCors]
        [HttpGet()]
        public string GetByFamily(int familyid)
        {
            List<Option> result = ModeService.Instance.GetOptionsByFamilyId(familyid);
            string retVal = JsonConvert.SerializeObject(result);
            return retVal;
        }

        // GET: api/modes/action
        [Route("api/modes/action/{actionid}")]
        [DisableCors]
        [HttpGet()]
        public string GetByAction(string actionid)
        {
            string message;
            if (actionid.Equals("flush"))
            {
                ModeService.Flush();
                message = "Modes flushed";
            }
            else
            {
                message = $"Action {actionid} requested. No action taken.";
            }
            return message;
        }
    }
}
