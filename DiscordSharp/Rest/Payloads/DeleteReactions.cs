using DiscordSharp.Models;
using Newtonsoft.Json;
using RestSharp;

namespace DiscordSharp.Rest.Payloads
{

    [JsonObject(MemberSerialization.OptIn)]
    public class DeleteReaction : IRestPayload
    {
        Method IRestPayload.Method => Method.DELETE;
        string IRestPayload.Request => $"/channels/{ChannelID}/messages/{MessageID}/reactions/{Reaction}/@me";
        object IRestPayload.Payload => this;

        public ulong ChannelID { get; set; }
        public ulong MessageID { get; set; }
        public ulong Reaction { get; set; }

        public DeleteReaction() { }
        public DeleteReaction(Channel channel, Emoji emoji) { this.ChannelID = channel.ID; Reaction = emoji.ID.Value; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class DeleteUserReaction : IRestPayload
    {
        Method IRestPayload.Method => Method.DELETE;
        string IRestPayload.Request => $"/channels/{ChannelID}/messages/{MessageID}/reactions/{Reaction}/{UserID}";
        object IRestPayload.Payload => this;

        public ulong ChannelID { get; set; }
        public ulong Reaction { get; set; }
        public ulong UserID { get; set; }

        public ulong MessageID { get; set; }

        public DeleteUserReaction() { }
        public DeleteUserReaction(Channel channel, Emoji emoji, User user) { this.ChannelID = channel.ID; Reaction = emoji.ID.Value; UserID = user.ID; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class DeleteAllReactions : IRestPayload
    {
        Method IRestPayload.Method => Method.DELETE;
        string IRestPayload.Request => $"/channels/{ChannelID}/messages/{MessageID}/reactions";
        object IRestPayload.Payload => this;

        public ulong ChannelID { get; set; }
        public ulong MessageID { get; set; }

        public DeleteAllReactions() { }
        public DeleteAllReactions(Channel channel) { this.ChannelID = channel.ID; }
    }



}
