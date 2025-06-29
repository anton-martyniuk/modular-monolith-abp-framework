using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shipments.Shipments;
using Shipments.Shipments.Models;

namespace Shipments.Web.Pages.Shipments;

public class IndexModel : PageModel
{
    private readonly IShipmentService _shipmentService;

    public List<ShipmentResponse> Shipments { get; set; } = new();

    [TempData]
    public string SuccessMessage { get; set; }

    [TempData]
    public string CreatedShipmentNumber { get; set; }

    public IndexModel(IShipmentService shipmentService)
    {
        _shipmentService = shipmentService;
    }

    public async Task OnGetAsync()
    {
        Shipments = await _shipmentService.GetAllAsync();
    }

    public async Task<IActionResult> OnPostUpdateStatusAsync(string shipmentNumber, ShipmentStatus status)
    {
        await _shipmentService.UpdateShipmentStatusAsync(shipmentNumber, new UpdateShipmentStatusRequest(status));

        return RedirectToPage();
    }
}
