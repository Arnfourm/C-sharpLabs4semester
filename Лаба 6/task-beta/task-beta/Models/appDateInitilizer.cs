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

            var result = await userManager.CreateAsync(adminUser, adminPass);
            if (result.Succeeded)
            {
                Console.WriteLine("User successfully created");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
            else
            {
                Console.WriteLine("User creation failed:");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error: {error.Description}");
                }
            }
        }
    }

}
