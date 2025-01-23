using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using SalesManagementLibrary.DataAccess.Dapper;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.BaseRepo;
using SalesManagementLibrary.Repo.Interfaces;

namespace SalesManagementLibrary.Repo;

public class CustomerRepository
    : BaseRepository,
        IEntityRepository<CustomerModel, CustomerCreateDto>
{
    private readonly IDapperDataAccess _dapperDataAccess;

    public CustomerRepository(IDapperDataAccess dapperDataAccess)
    {
        _dapperDataAccess = dapperDataAccess;
    }

    // Create
    public async Task<CustomerModel?> CreateAsync(CustomerCreateDto createDto)
    {
        return await this.ExecWithErrHandling<CustomerModel?>(async () =>
        {
            var parameter = new
            {
                Name = createDto.Name,
                Email = createDto.Email,
                Phone = createDto.Phone,
                Address = createDto.Address,
                CustomerTypeId = createDto.CustomerType.Id,
                CreatedDate = DateTime.Now
            };

            List<CustomerDetailDto?> results = await _dapperDataAccess.LoadData<
                CustomerDetailDto?,
                dynamic
            >("[dbo].[CustomerInsert]", parameter, "DefaultConnection");

            var customer = this.MapDetailDtoToModel(results.FirstOrDefault());

            return customer;
        });
    }

    // Read
    public async Task<List<CustomerModel?>> GetAllAsync()
    {
        return await ExecWithErrHandling<List<CustomerModel?>>(async () =>
        {
            List<CustomerDetailDto?> customerDetailDtos =
                await this.GetAllCustomerDetailDtosAsync();
            List<CustomerModel> customers = new List<CustomerModel>();

            foreach (var item in customerDetailDtos)
            {
                customers.Add(this.MapDetailDtoToModel(item));
            }

            return customers;
        });
    }

    public async Task<CustomerModel?> GetByIdAsync(int id)
    {
        CustomerDetailDto? customerDetailDto =
            await this.GetByIdCustomerDetailDtoAsync(id);
        if (customerDetailDto == null)
        {
            return null;
        }
        var customer = this.MapDetailDtoToModel(customerDetailDto);
        return customer;
    }

    private async Task<List<CustomerDetailDto?>> GetAllCustomerDetailDtosAsync()
    {
        var spName = "[dbo].[CustomersGetAllDetails]";
        var result = await _dapperDataAccess.LoadData<CustomerDetailDto>(
            spName,
            "DefaultConnection"
        );
        return result;
    }

    // Todo**: Ask AI if await can be omitted in the following method
    private async Task<CustomerDetailDto?> GetByIdCustomerDetailDtoAsync(int id)
    {
        var parameter = new { CustomerId = id };
        List<CustomerDetailDto?> result = await _dapperDataAccess.LoadData<
            CustomerDetailDto?,
            dynamic
        >("[dbo].[CustomersGetByIdDetails]", parameter, "DefaultConnection");
        return result.FirstOrDefault();
    }

    private CustomerModel MapDetailDtoToModel(
        CustomerDetailDto customerDetailDto
    )
    {
        return new CustomerModel
        {
            Id = customerDetailDto.Id,
            Name = customerDetailDto.Name,
            Email = customerDetailDto.Email,
            Phone = customerDetailDto.Phone,
            Address = customerDetailDto.Address,
            CustomerType = new CustomerTypeModel
            {
                Id = customerDetailDto.CustomerTypeId,
                TypeName = customerDetailDto.TypeName
            },
            CreatedDate = customerDetailDto.CreatedDate,
        };
    }

    // Update
    public async Task UpdateAsync(int id, CustomerCreateDto createDto)
    {
        await ExecWithErrHandling(async () =>
        {
            var parameter = new
            {
                CustomerId = id,
                Name = createDto.Name,
                Email = createDto.Email,
                Phone = createDto.Phone,
                Address = createDto.Address,
                CustomerTypeId = createDto.CustomerType.Id,
                CreatedDate = createDto.CreatedDate
            };

            await _dapperDataAccess.SaveData<dynamic>(
                "[dbo].[CustomersUpdate]",
                parameter,
                "DefaultConnection"
            );
        });
    }

    // Delete
    public async Task DeleteAsync(int id)
    {
        await ExecWithErrHandling(async () =>
        {
            var parameter = new { CustomerId = id };
            await _dapperDataAccess.SaveData<dynamic>(
                "[dbo].[CustomerDelete]",
                parameter,
                "DefaultConnection"
            );
        });
    }
}
