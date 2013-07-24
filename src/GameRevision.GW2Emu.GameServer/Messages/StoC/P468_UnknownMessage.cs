/*
 * This code was autogenerated by
 * GameRevision.GW2Emu.CodeWriter.
 * Date generated: 24-07-13
 */

using System;
using System.IO;
using System.Net;
using GameRevision.GW2Emu.Common;
using GameRevision.GW2Emu.Common.Math;
using GameRevision.GW2Emu.Common.Session;
using GameRevision.GW2Emu.Common.Messaging;
using GameRevision.GW2Emu.Common.Serialization;

namespace GameRevision.GW2Emu.GameServer.Messages.StoC
{
    public class P468_UnknownMessage : GenericMessage
    {
        public UID Unknown0;
        public UID Unknown1;
        public string Unknown2;
        public short Unknown3;
        public byte[] Unknown4;
        
        public override ushort Header
        {
            get
            {
                return 468;
            }
        }
        
        public override void Serialize(Serializer serializer)
        {
            serializer.Write(Header);
            serializer.Write(this.Unknown0);
            serializer.Write(this.Unknown1);
            serializer.WriteUtf16String(this.Unknown2);
            serializer.Write(this.Unknown3);
            serializer.Write((ushort)Unknown4.Length);
            for (int i = 0; i < Unknown4.Length; i++)
            {
                serializer.Write(Unknown4[i]);
            }
        }
    }
}
