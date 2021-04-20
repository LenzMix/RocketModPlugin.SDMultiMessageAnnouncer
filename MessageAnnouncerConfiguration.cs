using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;

namespace SDMultiMessageAnnouncer
{
    public sealed class TextCommand
    {
        public string Name;
        public string Help;
        [XmlArrayItem("Line")]
        public List<string> Text;
    }

    public sealed class string2
    {
        [XmlAttribute]
        public string lang;
        [XmlText]
        public string message;
    }

    public sealed class Message
    {
        public List<string2> Messages;
        public string Image;
        public string Color;
    }

    public class MessageAnnouncerConfiguration : IRocketPluginConfiguration
    {
        public float Interval;

        [XmlArrayItem("Message")]
        [XmlArray(ElementName = "Messages")]
        public List<Message> Messages;

        [XmlArrayItem("TextCommand")]
        [XmlArray(ElementName = "TextCommands")]
        public List<TextCommand> TextCommands;

        public void LoadDefaults()
        {
            Interval = 60f;
            Messages = new List<Message> { 
                new Message
                {
                    Messages = new List<string2>
                    {
                        new string2
                        {
                            lang = "ru",
                            message = "[Радио] Выглядит как какая-то реклама"
                        },
                        new string2
                        {
                            lang = "en",
                            message = "[Radio] Looks like advertise"
                        }
                    },
                    Color = "green",
                    Image = "https://gspics.org/images/2020/03/21/ZGD0i.png"
                },
                
            };
            TextCommands = new List<TextCommand>(){
                new TextCommand(){Name="rules",Help="Shows the server rules",Text = new List<string>(){
                    "#1 No offensive content in the chat, respect other players",
                    "#2 No bug using, exploiting or abuse of powers",
                    "#3 Don't ask admins for items, teleports, loot respawn, ect.",
                    "#4 Please speak english in the public chat"}
                }
            };
        }
    }
}
