using Domain.Entities;
using Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // Entidades de autenticación
    public DbSet<UserMember> UsersMembers => Set<UserMember>();
    public DbSet<Rol> Roles => Set<Rol>();
    public DbSet<UserMemberRol> UserMemberRols => Set<UserMemberRol>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    // Entidades principales
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Vehiculo> Vehiculos => Set<Vehiculo>();
    public DbSet<Cita> Citas => Set<Cita>();
    public DbSet<OrdenServicio> OrdenesServicio => Set<OrdenServicio>();
    public DbSet<Repuesto> Repuestos => Set<Repuesto>();
    public DbSet<HistorialInventario> HistorialesInventario => Set<HistorialInventario>();
    public DbSet<DetalleOrden> DetallesOrden => Set<DetalleOrden>();
    public DbSet<Factura> Facturas => Set<Factura>();
    public DbSet<Pago> Pagos => Set<Pago>();

    // Tablas de apoyo
    public DbSet<EstadoCita> EstadosCita => Set<EstadoCita>();
    public DbSet<EstadoOrden> EstadosOrden => Set<EstadoOrden>();
    public DbSet<EstadoPago> EstadosPago => Set<EstadoPago>();
    public DbSet<TipoMovimiento> TiposMovimiento => Set<TipoMovimiento>();
    public DbSet<MetodoPago> MetodosPago => Set<MetodoPago>();
    public DbSet<TipoServicio> TiposServicio => Set<TipoServicio>();

    // Extensiones de usuario
    public DbSet<Administrador> Administradores => Set<Administrador>();
    public DbSet<Mecanico> Mecanicos => Set<Mecanico>();
    public DbSet<Proveedor> Proveedores => Set<Proveedor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
    }
}
