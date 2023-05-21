using Microsoft.OpenApi.Models;

namespace App.GroupChat.Api.Extensions {
    public static class SwaggerDocumentGenerationExtension {
        public static void AddSwaggerDocumentGeneration(this IServiceCollection services) {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Group Chat Api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Description = "Please insert JWT token into field"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {{
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                        }});
            });
        }
    }
}
