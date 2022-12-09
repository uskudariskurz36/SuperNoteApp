namespace SuperNoteApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Session hizmetini aktifle�tirdik.
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "noteapp.session";    // session ID nin clinet taraf�nda tutulaca�� cookie ad�.
                options.IdleTimeout = TimeSpan.FromMinutes(5);  // session sonlanma s�resi ama site de i�lem yap�lmad�kca.
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseSession();   // Gelen isteklerde bu session hizmetini de aktifle�tir.
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