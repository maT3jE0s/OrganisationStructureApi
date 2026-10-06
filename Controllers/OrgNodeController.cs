using Microsoft.AspNetCore.Mvc;
using OrganisationStructureApi.Dtos;
using OrganisationStructureApi.Models;
using OrganisationStructureApi.Services;

namespace OrganisationStructureApi.Controllers
{
    [ApiController]
    public abstract class OrgNodeController(IOrgNodeService orgNodeService, OrgNodeType type) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrgNodeResponse>>> GetOrgNodes()
        {
            return Ok(await orgNodeService.GetAllOrgNodesAsync(type));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrgNodeResponse?>> GetOrgNode(int id)
        {
            return Ok(await orgNodeService.GetOrgNodeByIdAsync(type, id));
        }

        [HttpPost]
        public async Task<ActionResult<OrgNodeResponse>> CreateOrgNode(OrgNodeRequest orgNode)
        {
            var created = await orgNodeService.AddOrgNodeAsync(type, orgNode);
            return CreatedAtAction(nameof(GetOrgNode), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrgNodeResponse>> UpdateOrgNode(int id, OrgNodeRequest orgNode)
        {
            return Ok(await orgNodeService.UpdateOrgNodeAsync(type, id, orgNode));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrgNode(int id)
        {
            await orgNodeService.DeleteOrgNodeAsync(type, id);
            return NoContent();
        }

    }
}
