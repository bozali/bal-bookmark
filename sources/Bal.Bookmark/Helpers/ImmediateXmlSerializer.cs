namespace Bal.Bookmark.Helpers
{
    using System.Xml.Serialization;

    using System.IO;

    public static class ImmediateXmlSerializer
    {
        public static T DeserializeOrDefault<T>(FileInfo file) where T : class, new()
        {
            var serializer = new XmlSerializer(typeof(T));

            if (file.Exists)
            {
                using var reader = file.OpenRead();
                return (T)serializer.Deserialize(reader);
            }

            if (file.Directory != null && !file.Directory.Exists)
            {
                file.Directory.Create();
            }

            using var writer = file.Open(FileMode.Create, FileAccess.Write);
            var obj = new T();
            serializer.Serialize(writer, obj);

            return obj;
        }

        public static T DeserializeOrDefault<T>(string path) where T : class, new()
        {
            return ImmediateXmlSerializer.DeserializeOrDefault<T>(new FileInfo(path));
        }

        public static void Serialize<T>(FileInfo file, T obj)
        {
            var serializer = new XmlSerializer(typeof(T));

            if (file.Directory != null && !file.Directory.Exists)
            {
                file.Directory.Create();
            }

            using var writer = file.Open(FileMode.Create, FileAccess.Write);
            serializer.Serialize(writer, obj);
        }


        public static void Serialize<T>(string path, T obj)
        {
            ImmediateXmlSerializer.Serialize(new FileInfo(path), obj);
        }
    }
}
