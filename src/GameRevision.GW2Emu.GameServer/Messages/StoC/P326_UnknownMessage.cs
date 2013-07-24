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
    public class P326_UnknownMessage : GenericMessage
    {
        public int Unknown0;
        public int Unknown1;
        public int Unknown2;
        public Vector2 Unknown3;
        public int Unknown4;
        
        public override ushort Header
        {
            get
            {
                return 326;
            }
        }
        
        public override void Serialize(Serializer serializer)
        {
            serializer.Write(Header);
            serializer.WriteVarint(this.Unknown0);
            serializer.WriteVarint(this.Unknown1);
            serializer.WriteVarint(this.Unknown2);
            serializer.Write(this.Unknown3);
            serializer.WriteVarint(this.Unknown4);
        }
    }
}
