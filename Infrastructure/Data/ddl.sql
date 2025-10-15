DROP DATABASE IF EXISTS backend_cs;
CREATE DATABASE backend_cs;
USE backend_cs;

-- =========================================================
-- USUARIOS Y ROLES
-- =========================================================
-- TENER EN CUENTA QUE ESTAS TABLAS NO SE DEBEN MODIFICAR
CREATE TABLE users_members (
    id INT PRIMARY KEY AUTO_INCREMENT,
    user_name VARCHAR(80) NOT NULL,
    email VARCHAR(80) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

CREATE TABLE roles (
    id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(20) NOT NULL,
    description VARCHAR(255) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

CREATE TABLE user_member_rol (
    user_id INT NOT NULL,
    rol_id INT NOT NULL,
    PRIMARY KEY (user_id, rol_id),
    CONSTRAINT fk_umr_user FOREIGN KEY (user_id) REFERENCES users_members(id) ON DELETE CASCADE,
    CONSTRAINT fk_umr_rol FOREIGN KEY (rol_id) REFERENCES roles(id) ON DELETE CASCADE
);

CREATE TABLE refresh_tokens (
    id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT NOT NULL,
    token TEXT NOT NULL,
    expires_at TIMESTAMP NOT NULL,
    is_revoked BOOLEAN NOT NULL DEFAULT FALSE,
    revoked_at TIMESTAMP NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_refresh_token_user FOREIGN KEY (user_id) REFERENCES users_members(id) ON DELETE CASCADE
);

-- APARTIR DE AQUÍ SE PUEDEN MODIFICAR LAS TABLAS
-- =========================================================
-- TABLAS DE APOYO PARA ESTADOS Y TIPOS
-- =========================================================
CREATE TABLE estados_cita (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE estados_orden (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE tipos_movimiento (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE metodos_pago (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE estados_pago (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL UNIQUE
);

-- EXTENSIONES DE USERS
-- ========================================================================
-- tabla de clientes
CREATE TABLE clientes (
    id INT PRIMARY KEY,
    telefono VARCHAR(20),
    direccion VARCHAR(255),
    nombre VARCHAR(100) NOT NULL,
    correo VARCHAR(255) NOT NULL,
    -- si es false no puede hacer login
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_cliente_user FOREIGN KEY (id) REFERENCES users_members(id) ON DELETE CASCADE
);

-- tabla de proveedores
CREATE TABLE proveedores (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL,
    telefono VARCHAR(20),
    correo VARCHAR(255),
    direccion VARCHAR(255),
    -- si es false no puede hacer login
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_proveedor_user FOREIGN KEY (id) REFERENCES users_members(id) ON DELETE CASCADE
);

-- tabla de mecanicos
CREATE TABLE mecanicos (
    id INT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    telefono VARCHAR(20),
    especialidad VARCHAR(60),
    -- si es false no puede hacer login
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_mecanico_user FOREIGN KEY (id) REFERENCES users_members(id) ON DELETE CASCADE
);

-- tabla de administradores
CREATE TABLE administradores (
    id INT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    telefono VARCHAR(20),
    nivel_acceso VARCHAR(50),
    area_responsabilidad VARCHAR(255),
    -- si es false no puede hacer login
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_admin_user FOREIGN KEY (id) REFERENCES users_members(id) ON DELETE CASCADE
);

-- =========================================================
-- VEHÍCULOS Y CITAS
-- =========================================================
CREATE TABLE vehiculos (
    id INT PRIMARY KEY AUTO_INCREMENT,
    cliente_id INT NOT NULL,
    marca VARCHAR(100) NOT NULL,
    modelo VARCHAR(100) NOT NULL,
    anio SMALLINT NOT NULL,
    vin VARCHAR(50) NOT NULL UNIQUE,
    kilometraje INT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_vehiculo_cliente FOREIGN KEY (cliente_id) REFERENCES clientes(id) ON DELETE CASCADE
);

CREATE TABLE citas (
    id INT PRIMARY KEY AUTO_INCREMENT,
    cliente_id INT NOT NULL,
    vehiculo_id INT NOT NULL,
    fecha_cita DATETIME NOT NULL,
    motivo VARCHAR(255),
    estado_id INT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_cita_cliente FOREIGN KEY (cliente_id) REFERENCES clientes(id) ON DELETE CASCADE,
    CONSTRAINT fk_cita_vehiculo FOREIGN KEY (vehiculo_id) REFERENCES vehiculos(id) ON DELETE CASCADE,
    CONSTRAINT fk_cita_estado FOREIGN KEY (estado_id) REFERENCES estados_cita(id)
);
-- =========================================================
-- SERVICIOS Y ORDENES
-- =========================================================
CREATE TABLE tipos_servicio (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL,
    descripcion VARCHAR(255),
    precio_base DECIMAL(10,2) NOT NULL DEFAULT 0.00
);

CREATE TABLE orden_servicio (
    id INT PRIMARY KEY AUTO_INCREMENT,
    vehiculo_id INT NOT NULL,
    mecanico_id INT NOT NULL,
    tipo_servicio_id INT NOT NULL,
    estado_id INT NOT NULL,
    fecha_ingreso TIMESTAMP NOT NULL,
    fecha_entrega_estimada TIMESTAMP NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_os_vehiculo FOREIGN KEY (vehiculo_id) REFERENCES vehiculos(id) ON DELETE CASCADE,
    CONSTRAINT fk_os_mecanico FOREIGN KEY (mecanico_id) REFERENCES mecanicos(id) ON DELETE CASCADE,
    CONSTRAINT fk_os_tipo_serv FOREIGN KEY (tipo_servicio_id) REFERENCES tipos_servicio(id),
    CONSTRAINT fk_os_estado FOREIGN KEY (estado_id) REFERENCES estados_orden(id)
);

-- =========================================================
-- INVENTARIO Y PROVEEDORES
-- =========================================================
CREATE TABLE repuestos (
    id INT PRIMARY KEY AUTO_INCREMENT,
    codigo VARCHAR(50) NOT NULL UNIQUE,
    descripcion VARCHAR(255) NOT NULL,
    cantidad_stock INT NOT NULL,
    precio_unitario DECIMAL(10,2) NOT NULL,
    proveedor_id INT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_repuesto_proveedor FOREIGN KEY (proveedor_id) REFERENCES proveedores(id) ON DELETE SET NULL
);

CREATE TABLE historial_inventario (
    id INT PRIMARY KEY AUTO_INCREMENT,
    repuesto_id INT NOT NULL,
    admin_id INT,
    tipo_movimiento_id INT NOT NULL,
    cantidad INT NOT NULL,
    fecha_movimiento TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    observaciones VARCHAR(255),
    CONSTRAINT fk_hist_repuesto FOREIGN KEY (repuesto_id) REFERENCES repuestos(id) ON DELETE CASCADE,
    CONSTRAINT fk_hist_admin FOREIGN KEY (admin_id) REFERENCES administradores(id) ON DELETE SET NULL,
    CONSTRAINT fk_hist_tipo_mov FOREIGN KEY (tipo_movimiento_id) REFERENCES tipos_movimiento(id)
);

-- =========================================================
-- DETALLES, PAGOS Y FACTURAS
-- =========================================================
CREATE TABLE detalle_orden (
    orden_servicio_id INT NOT NULL,
    repuesto_id INT NOT NULL,
    cantidad INT NOT NULL,
    costo DECIMAL(10,2) NOT NULL,
    PRIMARY KEY (orden_servicio_id, repuesto_id),
    CONSTRAINT fk_do_orden FOREIGN KEY (orden_servicio_id) REFERENCES orden_servicio(id) ON DELETE CASCADE,
    CONSTRAINT fk_do_repuesto FOREIGN KEY (repuesto_id) REFERENCES repuestos(id)
);

CREATE TABLE facturas (
    id INT PRIMARY KEY AUTO_INCREMENT,
    orden_servicio_id INT NOT NULL,
    monto_repuestos DECIMAL(10,2) NOT NULL,
    mano_obra DECIMAL(10,2) NOT NULL,
    total DECIMAL(10,2) NOT NULL,
    fecha_generacion DATETIME NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_factura_orden FOREIGN KEY (orden_servicio_id) REFERENCES orden_servicio(id)
);

CREATE TABLE pagos (
    id INT PRIMARY KEY AUTO_INCREMENT,
    factura_id INT NOT NULL,
    metodo_pago_id INT NOT NULL,
    estado_pago_id INT NOT NULL,
    monto DECIMAL(10,2) NOT NULL,
    fecha_pago TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_pago_factura FOREIGN KEY (factura_id) REFERENCES facturas(id) ON DELETE CASCADE,
    CONSTRAINT fk_pago_metodo FOREIGN KEY (metodo_pago_id) REFERENCES metodos_pago(id),
    CONSTRAINT fk_pago_estado FOREIGN KEY (estado_pago_id) REFERENCES estados_pago(id)
);
