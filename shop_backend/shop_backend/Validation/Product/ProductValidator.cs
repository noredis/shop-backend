using Microsoft.AspNetCore.JsonPatch;

namespace shop_backend.Validation.Product
{
    public static class ProductValidator
    {
        public static Result<JsonPatchDocument> ValidatePatch(JsonPatchDocument patchDocument)
        {
            foreach (var item in patchDocument.Operations)
            {
                if (string.IsNullOrEmpty(item.op))
                {
                    return Result<JsonPatchDocument>.Failure(new Error("Invalid operation"));
                }

                if (string.IsNullOrEmpty(item.path))
                {
                    return Result<JsonPatchDocument>.Failure(new Error("Invalid path"));
                }

                if (item.path == "price" && (long) item.value <= 0)
                {
                    return Result<JsonPatchDocument>.Failure(new Error("Invalid price"));
                }
            }

            return Result<JsonPatchDocument>.Success(patchDocument);
        }
    }
}
