 public int Depth( ref Node n, ref Node s)
        {
            if(n.getName()  ==  s.getName()) 
            {
                return 0;
            }
            else
            {
                 
//                   if (s.getCountCh() != 0)
//                   {
//                       ++depth;
//                   }
		    depth++;
                 List<Node> data =s.getAllCh();
        
                 for(int i=0; i<s.getCountCh();i++)
                  {  
                   if(data[i].getName() == n.getName()) return depth;
                  } 
                 
                 for(int j=0;j<s.getCountCh();j++)
                  {
                  Node d=data[j];
                  //if(s.getCountCh()!=0)
                 return  Depth(ref n, ref d);  //error data[j] 
        
                  }
                  return 1;
            }
   

        }
      public void Space(Node k)
      {
         int n=0;
         depth=0;
         n=Depth( ref k,ref root);
          // Console.Write(n);
         for(int i=0;i<=n;i++)
          {
            Console.Write(" ");
          }
      }
      public void print(Node r ) 
      {  if(isStartTag==false)//// print startinftags
          {List<string> Tags=r.getStartingTags();
           for(int i=0;i<r.getCountStartingTags();i++)
           {
            Console.WriteLine(Tags[i]);
           }
           isStarting==true;
          }
         Space(r);
         if(r.getName()=="!--")//to print comment
         {
         Console.Write("<"+r.getName()+" "+r.getAllAttr().Dequeue());
         }
         else if(isOneLine()==true)/// to print <example />
         {
          Console.Write("<"+r.getName());
          if(r.getCountAttr()!=0)
            {
                foreach(string s in r.getAllAttr())
                {
                    Console.Write(" "+s);
                }
            }
            Console.Write(" />");

         }
         else
         {
            Console.Write("<"+r.getName());
            if(r.getCountAttr()!=0)
            {
                foreach(string s in r.getAllAttr())
                {
                    Console.Write(" "+s+">");
                }
            }
          Console.Write(r.getValue());
          List<Node> child =r.getAllCh();
          if (r.getCountCh()!=0)
           {
              Console.WriteLine("\n");
             for(int i=0;i<r.getCountCh();i++)
             {
              print(child[i]);
             }
           }
          if(r.getCountCh()!=0)
             Space(r);
          Console.Write("</"+r.getName()+">");
          Console.WriteLine("\n");
           }
        }

      public void format()
        {
			print(root);
		}

         /* ////add fn.in node
 public int getCountAttr(){
           return attr.Count; 
        }*/
/* ///add fn. in node

public int getCountStartingTags(){
            return StartingTags.count;
        }*/


