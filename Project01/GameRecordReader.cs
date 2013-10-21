/* 
 * Author: Qi Zhang
 * Date: 2013
 * Description: project 01
 */ 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01
{
    /// <summary>
    /// the GameRecordReader Class
    /// it is used to read and update the game score to TXT file
    /// </summary>
    public class GameRecordReader
    {
        /// <summary>
        /// private field variable to instance the file's path
        /// </summary>
        private string path;

        /// <summary>
        /// read-write proprety to get and set file's path
        /// </summary>
        public string Path { set { path = value; } get { return path; } }

        /// <summary>
        /// default constractor
        /// </summary>
        public GameRecordReader() 
        {
            
        }

        /// <summary>
        /// constractor with parameter
        /// </summary>
        /// <param name="fileName">file's name</param>
        public GameRecordReader(string fileName)
        {
            path = "..\\..\\GameRecord\\" + fileName + ".txt";
            if (File.Exists(path) == false) { File.Create(path); }
        }

        /// <summary>
        /// create the file if the file is not exist
        /// </summary>
        /// <param name="fileName">file name</param>
        public void CreateFile(string fileName)
        {
           path = "..\\..\\GameRecord\\"+fileName+".txt";
            if (File.Exists(path) == false) { File.Create(path); }
            
        }

        /// <summary>
        /// update the new score to the file
        /// </summary>
        /// <param name="score">new score</param>
        /// <param name="current">current time</param>
        public void UpdateNewScoreToFile(float score, DateTime current)
        {
            int index = 0;
            string newScore=Math.Round(score, 2, MidpointRounding.AwayFromZero).ToString();
            List<string> newList = new List<string>();
            newList = GetOldList();
            if (newList.Count != 0)
            {
                while (index< newList.Count )
                {
                    if (float.Parse(newScore.ToString()) < float.Parse(newList.ElementAt(index).Split(' ').ElementAt(0)))
                    {
                        index++;
                    }
                    else
                    {
                        break;
                    }
                    
                }
            }
            else
            {
                index = 0;
            }

           
            newList.Insert(index, newScore + " "+current.ToShortTimeString()+"@"+current.ToShortDateString());            
                
            newList = newList.Select(str => (newList.IndexOf(str)+1)+") " + str).ToList();

            newList.Insert(0, "Order | Score | Time of play");

            newList.Insert(1, "----------------------------");
           
            
            
            File.WriteAllLines(path, newList);            

        }

        /// <summary>
        /// getOldList method
        /// get the information from the TXT file
        /// </summary>
        /// <returns>return the information list</returns>
        public List<string> GetOldList()
        {
            List<string> oldList=new List<string>();

            if (new System.IO.FileInfo(path).Length != 0)
            {
                
                string line;

                // Read the file and display it line by line.
                System.IO.StreamReader file =
                   new System.IO.StreamReader(path);
                while ((line = file.ReadLine()) != null)
                {
                    oldList.Add(line);
                }

                file.Close();

            }
            if (oldList.Count!=0 && oldList.First().Contains("play"))
            {
                oldList.RemoveAt(0);
                oldList.RemoveAt(0);
                oldList = oldList.Select(str => DeleteOrderNumber(str)).ToList();
            }
            
           
            return oldList;
        }

        /// <summary>
        /// rebuild the information without order number
        /// </summary>
        /// <param name="str">initial the information</param>
        /// <returns>new string without order number</returns>
        private string DeleteOrderNumber(string str)
        {
            string newstr=null;
            List<string> charArray = new List<string>();
            charArray = str.Split(' ').Skip(1).ToList();
            for (int i = 0; i < charArray.Count; i++)
            {
                newstr += charArray.ElementAt(i).ToString() + " ";
            }

                return newstr;
        }
        
        


    }
}
