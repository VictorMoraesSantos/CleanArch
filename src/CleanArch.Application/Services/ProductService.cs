using AutoMapper;
using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using CleanArch.Application.Products.Commands;
using CleanArch.Application.Products.Queries;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using MediatR;

namespace CleanArch.Application.Services
{
    public class ProductService : IProductService
    {

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        //public async Task<ProductDTO> GetProductCategory(int id)
        //{
        //    var productByIdQuery = new GetProductByIdQuery(id);
        //    if (productByIdQuery is null)
        //        throw new ApplicationException($"Entity coud not be loaded");

        //    var result = await _mediator.Send(productByIdQuery);

        //    var productDTO = _mapper.Map<ProductDTO>(result);

        //    return productDTO;
        //}

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            var productsQuery = new GetProductsQuery();
            if (productsQuery is null)
                throw new ApplicationException($"Entity coud not be loaded");

            var result = await _mediator.Send(productsQuery);

            var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(result);

            return productsDTO;
        }


        public async Task<ProductDTO> GetProductById(int id)
        {
            var productByIdQuery = new GetProductByIdQuery(id);
            if (productByIdQuery is null)
                throw new ApplicationException($"Entity coud not be loaded");

            var result = await _mediator.Send(productByIdQuery);

            var productDTO = _mapper.Map<ProductDTO>(result);

            return productDTO;
        }

        public async Task CreateProduct(ProductDTO productDTO)
        {
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);

            await _mediator.Send(productCreateCommand);
        }


        public async Task UpdateProduct(ProductDTO productDTO)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);

            await _mediator.Send(productUpdateCommand);
        }

        public async Task RemoveProduct(int id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id);
            if (productRemoveCommand is null)
                throw new ApplicationException($"Entity coud not be loaded");

            await _mediator.Send(productRemoveCommand);

        }
    }
}
