using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DiscordSharp.Models
{
    public class Webhook
    {
        public ulong ID { get; internal set; }
        public string Username { get; }


        public void DiscordImageDecode(string hash, string file)
        {
            string image = hash.Replace("data:image/jpeg;base64,", "");
            byte[] bytes = Convert.FromBase64String(image);

            System.IO.File.WriteAllBytes(file, bytes);
        }
        public string DiscordImageEncode(string file)
        {
            byte[] bytes = System.IO.File.ReadAllBytes(file);
            return "data:image/jpeg;base64," + Convert.ToBase64String(bytes);

            /*
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
            */
        }

    }
}
