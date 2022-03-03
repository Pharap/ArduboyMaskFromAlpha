using System.Drawing;
using System.IO;

//
//  Copyright (C) 2022 Pharap (@Pharap)
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//

namespace MaskFromAlpha
{
    class Program
    {
        static void Main(string[] arguments)
        {
            foreach (var argument in arguments)
                Process(argument);
        }

        static void Process(string path)
        {
            using (var source = (Bitmap)Bitmap.FromFile(path))
            using (var desintation = GenerateMaskFromAlpha(source))
                desintation.Save(GeneratePath(path));
        }

        static Bitmap GenerateMaskFromAlpha(Bitmap source)
        {
            var destination = new Bitmap(source.Width, source.Height);

            for (int y = 0; y < destination.Height; ++y)
                for (int x = 0; x < destination.Width; ++x)
                    destination.SetPixel(x, y, (source.GetPixel(x, y).A > 127) ? Color.White : Color.Black);

            return destination;
        }

        static string GeneratePath(string path)
        {
            var directoryName = Path.GetDirectoryName(path);
            var fileName = Path.GetFileNameWithoutExtension(path);
            var extension = Path.GetExtension(path);

            return Path.Combine(directoryName, fileName + "Mask" + extension);
        }
    }
}
