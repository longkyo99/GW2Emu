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

namespace GameRevision.GW2Emu.Messages.GameServer.StoC
{
    public class P557_UnknownMessage : GenericMessage
    {
        public byte Unknown0;
        public int Unknown1;
        public byte Unknown2;
        public int Unknown3;
        public string Unknown4;
        public UID Unknown5;
        public byte Unknown6;
        public byte Unknown7;
        
        public override ushort Header
        {
            get
            {
                return 557;
            }
        }
        
        public override void Serialize(Serializer serializer)
        {
            base.Serialize(serializer);
            serializer.Write(this.Unknown0);
            serializer.WriteVarint(this.Unknown1);
            serializer.Write(this.Unknown2);
            serializer.WriteVarint(this.Unknown3);
            serializer.WriteUtf16String(this.Unknown4);
            serializer.WriteUID(this.Unknown5);
            serializer.Write(this.Unknown6);
            serializer.Write(this.Unknown7);
        }
    }
}