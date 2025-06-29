using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shipments.Shipments;
using Shipments.Shipments.Models;
using Stocks.Integration;
using Stocks.Models;
using Volo.Abp;

namespace Shipments.Web.Pages.Shipments;

public class CreateModel : PageModel
{
    private readonly IShipmentService _shipmentService;
    private readonly IStockIntegrationService _stockIntegrationService;

    [BindProperty]
    public CreateShipmentRequest CreateShipmentRequest { get; set; } = new(
        "", "", new Address { Street = "", City = "", Zip = "" }, "", "", new List<ShipmentItemRequest>());

    [TempData]
    public string SuccessMessage { get; set; }
    public string? ErrorMessage { get; set; }

    [TempData]
    public string CreatedShipmentNumber { get; set; }
    
    public List<StockItemDto> AvailableStocks { get; set; } = new();

    public CreateModel(IShipmentService shipmentService, IStockIntegrationService stockIntegrationService)
    {
        _shipmentService = shipmentService;
        _stockIntegrationService = stockIntegrationService;
    }

    public async Task OnGetAsync()
    {
        AvailableStocks = await _stockIntegrationService.GetAllStocksAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            var response = await _shipmentService.CreateShipmentAsync(CreateShipmentRequest);

            SuccessMessage = $"Shipment with number {response.Number} was created successfully";
            CreatedShipmentNumber = response.Number;

            return RedirectToPage("./Index");
        }
        catch (UserFriendlyException e)
        {
            ErrorMessage = e.Message;
            return Page();
        }
    }
}
