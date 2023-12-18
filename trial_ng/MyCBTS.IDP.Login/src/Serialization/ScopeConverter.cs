using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace MyCBTS.IDP.Login.Serialization
{
    public class ScopeLite
    {
        public string Name { get; set; }
    }

    public class ScopeConverter : JsonConverter
    {
        private readonly IResourceStore scopeStore;

        public ScopeConverter(IResourceStore scopeStore)
        {
            if (scopeStore == null) throw new ArgumentNullException("scopeStore");

            this.scopeStore = scopeStore;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(ApiScope) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var source = serializer.Deserialize<ScopeLite>(reader);
            var scopes = AsyncHelper.RunSync(async () => await scopeStore.FindResourcesByScopeAsync(new string[] { source.Name }));
            return scopes.IdentityResources.Single();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var source = (ApiScope)value;

            var target = new ScopeLite
            {
                Name = source.Name
            };
            serializer.Serialize(writer, target);
        }
    }
}