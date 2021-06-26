/*
    Copyright(c) 2017 Neodymium

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using RageLib.Resources.Common;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Clothes
{
    // clothController
    public class ClothController : PgBase64
    {
        public override long BlockLength => 0x80;

        // structure data
        public ulong BridgeSimGfxPointer;
        public ulong MorphControllerPointer;
        public ulong VerletCloth1Pointer;
        public ulong VerletCloth2Pointer;
        public ulong VerletCloth3Pointer;
        public ulong Unknown_38h; // 0x0000000000000000
        public ulong Unknown_40h; // 0x0000000000000000
        public ulong Unknown_48h; // 0x0000000000000000
        public uint Type;
        public uint Unknown_54h; // 0x00000000
        public string32_r Name;
        public ulong Unknown_78h; // 0x0000000000000000

        // reference data
        public ClothBridgeSimGfx BridgeSimGfx;
        public MorphController MorphController;
        public VerletCloth VerletCloth1;
        public VerletCloth VerletCloth2;
        public VerletCloth VerletCloth3;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.BridgeSimGfxPointer = reader.ReadUInt64();
            this.MorphControllerPointer = reader.ReadUInt64();
            this.VerletCloth1Pointer = reader.ReadUInt64();
            this.VerletCloth2Pointer = reader.ReadUInt64();
            this.VerletCloth3Pointer = reader.ReadUInt64();
            this.Unknown_38h = reader.ReadUInt64();
            this.Unknown_40h = reader.ReadUInt64();
            this.Unknown_48h = reader.ReadUInt64();
            this.Type = reader.ReadUInt32();
            this.Unknown_54h = reader.ReadUInt32();
            this.Name = reader.ReadBlock<string32_r>();
            this.Unknown_78h = reader.ReadUInt64();

            // read reference data
            this.BridgeSimGfx = reader.ReadBlockAt<ClothBridgeSimGfx>(
                this.BridgeSimGfxPointer // offset
            );
            this.MorphController = reader.ReadBlockAt<MorphController>(
                this.MorphControllerPointer // offset
            );
            this.VerletCloth1 = reader.ReadBlockAt<VerletCloth>(
                this.VerletCloth1Pointer // offset
            );
            this.VerletCloth2 = reader.ReadBlockAt<VerletCloth>(
                this.VerletCloth2Pointer // offset
            );
            this.VerletCloth3 = reader.ReadBlockAt<VerletCloth>(
                this.VerletCloth3Pointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.BridgeSimGfxPointer = (ulong)(this.BridgeSimGfx != null ? this.BridgeSimGfx.BlockPosition : 0);
            this.MorphControllerPointer = (ulong)(this.MorphController != null ? this.MorphController.BlockPosition : 0);
            this.VerletCloth1Pointer = (ulong)(this.VerletCloth1 != null ? this.VerletCloth1.BlockPosition : 0);
            this.VerletCloth2Pointer = (ulong)(this.VerletCloth2 != null ? this.VerletCloth2.BlockPosition : 0);
            this.VerletCloth3Pointer = (ulong)(this.VerletCloth3 != null ? this.VerletCloth3.BlockPosition : 0);

            // write structure data
            writer.Write(this.BridgeSimGfxPointer);
            writer.Write(this.MorphControllerPointer);
            writer.Write(this.VerletCloth1Pointer);
            writer.Write(this.VerletCloth2Pointer);
            writer.Write(this.VerletCloth3Pointer);
            writer.Write(this.Unknown_38h);
            writer.Write(this.Unknown_40h);
            writer.Write(this.Unknown_48h);
            writer.Write(this.Type);
            writer.Write(this.Unknown_54h);
            writer.WriteBlock(this.Name);
            writer.Write(this.Unknown_78h);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (BridgeSimGfx != null) list.Add(BridgeSimGfx);
            if (MorphController != null) list.Add(MorphController);
            if (VerletCloth1 != null) list.Add(VerletCloth1);
            if (VerletCloth2 != null) list.Add(VerletCloth2);
            if (VerletCloth3 != null) list.Add(VerletCloth3);
            return list.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x58, Name)
            };
        }
    }
}
