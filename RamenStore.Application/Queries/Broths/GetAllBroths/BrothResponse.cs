using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamenStore.Application.Queries.Broths.GetAllBroths;

public sealed class BrothResponse
{
    public string Id { get; init; }
    public string ImageInactive { get; init; }
    public string ImageActive { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
}
