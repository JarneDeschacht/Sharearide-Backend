﻿using System;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Sharearide.DTOs;
using Sharearide.Models;

namespace Sharearide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UploadController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {

                var file = Request.Form.Files[0];
                var folderName = Path.Combine("wwwroot","Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string part1 = fileName.Split('.')[0];
                    string part2 = fileName.Split('.')[1];
                    fileName = part1 + DateTime.Now.ToString().Replace(" ", "").Replace(":", "").Replace("/", "_") + '.' + part2;
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);


                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    dbPath = dbPath.Substring(8);
                    return Ok(new { dbPath} );
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("addUrlToUser")]
        public IActionResult AddUrlToUser(ImageDTO model)
        {
            var user = _userRepository.GetById(model.id);
            if (user == null) return NotFound();
            user.URL = model.URL;
            _userRepository.SaveChanges();
            return Ok(new { model.URL});
        }
    }
}