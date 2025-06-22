using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebForWork.WebApi.Models.Chapter;

namespace WebForWork.WebApi.Utils
{
    public class GetResponseModelConverter : JsonConverter<GetResponseModel>
    {
        public override GetResponseModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, GetResponseModel value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
            /*  writer.WriteStartObject();
              foreach (var kvp in value.ArticlesAttributes)
              {
                  writer.WriteString(kvp. string.Join(", ", kvp.Value));
              }
              writer.WriteEndObject();*/
        }
    }
}
