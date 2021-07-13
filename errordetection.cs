using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace error
{
    class Program
    {
        public static void errordetection(string mystring)
        {
            string storee = "";
            string checkk = "";            
            int counter = 1;
            bool errorr = false;
            Stack<string> myStack = new Stack<string>();
            Stack<int> counterer = new Stack<int>();
            for (int i = 0; i < mystring.Length; i++)
            {
                if (mystring[i] == '\n')
                    counter++;
                if (mystring[i] == '<')
                {
                    storee = "";
                    checkk = "";
                    i++;
                    if (mystring[i] == '/')
                    {
                        while (mystring[i] != ' ' && mystring[i + 1] != '>')
                        {
                            i++;
                            checkk += mystring[i];
                        }
                        if (myStack.Peek() == checkk)
                        {
                            myStack.Pop();
                            counterer.Pop();
                        }
                        else
                        {
                            errorr = true;
                            break;
                        }
                    }
                    else if (mystring[i] != '!' && mystring[i] != '?')
                    {
                        while (mystring[i] != ' ' && mystring[i] != '>')
                        {
                            storee += mystring[i];
                            i++;
                        }
                        while (mystring[i] != '>')
                        {
                            i++;
                        }
                        if (mystring[i - 1] != '/')
                        {
                            myStack.Push(storee);
                            counterer.Push(counter);
                        }
                    }
                }
            }
            if (errorr == true || myStack.Count != 0)
            {
                if (errorr == false)
                {
                    while (myStack.Count != 0 && counterer.Count != 0)
                    {
                        Console.WriteLine("error occur at line " + counterer.Pop());
                    }
                }
                else
                    Console.WriteLine("error occur at line " + counterer.Pop());
            }
            else
                Console.WriteLine("no error");
            Console.Read();
        }

        static void Main(string[] args)
        {
            string sstring = @"<?xml version=1.0 encoding=UTF-8?>
<element />
<users>
<user>
<id>1</id>
<element />
<name>user1</name>
<posts>
<post>
</post>
<post>
</post>
</posts>
<followers>
<follower>
<name>2</name>
</follower>
<follower>
<id>4</id>
</follower>
</followers>
</user>
</users>";
            errordetection(sstring);
        }
    }
}
