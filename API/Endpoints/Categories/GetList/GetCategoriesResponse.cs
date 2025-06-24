using API.Endpoints.Categories.Dto;

namespace API.Endpoints.Categories.GetList;

public readonly record struct GetCategoriesResponse(IReadOnlyCollection<CategoryInfo> Categories);