 public void Mini(Node k)
      {
        
          if(k.getName()!="!--")//remove all comments
          {
            if(isOneLine==true)
            {
              Console.Write("<"+k.getName());
          if(k.getCountAttr()!=0)
            {
                foreach(string s in k.getAllAttr())
                {
                    Console.Write(" "+s);
                }
            }
            Console.Write(" />");  
            }
            else 
            {
                  Console.Write( "<"+k.getName());
         

          if(k.getCountAttr()!=0)
          {
              Console.Write(" ");
             foreach(string s in k.getAllAttr())
             {
              Console.Write(s+" ");
             }
         }
          Console.Write(k.getValue());
		   List<Node> child =k.getAllCh();
          if (k.getCountCh() !=0)
          {
        
             for(int i=0;i<k.getCountCh();i++)
               {
                 Mini(child[i]);
               }
          
           }

          
          Console.Write("</"+k.getName()+">");
            } 
            


           
          }
        
           
            
   

      } 
      public void Minifying()
        {
               Mini(root);
        } 
