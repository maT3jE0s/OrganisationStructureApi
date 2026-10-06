using Microsoft.AspNetCore.Mvc;
using OrganisationStructureApi.Models;
using OrganisationStructureApi.Services;

namespace OrganisationStructureApi.Controllers
{
    [Route("api/divisions")]
    public class DivisionsController(IOrgNodeService orgNodeService)
        : OrgNodeController(orgNodeService, OrgNodeType.Division)
    {
    }
}
