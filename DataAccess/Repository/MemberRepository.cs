using BusinessObject;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public List<MemberObject> SearchMemberByName(string name)
        {
            List<MemberObject> result = new List<MemberObject>();

            List<MemberObject> members = MemberDAO.getAllMember();
            for (int i = 0; i < members.Count; i++)
            {
                if (members[i].MemberName.Contains(name) && members[i].Email != "admin@fstore.com")
                {
                    result.Add(members[i]);
                }
            }
            result = SortMemberByName(result);
            return result;

        }
        public List<MemberObject> SearchMemberById(int id)
        {

            List<MemberObject> result = new List<MemberObject>();
            List<MemberObject> members = MemberDAO.getAllMember();

            MemberObject member = null;
            for (int i = 0; i < members.Count; i++)
            {
                if (members[i].MemberID == id)
                {
                    result.Add(members[i]);
                }
            }

            return result;

        }
        public List<MemberObject> SortMemberByName(List<MemberObject> mems)
        {
            if (mems == null || mems.Count == 0) return mems;
            for (int i = 0; i < mems.Count - 1; i++)
            {
                int max = i;
                for (int j = i + 1; j < mems.Count; j++)
                {
                    if (mems[j].MemberName.CompareTo(mems[max].MemberName) > 0)
                    {
                        max = j;
                    }
                    if (max != i)
                    {
                        MemberObject tmp = mems[max];
                        mems[max] = mems[i];
                        mems[i] = tmp;
                    }
                }

            }
            return mems;
        }
        public List<MemberObject> GetMembersByCountry(string country, List<MemberObject> members)
        {
            List<MemberObject> result = new List<MemberObject>();

            for (int i = 0; i < members.Count; i++)
            {
                if (members[i].Country == country)
                {
                    result.Add(members[i]);
                }
            }
            result = SortMemberByName(result);
            return result;

        }
        public List<MemberObject> GetMembersByCity(string city, List<MemberObject> members)
        {
            List<MemberObject> result = new List<MemberObject>();

            for (int i = 0; i < members.Count; i++)
            {
                if (members[i].City == city)
                {
                    result.Add(members[i]);
                }
            }
            result = SortMemberByName(result);

            return result;

        }
        public List<MemberObject> GetMembersByCountryAndCity(string country, string city, List<MemberObject> members)
        {
            List<MemberObject> result = new List<MemberObject>();

            for (int i = 0; i < members.Count; i++)
            {
                if (members[i].Country == country && members[i].City == city)
                {
                    result.Add(members[i]);
                }
            }
            result = SortMemberByName(result);

            return result;

        }
        public HashSet<string> GetAllMemberCountry(List<MemberObject> members)
        {
            HashSet<string> result = new HashSet<string>();
            for (int i = 0; i < members.Count; i++)
            {
                if (members[i].Email != "admin@fstore.com")
                    result.Add(members[i].Country);
            }
            return result;
        }
        public HashSet<string> GetAllMemberCity(List<MemberObject> members)
        {
            HashSet<string> result = new HashSet<string>();
            for (int i = 0; i < members.Count; i++)
            {
                if (members[i].Email != "admin@fstore.com")
                    result.Add(members[i].City);
            }
            return result;
        }
        public HashSet<string> GetAllMemberCityByCountry(string country, List<MemberObject> members)
        {
            HashSet<string> result = new HashSet<string>();
            for (int i = 0; i < members.Count; i++)
            {
                if (members[i].Country == country && members[i].Email != "admin@fstore.com")
                    result.Add(members[i].City);
            }
            return result;
        }
        public bool DeleteMemberById(int id)
        {
            List<MemberObject> members = MemberDAO.getAllMember();

            for (int i = 0; i < members.Count; i++)
            {
                if (members[i].MemberID == id)
                {
                    members.RemoveAt(i);
                    MemberDAO.WriteMembersInforToFile(members);
                    return true;
                }
            }
            return false;
        }
        public MemberObject GetMemberById(int id)
        {
            List<MemberObject> members = MemberDAO.getAllMember();

            for (int i = 0; i < members.Count; i++)
            {
                if (members[i].MemberID == id)
                {
                    return members[i];
                }
            }
            return null;
        }
        public bool IsEmailHasBeenUsed(string email)
        {
            List<MemberObject> members = MemberDAO.getAllMember();
            for (int i = 0; i < members.Count; i++)
            {
                if (members[i].Email == email) { return true; }
            }
            return false;
        }
        public MemberObject CheckLogin(string email, string password)
        {
            List<MemberObject> members = MemberDAO.getAllMember();
            try
            {
                List<MemberObject> member = members.Where(mem => mem.Email == email &&
                                     mem.Password == password).ToList();
                if (member.Count > 0)
                {
                    return member[0];
                }
            }
            catch (ArgumentNullException ex)
            {
                return null;
            }

            return null;
        }

        public void CreateNewMember(MemberObject newMember)
        {

            List<MemberObject> members = MemberDAO.getAllMember();
            members.Add(newMember);
            MemberDAO.WriteMembersInforToFile(members);

        }

        public List<MemberObject> GetAllMember()
        {
            return MemberDAO.getAllMember();
        }

        public int GetLastMemberID()
        {
            int max = 0;
            List<MemberObject> members = MemberDAO.getAllMember();
            for (int i = 0; i < members.Count; i++)
            {
                if (max < members[i].MemberID)
                {
                    max = members[i].MemberID;
                }
            }
            return max;
        }

        public bool UpdateMemberInformation(MemberObject oldInfor, MemberObject newInfor)
        {
            bool result = false;
            List<MemberObject> members = MemberDAO.getAllMember();
            oldInfor.MemberName = newInfor.MemberName;
            oldInfor.Country = newInfor.Country;
            oldInfor.City = newInfor.City;
            oldInfor.Email = newInfor.Email;
            oldInfor.Password = newInfor.Password;
            MemberDAO.WriteMembersInforToFile(members);
            result = true;
            return result;
        }
        public List<MemberObject> GetAllMemberSortedByName()
        {

            List<MemberObject> members = MemberDAO.getAllMember();
            for (int i = 0; i < members.Count - 1; i++)
            {
                int max = i;
                for (int j = i + 1; j < members.Count; j++)
                {
                    if (members[j].MemberName.CompareTo(members[max].MemberName) > 0)
                    {
                        max = j;
                    }
                    if (max != i)
                    {
                        MemberObject tmp = members[max];
                        members[max] = members[i];
                        members[i] = tmp;
                    }
                }

            }
            return members;
        }


    }
}
