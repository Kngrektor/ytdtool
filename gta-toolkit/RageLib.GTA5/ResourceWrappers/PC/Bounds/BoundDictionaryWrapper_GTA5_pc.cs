﻿/*
    Copyright(c) 2015 Neodymium

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
using RageLib.Resources.GTA5.PC.Bounds;
using RageLib.ResourceWrappers.Bounds;
using System;

namespace RageLib.GTA5.ResourceWrappers.PC.Bounds
{
    public class BoundDictionaryWrapper_GTA5_pc : IBoundDictionary
    {
        private PgDictionary64<Bound> boundDictionary;

        public BoundDictionaryWrapper_GTA5_pc(PgDictionary64<Bound> boundDictionary)
        {
            this.boundDictionary = boundDictionary;
        }

        public IBoundList Bounds
        {
            get
            {
                return new BoundListWrapper_GTA5_pc(boundDictionary.Values.Entries);
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public uint GetHash(int index)
        {
            return (uint)boundDictionary.Hashes.Entries[index];
        }
    }
}