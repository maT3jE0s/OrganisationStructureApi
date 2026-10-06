using Microsoft.AspNetCore.Mvc;
using OrganisationStructureApi.Models;
using OrganisationStructureApi.Services;

namespace OrganisationStructureApi.Controllers
{
    [Route("api/departments")]
    public class DepartmentsController(IOrgNodeService orgNodeService)
        : OrgNodeController(orgNodeService, OrgNodeType.Department)
    {
    }
}
