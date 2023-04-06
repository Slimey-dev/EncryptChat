using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EncryptChat.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EncryptChat.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserApiController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        
        [ActionName("GetApiKey")]
        [HttpPost]
        public async Task<string> GetApiKey()
        {
            string email = Request.Form["email"];
            string password = Request.Form["password"];
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, password);
                if (result)
                {
                    var apiKey = _context.ApiKeys.FirstOrDefault(x => x.UserId == new Guid(user.Id));
                    if (apiKey != null)
                    {
                        return apiKey.Key;
                    }
                    else
                    {
                        // Generate api key and put it into the database
                        var newApiKey = new ApiKey
                        {
                            Key = Guid.NewGuid().ToString(),
                            UserId = new Guid(user.Id)
                        };
                        
                        _context.ApiKeys.Add(newApiKey);
                        await _context.SaveChangesAsync();
                        return newApiKey.Key;
                    }
                }
                // Credentials are valid
            }
            else
            {
                // Credentials are invalid
            }
            return "null";
        }
        
        [ActionName("GetPublicKey")]
        [HttpGet]
        public async Task<string> GetPublicKey(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var publicKey = _context.PublicKeys.FirstOrDefault(x => x.UserId == new Guid(user.Id));
                if (publicKey != null)
                {
                    return publicKey.Key;
                }
            }
            return "null";
        }
        
        // Set public key, using API key to authenticate
        [ActionName("SetPublicKey")]
        [HttpPost]
        public async Task<string> SetPublicKey()
        {
            string apiKey = Request.Form["apiKey"];
            string publicKey = Request.Form["publicKey"];
            var key = _context.ApiKeys.FirstOrDefault(x => x.Key == apiKey);
            if (key != null)
            {
                var user = await _userManager.FindByIdAsync(key.UserId.ToString());
                if (user != null)
                {
                    var publicKeyDb = _context.PublicKeys.FirstOrDefault(x => x.UserId == new Guid(user.Id));
                    if (publicKeyDb != null)
                    {
                        publicKeyDb.Key = publicKey;
                        _context.PublicKeys.Update(publicKeyDb);
                        await _context.SaveChangesAsync();
                        return "Updated";
                    }
                    else
                    {
                        var newPublicKey = new PublicKey
                        {
                            Key = publicKey,
                            UserId = new Guid(user.Id)
                        };
                        _context.PublicKeys.Add(newPublicKey);
                        await _context.SaveChangesAsync();
                        return "Added";
                    }
                }
            }
            return "null";
        }
    }
}
