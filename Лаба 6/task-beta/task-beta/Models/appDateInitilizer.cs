using Microsoft.AspNetCore.Identity;

public class appDateInitilizer
{
    public static async Task Seed(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));

        if (!await roleManager.RoleExistsAsync("SimpleUser"))
            await roleManager.CreateAsync(new IdentityRole("SimpleUser"));

        string adminLogin = "LoginSecret@mail.ru";
        string adminPass = "PassSecret123";

        var adminUser = await userManager.FindByEmailAsync(adminLogin);
        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = adminLogin,
                Email = adminLogin
            };

            // Сначала создайте пользователя и сохраните его в базе данных
            var result = await userManager.CreateAsync(adminUser, adminPass);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
            else
            {
                // Выводим ошибку, если создание не удалось
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);  // Логируем ошибки
                }
            }

        }
    }
}
