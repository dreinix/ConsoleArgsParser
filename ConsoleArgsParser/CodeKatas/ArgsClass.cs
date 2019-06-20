using System;
using System.Collections.Generic;
using System.Text;

namespace CodeKatas
{
    public class InvalidSchemeException : Exception { };
    public class NoSchemeException : Exception { };
    public class InvalidArgException : Exception { };


    public class ArgsClass
    {   
        private Dictionary<string, string> schems = new Dictionary<string, string>();

        public void AddScheme<S>(Dictionary<string, S> scheme)
        {
            if (scheme.Count == 0 || scheme == null)
                throw new NoSchemeException();

            foreach (var command in scheme)
            {
                if (command.Key.Length != 2 || command.Key[0] != '-')
                    throw new InvalidSchemeException();

                else
                {
                    Type ParameterType = default(S).GetType();
                    if (ParameterType == typeof(bool))
                        schems.Add(command.Key, "false");
                    else
                        schems.Add(command.Key, "");
                }
            }
        }
        public Dictionary<string,string> GetSchemes()
        {
            return schems;
        }
        private string[] chain;

        public Dictionary<string, string> GeneralSParse(string args)
        {
            var dict = new Dictionary<string, string>();
            if (args != null || args.Length == 0)
            {
                chain = args.Split(" ");
                int cant = chain.Length;
                //if()
                for (int i = 0; i < cant; i++)
                {
                    if (schems.ContainsKey(chain[i]))
                    {
                        bool bollean = false;
                        if (schems[chain[i]] == "false" || schems[chain[i]] == "true")
                            bollean = true;
                        if (!bollean)
                        {
                            if (i == cant - 1)
                                throw new InvalidArgException();
                            if (i != cant - 1 && chain[i + 1][0] == '-')
                                throw new InvalidArgException();

                        }
                        if (chain[i].Length != 2 && chain[i][0] != '-')
                        {
                            throw new InvalidArgException();
                        }/*
                        if ((schems[chain[i]]=="false" || schems[chain[i]] == "true"))
                        {   //if(chain[i + 1][0] == '-')
                                throw new InvalidArgException();
                        }*/

                        //dict.Remove(chain[i]);
                        if (schems[chain[i]] == "false")
                            dict.Add(chain[i], "true");
                        else
                        {
                            string value = chain[i + 1].ToString();
                            dict.Add(chain[i], value);
                            i++;
                        }

                    }
                    else
                    {
                        throw new InvalidArgException();
                    }
                }
            }
            return dict;
        }


        public Dictionary<string, string> GeneralSParse(string[] args)
        {
            var dict = new Dictionary<string, string>();           
            if (args != null || args.Length==0)
            {                  
                int cant = chain.Length;
                //if()
                for(int i = 0;i< cant; i++)
                {   
                    if (schems.ContainsKey(chain[i]))
                    {
                        bool bollean = false;
                        if (schems[chain[i]] == "false" || schems[chain[i]] == "true")
                            bollean = true;
                        if (!bollean)
                        {
                            if (i == cant - 1)
                                throw new InvalidArgException();
                            if (i != cant - 1 && chain[i + 1][0] == '-')
                                throw new InvalidArgException();

                        }
                        if(chain[i].Length != 2 && chain[i][0] != '-')
                        {
                            throw new InvalidArgException();
                        }/*
                        if ((schems[chain[i]]=="false" || schems[chain[i]] == "true"))
                        {   //if(chain[i + 1][0] == '-')
                                throw new InvalidArgException();
                        }*/
                        dict.Remove(chain[i]);
                        if (schems[chain[i]] == "false")
                            dict.Add(chain[i], "true");
                        else
                        {
                            string value = chain[i + 1].ToString();
                            dict.Add(chain[i],value);
                            i++;
                        }                           
                        
                    }
                    else
                    {
                        throw new InvalidArgException();
                    }
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
