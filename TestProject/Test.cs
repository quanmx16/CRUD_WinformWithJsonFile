using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Test
    {
       public static void Main()
        {
            IMemberRepository memberRepository = new MemberRepository();
            Console.WriteLine($"case wrong email and password: {memberRepository.checkLogin("a", "b")}");
            Console.WriteLine($"case correct email and wrong password: {memberRepository.checkLogin("admin@fstore.com", "b")}");
            Console.WriteLine($"case wrong email and correct password: {memberRepository.checkLogin("c", "admin@@")}");
            Console.WriteLine($"case correct email and correct password: {memberRepository.checkLogin("admin@fstore.com", "admin@@")}");

        }
    }
}
