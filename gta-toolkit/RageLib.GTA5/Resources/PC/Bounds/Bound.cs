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
using System.Numerics;
using RageLib.Resources.Common;

namespace RageLib.Resources.GTA5.PC.Bounds
{
    // phBoundBase
    // phBound
    public class Bound : PgBase64, IResourceXXSystemBlock
    {
        public override long BlockLength => 0x70;

        // structure data
        public byte Type;
        public byte Flags;
        public ushort PartIndex;
        public float RadiusAroundCentroid;
        public uint Unknown_18h;
        public uint Unknown_1Ch;
        public Vector3 BoundingBoxMax;
        public float Margin;
        public Vector3 BoundingBoxMin;
        public uint RefCount;
        public Vector3 CentroidOffset;
        public uint MaterialId0;
        public Vector3 CGOffset;
        public uint MaterialId1;
        public Vector4 VolumeDistribution;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Type = reader.ReadByte();
            this.Flags = reader.ReadByte();
            this.PartIndex = reader.ReadUInt16();
            this.RadiusAroundCentroid = reader.ReadSingle();
            this.Unknown_18h = reader.ReadUInt32();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.BoundingBoxMax = reader.ReadVector3();
            this.Margin = reader.ReadSingle();
            this.BoundingBoxMin = reader.ReadVector3();
            this.RefCount = reader.ReadUInt32();
            this.CentroidOffset = reader.ReadVector3();
            this.MaterialId0 = reader.ReadUInt32();
            this.CGOffset = reader.ReadVector3();
            this.MaterialId1 = reader.ReadUInt32();
            this.VolumeDistribution = reader.ReadVector4();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.Type);
            writer.Write(this.Flags);
            writer.Write(this.PartIndex);
            writer.Write(this.RadiusAroundCentroid);
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_1Ch);
            writer.Write(this.BoundingBoxMax);
            writer.Write(this.Margin);
            writer.Write(this.BoundingBoxMin);
            writer.Write(this.RefCount);
            writer.Write(this.CentroidOffset);
            writer.Write(this.MaterialId0);
            writer.Write(this.CGOffset);
            writer.Write(this.MaterialId1);
            writer.Write(this.VolumeDistribution);
        }

        public IResourceSystemBlock GetType(ResourceDataReader reader, params object[] parameters)
        {
            reader.Position += 16;
            var type = reader.ReadByte();
            reader.Position -= 17;

            switch (type)
            {
                case 0: return new BoundSphere();
                case 1: return new BoundCapsule();
                case 3: return new BoundBox();
                case 4: return new BoundGeometry();
                case 8: return new BoundBVH();
                case 10: return new BoundComposite();
                case 12: return new BoundDisc();
                case 13: return new BoundCylinder();
                case 15: return new BoundPlane();
                default: throw new Exception("Unknown bound type");
            }
        }
    }
}
