using System;
using System.Collections.Generic;
using System.Text;

namespace CodeKatas
{
    public class InvalidSchemeException : Exception { };
    public class NoSchemeException : Exception { };
    public class InvalidArgException : Exception { };


    public class ArgsClass<S>
    {   
        
        public bool IsValueType<T>(T obj)
        {
            return typeof(S).IsValueType;
        }


        public Dictionary<string, S> GeneralSParse(Dictionary<string, S> scheme, string args)
        {
            var dict = new Dictionary<string, S>();

            if (scheme.Count == 0 || scheme == null)
                throw new NoSchemeException();
            
            foreach (var command in scheme)
            {   
                if (command.Key.Length != 2 || command.Key[0] != '-')
                    throw new InvalidSchemeException();

                else
                {

                }
            }
            if (args != null)
            {
                string[] chain = args.Split(" ");
                foreach (var item in chain)
                {

                }
            }
           return dict;
        }

        public Dictionary<string, bool> BoolArgs(string[] scheme, string args)
        {
            var dict = new Dictionary<string, bool>();

            if (scheme.Length == 0 || scheme == null)
                throw new NoSchemeException();

            foreach (string command in scheme)
            {
                if (command.Length != 2 || command[0] != '-')
                    throw new InvalidSchemeException();
                else
                {
                    dict.Add(command, false);
                }
            }
            if (args!=null)
            {
                string[] chain = args.Split(" ");
                foreach (var item in chain)
                {   
                    if(item.Length!=2 && item[0]!='-')
                        throw new InvalidArgException();

                    if (dict.ContainsKey(item))
                    {
                        dict.Remove(item);
                        dict.Add(item, true);
                    }
                    else
                    {
                        throw new InvalidArgException();
                    }
                    
                }
            }
            return dict;
        }
    }
}
