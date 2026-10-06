using Microsoft.AspNetCore.Mvc;
using OrganisationStructureApi.Models;
using OrganisationStructureApi.Services;

namespace OrganisationStructureApi.Controllers
{
    [Route("api/companies")]
    public class CompaniesController(IOrgNodeService orgNodeService)
        : OrgNodeController(orgNodeService, OrgNodeType.Company)
    {
    }
}
