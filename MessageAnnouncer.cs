using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using SDG.Unturned;
using Rocket.Core.Plugins;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Core;
using System.Collections.Generic;
using Logger = Rocket.Core.Logging.Logger;
using Random = System.Random;
using System.Collections;
using Rocket.Unturned.Player;

namespace SDMultiMessageAnnouncer
{
    public class MessageAnnouncer : RocketPlugin<MessageAnnouncerConfiguration>
    {
        public static MessageAnnouncer Instance;
        public static Coroutine coroutine;
        public const string pluginid = "MultiMessageAnnouncer";
        public static SDMultiLangLib.Lib SDLL;

        private List<RocketTextCommand> commands = new List<RocketTextCommand>();

        protected override void Load()
        {
            Instance = this;
            Logger.Log("------------------------------------------------------------", System.ConsoleColor.Blue);
            Logger.Log("|                                                          |", System.ConsoleColor.Blue);
            Logger.Log("|                     Dublicate version                    |", System.ConsoleColor.Blue);
            Logger.Log("|              SodaDevs: MultiMessageAnnouncer             |", System.ConsoleColor.Blue);
            Logger.Log("|                     RocketMod Version                    |", System.ConsoleColor.Blue);
            Logger.Log("|                                                          |", System.ConsoleColor.Blue);
            Logger.Log("------------------------------------------------------------", System.ConsoleColor.Blue);
            Logger.Log("Version: " + Assembly.GetName().Version, System.ConsoleColor.Blue);
            Logger.Log("Remake of 'MessageAnnouncer' by fr34kyn01535");
            coroutine = StartCoroutine(MessageWork());
            SDLL = new SDMultiLangLib.Lib(this);
            if (Level.isLoaded)
                OnLevelLoaded(0);
            else
                Level.onLevelLoaded += OnLevelLoaded;
            if (Configuration != null && Configuration.Instance.TextCommands != null)
            {
                foreach (TextCommand t in Configuration.Instance.TextCommands)
                {
                    RocketTextCommand command = new RocketTextCommand(t.Name, t.Help, t.Text);
                    commands.Add(command);
                    R.Commands.Register(command);
                }
            }
        }

        private void OnLevelLoaded(int level)
        {
            SDLL.CheckTranslateSystem(pluginid, DefaultTranslations);
        }

        IEnumerator MessageWork()
        {
            while(true)
            {
                printMessage();
                yield return new WaitForSeconds(Instance.Configuration.Instance.Interval);
            }
        }

        protected override void Unload()
        {
            Logger.Log("Unload");
            StopCoroutine(coroutine);
            Instance = null;
        }

        private void printMessage()
        {
            Random random = new Random();
            int r = random.Next(0, Instance.Configuration.Instance.Messages.Count-1);
            Color color = Color.white;
            switch (Instance.Configuration.Instance.Messages[r].Color.ToLower())
            {
                case "green":
                    color = Color.green;
                    break;
                case "black":
                    color = Color.black;
                    break;
                case "white":
                    color = Color.white;
                    break;
                case "blue":
                    color = Color.blue;
                    break;
                case "red":
                    color = Color.red;
                    break;
                case "clear":
                    color = Color.clear;
                    break;
                case "grey":
                    color = Color.grey;
                    break;
                case "cyan":
                    color = Color.cyan;
                    break;
                case "magente":
                    color = Color.magenta;
                    break;
                case "yellow":
                    color = Color.yellow;
                    break;
                default:
                    color = Color.white;
                    break;
            }
            foreach (SteamPlayer sp in Provider.clients)
            {
                UnturnedPlayer UP = UnturnedPlayer.FromSteamPlayer(sp);
                if (SDLL.GetLang(UP) == null) ChatManager.serverSendMessage(Instance.Configuration.Instance.Messages[r].Messages[0].message, color, null, sp, EChatMode.SAY, Instance.Configuration.Instance.Messages[r].Image, true);
                else ChatManager.serverSendMessage(Instance.Configuration.Instance.Messages[r].Messages.Find(x => x.lang == SDLL.GetLang(UP)).message, color, null, sp, EChatMode.SAY, Instance.Configuration.Instance.Messages[r].Image, true);
            }
        }
    }
}
