using Microsoft.EntityFrameworkCore;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Enums;

namespace SD_Restaurant.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void SeedData(RestaurantDbContext context)
        {
            // Kategoriler
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "İçecekler", Description = "Soğuk ve sıcak içecekler" },
                    new Category { Name = "Ana Yemekler", Description = "Ana yemek seçenekleri" },
                    new Category { Name = "Tatlılar", Description = "Çeşitli tatlılar" },
                    new Category { Name = "Salatalar", Description = "Taze salatalar" },
                    new Category { Name = "Kahvaltı", Description = "Kahvaltı menüsü" }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            // Malzemeler
            if (!context.Ingredients.Any())
            {
                var ingredients = new List<Ingredient>
                {
                    new Ingredient { Name = "Kahve Çekirdeği", Description = "Taze kahve çekirdeği", Unit = "kg", Cost = 120.00m },
                    new Ingredient { Name = "Süt", Description = "Taze süt", Unit = "Litre", Cost = 15.00m },
                    new Ingredient { Name = "Şeker", Description = "Beyaz şeker", Unit = "kg", Cost = 8.00m },
                    new Ingredient { Name = "Un", Description = "Buğday unu", Unit = "kg", Cost = 12.00m },
                    new Ingredient { Name = "Yumurta", Description = "Taze yumurta", Unit = "Adet", Cost = 2.50m },
                    new Ingredient { Name = "Tereyağı", Description = "Tereyağı", Unit = "kg", Cost = 80.00m },
                    new Ingredient { Name = "Domates", Description = "Taze domates", Unit = "kg", Cost = 20.00m },
                    new Ingredient { Name = "Marul", Description = "Taze marul", Unit = "Adet", Cost = 8.00m }
                };
                context.Ingredients.AddRange(ingredients);
                context.SaveChanges();
            }

            // Ürünler
            if (!context.Products.Any())
            {
                var categories = context.Categories.ToList();
                var products = new List<Product>
                {
                    new Product { Name = "Türk Kahvesi", Description = "Geleneksel Türk kahvesi", Price = 25.00m, Unit = "Fincan", CategoryId = categories.First(c => c.Name == "İçecekler").Id, IsRecipe = true },
                    new Product { Name = "Espresso", Description = "Tek shot espresso", Price = 20.00m, Unit = "Shot", CategoryId = categories.First(c => c.Name == "İçecekler").Id, IsRecipe = true },
                    new Product { Name = "Latte", Description = "Sütlü kahve", Price = 35.00m, Unit = "Bardak", CategoryId = categories.First(c => c.Name == "İçecekler").Id, IsRecipe = true },
                    new Product { Name = "Cappuccino", Description = "Köpüklü kahve", Price = 30.00m, Unit = "Bardak", CategoryId = categories.First(c => c.Name == "İçecekler").Id, IsRecipe = true },
                    new Product { Name = "Çay", Description = "Demli çay", Price = 15.00m, Unit = "Fincan", CategoryId = categories.First(c => c.Name == "İçecekler").Id, IsRecipe = false },
                    new Product { Name = "Su", Description = "Maden suyu", Price = 8.00m, Unit = "Şişe", CategoryId = categories.First(c => c.Name == "İçecekler").Id, IsRecipe = false },
                    new Product { Name = "Cheesecake", Description = "New York cheesecake", Price = 45.00m, Unit = "Dilim", CategoryId = categories.First(c => c.Name == "Tatlılar").Id, IsRecipe = true },
                    new Product { Name = "Tiramisu", Description = "İtalyan tatlısı", Price = 50.00m, Unit = "Porsiyon", CategoryId = categories.First(c => c.Name == "Tatlılar").Id, IsRecipe = true },
                    new Product { Name = "Caesar Salata", Description = "Caesar salata", Price = 40.00m, Unit = "Porsiyon", CategoryId = categories.First(c => c.Name == "Salatalar").Id, IsRecipe = true },
                    new Product { Name = "Çoban Salata", Description = "Geleneksel çoban salata", Price = 35.00m, Unit = "Porsiyon", CategoryId = categories.First(c => c.Name == "Salatalar").Id, IsRecipe = true }
                };
                context.Products.AddRange(products);
                context.SaveChanges();
            }

            // Masalar
            if (!context.Tables.Any())
            {
                var tables = new List<Table>
                {
                    new Table { TableNumber = "M1", Capacity = 4, Status = TableStatus.Available, Location = "Bahçe", IsAvailable = true },
                    new Table { TableNumber = "M2", Capacity = 4, Status = TableStatus.Available, Location = "Bahçe", IsAvailable = true },
                    new Table { TableNumber = "M3", Capacity = 6, Status = TableStatus.Available, Location = "İç Mekan", IsAvailable = true },
                    new Table { TableNumber = "M4", Capacity = 6, Status = TableStatus.Available, Location = "İç Mekan", IsAvailable = true },
                    new Table { TableNumber = "M5", Capacity = 2, Status = TableStatus.Available, Location = "Bar", IsAvailable = true },
                    new Table { TableNumber = "M6", Capacity = 8, Status = TableStatus.Available, Location = "VIP", IsAvailable = true }
                };
                context.Tables.AddRange(tables);
                context.SaveChanges();
            }

            // Müşteriler
            if (!context.Customers.Any())
            {
                var customers = new List<Customer>
                {
                    new Customer { FirstName = "Ahmet", LastName = "Yılmaz", Email = "ahmet@email.com", Phone = "0555-111-1111", Address = "İstanbul", CustomerType = CustomerType.Regular, TotalSpent = 0, VisitCount = 0 },
                    new Customer { FirstName = "Ayşe", LastName = "Demir", Email = "ayse@email.com", Phone = "0555-222-2222", Address = "Ankara", CustomerType = CustomerType.Regular, TotalSpent = 0, VisitCount = 0 },
                    new Customer { FirstName = "Mehmet", LastName = "Kaya", Email = "mehmet@email.com", Phone = "0555-333-3333", Address = "İzmir", CustomerType = CustomerType.Regular, TotalSpent = 0, VisitCount = 0 },
                    new Customer { FirstName = "Fatma", LastName = "Özkan", Email = "fatma@email.com", Phone = "0555-444-4444", Address = "Bursa", CustomerType = CustomerType.Regular, TotalSpent = 0, VisitCount = 0 }
                };
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }

            // Personel
            if (!context.Employees.Any())
            {
                var employees = new List<Employee>
                {
                    new Employee { FirstName = "Ali", LastName = "Veli", Email = "ali@restaurant.com", Phone = "0555-555-5555", Position = "Garson", Department = "Servis", Salary = 8000.00m },
                    new Employee { FirstName = "Zeynep", LastName = "Kara", Email = "zeynep@restaurant.com", Phone = "0555-666-6666", Position = "Kasiyer", Department = "Muhasebe", Salary = 9000.00m },
                    new Employee { FirstName = "Can", LastName = "Yıldız", Email = "can@restaurant.com", Phone = "0555-777-7777", Position = "Şef", Department = "Mutfak", Salary = 12000.00m },
                    new Employee { FirstName = "Elif", LastName = "Aydın", Email = "elif@restaurant.com", Phone = "0555-888-8888", Position = "Barista", Department = "Bar", Salary = 8500.00m }
                };
                context.Employees.AddRange(employees);
                context.SaveChanges();
            }

            // Stoklar
            if (!context.Stocks.Any())
            {
                var products = context.Products.Where(p => p.IsRecipe).ToList();
                var ingredients = context.Ingredients.ToList();
                var stocks = new List<Stock>();

                // Ürün stokları
                foreach (var product in products)
                {
                    stocks.Add(new Stock
                    {
                        ProductId = product.Id,
                        Quantity = 50,
                        Unit = "Adet",
                        MinimumStock = 10,
                        Location = "Depo",
                        Cost = product.Price * 0.6m // %60 maliyet
                    });
                }

                // Malzeme stokları
                foreach (var ingredient in ingredients)
                {
                    stocks.Add(new Stock
                    {
                        IngredientId = ingredient.Id,
                        Quantity = 100,
                        Unit = ingredient.Unit,
                        MinimumStock = 20,
                        Location = "Depo",
                        Cost = ingredient.Cost
                    });
                }

                context.Stocks.AddRange(stocks);
                context.SaveChanges();
            }

            // Roller
            if (!context.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role { Name = "Admin", Description = "Sistem yöneticisi" },
                    new Role { Name = "Manager", Description = "Restaurant müdürü" },
                    new Role { Name = "Waiter", Description = "Garson" },
                    new Role { Name = "Cashier", Description = "Kasiyer" },
                    new Role { Name = "Chef", Description = "Şef" }
                };
                context.Roles.AddRange(roles);
                context.SaveChanges();
            }

            // Kullanıcılar
            if (!context.Users.Any())
            {
                // Hash passwords
                string HashPassword(string password)
                {
                    using var sha256 = System.Security.Cryptography.SHA256.Create();
                    var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                    return Convert.ToBase64String(hashedBytes);
                }

                var users = new List<User>
                {
                    new User
                    {
                        Username = "admin",
                        Email = "admin@restaurant.com",
                        PasswordHash = HashPassword("admin123"),
                        FirstName = "Admin",
                        LastName = "User",
                        Phone = "0555-999-9999",
                        IsActive = true,
                        LastLoginDate = DateTime.UtcNow
                    },
                    new User
                    {
                        Username = "manager",
                        Email = "manager@restaurant.com",
                        PasswordHash = HashPassword("manager123"),
                        FirstName = "Manager",
                        LastName = "User",
                        Phone = "0555-000-0000",
                        IsActive = true,
                        LastLoginDate = DateTime.UtcNow
                    }
                };
                context.Users.AddRange(users);
                context.SaveChanges();

                // Kullanıcı rolleri
                var adminUser = users.First(u => u.Username == "admin");
                var managerUser = users.First(u => u.Username == "manager");
                var adminRole = context.Roles.First(r => r.Name == "Admin");
                var managerRole = context.Roles.First(r => r.Name == "Manager");

                var userRoles = new List<UserRole>
                {
                    new UserRole { UserId = adminUser.Id, RoleId = adminRole.Id },
                    new UserRole { UserId = managerUser.Id, RoleId = managerRole.Id }
                };
                context.UserRoles.AddRange(userRoles);
                context.SaveChanges();
            }
        }
    }
} 