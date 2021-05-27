using System.Collections.Generic;

namespace CyberRunner

{
    public class SkillCheck
    {
        public enum SkillList
        {
            Charisma,
            Body,
            Volition,
            Empathy,
            Reflexes,
            Technics,
            Intelligence,
            Health,
        }

        public SkillList Skill;
        
        public int Power;
        public SkillCheck(SkillList skill, int power)
        {
            Skill = skill;
            Power = power;
        }
    }

    public class Chapter
    {
        public string CurrentChapterText;
        public Choice[] Choices;

        public Chapter(string currentChapterText, Choice[] choices)
        {
            Choices = choices;
            CurrentChapterText = currentChapterText;
        }
    }
    public class Game
    {
        public int CurrentChapterNumber;
        public readonly LinkedList<Chapter> GameList = new LinkedList<Chapter>();
    }
}