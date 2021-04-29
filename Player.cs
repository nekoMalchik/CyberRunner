namespace CyberRunner
{
    public class Player
    {
        public int Charisma = 0;
        public int Body = 0;
        public int Volition = 0;
        public int Empathy = 0;
        public int Intelligence = 0;
        public int Reflexes = 0;
        public int Technics = 0;
        public int Health = 10;

        public Player(int C, int B, int V, int E, int R, int T, int I, int H = 10)
        {
            Charisma = C;
            Body = B;
            Volition = V;
            Empathy = E;
            Reflexes = R;
            Technics = T;
            Intelligence = I;
            Health = H;
        }
        
        public string ToString()
        {
            return $" Charisma = {Charisma}\r\n Body = {Body}\r\n " +
                   $"Volition = {Volition}\r\n Empathy = {Empathy}\r\n " +
                   $"Reflexes = {Reflexes}\r\n Technics = {Technics}\r\n " +
                   $"Intelligence = {Intelligence}\r\n Health = {Health}\r\n ";
        }

        // public string[] ToStrings()
        // {
        //     var strings = new string[8];
        //     strings[0] = $"Charisma = {Charisma}";
        //     strings[1] = $"Body = {Body} ";
        //     strings[2] = $"Volition = {Volition}";
        //     strings[3] = $"Empathy = {Empathy}";
        //     strings[4] = $"Reflexes = {Reflexes}";
        //     strings[5] = $"Technics = {Technics}";
        //     strings[6] = $"Intelligence = {Intelligence}";
        //     strings[7] = $"Health = {Health}";
        //     return strings;
        // }
    }
}