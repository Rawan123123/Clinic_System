using clinic_system.Exeptions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace clinic_system.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        protected void ValidateModel()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );
                throw new ValidationException("Validation Failed" ,  errors );
            }
        }
    }
}
