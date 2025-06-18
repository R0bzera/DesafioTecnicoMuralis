using DesafioTecnicoMuralis.API.DTOs;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DesafioTecnicoMuralis.API.SwaggerExemplos
{
    public class ClienteDtoExample : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(ClienteDto))
            {
                schema.Example = new OpenApiObject
                {
                    ["nome"] = new OpenApiString("Cliente"),
                    ["enderecos"] = new OpenApiArray
                {
                    new OpenApiObject
                    {
                        ["cep"] = new OpenApiString("01001000"),
                        ["numero"] = new OpenApiString("123")
                    }
                },
                    ["contatos"] = new OpenApiArray
                {
                    new OpenApiObject
                    {
                        ["tipo"] = new OpenApiString("Email ou Telefone"),
                        ["valor"] = new OpenApiString("Cliente@email.com ou 1199999-9999")
                    }
                }
                };
            }
        }
    }
}
