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

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Member>> GetMembersAll()
        {
            var ResultRepo = _memberRepository.GetMembers(new object[] { });

            return Ok(ResultRepo);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> GetMember(int memberID)
        {
            var ResultRepo = _memberRepository.GetMembers(memberID);
            return Ok(ResultRepo);
        }
        // POST api/values
        [HttpPost]
        public IActionResult AddMember([FromBody] Member member)
        {
            if (member == null) return BadRequest();
            var result = _memberRepository.AddMember(member);
            Member memberToReturn = new Member();
            if (result < 0)
                return NoContent();
            else
                return CreatedAtRoute("GetMember", new { id = result }, memberToReturn)
                ;
        }

        // PUT api/values/5
        [HttpPost]
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

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMember(int memberid)
        {
            var delRepo = _memberRepository.DeleteMember(memberid);
            if (!delRepo)
                throw new Exception($"Deleting Member with {memberid} failed.");
            return NoContent();
        }
    }
}