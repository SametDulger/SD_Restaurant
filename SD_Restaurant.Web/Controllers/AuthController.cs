using Microsoft.AspNetCore.Mvc;
using SD_Restaurant.Web.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SD_Restaurant.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AuthController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl ?? string.Empty;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl ?? string.Empty;

            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient("ApiClient");
                    
                    var loginDto = new
                    {
                        Username = model.Username,
                        Password = model.Password
                    };

                    var json = JsonSerializer.Serialize(loginDto);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync("auth/login", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var authResponse = JsonSerializer.Deserialize<AuthResponseViewModel>(responseContent, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (authResponse?.Success == true)
                        {
                            // Store token in session
                            HttpContext.Session.SetString("JWTToken", authResponse.Token);
                            HttpContext.Session.SetString("Username", authResponse.Username);
                            HttpContext.Session.SetString("UserRoles", string.Join(",", authResponse.Roles));

                            if (model.RememberMe)
                            {
                                Response.Cookies.Append("RememberMe", authResponse.Token, new CookieOptions
                                {
                                    Expires = DateTime.Now.AddDays(30),
                                    HttpOnly = true
                                });
                            }

                            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }

                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        var errorResponse = JsonSerializer.Deserialize<dynamic>(errorContent);
                        ModelState.AddModelError("", "Giriş başarısız. Lütfen bilgilerinizi kontrol edin.");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Bir hata oluştu. Lütfen tekrar deneyin.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient("ApiClient");
                    
                    var registerDto = new
                    {
                        Username = model.Username,
                        Email = model.Email,
                        Password = model.Password,
                        ConfirmPassword = model.ConfirmPassword,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Phone = model.Phone
                    };

                    var json = JsonSerializer.Serialize(registerDto);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync("auth/register", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var authResponse = JsonSerializer.Deserialize<AuthResponseViewModel>(responseContent, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (authResponse?.Success == true)
                        {
                            // Store token in session
                            HttpContext.Session.SetString("JWTToken", authResponse.Token);
                            HttpContext.Session.SetString("Username", authResponse.Username);
                            HttpContext.Session.SetString("UserRoles", string.Join(",", authResponse.Roles));

                            TempData["Success"] = "Kayıt başarılı! Hoş geldiniz.";
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        var errorResponse = JsonSerializer.Deserialize<dynamic>(errorContent);
                        ModelState.AddModelError("", "Kayıt başarısız. Lütfen bilgilerinizi kontrol edin.");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Bir hata oluştu. Lütfen tekrar deneyin.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login");
            }

            // For now, return a simple profile view
            // In a real application, you would fetch user details from API
            var profile = new UserProfileViewModel
            {
                Username = username,
                Email = "user@example.com", // This should come from API
                FirstName = "Kullanıcı",
                LastName = "Adı",
                LastLoginDate = DateTime.Now,
                IsActive = true,
                Roles = HttpContext.Session.GetString("UserRoles")?.Split(',').ToList() ?? new List<string>()
            };

            return View(profile);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login");
            }

            return View(new ChangePasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient("ApiClient");
                    
                    // Add authorization header
                    var token = HttpContext.Session.GetString("JWTToken");
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    }

                    var changePasswordDto = new
                    {
                        CurrentPassword = model.CurrentPassword,
                        NewPassword = model.NewPassword,
                        ConfirmNewPassword = model.ConfirmNewPassword
                    };

                    var json = JsonSerializer.Serialize(changePasswordDto);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync("auth/change-password", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var successContent = await response.Content.ReadAsStringAsync();
                        try
                        {
                            var successResponse = JsonSerializer.Deserialize<dynamic>(successContent);
                            var successMessage = successResponse?.GetProperty("message")?.GetString() ?? "Şifreniz başarıyla değiştirildi.";
                            TempData["Success"] = successMessage;
                        }
                        catch
                        {
                            TempData["Success"] = "Şifreniz başarıyla değiştirildi.";
                        }
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        try
                        {
                            var errorResponse = JsonSerializer.Deserialize<dynamic>(errorContent);
                            var errorMessage = errorResponse?.GetProperty("message")?.GetString() ?? "Şifre değiştirme başarısız.";
                            ModelState.AddModelError("", errorMessage);
                        }
                        catch
                        {
                            ModelState.AddModelError("", "Şifre değiştirme başarısız. Lütfen mevcut şifrenizi kontrol edin.");
                        }
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Bir hata oluştu. Lütfen tekrar deneyin.");
                }
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("RememberMe");
            
            TempData["Success"] = "Başarıyla çıkış yaptınız.";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
} 