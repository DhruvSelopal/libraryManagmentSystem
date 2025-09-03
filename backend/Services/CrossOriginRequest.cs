class CorsService
{
    private WebApplicationBuilder _builder;
    public CorsService(WebApplicationBuilder builder)
    {
        _builder = builder;
    }

    public void useCors()
    {
        _builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }
}