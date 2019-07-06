using System;
using System.Drawing;
using Newtonsoft.Json;
using System.IO;
namespace ClientCSharpNodeSocket
{
    public class ImageConverter : JsonConverter
    {

        public override bool CanConvert(Type objectType)
        {
            //Habilitamos para que pueda convertir imagenes
            return objectType == typeof(Image);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //guardamos la imagen en un MemoryStream
            using (var img = (Image)value)
            {
                var ms = new MemoryStream();
                img.Save(ms, img.RawFormat);
                ms.Close();
                byte[] data = ms.ToArray();
                // Escribimos el la imagen al JsonWriter, la cual será convertida a 
                // un string base64 por JSON.Net
                writer.WriteValue(data);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //Obtenemos el string base64 que contiene la imagen
            var base64 = (string)reader.Value;
            // y lo convertimos en la imagen
            return Image.FromStream(new MemoryStream(Convert.FromBase64String(base64)));
        }
    }


    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreationDate { get; set; }

        [JsonConverter(typeof(ImageConverter))]
        public Image Avatar { get; set; }

        public Employee(int ID, string Name, string Surname, DateTime CreationDate, Image Avatar)
        {
            this.ID = ID;
            this.Name = Name;
            this.Surname = Surname;
            this.CreationDate = CreationDate;
            this.Avatar = Avatar;
        }
    }
}
