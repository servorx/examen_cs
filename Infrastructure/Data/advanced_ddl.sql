DROP DATABASE IF EXISTS backend_cs;
CREATE DATABASE backend_cs;
USE backend_cs;

-- =========================================================
-- USUARIOS Y ROLES (SIN MODIFICACIONES)
-- =========================================================
-- Base de autenticación y datos comunes
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
    name VARCHAR(20) NOT NULL UNIQUE,
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

-- =========================================================
-- EXTENSIONES DE USERS (NORMALIZADAS: NO DUPLICAN NOMBRE/CORREO/TELEFONO, NO SON AUTO_INCREMENT)
-- =========================================================
-- tabla de clientes: El cliente puede ser un usuario logueable.
CREATE TABLE clientes (
    id INT PRIMARY KEY, -- FK y PK a users_members(id)
    direccion VARCHAR(255) NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE, -- Movido aquí para ser centralizado
    telefono VARCHAR(20) NULL, -- Movido aquí para ser centralizado
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_cliente_user FOREIGN KEY (id) REFERENCES users_members(id) ON DELETE CASCADE
);

-- tabla de mecanicos: El mecánico debe ser un usuario logueable.
CREATE TABLE mecanicos (
    id INT PRIMARY KEY, -- FK y PK a users_members(id)
    especialidad VARCHAR(60) NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE, -- Movido aquí para ser centralizado
    telefono VARCHAR(20) NULL, -- Movido aquí para ser centralizado
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_mecanico_user FOREIGN KEY (id) REFERENCES users_members(id) ON DELETE CASCADE
);

-- tabla de administradores: El administrador debe ser un usuario logueable.
CREATE TABLE administradores (
    id INT PRIMARY KEY, -- FK y PK a users_members(id)
    nivel_acceso VARCHAR(50) NULL,
    area_responsabilidad VARCHAR(255) NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE, -- Movido aquí para ser centralizado
    telefono VARCHAR(20) NULL, -- Movido aquí para ser centralizado
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_admin_user FOREIGN KEY (id) REFERENCES users_members(id) ON DELETE CASCADE
);

-- tabla de proveedores: CORRECCIÓN CRÍTICA: NO ES UN USUARIO DEL SISTEMA.
CREATE TABLE proveedores (
    id INT PRIMARY KEY AUTO_INCREMENT, -- Es una entidad de negocio externa, no de autenticación
    nombre VARCHAR(100) NOT NULL,
    telefono VARCHAR(20) NULL,
    correo VARCHAR(255) NULL,
    direccion VARCHAR(255) NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
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
    -- Se elimina mecanico_id y tipo_servicio_id directo para soportar M:M
    estado_id INT NOT NULL,
    diagnostico_inicial TEXT NULL, -- Campo para la recepcionista/mecánico
    fecha_ingreso TIMESTAMP NOT NULL,
    fecha_entrega_estimada TIMESTAMP NOT NULL,
    fecha_cierre TIMESTAMP NULL, -- Cuando se finaliza el trabajo
    created_by_user_id INT NOT NULL, -- Quién creó la orden (recepcionista/admin)
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_os_vehiculo FOREIGN KEY (vehiculo_id) REFERENCES vehiculos(id) ON DELETE CASCADE,
    CONSTRAINT fk_os_estado FOREIGN KEY (estado_id) REFERENCES estados_orden(id),
    CONSTRAINT fk_os_created_by FOREIGN KEY (created_by_user_id) REFERENCES users_members(id)
);

-- Relación M:M para asignar múltiples servicios a una Orden
CREATE TABLE servicio_orden (
    orden_servicio_id INT NOT NULL,
    tipo_servicio_id INT NOT NULL,
    precio_aplicado DECIMAL(10,2) NOT NULL, -- Para registrar el precio base al momento de crear la orden
    PRIMARY KEY (orden_servicio_id, tipo_servicio_id),
    CONSTRAINT fk_so_orden FOREIGN KEY (orden_servicio_id) REFERENCES orden_servicio(id) ON DELETE CASCADE,
    CONSTRAINT fk_so_tipo_serv FOREIGN KEY (tipo_servicio_id) REFERENCES tipos_servicio(id)
);

