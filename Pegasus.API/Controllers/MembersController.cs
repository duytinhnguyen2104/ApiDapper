using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pegasus.API.Interfaces;
using Pegasus.Model;

namespace Pegasus.API.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberRepository _memberRepository;

        public MembersController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        // POST api/GetMembersAll
        [HttpPost("GetMembersAll")]
        public ActionResult<IEnumerable<Member>> GetMembersAll([FromBody] Member member)
        {
            var ResultRepo = _memberRepository.GetMembers(member);
            return Ok(ResultRepo);
        }
        // GET api/GetMember/1
        [HttpGet("GetMember")]
        public ActionResult<Member> GetMember(int memberID)
        {
            if (memberID <= 0) return new Member() ;
            var ResultRepo = _memberRepository.GetMember(memberID);
            return Ok(ResultRepo);
        }
        // POST api/AddMember
        [HttpPost("AddMember")]
        public IActionResult AddMember([FromBody] Member member)
        {
            if (member == null) return BadRequest();
            var result = _memberRepository.AddMember(member)    ;
            Member memberToReturn = new Member();
            if (result < 0)
                return NoContent();
            else
                return CreatedAtRoute("GetMember", new { id = result }, memberToReturn)
                ;
        }
        // PUT api/UpdateMember
        [HttpPost("UpdateMember")]
        public IActionResult UpdateMember([FromBody] Member member)
        {
            if (member == null) return BadRequest();
            var resultrepo = _memberRepository.UpdateMember(member);
            if (!resultrepo)
                return NotFound();
            else
                return CreatedAtRoute("GetMember", new { id = member.MemberID }, member)
            ;
        }
        // DELETE api/DeleteMember
        [HttpDelete("DeleteMember")]
        public IActionResult DeleteMember(int memberid)
        {
            var delRepo = _memberRepository.DeleteMember(memberid);
            if (!delRepo)
                throw new Exception($"Deleting Member with {memberid} failed.");
             return Accepted();
            
        }
    }
}