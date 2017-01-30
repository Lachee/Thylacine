using Thylacine.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Channel
    {
        public Guild Guild { get; internal set; }
        public DiscordBot Discord { get { return Guild.Discord; } }

        public string MentionTag { get { return "<#" + this.ID + ">"; } }

        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ID { get; internal set; }

        [JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong GuildID { get; internal set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public ChannelType Type { get; internal set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("is_private")]
        public bool IsPrivate { get; internal set; }

        [JsonProperty("permission_overwrites")]
        public List<Overwrite> PermissionOverwrites { get; internal set; }

        #region Channel Management
        /// <summary>
        /// Update a channels settings. Requires the 'MANAGE_GUILD' permission for the guild. 
        /// After editing a channel, you need to call this function for it to be applied to the discord server.
        /// </summary>
        public void ApplyModifications()
        {
            if (Discord == null) return;
            Discord.Rest.SendPayload(new Rest.Payloads.ModifyChannel(this));
        }

        public void SetPermission(Overwrite permission) { this.SetPermission(permission, permission.Allow, permission.Deny, permission.Type); }
        public void SetPermission(Overwrite permission, Permission allow, Permission deny, OverwriteType type) { this.SetPermission(permission.ID, permission.Allow, permission.Deny, permission.Type); }
        public void SetPermission(ulong permissionID, Permission allow, Permission deny, OverwriteType type)
        {
            if (Discord == null) return;
            Discord.Rest.SendPayload(new Rest.Payloads.EditChannelPermissions()
            {
                ChannelID = this.ID,
                OverwriteID = permissionID,
                Allow = allow,
                Deny = deny,
                Type = type
            });

            Overwrite poverwrite = PermissionOverwrites.Where(o => o.ID.Equals(permissionID)).First();
            if (poverwrite != null)
            {
                poverwrite.Allow = allow;
                poverwrite.Deny = deny;
                poverwrite.Type = type;
            }
            else
            {
                PermissionOverwrites.Add(new Overwrite() { ID = permissionID, Allow = allow, Deny = deny, Type = type });
            }
        }

        /// <summary>
        /// Delete a channel permission overwrite for a user or role in a channel. Only usable for guild channels. Requires the 'MANAGE_ROLES' permission
        /// </summary>
        /// <param name="permission"></param>
        public void DeletePermission(Overwrite permission) { DeletePermission(permission.ID); }

        /// <summary>
        /// Delete a channel permission overwrite for a user or role in a channel. Only usable for guild channels. Requires the 'MANAGE_ROLES' permission
        /// </summary>
        /// <param name="permissionID"></param>
        public void DeletePermission(ulong permissionID)
        {
            if (Discord == null) return;
            Discord.Rest.SendPayload(new Rest.Payloads.DeleteChannelPermission()
            {
                ChannelID = this.ID,
                OverwriteID = permissionID
            });
        }

        /// <summary>
        /// Delete a guild channel, or close a private message. Requires the 'MANAGE_CHANNELS' permission for the guild
        /// </summary>
        public void DeleteChannel()
        {
            if (Discord == null) return;
            Discord.Rest.SendPayload(new Rest.Payloads.DeleteChannel(this));
        }

        /// <summary>
        /// Create a new invite object for the channel. Only usable for guild channels. Requires the CREATE_INSTANT_INVITE permission
        /// </summary>
        /// <param name="lifetime">duration of invite in seconds before expiry, or 0 for never</param>
        /// <param name="uses">	max number of uses or 0 for unlimited</param>
        /// <param name="temporary">whether this invite only grants temporary membership</param>
        /// <param name="unique">if true, don't try to reuse a similar invite (useful for creating many unique one time use invites)</param>
        public Invite CreateInvite(int lifetime = 86400, int uses = 0, bool temporary = false, bool unique = false)
        {
            if (Discord == null) return null;
            return Discord.Rest.SendPayload<Invite>(new Rest.Payloads.CreateInvite()
            {
                ChannelID = this.ID, 
                MaxAge = lifetime,
                MaxUses = uses,
                Temporary = temporary,
                Unique = unique
            });
        }

        #endregion

        #region Message Management
        /// <summary>
        /// Post a message to a guild text or DM channel. It is recommended to use the MessageBuilder version of this call instead as it accounts for size limitations. Requires the 'SEND_MESSAGES' permission to be present on the current user. Returns a message object.
        /// </summary>
        /// <param name="message">the message contents (up to 2000 characters)</param>
        /// <param name="tts">true if this is a TTS message</param>
        /// <param name="embed">embedded rich content</param>
        /// <returns></returns>
        public Message SendMessage(string message, bool tts = false, Embed embed = null)
        {
            if (Discord == null) return null;
            if (message.Length >= 2000) return null;

            Message msg = Discord.Rest.SendPayload<Message>(new Rest.Payloads.CreateMessagePayload()
            {
                ChannelID = ID,
                Message = message,
                Embed = embed,
                TTS = tts
            });

            if (msg == null)
            {
                Console.WriteLine("Something done fucked up!");
                return null;
            }

            msg.Discord = Discord;
            return msg;
        }
        
        /// <summary>
        /// Post a message to a guild text or DM channel. Requires the 'SEND_MESSAGES' permission to be present on the current user. Returns a message object.
        /// </summary>
        /// <param name="message">The message builder.</param>
        /// <param name="tts">true if this is a TTS message</param>
        /// <param name="embed">embedded rich content</param>
        /// <returns></returns>
        public Message SendMessage(MessageBuilder builder, bool tts = false, Embed embed = null)
        {
            return this.SendMessage(builder.ToString(), tts, embed);
        }
        #endregion

        /// <summary>
        /// Shows the typing indicator in current channel for current user.
        /// Generally bots should not implement this route. However, if a bot is responding to a command and expects the computation to take a few seconds, this endpoint may be called to let the user know that the bot is processing their message.
        /// </summary>
        public void ShowTyping()
        {
            if (Discord == null) return;
            Discord.Rest.SendPayload(new Rest.Payloads.TriggerTypingIndicator() { ChannelID = this.ID });
        }
    }

    public class TextChannel : Channel
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("last_message_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong LastMessageID { get; internal set; }
    }

    public class VoiceChannel : Channel
    {
        [JsonProperty("bitrate")]
        public int Bitrate { get; set; }

        [JsonProperty("user_limit")]
        public int UserLimit { get; set; }
    }

    public enum ChannelType
    {
        Text,
        Voice
    }
}
