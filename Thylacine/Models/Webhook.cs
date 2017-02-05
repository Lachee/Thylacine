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
    /// <summary>
    /// A Discord Webhook object. This is used to send messages to discord without the need of complicated bots. They are channel specific.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Webhook
    {
        /// <summary>
        /// A reference to the Discord this object was created with
        /// </summary>
        public Discord Discord { get; internal set; }

        /// <summary>
        /// The ID of the webhook
        /// </summary>
        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ID { get; internal set; }
        
        /// <summary>
        /// The guild this webhook belongs too.
        /// </summary>
        [JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong? GuildID { get; internal set; }

        /// <summary>
        /// The channel this webhook belongs too.
        /// </summary>
        [JsonProperty("channel_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ChannelID { get; internal set; }

        /// <summary>
        /// The "user" that this webhook pretends to be.
        /// </summary>
        [JsonProperty("user")]
        public User User { get; internal set; }

        /// <summary>
        /// The current username.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; }

        /// <summary>
        /// The current avatar.
        /// </summary>
        [JsonProperty("avatar")]
        public Avatar Avatar { get; internal set; }

        /// <summary>
        /// The current token of the webhook.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; internal set; }

        //TODO: Implement Discord Exeption

        /// <summary>
        /// Modify a webhook
        /// </summary>
        /// <param name="username">The username to set too</param>
        public void Modify(string username) { Modify(username, null); }

        /// <summary>
        /// Modify a webhook
        /// </summary>
        /// <param name="avatar">The avatar to set too</param>
        public void Modify(Avatar avatar) { Modify(null, avatar); }

        /// <summary>
        /// Modify a webhook
        /// </summary>
        /// <param name="username">The username to set too</param>
        /// <param name="avatar">The avatar to set too</param>
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
        /// <param name="message">The message to send.</param>
        /// <param name="username">override the default username of the webhook.</param>
        /// <param name="avatarURL">override the default avatar of the webhook.</param>
        /// <param name="tts">true if this is a TTS message</param>
        /// <param name="embed">embedded rich content</param>
        /// <remarks>It is recommened to use <see cref="SendMessage(MessageBuilder, string, string, bool, Embed)"/> instead as it uses the <see cref="MessageBuilder"/>.</remarks>
        /// <seealso cref="MessageBuilder"/>
        /// <seealso cref="SendMessage(MessageBuilder, string, string, bool, Embed)"/>
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
        /// <param name="builder">The message builder.</param>
        /// <param name="username">override the default username of the webhook.</param>
        /// <param name="avatarURL">override the default avatar of the webhook.</param>
        /// <param name="tts">true if this is a TTS message</param>
        /// <param name="embed">embedded rich content</param>
        /// <seealso cref="MessageBuilder"/>
        /// <returns></returns>
        public void SendMessage(MessageBuilder builder, string username = "", string avatarURL = "", bool tts = false, Embed embed = null)
        {
            this.SendMessage(builder.ToString(), username, avatarURL, tts, embed);
        }
        #endregion
    }
}
