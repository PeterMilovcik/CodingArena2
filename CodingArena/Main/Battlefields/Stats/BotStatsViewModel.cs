﻿using CodingArena.Main.Battlefields.Bots;
using System;
using System.Windows;
using System.Windows.Media;

namespace CodingArena.Main.Battlefields.Stats
{
    public class BotStatsViewModel : Observable
    {
        private readonly Bot myBot;
        private string myBotName;
        private int myResourceCount;
        private int myAmmo;
        private string myWeapon;
        private Visibility myAliveGridVisibility;
        private Visibility myDeadGridVisibility;
        private TimeSpan myRespawnIn;
        private Brush myBackground;

        public BotStatsViewModel(Bot bot)
        {
            myBot = bot;
            myBot.Changed += (sender, args) => Update();
            myBot.WeaponPicked += (sender, args) => Update();
            myBot.ResourcePicked += (sender, args) => Update();
            myBot.ResourceDropped += (sender, args) => Update();
            myBot.Died += (sender, args) => Update();
            myBot.Respawned += (sender, args) => Update();
            Background = Brushes.Transparent;
        }

        private void Update()
        {
            BotName = myBot.Name;
            ResourceCount = myBot.Home.Count;
            Ammo = myBot.EquippedWeapon.Ammunition.Remaining;
            Weapon = myBot.EquippedWeapon.Name;
            if (myBot.IsDead)
            {
                AliveGridVisibility = Visibility.Hidden;
                DeadGridVisibility = Visibility.Visible;
                RespawnIn = myBot.RespawnIn;
                Background = Brushes.LightGray;
            }
            else
            {
                AliveGridVisibility = Visibility.Visible;
                DeadGridVisibility = Visibility.Hidden;
                Background = Brushes.Transparent;
            }
        }

        public Brush Background
        {
            get => myBackground;
            set
            {
                if (Equals(value, myBackground)) return;
                myBackground = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan RespawnIn
        {
            get => myRespawnIn;
            set
            {
                if (value.Equals(myRespawnIn)) return;
                myRespawnIn = value;
                OnPropertyChanged();
            }
        }

        public Visibility AliveGridVisibility
        {
            get => myAliveGridVisibility;
            private set
            {
                if (value == myAliveGridVisibility) return;
                myAliveGridVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility DeadGridVisibility
        {
            get => myDeadGridVisibility;
            private set
            {
                if (value == myDeadGridVisibility) return;
                myDeadGridVisibility = value;
                OnPropertyChanged();
            }
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

        public int Ammo
        {
            get => myAmmo;
            set
            {
                if (value == myAmmo) return;
                myAmmo = value;
                OnPropertyChanged();
            }
        }

        public string Weapon
        {
            get => myWeapon;
            set
            {
                if (value == myWeapon) return;
                myWeapon = value;
                OnPropertyChanged();
            }
        }
    }
}
