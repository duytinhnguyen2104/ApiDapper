using Pegasus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pegasus.API.Interfaces
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetMembers(Member paramaters);
        Member GetMember(int _memberID);
        int AddMember(Member _member);
        bool UpdateMember(Member _member);
        bool DeleteMember(int _memberID);

    }
}
