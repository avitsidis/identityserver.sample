using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;

namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("input or output file is missing");
                return;
            }
            string input = args[0];
            string output = args[1];
            var users = File.ReadAllLines(input);
            List<string> usersout = new List<string>(users.Length);
            foreach (var user in users)
            {
                usersout.Add(uEmail(user));
            }
            File.WriteAllLines(output, usersout);
        }

        public static string uEmail(string uid)
        {
            DirectorySearcher dirSearcher = new DirectorySearcher();
            DirectoryEntry entry = new DirectoryEntry(dirSearcher.SearchRoot.Path);
            //dirSearcher.Filter = "(&(objectCategory=person)(objectClass=user)(cn="+uid+"*))";
            dirSearcher.Filter = String.Format("(sAMAccountName={0})", uid);
            SearchResult searchResult = dirSearcher.FindOne();
            try
            {
                return String.Format("{0} = {2} <{1}>", uid, searchResult.Properties["mail"][0].ToString().ToLower(), searchResult.Properties["name"][0]);
            }
            catch
            {
                return String.Format("{0} = {2} <{1}@sodexo.com>", uid, uid, uid);
            }

        }
    }
}