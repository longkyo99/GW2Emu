/*
 * This code was autogenerated by
 * GameRevision.GW2Emu.CodeWriter.
 * Date generated: 08-07-13
 */

using System;
using System.IO;
using System.Net;
using GameRevision.GW2Emu.Core;
using GameRevision.GW2Emu.Core.Types;
using GameRevision.GW2Emu.Core.Serializers;

namespace GameRevision.GW2Emu.Messages.LoginServer.StoC
{
    public class P17_CharacterInfoMessage : GenericMessage
    {
        public int SyncCount;
        public UID CharacterId;
        public int Unknown2;
        public string CharacterName;
        public byte[] CharacterData;
        
        public override ushort Header
        {
            get
            {
                return 17;
            }
        }
        
        public override void Serialize(Serializer serializer)
        {
            base.Serialize(serializer);
            serializer.WriteVarint(this.SyncCount);
            serializer.WriteUID(this.CharacterId);
            serializer.WriteVarint(this.Unknown2);
            serializer.WriteUtf16String(this.CharacterName);
            serializer.Write((ushort)CharacterData.Length);
            for (int i = 0; i < CharacterData.Length; i++)
            {
                serializer.Write(CharacterData[i]);
            }
        }
    }
}