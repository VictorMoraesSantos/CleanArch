using CleanArch.Application.Products.Commands;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using MediatR;

namespace CleanArch.Application.Products.Handlers.CommandHandlers
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product>
    {
        private readonly IProductRepository _repository;

        public ProductCreateCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> Handle(ProductCreateCommand request,
                                          CancellationToken cancellationToken)
        {
            var product = new Product(request.Name!, request.Description!,
                                      request.Price, request.Stock, request.Image!);

            if (product is null)
                throw new ApplicationException($"Error creating entity");

            product.CategoryId = request.CategoryId;
            return await _repository.CreateAsync(product);
        }
    }
}
