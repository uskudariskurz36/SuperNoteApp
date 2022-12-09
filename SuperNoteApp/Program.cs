namespace SuperNoteApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Session hizmetini aktifleþtirdik.
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "noteapp.session";    // session ID nin clinet tarafýnda tutulacaðý cookie adý.
                options.IdleTimeout = TimeSpan.FromMinutes(5);  // session sonlanma süresi ama site de iþlem yapýlmadýkca.
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseSession();   // Gelen isteklerde bu session hizmetini de aktifleþtir.
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}