-- Relación M:M para asignar múltiples Mecánicos a una Orden
CREATE TABLE mecanico_orden (
    orden_servicio_id INT NOT NULL,
    mecanico_id INT NOT NULL,
    es_principal BOOLEAN NOT NULL DEFAULT FALSE, -- Opcional: para identificar al jefe de la orden
    PRIMARY KEY (orden_servicio_id, mecanico_id),
    CONSTRAINT fk_mo_orden FOREIGN KEY (orden_servicio_id) REFERENCES orden_servicio(id) ON DELETE CASCADE,
    CONSTRAINT fk_mo_mecanico FOREIGN KEY (mecanico_id) REFERENCES mecanicos(id) ON DELETE CASCADE
);

-- TABLA CRÍTICA: TRAZABILIDAD DE ESTADOS
CREATE TABLE historial_orden_servicio (
    id INT PRIMARY KEY AUTO_INCREMENT,
    orden_servicio_id INT NOT NULL,
    usuario_id INT NOT NULL, -- Quién realizó el cambio (Admin/Mecánico/Recepcionista)
    estado_anterior_id INT NULL,
    estado_nuevo_id INT NOT NULL,
    observaciones TEXT NULL,
    fecha_cambio TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_hos_orden FOREIGN KEY (orden_servicio_id) REFERENCES orden_servicio(id) ON DELETE CASCADE,
    CONSTRAINT fk_hos_usuario FOREIGN KEY (usuario_id) REFERENCES users_members(id),
    CONSTRAINT fk_hos_estado_ant FOREIGN KEY (estado_anterior_id) REFERENCES estados_orden(id),
    CONSTRAINT fk_hos_estado_nue FOREIGN KEY (estado_nuevo_id) REFERENCES estados_orden(id)
);

-- TABLA CRÍTICA: DETALLE DEL TRABAJO Y CÁLCULO DE MANO DE OBRA
CREATE TABLE registro_trabajo (
    id INT PRIMARY KEY AUTO_INCREMENT,
    orden_servicio_id INT NOT NULL,
    mecanico_id INT NOT NULL,
    descripcion_tarea TEXT NOT NULL,
    horas_invertidas DECIMAL(4,2) NOT NULL,
    tarifa_hora_aplicada DECIMAL(10,2) NOT NULL, -- Tarifa del mecánico/taller al momento del registro
    fecha_registro TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_rt_orden FOREIGN KEY (orden_servicio_id) REFERENCES orden_servicio(id) ON DELETE CASCADE,
    CONSTRAINT fk_rt_mecanico FOREIGN KEY (mecanico_id) REFERENCES mecanicos(id)
);

-- =========================================================
-- INVENTARIO Y PROVEEDORES
-- =========================================================
CREATE TABLE repuestos (
    id INT PRIMARY KEY AUTO_INCREMENT,
    codigo VARCHAR(50) NOT NULL UNIQUE,
    descripcion VARCHAR(255) NOT NULL,
    cantidad_stock INT NOT NULL DEFAULT 0,
    precio_unitario_compra DECIMAL(10,2) NOT NULL, -- Costo de adquisición (para informes de margen)
    precio_unitario_venta DECIMAL(10,2) NOT NULL, -- Precio al público
    proveedor_id INT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_repuesto_proveedor FOREIGN KEY (proveedor_id) REFERENCES proveedores(id) ON DELETE SET NULL
);

