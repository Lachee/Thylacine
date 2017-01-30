using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thylacine.Helper;

namespace Thylacine.Models
{
    [JsonConverter(typeof(AvatarConverter))]
    public class Avatar
    {
        private static readonly string HASH_PREFIX = "data:image/jpeg;base64,";

        private byte[] _bytes;

        public string DiscordHash { get; }
        public byte[] ImageBytes { get { return _bytes; } }

        public Avatar(byte[] bytes)
        {
            this._bytes = bytes;
            DiscordHash = HASH_PREFIX + Convert.ToBase64String(bytes);
        }
        public Avatar(string discord)
        {
            DiscordHash = discord;

            string hash = discord.Substring(HASH_PREFIX.Length);
            this._bytes = Convert.FromBase64String(hash);
        }

        public void ToFile(string file)
        {
            File.WriteAllBytes(file, _bytes);
        }
        public static Avatar FromFile(string file)
        {
            byte[] bytes = File.ReadAllBytes(file);
            return new Avatar(bytes);
        }
    }
}
