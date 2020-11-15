using LojaVirtual.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using LojaVirtual.Repository;
using LojaVirtual.Repository.Contract;
using LojaVirtual.Libraries.Session;
using LojaVirtual.Libraries.Login;
using System.Net.Mail;
using System.Net;
using LojaVirtual.Libraries.Email;

namespace LojaVirtual
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*Padrao repository sendo usado
             * 
             */
            services.AddHttpContextAccessor();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<INewsletterRepository, NewsletterRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            /*
             * SMTP
             */
            services.AddScoped<SmtpClient>(options =>
            {
                SmtpClient smtp = new SmtpClient()
                {
                    Host = Configuration.GetValue<string>("Email:ServerSMTP"),
                    Port = Configuration.GetValue<int>("Email:ServerPort"),
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Configuration.GetValue<string>("Email:Username"), Configuration.GetValue<string>("Email:Password")),
                    EnableSsl = true
                };

                return smtp;
            });
            services.AddScoped<GerenciarEmail>();




            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //Session - Configuration


            //Como vai ser armazenado a secao
            services.AddMemoryCache();//Guarda os dados na memoria
            services.AddSession(options =>
            {


            });
            services.AddScoped<Session>();
            services.AddScoped<LoginUsuario>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<LojaVirtualContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            /*
             * https://www.site.com.br -> Qual controlador? (Gestão) -> Rotas
             * https://www.site.com.br/Produto/Visualizar/MouseRazorZK
             * https://www.site.com.br/Produto/Visualizar/10
             * https://www.site.com.br/Produto/Visualizar -> Listagem de todos os produtos
             * 
             * https://www.site.com.br -> https://www.site.com.br/Home/Index
             * https://www.site.com.br/Produto -> https://www.site.com.br/Produto/Index
             */


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "areas",
                template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );

                routes.MapRoute(
                    name: "default",
                    template: "/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
