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

		/// <summary>
		/// A list of all overwrites for the channel
		/// </summary>
		public Overwrite[] Overwrites => _overwrites.Values.ToArray();

		private Dictionary<ulong, Overwrite> _overwrites = new Dictionary<ulong, Overwrite>();
		[JsonProperty("permission_overwrites")]
		private Overwrite[] j_overwrites
		{
			get { return _overwrites.Values.ToArray(); }
			set
			{
				_overwrites = new Dictionary<ulong, Overwrite>();
				foreach (Overwrite o in value) _overwrites.Add(o.ID, o);
			}
		}

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
			//We are missing discord. This object must not have been initialized properly.
			if (Discord == null) throw new DiscordMissingException();

			//Send the payload to the discord, updating the things
			Discord.Rest.SendPayload(new Rest.Payloads.EditChannelPermissions()
			{
				ChannelID = this.ID,
				OverwriteID = permissionID,
				Allow = allow,
				Deny = deny,
				Type = type
			});


			//This will be updated anyways.
			/*
			//Update the existing values. If they don't exist, add them to the table.
			Overwrite overwrite;
			if (_overwrites.TryGetValue(permissionID, out overwrite))
			{
				overwrite.Allow = allow;
				overwrite.Deny = deny;
				overwrite.Type = type;
            }
            else
            {
                _overwrites.Add(permissionID, new Overwrite() { ID = permissionID, Allow = allow, Deny = deny, Type = type });
            }
			*/
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
        /// <param name="builder">The message builder.</param>
        /// <param name="tts">true if this is a TTS message</param>
        /// <param name="embed">embedded rich content</param>
        /// <returns></returns>
        public Task<Message> SendMessage(MessageBuilder builder, bool tts = false, Embed embed = null)
        {
            return this.SendMessage(builder.ToString(), tts, embed);
        }

		#region Fetch Messages
		public Task<List<Message>> FetchMessages(int limit)
		{
			return m_FetchMessages(limit: limit);
		}

		/// <summary>
		/// Gets an array of messages around the supplied id.
		/// </summary>
		/// <param name="around">The ID to get messages around.</param>
		/// <param name="limit">The number of messages to fetch (1 - 100) </param>
		/// <returns>A list of messages</returns>
		public Task<List<Message>> FetchMessagesAround(ulong around, int limit)
		{
			return m_FetchMessages(around: around, limit: limit);
		}

		/// <summary>
		/// Gets an array of messages before the supplied id.
		/// </summary>
		/// <param name="before">The ID to get messages before.</param>
		/// <param name="limit">The number of messages to fetch (1 - 100) </param>
		/// <returns>A list of messages</returns>
		public Task<List<Message>> FetchMessagesBefore(ulong before, int limit)
		{
			return m_FetchMessages(before: before, limit: limit);
		}

		/// <summary>
		/// Gets an array of messages after the supplied id.
		/// </summary>
		/// <param name="after">The ID to get messages after.</param>
		/// <param name="limit">The number of messages to fetch (1 - 100) </param>
		/// <returns>A list of messages</returns>
		public Task<List<Message>> FetchMessagesAfter(ulong after, int limit)
		{
			return m_FetchMessages(after: after, limit: limit);
		}
        private Task<List<Message>> m_FetchMessages(ulong? around = null, ulong? before = null, ulong? after = null, int limit = 50)
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
		#endregion

		/// <summary>
		/// Returns a specific message in the channel. If operating on a guild channel, this endpoints requires the 'READ_MESSAGE_HISTORY' permission to be present on the current user.
		/// </summary>
		/// <param name="messageID">The ID of the message</param>
		/// <returns>The message</returns>
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
		/// Computes the permission the member has in this channel
		/// </summary>
		/// <param name="member">The member</param>
		/// <returns>The final permission the member has in the channel</returns>
		public Permission ComputePermissions(GuildMember member)
		{
			if (Guild != member.Guild)
				throw new ArgumentException("Member and Channel have different guilds!", "member");

			//Get the base permission. If they are admin, they get everything anyways.
			Permission basepems = member.ComputePermissions();
			if (basepems.HasFlag(Permission.Adminstrator)) return Permission.ALL;

			Permission perms = basepems;

			//Find the everyone overwrites
			Overwrite owEveryone;
			if (_overwrites.TryGetValue(Guild.ID, out owEveryone))
			{
				perms &= ~owEveryone.Deny;
				perms |= owEveryone.Allow;
			}

			//Find role specific overwrites
			Permission deny = 0, allow = 0;
			foreach (Role r in member.Roles)
			{
				Overwrite ow;
				if (_overwrites.TryGetValue(r.ID, out ow))
				{
					allow |= ow.Allow;
					deny |= ow.Deny;
				}
			}

			//Apply them to the perms
			perms &= ~deny;
			perms |= allow;

			//Apply member specific overrides
			Overwrite owMember;
			if (_overwrites.TryGetValue(member.ID, out owMember))
			{
				perms &= ~owMember.Deny;
				perms |= owMember.Allow;
			}

			//Return the final permissions
			return perms;
		}

		/// <summary>
		/// Shows the typing indicator in current channel for current user.
		/// <para>
		/// Generally bots should not implement this route. However, if a bot is responding to a command and expects the computation to take a few seconds, this endpoint may be called to let the user know that the bot is processing their message.
		/// </para>
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
