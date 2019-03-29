using CodingArena.Main.Battlefields.Bots;

namespace CodingArena.Main.Battlefields.Stats
{
    public class BotStatsViewModel : Observable
    {
        private readonly Bot myBot;
        private string myBotName;
        private int myResourceCount;

        public BotStatsViewModel(Bot bot)
        {
            myBot = bot;
            myBot.Changed += (sender, args) => Update();
            myBot.WeaponPicked += (sender, args) => Update();
            myBot.ResourcePicked += (sender, args) => Update();
            myBot.ResourceDropped += (sender, args) => Update();
            myBot.Died += (sender, args) => Update();
        }

        private void Update()
        {
            BotName = myBot.Name;
            ResourceCount = myBot.Home.Count;
        }

        public string BotName
        {
            get => myBotName;
            private set
            {
                if (value == myBotName) return;
                myBotName = value;
                OnPropertyChanged();
            }
        }

        public int ResourceCount
        {
            get => myResourceCount;
            private set
            {
                if (value == myResourceCount) return;
                myResourceCount = value;
                OnPropertyChanged();
            }
        }
    }
}
