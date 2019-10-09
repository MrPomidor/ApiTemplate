﻿using System;
using System.Collections.Generic;
using System.Text;
using App.UserSupport.Models;

namespace App.UserSupport.Models
{
    public class Handling 
    {//unique id for Handling
        public int Id { get; set; }
        //context of Handling
        public List<string> context { get; set; } = new List<string>();
        //Status 
        public bool status { get; set; }
        private Client client_handling;
        public Handling(Client client,string firstmessage,int id) 
        {
            Id = id;
            status = false;
            client_handling = client;
            WriteMessage(client_handling.Name, firstmessage);
        }
        public void Set_Handling_Status_Completed() => status = true;
        public void WriteMessage(string mess) {
            if (status == false)
                context.Add(client_handling.Name + " :  " + mess +"   " );
        }
        public void WriteAnswer(string answer)
        {
            if (status == false)
                context.Add("   Moderator :  " + answer );
        }
        public string Get_Handling_10_last_messages()
        {
            string temp = "";
            List<string> somelist = context;
            somelist.Reverse();
            int i = context.Count-1;
            int j = 0;
            while (i >=0 )
            {
                if (j < 10)
                temp += somelist.ToArray()[i];
                i--;
                j++;
            }
            return temp;
        }
        //If closed handling, no display in active handlings
        public override string ToString()
        {
            string temp = "";
            if (status == false)
            {
                foreach (string a in context)
                    temp += a;
            }
            return temp;
            }
        public void WriteMessage(string Name, string mess)
        {
            if (status == false)
                context.Add(Name + " :  " + mess + "   ");
        }
    }
}
