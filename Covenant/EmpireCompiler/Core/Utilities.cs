﻿// Author: Ryan Cobb (@cobbr_io)
// Project: Covenant (https://github.com/cobbr/Covenant)
// License: GNU GPLv3

using System;
using System.IO;
using System.Threading.Tasks;
using System.IO.Compression;

namespace EmpireCompiler.Core
{
    public static class Utilities
    {
        public static byte[] Compress(byte[] bytes)
        {
            byte[] compressedBytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress))
                {
                    deflateStream.Write(bytes, 0, bytes.Length);
                }
                compressedBytes = memoryStream.ToArray();
            }
            return compressedBytes;
        }

        public static string CreateShortGuid()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
        }

        public static string GetSanitizedFilename(string filename)
        {
            foreach (char invalid in Path.GetInvalidFileNameChars())
            {
                filename = filename.Replace(invalid, '_');
            }
            return filename;
        }

        public static string GetExtensionForLanguage(Models.Grunts.ImplantLanguage language)
        {
            switch (language)
            {
                case Models.Grunts.ImplantLanguage.CSharp:
                    return ".cs";
                default:
                    return ".cs";
            }
        }
    }

    public static class TaskExtensions
    {
        public static T WaitResult<T>(this Task<T> Task)
        {
            return TaskExtensions.WaitTask(Task).Result;
        }

        public static Task<T> WaitTask<T>(this Task<T> Task)
        {
            Task.Wait();
            return Task;
        }

        public static T WaitResult<T>(this ValueTask<T> Task)
        {
            return TaskExtensions.WaitTask(Task).Result;
        }

        public static ValueTask<T> WaitTask<T>(this ValueTask<T> Task)
        {
            Task.AsTask().Wait();
            return Task;
        }
    }
}
