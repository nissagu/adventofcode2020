namespace PasswordPhilosophy
{
    public class Password
    {

        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public string Character { get; set; }
        public string PassPhrase { get; set; }

        public Password(int min, int max, string charac, string phrase)
        {
            MinValue = min;
            MaxValue = max;
            Character = charac;
            PassPhrase = phrase;

        }

    }
}