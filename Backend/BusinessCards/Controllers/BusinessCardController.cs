using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Interface;
using Domain.ViewModels;

namespace BusinessCardsProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessCardController : ControllerBase
    {
        private readonly IBusinessCard<BusinessCard> _businessCardService;

        public BusinessCardController(IBusinessCard<BusinessCard> businessCardService)
        {
            _businessCardService = businessCardService;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateBusinessCard([FromForm] BusinessCardViewModel model)
        {
            if (model == null)
            {
                return BadRequest(new StandardResponse<object>
                {
                    Success = false,
                    Message = "Invalid business card data.",
                    Error = "Request body is null."
                });
            }

            try
            {
                if (model.PhotoFile != null)
                {
                    if (model.PhotoFile.Length > 1 * 1024 * 1024)
                    {
                        return StatusCode(500, new StandardResponse<object>
                        {
                            Success = false,
                            Message = "File size exceeds 1 MB. Please upload a smaller file.",
                            Error = "File size exceeds 1 MB. Please upload a smaller file."
                        });
                    }

                }

                string? photoBase64 = await _businessCardService.GetBase64String(model.PhotoFile);

                var newCard = new BusinessCard
                {
                    Name = model.Name,
                    Gender = model.Gender,
                    DateOfBirth = (DateTime)model.DateOfBirth,
                    Email = model.Email,
                    Phone = model.Phone,
                    Address = model.Address,
                    PhotoBase64 = photoBase64
                };

                var result = await _businessCardService.CreateBusinessCardAsync(newCard);
                if (result > 0)
                {
                    return Ok(new StandardResponse<object>
                    {
                        Success = true,
                        Message = "Business card created successfully.",
                        Data = null
                    });
                }

                return StatusCode(500, new StandardResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while creating the business card.",
                    Error = "Database operation did not return the expected result."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new StandardResponse<object>
                {
                    Success = false,
                    Message = "An exception occurred while creating the business card.",
                    Error = ex.Message
                });
            }
        }
        [HttpPost("list")]
        public async Task<IActionResult> GetBusinessCards()
        {
            try
            {
                var cards = await _businessCardService.GetBusinessCardsAsync();
                return Ok(new StandardResponse<IEnumerable<BusinessCard>>
                {
                    Success = true,
                    Message = "Business cards retrieved successfully.",
                    Data = cards
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new StandardResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the business cards.",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetBusinessCardById([FromBody] IdViewModel cardId)
        {
            try
            {
                var card = await _businessCardService.GetBusinessCardByIdAsync(cardId.Id);
                if (card == null)
                {
                    return NotFound(new StandardResponse<object>
                    {
                        Success = false,
                        Message = "Business card not found.",
                        Error = $"No business card found with ID {cardId.Id}."
                    });
                }

                return Ok(new StandardResponse<BusinessCard>
                {
                    Success = true,
                    Message = "Business card retrieved successfully.",
                    Data = card
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new StandardResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the business card.",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteBusinessCard([FromBody] IdViewModel cardId)
        {
            try
            {
                var result = await _businessCardService.DeleteBusinessCardAsync(cardId.Id);
                if (result > 0)
                {
                    return Ok(new StandardResponse<object>
                    {
                        Success = true,
                        Message = "Business card deleted successfully.",
                        Data = null
                    });
                }

                return StatusCode(500, new StandardResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while deleting the business card.",
                    Error = "Database operation did not return the expected result."
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new StandardResponse<object>
                {
                    Success = false,
                    Message = "An exception occurred while deleting the business card.",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("filter")]
        public async Task<IActionResult> FilterBusinessCards([FromBody] BusinessCardFilter filter)
        {
            try
            {
                var cards = await _businessCardService.FilterBusinessCardsAsync(
                    filter.Name, filter.Email, filter.Phone, filter.Gender);
                return Ok(new StandardResponse<IEnumerable<BusinessCard>>
                {
                    Success = true,
                    Message = "Filtered business cards retrieved successfully.",
                    Data = cards
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new StandardResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while filtering the business cards.",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("export")]
        public async Task<IActionResult> ExportBusinessCards([FromBody] ExportFormatViewModel model)
        {
            try
            {
                var format = model.Format;

                var (fileContent, fileName) = await _businessCardService.ExportBusinessCardsAsync(format);
                if (fileContent == null)
                {
                    return NotFound(new StandardResponse<object>
                    {
                        Success = false,
                        Message = "No business cards available for export or invalid format.",
                        Error = "Either no business cards exist, or the requested format is invalid."
                    });
                }

                var contentType = format.Equals("csv", System.StringComparison.OrdinalIgnoreCase) ? "text/csv" : "application/xml";
                var file = File(fileContent, contentType, fileName);
                return file;
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new StandardResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while exporting the business cards.",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportBusinessCards([FromForm] ImportFormatViewModel file)
        {
            if (file == null || file.File.Length == 0)
            {
                return BadRequest("Invalid file. Please upload a valid file.");
            }

            try
            {
                await _businessCardService.ImportBusinessCardsAsync(file.File);
                return Ok(new { message = "Business cards imported successfully." });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the file.", error = ex.Message });
            }
        }
    }
}
