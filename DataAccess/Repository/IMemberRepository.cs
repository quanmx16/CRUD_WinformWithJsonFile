using BusinessObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        public MemberObject CheckLogin(string email, string password);
        public bool UpdateMemberInformation(MemberObject oldInfor, MemberObject newInfor);
        public int GetLastMemberID();
        public void CreateNewMember(MemberObject newMember);
        public List<MemberObject> GetAllMember();
        public List<MemberObject> GetAllMemberSortedByName();
        public bool IsEmailHasBeenUsed(string email);
        public bool DeleteMemberById(int id);
        public MemberObject GetMemberById(int id);

        public HashSet<string> GetAllMemberCountry(List<MemberObject> members);
        public HashSet<string> GetAllMemberCity(List<MemberObject> members);
        public HashSet<string> GetAllMemberCityByCountry(string country, List<MemberObject> members);
        public List<MemberObject> GetMembersByCountry(string country, List<MemberObject> members);
        public List<MemberObject> GetMembersByCity(string city, List<MemberObject> members);
        public List<MemberObject> GetMembersByCountryAndCity(string country, string city, List<MemberObject> members);
        public List<MemberObject> SortMemberByName(List<MemberObject> mems);
        public List<MemberObject> SearchMemberByName(string name);
        public List<MemberObject> SearchMemberById(int id);




    }
}
