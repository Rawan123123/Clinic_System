using clinic_system.Controllers.Base;
namespace ClinicSystem.Tests.Controllers
{
    internal class TestableController : BaseController
    {
        public int CallGetCurrentUserId() => GetCurrentUserId();
        public void CallValidateModel() => ValidateModel();
    }
}
