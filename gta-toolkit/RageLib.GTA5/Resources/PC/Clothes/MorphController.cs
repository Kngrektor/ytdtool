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

using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Clothes
{
    // pgBase
    // phMorphController
    public class MorphController : PgBase64
    {
        public override long BlockLength => 0x40;

        // structure data
        public ulong Unknown_10h; // 0x0000000000000000
        public ulong Unknown_18h_Pointer;
        public ulong Unknown_20h_Pointer;
        public ulong Unknown_28h_Pointer;
        public ulong Unknown_30h; // 0x0000000000000000
        public ulong Unknown_38h; // 0x0000000000000000

        // reference data
        public Unknown_C_006 Unknown_18h_Data;
        public Unknown_C_006 Unknown_20h_Data;
        public Unknown_C_006 Unknown_28h_Data;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_10h = reader.ReadUInt64();
            this.Unknown_18h_Pointer = reader.ReadUInt64();
            this.Unknown_20h_Pointer = reader.ReadUInt64();
            this.Unknown_28h_Pointer = reader.ReadUInt64();
            this.Unknown_30h = reader.ReadUInt64();
            this.Unknown_38h = reader.ReadUInt64();

            // read reference data
            this.Unknown_18h_Data = reader.ReadBlockAt<Unknown_C_006>(
                this.Unknown_18h_Pointer // offset
            );
            this.Unknown_20h_Data = reader.ReadBlockAt<Unknown_C_006>(
                this.Unknown_20h_Pointer // offset
            );
            this.Unknown_28h_Data = reader.ReadBlockAt<Unknown_C_006>(
                this.Unknown_28h_Pointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.Unknown_18h_Pointer = (ulong)(this.Unknown_18h_Data != null ? this.Unknown_18h_Data.BlockPosition : 0);
            this.Unknown_20h_Pointer = (ulong)(this.Unknown_20h_Data != null ? this.Unknown_20h_Data.BlockPosition : 0);
            this.Unknown_28h_Pointer = (ulong)(this.Unknown_28h_Data != null ? this.Unknown_28h_Data.BlockPosition : 0);

            // write structure data
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_18h_Pointer);
            writer.Write(this.Unknown_20h_Pointer);
            writer.Write(this.Unknown_28h_Pointer);
            writer.Write(this.Unknown_30h);
            writer.Write(this.Unknown_38h);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Unknown_18h_Data != null) list.Add(Unknown_18h_Data);
            if (Unknown_20h_Data != null) list.Add(Unknown_20h_Data);
            if (Unknown_28h_Data != null) list.Add(Unknown_28h_Data);
            return list.ToArray();
        }
    }
}
