using Thylacine.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thylacine.Helper;
using Newtonsoft.Json;

namespace Thylacine.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Webhook
    {
        public DiscordBot Discord { get; internal set; }

        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ID { get; internal set; }

        [JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong? GuildID { get; internal set; }

        [JsonProperty("channel_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ChannelID { get; internal set; }

        [JsonProperty("user")]
        public User User { get; internal set; }

        [JsonProperty("name")]
        public string Name { get; internal set; }

        [JsonProperty("avatar")]
        public Avatar Avatar { get; internal set; }

        [JsonProperty("token")]
        public string Token { get; internal set; }

        //TODO: Implement Discord Exeption

        /// <summary>
        /// Modify a webhook
        /// </summary>
        /// <param name="username"></param>
        public void Modify(string username) { Modify(username, null); }

        /// <summary>
        /// Modify a webhook
        /// </summary>
        /// <param name="avatar"></param>
        public void Modify(Avatar avatar) { Modify(null, avatar); }

        /// <summary>
        /// Modify a webhook
        /// </summary>
        /// <param name="username"></param>
        /// <param name="avatar"></param>
        public void Modify(string username, Avatar avatar) {
            if (Discord == null) return;
            Discord.Rest.SendPayload(new Rest.Payloads.ModifyWebhook(this) { Name = username, Avatar = avatar });

            this.Name = username ?? this.Name;
            this.Avatar = avatar ?? this.Avatar;
        }

        /// <summary>
        /// Delete a webhook permanently. User must be owner.
        /// </summary>
        public void Delete()
        {
            if (Discord == null) return;
            Discord.Rest.SendPayload(new Rest.Payloads.DeleteWebhook(this));
        }


        #region Message Management
        /// <summary>
        /// Post a message to a channel. For the webhook embed objects, you can set every field except type (it will be rich regardless of if you try to set it), provider, video, and any height, width, or proxy_url values for images.
        /// </summary>
        /// <param name="message">The message builder.</param>
        /// <param name="username">override the default username of the webhook.</param>
        /// <param name="avatarURL">override the default avatar of the webhook.</param>
        /// <param name="tts">true if this is a TTS message</param>
        /// <param name="embed">embedded rich content</param>
        /// <returns></returns>
        public void SendMessage(string message, string username = "", string avatarURL = "", bool tts = false, Embed embed = null)
        {
            if (Discord == null) return;
            if (message.Length >= 2000) return;

            Discord.Rest.SendPayload<Message>(new Rest.Payloads.ExecuteWebhook(this)
            {
                Message = message,
                Username = username,
                AvatarURL = avatarURL,
                Embed = embed,
                TTS = tts
            });
        }

        /// <summary>
        /// Post a message to a channel. For the webhook embed objects, you can set every field except type (it will be rich regardless of if you try to set it), provider, video, and any height, width, or proxy_url values for images.
        /// </summary>
        /// <param name="message">The message builder.</param>
        /// <param name="username">override the default username of the webhook.</param>
        /// <param name="avatarURL">override the default avatar of the webhook.</param>
        /// <param name="tts">true if this is a TTS message</param>
        /// <param name="embed">embedded rich content</param>
        /// <returns></returns>
        public void SendMessage(MessageBuilder builder, string username = "", string avatarURL = "", bool tts = false, Embed embed = null)
        {
            this.SendMessage(builder.ToString(), username, avatarURL, tts, embed);
        }
        #endregion
    }
}
