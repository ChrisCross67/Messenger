using System.Collections.ObjectModel;
using System.Net;

namespace Messenger
{
    public class MemberList : ObservableCollection<Member>
    {

        public void AddMember(Member member)
        {
            int index = FindMember(member.IPAddress);
            if (index == -1)
            {
                base.Add(member);
            }
            else
            {
                base[index] = member;
            }
        }

        public void RemoveMember(IPAddress ipAddress)
        {
            int index = FindMember(ipAddress);
            if (index != -1)
            {
                base.RemoveAt(index);
            }
        }

        public int FindMember(IPAddress ipAddress)
        {
            for (int i = 0; i < Count; i++)
            {
                Member existed_member = base[i] as Member;
                if (existed_member.IPAddress.Equals(ipAddress))
                {
                    return i;
                }
            }
            return -1;
        }

        public Member GetMember(IPAddress ipAddress)
        {
            int index = FindMember(ipAddress);
            if (index != -1)
            {
                return base[index];
            }
            return null;
        }
    }
}
