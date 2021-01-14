using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Scores364.Api.Manager.Models
{
    [ProtoContract]
    public class GamesPageToken
    {
        [ProtoMember(1)]
        public DateTime From { get; set; }
        [ProtoMember(2)]
        public int Offset { get; set; }
        [ProtoMember(3)]
        public string LanguageId { get; set; }

        public static GamesPageToken Deserialize(string token)
        {
            using (var stream = new MemoryStream(Convert.FromBase64String(token)))
            {
                return Serializer.Deserialize<GamesPageToken>(stream);
            }
                
        }
        public override string ToString()
        {
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, this);
                return Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Position);
            }
        }
    }
}
