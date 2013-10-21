/* 
 * Author: Qi Zhang
 * Date: 2013
 * Description: project 01
 */ 
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01
{
    /// <summary>
    /// the abstract class games
    /// other special game will use this base class
    /// </summary>
    public abstract class Games
    {
        /// <summary>
        /// field variable with protected to instance the select game level
        /// </summary>
        protected int selectGameLevel;
        /// <summary>
        /// field variable with protected to instance game levels in total
        /// </summary>
        protected int gameLevel;
        /// <summary>
        /// field variable with protected to instance numbers
        /// </summary>
        protected int[] numbers;

        /// <summary>
        /// field variable with protected to instance correct answer
        /// </summary>
        protected int[] answer;
        /// <summary>
        /// field variable with protected to instance the user answer
        /// </summary>
        protected int answerAttempt;
        /// <summary>
        /// field variable with protected to instance bool to show whether the game is played
        /// </summary>
        protected bool isPlayed=false;
        /// <summary>
        /// field variable with protected to instance the game name
        /// </summary>
        protected string gameName;
       
        /// <summary>
        /// read- write property to set and get selectGameLevel 
        /// </summary>
        public int SelectGameLevel 
        { set { selectGameLevel=value; } get { return selectGameLevel; } }

        /// <summary>
        /// read- write property to set and get gameLevel 
        /// </summary>
        public int GameLevel { set { gameLevel = value; } get { return gameLevel; } }

        /// <summary>
        /// read-only property to  get numbers 
        /// </summary>
        public ReadOnlyCollection<int> Numbers { get{return new ReadOnlyCollection<int>(numbers);} }

        /// <summary>
        /// read- write property to set and get answer
        /// </summary>
        public int[] Answer { set { answer = value; } get { return answer; } }

        /// <summary>
        /// read- write property to set and get answerattempt 
        /// </summary>
        public int AnswerAttempt { set { selectGameLevel = value; } get { return answerAttempt; } }
        /// <summary>
        /// read- write property to set and get IsPlayed 
        /// </summary>
        public bool IsPlayed { set { isPlayed = value; } get { return isPlayed; } }

        /// <summary>
        /// read- write property to set and get GameName
        /// </summary>
        public string GameName { set { gameName = value; } get { return gameName; } }
       
        /// <summary>
        /// abstract method of QuestionText
        /// </summary>
        /// <returns>question information</returns>
        public abstract string QuestionText();

        /// <summary>
        /// abstract method of Question with answer text
        /// </summary>
        /// <returns>question and correct answerinformation </returns>
        public abstract string QuestionWithAnswerText();

        /// <summary>
        /// abstract method of check answer
        /// </summary>
        /// <param name="userAnswer">the user answer</param>
        /// <returns>if user answer is correct ,return true. otherwise , return false</returns>
        public abstract bool CheckAnswer(int userAnswer);

        /// <summary>
        /// override the ToString() method
        /// </summary>
        /// <returns>the question With answer Text</returns>
        public override string ToString()
        {
            return QuestionWithAnswerText();
        }

        
       

    }
}
