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
    public class P716_UnknownMessage : GenericMessage
    {
        public int Unknown0;
        public struct Struct1
        {
            public int Unknown2;
            public float Unknown3;
            
            public void Serialize(Serializer serializer)
            {
                serializer.WriteVarint(this.Unknown2);
                serializer.Write(this.Unknown3);
            }
        }
        public Struct1[] Unknown4;
        
        public override ushort Header
        {
            get
            {
                return 716;
            }
        }
        
        public override void Serialize(Serializer serializer)
        {
            serializer.Write(Header);
            serializer.WriteVarint(this.Unknown0);
            serializer.Write((byte)Unknown4.Length);
            for (int i = 0; i < Unknown4.Length; i++)
            {
                Unknown4[i].Serialize(serializer);
            }
        }
    }
}