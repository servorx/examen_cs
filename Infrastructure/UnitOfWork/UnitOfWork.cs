using System;
using Application.Abstractions;
using Application.Abstractions.Auth;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Auth;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    // carga de contexto de la base de datos
    private readonly AppDbContext _context;
    // carga de interfaz de repositorios
    private IAdministradorRepository? _administradorRepository;
    private ICitaRepository? _citaRepository;
    private IClienteRepository? _clienteRepository;
    private IDetalleOrdenRepository? _detalleOrdenRepository;
    private IEstadoCitaRepository? _estadoCitaRepository;
    private IEstadoOrdenRepository? _estadoOrdenRepository;
    private IEstadoPagoRepository? _estadoPagoRepository;
    private IFacturaRepository? _facturaRepository;
    private IHistorialInventarioRepository? _historialInventarioRepository;
    private IMecanicoRepository? _mecanicoRepository;
    private IMetodoPagoRepository? _metodoPagoRepository;
    private IOrdenServicioRepository? _ordenServicioRepository;
    private IPagoRepository? _pagoRepository;
    private IProveedorRepository? _proveedorRepository;
    private IRecepcionistaRepository? _recepcionistaRepository;
    private IRepuestoRepository? _repuestoRepository;
    private ITipoMovimientoRepository? _tipoMovimientoRepository;
    private ITipoServicioRepository? _tipoServicioRepository;
    private IVehiculoRepository? _vehiculoRepository;

    // auth
    private IUserMemberService? _userMemberService;
    private IRolService? _rolService;
    // quizas esto no esta implementado
    // private IUserMemberRolService? _userMemberRoleService;

    // esto es la implementación de la IUnitOfWork con el contexto de la base de datos
    public UnitOfWork(AppDbContext context) => _context = context;
    public Task<int> SaveChanges(CancellationToken ct = default)
        => _context.SaveChangesAsync(ct);
    public async Task ExecuteInTransactionAsync(Func<CancellationToken, Task> operation, CancellationToken ct = default)
    {
        await using var tx = await _context.Database.BeginTransactionAsync(ct);
        try
        {
            await operation(ct);
            await _context.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);
        }
        catch
        {
            await tx.RollbackAsync(ct);
            throw;
        }
    }
    // public IProductRepository Products
    // {
    //     get
    //     {
    //         if (_productRepository == null)
    //         {
    //             _productRepository = new ProductRepository(_context);
    //         }
    //         return _productRepository;
    //     }
    // }
    // repositorios con las entidades
    public IAdministradorRepository Admins => _administradorRepository ??= new AdministradorRepository(_context);
    public ICitaRepository Citas => _citaRepository ??= new CitaRepository(_context);
    public IClienteRepository Clientes => _clienteRepository ??= new ClienteRepository(_context);
    public IDetalleOrdenRepository DetalleOrden => _detalleOrdenRepository ??= new DetalleOrdenRepository(_context);
    public IEstadoCitaRepository EstadoCita => _estadoCitaRepository ??= new EstadoCitaRepository(_context);
    public IEstadoOrdenRepository EstadoOrden => _estadoOrdenRepository ??= new EstadoOrdenRepository(_context);
    public IEstadoPagoRepository EstadoPago => _estadoPagoRepository ??= new EstadoPagoRepository(_context);
    public IFacturaRepository Facturas => _facturaRepository ??= new FacturaRepository(_context);
    public IHistorialInventarioRepository HistorialInventario => _historialInventarioRepository ??= new HistorialInventarioRepository(_context);
    public IMecanicoRepository Mecanicos => _mecanicoRepository ??= new MecanicoRepository(_context);
    public IMetodoPagoRepository MetodoPago => _metodoPagoRepository ??= new MetodoPagoRepository(_context);
    public IOrdenServicioRepository OrdenServicio => _ordenServicioRepository ??= new OrdenServicioRepository(_context);
    public IPagoRepository Pagos => _pagoRepository ??= new PagoRepository(_context);
    public IProveedorRepository Proveedores => _proveedorRepository ??= new ProveedorRepository(_context);
    public IRecepcionistaRepository Recepcionistas => _recepcionistaRepository ??= new RecepcionistaRepository(_context);
    public IRepuestoRepository Repuestos => _repuestoRepository ??= new RepuestoRepository(_context);
    public ITipoMovimientoRepository TipoMovimiento => _tipoMovimientoRepository ??= new TipoMovimientoRepository(_context);
    public ITipoServicioRepository TipoServicio => _tipoServicioRepository ??= new TipoServicioRepository(_context);
    public IVehiculoRepository Vehiculos => _vehiculoRepository ??= new VehiculoRepository(_context);

    // auth
    public IUserMemberService UserMembers => _userMemberService ??= new UserMemberRepository(_context);
    public IRolService Roles => _rolService ??= new RolRepository(_context);
    // quizas esto no esta implementado 
    public IUserMemberRolService UserMemberRoles => throw new NotImplementedException();

    // public IUserMemberRolService UserMemberRoles => _userMemberRolService ??= new UserMemberRolRepository(_context);
}