CREATE TABLE historial_inventario (
    id INT PRIMARY KEY AUTO_INCREMENT,
    repuesto_id INT NOT NULL,
    admin_id INT, -- El administrador/usuario que hizo el movimiento
    tipo_movimiento_id INT NOT NULL,
    cantidad INT NOT NULL,
    costo_afectado DECIMAL(10,2) NOT NULL, -- Costo unitario al momento del movimiento (importante para contabilidad)
    referencia_orden_id INT NULL, -- Opcional: si el movimiento fue por una Orden de Servicio
    fecha_movimiento TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    observaciones VARCHAR(255) NULL,
    CONSTRAINT fk_hist_repuesto FOREIGN KEY (repuesto_id) REFERENCES repuestos(id) ON DELETE CASCADE,
    CONSTRAINT fk_hist_admin FOREIGN KEY (admin_id) REFERENCES administradores(id) ON DELETE SET NULL,
    CONSTRAINT fk_hist_tipo_mov FOREIGN KEY (tipo_movimiento_id) REFERENCES tipos_movimiento(id),
    CONSTRAINT fk_hist_orden_ref FOREIGN KEY (referencia_orden_id) REFERENCES orden_servicio(id) ON DELETE SET NULL
);

-- =========================================================
-- DETALLES, PAGOS Y FACTURAS
-- =========================================================
CREATE TABLE detalle_orden (
    orden_servicio_id INT NOT NULL,
    repuesto_id INT NOT NULL,
    cantidad INT NOT NULL,
    -- CORRECCIÓN CRÍTICA: Se guarda el precio del repuesto al momento de usarlo/facturar
    precio_unitario_aplicado DECIMAL(10,2) NOT NULL,
    PRIMARY KEY (orden_servicio_id, repuesto_id),
    CONSTRAINT fk_do_orden FOREIGN KEY (orden_servicio_id) REFERENCES orden_servicio(id) ON DELETE CASCADE,
    CONSTRAINT fk_do_repuesto FOREIGN KEY (repuesto_id) REFERENCES repuestos(id)
);

CREATE TABLE facturas (
    id INT PRIMARY KEY AUTO_INCREMENT,
    orden_servicio_id INT NOT NULL UNIQUE,
    monto_repuestos DECIMAL(10,2) NOT NULL,
    mano_obra DECIMAL(10,2) NOT NULL,
    impuestos DECIMAL(10,2) NOT NULL DEFAULT 0.00, -- Añadido para manejo financiero completo
    total DECIMAL(10,2) NOT NULL,
    fecha_generacion DATETIME NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_factura_orden FOREIGN KEY (orden_servicio_id) REFERENCES orden_servicio(id) ON DELETE CASCADE
);

CREATE TABLE pagos (
    id INT PRIMARY KEY AUTO_INCREMENT,
    factura_id INT NOT NULL,
    metodo_pago_id INT NOT NULL,
    estado_pago_id INT NOT NULL,
    monto DECIMAL(10,2) NOT NULL,
    fecha_pago TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_SECOND,
    CONSTRAINT fk_pago_factura FOREIGN KEY (factura_id) REFERENCES facturas(id) ON DELETE CASCADE,
    CONSTRAINT fk_pago_metodo FOREIGN KEY (metodo_pago_id) REFERENCES metodos_pago(id),
    CONSTRAINT fk_pago_estado FOREIGN KEY (estado_pago_id) REFERENCES estados_pago(id)
);

-- =========================================================
-- AUDITORÍA GENERAL (CRÍTICO PARA TRAZABILIDAD DEL SISTEMA)
-- =========================================================
CREATE TABLE auditorias (
    id INT PRIMARY KEY AUTO_INCREMENT,
    usuario_id INT NULL, -- Quién realizó la acción (NULL si es una acción de sistema)
    entidad_afectada VARCHAR(50) NOT NULL, -- Nombre de la tabla ('clientes', 'repuestos', 'orden_servicio')
    registro_id INT NOT NULL, -- El ID de la fila afectada
    tipo_accion VARCHAR(10) NOT NULL, -- 'CREATE', 'UPDATE', 'DELETE'
    detalles_cambio JSON NULL, -- Opcional: Almacenar la data del cambio (JSON/TEXT)
    fecha_accion TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_auditoria_usuario FOREIGN KEY (usuario_id) REFERENCES users_members(id) ON DELETE SET NULL
);