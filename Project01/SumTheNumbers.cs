/* 
 * Author: Qi Zhang
 * Date: 2013
 * Description: project 01
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01
{
    /// <summary>
    /// sumTheNumbers class, inhert from Games class
    /// game descroption
    /// show the user 5 random numbers from 100 to 999 one at a time with 2 second interval
    /// the user needs to enter the sum of some of the nubmers shown within 5s
    /// tell them if they were correct or incorrect from each game and display the correct sum on screen
    /// display a score of wins versus losses tallied acroos multiple games
    /// three levels: last two, last three,last four number showns
    /// </summary>
    public class SumTheNumbers:Games
    {
        /// <summary>
        /// default constractor
        /// set initial conditions for the game
        /// </summary>
        public SumTheNumbers()
        {
            GameName = "SumTheNumbers";           
            gameLevel = 3;
            numbers = new int[5];
            answer = new int[gameLevel];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = RandomUtil.IntWithRange(100, 999);
            }

            for (int i = 0; i < answer.Length; i++)
            {
                answer[i] = Numbers.Skip(Numbers.Count-2-i).Sum();
                
                
            }               
           

        }
        /// <summary>
        /// override the games questionText method
        /// </summary>
        /// <returns>the question information</returns>
        public override string QuestionText()
        {
            string inf=null;
            for (int i = numbers.Length - selectGameLevel - 2; i < numbers.Length; i++)
            {
                inf += Numbers.ElementAt(i);
                if (i != numbers.Length - 1)
                {
                    inf += " + ";
                }
            }
            return inf+=" is ?";
        }

        /// <summary>
        /// override the games' questionWithAnswerText() method
        /// </summary>
        /// <returns>the question and answer information</returns>
        public override string QuestionWithAnswerText()
        {
            string inf = null;
            for (int i = numbers.Length - selectGameLevel - 2; i < numbers.Length; i++)
            {
                inf += Numbers.ElementAt(i);
                if (i != numbers.Length-1)
                {
                    inf += " + ";
                }
            }
            return inf += " is " + Answer.ElementAt(selectGameLevel);
        }

        /// <summary>
        /// override the games checkAnswer method
        /// </summary>
        /// <param name="userAnswer">the user's answer</param>
        /// <returns>if user's answer is correct, return true. otherwise , return false </returns>
        public override bool CheckAnswer(int userAnswer)
        {
            answerAttempt = userAnswer;
            
            if (answerAttempt == int.Parse(Answer.ElementAt(selectGameLevel).ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
