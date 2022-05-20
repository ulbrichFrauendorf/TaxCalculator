namespace TaxCalculatorAPI.Authorization;

using AutoMapper;
using BCrypt.Net;
using TaxCalculatorAPI.Authorization.Middleware;
using TaxCalculatorAPI.Data;
using TaxCalculatorAPI.Data.Entities;
using TaxCalculatorAPI.ExceptionHandlers;

public interface IUserService
{
    AuthResponse Authenticate(AuthRequest model);
    IEnumerable<ApiUser> GetAll();
    ApiUser GetById(int id);
    void Register(AuthRequest model);
    void Update(int id, AuthRequest model);
    void Delete(int id);
}

public class UserService : IUserService
{
    private SQLDatacontext _context;
    private IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IMapper _mapper;

    public UserService(SQLDatacontext context,
        IJwtTokenGenerator jwtTokenGenerator,
        IMapper mapper)
    {
        _context = context;
        _jwtTokenGenerator = jwtTokenGenerator;
        _mapper = mapper;
    }

    public AuthResponse Authenticate(AuthRequest model)
    {
        var user = _context.ApiUsers.SingleOrDefault(x => x.Email == model.Email);

        // validate
        if (user == null || !BCrypt.Verify(model.Password, user.Password))
            throw new ApiException("Username or password is incorrect");

        // authentication successful
        var response = _mapper.Map<AuthResponse>(user);
        response.Token = _jwtTokenGenerator.Generate(user);
        return response;
    }

    public IEnumerable<ApiUser> GetAll()
    {
        return _context.ApiUsers;
    }

    public ApiUser GetById(int id)
    {
        return getUser(id);
    }

    public void Register(AuthRequest model)
    {
        if (_context.ApiUsers.Any(x => x.Email == model.Email))
            throw new ApiException("Username '" + model.Email + "' is already taken");

        var user = _mapper.Map<ApiUser>(model);

        user.Password = BCrypt.HashPassword(model.Password);

        _context.ApiUsers.Add(user);
        _context.SaveChanges();
    }

    public void Update(int id, AuthRequest model)
    {
        var user = getUser(id);

        if (model.Email != user.Email && _context.ApiUsers.Any(x => x.Email == model.Email))
            throw new ApiException("Username '" + model.Email + "' is already taken");

        if (!string.IsNullOrEmpty(model.Password))
            user.Password = BCrypt.HashPassword(model.Password);

        _mapper.Map(model, user);
        _context.ApiUsers.Update(user);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = getUser(id);
        _context.ApiUsers.Remove(user);
        _context.SaveChanges();
    }

    // helper methods

    private ApiUser getUser(int id)
    {
        var user = _context.ApiUsers.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }
}

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // User -> AuthenticateResponse
        CreateMap<ApiUser, AuthResponse>();

        // RegisterRequest -> User
        CreateMap<AuthRequest, ApiUser>();
    }
}