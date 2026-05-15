using Microsoft.AspNetCore.Components;

namespace ProductManagement.Features.Helpers;

public class RedirectToLogin : ComponentBase
{
    [Inject] NavigationManager Navigation { get; set; } = default!;

    protected override void OnInitialized()
        => Navigation.NavigateTo("/login", forceLoad: true);
}

