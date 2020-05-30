using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using apiweb.Models;
using apiweb.Services;
using Microsoft.AspNetCore.HttpOverrides;

namespace apiweb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => 
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters=new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            
            /////Cliente
            services.Configure<ClientestoreDatabaseSettings>(Configuration.GetSection(nameof(ClientestoreDatabaseSettings)));
            services.AddSingleton<IClientestoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<ClientestoreDatabaseSettings>>().Value);
            services.AddSingleton<ClienteService>();
            /////Cliente

            //****************************
            services.Configure<VehiculostoreDatabaseSettings>(Configuration.GetSection(nameof(VehiculostoreDatabaseSettings)));
            services.AddSingleton<IVehiculostoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<VehiculostoreDatabaseSettings>>().Value);
            services.AddSingleton<VehiculoService>();
            //****************************

            //****************************
            services.Configure<TallerstoreDatabaseSettings>(Configuration.GetSection(nameof(TallerstoreDatabaseSettings)));
            services.AddSingleton<ITallerstoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<TallerstoreDatabaseSettings>>().Value);
            services.AddSingleton<TallerService>();
            //****************************

            //****************************
            services.Configure<SolicitudstoreDatabaseSettings>(Configuration.GetSection(nameof(SolicitudstoreDatabaseSettings)));
            services.AddSingleton<ISolicitudstoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<SolicitudstoreDatabaseSettings>>().Value);
            services.AddSingleton<SolicitudService>();
            //***************************
             //****************************
            services.Configure<ComentariostoreDatabaseSettings>(Configuration.GetSection(nameof(ComentariostoreDatabaseSettings)));
            services.AddSingleton<IComentariostoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<ComentariostoreDatabaseSettings>>().Value);
            services.AddSingleton<ComentarioService>();
            //***************************

            //****************************
            services.Configure<MensajeriastoreDatabaseSettings>(Configuration.GetSection(nameof(MensajeriastoreDatabaseSettings)));
            services.AddSingleton<IMensajeriastoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<MensajeriastoreDatabaseSettings>>().Value);
            services.AddSingleton<MensajeriaService>();
            //****************************

            //services.AddCors(options => { options.AddPolicy("MyAllowSpecificOrigins", builder => { builder.WithOrigins() .AllowAnyHeader() .AllowAnyMethod(); }); });
            
			//services.AddCors(options => { options.AddPolicy("MyAllowSpecificOrigins", builder => { builder.WithOrigins("*") .AllowAnyHeader() .AllowAnyMethod(); }); });
			
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("*").SetIsOriginAllowed(isOriginAllowed: _ => true).AllowAnyHeader().AllowAnyMethod();
                });
            });
            /*
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = 
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddHttpsRedirection(options => options.HttpsPort = 5000);
            */
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            //app.UseCors("MyAllowSpecificOrigins");
			
			/*
			app.UseCors(builder => builder.WithOrigins("*")
                              .AllowAnyMethod()
                              .AllowAnyHeader());
            */

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
