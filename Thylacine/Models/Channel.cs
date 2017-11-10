using Thylacine.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thylacine.Exceptions;

namespace Thylacine.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Channel
    {
        public Guild Guild { get; internal set; }
        public Discord Discord { get { return Guild.Discord; } }

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
            if (Discord == null) throw new DiscordMissingException();
            Discord.Rest.SendPayload(new Rest.Payloads.ModifyChannel(this));
        }
		
		/// <summary>
		/// Sets the permission/role for the channel.
		/// </summary>
		/// <param name="permission">The permission to set</param>
        public void SetPermission(Overwrite permission) { this.SetPermission(permission, permission.Allow, permission.Deny, permission.Type); }
        public void SetPermission(Overwrite permission, Permission allow, Permission deny, OverwriteType type) { this.SetPermission(permission.ID, permission.Allow, permission.Deny, permission.Type); }
        public void SetPermission(ulong permissionID, Permission allow, Permission deny, OverwriteType type)
        {
            if (Discord == null) throw new DiscordMissingException();
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
		/// Delete a channel permission overwrite for a user or role in a channel. Only usable for guild channels.
		/// <para>Permission: MANAGE_ROLES</para>
		/// </summary>
		/// <param name="permission"></param>
		public void DeletePermission(Overwrite permission) { DeletePermission(permission.ID); }

		/// <summary>
		/// Delete a channel permission overwrite for a user or role in a channel. Only usable for guild channels.
		/// <para>Permission: MANAGE_ROLES</para>
		/// </summary>
		/// <param name="permissionID"></param>
		public void DeletePermission(ulong permissionID)
        {
            if (Discord == null) throw new DiscordMissingException();
            Discord.Rest.SendPayload(new Rest.Payloads.DeleteChannelPermission()
            {
                ChannelID = this.ID,
                OverwriteID = permissionID
            });
        }

		/// <summary>
		/// Delete a guild channel, or close a private message. 
		/// <para>Permission: MANAGE_CHANNELS</para>
		/// </summary>
		public void DeleteChannel()
        {
            if (Discord == null) throw new DiscordMissingException();
            Discord.Rest.SendPayload(new Rest.Payloads.DeleteChannel(this));
        }

		/// <summary>
		/// Create a new invite object for the channel. Only usable for guild channels. Default behaviour as the discord client, making a day long invite with infinite uses.
		/// Requires the CREATE_INSTANT_INVITE permission.
		/// <para>Permission: CREATE_INSTANT_INVITE</para>
		/// </summary>
		/// <param name="lifetime">duration of invite in seconds before expiry, or 0 for never</param>
		/// <param name="uses">	max number of uses or 0 for unlimited</param>
		/// <param name="temporary">whether this invite only grants temporary membership</param>
		/// <param name="unique">if true, don't try to reuse a similar invite (useful for creating many unique one time use invites)</param>
		public Task<Invite> CreateInvite(int lifetime = 86400, int uses = 0, bool temporary = false, bool unique = false)
        {
            if (Discord == null) throw new DiscordMissingException();
            return Discord.Rest.SendPayload<Invite>(new Rest.Payloads.CreateInvite()
            {
                ChannelID = this.ID, 
                MaxAge = lifetime,
                MaxUses = uses,
                Temporary = temporary,
                Unique = unique
            });
        }

        /// <summary>
        /// Returns a list of invites for the channel
        /// </summary>
        /// <returns></returns>
        public Task<List<Invite>> FetchInvites()
        {
            if (Discord == null) throw new DiscordMissingException();
            return Discord.Rest.SendPayload<List<Invite>>(new Rest.Payloads.GetChannelInvites() { ChannelID = this.ID });
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
        public async Task<Message> SendMessage(string message, bool tts = false, Embed embed = null)
        {
            if (Discord == null) throw new DiscordMissingException();
            if (message.Length >= 2000) return null;

            Message msg = await Discord.Rest.SendPayload<Message>(new Rest.Payloads.CreateMessagePayload()
            {
                ChannelID = ID,
                Message = message,
                Embed = embed,
                TTS = tts
            });
            
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
        public async Task<Message> SendMessage(MessageBuilder builder, bool tts = false, Embed embed = null)
        {
            Message message = await this.SendMessage(builder.ToString(), tts, embed);
            message.Discord = this.Discord;
            return message;
        }

        /// <summary>
        /// Returns the messages for a channel. If operating on a guild channel, this endpoint requires the 'READ_MESSAGES' permission to be present on the current user
        /// </summary>
        /// <param name="around">get messages around this message ID</param>
        /// <param name="before">	get messages before this message ID</param>
        /// <param name="after">get messages after this message ID</param>
        /// <param name="limit">max number of messages to return (1-100)</param>
        /// <returns></returns>
        public Task<List<Message>> FetchMessages(ulong? around = null, ulong? before = null, ulong? after = null, int limit = 50)
        {
            if (Discord == null) throw new DiscordMissingException();

            //Clamp the limit
            limit = limit < 1 ? 1 : (limit > 100 ? 100 : limit);

            //Send the request
            return Discord.Rest.SendPayload<List<Message>>(new Rest.Payloads.GetMessages()
            {
                ChannelID = ID,
                Around = around,
                Before = before,
                After = after,
                Limit = limit
            });
        }

        /// <summary>
        /// Returns a specific message in the channel. If operating on a guild channel, this endpoints requires the 'READ_MESSAGE_HISTORY' permission to be present on the current user.
        /// </summary>
        /// <param name="messageID"></param>
        /// <returns></returns>
        public Task<Message> FetchMessage(ulong messageID)
        {
            if (Discord == null) throw new DiscordMissingException();
            return Discord.Rest.SendPayload<Message>(new Rest.Payloads.GetMessage()
            {
                ChannelID = ID,
                MessageID = messageID
            });
        }

        /// <summary>
        /// Returns all pinned messages in the channel
        /// </summary>
        /// <returns></returns>
        public Task<List<Message>> FetchPinnedMessages()
        {
            if (Discord == null) throw new DiscordMissingException();
            return Discord.Rest.SendPayload<List<Message>>(new Rest.Payloads.GetPinnedMessages() { ChannelID = ID });
        }

        #endregion

        #region Webhook Management
        public async Task<Webhook> CreateWebhook(string name, Avatar avatar)
        {
            if (Discord == null) throw new DiscordMissingException();

            Webhook hook = await Discord.Rest.SendPayload<Webhook>(new Rest.Payloads.CreateWebhook()
            {
                ChannelID = this.ID,
                Name = name,
                Avatar = avatar
            });
            hook.Discord = this.Discord;

            return hook;
        }
        public async Task<List<Webhook>> FetchWebhooks()
        {
            if (Discord == null) throw new DiscordMissingException();
            List<Webhook> hooks = await Discord.Rest.SendPayload<List<Webhook>>(new Rest.Payloads.GetWebhooks() { ScopeID = this.ID, Scope = "channels" });
            foreach (Webhook h in hooks) h.Discord = Discord;

            return hooks;
        }
        #endregion

        /// <summary>
        /// Shows the typing indicator in current channel for current user.
        /// Generally bots should not implement this route. However, if a bot is responding to a command and expects the computation to take a few seconds, this endpoint may be called to let the user know that the bot is processing their message.
        /// </summary>
        public void ShowTyping()
        {
            if (Discord == null) throw new DiscordMissingException();
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
