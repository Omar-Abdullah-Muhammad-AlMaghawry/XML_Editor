
        public int Depth( ref Node n, ref Node s)
        {
            if(n.getName()  ==  s.getName()) 
            {
                return 0;
            }
            else
            {
                 
                  if (s.getCountCh() != 0)
                  {
                      ++depth;
                  }
                 List<Node> data =s.getAllCh();
        
                 for(int i=0; i<s.getCountCh();i++)
                  {  
                   if(data[i].getName() == n.getName()) return depth;
                  } 
                 
                 for(int j=0;j<s.getCountCh();j++)
                  {
                  Node d=data[j];
                  if(s.getCountCh()!=0)
                   Depth(ref n, ref d);  //error data[j] 
        
                  }
                  return depth;
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
      { 
          Space(r);
          Console.Write("<"+r.getName()+" "+r.getOneAttr()+">");
          Console.Write(r.getValue());
          List<Node> child =r.getAllCh();
          if (r.getCountCh()!=0)
           {
              Console.Write("\n");
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

      
      public void format()
        {
			print(root);
		}

         
