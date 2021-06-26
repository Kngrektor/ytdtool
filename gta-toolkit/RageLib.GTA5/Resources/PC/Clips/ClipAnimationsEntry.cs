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

using System;

namespace RageLib.Resources.GTA5.PC.Clips
{
    public class ClipAnimationsEntry : ResourceSystemBlock
    {
        public override long BlockLength => 24;

        // structure data
        public float StartTime;
        public float EndTime;
        public float Rate;
        public uint Unknown_Ch; // 0x00000000
        public ulong AnimationPointer;

        // reference data
        public Animation Animation;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.StartTime = reader.ReadSingle();
            this.EndTime = reader.ReadSingle();
            this.Rate = reader.ReadSingle();
            this.Unknown_Ch = reader.ReadUInt32();
            this.AnimationPointer = reader.ReadUInt64();

            // read reference data
            this.Animation = reader.ReadBlockAt<Animation>(
                this.AnimationPointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            this.AnimationPointer = (ulong)(this.Animation != null ? this.Animation.BlockPosition : 0);

            // write structure data
            writer.Write(this.StartTime);
            writer.Write(this.EndTime);
            writer.Write(this.Rate);
            writer.Write(this.Unknown_Ch);
            writer.Write(this.AnimationPointer);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            return Animation == null ? Array.Empty<IResourceBlock>() : new IResourceBlock[] { Animation };
        }
    }
}
