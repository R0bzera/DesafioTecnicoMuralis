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
                    ["nome"] = new OpenApiString("Rob"),
                    ["enderecos"] = new OpenApiArray
                {
                    new OpenApiObject
                    {
                        ["cep"] = new OpenApiString("01001000"),
                        ["numero"] = new OpenApiString("123"),
                        ["complemento"] = new OpenApiString("Casa 2, ap 101")
                    }
                },
                    ["contatos"] = new OpenApiArray
                {
                    new OpenApiObject
                    {
                        ["tipoContato"] = new OpenApiInteger(1),
                        ["contato"] = new OpenApiString("rob@email.com")
                    }
                }
                };
            }
        }
    }
}
