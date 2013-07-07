﻿using System;
using System.Collections.Generic;
using GameRevision.GW2Emu.CodeWriter.CSharp;
using GameRevision.GW2Emu.CodeWriter.Headers;
using GameRevision.GW2Emu.CodeWriter.Xml;
using GameRevision.GW2Emu.CodeWriter.Messages.Fields;

namespace GameRevision.GW2Emu.CodeWriter.Messages
{
    public class MessageWriter
    {
        private CSharpWriter writer;
        private CommunicationDirection protocol;
        private PacketType message;
        private HeaderEnum headerEnum;
        private DateTime date;
        private int fieldNumber;

        public MessageWriter(CSharpWriter writer, CommunicationDirection protocol, PacketType message, HeaderEnum headerEnum, DateTime date)
        {
            this.writer = writer;
            this.protocol = protocol;
            this.message = message;
            this.headerEnum = headerEnum;
            this.date = date;
        }

        public void WriteMessage()
        {
            this.WriteComment();            
            this.writer.WriteLine();
            this.WriteUsing();
            this.writer.WriteLine();
            this.WriteNamespace();
        }

        private void WriteComment()
        {
            this.writer.WriteMultilineComment("This code was autogenerated by",
                                              "GameRevision.GW2Emu.CodeWriter.",
                                              "Date generated: " + this.date.ToString("dd-MM-yy"));
        }

        private void WriteUsing()
        {
            this.writer.WriteUsing("System");
            this.writer.WriteUsing("System.Net");
            this.writer.WriteUsing("GameRevision.GW2Emu.Core");
            this.writer.WriteUsing("GameRevision.GW2Emu.Core.Types");
            this.writer.WriteUsing("GameRevision.GW2Emu.Core.Serializers");
        }

        private void WriteNamespace()
        {
            ProtocolSimpleTypes type = protocol.type;
            string direction = string.Empty;

            if (type.GetPacketDirection() == PacketDirection.In)
            {
                direction = "CtoS";
            }
            else
            {
                direction = "StoC";
            }

            this.writer.WriteNamespace("Messages." + type.GetServerName().ToString() + "." + direction);
            this.writer.WriteInBlock(this.WriteClass);
        }

        private void WriteClass()
        {
            ProtocolSimpleTypes type = protocol.type;
            string baseClass = string.Empty;

            if (type.GetPacketDirection() == PacketDirection.Out)
            {
                baseClass = "GenericMessage";
            }
            else
            {
                baseClass = "GenericTriggerableMessage";
            }

            this.writer.WriteClass(this.headerEnum.NamesByHeader[message.header], baseClass);

            fieldNumber = 0;
            this.writer.WriteInBlock(delegate
            {
                this.WriteTypeMembers(this.headerEnum.NamesByHeader[message.header], message.Field, true);
            });
        }

        private void WriteTypeMembers(string typeName, IEnumerable<BasicFieldType> basicFields, bool isMessage)
        {
            var fields = new List<Field>();

            if (basicFields != null)
            {
                WriteFields(fields, basicFields);
                this.writer.WriteLine();
            }

            this.WriteHeaderProperty();
            this.WriteMethod(fields, isMessage);
        }

        private void WriteStruct(InnerStructFieldType structure)
        {
            this.writer.WriteStruct(structure.Name);
            this.writer.WriteInBlock(delegate
            {
                WriteTypeMembers(structure.Name, structure.Type.Field, false);
            });
        }

        private static InnerStructFieldType GetInnerStruct(FieldType fieldType)
        {
            OuterFieldType outerType = fieldType as OuterFieldType;

            if (outerType != null)
            {
                return outerType.InnerType as InnerStructFieldType;
            }

            return null;
        }

        private void WriteFields(List<Field> fields, IEnumerable<BasicFieldType> basicFields)
        {
            foreach (BasicFieldType basicField in basicFields)
            {
                FieldType fieldType = FieldTypeFactory.Create(basicField, this.writer);

                InnerStructFieldType innerStructFieldType = GetInnerStruct(fieldType);
                if (innerStructFieldType != null)
                {
                    innerStructFieldType.Name = "Struct" + fieldNumber++;
                    innerStructFieldType.Type = (StructFieldType)basicField;

                    WriteStruct(innerStructFieldType);
                }

                string fieldName = basicField.HasName()
                                       ? basicField.GetName()
                                       : "Unknown" + fieldNumber++;

                var field = new Field(fieldName, fieldType);

                field.Write();

                fields.Add(new Field("this." + field.Name, field.Type));
            }
        }

        private void WriteHeaderProperty()
        {
            this.writer.WriteOverridingProperty("ushort", "Header");
            this.writer.WriteInBlock(delegate
            {
                this.writer.WriteGet();
                this.writer.WriteInBlock(delegate
                {
                    this.writer.WriteReturn(message.header);
                });
            });
            this.writer.WriteLine();
        }

        private void WriteMethod(IEnumerable<Field> fields, bool isMessage)
        {
            if (protocol.type.GetPacketDirection() == PacketDirection.Out)
            {
                WriteSerializer(fields, isMessage);
            }
            else
            {
                WriteDeserializer(fields);
            }
        }

        private void WriteDeserializer(IEnumerable<Field> fields)
        {
            this.writer.WriteOverridingMethod(Deserializer.MessageMethod, Deserializer.Type + " " + Deserializer.Name);
            this.writer.WriteInBlock(delegate
            {
                foreach (Field field in fields)
                {
                    field.WriteDeserializer();
                }
            });
        }

        private void WriteSerializer(IEnumerable<Field> fields, bool isMessage)
        {
            this.writer.WriteOverridingMethod(Serializer.MessageMethod, Serializer.Type + " " + Serializer.Name);
            this.writer.WriteInBlock(delegate
            {
                if (isMessage)
                {
                    this.writer.WriteMethodCall("base", "Serialize", "serializer");
                }

                foreach (Field field in fields)
                {
                    field.WriteSerializer();
                }
            });
        }
    }
}
