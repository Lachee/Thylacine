using Thylacine.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Thylacine.Exceptions;

namespace Thylacine.Models
{    
    [JsonObject(MemberSerialization.OptIn)]
    public class Message
    {
        public Discord Discord { get; internal set; }

        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ID { get; internal set; }
        
        [JsonProperty("channel_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ChannelID { get; internal set; }

        [JsonProperty("author")]
        public User Author { get; internal set; }

        [JsonProperty("content")]
        public string Content { get; internal set; }

        [JsonProperty("timestamp"), JsonConverter(typeof(TimestampConverter))]
        public DateTime? Timestamp { get; internal set; }

        [JsonProperty("edited_timestamp"), JsonConverter(typeof(TimestampConverter))]
        public DateTime? EditedTimestamp { get; internal set; }

        [JsonProperty("tts")]
        public bool IsTTS { get; internal set; }

        [JsonProperty("mention_everyone")]
        public bool MentionsEveryone { get; internal set; }

        [JsonProperty("mentions")]
        public User[] MentionedUsers { get; internal set; }

        [JsonProperty("mention_roles"), JsonConverter(typeof(SnowflakeArrayConverter))]
        public ulong[] MentionRoles { get; internal set; }

        [JsonProperty("attachments")]
        public Attachment[] Attachments { get; internal set; }

        [JsonProperty("embeds")]
        public Embed[] Embeds { get; internal set; }

        [JsonProperty("reactions")]
        public Reaction[] Reactions { get; internal set; }

        [JsonProperty("pinned")]
        public bool Pinned { get; internal set; }

        [JsonProperty("webhook_id")]
        public string WebhookID { get; internal set; }

        /// <summary>
        /// Used for validating the message was sent.
        /// </summary>
        [JsonProperty("nonce"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong? Nonce { get; set; }

        /// <summary>
        /// Checks if the message mentions a said user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Mentions(User user)
        {
            return MentionedUsers.Contains(user);
        }

        /// <summary>
        /// Checks if the message mentions the bot user.
        /// </summary>
        /// <returns></returns>
        public bool MentionsBot()
        {
            if (Discord == null) throw new DiscordMissingException();
            return Mentions(Discord.User);
        }

        public Channel GetChannel()
        {
            return GetChannel(Discord);
        }
        public Channel GetChannel(Discord discord)
        {
            if (discord == null) throw new DiscordMissingException();
            return discord.GetChannel(this.ChannelID);
        }

        #region Helpers
        public string FormatContent() { return FormatContent(Discord, this.Content); }
        public string FormatContent(string content) { return FormatContent(Discord, content); }
        public string FormatContent(Discord discord, string content)
        {
            if (discord == null) return content;

            Regex regex = new Regex(@"(?:<@|<@!)(?'user'\d+)>|<#(?'channel'\d+)>|<@&(?'role'\d+)>|(?:<:)(?'emoji'\w+)(?::)(?'emoji_id'\d+)(?:)");
            return regex.Replace(content, FormatContentEvaluator);
        }

        private string FormatContentEvaluator(Match e)
        {
            if (this.Discord == null) return e.Value;

            //Prepare the guild
            Guild g = Discord.GetChannelGuild(ChannelID);

            //Guild cannot be null
            if (g == null) return e.Value;

            //Check if this is a user
            if (e.Groups["emoji"].Success)
            {
                return ":" + e.Groups["emoji"].Value + ":";
            }
            else
            {
                ulong id;
                if (!ulong.TryParse(e.Groups[1].Value, out id)) return e.Value;

                if (e.Groups["user"].Success)
                {
                    var member = g.GetMember(id);
                    if (member == null) return e.Value;

                    return string.IsNullOrEmpty(member.Nickname) ? member.User.Username : member.Nickname;
                }

                if (e.Groups["channel"].Success)
                {
                    var channel = g.GetChannel(id);
                    if (channel == null) return e.Value;

                    return channel.Name;
                }

                if (e.Groups["role"].Success)
                {
                    var role = g.GetRole(id);
                    if (role == null) return e.Value;

                    return role.Name;
                }
            }

            return e.Value;
        }
        #endregion

        #region REST Calls
        /// <summary>
        /// Edit a previously sent message. You can only edit messages that have been sent by the current user. 
        /// </summary>
        /// <param name="content"></param>
        public void EditMessage(string content)
        {
            if (Discord == null) throw new DiscordMissingException();
            
            Message msg = Discord.Rest.SendPayload<Message>(new Rest.Payloads.EditMessage(this) { Message = content });
            if (msg == null) return;

            this.Content = msg.Content;
        }

        /// <summary>
        /// Delete a message. If operating on a guild channel and trying to delete a message that was not sent by the current user, this endpoint requires the 'MANAGE_MESSAGES' permission. 
        /// </summary>
        public void DeleteMessage()
        {
            if (Discord == null) throw new DiscordMissingException();
            Discord.Rest.SendPayload(new Rest.Payloads.DeleteMessage(this));
        }

        public List<User> GetReactions(Emoji reaction)
        {
            if (Discord == null) throw new DiscordMissingException();
            return Discord.Rest.SendPayload<List<User>>(new Rest.Payloads.GetReactions(this) { Reaction = reaction.ID.GetValueOrDefault() });
        } 
        
        /// <summary>
        /// Create a reaction for the message. If nobody else has reacted to the message using this emoji, this endpoint requires the 'ADD_REACTIONS' permission to be present on the current user.
        /// </summary>
        /// <param name="reaction">The reaction to add</param>
        public void CreateReaction(Emoji reaction)
        {
            if (Discord == null) throw new DiscordMissingException();
            Discord.Rest.SendPayload(new Rest.Payloads.CreateReaction()
            {
                ChannelID = this.ChannelID,
                MessageID = this.ID,
                Reaction = reaction.ID.GetValueOrDefault()
            });
        }

        /// <summary>
        /// Delete a reaction the current user has made for the message.
        /// </summary>
        /// <param name="reaction">The reaction to delete</param>
        public void DeleteReaction(Emoji reaction)
        {
            if (Discord == null) throw new DiscordMissingException();
            Discord.Rest.SendPayload(new Rest.Payloads.DeleteReaction()
            {
                ChannelID = this.ChannelID,
                MessageID = this.ID,
                Reaction = reaction.ID.GetValueOrDefault()
            });
        }

        /// <summary>
        /// Deletes another user's reaction. This endpoint requires the 'MANAGE_MESSAGES' permission to be present on the current user.
        /// </summary>
        /// <param name="reaction">The reaction to delete</param>
        /// <param name="user">The user to delete the reaction off</param>
        public void DeleteReaction(Emoji reaction, User user)
        {
            if (Discord == null) throw new DiscordMissingException();
            Discord.Rest.SendPayload(new Rest.Payloads.DeleteUserReaction()
            {
                ChannelID = this.ChannelID,
                MessageID = this.ID,
                Reaction = reaction.ID.GetValueOrDefault(),
                UserID = user.ID
            });
        }

        /// <summary>
        /// Deletes all reactions on a message. This endpoint requires the 'MANAGE_MESSAGES' permission to be present on the current user.
        /// </summary>
        public void ClearReactions()
        {
            if (Discord == null) throw new DiscordMissingException();
            Discord.Rest.SendPayload(new Rest.Payloads.DeleteAllReactions()
            {
                MessageID = this.ID
            });
        }

        /// <summary>
        /// Pin a message in a channel. Requires the 'MANAGE_MESSAGES' permission.
        /// </summary>
        public void Pin()
        {
            if (Discord == null) throw new DiscordMissingException();
            Discord.Rest.SendPayload(new Rest.Payloads.PinChannelMessage() { ChannelID = this.ChannelID, MessageID = this.ID });
            Pinned = true;
        }

        /// <summary>
        /// Delete a pinned message in a channel. Requires the 'MANAGE_MESSAGES' permission.
        /// </summary>
        public void Unpin()
        {
            if (Discord == null) throw new DiscordMissingException();
            Discord.Rest.SendPayload(new Rest.Payloads.UnpinChannelMessage() { ChannelID = this.ChannelID, MessageID = this.ID });
            Pinned = false;
        }
        #endregion
    }

    public static class BulkMessages
    {
        /// <summary>
        /// Delete multiple messages in a single request. This endpoint can only be used on guild channels and requires the 'MANAGE_MESSAGES' permission.
        /// It will group messages by channel and execute the REST call once per channel. It will use the REST client from the first discord client
        /// </summary>
        /// <param name="messages"></param>
        [System.Obsolete("Bulk Delete endpoint has been depreciated and will be removed in the next Discord API updated. A new purge endpoint will take its place.")]
        public static void DeleteMessages(this Message[] messages)
        {
            if (messages.Length <= 0) return;
            DeleteMessages(messages, messages[0].Discord);
        }

        /// <summary>
        /// Delete multiple messages in a single request. This endpoint can only be used on guild channels and requires the 'MANAGE_MESSAGES' permission.
        /// It will group messages by channel and execute the REST call once per channel.
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="discord"></param>
        [System.Obsolete("Bulk Delete endpoint has been depreciated and will be removed in the next Discord API updated. A new purge endpoint will take its place.")]
        public static void DeleteMessages(this Message[] messages, Discord discord)
        {
            if (discord == null) throw new DiscordMissingException();
            if (messages.Length <= 0) return;

            var channelGrouping = messages.GroupBy(m => m.ChannelID);
            foreach (var cg in channelGrouping)
            {
                ulong channel = cg.First().ChannelID;
                ulong[] snowflakes = cg.Select(m => m.ID).ToArray();

                discord.Rest.SendPayload(new Rest.Payloads.DeleteBulkMessages() { ChannelID = channel, Snowflakes = snowflakes });
            }
        }
    }
}
