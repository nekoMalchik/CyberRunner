using System.Collections.Generic;
using static CyberRunner.SkillCheck;

namespace CyberRunner
{
    public class Player
    {
        public Dictionary<SkillList, int> PlayerSkills;

        public Player(int C, int B, int V, int E, int R, int T, int I, int H = 10)
        {
            PlayerSkills = new Dictionary<SkillList, int>
            {
                [SkillList.Charisma] = C,
                [SkillList.Body] = B,
                [SkillList.Volition] = V,
                [SkillList.Empathy] = E,
                [SkillList.Reflexes] = R,
                [SkillList.Technics] = T,
                [SkillList.Intelligence] = I,
                [SkillList.Health] = H
            };
        }

        public override string ToString()
        {
            return $" Харизма = {PlayerSkills[SkillList.Charisma]}\r\n Сила = {PlayerSkills[SkillList.Body]}\r\n " +
                   $"Воля = {PlayerSkills[SkillList.Volition]}\r\n Эмпатия = {PlayerSkills[SkillList.Empathy]}\r\n " +
                   $"Рефлексы = {PlayerSkills[SkillList.Reflexes]}\r\n Техника = {PlayerSkills[SkillList.Technics]}\r\n " +
                   $"Интеллект = {PlayerSkills[SkillList.Intelligence]}\r\n Жизнь = {PlayerSkills[SkillList.Health]}\r\n ";
        }
    }
}