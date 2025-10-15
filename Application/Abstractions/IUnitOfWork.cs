using System;
using Application.Abstractions.Auth;

namespace Application.Abstractions;

public interface IUnitOfWork
{
    // colocar todos los repositorios
    IAdministradorRepository Admins { get; }
    ICitaRepository Citas { get; }
    IClienteRepository Clientes { get; }
    IDetalleOrdenRepository DetalleOrden { get; }
    IEstadoCitaRepository EstadoCita { get; }
    IEstadoOrdenRepository EstadoOrden { get; }
    IEstadoPagoRepository EstadoPago { get; }
    IFacturaRepository Facturas { get; }
    IHistorialInventarioRepository HistorialInventario { get; }
    IMecanicoRepository Mecanicos { get; }
    IMetodoPagoRepository MetodoPago { get; }
    IOrdenServicioRepository OrdenServicio { get; }
    IPagoRepository Pagos { get; }
    IProveedorRepository Proveedores { get; }
    IRepuestoRepository Repuestos { get; }
    ITipoMovimientoRepository TipoMovimiento { get; }
    ITipoServicioRepository TipoServicio { get; }
    IVehiculoRepository Vehiculos { get; }
    // auth
    IUserMemberService UserMembers { get; }
    IUserMemberRolService UserMemberRoles { get; }
    IRolService Roles { get; }
    // Task<int> SaveAsync();
    Task<int> SaveChanges(CancellationToken ct = default);
    Task ExecuteInTransactionAsync(Func<CancellationToken, Task> operation, CancellationToken ct = default);
}
