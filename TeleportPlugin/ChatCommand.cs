namespace TeleportPlugin
{
    internal class ChatCommand
    {
        public string Command { get; set; }
        public string Parameter { get; set; }

        public ChatCommand(string command, string parameter)
        {
            Command = command;
            Parameter = parameter;
        }

        public static ChatCommand Parse(string text)
        {
            if (!string.IsNullOrWhiteSpace(text) && text.Length > 1 && text.StartsWith("/"))
            {
                string command, parameter = "";

                text = text.Remove(0, 1);

                int spaceIndex = text.IndexOf(' ');

                if (spaceIndex == -1)
                {
                    command = text.Trim();
                }
                else
                {
                    command = text.Substring(0, spaceIndex).Trim();
                    parameter = text.Substring(spaceIndex).Trim();
                }

                return new ChatCommand(command, parameter);
            }

            return null;
        }
    }
}