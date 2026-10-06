using Microsoft.AspNetCore.Mvc;
using OrganisationStructureApi.Models;
using OrganisationStructureApi.Services;

namespace OrganisationStructureApi.Controllers
{
    [Route("api/projects")]
    public class ProjectsController(IOrgNodeService orgNodeService)
        : OrgNodeController(orgNodeService, OrgNodeType.Project)
    {
    }
}
